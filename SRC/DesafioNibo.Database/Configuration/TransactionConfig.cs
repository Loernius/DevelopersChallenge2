using System;
using Desafio.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Desafio.Database.Configuration.Abstract;

namespace Desafio.Database.Configuration
{
    public class TransactionConfig : BaseConfig<TransactionEntity>
    {
        public TransactionConfig() : base("Extrato") { }

        public override void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            base.Configure(builder);

        }
    }
}
