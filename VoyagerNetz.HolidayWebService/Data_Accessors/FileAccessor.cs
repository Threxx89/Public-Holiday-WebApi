using Newtonsoft.Json.Linq;
using Public.Holiday.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace Public.Holiday.WebAPI.Data_Accessors
{
    public class FileAccessor : IDataAccessor
    {
        #region Properties
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["ContentStorageFilePath"];
            }
        }
        public string Name
        {
            get { return "Json TextFile"; }
        }

        #endregion

        #region Public Methods
        public void Writer(IEnumerable<PublicHoliday> publicHoliday)
        {
            try
            {
                if (!String.IsNullOrEmpty(ConnectionString))
                {
                    if (!File.Exists(ConnectionString))
                    {
                        File.Create(ConnectionString);
                        _Writer(publicHoliday);
                    }
                    else
                    {
                        _Writer(publicHoliday);
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public IEnumerable<PublicHoliday> Reader()
        {
            try
            {
                string jsonLine;
                List<PublicHoliday> publicHolidays = new List<PublicHoliday>();
                if (!String.IsNullOrEmpty(ConnectionString))
                {
                    if (File.Exists(ConnectionString))
                    {
                        StreamReader reader = new StreamReader(ConnectionString);
                        if (!String.IsNullOrEmpty(reader.ReadLine()))
                        {
                            while ((jsonLine = reader.ReadLine()) != null)
                            {
                                JObject jsonObject = JObject.Parse(jsonLine);
                                PublicHoliday day = new PublicHoliday();
                                day.Day = (string)jsonObject["Day"];
                                day.Month = (string)jsonObject["Month"];
                                day.Year = (string)jsonObject["Year"];
                                day.Name = (string)jsonObject["Name"];
                                publicHolidays.Add(day);
                            }
                            reader.Close();
                        }
                        reader.Close();
                    }

                }
                return publicHolidays;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public IEnumerable<PublicHoliday> Reader(int year)
        {
            try
            {
                string jsonLine;
                List<PublicHoliday> publicHolidays = new List<PublicHoliday>();
                if (!String.IsNullOrEmpty(ConnectionString))
                {
                    if (File.Exists(ConnectionString))
                    {
                        StreamReader reader = new StreamReader(ConnectionString);
                        if (!String.IsNullOrEmpty(reader.ReadLine()))
                        {

                            while ((jsonLine = reader.ReadLine()) != null)
                            {
                                JObject jsonObject = JObject.Parse(jsonLine);
                                if ((string)jsonObject["Year"] == year.ToString())
                                {
                                    PublicHoliday day = new PublicHoliday();
                                    day.Day = (string)jsonObject["Day"];
                                    day.Month = (string)jsonObject["Month"];
                                    day.Year = (string)jsonObject["Year"];
                                    day.Name = (string)jsonObject["Name"];
                                    publicHolidays.Add(day);
                                }
                            }
                            reader.Close();
                        }
                        reader.Close();
                    }
                }
                return publicHolidays;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public bool IsYearLoaded(int year)
        {
                if (Reader(year).Count() == 0)
                {
                    return false;
                }
                return true;
        }

        public bool IsLoaded()
        {

                if (Reader().Count() == 0)
                {
                    return false;
                }
                return true;
        }
        #endregion

        #region Private Methods
        private void _Writer(IEnumerable<PublicHoliday> publicHoliday)
        {
            StreamWriter writer = null;
            try
            {
                List<PublicHoliday> localPublicHoliday = Reader().ToList();

                foreach (PublicHoliday day in localPublicHoliday)
                {
                    if (day.Year == publicHoliday.First().Year)
                    {
                        localPublicHoliday.Remove(day);
                    }
                }
                localPublicHoliday.AddRange(publicHoliday);
                writer = new StreamWriter(ConnectionString, false);
                foreach (PublicHoliday holiday in localPublicHoliday)
                {
                    string json = new JavaScriptSerializer().Serialize(holiday);

                    writer.WriteLine(json);
                    writer.Flush();
                }
            }
            catch (Exception)
            {
                writer.Close();
                throw;
            }finally
            {
                writer.Close();
            }

               
        }
        #endregion
    }
}
