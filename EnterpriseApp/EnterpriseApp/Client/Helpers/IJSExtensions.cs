namespace EnterpriseApp.Client.Helpers
{
    using Microsoft.JSInterop;
    public class IJSExtensions
    {

        private readonly IJSRuntime Js;

        public IJSExtensions(IJSRuntime js)
        {
            this.Js = js;
        }

        public ValueTask<object> SetInLocalStorage(string key, string content)
        {
            return Js.InvokeAsync<object>("localStorage.setItem", key, content);
        }

        public ValueTask<string> GetFromLocalStorage(string key)
        {
            return Js.InvokeAsync<string>("localStorage.getItem", key);
        }

        public ValueTask<object> RemoveItem(string key)
        {
            return Js.InvokeAsync<object>("localStorage.removeItem", key);
        }


    }
}
