using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using ServiceLayer;
using System.Collections.Generic;
using System;
using System.Net;
using Nest;
using RentalCar.DomainLayer.CommonObjects.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DomainLayer.CommonObjects;
using Elasticsearch.Net;
using ZoomNet.Models;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Http;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis.Differencing;
using System.Data;
using Microsoft.Extensions.Options;
using ServiceLayer.Interface;
using EsnadTakaful.ServiceLayer.Interface;
using DomainLayer.LookUpModels;
using DomainLayer.DTO_EXT;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using DomainLayer.DTO;
using Microsoft.AspNetCore.Hosting;
using Ganss.Xss;

namespace RentalCar.Controllers
{

	public class AdminController : Controller
	{
		private readonly RentalCarDbContext _context;
		private readonly IAdmin _IAdminServices;
		private readonly IAnnouncement _IAnnouncementServices;
		private readonly IAppLabel _IAppLabelServices;
		private readonly IUser _IUserServices;
		private readonly EUserManager _EUserManager;
		private readonly IEUser _IEUserServices;
		private readonly IWebHostEnvironment _env;
		private readonly IAppSettings _IAppSettingsServices;
		public AdminController(
			RentalCarDbContext context,
			IAdmin adminServices,
			IAnnouncement announcementServices,
			IAppLabel appLabelServices,
			IUser userServices,
			EUserManager euserManager,
			IEUser eUserServices,
			IWebHostEnvironment env,
			IAppSettings appSettingsServices
		)
		{
			_context = context;
			_IAdminServices = adminServices;
			_IAnnouncementServices = announcementServices;
			_IAppLabelServices = appLabelServices;
			_IUserServices = userServices;
			_EUserManager = euserManager;
			_IEUserServices = eUserServices;
			_env = env;
			_IAppSettingsServices = appSettingsServices;
		}

		#region Login
		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		#endregion

		#region Login Post
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult> Login(UserLoginRequest user)
		{
			try
			{
				if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
				{
					TempData["PopupMessage"] = "Invalid Credentials";
					return View();
				}

				var existingEUser = await _context.EUsers
                    .Where(e => e.Email == user.Email && !e.Is_deleted)
					.FirstOrDefaultAsync();

				if (existingEUser != null)
				{
					var isCorrect = await _EUserManager.CheckPasswordAsync(existingEUser, user.Password);
					

					if (isCorrect)
					{
						var role = await _EUserManager.GetRolesAsync(existingEUser);

						HttpContext.Session.SetString("RentalCarId", existingEUser.Id);
						HttpContext.Session.SetString("RentalCarUserName", existingEUser.FirstName_en);

						string delimitedString = string.Join(",", role);
						if (role.Any(r => r != "EUser"))
						{
							HttpContext.Session.SetString("RentalCarUserRoles", delimitedString);
						}

						return RedirectToAction("Index", "Admin");
					}

					TempData["PopupMessage"] = "Invalid Credentials";
					return View();
				}

				TempData["PopupMessage"] = "Invalid Credentials";
				return View();
			}
			catch (Exception ex)
			{
				Tools.WriteToFile("Admin Index ---" + ex, "");
				return RedirectToAction("Index", "Error");
			}
		}

		#endregion

		#region Error
		[HttpGet]
		public IActionResult Error()
		{

			return View();
		}
		#endregion

		#region Index
		
		[HttpGet]
		public IActionResult Index()
		{

			return View();
		}
		#endregion

		#region Menu
		
		[HttpGet]
		public async Task<IActionResult> Menu()
		{
			try
			{
				// =======================
				// English Menu
				// =======================
				var menuResult = await _IAdminServices.GetContent<Menu>(
					AdminTables.Menu, "en", 99
				);

				var menu = menuResult?.Contents != null
					? menuResult.Contents.OrderBy(e => e.PositionNumber).ToList()
					: new List<Menu>();

				ViewBag.MenuEn = menu;

				// =======================
				// Arabic Menu
				// =======================
				var menuResultAr = await _IAdminServices.GetContent<Menu>(
					AdminTables.Menu, "ar", 99
				);

				var menu_ar = menuResultAr?.Contents != null
					? menuResultAr.Contents.OrderBy(e => e.PositionNumber).ToList()
					: new List<Menu>();

				ViewBag.MenuAr = menu_ar;

				return View();
			}
			catch (Exception ex)
			{
				ViewBag.MenuEn = new List<Menu>();
				ViewBag.MenuAr = new List<Menu>();
				ViewBag.ErrorMessage = "An error occurred while loading the menu: " + ex.Message;

				return View();
			}
		}
		#endregion


