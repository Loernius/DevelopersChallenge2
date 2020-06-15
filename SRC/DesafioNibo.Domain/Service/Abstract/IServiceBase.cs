using Desafio.Domain.Domain.Base;
using Desafio.Domain.Seletores.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Domain.Service.Abstract
{
    public interface IServiceBase<TDomain, TSeletor> where TDomain : DomainBase, new() where TSeletor : SeletorBase
    {
        bool Insert(TDomain domain);
        TDomain InsertWithReturningObject(TDomain domain);
        IEnumerable<TDomain> InsertManyReturningObject(List<TDomain> items);
        bool Update(TDomain domain);
        IEnumerable<TDomain> Get();
        IEnumerable<TDomain> GetList(TSeletor seletor);
        TDomain UpdateWithReturningObject(TDomain domain);
        IEnumerable<TDomain> UpdateManyReturningObject(List<TDomain> items);
        IEnumerable<TDomain> GetListWithManyParams(IEnumerable<TSeletor> seletores);
        TDomain GetOne(TSeletor seletor);

        int Count(TSeletor seletor);
    }
}
