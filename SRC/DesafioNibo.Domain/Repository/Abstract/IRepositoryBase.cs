using Desafio.Domain.Domain.Base;
using Desafio.Domain.Seletores.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desafio.Domain.Repository.Abstract
{
    public interface IRepositoryBase<TDomain, TSeletor>
        where TDomain: DomainBase
        where TSeletor: SeletorBase
    {
        TDomain InsertWithReturningObject(TDomain obj);

        bool Insert(TDomain obj);

        IEnumerable<TDomain> InsertManyReturningObject(List<TDomain> items);

        //IEnumerable<int> InsertMany(IEnumerable<TDomain> producoes);

        bool Update(TDomain obj);

        TDomain UpdateWithReturningObject(TDomain obj);

        IEnumerable<TDomain> UpdateManyReturningObject(List<TDomain> items);

        void Delete(TDomain obj);

        void DeleteMany(IEnumerable<TDomain> items);

        IQueryable<TDomain> Get();

        IEnumerable<TDomain> GetList(TSeletor seletor);
        //TDomain GetById(long id);

        TDomain GetOne(TSeletor seletor);

        void Save();

        IEnumerable<TDomain> GetListWithManyParams(IEnumerable<TSeletor> seletores);

        int Count(TSeletor seletor);
    }
}
