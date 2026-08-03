using DomainLayer.CommonObjects;
using DomainLayer.LookUpModels;
using EsnadTakaful.ServiceLayer.Interface;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.ServiceLayer.Interface;

namespace RentalCar.Views.Components.HeaderLayout
{
    public class HeaderLayoutViewComponent : ViewComponent
    {
        private readonly ILanguage _ILanguage;
        private readonly IAdmin _IAdminServices;
        private readonly IAppLabel _IAppLabelServices;
        private readonly IAnnouncement _IAnnouncement;

        public HeaderLayoutViewComponent(ILanguage ILanguage ,
          IAdmin IAdminServices ,
          IAppLabel IAppLabelServices,
          IAnnouncement IAnnouncementServices


        )
        {
            _ILanguage = ILanguage;
            _IAdminServices = IAdminServices;
            _IAppLabelServices = IAppLabelServices;
            _IAnnouncement = IAnnouncementServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
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
                var title = await _IAppLabelServices.GetValAsync("websiteTitle",langData.Id);
                ViewBag.WebsiteTitle = title;

                var donateLabel = await _IAppLabelServices.GetValAsync("Donate_Label", langData.Id);
                ViewBag.DonateLabel = donateLabel;

                // MAIN MENU
                var menuResult = await _IAdminServices.GetContent<Menu>(AdminTables.Menu, langCode, 99);

                var menu = menuResult?.Contents?
                                     .OrderBy(e => e.PositionNumber)
                                     .ToList();

                ViewBag.Menu = menu != null && menu.Any()
                               ? menu
                               : new List<Menu>();


                // PRODUCTS SUBMENU
                var prodResult = await _IAdminServices.GetContent<ProductsMenu>(AdminTables.ProductsMenu, langCode, 99);

                var productsMenus = prodResult?.Contents?
                                                 .OrderBy(e => e.Id)
                                                 .ToList();

                ViewBag.ProductsMenu = productsMenus != null && productsMenus.Any()
                                       ? productsMenus
                                       : new List<ProductsMenu>();

                //Announcements
                var announcement = await _IAnnouncement.GetAllByLanguageIdAsync(langData.Id);
                ViewBag.Announcements = announcement.Data;

                return View("HeaderLayout");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }


    }
}
