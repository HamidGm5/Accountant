using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accountant.Web.Pages.LoanPages
{
    public class DeleteLoanBase : ComponentBase
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
        public IJSRuntime JS { get; set; }
        [Inject]
        public ILoanServices services { get; set; }
        [Inject]
        public IUserServices User { get; set; }
        [Inject]
        public NavigationManager navigation { get; set; }

        public LoanDto Loan { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Loan = await services.GetLoanByLoanID(UserID, LoanID);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async void DeleteLoan_Click()
        {
            var PromptinPass = await JS.InvokeAsync<string>("DeletePrompting", "Enter your passsword : ");
            if (PromptinPass != Password)
            {
                await JS.InvokeVoidAsync("alert", "your password isn't match !");

            }
            else
            {
                var Response = await services.DeleteLoan(UserID, LoanID);
                if (Response)
                {
                    await JS.InvokeVoidAsync("alert", "your Loan deleted successfully !");
                    StateHasChanged();
                    navigation.NavigateTo($"/Loans/{UserID}/{Username}/{Password}");

                }
                else
                {
                    await JS.InvokeVoidAsync("alert", "somthing went wrong so try again !");

                }
            }
        }
    }
}
