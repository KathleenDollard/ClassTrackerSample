using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EntityFramework
{
    public abstract class Mapper<TDomain, TEntity>
    {
        protected abstract TEntity MapEntityToDomain(TDomain domain);
        protected abstract TDomain MapEntityToDomain(TEntity entity);
    }
}
