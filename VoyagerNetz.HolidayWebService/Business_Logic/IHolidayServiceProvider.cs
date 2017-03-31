using Public.Holiday.WebAPI.Models;
using System;
using System.Collections.Generic;

namespace Public.Holiday.WebAPI.Business_Logic
{
    public interface IHolidayServiceProvider
    {
        Uri ConntectionUri { get;}
       
        string ServiceProviderName { get;}

        IEnumerable<PublicHoliday> GetPublicHolidays(int year);

        IEnumerable<PublicHoliday> GetPublicHolidaysByCountry(int year, string country);
    }
}
