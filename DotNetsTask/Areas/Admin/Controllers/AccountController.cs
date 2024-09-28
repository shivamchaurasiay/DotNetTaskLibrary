using AutoMapper;
using DotNetsTask.Data.Models;
using DotNetsTask.Services;
using DotNetTask.Dto;

using Microsoft.AspNetCore.Mvc;
using NToastNotify;




namespace DotNetsTask.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class AccountController : BaseController
    {

        private readonly IUsersService usersService;
		private readonly IMapper _mapper;
		private readonly IToastNotification _toastNotification;
        public AccountController(IMapper mapper,IUsersService _usersService, IToastNotification toastNotification)
        {
            usersService = _usersService;
            _toastNotification = toastNotification;
			_mapper = mapper;
		}
      
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = usersService.GetUserByEmailOrUserName(model?.Name); 

                    if (user != null && model?.Password?.Trim() == user.Password)
                    {
                         //CreateAuthenticationTicket(user);
                        if (user.RoleId == 1)
                        {
							return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
						}
						else
                        {
                            return RedirectToAction("Login", "Account", new { Area = "Admin" });
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                       
                        return View("Login", model);
                    }
                }
                else
                {
                    return View("Login", model);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error!", "Something went wrong");            
                return View("Login", model);
            }
        }

		public IActionResult Profile()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Register(UserDto model)
		{
			try
			{
				if (model == null)
				{
					return View(model);
				}

				if (!ModelState.IsValid)
				{
					return View(model);
				}

				var data = _mapper.Map<User>(model);
				data.IsActive = true;
				data.CreatedOn = DateTime.UtcNow;
				data.RoleId = 1;

				await usersService.SaveUser(data); // Assuming SaveUser is async

				_toastNotification.AddSuccessToastMessage("Data Saved Successfully");

				return RedirectToAction("Register");
			}
			catch (Exception ex)
			{
				_toastNotification.AddErrorToastMessage("Something went wrong");
				return View(model);
			}
		}


		public IActionResult Settings()
		{
			return View();
		}
		public IActionResult Logout()
		{
			return View();
		}
		public IActionResult ForgotPassword()
		{
			return View();
		}

	}
}