		//[HttpPost]
		//public IActionResult EditMenu([FromBody] EditMenuModel model)
		//{
		//	try
		//	{
		//		if (!ModelState.IsValid)
		//		{
		//			return Json(new
		//			{
		//				success = false,
		//				message = "Invalid data submitted."
		//			});
		//		}

		//		// Update English menu
		//		_IAdminServices.DeleteContent(model.MenuEnId);

		//		// Update Arabic menu
		//		_IAdminServices.DeleteContent(model.MenuArId);

		//		if (resultEn && resultAr)
		//		{
		//			return Json(new
		//			{
		//				success = true,
		//				message = "Menu updated successfully."
		//			});
		//		}

		//		return Json(new
		//		{
		//			success = false,
		//			message = "Failed to update menu. Please try again."
		//		});
		//	}
		//	catch (Exception ex)
		//	{
		//		// Optional: log the exception
		//		return Json(new
		//		{
		//			success = false,
		//			message = "An unexpected error occurred while updating the menu."
		//		});
		//	}
		//}

		#region Edit Menu
		
		[HttpPost]
		public async Task<IActionResult> EditMenu([FromBody] EditMenuModel model)
		{
			// Validate required fields
			if (model == null || string.IsNullOrWhiteSpace(model.TitleEn) ||
				string.IsNullOrWhiteSpace(model.TitleAr))
			{
				return BadRequest(new
				{
					success = false,
					message = "Invalid request data."
				});
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(new
				{
					success = false,
					message = "Validation failed.",
					errors = ModelState.Values.SelectMany(v => v.Errors)
											  .Select(e => e.ErrorMessage)
				});
			}

			// English menu
			var menuEn = new Menu
			{
				Id = model.MenuEnId,
				Title = model.TitleEn,
				Action = model.Action,
				is_subTest = model.is_subTest,
				PositionNumber = model.PositionNumber,
			};

			// Arabic menu
			var menuAr = new Menu
			{
				Id = model.MenuArId,
				Title = model.TitleAr,
				Action = model.Action,
				is_subTest = model.is_subTest,
				PositionNumber = model.PositionNumber
			};

			try
			{
				// Remove old English menu (base record)
				_IAdminServices.DeleteContent(menuEn.Id);

				// Recreate English menu
				long idnew = await _IAdminServices.AddContent(menuEn, AdminTables.Menu, 2);

				// Recreate Arabic translation
				await _IAdminServices.AddContentLang<Menu>(
					menuAr,
					AdminTables.Menu,
					idnew,
					1
				);

				return Ok(new
				{
					success = true,
					message = "Menu updated successfully."
				});
			}
			catch (Exception ex)
			{
				// TODO: log ex (ILogger)
				return StatusCode(500, new
				{
					success = false,
					message = "An unexpected error occurred while updating the menu."
				});
			}
		}
		#endregion

		#region Delete Menu
		
		[HttpPost]
		public IActionResult DeleteMenu([FromBody] EditMenuModel model)
		{
			try
			{
				if (model.MenuEnId <= 0)
				{
					return Json(new
					{
						success = false,
						message = "Invalid menu ID(s)."
					});
				}

				// Delete English menu
				_IAdminServices.DeleteContent(model.MenuEnId);

				return Json(new
				{
					success = true,
					message = "Menu deleted successfully."
				});
			}
			catch (Exception ex)
			{
				// Optional: log the exception
				return Json(new
				{
					success = false,
					message = "An error occurred while deleting the menu."
				});
			}
		}
		#endregion

		#region Announcements
		
		[HttpGet]
		public async Task<IActionResult> Announcements()
		{
			try
			{
				var announcementEn = await _IAnnouncementServices.GetAllByLanguageIdAsync(2);
				ViewBag.AnnouncementEn = announcementEn.Data ?? new List<AnnouncementDTO>();

				var announcementAr = await _IAnnouncementServices.GetAllByLanguageIdAsync(1);
				ViewBag.AnnouncementAr = announcementAr.Data ?? new List<AnnouncementDTO>();

				return View();
			}
			catch (Exception ex)
			{
			
				ViewBag.ErrorMessage = "An error occurred while loading the menu: " + ex.Message;
				return View();
			}
		}
		#endregion


