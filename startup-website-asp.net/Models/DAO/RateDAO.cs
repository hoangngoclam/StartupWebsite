using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models.DAO
{
    public class RateDAO:BaseDAO
    {
        public RateDAO()
        {

        }
        public double GetAverageRate(long productId)
        {
            List<Rate> rates = GetRateByProductId(productId);
            double countRate = rates.Count();
            if (countRate == 0)
            {
                return 0;
            }
            float sumRate = 0;
            foreach (Rate item in rates)
            {
                sumRate += float.Parse(item.RateNumber.ToString());
            }
            return Math.Round(sumRate / countRate, 1);
        }
        public List<Rate> GetRateByProductId(long productId)
        {
            return db.Rates.Where(x => x.ProductId == productId).ToList();
        }
    }
}