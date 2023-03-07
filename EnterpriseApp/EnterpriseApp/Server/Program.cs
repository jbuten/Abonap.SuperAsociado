using Abonap.Users;
using EnterpriseApp.Shared.Contexts;
using EnterpriseApp.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


//------------------------------------------------------------------------------------------

builder.Services.Configure<UsersSettings>(builder.Configuration.GetSection(nameof(UsersSettings)));
builder.Services.AddSingleton<IUsersSettings>(sp => sp.GetRequiredService<IOptions<UsersSettings>>().Value);
builder.Services.AddScoped<IUsersContext, UsersContext>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

builder.Services.AddSingleton<IAppSettings>(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);
builder.Services.AddSingleton<IMailSettings>(sp => sp.GetRequiredService<IOptions<MailSettings>>().Value);
builder.Services.AddSingleton<ISolicitudInversionSettings>(sp => sp.GetRequiredService<IOptions<SolicitudInversionSettings>>().Value);

builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IMailAdmin, MailAdmin>();

//------------------------------------------------------------------------------------------

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["UsersSettings:JwtBearer:Issuer"],
        ValidAudience = builder.Configuration["UsersSettings:JwtBearer:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["UsersSettings:JwtBearer:Key"]))
    };
});

builder.Services.AddMvc(options => options.OutputFormatters.Add(new EnterpriseApp.Server.HtmlOutputFormatter()));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddCors(option => { option.AddPolicy("allowedOrigin", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Gloabal API",
        Description = "Global API applications of the Abonap",
        TermsOfService = new Uri("https://api.abonap.com.do/terms"),
        Contact = new OpenApiContact
        {
            Name = "Jose Buten",
            Email = "jbuten@abonap.com.do",
            Url = new Uri("https://api.abonap.com.do/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Jose Buten",
            Url = new Uri("https://api.abonap.com.do/license")
        }
    });
    ///options.ResolveConflictingActions(apiDescriptions => apiDescriptions.Where(a=>a.HttpMethod == "POST").FirstOrDefault()  );
});

CultureInfo[] supportedCultures = new[] {
            new CultureInfo("es"),
            new CultureInfo("en")
        };

var dateformat = new DateTimeFormatInfo
{
    ShortDatePattern = "dd/MM/yyyy",
    LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
};

foreach (CultureInfo ci in supportedCultures)
{
    ci.NumberFormat.NumberDecimalSeparator = ".";
    ci.NumberFormat.CurrencyDecimalSeparator = ".";
    ci.DateTimeFormat = dateformat;
}

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
});

var app = builder.Build();

app.UseCors("allowedOrigin");

app.UseSwagger();
app.UseSwaggerUi(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnterpriseApp v1"));

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapGet("/api", () => $"Enterprise APP v1.0 @abonap  *  API").ExcludeFromDescription();


app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

FileExtensionContentTypeProvider contentTypes = new FileExtensionContentTypeProvider();
contentTypes.Mappings[".apk"] = "application/vnd.android.package-archive";
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = contentTypes
});

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");



app.Run();

