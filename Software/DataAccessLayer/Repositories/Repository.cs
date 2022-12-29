﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public abstract class Repository<T> : IDisposable where T : class
    {
        protected AutoPrimeModel Context { get; set; }
        public DbSet<T> Entities { get; set; }

        public Repository(AutoPrimeModel context)
        {
            Context = context;
            Entities = context.Set<T>();
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            var query = from e in Entities
                        select e;

            return query;
        }

        public virtual int Add(T entity, bool saveChanges = true)
        {
            Entities.Add(entity);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public virtual int Remove(T entity, bool saveChanges = true)
        {
            Entities.Attach(entity);
            Entities.Remove(entity);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public abstract int Update(T entity, bool saveChanges = true);

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
