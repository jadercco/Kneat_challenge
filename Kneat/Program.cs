
using System;
using System.Collections.Generic;
using System.IO;
using Kneat.API;
using Kneat.Model;

namespace Kneat
{
    class Program
    {
        static void Main(string[] args)
        {
            int distance = GetDistance();
            var gatewayApi = new GatewayApi();
            try
            {
                var starships = gatewayApi.GetAllStarships();

                foreach (StarshipModel item in starships)
                {
                    StarshipModel stops = MainCalculator.CalculateStops(distance, item);
                    if(int.TryParse(stops.Stops, out int stop)) 
                    {
                        if (Convert.ToInt32(stops.Stops) >= 0)
                        {
                            Console.WriteLine($"{item.Name}: {stops.Stops}");
                        }
                    }
                    
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred. {e}");

            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
        }

        static int GetDistance()
        {

            string title = "Episode X";
            var text = "The Kneat Software Test";
            var text2 = "Test! The company Kneat Software is looking for the best analysts to help her to discover the autonomy of all the ships of the galaxy, in search of choosing the best option to be used in the fight against Darth Sidious(Sheev Palpatine).";
            var text3 = "Evil is everywhere.";
            var text4 = "The output is a collection of all starships and the total number of stops needed to make the distance between the planets. [name: stops]";
            var line = "_______________________________________";

            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (title.Length / 2)) + "}", title));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (text2.Length / 2)) + "}", text2));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (text3.Length / 2)) + "}", text3));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (line.Length / 2)) + "}", line));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (text4.Length / 2)) + "}", text4));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (line.Length / 2)) + "}", line));



            Console.Write("Please type the MGLT distance: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int mglt))
            {
                return mglt;
            }
            else
            {
                Console.WriteLine("Please enter a valid entry. Ex: 100000");
                return GetDistance();
            }
        }

    }
}
