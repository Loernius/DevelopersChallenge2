using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Desafio.Database.Entities.Base;

namespace Desafio.Database.Entities
{
    [Table("Extrato")]
    public class TransactionEntity : EntityBase
    {
        public string type { get; set; }
        public DateTime date { get; set; }
        public string memo { get; set; }
        public float amount { get; set; }
    }
}