		#region Add Announcement
		
		[HttpPost]
		public IActionResult AddAnnouncement(AnnouncementDTO_Ext model)
		{
			try
			{
				// Validate required fields
				if (model == null ||
					string.IsNullOrWhiteSpace(model.TitleEn) ||
					string.IsNullOrWhiteSpace(model.TitleAr))
				{
					return Json(new
					{
						success = false,
						message = "English and Arabic titles are required."
					});
				}

				// Create English announcement
				var announcementDtoEn = new AnnouncementDTO
				{
					Content = model.TitleEn,
					LanguageId = 2,
					PublishDate = DateTime.Now,
					Created_at = DateTime.Now,
					Updated_at = DateTime.Now,
					Icon = model.Icon,           
					Image = model.Image          
				};

				var resultEn = _IAnnouncementServices.Add(announcementDtoEn);

				// Create Arabic announcement
				var announcementDtoAr = new AnnouncementDTO
				{
					Content = model.TitleAr,
					LanguageId = 1,
					PublishDate = DateTime.Now,
					Created_at = DateTime.Now,
					Updated_at = DateTime.Now,
					Icon = model.Icon,          
					Image = model.Image          
				};

				var resultAr = _IAnnouncementServices.Add(announcementDtoAr);

				// Check if both inserts succeeded
				bool success = (resultEn.Data && resultAr.Data);
				string message = success ? "Announcement added successfully." : "Failed to add announcement.";

				return Json(new
				{
					success = success,
					message = message
				});
			}
			catch (Exception ex)
			{
				// Log exception if needed
				return Json(new
				{
					success = false,
					message = "An error occurred while adding the announcement. Please try again.",
					serverMessage = ex.Message
				});
			}
		}
		#endregion

		#region Edit Announcement
		
		[HttpPost]
		public IActionResult EditAnnouncement([FromBody] AnnouncementDTO_Ext model)
		{
			// Validate required fields
			if (model == null || string.IsNullOrWhiteSpace(model.TitleEn) ||
				string.IsNullOrWhiteSpace(model.TitleAr))
			{
				return Json(new
				{
					success = false,
					message = "Invalid request data."
				});
			}

			if (!ModelState.IsValid)
			{
				return Json(new
				{
					success = false,
					message = "Validation failed.",
					errors = ModelState.Values.SelectMany(v => v.Errors)
											  .Select(e => e.ErrorMessage)
				});
			}

			try
			{
				// English
				var announcementEn = new AnnouncementDTO
				{
					Id = model.IdEn,
					Content = model.TitleEn,
					LanguageId = 2,
					Created_at = model.CreatedAt,
					PublishDate = model.PublishDate,
					Icon = model.Icon,
					Image = model.Image
				};

				// Arabic
				var announcementAr = new AnnouncementDTO
				{
					Id = model.IdAr,
					Content = model.TitleAr,
					LanguageId = 1,
					Updated_at = DateTime.Now,
					Created_at = model.CreatedAt,
					PublishDate = model.PublishDate,
					Icon = model.Icon,
					Image = model.Image
				};

				// Update
				var resultEn = _IAnnouncementServices.Update(announcementEn);
				var resultAr = _IAnnouncementServices.Update(announcementAr);

				bool success = (resultEn.Data && resultAr.Data);
				string message = success ? "Announcement updated successfully." : "Failed to update announcement.";

				return Json(new
				{
					success = success,
					message = message
				});
			}
			catch (Exception ex)
			{
				return Json(new
				{
					success = false,
					message = "An unexpected error occurred while updating the announcement."
				});
			}
		}
		#endregion

		#region Delete Announcement
		
		[HttpPost]
		public IActionResult DeleteAnnouncement([FromBody] AnnouncementDTO_Ext model)
		{
			try
			{
				if (model.IdEn <= 0 || model.IdAr <= 0)
				{
					return Json(new
					{
						success = false,
						message = "Invalid announcement ID(s)."
					});
				}

				// Delete
				var resultEn = _IAnnouncementServices.Delete(model.IdEn);
				var resultAr = _IAnnouncementServices.Delete(model.IdAr);


				// Check if both inserts succeeded
				bool success = (resultEn.Data && resultAr.Data);
				string message = success ? "Announcement deleted successfully." : "Failed to delete announcement .";

				return Json(new
				{
					success = success,
					message = message
				});
			}
			catch (Exception ex)
			{
				// Optional: log the exception
				return Json(new
				{
					success = false,
					message = "An error occurred while deleting the announcement."
				});
			}
		}
		#endregion


