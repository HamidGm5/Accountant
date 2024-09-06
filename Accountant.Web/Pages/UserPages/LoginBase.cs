using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection.Metadata.Ecma335;

namespace Accountant.Web.Pages.UserPages
{
    public class LoginBase : ComponentBase
    {
        [Parameter]
        public string Username { get; set; }
        [Parameter]
        public string Password { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }
        [Inject]
        public NavigationManager Navigate { get; set; }

        public int userid { get; set; }

        public string? ErorMessage { get; set; }


        protected async void Login_Click()
        {
            if(await CheckValue(Username, Password))
            {
                Navigate.NavigateTo($"/UserMainPage/{Username}/{Password}");
            }
        }

        protected async Task<bool> CheckValue(string username, string password)
        {
            try
            {
                if (username == null)
                {
                    await JS.InvokeAsync<UserDto>("alert", "Your username it's null !");
                    return false;
                }
                if (password == null)
                {
                    await JS.InvokeAsync<UserDto>("alert", "Your password it's null !");
                    return false;
                }

                else
                {
                    return true;
                }
            }

            catch (Exception ex)
            {
                ErorMessage = ex.Message;
                return false;
            }
        }
    }//Class
}


