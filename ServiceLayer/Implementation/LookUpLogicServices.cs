using DomainLayer.CommonObjects;
using DomainLayer.LookUpModels;
using RentalCar.DomainLayer.LookUpObjects;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.RespositoryPattern;
using ServiceLayer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.CommonObjects;
using FastMember;
using ServiceLayer.Interface;

namespace RentalCar.ServiceLayer.Implementation
{
    public class LookUpLogicservices : ILookUpLogic
    {
        private readonly IRepository<LookUps> _repository;
        private RentalCarDbContext _dbContext;
        private readonly ILookUps _LookUpServices;
        private readonly ILookUpMultiLang _LookUpMultiServices;
        private readonly ILookUpMedia _MediaServices ;
        private readonly ILanguage _LanguageServices;


        public IConfiguration Configuration { get; }

        #region Servicess



        #endregion



        #region Static Shared Variables
        //private static Dictionary<" LookUpLogicservices." _SharedTables = new Dictionary<" LookUpLogicservices."();

        private static ConcurrentDictionary<string, long> __SharedTableIds = new ConcurrentDictionary<string, long>();
        private static ConcurrentDictionary<string, System.Type> __SharedClassesForeachTable = new ConcurrentDictionary<string, System.Type>();
        public object FastMember { get; private set; }
        #endregion

        #region Constructors
        
        public LookUpLogicservices(ILookUps ILookUps, ILookUpMultiLang ILookUpMultiLang, ILookUpMedia ILookUpMedia, ILanguage ILanguage)
        {
            Configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json")
                      .Build();
            this._LookUpServices = ILookUps;
            this._LookUpMultiServices = ILookUpMultiLang;
            this._MediaServices = ILookUpMedia;
            this._LanguageServices = ILanguage;

            if (__SharedTableIds.Count() == 0)
            {
                /*__SharedTableIds[0"] = 0);*/ // Dummy value
                __SharedTableIds.GetOrAdd("Menu", 1);
                __SharedTableIds.GetOrAdd("ProductsMenu", 2);
                __SharedTableIds.GetOrAdd("Vision", 3);
                __SharedTableIds.GetOrAdd("Mission", 4);
                __SharedTableIds.GetOrAdd("AboutUs", 5);
            }


            if (__SharedClassesForeachTable.Count() == 0)
            {
                //__SharedClassesForeachTable[0"] = null;
                __SharedClassesForeachTable["Menu"] = typeof(Menu);
                __SharedClassesForeachTable["ProductsMenu"] = typeof(ProductsMenu);
                __SharedClassesForeachTable["Vision"] = typeof(Vision);
                __SharedClassesForeachTable["Mission"] = typeof(Mission);
                __SharedClassesForeachTable["AboutUs"] = typeof(AboutUs);
            }
        }

        #endregion



      
  


        #region Content


