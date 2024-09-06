using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;

namespace Accountant.Web.Pages.InstallmentPages
{
    public class InstallmentsBase : ComponentBase
    {
        [Parameter]
        public int UserID { get; set; }
        [Parameter]
        public string Username { get; set; }
        [Parameter]
        public string Password { get; set; }
        [Parameter]
        public int LoanID { get; set; }

        [Inject]
        public IinstallmentServices services { get; set; }
        [Inject]
        public ILoanServices LoanServices { get; set; }
        [Inject]
        public IUserServices UserServices { get; set; }
        [Inject]
        public NavigationManager navigate { get; set; }

        public ICollection<InstallmentDto>? Installments { get; set; }
        public UserDto? UserResponse { get; set; }
        public LoanDto? LoanResponse { get; set; }
        public bool URLOK { get; set; }

        public string? ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                UserResponse = await UserServices.Login(Username, Password);
                LoanResponse = await LoanServices.GetLoanByLoanID(UserID, LoanID);
                if (UserResponse != null && LoanResponse != null)
                {
                    if (UserResponse.Id == UserID)
                    {
                        URLOK = true;
                        Installments = await services.GetInstallments(LoanID);
                    }
                    else
                    {
                        URLOK = false;
                        ErrorMessage = "URL Is manipulated !";
                    }
                }
                else
                {
                    URLOK = false;
                    ErrorMessage = "URL Is manipulated !";

                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
