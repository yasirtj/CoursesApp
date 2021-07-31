using CoursesApp.Data;
using CoursesApp.Models;
using CoursesApp.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoursesApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly TraineeService traineeService;
        public AccountController()
        {
            CoursesIdentityContext db = new CoursesIdentityContext();
            var userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);
            traineeService = new TraineeService();
        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "")
        {
            return View(new LoginViewModel { 
                returnUrl = returnUrl
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginData)
        {
            if (ModelState.IsValid)
            {


                var userExsits = await userManager.FindAsync(loginData.Email, loginData.Password);
                if (userExsits != null)
                {
                    await SignIn(userExsits);
                    // Business
                    if (!string.IsNullOrEmpty(loginData.returnUrl))
                    {
                        return Redirect(loginData.returnUrl);
                    }

                    var userRoles = userManager.GetRoles(userExsits.Id);
                    var role = userRoles.FirstOrDefault();
                    if (role == "Admin")
                    {
                        return RedirectToAction("Index", "Default", new { area = "Admin" });
                    }

                    return RedirectToAction("Index", "Default");
                }
                loginData.Message = "Email or password is incorrect !";
            }
            return View(loginData);
        }
        private async Task SignIn(MyIdentityUser myIdentityUser)
        {
            var identity = await userManager.CreateIdentityAsync(myIdentityUser, DefaultAuthenticationTypes.ApplicationCookie);
            var owinContext = Request.GetOwinContext();
            var authManager = owinContext.Authentication;
            authManager.SignIn(identity);
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel userInfo)
        {
            if (ModelState.IsValid)
            {
                var identityUsuer = new MyIdentityUser
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Name
                };
                var creationResult = await userManager.CreateAsync(identityUsuer, userInfo.Password);

                // User Created 
                if (creationResult.Succeeded)
                {
                    var userId = identityUsuer.Id;
                   creationResult = userManager.AddToRole(userId, "Trainee");
                    
                    // Role Assigned
                    if (creationResult.Succeeded)
                    {
                        // Save to trainee table
                       var savedTrainee = traineeService.Create(new Trainee
                        {
                            Email = userInfo.Email,
                            Name = userInfo.Name,
                            Is_Active = true,
                            Creation_Date = System.DateTime.Now
                        });
                        if (savedTrainee == null)
                        {
                            userInfo.Message = "An Error Occured!";
                            return View(userInfo);
                        }
                        return RedirectToAction("Index", "Default");
                    }
                }
                var message = creationResult.Errors.FirstOrDefault();
                userInfo.Message = message;
                return View(userInfo);
            }
            return View(userInfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var owinContext = Request.GetOwinContext();
            var authManager = owinContext.Authentication;
            authManager.SignOut("ApplicationCookie");
            Session.Abandon();
            return RedirectToAction("index", "default");
        }
    }
}