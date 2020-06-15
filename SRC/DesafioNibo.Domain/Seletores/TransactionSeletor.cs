using System;
using System.Collections.Generic;
using System.Text;
using Desafio.Domain.Seletores.Base;

namespace Desafio.Domain.Seletores
{
    public class TransactionSeletor : SeletorBase
    {
        public string type { get; set; }
        public DateTime? date { get; set; }
        public string memo { get; set; }
        public float amount { get; set; }
    }
}
