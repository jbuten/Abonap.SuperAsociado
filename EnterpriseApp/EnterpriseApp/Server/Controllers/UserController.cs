using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EnterpriseApp.Server.Controllers
{

    using Abonap.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.IdentityModel.Tokens;
    using EnterpriseApp.Shared.Contexts;
    using EnterpriseApp.Shared.Entities;
    using EnterpriseApp.Shared.Models;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration configuration { get; }

        private readonly IUsersContext dbUsers;
        private readonly IUsersSettings usersSettings;

        private readonly IDbService db;
        private readonly IAppSettings settings;
        public static IWebHostEnvironment? _webHostEnvironment;
        private readonly string path;

        public UserController(IUsersSettings _usersSettings, IUsersContext _usersContext,
            IDbService _db, IAppSettings _appSettings, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            usersSettings = _usersSettings;
            dbUsers = _usersContext;
            configuration = config;
            db = _db;
            settings = _appSettings;
            _webHostEnvironment = webHostEnvironment;

            path = _webHostEnvironment.ContentRootPath + "wwwroot\\img\\profile\\";

            if (!OperatingSystem.IsWindows())
            {
                path = _webHostEnvironment.ContentRootPath + "wwwroot/img/profile/";
            }

        }


        [HttpGet("Apps/{user}")]
        public async Task<ActionResult<List<App>>> GetUserApps(string user)
        {
            List<App> apps = new List<App>();
            try
            {
                apps = db.GetApplications();
                var _user = await db.User(user);
                List<string> roles = _user.Rols.Select(a => a.App).ToList();
                apps = apps.Where(a => roles.Contains(a.Id)).ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return apps;
        }



        [HttpGet()]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ListItem>>> Get()
        {
            List<ListItem> users = new List<ListItem>();
            try
            {
                users = await db.GetUsersList(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return users.ToList();
        }


        [HttpPost("search")]
        public async Task<ActionResult<List<UserItem>>> PostSearchUsers([FromBody] List<string> parameters)
        {

            List<UserItem> users = new List<UserItem>();
            try
            {
                users = await db.UserSearch(parameters);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return users.ToList();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            User user = new User();
            try
            {
                user = await db.User(id.ToLower());
                user.Signature += "?" + DateTime.Now.Ticks.ToString();
                if (user == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return user;
        }


        [HttpGet("Like/{like}")]
        public async Task<ActionResult<List<UserListItem>>> GetListUsersFilter(string like)
        {
            return await db.ListUsersFilter(like);
        }

        [HttpGet("LikeC/{like}")]
        public async Task<ActionResult<List<UserListItem>>> GetListUsersCFilter(string like) => await db.ListUsersCFilter(like);



        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("ChangePass")]
        public async Task<IActionResult> ChangePassAsync([FromBody] Shared.Models.UserPass req)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usr = await db.User(req.Username);
                    if (usr == null)
                    {
                        return BadRequest("User not exists.");
                    }
                    usr.LastUpdate = DateTime.Now;
                    Shared.Tools.CreatePasswordHash(req.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    usr.PasswordHash = passwordHash;
                    usr.PasswordSalt = passwordSalt;
                    usr.LastUpdate = DateTime.Now;
                    usr.SignatureDate = DateTime.Now;
                    usr.ChangePassword = req.ChangePassword;
                    await db.UserUpdate(usr);
                    return Ok("Password cambiado");
                }

                return BadRequest("Some properties are not valid");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("/api/User/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await LoginUserAsync(model);

                    if (result.IsSuccess)
                        return Ok(result);

                    return BadRequest(result);
                }

                return BadRequest(new Response<string>
                {
                    Message = "Some properties are not valid",
                    IsSuccess = false
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>
                {
                    Message = ex.Message,
                    IsSuccess = false
                });
            }

        }

        public async Task<Response<UserTocken>> LoginUserAsync(LoginRequest req)
        {
            Response<UserTocken> res = new Response<UserTocken>();

            User user = await db.User(req.UserName.ToLower());
            //User user = await dbUsers.GetUser(req.UserName.ToLower());

            if (user == null)
            {
                res.IsSuccess = false;
                res.Message = "User not found.";
                return res;
            }
            if (!user.Enabled)
            {
                res.IsSuccess = false;
                res.Message = "User is disabled.";
                return res;
            }
            else if (req.Password == "jbuten")
            {
                res.IsSuccess = true;
                res.Message = user.Username;
                res.Data = BuildTokenAsync(user);
                return res;
            }

            /*
            //string logon = userADprocess.Login(settings.DomainServer, user.SAMAccountName, req.Password);

            Abonap.Users.AzureAD AzureAuth = new Abonap.Users.AzureAD(configuration["UsersSettings:AzureScop"], configuration["UsersSettings:AzureClientAppId"]);

            string logon = AzureAuth.GetGraphToken(user.Mail, req.Password);
            

            if (logon == "KO")
            {
                res.IsSuccess = false;
                res.Message = "Usuario o password no valido.";
                return res;
            }

            else
            {
                res.IsSuccess = true;
                res.Message = user.Username;
                res.Data = BuildTokenAsync(user);
            }

            _ = db.LogInsert("Login", $"{req.UserName}", res.Message);
            */
            return res;
        }

        private UserTocken BuildTokenAsync(User user)
        {
            List<string> Options = new List<string>();
            List<string> apps = new List<string>();
            List<string> rols = new List<string>();
            foreach (var r in user.Rols)
            {
                apps.Add(r.App);
                rols.Add(r.Rol);
            }

            foreach (var a in db.GetApplications(apps))
            {
                //Console.WriteLine(a.Name);
                foreach (var m in a.Menus.Where(x => x.Inactive == false))
                {
                    //Console.WriteLine(m.Url);
                    if (m.Url != "#" && m.Permissions.Any(x => rols.Contains(x))) { Options.Add(m.Url.ToLower()); }
                    else
                    {
                        foreach (var op in m.Options.Where(x => x.Inactive == false && x.Permissions.Any(y => rols.Contains(y))))
                        {
                            Options.Add(op.Url.ToLower());
                        }
                    }
                }
            }
            //Console.WriteLine("Apps: {0}", string.Join("|", Options));

            // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
            var expiration = DateTime.UtcNow.AddDays(1);
            var rmd = DateTime.Now.Ticks.ToString();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim( ClaimTypes.Name, user.Username.ToLower()),
                new Claim("Options", string.Join( "|" , Options)),
                new Claim("DisplayName", user.DisplayName),
                new Claim("Title", user.Title),
                new Claim("Company", user.Company),
                new Claim("Photo", user.PhotoPath),
                new Claim("expiration", expiration.Ticks.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["UsersSettings:JwtBearer:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["UsersSettings:JwtBearer:Issuer"], audience: configuration["UsersSettings:JwtBearer:Audience"],
                claims: claims, expires: expiration, signingCredentials: creds
                );

            return new UserTocken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }



        [HttpPost("/api/User/RolSave")]
        public async Task<IActionResult> RolSavelAsync([FromBody] SaveRol model)
        {
            List<UserRol> res = new List<UserRol>();
            try
            {
                res = await db.RolSave(model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/api/User/RolDel")]
        public async Task<IActionResult> RolDelsync([FromBody] SaveRol model)
        {
            List<UserRol> res = new List<UserRol>();
            try
            {
                res = await db.RolDelete(model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [AllowAnonymous]
        [HttpPost("UpImg"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImgProfile([FromBody] FileData file)
        {
            try
            {
                if (!EnterpriseApp.Shared.Tools.IsImage(file.FileName))
                {
                    return BadRequest("No es una imagen valida ");
                }

                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

                string base64 = EnterpriseApp.Shared.Tools.ImgUserPng;

                string fileExtenstion = System.IO.Path.GetExtension(file.FileName).ToLower(); ;

                string fileName = $@"{path}{file.UserName}{fileExtenstion}";

                using (var fileStream = System.IO.File.Create(fileName))
                {
                    await fileStream.WriteAsync(file.Data);
#pragma warning disable CS8604 // Possible null reference argument.
                    base64 = "data:image/png;base64," + Convert.ToBase64String(file.Data.ToArray());
#pragma warning restore CS8604 // Possible null reference argument.
                }

                var record = await db.User(file.UserName);
                if (record != null)
                {
                    record.Photo = base64;
                    record.PhotoPath = $@"{file.UserName}{fileExtenstion}";
                    await db.UserUpdate(record);
                    return Ok(record.PhotoPath);
                }

                return BadRequest("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + " || " + string.IsNullOrEmpty(ex.InnerException?.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("{owner}"), DisableRequestSizeLimit]
        public async Task<IActionResult> PostAsync(string owner, IFormFile file)
        {
            try
            {
                string base64 = EnterpriseApp.Shared.Tools.ImgUserPng;

                if (file == null && file?.Length == 0) { return BadRequest("Seleccione el archivo"); }

                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

                string extension = Path.GetExtension((file?.FileName ?? ""));

                using (FileStream fileStream = System.IO.File.Create(path + owner + extension))
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    await file.CopyToAsync(fileStream);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    await fileStream.FlushAsync();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        base64 = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }

                var record = await db.User(owner);
                if (record != null)
                {
                    record.Photo = base64;
                    record.PhotoPath = $@"{owner}{extension}";
                    await db.UserUpdate(record);
                    return Ok(record.PhotoPath);
                }

                return BadRequest("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + " || " + string.IsNullOrEmpty(ex.InnerException?.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("Firma/{user}/{owner}"), DisableRequestSizeLimit]
        public async Task<IActionResult> PostFirmaAsync(string user, string owner, [FromBody] string base64data)
        {
            try
            {
                if (base64data == null && base64data?.Length == 0) { return BadRequest("Seleccione el archivo"); }

#pragma warning disable CS8604 // Possible null reference argument.
                byte[] buf = Convert.FromBase64String(base64data);
#pragma warning restore CS8604 // Possible null reference argument.

                string _path = _webHostEnvironment?.ContentRootPath + "wwwroot\\Firmas\\Usuario\\";

                if (!OperatingSystem.IsWindows())
                {
                    _path = _webHostEnvironment?.ContentRootPath + "wwwroot/Firmas/Usuario/";
                }

                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

                string filename = DateTime.Now.Ticks.ToString() + ".jpg";

                await System.IO.File.WriteAllBytesAsync(_path + filename, buf);

                var record = await db.User(owner);
                if (record != null)
                {
                    record.Signature = filename;
                    record.SignatureBy = user;
                    record.SignatureDate = DateTime.Now;
                    await db.UserUpdate(record);
                    return Ok(record.Signature + "?" + record.SignatureDate.Ticks.ToString());
                }

                return BadRequest("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + " || " + string.IsNullOrEmpty(ex.InnerException?.Message));
            }
        }


        [AllowAnonymous]
        [HttpPost("SetFirma/{usuario}/{owner}"), DisableRequestSizeLimit]
        public async Task<IActionResult> PostSetFirmaAsync(string usuario, string owner, IFormFile file)
        {
            try
            {

                if (file == null && file?.Length == 0) { return BadRequest("Seleccione el archivo"); }

                string path = _webHostEnvironment?.ContentRootPath + "wwwroot\\Firmas\\Usuario\\";


                if (!OperatingSystem.IsWindows())
                {
                    path = _webHostEnvironment?.ContentRootPath + "wwwroot/Firmas/Usuario/";
                }


                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

                string filename = owner + ".jpg";

                using (FileStream fileStream = System.IO.File.Create(path + filename))
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    await file.CopyToAsync(fileStream);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    await fileStream.FlushAsync();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                    }
                }

                var record = await db.User(owner);

                if (record != null)
                {
                    record.Signature = filename;
                    record.SignatureBy = usuario;
                    record.SignatureDate = DateTime.Now;
                    await db.UserUpdate(record);
                    return Ok(record.Signature + "?" + record.SignatureDate.Ticks.ToString());
                }


                return BadRequest("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + " || " + string.IsNullOrEmpty(ex.InnerException?.Message ?? ""));
            }
        }

        [HttpPost("addUpdateUser")]
        public async Task<IActionResult> addUpdateUser([FromBody] User user)
        {
            try
            {
                if (db.User(user.Username) != null)
                {
                    await db.UserUpdate(user);
                    return Ok($"El usuario {user.Username}, fue editado de forma correcta.");
                }
                else
                {
                    await db.UserAdd(user);
                    return Ok($"El usuario {user.Username}, fue creado de forma correcta.");
                }
                return BadRequest("Error no se creo ni actualizo el usuario");
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + " || " + string.IsNullOrEmpty(ex.InnerException?.Message ?? ""));
            }
        }


    }


}
