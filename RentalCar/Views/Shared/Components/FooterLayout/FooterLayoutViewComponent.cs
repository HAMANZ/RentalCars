using DomainLayer.CommonObjects;
using DomainLayer.LookUpModels;
using EsnadTakaful.ServiceLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.ServiceLayer.Interface;

namespace RentalCar.Views.Components.FooterLayout
{
    public class FooterLayoutViewComponent : ViewComponent
    {
        private readonly ILanguage _ILanguage;
        private readonly IAdmin _IAdminServices;
        private readonly IAppLabel _IAppLabelServices;
        public FooterLayoutViewComponent(ILanguage ILanguage,
          IAdmin IAdminServices,
          IAppLabel IAppLabelServices
        )
        {
            _ILanguage = ILanguage;
            _IAdminServices = IAdminServices;
            _IAppLabelServices = IAppLabelServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                //Menu
                //List<Menu> menu = _AdminServices.GetContent<Menu>(AdminTables.Menu, languageCode, 99).Contents.ToList();
                //ViewBag.Menu = menu;
                string langCode = _ILanguage.GetCurrentLanguage(HttpContext);

                var langData = _ILanguage.Get(langCode);
                ViewBag.CurrentLanguage = langData;
                ViewBag.Direction = langData.LanguageCode == "ar" ? "rtl" : "ltr";

                //Labels
                var title = await _IAppLabelServices.GetValAsync("websiteTitle", langData.Id);
                ViewBag.WebsiteTitle = title; 
                
                var privacyPolicyLabel = await _IAppLabelServices.GetValAsync("Privacy_Policy_Label", langData.Id);
                ViewBag.PrivacyPolicyLabel = privacyPolicyLabel; 
                
                var FAQLabel = await _IAppLabelServices.GetValAsync("FAQ_Label", langData.Id);
                ViewBag.FAQLabel = FAQLabel;

                // MAIN MENU
                var menuResult = await _IAdminServices.GetContent<Menu>(AdminTables.Menu, langCode, 99);

                var menu = menuResult?.Contents?
                                     .OrderBy(e => e.Id)
                                     .ToList();

                ViewBag.Menu = menu != null && menu.Any()
                               ? menu
                               : new List<Menu>();

                return View("FooterLayout");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
