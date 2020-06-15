using Desafio.Service.Abstract;
using Desafio.Domain.Domain;
using Desafio.Domain.Repository;
using Desafio.Domain.Seletores;
using Desafio.Domain.Service;
using Desafio.Database.Entities;
using System.Collections.Generic;
using System.Transactions;
using System;
using System.Linq;

namespace Desafio.Service
{
    public class TransactionService : ServiceBase<TransactionDomain, TransactionEntity, TransactionSeletor, ITransactionRepository>, ITransactionService
    {
        
        public TransactionService(ITransactionRepository repository) : base(repository) { }

        public List<TransactionDomain> ParseOFXData(List<string> lines)
        {
            var takingData = false;
            var transaction = new TransactionDomain();
            var transactionList = new List<TransactionDomain>();
            foreach (var line in lines)
            {
                if (takingData == true) // if we passed the transaction block we will parse the date until we find the closing tag
                {
                    ParseLineData(ref transaction, line);
                }
                switch (line)
                {
                    case "<STMTTRN>": // here we check if its the start of the transaction block
                        takingData = true;
                        break;
                    case "</STMTTRN>":
                        // after reaching the closing tag, we will add them to the list of transactions, 
                        // and reset the lone transaction variable
                        transactionList.Add(transaction);
                        transaction = new TransactionDomain();
                        takingData = false;
                        break;
                }

            }
            return transactionList;
        }
        public List<TransactionDomain> BlendTransactions(List<List<TransactionDomain>> transactionsByFiles)
        {// here we recieve the transactions filtered by files, already parsed and ready to the database
            var blendedTransactions = this.GetList(new TransactionSeletor()).ToList(); 
            // first we get the transactions that are already on the db.
            foreach(var transactionList in transactionsByFiles)
            {
                if(blendedTransactions.Count == 0)
                {// if we dont have nothing on the database, we recieve all the list for the first time
                    blendedTransactions = transactionList;
                }else
                {
                    foreach(var notListedTransaction in transactionList)
                    {
                        var transaction = this.GetList(new TransactionSeletor() { date = notListedTransaction.date }).FirstOrDefault();
                        // if we have transactions on the db or from another iteration, we check if there is already the entries from a given date
                        
                        if(transaction == null) // and ignore it, so there is no duplicates
                        {
                            blendedTransactions.Add(notListedTransaction);
                        }
                    }
                }
            }
            blendedTransactions = blendedTransactions.OrderBy(x => x.date).ToList();
            return blendedTransactions; // so we return the blended transactions of multiples files
        }

        private static void ParseLineData(ref TransactionDomain transaction, string line)
        { // simply checking the prefix of each line, and parsing its data to the correct variable of the class
            // here we use a ref to the outer variable so we can modify its values from inside the function.
            // no need to return here
            if (line.StartsWith("<TRNTYPE>"))
            {
                transaction.type = line.Replace("<TRNTYPE>", "");
            }else if (line.StartsWith("<DTPOSTED>"))
            {
                var date_string = line.Replace("<DTPOSTED>", "").Replace("[-03:EST]", "");
                var year = date_string.Substring(0, 4);
                var month = date_string.Substring(4, 2);
                var day = date_string.Substring(6, 2);
                transaction.date = DateTime.Parse(year + '-' + month + '-' + day);
            }
            else if (line.StartsWith("<TRNAMT>"))
            {
                transaction.amount = float.Parse(line.Replace("<TRNAMT>", ""))/100;
            }
            else if (line.StartsWith("<MEMO>"))
            {
                transaction.memo = line.Replace("<MEMO>", "");
            }
        }
    }
}

