using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accountant.Web.Pages.LoanPages
{
    public class UpdateLoanBase : ComponentBase
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
        public IJSRuntime js { get; set; }
        [Inject]
        public ILoanServices services { get; set; }
        [Inject]
        public NavigationManager navigation { get; set; }

        public LoanDto Loan { get; set; }
        public double LoanAmount { get; set; }
        public int Period { get; set; }
        public float Percentage { get; set; }
        public DateTime StartTime { get; set; }
        public string? Description { get; set; }

        public string? ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Loan = await services.GetLoanByLoanID(UserID, LoanID);
                LoanAmount = Loan.LoanAmount;
                Period = Loan.PeriodPerMonth;
                Percentage = Loan.Percentage;
                StartTime = Loan.StartTime;
                Description = Loan.Description;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async void UpdateLoan_Click()
        {
            var PromptPass = await js.InvokeAsync<string>("window.prompt", "Enter your password :");

            if (PromptPass != Password)
            {
                await js.InvokeVoidAsync("alert", "your password is wrong try again");
            }
            else
            {
                Loan = new LoanDto()
                {
                    LoanAmount = LoanAmount,
                    ID = Loan.ID,
                    Userid = UserID,
                    Percentage = Percentage,
                    StartTime = StartTime,
                    Description = Description,
                    PeriodPerMonth = Period

                };


                var Response = await services.UpdateLoan(LoanID, Loan);
                if (Response)
                {
                    await js.InvokeVoidAsync("alert", "your Loan is updated !");
                    navigation.NavigateTo($"/Loans/{UserID}/{Username}/{Password}");
                    StateHasChanged();
                }
                else
                {
                    await js.InvokeVoidAsync("alert", "somthing went wrong !");
                }
            }

        }
    }
}
