using Desafio.Database.Entities.Base;
using Desafio.Database.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Database.Configuration.Abstract
{
    public abstract class BaseConfig<TType> : IDesafioContextConfiguration, IEntityTypeConfiguration<TType> where TType : EntityBase
    {
        public BaseConfig(string tableName)
            => TableName = tableName;

        public string TableName { get; }

        public virtual void Configure(EntityTypeBuilder<TType> builder)
        {
            builder.ToTable(TableName);

            // builder.HasKey(obj => obj.Id);
            // builder.Property(obj => obj.Ativo).HasDefaultValue(true);
            // builder.Property(x => x.DataCriacao).IsRequired();
        }
    }
}
