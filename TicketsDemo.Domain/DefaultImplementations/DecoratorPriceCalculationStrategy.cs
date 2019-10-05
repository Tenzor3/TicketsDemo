using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy
{
    public class DecoratorCalculationStrategy : IPriceCalculationStrategy
    {
        private IPriceCalculationStrategy _defaultStrategy;
        private IPriceCalculationStrategy _teaStrategy;
        private IPriceCalculationStrategy _coffeeStrategy;

        public bool IncludeTea { get; set; } = true;
        public bool IncludeCoffee { get; set; } = true;
        
        public DecoratorCalculationStrategy(IRunRepository runRepository, ITrainRepository trainRepository)
        {
            _defaultStrategy = new DefaultPriceCalculationStrategy(runRepository, trainRepository);
            _teaStrategy = new TeaPriceCalculationStrategy(Prices.Tea);
            _coffeeStrategy = new CoffeePriceCalculationStrategy(Prices.Coffee);
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var components = _defaultStrategy.CalculatePrice(placeInRun);

            if (IncludeTea)
            {
                components.AddRange(_teaStrategy.CalculatePrice(placeInRun));
            }

            if (IncludeCoffee)
            {
                components.AddRange(_coffeeStrategy.CalculatePrice(placeInRun));
            }

            return components;
        }
    
    }
}