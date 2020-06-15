using Desafio.Domain.Domain;
using Desafio.Domain.Repository.Abstract;
using Desafio.Domain.Seletores;

namespace Desafio.Domain.Repository
{
    public interface ITransactionRepository: IRepositoryBase<TransactionDomain, TransactionSeletor>
    {
    }
}
