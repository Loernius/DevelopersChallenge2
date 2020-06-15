using Desafio.Database.Entities.Base;
using Desafio.Domain.Domain.Base;
using Desafio.Domain.Repository.Abstract;
using Desafio.Domain.Seletores.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Service.Abstract
{
    public abstract class ServiceBase<TDomain, TEntity, TSeletor, TRepository>
        where TDomain : DomainBase,
        new() where TEntity : EntityBase
        where TSeletor : SeletorBase
        where TRepository : IRepositoryBase<TDomain, TSeletor>
    { // here we have a generic monster CRUD, previously mounted, and now agilizing every C# project
        // each of those will call its correspondent from the RepositoryBase.cs
        // if we need more expecific service, we need to create a domain for that. 
        protected readonly TRepository _repository;

        public ServiceBase(TRepository repository)
        {
            _repository = repository;
        }

        public bool Insert(TDomain domain)
        {
            return _repository.Insert(domain);
        }

        public TDomain InsertWithReturningObject(TDomain domain)
        {
            return _repository.InsertWithReturningObject(domain);
        }

        public IEnumerable<TDomain> InsertManyReturningObject(List<TDomain> items)
        {
            return _repository.InsertManyReturningObject(items);
        }

        public bool Update(TDomain domain)
        {
            return _repository.Update(domain);
        }

        public TDomain UpdateWithReturningObject(TDomain domain)
        {
            return _repository.UpdateWithReturningObject(domain);
        }

        public IEnumerable<TDomain> UpdateManyReturningObject(List<TDomain> items)
        {
            return _repository.UpdateManyReturningObject(items);
        }

        public IEnumerable<TDomain> Get()
        {
            return _repository.Get();
        }

        public IEnumerable<TDomain> GetList(TSeletor seletor)
        {
            return _repository.GetList(seletor);
        }

        public IEnumerable<TDomain> GetListWithManyParams(IEnumerable<TSeletor> seletores)
        {
            return _repository.GetListWithManyParams(seletores);
        }

        public TDomain GetOne(TSeletor seletor)
        {
            return _repository.GetOne(seletor);
        }

        public int Count(TSeletor seletor)
        {
            return _repository.Count(seletor);
        }
    }
}