		#region Website Labels
		
		[HttpGet]
		public async Task<IActionResult> websiteLabels()
		{
			try
			{
				var websiteLabelEn = await _IAppLabelServices.GetAllByLanguageIdAsync(2);
				ViewBag.websiteLabelEn = websiteLabelEn.Data ?? new List<AppLabelDTO>();

				var websiteLabelAr = await _IAppLabelServices.GetAllByLanguageIdAsync(1);
				ViewBag.websiteLabelAr = websiteLabelAr.Data ?? new List<AppLabelDTO>();

				return View();
			}
			catch (Exception ex)
			{

				ViewBag.ErrorMessage = "An error occurred while loading the website label: " + ex.Message;
				return View();
			}
		}
		#endregion

		#region Add Website Label
		
		[HttpPost]
		public IActionResult AddWebsiteLabel(websiteLabelDTOExt model)
		{
			try
			{
				// Validate required fields
				if (model == null ||
					string.IsNullOrWhiteSpace(model.TitleEn) ||
					string.IsNullOrWhiteSpace(model.TitleAr) ||
					string.IsNullOrWhiteSpace(model.WebsiteLabelName))
				{
					return Json(new
					{
						success = false,
						message = "All data is required."
					});
				}

				// Create English
				var websiteLabelDtoEn = new AppLabelDTO
				{

					LabelName = model.WebsiteLabelName,
					FriendlyName = model.WebsiteLabelName,
					Value = model.TitleEn,
					Desc = model.WebsiteLabelName,
					LanguagId = 2,
					Created_at = DateTime.Now,
					Updated_at = DateTime.Now
				};

				var resultEn = _IAppLabelServices.Add(websiteLabelDtoEn);

				// Create Arabic
				var websiteLabelDtoAr = new AppLabelDTO
				{
					LabelName = model.WebsiteLabelName,
					FriendlyName = model.WebsiteLabelName,
					Value = model.TitleAr,
					Desc = model.WebsiteLabelName,
					LanguagId = 1,
					Created_at = DateTime.Now,
					Updated_at = DateTime.Now
				};

				var resultAr = _IAppLabelServices.Add(websiteLabelDtoAr);

				// Check if both inserts succeeded
				bool success = (resultEn.Data && resultAr.Data);
				string message = success ? "Website Label added successfully." : "Failed to add website label.";

				return Json(new
				{
					success = success,
					message = message
				});
			}
			catch (Exception ex)
			{
				// Log exception if needed
				return Json(new
				{
					success = false,
					message = "An error occurred while adding the website label. Please try again.",
					serverMessage = ex.Message
				});
			}
		}
		#endregion

		#region Edit Website Label
		
		[HttpPost]
		public  async Task<IActionResult> EditWebsiteLabel([FromBody] websiteLabelDTOExt model)
		{
			// Validate required fields
			if (model == null || string.IsNullOrWhiteSpace(model.TitleEn) ||
				string.IsNullOrWhiteSpace(model.TitleAr) ||
				string.IsNullOrWhiteSpace(model.WebsiteLabelName) 
				)
			{
				return BadRequest(new
				{
					success = false,
					message = "Invalid request data."
				});
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(new
				{
					success = false,
					message = "Validation failed.",
					errors = ModelState.Values.SelectMany(v => v.Errors)
											  .Select(e => e.ErrorMessage)
				});
			}

			// English
			var websiteLabelEn = new AppLabelDTO
			{
				Id = model.websiteLabelEnId,
				LabelName = model.WebsiteLabelName,
				FriendlyName = model.WebsiteLabelName,
				Value = model.TitleEn,
				Desc = model.WebsiteLabelName,
				LanguagId = 2,
				Updated_at = DateTime.Now
			};

			// Arabic
			var websiteLabelAr = new AppLabelDTO
			{
				Id = model.websiteLabelArId,
				LabelName = model.WebsiteLabelName,
				FriendlyName = model.WebsiteLabelName,
				Value = model.TitleAr,
				Desc = model.WebsiteLabelName,
				LanguagId = 1,
				Updated_at = DateTime.Now
			};

			try
			{
				// Edit
				var resultEn =  _IAppLabelServices.Update(websiteLabelEn);
				var resultAr =  _IAppLabelServices.Update(websiteLabelAr);

				// Check if both inserts succeeded
				bool success = (resultEn.Data && resultAr.Data);
				string message = success ? "Website Label updated successfully." : "Failed to update website label .";

				return Json(new
				{
					success = success,
					message = message
				});
			}
			catch (Exception ex)
			{
				// TODO: log ex (ILogger)
				return StatusCode(500, new
				{
					success = false,
					message = "An unexpected error occurred while updating the website label."
				});
			}
		}
		#endregion

