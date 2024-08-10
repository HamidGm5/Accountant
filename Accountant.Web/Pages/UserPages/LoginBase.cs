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

        public int userid { get; set; }

        public string ErorMessage { get; set; }


        protected void Login_Click()
        {
            CheckValue(Username, Password);
        }

        protected bool CheckValue(string username, string password)
        {
            try
            {
                if (username == null)
                {
                    JS.InvokeAsync<UserDto>("alert", "Your username it's null !");
                    return false;
                }
                else if (password == null)
                {
                    JS.InvokeAsync<UserDto>("alert", "Your password it's null !");
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


