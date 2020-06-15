using Desafio.Domain.Domain;
using Desafio.Domain.Repository;
using Desafio.Domain.Seletores;
using Desafio.Database.Entities;
using Desafio.Database.Interface;
using Desafio.Database.Repository.Abstract;
using System.Linq;

namespace Desafio.Database.Repository
{
    public class TransactionRepository : RepositoryBase<TransactionEntity, TransactionDomain, TransactionSeletor>, ITransactionRepository
    {
        public TransactionRepository(IDesafioContext context) : base(context) { }
        
        public override IQueryable<TransactionEntity> CreateParameters(TransactionSeletor seletor, IQueryable<TransactionEntity> query)
        {
            // here is a example of the usage of the selector, where we check if there is a value on the selector
            // and we use it to query the data with it
            if (seletor.date != null)
            { // brotip: the date on the selector is nullable, so we can check against that value
                query = query.Where(x => x.date == seletor.date);
            }
            return query;
        }

    }
}