		#region Delete Website Label
		
		[HttpPost]
		public IActionResult DeleteWebsiteLabel([FromBody] websiteLabelDTOExt model)
		{
			try
			{
				if (model.websiteLabelEnId <= 0 || model.websiteLabelArId <=0)
				{
					return Json(new
					{
						success = false,
						message = "Invalid website label ID(s)."
					});
				}

				// Delete
				var resultEn = _IAppLabelServices.Delete(model.websiteLabelEnId);
				var resultAr = _IAppLabelServices.Delete(model.websiteLabelArId);

				// Check if both inserts succeeded
				bool success = (resultEn.Data && resultAr.Data);
				string message = success ? "Website Label deleted successfully." : "Failed to delete website label .";

				return Json(new
				{
					success = success,
					message = message
				});
			}
			catch (Exception ex)
			{
				// Optional: log the exception
				return Json(new
				{
					success = false,
					message = "An error occurred while deleting the website label."
				});
			}
		}
		#endregion

		#region Users
		[HttpGet]
		public async Task<IActionResult> Users()
		{
			try
			{
				//Users
				var users = await _IEUserServices.GetAllAsync();
				ViewBag.Users = users;

				//Roles
				var AllRoles = await _IEUserServices.GetAllRoles();
				ViewBag.AllRoles = AllRoles.Data;

				return View();
			}
			catch (Exception ex)
			{

				ViewBag.ErrorMessage = "An error occurred while loading the menu: " + ex.Message;
				return View();
			}
		}
		#endregion

		#region Add User
		[HttpGet]
		public async Task<IActionResult> AddUser()
		{
			try
			{
				//Roles
				var AllRoles = await _IEUserServices.GetAllRoles();
				ViewBag.AllRoles = AllRoles.Data;

				return View();
			}
			catch (Exception ex)
			{

				ViewBag.ErrorMessage = "An error occurred while loading the menu: " + ex.Message;
				return View();
			}
		}
		#endregion

		#region Add User
		[HttpPost]
		public async Task<IActionResult> AddUser([FromForm] EUserRegisterDTO model)
		{
			if (model == null)
				return Json(new { success = false, message = "Invalid data." });

			// Check required fields
			if (string.IsNullOrWhiteSpace(model.FirstName_en) ||
				string.IsNullOrWhiteSpace(model.LastName_en) ||
				string.IsNullOrWhiteSpace(model.FirstName_ar) ||
				string.IsNullOrWhiteSpace(model.LastName_ar) ||
				string.IsNullOrWhiteSpace(model.Email) ||
				string.IsNullOrWhiteSpace(model.PhoneNumber) ||
				string.IsNullOrWhiteSpace(model.Role) ||
				string.IsNullOrWhiteSpace(model.Password))
			{
				return Json(new { success = false, message = "Please fill in all required fields." });
			}

			// ===== HANDLE IMAGE UPLOAD (CORRECT) =====
			if (model.Image != null && model.Image.Length > 0)
			{
				var file = model.Image;

				var ext = Path.GetExtension(file.FileName).ToLower();
				var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };

				if (!allowed.Contains(ext))
				{
					return Json(new { success = false, message = "Invalid image type." });
				}

				var fileName = Guid.NewGuid() + ext;

				var uploadPath = Path.Combine(_env.WebRootPath, "Images", "Profiles");

				if (!Directory.Exists(uploadPath))
					Directory.CreateDirectory(uploadPath);

				var fullPath = Path.Combine(uploadPath, fileName);

				using (var stream = new FileStream(fullPath, FileMode.Create))
				{
					await file.CopyToAsync(stream);   
				}

				model.ProfileImage = fileName;
			}
			else
			{
				return Json(new { success = false, message = "Profile image is required." });
			}

