using System;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace paynamart.com.Client.Helper
{
    public class SessionState
    {
        private readonly ILocalStorageService _local;
        private readonly IJSRuntime _jsRuntime;
        private static string SystemLang { get; set; } = "tr";
        public SessionState(ILocalStorageService local, IJSRuntime jsRuntime)
        {
            _local = local;

            _jsRuntime = jsRuntime;


        }
        public async Task SetLanguageLocal(string lang)
        {
            await _local.SetItemAsync<string>("lang", lang);

        }
        public async Task<string> GetLanguageLocal()
        {
            SystemLang = await _local.GetItemAsync<string>("lang");

            return SystemLang;
        }
        public bool EmailIsValid(string email)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }

     

    }
}

