using Accountant.Model.Dto;
using Accountant.Web.Services;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Accountant.Web.Pages
{
    public class UserMainPageBase : ComponentBase
    {
        [Parameter]
        public string username { get; set; }

        [Parameter]
        public string password { get; set; }

        [Inject]
        public IUserServices UserServices { get; set; }

        [Inject]
        public IIncomeServices IncomeServices { get; set; }
        [Inject]
        public IPaymentServices PaymentServices { get; set; }

        public UserDto user { get; set; }
        public ICollection<PaymentTransactionDto> PaymentTransactions { get; set; }
        public ICollection<IncomeTransactionDto> IncomeTransactions { get; set; }

        public ICollection<PaymentTransactionDto> MonthPayment { get; set; }
        public ICollection<IncomeTransactionDto> MonthIncome { get; set; }

        public NavigationManager navigationManager { get; set; }

        public double Remaining { get; set; }
        public int userid { get; set; }
        public string AddPaymentURL { get; set; }
        public string AddIncomeURL { get; set; }
        public string DeleteUserURL { get; set; }
        public string UpdateUserURL { get; set; }
        public string LoanURL { get; set; }
        public string ErrorMessage { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            try
            {
                user = await UserServices.Login(username, password);
                userid = (int)user.Id;

                PaymentTransactions = await PaymentServices.GetTransactions(userid);
                IncomeTransactions = await IncomeServices.GetTransactions(userid);

                MonthIncome = await IncomeInMonth(DateTime.Now);
                MonthPayment = await PaymentInMonth(DateTime.Now);

                Remaining = MonthIncome.Sum(mi => mi.Amount) - MonthPayment.Sum(mp => mp.Amount);

                // For Navigate To Pages
                AddPaymentURL = $"AddPayment/{userid}/{username}/{password}";
                AddIncomeURL = $"AddIncome/{userid}/{username}/{password}";
                DeleteUserURL = $"DeleteUser/{username}/{password}";
                UpdateUserURL = $"UpdateUser/{username}/{password}";
                LoanURL = $"Loans/{userid}/{username}/{password}";
            }

            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task<ICollection<IncomeTransactionDto>> IncomeInMonth(DateTime date)
        {
            var incomeInMonth = IncomeTransactions.Where(ic => ic.TransactionTime.Month == date.Month)
                .OrderBy(ic => ic.TransactionTime).ToList();
            return incomeInMonth;
        }

        protected async Task<ICollection<PaymentTransactionDto>> PaymentInMonth(DateTime date)
        {
            var incomeInMonth = PaymentTransactions.Where(ic => ic.TransactionTime.Month == date.Month).OrderByDescending(ic => ic.TransactionTime).ToList();
            return incomeInMonth;
        }

    }  //Class
}