			// ===== Password validation =====
			var missingRequirements = new List<string>();

			if (model.Password.Length < 8)
				missingRequirements.Add("Length must be at least 8 characters.");
			if (!Regex.IsMatch(model.Password, "[A-Z]"))
				missingRequirements.Add("Must contain at least one uppercase letter.");
			if (!Regex.IsMatch(model.Password, "[a-z]"))
				missingRequirements.Add("Must contain at least one lowercase letter.");
			if (!Regex.IsMatch(model.Password, "\\d"))
				missingRequirements.Add("Must contain at least one digit.");
			if (!Regex.IsMatch(model.Password, "[!@#$%^&*(),.?\":{}|<>]"))
				missingRequirements.Add("Must contain at least one special character.");

			if (missingRequirements.Any())
			{
				return Json(new
				{
					success = false,
					message = "Password does not meet requirements: <ul>" +
							  string.Join("", missingRequirements.Select(r => $"<li>{r}</li>")) +
							  "</ul>"
				});
			}

			// ===== CALL SERVICE LAYER =====
			var addUser = _IEUserServices.Add(model);

			return Json(new { success = true, message = "User added successfully!" });
		}
        #endregion

        #region Edit User
        [HttpPost]
		public async Task<IActionResult> EditUser(EUserRegisterDTO model, IFormFile ProfileImage)
		{
			return Json(new { success = true, message = "User updated successfully!" });
		}
		#endregion

		#region Delete User
		[HttpPost]
		public async Task<IActionResult> DeleteUser(EUserRegisterDTO model)
		{
			return Json(new { success = true, message = "User deleted successfully!" });
		}
		#endregion

		#region About
		[HttpGet]
		public async Task<IActionResult> About()
		{
			try
			{
				// ABOUT US
				//English
				var aboutUsResultEn = await _IAdminServices
					.GetContent<AboutUs>(AdminTables.AboutUs, "en", 99);

				AboutUs aboutUsEn = aboutUsResultEn?.Contents?
					.OrderBy(e => e.Id)
					.FirstOrDefault();

				ViewBag.AboutUsEn = aboutUsEn ?? new AboutUs();

				//Arabic
				var aboutUsResultAr = await _IAdminServices
					.GetContent<AboutUs>(AdminTables.AboutUs, "ar", 99);

				AboutUs aboutUsAr = aboutUsResultAr?.Contents?
					.OrderBy(e => e.Id)
					.FirstOrDefault();

				ViewBag.AboutUsAr = aboutUsAr ?? new AboutUs();

				return View();
			}
			catch (Exception ex)
			{

				ViewBag.ErrorMessage = "An error occurred while loading the about section: " + ex.Message;
				return View();
			}
		}
		#endregion

