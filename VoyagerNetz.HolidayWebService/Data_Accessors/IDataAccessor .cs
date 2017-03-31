using Public.Holiday.WebAPI.Models;
using System.Collections.Generic;

namespace Public.Holiday.WebAPI.Data_Accessors
{
    public interface IDataAccessor 
    {
         string Name { get; }  

         string ConnectionString { get;}

         void Writer(IEnumerable<PublicHoliday> publicHoliday);

         bool IsLoaded();
         bool IsYearLoaded(int year);

         IEnumerable<PublicHoliday> Reader();

         IEnumerable<PublicHoliday> Reader(int year);
    }
}
