using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accountant.Web.Pages.LoanPages
{
    public class LoanPageBase : ComponentBase
    {
        [Parameter]
        public int UserID { get; set; }
        [Parameter]
        public string Username { get; set; }
        [Parameter]
        public string Password { get; set; }

        [Inject]
        public ILoanServices services { get; set; }

        [Inject]
        public IJSRuntime js { get; set; }
        [Inject]
        public IToastService Toast { get; set; }
        [Inject]
        public NavigationManager navigation { get; set; }

        public ICollection<LoanDto> Loans { get; set; }
        public string AddNewLoanURL { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Loans = await services.GetUserLoan(UserID);
                AddNewLoanURL = $"/AddNewLoan/{UserID}/{Username}/{Password}";

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async void DeleteAllLoans_Click()
        {
            try
            {

                var Pass = await js.InvokeAsync<string>("DeletePrompting", "Enter your password :");
                if (Pass == Password)
                {
                    bool Response = await services.DeleteLoans(UserID);
                    if (Response)
                    {
                        await js.InvokeVoidAsync("alert", "your Loans Deleted successfuly!");

                        StateHasChanged();
                        navigation.NavigateTo($"/UserMainPage/{Username}/{Password}");
                    }
                    else
                    {
                        await js.InvokeVoidAsync("alert", "Somthing went wrong try again");
                    }
                }
                else
                {
                    await js.InvokeVoidAsync("alert", "Your Logged password isn't match with your orginal password !");
                }
            }
            catch (Exception ex)
            {
                await js.InvokeVoidAsync("alert", ex.Message);
            }
        }
        
        public async void DeleteLoan()
        {

        }
    }
}
