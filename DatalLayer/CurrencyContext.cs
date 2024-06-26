using DatalLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalLayer
{
    public class CurrencyContext : DbContext
    {

                
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyPair> CurrencyPairs { get; set; }
        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
        {
        }

        /// <summary>
        ///  builds CurrencyContext instance with the given sql connection
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static CurrencyContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CurrencyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new CurrencyContext(optionsBuilder.Options);
        }


       
    }
}
