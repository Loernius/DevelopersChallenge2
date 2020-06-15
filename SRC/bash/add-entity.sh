#!/bin/bash

path=".."
entity=$1

## Desafio.Infra

# Desafio.Infra => Entities

# ${entity}

file=$path"/DesafioNibo.Database/Entities/"$entity"Entity.cs"

echo $file

tee $file > /dev/null << EOF
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Desafio.Database.Entities.Base;

namespace Desafio.Database.Entities
{
    [Table("${entity}")]
    public class ${entity}Entity : EntityBase
    {
        
    }
}
EOF

# Desafio.Infra => Mapping

file=$path"/DesafioNibo.CrossCutting/Mappings/"$entity"Profile.cs"

echo $file

tee $file > /dev/null << EOF
using AutoMapper;
using Desafio.Domain.Domain;
using Desafio.Database.Entities;


namespace Desafio.CrossCutting.Mappings
{
    public class ${entity}Profile : Profile
    {
        public ${entity}Profile()
        {
            CreateMap<${entity}Entity, ${entity}Domain>().ReverseMap();
        }
    }
}

EOF

# Desafio.Database => Configuration

file=$path"/DesafioNibo.Database/Configuration/"$entity"Config.cs"

echo $file

tee $file > /dev/null << EOF
using System;
using Desafio.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Desafio.Database.Configuration.Abstract;

namespace Desafio.Database.Configuration
{
    public class ${entity}Config : BaseConfig<${entity}Entity>
    {
        public ${entity}Config() : base("${entity}") { }

        public override void Configure(EntityTypeBuilder<${entity}Entity> builder)
        {
            base.Configure(builder);

        }
    }
}
EOF

# Desafio.Database => Repositories

file=$path"/DesafioNibo.Database/Repository/"$entity"Repository.cs"

echo $file

tee $file > /dev/null << EOF
using Desafio.Domain.Domain;
using Desafio.Domain.Repository;
using Desafio.Domain.Seletores;
using Desafio.Database.Entities;
using Desafio.Database.Interface;
using Desafio.Database.Repository.Abstract;
using System.Linq;

namespace Desafio.Database.Repository
{
    public class ${entity}Repository : RepositoryBase<${entity}Entity, ${entity}Domain, ${entity}Seletor>, I${entity}Repository
    {
        public ${entity}Repository(IDesafioContext context) : base(context) { }
        
        public override IQueryable<${entity}Entity> CreateParameters(${entity}Seletor seletor, IQueryable<${entity}Entity> query)
        {
            return query;
        }

    }
}
EOF

## Domain

# Desafio.Domain

file=$path"/DesafioNibo.Domain/Domain/"$entity"Domain.cs"

echo $file

tee $file > /dev/null << EOF
using Desafio.Domain.Domain.Base;
namespace Desafio.Domain.Domain
{
    public class ${entity}Domain : DomainBase
    {
        
    }
}

EOF


# Desafio.Domain => Seletores

file=$path"/DesafioNibo.Domain/Seletores/"$entity"Seletor.cs"

echo $file

tee $file > /dev/null << EOF
using System;
using System.Collections.Generic;
using System.Text;
using Desafio.Domain.Seletores.Base;

namespace Desafio.Domain.Seletores
{
    public class ${entity}Seletor : SeletorBase
    {
    }
}
EOF


# Desafio.Domain => Repository

file=$path"/DesafioNibo.Domain/Repository/I"$entity"Repository.cs"

echo $file

tee $file > /dev/null << EOF
using Desafio.Domain.Domain;
using Desafio.Domain.Repository.Abstract;
using Desafio.Domain.Seletores;

namespace Desafio.Domain.Repository
{
    public interface I${entity}Repository: IRepositoryBase<${entity}Domain, ${entity}Seletor>
    {
    }
}
EOF


# Desafio.Domain => Service

file=$path"/DesafioNibo.Domain/Service/I"$entity"Service.cs"

echo $file

tee $file > /dev/null << EOF
using Desafio.Domain.Domain;
using Desafio.Domain.Seletores;
using Desafio.Domain.Service.Abstract;

namespace Desafio.Domain.Service
{
    public interface I${entity}Service: IServiceBase<${entity}Domain, ${entity}Seletor>
    {

    }
}
EOF

## Application

# Desafio.Service => Services

file=$path"/DesafioNibo.Service/"$entity"Service.cs"

echo $file

tee $file > /dev/null << EOF
using Desafio.Service.Abstract;
using Desafio.Domain.Domain;
using Desafio.Domain.Repository;
using Desafio.Domain.Seletores;
using Desafio.Domain.Service;
using Desafio.Database.Entities;

namespace Desafio.Service
{
    public class ${entity}Service : ServiceBase<${entity}Domain, ${entity}Entity, ${entity}Seletor, I${entity}Repository>, I${entity}Service
    {
        
        public ${entity}Service(I${entity}Repository repository) : base(repository) { }
    }
}

EOF