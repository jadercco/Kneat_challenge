using Kneat.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kneat.Test
{
    [TestClass]
    public class KneatCalculateStops
    {
        [TestMethod]
        public void Calculate_Stops_Number()
        {
            StarshipModel test = new StarshipModel()
            {
                Name = "Millennium Falcon",
                Consumables = "2 months",
                Mglt = "75"
            };

            Assert.IsTrue(Convert.ToInt32(MainCalculator.CalculateStops(1000000, test).Stops) == 9, "OK");

            test = new StarshipModel()
            {
                Name = "Y-wing",
                Consumables = "1 week",
                Mglt = "80"
            };

            Assert.IsTrue(Convert.ToInt32(MainCalculator.CalculateStops(1000000, test).Stops) == 74, "OK");

            test = new StarshipModel()
            {
                Name = "Rebel Transport",
                Consumables = "6 months",
                Mglt = "20"
            };

            Assert.IsTrue(Convert.ToInt32(MainCalculator.CalculateStops(1000000, test).Stops) == 11, "OK");

        }
    }
}
