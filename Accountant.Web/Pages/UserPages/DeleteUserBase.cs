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

        public string Email { get; set; }
        public int userid { get; set; }

        public UserDto Finduser { get; set; }

        public string ErorMessage { get; set; }

        [Inject]
        public IUserServices UserServices { get; set; }

        [Inject]
        public IPaymentServices PaymentServices { get; set; }

        [Inject]
        public IIncomeServices IncomeServices { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Finduser = await UserServices.Login(Username, Password);

                if (Finduser != null)
                {
                    Email = Finduser.Email;
                    userid = (int)Finduser.Id;
                }
            }

            catch (Exception ex)
            {
                ErorMessage = ex.Message;
            }
        }

        protected void DeleteUser_Click()
        {
            if (userid != null)
            {
                PaymentServices.DeleteTransactions(userid);
                IncomeServices.DeleteTransactions(userid);
                UserServices.DeleteUser(userid);
            }

            else
            {
                ErorMessage = "Your Specs Not Found ! ";
            }
        }

    } // Class
}
