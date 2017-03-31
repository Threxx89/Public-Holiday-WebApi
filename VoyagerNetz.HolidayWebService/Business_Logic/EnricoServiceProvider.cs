using Newtonsoft.Json.Linq;
using Public.Holiday.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Public.Holiday.WebAPI.Business_Logic
{
    public class EnricoServiceProvider : IHolidayServiceProvider
    {
        public Uri ConntectionUri
        {
            get { return new Uri("http://www.kayaposoft.com/enrico/json/v1.0/index.php"); }
        }

        public string ServiceProviderName
        {
            get { return "Enrico"; }
        }

        public IEnumerable<PublicHoliday> GetPublicHolidays(int year)
        {
            try
            {
                string request = string.Format("?action=getPublicHolidaysForYear&year={0}&country=zaf", year);
                List<PublicHoliday> publicHoliday = new List<PublicHoliday>();
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = ConntectionUri;
                    HttpResponseMessage response = httpclient.GetAsync(request).Result;
                    string result = response.Content.ReadAsStringAsync().Result;

                    JArray jsonArray = JArray.Parse(result);
                    foreach (JObject jsonObject in jsonArray)
                    {
                        PublicHoliday day = new PublicHoliday();
                        day.Day = (string)jsonObject["date"]["day"];
                        day.Month = (string)jsonObject["date"]["month"];
                        day.Year = (string)jsonObject["date"]["year"];
                        day.Name = (string)jsonObject["englishName"];
                        publicHoliday.Add(day);
                    }

                }
                return publicHoliday;
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public IEnumerable<PublicHoliday> GetPublicHolidaysByCountry(int year, string country)
        {
            try
            {
                string request = string.Format("?action=getPublicHolidaysForYear&year={0}&country={1}", year,country);
                List<PublicHoliday> publicHoliday = new List<PublicHoliday>();
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = ConntectionUri;
                    HttpResponseMessage response = httpclient.GetAsync(request).Result;
                    string result = response.Content.ReadAsStringAsync().Result;

                    JArray jsonArray = JArray.Parse(result);
                    foreach (JObject jsonObject in jsonArray)
                    {
                        PublicHoliday day = new PublicHoliday();
                        day.Day = (string)jsonObject["date"]["day"];
                        day.Month = (string)jsonObject["date"]["month"];
                        day.Year = (string)jsonObject["date"]["year"];
                        day.Name = (string)jsonObject["englishName"];
                        publicHoliday.Add(day);
                    }

                }
                return publicHoliday;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public override string ToString()
        {
            return ServiceProviderName;
        }
    }
}
