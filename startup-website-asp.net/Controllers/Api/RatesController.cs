using startup_website_asp.net.ApiViewModels;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace startup_website_asp.net.Controllers.Api
{
    
    public class RatesController : ApiController
    {
        private StartupWebsite db = new StartupWebsite();
        //[HttpGet]
        //// GET: api/Rates
        //public IHttpActionResult GetRates()
        //{
        //    List<RateItemApiViewModel> result = new List<RateItemApiViewModel>();
        //    List<OrderDetail> orderDetails = db.OrderDetails.
            
        //    return Ok(result);
        //}
    }
}
