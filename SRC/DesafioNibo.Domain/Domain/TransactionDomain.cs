using Desafio.Domain.Domain.Base;
using System;

namespace Desafio.Domain.Domain
{
    public class TransactionDomain : DomainBase
    {
        public string type { get; set; }
        public DateTime date { get; set; }
        public string memo { get; set; }
        public float amount { get; set; }
    }
}

