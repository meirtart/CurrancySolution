using DatalLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DatalLayer.Repository
{
    public class CurrencyRepository
    {
        private readonly string sqlConn = "Data Source=LAPTOP-TJOHBI1M;Initial Catalog=CurrencyExchange;Integrated Security=True;Trust Server Certificate=True";

        /// <summary>
        /// get CurrencyPair list, populating the navigation fields
        /// </summary>
        /// <returns>CurrencyPair IEnumerable</returns>
        public IEnumerable<CurrencyPair> GetCurrencyPairs()
        {

            using (var currencyContext = CurrencyContext.CreateDbContext(sqlConn))
            {
                return currencyContext.CurrencyPairs.Include(cp => cp.BaseCurrency).Include(cp => cp.QuoteCurrency).ToList();
            }
            
        }

        /// <summary>
        /// updates given currencyPair
        /// </summary>
        /// <param name="currencyPair"></param>
        public void UpdateCurrencyPair(CurrencyPair currencyPair)
        {
            

            using (var currencyContext = CurrencyContext.CreateDbContext(sqlConn))
            {
                currencyContext.CurrencyPairs.Update(currencyPair);
                currencyContext.SaveChanges();
            }

          
        }
        
        
   
    }
}
