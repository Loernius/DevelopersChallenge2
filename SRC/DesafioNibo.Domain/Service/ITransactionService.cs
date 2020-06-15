using Desafio.Domain.Domain;
using Desafio.Domain.Seletores;
using Desafio.Domain.Service.Abstract;
using System.Collections.Generic;

namespace Desafio.Domain.Service
{
    public interface ITransactionService: IServiceBase<TransactionDomain, TransactionSeletor>
    {
        List<TransactionDomain> ParseOFXData(List<string> lines);
        List<TransactionDomain> BlendTransactions(List<List<TransactionDomain>> transactionsByFiles);
    }
}
