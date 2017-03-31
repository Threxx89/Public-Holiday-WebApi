using Public.Holiday.WebAPI.Data_Accessors;
using Public.Holiday.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Public.Holiday.WebAPI.Business_Logic
{
    public class PublicHolidayManager
   {
       #region Private Members
       private IHolidayServiceProvider m_ServiceProvider;
       private static IDataAccessor m_DataAccessor;


       private static List<IHolidayServiceProvider> m_CollectionServiceProviders = new List<IHolidayServiceProvider>();
       #endregion

       #region Propertiest
       public IHolidayServiceProvider ServiceProvider
       {
            get { return m_ServiceProvider; }
            set { m_ServiceProvider = value; }
       }

       public static IDataAccessor DataAccessor
       {
           get { return m_DataAccessor; }
           set { m_DataAccessor = value; }
       }
       #endregion

       #region Public Methods

       public PublicHolidayManager(string serviceProvider)
       {
          m_ServiceProvider = m_CollectionServiceProviders.FirstOrDefault(provider => provider.ServiceProviderName.Equals(serviceProvider, StringComparison.OrdinalIgnoreCase));
       }

       public IEnumerable<PublicHoliday> GetAllHolidaysFromService(int year ,bool Write = true)
       {
           try
           {
               PublicHoliday[] holidays = ServiceProvider.GetPublicHolidays(year).ToArray();
               if (Write)
               {
                   DataAccessor.Writer(holidays);
               }
               return holidays;
           }
           catch (Exception)
           {
               
               throw;
           }

       }

       public IEnumerable<PublicHoliday> GetAllHolidayForAllYears()
       {
           try
           {
               if (!m_DataAccessor.IsLoaded())
               {
                   return DataAccessor.Reader().ToArray<PublicHoliday>();
               }
               return null;
           }
           catch (Exception)
           {
               
               throw;
           }

       }

       public IEnumerable<PublicHoliday> GetAllHolidaysLocaly(int year)
       {
           try
           {
               if (!m_DataAccessor.IsYearLoaded(year))
               {
                   GetAllHolidaysFromService(year);
               }
               return DataAccessor.Reader(year).ToArray<PublicHoliday>();
           }
           catch (Exception)
           {
               
               throw;
           }

       }
       #endregion

       #region Static Methods
       public static void RegisterServiceProvider(IHolidayServiceProvider serviceProvider)
       {
           m_CollectionServiceProviders.Add(serviceProvider);
       }
       #endregion
   }
}
