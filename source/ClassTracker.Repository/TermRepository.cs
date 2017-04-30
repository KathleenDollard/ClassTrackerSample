using KadGen.ClassTracker.Domain;
using KadGen.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace KadGen.ClassTracker.Repository
{
    public class TermRepository
    {
        protected Func<EfTerm, Term> _mapEntityToDomain { get; }
        protected Func<Term, EfTerm> _mapDomainToEntity { get; }

        public TermRepository()
        {
            _mapEntityToDomain = Mapper.MapEntityToDomain;
            _mapDomainToEntity = Mapper.MapDomainToEntity;
        }

        public DataResult<Term> GetAsync(int id)
        {
            try
            {
                using (var dbContext = new ClassTrackerDbContext())
                {
                    var entity = dbContext
                            .Terms
                            .Where(x => x.Id == id)
                            .SingleOrDefault();
                    var domain = _mapEntityToDomain(entity);
                    return DataResult<Term>.CreateSuccessResult(domain);
                }
            }
            catch (Exception ex)
            {
                return DataResult<Term>.CreateErrorResult(
                    ErrorCode.ExceptionThrown, ex, null);
            }
        }


        public DataResult<List<Term>> GetAllAsync()
        {
            try
            {
                using (var dbContext = new ClassTrackerDbContext())
                {
                    var entities = dbContext
                            .Terms;
                    var domains = entities.Select(x => _mapEntityToDomain(x));
                    return DataResult<List<Term>>.CreateSuccessResult(domains.ToList());
                }
            }
            catch (Exception ex)
            {
                return DataResult<List<Term>>.CreateErrorResult(
                    ErrorCode.ExceptionThrown, ex, null);
            }
        }

        // TODO: Figure out how to map filters from domain to EF
        //public DataResult<List<Term>> GetAllWhereAsync(
        //        Expression<Func<EfTerm, bool>> filterFunc)
        //{
        //    try
        //    {
        //        using (var dbContext = new ClassTrackerDbContext())
        //        {
        //            var entities = dbContext
        //                    .Terms
        //                    .Where(filterFunc);
        //            var domains = entities.Select(x => MapEntityToDomain(x));
        //            return DataResult<List<Term>>.CreateSuccessResult(domains.ToList());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return DataResult<List<Term>>.CreateErrorResult(
        //            ErrorCode.ExceptionThrown, ex, null);
        //    }
        //}

        public DataResult<int?> CreateAsync(Term domain)
        {
            try
            {
                var entity = _mapDomainToEntity(domain);
                using (var dbContext = new ClassTrackerDbContext())
                {
                    dbContext.Terms.Add(entity);
                    dbContext.SaveChanges();
                    return DataResult<int?>.CreateSuccessResult(entity.Id);
                }
            }
            catch (Exception ex)
            {
                return DataResult<int?>.CreateErrorResult(
                    ErrorCode.ExceptionThrown, ex, null);
            }
        }

        public Result UpdateAsync(Term domain)
        {
            try
            {
                var entity = _mapDomainToEntity(domain);
                using (var dbContext = new ClassTrackerDbContext())
                {
                    dbContext.Terms.Attach(entity);
                    dbContext.SaveChanges();
                    return Result.CreateSuccessResult();
                }
            }
            catch (Exception ex)
            {
                return Result.CreateErrorResult(
                    ErrorCode.ExceptionThrown, ex, null);
            }
        }

        public Result DeleteAsync(Term domain)
        {
            try
            {
                var entity = _mapDomainToEntity(domain);
                using (var dbContext = new ClassTrackerDbContext())
                {
                    var entry = dbContext.Entry(entity);
                    entry.State = EntityState.Deleted;
                    dbContext.SaveChanges();
                    return Result.CreateSuccessResult();
                }
            }
            catch (Exception ex)
            {
                return Result.CreateErrorResult(
                    ErrorCode.ExceptionThrown, ex, null);
            }
        }

        private class Mapper
        {
            public static Term MapEntityToDomain(EfTerm entity)
            {
                // TODO: Figure out how to share mappings, like here for org
                return new Term(entity.Id, null, entity.Name, entity.StartDate, entity.EndDate);
            }

            public static EfTerm MapDomainToEntity(Term domain)
            {
                var entity = new EfTerm();
                entity.Id = domain.Id;
                entity.Name = domain.Name;
                return entity;
            }
        }
    }
}