		#region Edit About Us
		[HttpPost]
		public async Task<IActionResult> EditAboutUs(AboutUs_EXT model, IFormFile AboutUsUploadImage)
		{
			if (model == null)
				return BadRequest(new { success = false, message = "Request body cannot be empty." });

			// Basic field validation
			var missingFields = new List<string>();
			if (string.IsNullOrWhiteSpace(model.AboutUsArTitle)) missingFields.Add("AboutUsArTitle");
			if (string.IsNullOrWhiteSpace(model.AboutUsEnTitle)) missingFields.Add("AboutUsEnTitle");
			if (string.IsNullOrWhiteSpace(model.AboutUsArDescription)) missingFields.Add("AboutUsArDescription");
			if (string.IsNullOrWhiteSpace(model.AboutUsEnDescription)) missingFields.Add("AboutUsEnDescription");

			if (missingFields.Any())
				return BadRequest(new { success = false, message = "Missing required fields.", fields = missingFields });

			if (!ModelState.IsValid)
				return BadRequest(new { success = false, message = "Validation failed.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

			// Sanitize HTML from TinyMCE
			var sanitizer = new HtmlSanitizer();
			var cleanEnDesc = sanitizer.Sanitize(model.AboutUsEnDescription);
			var cleanArDesc = sanitizer.Sanitize(model.AboutUsArDescription);

			// Map AboutUs objects
			var aboutUsEn = new AboutUs
			{
				Id = model.AboutUsEnId,
				Title = model.AboutUsEnTitle,
				Description = cleanEnDesc // sanitized
			};

			var aboutUsAr = new AboutUs
			{
				Id = model.AboutUsArId,
				Title = model.AboutUsArTitle,
				Description = cleanArDesc // sanitized
			};

			// Handle image upload if provided
			if (AboutUsUploadImage != null && AboutUsUploadImage.Length > 0)
			{
				var uploadResult = await SaveImageAsync(AboutUsUploadImage);
				if (!uploadResult.Success)
					return Json(new { success = false, message = uploadResult.Message });

				aboutUsEn.Image = uploadResult.FileName;
				aboutUsAr.Image = uploadResult.FileName;
			}

			try
			{
                _IAdminServices.DeleteContent(aboutUsEn.Id);

                long newId = await _IAdminServices.AddContent(aboutUsEn, AdminTables.AboutUs, 2);

                await _IAdminServices.AddContentLang<AboutUs>(
                    aboutUsAr,
                    AdminTables.AboutUs,
                    newId,
                    1
                );

                return Ok(new { success = true, message = "About Us Section updated successfully." });
			}
			catch (Exception ex)
			{
				// TODO: log ex (ILogger)
				return StatusCode(500, new { success = false, message = "An unexpected error occurred while updating the About Us section." });
			}
		}

		/// <summary>
		/// Handles image upload and validation
		/// </summary>
		private async Task<(bool Success, string FileName, string Message)> SaveImageAsync(IFormFile file)
		{
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
			var ext = Path.GetExtension(file.FileName).ToLower();

			if (!allowedExtensions.Contains(ext))
				return (false, null, "Invalid image type. Allowed types: jpg, jpeg, png, webp.");

			var fileName = Guid.NewGuid() + ext;
			var uploadPath = Path.Combine(_env.WebRootPath, "Images");

			if (!Directory.Exists(uploadPath))
				Directory.CreateDirectory(uploadPath);

			var fullPath = Path.Combine(uploadPath, fileName);

			try
			{
				await using var stream = new FileStream(fullPath, FileMode.Create);
				await file.CopyToAsync(stream);
				return (true, fileName, null);
			}
			catch (Exception ex)
			{
				// TODO: log exception
				return (false, null, "Failed to save the image.");
			}
		}

		#endregion

		#region Contact
		[HttpGet]
		public IActionResult Contact()
		{
			try
			{
				var geninfo = _IAppSettingsServices.Get();

				if (geninfo == null || geninfo.Data == null)
				{
					return View(new AppSettingsDTO());
				}

				return View(geninfo.Data);
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = "An error occurred while loading the contact section: " + ex.Message;
				return View(new AppSettingsDTO());
			}
		}
		#endregion

		#region Edit General Settings
		[HttpPost]
		public async Task<IActionResult> EditGeneralSettings(AppSettingsDTO model, IFormFile LogoUpload)
		{
			try
			{

				if (LogoUpload != null)
				{
					var result = await SaveImageAsync(LogoUpload);
					if (result.Success)
					{
						model.Logo = result.FileName;
					}
					else
					{
						return Json(new { success = false, message = result.Message });
					}
				}

				// Sanitize HTML from TinyMCE
				var sanitizer = new HtmlSanitizer();
                if (!string.IsNullOrEmpty(model.ShortDescription))
                {
					model.ShortDescription = sanitizer.Sanitize(model.ShortDescription);
				} 
				
				if (!string.IsNullOrEmpty(model.Description))
                {
					model.Description = sanitizer.Sanitize(model.Description);
				}
				
				if (!string.IsNullOrEmpty(model.PrivacyPolicy))
                {
					model.PrivacyPolicy = sanitizer.Sanitize(model.PrivacyPolicy);
				}
				
				if (!string.IsNullOrEmpty(model.RefundPolicy))
                {
					model.RefundPolicy = sanitizer.Sanitize(model.RefundPolicy);
				}
				
				if (!string.IsNullOrEmpty(model.TermsConditions))
                {
					model.TermsConditions = sanitizer.Sanitize(model.TermsConditions);
				}
				

				_IAppSettingsServices.Update(model);

				return Json(new
				{
					success = true,
					message = "General settings updated successfully.",
				});
			}
			catch
			{
				return Json(new { success = false, message = "An error occurred while updating the settings." });
			}
		}
		#endregion

	}
}
