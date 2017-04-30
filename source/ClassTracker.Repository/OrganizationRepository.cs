using KadGen.ClassTracker.Domain;
using KadGen.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace KadGen.ClassTracker.Repository
{
    public class OrganizationRepository
    {
        protected Func<EfOrganization, Organization> _mapEntityToDomain { get; }
        protected Func<Organization, EfOrganization> _mapDomainToEntity { get; }

        public OrganizationRepository()
        {
            _mapEntityToDomain = Mapper.MapEntityToDomain;
            _mapDomainToEntity = Mapper.MapDomainToEntity;
        }

        public DataResult<Organization> GetAsync(int id)
        {
            try
            {
                using (var dbContext = new ClassTrackerDbContext())
                {
                    var entity = dbContext
                            .Organizations
                            .Where(x => x.Id == id)
                            .SingleOrDefault();
                    var domain = _mapEntityToDomain(entity);
                    return DataResult<Organization>.CreateSuccessResult(domain);
                }
            }
            catch (Exception ex)
            {
                return DataResult<Organization>.CreateErrorResult(
                    ErrorCode.ExceptionThrown, ex, null);
            }
        }


        public DataResult<List<Organization>> GetAllAsync()
        {
            try
            {
                using (var dbContext = new ClassTrackerDbContext())
                {
                    var entities = dbContext
                            .Organizations
                            .ToList();
                    var domains = entities.Select(x => _mapEntityToDomain(x));
                    return DataResult<List<Organization>>.CreateSuccessResult(domains.ToList());
                }
            }
            catch (Exception ex)
            {
                return DataResult<List<Organization>>.CreateErrorResult(
                    ErrorCode.ExceptionThrown, ex, null);
            }
        }

        // TODO: Figure out how to map filters from domain to EF
        //public DataResult<List<Organization>> GetAllWhereAsync(
        //        Expression<Func<EfOrganization, bool>> filterFunc)
        //{
        //    try
        //    {
        //        using (var dbContext = new ClassTrackerDbContext())
        //        {
        //            var entities = dbContext
        //                    .Organizations
        //                    .Where(filterFunc);
        //            var domains = entities.Select(x => MapEntityToDomain(x));
        //            return DataResult<List<Organization>>.CreateSuccessResult(domains.ToList());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return DataResult<List<Organization>>.CreateErrorResult(
        //            ErrorCode.ExceptionThrown, ex, null);
        //    }
        //}

        public DataResult<int?> CreateAsync(Organization domain)
        {
            try
            {
                var entity = _mapDomainToEntity(domain);
                using (var dbContext = new ClassTrackerDbContext())
                {
                    dbContext.Organizations.Add(entity);
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

        public Result UpdateAsync(Organization domain)
        {
            try
            {
                var entity = _mapDomainToEntity(domain);
                using (var dbContext = new ClassTrackerDbContext())
                {
                    dbContext.Organizations.Attach(entity);
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

        public Result DeleteAsync(Organization domain)
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
            public static Organization MapEntityToDomain(EfOrganization entity)
            {
                return new Organization(entity.Id, entity.Name);
            }

            public static EfOrganization MapDomainToEntity(Organization domain)
            {
                var entity = new EfOrganization();
                entity.Id = domain.Id;
                entity.Name = domain.Name;
                return entity;
            }
        }
    }
}
