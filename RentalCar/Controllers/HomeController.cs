using DomainLayer.CommonObjects;
using DomainLayer.DTO_EXT;
using DomainLayer.LookUpModels;
using EsnadTakaful.ServiceLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.DomainLayer.DTO;
using RentalCar.ServiceLayer.Interface;

namespace RentalCar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILanguage _ILanguage;
        private readonly IAdmin _IAdminServices;
        private readonly IAppLabel _IAppLabelServices;
        private readonly IContactus _IContactUs;

        public HomeController(ILanguage ILanguage,
          IAdmin IAdminServices,
          IAppLabel IAppLabelServices,
          IContactus IContactUs

        )
        {
            _ILanguage = ILanguage;
            _IAdminServices = IAdminServices;
            _IAppLabelServices = IAppLabelServices;
            _IContactUs = IContactUs;
        }

        #region Cookies - Set Language
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                "RentalCarLang", 
                culture,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddMonths(1), //AddYears(1)
                    HttpOnly = false,
                    Path = "/"
                }
            );

            return LocalRedirect(returnUrl);
        }
        #endregion

        #region Page Not Found
        public IActionResult PageNotFound(int code)
        {
            return View();
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {

            try
            {
                string langCode = _ILanguage.GetCurrentLanguage(HttpContext);

                var langData = _ILanguage.Get(langCode);
                ViewBag.CurrentLanguage = langData;
                ViewBag.Direction = langData.LanguageCode == "ar" ? "rtl" : "ltr";

                var languages = _ILanguage.GetAll();
                ViewBag.languages = languages.Data;

                //Label 
                var title = await _IAppLabelServices.GetValAsync("websiteTitle", langData.Id);
                ViewBag.WebsiteTitle = title;

                var classLabel = await _IAppLabelServices.GetValAsync("Class_Label", langData.Id);
                ViewBag.ClassLabel = classLabel; 
                
                var quranEnglish = await _IAppLabelServices.GetValAsync("Quran_Sura_Label", 2);
                ViewBag.QuranEnglish = quranEnglish; 
                
                var quranArabic = await _IAppLabelServices.GetValAsync("Quran_Sura_Label", 1);
                ViewBag.QuranArabic = quranArabic;
                
                var moreLabel = await _IAppLabelServices.GetValAsync("More_Label", langData.Id);
                ViewBag.MoreLabel = moreLabel; 
                
                var registerLabel = await _IAppLabelServices.GetValAsync("Register_Label", langData.Id);
                ViewBag.RegisterLabel = registerLabel; 
               
                //var programsLabel = _IAppLabelServices.GetValAsync("Programs_Label", langData.Id);
                //ViewBag.ProgramsLabel = programsLabel;

                // VISION
                var visionResult = await _IAdminServices
                    .GetContent<Vision>(AdminTables.Vision, langCode, 99);

                Vision vision = visionResult?.Contents?
                    .OrderBy(e => e.Id)
                    .FirstOrDefault();

                ViewBag.Vision = vision ?? new Vision();

                // MISSION
                var missionResult = await _IAdminServices
                    .GetContent<Mission>(AdminTables.Mission, langCode, 99);

                Mission mission = missionResult?.Contents?
                    .OrderBy(e => e.Id)
                    .FirstOrDefault();

                ViewBag.Mission = mission ?? new Mission();
                
                // ABOUT US
                var aboutUsResult = await _IAdminServices
                    .GetContent<AboutUs>(AdminTables.AboutUs, langCode, 99);

                AboutUs aboutUs = aboutUsResult?.Contents?
                    .OrderBy(e => e.Id)
                    .FirstOrDefault();

                ViewBag.AboutUs = aboutUs ?? new AboutUs();

                string currentAction = "/Home/" + ControllerContext.ActionDescriptor.ActionName;
                ViewBag.CurrentUrl = currentAction;
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region Contact
        public IActionResult Contact()
        {
            string currentAction = "/Home/" + ControllerContext.ActionDescriptor.ActionName;
            ViewBag.CurrentUrl = currentAction;
            return View();
        }
        #endregion

        #region Contact - Send Message
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(ContactUsDTOExt model)
        {
            // Required fields check
            if (string.IsNullOrWhiteSpace(model.FullName) ||
                string.IsNullOrWhiteSpace(model.Message) ||
                string.IsNullOrWhiteSpace(model.Email))
            {
                TempData["ErrorContactMessage"] = "All fields are required.";
                return RedirectToAction("Contact");
            }

            // Email format validation
            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(model.Email))
            {
                TempData["ErrorContactMessage"] = "Please enter a valid email address.";
                return RedirectToAction("Contact");
            }

            //if (!Regex.IsMatch(model.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            //{
            //    TempData["ErrorContactMessage"] = "Please enter a valid email address.";
            //    return RedirectToAction("Contact");
            //}

            // Auto values
            model.Subject = $"RentalCar Quran Institute (Contact Form) - New message from {model.FullName}";
            model.Created_at = DateTime.Now;
            model.Updated_at = DateTime.Now;
            model.Is_Seen = false;

            _IContactUs.AddMessage(model);

            TempData["SuccessContactMessage"] = "Your message has been sent successfully.";

            return RedirectToAction("Contact");
        }
        #endregion

        #region About Us
        public async Task<IActionResult> About()
        {
            string langCode = _ILanguage.GetCurrentLanguage(HttpContext);

            string currentAction = "/Home/" + ControllerContext.ActionDescriptor.ActionName;
            ViewBag.CurrentUrl = currentAction;

            // ABOUT US
            var aboutUsResult = await _IAdminServices
                .GetContent<AboutUs>(AdminTables.AboutUs, langCode, 99);

            AboutUs aboutUs = aboutUsResult?.Contents?
                .OrderBy(e => e.Id)
                .FirstOrDefault();

            ViewBag.AboutUs = aboutUs ?? new AboutUs();

            return View();
        }
        #endregion

        #region Classes
        public IActionResult Classes()
        {
            string currentAction = "/Home/" + ControllerContext.ActionDescriptor.ActionName;
            ViewBag.CurrentUrl = currentAction;
            return View();
        }
        #endregion

        #region Programs
        public IActionResult Programs()
        {
            string currentAction = "/Home/" + ControllerContext.ActionDescriptor.ActionName;
            ViewBag.CurrentUrl = currentAction;
            return View();
        }
        #endregion
        
        #region FAQ
        public IActionResult FAQ()
        {

            return View();
        }
        #endregion
        
        #region Privacy Policy
        public IActionResult PrivacyPolicy()
        {

            return View();
        }
        #endregion
    }
}
