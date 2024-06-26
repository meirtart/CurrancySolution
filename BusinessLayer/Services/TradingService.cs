using DatalLayer.Models;
using DatalLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TradingService
    {
        private readonly CurrencyRepository _repository;
        private readonly Random _random;
        private Timer _timer;

        
        public TradingService()
        {
            _repository = new CurrencyRepository();
            _random = new Random();
            StartTrading();
        }

        /// <summary>
        /// initiates a time the runs every 2 seconds nand executes EmulateTrade
        /// </summary>
        private void StartTrading()
        {
            _timer = new Timer(EmulateTrade, null, 0, 2000);
        }

        /// <summary>
        /// Iterates over currencypair list,
        /// generates a random rate and update the pair acordingly (max and min values)
        /// </summary>
        /// <param name="state"></param>
        private void EmulateTrade(object state)
        {
            var currencyPairs = _repository.GetCurrencyPairs(); //loads currencyPair list

            foreach (var pair in currencyPairs)
            {
                var newValue = (decimal)(_random.NextDouble() * (double)(pair.MaxValue - pair.MinValue) + (double)pair.MinValue);

                if (newValue < pair.MinValue)
                {
                    pair.MinValue = newValue;
                    _repository.UpdateCurrencyPair(pair);
                }
                else if (newValue > pair.MaxValue)
                {
                    pair.MaxValue = newValue;
                    _repository.UpdateCurrencyPair(pair);
                }
            }
        }

        public IEnumerable<CurrencyPair> GetCurrencyPairs()
        {
            return _repository.GetCurrencyPairs();
        }


    }
}
