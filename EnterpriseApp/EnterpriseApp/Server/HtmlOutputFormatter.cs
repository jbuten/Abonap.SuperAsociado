namespace EnterpriseApp.Server
{
    public class HtmlOutputFormatter : Microsoft.AspNetCore.Mvc.Formatters.StringOutputFormatter
    {
        public HtmlOutputFormatter()
        {
            SupportedMediaTypes.Add("text/html");
        }
    }
}
