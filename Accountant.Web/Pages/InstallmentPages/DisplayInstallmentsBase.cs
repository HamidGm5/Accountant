﻿using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;

namespace Accountant.Web.Pages.InstallmentPages
{
    public class DisplayInstallmentsBase : ComponentBase
    {
        [Parameter]
        public ICollection<InstallmentDto> Installments { get; set; }
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
        public NavigationManager Navigation { get; set; }

        public async void PayInstallment(int ID)
        {
            var Response = await services.UpdatePay(ID);
            if (Response)
            {
                StateHasChanged();
                Navigation.NavigateTo(Navigation.Uri, true);
            }
        }
    }


}