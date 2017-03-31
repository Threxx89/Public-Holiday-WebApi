using Public.Holiday.WebAPI.Business_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Public.Holiday.WebAPI.Controllers
{
    public class PublicHolidayController : ApiController
    {
        PublicHolidayManager m_PublicHolidayManager;
        // GET api/publicholiday
        public IHttpActionResult Get()
        {
            try
            {
                Dictionary<string, string> requestParameters = this.Request.GetQueryNameValuePairs()
                 .ToDictionary(key => key.Key, value => value.Value);

                if (requestParameters.Count > 0 && requestParameters.ContainsKey("ServiceProvider"))
                {
                    m_PublicHolidayManager = new PublicHolidayManager(requestParameters["ServiceProvider"]);
                    string format = requestParameters["Formatter"] == null ? string.Empty : requestParameters["Formatter"];
                    _Formatter(format);
                    return Ok(m_PublicHolidayManager.GetAllHolidayForAllYears());
                }
                else
                {
                    return this.BadRequest("Invalid Parameter. Require ServiceProvide=value.");
                }
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        public IHttpActionResult Get(int year)
        {
            try
            {
                Dictionary<string, string> requestParameters = this.Request.GetQueryNameValuePairs()
                .ToDictionary(key => key.Key, value => value.Value);

                if (requestParameters.Count > 0 && requestParameters.ContainsKey("ServiceProvider"))
                {
                    m_PublicHolidayManager = new PublicHolidayManager(requestParameters["ServiceProvider"]);
                    string format = requestParameters["Formatter"] == null ? string.Empty : requestParameters["Formatter"];
                    _Formatter(format);
                    return Ok(m_PublicHolidayManager.GetAllHolidaysLocaly(year));
                }
                else
                {
                    return this.BadRequest("Invalid Parameter. Require ServiceProvide=value.");
                }
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }


        }

        // POST api/publicholiday
        public void Post([FromBody]string value)
        {

        }

        // PUT api/publicholiday/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/publicholiday/5
        public void Delete(int id)
        {
        }

        private void _Formatter(string format)
        {
            if (format.Equals("json"))
            {
                Request.Headers.Add("accept", "application/json");
            }
        }
    }
}
