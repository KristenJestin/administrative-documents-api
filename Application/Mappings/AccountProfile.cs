using Application.DTOs.Account;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AuthenticateResponse>();

            CreateMap<RegisterRequest, Account>();
        }
    }
}
