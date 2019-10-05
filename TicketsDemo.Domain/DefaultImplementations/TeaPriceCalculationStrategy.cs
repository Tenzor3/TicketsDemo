﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy
{
    public class TeaPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private decimal _price;

        public TeaPriceCalculationStrategy(decimal price)
        {
            _price = price;
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var components = new List<PriceComponent>();

            var teaComponent = new PriceComponent()
            {
                Name = "Price for tea",
                Value = _price
            };
            components.Add(teaComponent);

            return components;
        }
    
    }
}