        public void DeleteContent(long parentId)
        {
            try
            {
                //delete all lookups that hold this id as parent or original one.
                _LookUpServices.DeleteContent(true, parentId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<long> AddContent<T>(T obj, AdminTables  table,int LangId)
        {
            string XMLPath = Configuration["XML_Path"];
            string multiLangValue;
            string imageValue;

            Media img = new Media();

            List<string> imageValues = new List<string>();


            List<LookUpAttributes> attributes = Tools.GetAttributes(table.ToString(), XMLPath);


            var t = typeof(T);


            LookUps lk = new LookUps();
            LookUpMultiLang ml = new LookUpMultiLang();
            long parentId = 0;
            bool isFirst = true;
         
            foreach (LookUpAttributes att in attributes)
            {


                lk = new LookUps();

                lk.TableId = (int)__SharedTableIds[table.ToString()];
                lk.SysDate = DateTime.Now;
                lk.UserId = 1;
                lk.IsDeleted = false;
                lk.Code = att.Code;
                lk.isPublished = true;


                if (isFirst)
                {
                    lk.ParentId = null;

                }
                else
                {
                    lk.ParentId = parentId;
                }

                lk = _LookUpServices.Add(lk);

                if (isFirst)
                {
                    isFirst = false;
                    parentId = lk.Id;
                }


                if (att.isMedia)
                {
                    //check if the property is list
                    if (att.isList)
                    {
                        imageValues = (List<string>)(t.GetProperty(att.Name).GetValue(obj));

                        foreach (string s in imageValues)
                        {
                            // add media
                            img = new Media();

                            img.IsActive = true;
                            img.Is_deleted = false;
                            img.IsVideo = att.isVideo;
                            img.LookUpId = lk.Id; ;
                            img.SysDate = DateTime.Now;
                            img.Name = s;


                            _MediaServices.Add(img);
                        }
                    }
                    else
                    {
                        imageValue = (string)(t.GetProperty(att.Name).GetValue(obj));

                        img = new Media();

                        img.IsActive = true;
                        img.Is_deleted = false;
                        img.IsVideo = att.isVideo;
                        img.LookUpId = lk.Id;
                        img.SysDate = DateTime.Now;
                        img.Name = imageValue;


                        _MediaServices.Add(img);
                    }
                }
                else
                {
                    // add lookupmulti

                    multiLangValue = (t.GetProperty(att.Name).GetValue(obj)).ToString();

                    ml = new LookUpMultiLang();

                    ml.LookUpId = lk.Id;
                    if (att.isLangNull)
                    {
                        ml.LanguageId = null;
                    }
                    else
                    {
                        ml.LanguageId = LangId;
                    }
                    ml.LanguageId = LangId;
                    ml.SysDate = DateTime.Now;
                    ml.Is_deleted = false;
                    //ml.UserId = 1;
                    ml.Description = multiLangValue;
                    // ml.isPublished = true;

                    await _LookUpMultiServices.AddAsync(ml);
                }

            }
            return parentId;
        }

        
        public async Task<long> AddContentLang<T>(T obj, AdminTables table,long parentId, int LangId)
        {
            string XMLPath = Configuration["XML_Path"];
            string multiLangValue;
            string imageValue;

            Media img = new Media();

            List<string> imageValues = new List<string>();


            List<LookUpAttributes> attributes = Tools.GetAttributes(table.ToString(), XMLPath);


            var t = typeof(T);


            LookUps lk = new LookUps();
            LookUpMultiLang ml = new LookUpMultiLang();

            foreach (LookUpAttributes att in attributes)
            {
                ml = new LookUpMultiLang();


                // add lookupmulti
                if (att.isMain)
                {
                    ml.LookUpId = parentId;
                }
                else
                {
                    lk = _LookUpServices.Get(parentId, att.Code, true);
                    ml.LookUpId = lk.Id;
                }
                  
                multiLangValue = (t.GetProperty(att.Name).GetValue(obj)).ToString();

             
              
                ml.LanguageId = LangId;
                ml.SysDate = DateTime.Now;
                ml.Is_deleted = false;
                //ml.UserId = 1;
                ml.Description = multiLangValue;
                // ml.isPublished = true;

              await  _LookUpMultiServices.AddAsync(ml);


            }
            return parentId;
        }



        public List<LookUps> GetRowsOfTableByCode(long tableId, string code)
        {
            List<LookUps> lookups = new List<LookUps>();

            try
            {
                lookups = _LookUpServices.GetList(tableId, code);
            }
            catch (Exception ex)
            {

                throw;
            }

            return lookups;
        }

        public async Task<(List<T>,int)> ReturnListOf<T>( AdminTables table, long langId, int offset, int limit, List<LookUpAttributes> attributes, LookUpAttributes main, List<string> fields, bool isAll,  int total, long? lookUpId)
        {
            try
            {

                LookUpMultiLang lookupMulti = new LookUpMultiLang();
                LookUps lookUp = new LookUps();
                long tableId;
                List<LookUps> allRowsThatHaveCodeOfMainChild = new List<LookUps>();
                List<LookUps> selectedRowsThatHaveCodeOfMainChild = new List<LookUps>();
                List<LookUps> contents = new List<LookUps>();

                System.Type UsedClass = __SharedClassesForeachTable[table.ToString()];
                var UsedObject = Activator.CreateInstance(UsedClass);

                List<string> images = new List<string>();

                dynamic dynamic = (dynamic)UsedObject;
                tableId = __SharedTableIds[table.ToString()];
                List<dynamic> allDynamic = new List<dynamic>();


                var Services = TypeAccessor.Create(typeof(T));

                if (lookUpId == null)
                {
                    allRowsThatHaveCodeOfMainChild = GetRowsOfTableByCode(tableId, main.Code);

                     total = allRowsThatHaveCodeOfMainChild.Count();
                    selectedRowsThatHaveCodeOfMainChild = allRowsThatHaveCodeOfMainChild.OrderByDescending(s => s.Id).Skip(offset).Take(limit).ToList();
                }
                else
                {
                    selectedRowsThatHaveCodeOfMainChild.Add(_LookUpServices.Get((long)lookUpId));
                    total = 1;
                }

                foreach (LookUps row in selectedRowsThatHaveCodeOfMainChild)
                {
                    contents = new List<LookUps>();
                    contents = _LookUpServices.GetChildren(row.Id);
                    dynamic = (dynamic)Activator.CreateInstance(UsedClass);
                    Services[dynamic, "Id"] = row.Id;

                    // main
                    if (main.isLangNull)
                        lookupMulti =await _LookUpMultiServices.GetAsync(row.Id, null);
                    else
                        lookupMulti = await _LookUpMultiServices.GetAsync(row.Id, langId);
                    if (lookupMulti != null)
                        Services[dynamic, main.Name] = lookupMulti.Description;

                    //TO DO set id 

                    foreach (LookUpAttributes obj in attributes)
                    {

                        if (obj.Code == main.Code)
                            continue;
                        if (!isAll)
                        {
                            if (fields.Contains(obj.Name) == false)
                                continue;
                        }
                        lookUp = new LookUps();
                        lookUp = contents.Where(e => e.Code == obj.Code).FirstOrDefault();
                        if (lookUp == null)
                            continue;
                        if (obj.Name == "Date")
                        {
                            Services[dynamic, obj.Name] = (row.SysDate).Value.Date; //new
                            continue;
                        }
                        if (obj.isMedia)
                        {
                            try
                            {
                                if (obj.isList)
                                {

                                    //get list
                                    images = new List<string>();
                                    images = _MediaServices.Get(lookUp.Id, obj.isVideo)
                                        .Select(e => e.Name).ToList();
                                    if (images.Count != 0)
                                        Services[dynamic, obj.Name] = images;
                                }
                                else
                                {
                                    Media media = new Media();
                                    media = _MediaServices.Get(lookUp.Id, obj.isVideo).FirstOrDefault();
                                    if (media == null)
                                        continue;
                                    // get one image
                                    Services[dynamic, obj.Name] = media.Name;
                                }
                            }
                            catch (Exception ex)
                            {

                                throw new Exception("exception in media id=" + lookUp.Id);
                            }

                        }
                        else
                        {
                            try
                            {
                                if (obj.isLangNull)
                                    lookupMulti = await _LookUpMultiServices.GetAsync(lookUp.Id, null);
                                else
                                    lookupMulti = await _LookUpMultiServices.GetAsync(lookUp.Id, langId);

                                Services[dynamic, obj.Name] = lookupMulti.Description;
                            }
                            catch (Exception ex)
                            {

                                throw new Exception("exception in LookUps multi id=" + lookUp.Id);
                            }
                        }


                    }
                    allDynamic.Add(dynamic);

                }

                List<T> list = new List<T>();
                foreach (dynamic d in allDynamic)
                {
                    list.Add((T)d);
                }

                return (list,total);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<LookUpAttributes> GetAttributes(AdminTables table,string langCode)
        {
            LookUpAttributes main = new LookUpAttributes();
            Language lang = new Language();
            List<LookUpAttributes> attributes = new List<LookUpAttributes>();

            try
            {
                string XMLPath = Configuration["XML_Path"];

                lang = _LanguageServices.Get(langCode);

                //Get class's attributes

                attributes = Tools.GetAttributes(table.ToString(), XMLPath);

                return attributes;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<GeneralContents<T>> GetContent<T>(AdminTables table, string langCode, int offset, int limit, List<string> fields, bool isAll, long? LookUpId)
        {
            GeneralContents<T> contents = new GeneralContents<T>();

            (List<T>, int) contentss = new();
            LookUpAttributes main = new LookUpAttributes();
           Language lang = new Language();
            List<LookUpAttributes> attributes = new List<LookUpAttributes>();

            try
            {
                string XMLPath = Configuration["XML_Path"];
                lang = _LanguageServices.Get(langCode);

                //Get class's attributes
                
                attributes = Tools.GetAttributes(table.ToString(), XMLPath);
                //Get main attribute
                main = attributes
                       .Where(e => e.isMain == true)
                       .FirstOrDefault();

                int total = 0;
                if (main == null)
                {
                    main = new LookUpAttributes();
                    main.Code = "This is any text";
                }

                /* contents = GetMono<T>(table, lang.Id, main.Code);*/
                if (attributes.Count != 0)
                {
                    contentss = (await ReturnListOf<T>(table, lang.Id, offset, limit, attributes, main, fields, isAll,  total, LookUpId));
                    contents.Contents = contentss.Item1;
                }
                contents.TotalContent = contentss.Item2;
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return contents;
        }
        #endregion



        public async Task<bool> UpdatePosition(string positionCode, long tableId, bool isAdd, long? recordId,string pos)
        {
            try
            {

                //get position
                LookUps position = _LookUpServices.Get(positionCode, (long)recordId, false);
                LookUpMultiLang multi = new LookUpMultiLang();
                LookUpMultiLang posMulti = new LookUpMultiLang();
                posMulti =await _LookUpMultiServices.GetAsync(position.Id, 1);
                _LookUpMultiServices.EditDesription(posMulti.Id, pos);
                //List<LookUps> positions = _LookUpServices.GetList(tableId).Where(e => e.Code == positionCode && e.Id != position.Id && e.Is_deleted == false).ToList();

                //foreach (LookUps p in positions)
                //{
                //    foreach (PosLO item in toedit)
                //    {

                //        //get multi
                //        multi = new LookUpMultiLang();
                //        multi = _LookUpMultiServices.Get(p.Id, 1);

                //        if (multi.Id == item.Id)
                //        {
                //            //update
                            
                //        }
                //    }
                //}

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

      
    }
}
