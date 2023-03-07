namespace EnterpriseApp.Client.Auth
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text.Json;

    public interface ILoginService
    {
        Task Login(string token);
        Task Logout();
    }

    public class JWTAuthenticationProvider : AuthenticationStateProvider, ILoginService
    {
        #region Local

        private readonly HttpClient httpClient;
        private readonly ILocalStorageService LocalStorage;
        public static readonly string TOKENKEY = "TOKENKEY";
        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        public JWTAuthenticationProvider(HttpClient httpClient, ILocalStorageService _LocalStorage)
        {
            this.httpClient = httpClient;
            LocalStorage = _LocalStorage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await LocalStorage.GetItemAsStringAsync(TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return Anonimo;
            }

            return ConstruirAuthenticationState(token);
        }

        private AuthenticationState ConstruirAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
            var token = await LocalStorage.GetItemAsStringAsync(TOKENKEY);
            return ParseClaimsFromJwt(token);
        }

        public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();

            try
            {
                var payload = jwt.Split('.')[1];
                //var jsonBytes = ParseBase64WithoutPadding(payload);
                var jsonBytes = DecodeUrlBase64(payload);
                var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

                keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

                if (roles != null)
                {
                    if (roles.ToString().Trim().StartsWith("["))
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                        foreach (var parsedRole in parsedRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                    }

                    keyValuePairs.Remove(ClaimTypes.Role);
                }

                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                //claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, "none"));
                claims.Add(new Claim(ClaimTypes.Name, "none"));
                claims.Add(new Claim("user", "none"));
                claims.Add(new Claim("username", ex.Message));
                claims.Add(new Claim("rol", "rol"));
                claims.Add(new Claim("rolName", "perfil"));
                claims.Add(new Claim("email", "info@butensoft.com"));
                claims.Add(new Claim("mobilephone", "809-555-5555"));
                claims.Add(new Claim("photo", "photo.png"));
                claims.Add(new Claim("menu", ""));

            }

            return claims;
        }

        public byte[] DecodeUrlBase64(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/').PadRight(4 * ((s.Length + 3) / 4), '=');
            return Convert.FromBase64String(s);
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        #endregion

        #region Method

        public async Task Login(string token)
        {
            await LocalStorage.SetItemAsync<string>(TOKENKEY, token);
            var authState = ConstruirAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            await GetAuthenticationStateAsync();
        }

        public async Task Logout()
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
            await LocalStorage.RemoveItemAsync(TOKENKEY);
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
            await GetAuthenticationStateAsync();
        }

        #endregion
    }
}
