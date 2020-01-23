using Kneat.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kneat
{
    public static class MainCalculator
    {

        private static readonly Dictionary<string, double> Consumables;
        static MainCalculator()
        {
            Consumables = new Dictionary<string, double>()
            {
                { "hour", 1},
                { "hours", 1 },
                { "day", 24},
                { "days", 24},
                {"week", 168},
                {"weeks", 168},
                {"month", 730.001},
                {"months", 730.001},
                {"year", 8760},
                {"years", 8760}
            };
        }

        /// <summary>
        /// Method that calculates the number of necessary stops using as reference the value of consumables, distance and mglt
        /// </summary>
        /// <param name="distance">Long integer representing the distance in MGLT</param>
        /// <param name="starship">Model containing the ship information to be calculated</param>
        /// <returns>Object containing the name, number of necessary stops and other auxiliary properties</returns>
        public static StarshipModel CalculateStops(int distance, StarshipModel starship)
        {

            if (starship == null || distance == 0)
            {
                starship.Stops = "unknown";
                return starship;
            }

            try
            {
                int consumable = ConverterComsumableToInt(starship.Consumables); 

                if (int.TryParse( starship.Mglt, out int mglt))
                {
                    if (mglt == 0 || consumable == 0)
                        starship.Stops = "unknown";

                    starship.Stops = ((long)(distance / mglt / consumable)).ToString();
                }
                else 
                {
                    starship.Stops = "unknown";

                }

                return starship;
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// Method that converts the value of consumables to hourly value (int) through a dictionary of the manufacturer.
        /// </summary>
        /// <param name="consumables">String with the value of consumables</param>
        /// <returns>Value of hourly consumables (int)</returns>
        public static int ConverterComsumableToInt(string consumables)
        {
            if (string.IsNullOrEmpty(consumables)) return 0;
            
            string[] arrayConsumable = consumables.Split(" ");

            if (arrayConsumable.Length == 2)
            {
                if (!int.TryParse(arrayConsumable[0], out int number))
                {
                    return 0;
                }

                double hours = Consumables[arrayConsumable[1]];

                return (hours > 0) ? (int)(number * hours) : 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
