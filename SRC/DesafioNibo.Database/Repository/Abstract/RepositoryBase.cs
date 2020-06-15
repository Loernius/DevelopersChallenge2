using AutoMapper;
using Desafio.Database.Entities.Base;
using Desafio.Database.Interface;
using Desafio.Domain.Domain.Base;
using Desafio.Domain.Seletores.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Desafio.Database.Repository.Abstract
{
    public abstract class RepositoryBase<TEntity, TDomain, TSeletor>
        where TEntity : EntityBase
        where TDomain : DomainBase
        where TSeletor : SeletorBase
    { // here we have the responsible for the interaction with the database
        // it was used the Mapper to map in and out from domain to entity.
        // The specialized repository implements the CreateParameters function, that uses the selector
        // to query the db
        protected readonly IDesafioContext context;
        private DbSet<TEntity> dbSet;

        public RepositoryBase(IDesafioContext _context)
        {
            this.context = _context;
        }
        protected virtual IQueryable<TEntity> Query()
        {
            return this.context.Set<TEntity>().AsNoTracking().AsQueryable();
        }
        public virtual bool Insert(TDomain obj)
        {
            try
            {
                TEntity entity = Mapper.Map<TEntity>(obj);
                context.Set<TEntity>().Add(entity);
                this.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Erro ao salvar registro: " + ex);
            }
        }

        public virtual IEnumerable<TDomain> InsertManyReturningObject(List<TDomain> items)
        {
            var insertedList = new List<TEntity>();
            foreach (var item in items)
            {
                var entity = Mapper.Map<TEntity>(item);
                insertedList.Add(entity);
                context.Set<TEntity>().Add(entity);
            }
            this.Save();

            return insertedList.Select(x => Mapper.Map<TDomain>(x));
        }

        public virtual TDomain InsertWithReturningObject(TDomain obj)
        {
            TEntity entity = Mapper.Map<TEntity>(obj);
            context.Set<TEntity>().Add(entity);

            this.Save();
            return Mapper.Map<TDomain>(entity);
        }

        //public virtual TEntity InsertWithReturningId(TDomain obj)
        //{
        //    TEntity entity = MapperToEntity(obj);
        //    _context.Set<TEntity>().Add(entity);
        //    this.Save();
        //    return entity;
        //}


        //public virtual IEnumerable<TEntity> InsertMany(IEnumerable<TDomain> items)
        //{
        //    var insertedList = new List<TEntity>();
        //    foreach (var item in items)
        //    {
        //        var entity = MapperToEntity(item);
        //        insertedList.Add(entity);
        //        _context.Set<TEntity>().Add(entity);
        //    }
        //    Save();
        //    return insertedList.Select(x => x.ID);
        //}

        /// <summary>
        /// Method for update
        /// </summary>
        /// <param name="obj"></param>
        public bool Update(TDomain obj)
        {
            try
            {
                TEntity entity = MapperToEntity(obj);
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Erro ao atualizar registros: " + ex);
            }

        }

        public virtual TDomain UpdateWithReturningObject(TDomain obj)
        {
            TEntity entity = Mapper.Map<TEntity>(obj);
            context.Entry(entity);
            context.Set<TEntity>().Attach(entity).State = EntityState.Modified;
            context.SaveChanges();

            return Mapper.Map<TDomain>(entity);
        }

        public virtual IEnumerable<TDomain> UpdateManyReturningObject(List<TDomain> items)
        {
            var updatedList = new List<TEntity>();

            foreach (var item in items)
            {
                var entity = Mapper.Map<TEntity>(item);
                updatedList.Add(entity);
                context.Entry(entity);
                context.Set<TEntity>().Attach(entity).State = EntityState.Modified;
            }

            context.SaveChanges();

            return updatedList.Select(x => Mapper.Map<TDomain>(x));
        }

        /// <summary>
        /// Method for delete
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Delete(TDomain obj)
        {
            TEntity entity = Mapper.Map<TEntity>(obj);
            context.Entry(entity).State = EntityState.Deleted;
            this.Save();
        }

        public virtual void DeleteMany(IEnumerable<TDomain> items)
        {
            foreach (var item in items)
            {
                var entity = Mapper.Map<TEntity>(item);
                context.Entry(entity).State = EntityState.Deleted;
            }
            this.Save();
        }

        /// <summary>
        /// Return a list of entity
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TDomain> Get()
        {
            var query = context.Set<TEntity>()
                .Select(x => Mapper.Map<TDomain>(x))
                .AsQueryable();

            return query;
        }

        /// <summary>
        /// Return a entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public virtual TDomain GetById(TDomain domain)
        //{
        //    TDomain obj = MapperToDomain(_context.Set<TEntity>()
        //        .AsNoTracking()
        //        .Where(x => x == domain)
        //        .SingleOrDefault());
        //    return obj;
        //}

        /// <summary>
        /// Verify if exist objects
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected virtual bool Exist(Expression<Func<TEntity, bool>> expression)
        {
            var query = context.Set<TEntity>()
                .Where(expression)
                .Count();

            if (query > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Save actions
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }



        protected virtual TEntity MapperToEntity(object dto)
            => MapperToEntity<TEntity>(dto);

        protected virtual T MapperToEntity<T>(object dto) where T : EntityBase
        {
            T entity = (T)Activator.CreateInstance(typeof(T));
            Type entityTypes = entity.GetType();

            PropertyInfo[] propertyInfo = dto.GetType().GetProperties();
            foreach (PropertyInfo property in propertyInfo)
            {
                var value = property.GetValue(dto, null);
                var objectProperty = entityTypes.GetProperty(property.Name);

                if (objectProperty != null && objectProperty.PropertyType == property.PropertyType)
                {
                    objectProperty.SetValue(entity, value);
                }
            }

            return entity;
        }

        protected static TDomain MapperToDomain(TEntity entity)
        {
            return Mapper.Map<TDomain>(entity);
        }

        public IQueryable<TEntity> CreateOrder(SeletorBase seletor, IQueryable<TEntity> query)
        {
            try
            {
                if (seletor.OrderBy != null)
                {
                    query = query.OrderBy(y => 1);

                    string[] fields = seletor.OrderBy.Split(',');

                    foreach (string fieldWithOrder in fields)
                    {

                        string[] fieldParam = fieldWithOrder.Split(' ');

                        OrderBy order = fieldParam.Length > 1 ? (OrderBy)Enum.Parse(typeof(OrderBy), fieldParam[1]) : seletor.OrderByOrder;

                        string orderBy = "ThenBy";
                        if (order == OrderBy.DESC)
                        {
                            orderBy = "ThenByDescending";
                        }

                        ParameterExpression x = Expression.Parameter(query.ElementType, "x");
                        LambdaExpression exp = Expression.Lambda(Expression.PropertyOrField(x, fieldParam[0].Trim()), x);
                        query = (IQueryable<TEntity>)query.Provider.CreateQuery(Expression.Call(typeof(Queryable), orderBy, new Type[] {
                    query.ElementType, exp.Body.Type
                }, query.Expression, exp));
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return query;
        }

        public virtual IEnumerable<TDomain> GetList(TSeletor seletor)
        {

            IQueryable<TEntity> query = this.Query();

            query = this.CreateParameters(seletor, query);
            query = this.CreateOrder(seletor, query);
            //query = this.CreateLimit(seletor, query);

            return query.Select(r => Mapper.Map<TDomain>(r));
        }

        public virtual TDomain GetOne(TSeletor seletor)
        {
            var query = context.Set<TEntity>().AsQueryable();
            query = this.CreateParameters(seletor, query);

            return Mapper.Map<TDomain>(query.FirstOrDefault());
        }

        public virtual IEnumerable<TDomain> GetListWithManyParams(IEnumerable<TSeletor> seletores)
        {
            List<TEntity> queryList = new List<TEntity>();
            foreach (var seletor in seletores)
            {
                IQueryable<TEntity> query = this.Query();

                query = this.CreateParameters(seletor, query);
                query = this.CreateOrder(seletor, query);
                query = this.CreateLimit(seletor, query);

                queryList.AddRange(query);
            }

            return queryList.Select(r => Mapper.Map<TDomain>(r));
        }

        public IQueryable<TEntity> CreateLimit(SeletorBase seletor, IQueryable<TEntity> query)
        {
            if (seletor.Pagina < 0)
            {
                seletor.Pagina = 1;
            }
            int skip = ((seletor.Pagina - 1) * seletor.RegistroPorPagina);
            int take = seletor.RegistroPorPagina;
            return query.Skip(skip).Take(take);

        }

        public int Count(TSeletor seletor)
        {
            var query = Query();
            query = CreateParameters(seletor, query);

            return query.Count();
        }

        public abstract IQueryable<TEntity> CreateParameters(TSeletor seletor, IQueryable<TEntity> query);
    }
}
