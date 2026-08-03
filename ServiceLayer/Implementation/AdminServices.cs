using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainLayer.CommonObjects;
using EsnadTakaful.ServiceLayer.Implementation;
using EsnadTakaful.ServiceLayer.Interface;
using RentalCar.DomainLayer.LookUpObjects;
using RentalCar.ServiceLayer.Interface;

namespace EsnadTakaful.ServiceLayer.Implementation
{
    public class AdminServices : IAdmin
    {

        private readonly ILookUpLogic _ContentAdminLogic;

            #region Shared Variables

            private static ConcurrentDictionary<AdminTables, string> _SharedTables = new ConcurrentDictionary<AdminTables, string>();

        #endregion

        public AdminServices(ILookUpLogic ILookUpLogic)
         {
            this._ContentAdminLogic = ILookUpLogic;
                if (_SharedTables.Count == 0)
                {
                   /*_SharedTables.GetOrAdd(0] = AdminTables.Menu;*/ // Dummy value
                   _SharedTables.GetOrAdd(AdminTables.Menu,"Menu"); //2
                   _SharedTables.GetOrAdd(AdminTables.ProductsMenu, "ProductsMenu"); //3
                   _SharedTables.GetOrAdd(AdminTables.Vision, "Vision"); //4
                   _SharedTables.GetOrAdd(AdminTables.Mission, "Mission"); //5
                   _SharedTables.GetOrAdd(AdminTables.AboutUs, "AboutUs"); //6
            }
        }



         #region My Variables

        #endregion



        public bool UpdatePosition(string positionCode, long tableId, bool isAdd, long? recordId,string pos)
        {
            try
            {
                _ContentAdminLogic.UpdatePosition(positionCode, tableId, isAdd, recordId, pos);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ;
            }
        }




        #region Contant

        public async Task<long> AddContent<T>(T obj, AdminTables table, int LangId)
            {
                try
                {
                    return await _ContentAdminLogic.AddContent<T>(obj, table, LangId);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        
        public async Task<long> AddContentLang<T>(T obj, AdminTables table,long parentId, int LangId)
            {
                try
                {
                    return await _ContentAdminLogic.AddContentLang<T>(obj, table, parentId, LangId);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }


            public void DeleteContent(long contenId)
            {
                try
                {
                    _ContentAdminLogic.DeleteContent(contenId);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }



        public List<string> GetAttributes(AdminTables table)
        {
            try
            {
                List<LookUpAttributes> result = new List<LookUpAttributes>();
                List<string> attrName = new List<string>();
                result = _ContentAdminLogic.GetAttributes(table, "en");
                foreach (LookUpAttributes item in (List<LookUpAttributes>)result)
                {
                    attrName.Add(item.Name);
                }
                return attrName;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<GeneralContents<T>> GetContent<T>(AdminTables table, string LangCode,int limit)
        {
            try
            {
                GeneralContents<T> result = new GeneralContents<T>();

                result =await _ContentAdminLogic.GetContent<T>(table, LangCode, 0, limit, null, true, null);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        List<LookUpAttributes> attributes = new List<LookUpAttributes>();
        public object ConfigurationManager { get; private set; }

        public async Task<GeneralContents<T>> GetContentOfItem<T>(AdminTables table,string LangCode, int limit, long mainId)
        {
            try
            {
                GeneralContents<T> result = new GeneralContents<T>();

                result =await _ContentAdminLogic.GetContent<T>(table, LangCode, 0, limit, null, true, mainId);

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion




    }
}


