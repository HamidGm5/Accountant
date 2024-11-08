using Accountant.API.Entities;
using Accountant.API.Repository;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using AutoMapper;

namespace Accountant.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Payment Transactions
            CreateMap<PaymentTransaction, PaymentTransactionDto>();
            CreateMap<PaymentTransaction, AddTransactionsStandardDto>();
            CreateMap<PaymentTransactionDto, AddTransactionsStandardDto>();
            CreateMap<PaymentTransactionDto, PaymentTransaction>();
            CreateMap<AddTransactionsStandardDto, PaymentTransaction>();
            CreateMap<AddTransactionsStandardDto, PaymentTransactionDto>();

            //User
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            //Admin
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminDto, Admin>();


            //Income Transaction
            CreateMap<IncomeTransaction, IncomeTransactionDto>();
            CreateMap<IncomeTransaction, AddTransactionsStandardDto>();
            CreateMap<IncomeTransactionDto, AddTransactionsStandardDto>();
            CreateMap<IncomeTransactionDto, IncomeTransaction>();
            CreateMap<AddTransactionsStandardDto, IncomeTransaction>();
            CreateMap<AddTransactionsStandardDto, IncomeTransactionDto>();

            // Loan
            CreateMap<Loan, LoanDto>();
            CreateMap<LoanDto, Loan>();
        }
    }
}
