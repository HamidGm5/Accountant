using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;

namespace Accountant.Web.Pages.UserPages
{
    public class DeleteUserBase : ComponentBase
    {
        [Parameter]
        public string Username { get; set; }

        [Parameter]
        public string Password { get; set; }

        [Inject]
        public IUserServices UserServices { get; set; }

        [Inject]
        public IPaymentServices PaymentServices { get; set; }

        [Inject]
        public IIncomeServices IncomeServices { get; set; }
        [Inject]
        public ILoanServices LoanServices { get; set; }

        public string? Email { get; set; }
        public int userid { get; set; } = 0;

        public UserDto? Finduser { get; set; }

        public string? ErorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Finduser = await UserServices.Login(Username, Password);

                if (Finduser != null)
                {
                    Email = Finduser.Email;
                    userid = Convert.ToInt32(Finduser.Id);
                }
            }

            catch (Exception ex)
            {
                ErorMessage = ex.Message;
            }
        }

        protected async void DeleteUser_Click()
        {
            if (userid != 0)
            {
                await PaymentServices.DeleteTransactions(userid);
                await IncomeServices.DeleteTransactions(userid);
                await LoanServices.DeleteLoans(userid);
                await UserServices.DeleteUser(userid);
            }

            else
            {
                ErorMessage = "Somthing went wrong ! ";
            }
        }

    } // Class
}
