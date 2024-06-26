using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalLayer.Models
{
    [Table("CurrencyPair")]

    public class CurrencyPair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BaseCurrencyId { get; set; }
        public Currency BaseCurrency { get; set; } //navigation to baseCurrancyId obj
        public int QuoteCurrencyId { get; set; }
        public Currency QuoteCurrency { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }

    }
}
