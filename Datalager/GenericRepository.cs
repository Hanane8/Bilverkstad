using DataLager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Datalager
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private EntityFramework _dbContext;

        public GenericRepository(EntityFramework dbContext)
        {
            _dbContext = dbContext;
        }
        public void LäggTill(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Uppdatera(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        //public TEntity HämtaPerson(string personnummer)
        //{
        //    return _dbContext.Set<TEntity>().FirstOrDefault(k => k.Personnummer);
        //}

        public List<TEntity> HämtaLista(Expression<Func<TEntity, bool>> funcExpression)
        {
            return _dbContext.Set<TEntity>().Where(funcExpression).ToList();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> funcExpression)
        {
            return _dbContext.Set<TEntity>().SingleOrDefault(funcExpression);
        }

        public List<TEntity> Hämta() =>_dbContext.Set<TEntity>().ToList();
        
    }
}
