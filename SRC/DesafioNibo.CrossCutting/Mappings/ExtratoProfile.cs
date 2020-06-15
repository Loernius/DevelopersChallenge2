using AutoMapper;
using Desafio.Domain.Domain;
using Desafio.Database.Entities;


namespace Desafio.CrossCutting.Mappings
{
    public class ExtratoProfile : Profile
    {
        public ExtratoProfile()
        {
            CreateMap<TransactionEntity, TransactionDomain>().ReverseMap();
        }
    }
}

