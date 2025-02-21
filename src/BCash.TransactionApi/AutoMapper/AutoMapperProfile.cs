using AutoMapper;
using BCash.Domain.Entities;
using BCash.TransactionApi.DTOs;

namespace BCash.TransactionApi.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Transaction, TransactionDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Balance, BalanceDTO>()
                .ForMember(dest => dest.TotalCredit, opt => opt.MapFrom(src => src.TotalCredit))
                .ForMember(dest => dest.TotalDebit, opt => opt.MapFrom(src => src.TotalDebit))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));
        }
    }
}
