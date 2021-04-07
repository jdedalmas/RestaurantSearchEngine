using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Search_Engine
{
    internal abstract class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Booting up... \n");
            Console.WriteLine("Loading files... \n");
            // Get data from CSVs
            var restaurantsList =
                LoadRestaurants(
                    "/Users/jeandedalmas/Documents/GitHub/take-home-assessment-alphasight/csv/restaurants.csv");
            var cuisinesList =
                LoadCuisines("/Users/jeandedalmas/Documents/GitHub/take-home-assessment-alphasight/csv/cuisines.csv");

            // Initialize search data
            var recommendationsList = MergeCuisinesAndRestaurants(cuisinesList, restaurantsList);


            // Initialize query
            //var query = new Recommendation(price: 10, name: "Wi", customerRating: null, distance: 2, cuisine: null);
            var query = new Recommendation();

            Console.WriteLine(
                "Welcome to the ultimate restaurant search application!");
            Console.WriteLine("Please compose a query below for your list of recommendations: \n");
            Console.WriteLine("Type in a Cuisine (Chinese, American, Thai, etc.):");
            query.Cuisine = Console.ReadLine();
            Console.WriteLine("Type in a Restaurant Name:");
            query.Name = Console.ReadLine();
            Console.WriteLine("Type in a Distance (1 mile ~ 10 miles):");
            var d = Console.ReadLine();
            if (!string.IsNullOrEmpty(d)) query.Distance = Convert.ToInt32(d);
            Console.WriteLine("Type in a Price ($10 ~ $50):");
            var p = Console.ReadLine();
            if (!string.IsNullOrEmpty(p)) query.Price = Convert.ToInt32(p);
            Console.WriteLine("Type in a Customer Rating (1 star ~ 5 stars):");
            var r = Console.ReadLine();
            if (!string.IsNullOrEmpty(r)) query.CustomerRating = Convert.ToInt32(r);

            Console.WriteLine("\nSearching... \n");

            // Get top five matches
            var topFiveMatches = GetTopFiveRecommendations(query, recommendationsList);

            // Print to console
            if (!topFiveMatches.Any())
                Console.WriteLine("Your search returned no results.");
            else
                Console.WriteLine($"Please see your {topFiveMatches.Count()} recommendation(s) below: \n");
            foreach (var match in topFiveMatches)
                Console.WriteLine(
                    $"Restaurant name: {match.Name}, Cuisine: {match.Cuisine}, Distance: {match.Distance}, Customer rating: {match.CustomerRating}, Price: {match.Price}");

            Console.WriteLine("\nGoodbye!");
        }

        private static IEnumerable<Recommendation> GetTopFiveRecommendations(Recommendation query,
            List<Recommendation> recommendationsList)
        {
            var matchingRecommendations = from recommendation in recommendationsList
                where recommendation.Cuisine.Contains(query.Cuisine) && recommendation.Name.Contains(query.Name) &&
                      query.Distance >= recommendation.Distance &&
                      query.CustomerRating <= recommendation.CustomerRating && query.Price >= recommendation.Price
                select recommendation;

            return matchingRecommendations.ToList().OrderBy(r => r.Distance).ThenByDescending(r => r.CustomerRating)
                .ThenBy(r => r.Price).Take(5);
        }

        private static List<Recommendation> MergeCuisinesAndRestaurants(List<Cuisine> cuisines,
            List<Restaurant> restaurants)
        {
            return (from item in restaurants
                    let cuisineName = cuisines.First(c => c.Id == item.CuisineId).Name
                    select new Recommendation(item.Price, item.Name, item.CustomerRating, item.Distance, cuisineName))
                .ToList();
        }

        private static List<Restaurant> LoadRestaurants(string filePath)
        {
            using var streamReader = File.OpenText(filePath);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<RestaurantClassMap>();
            return csvReader.GetRecords<Restaurant>().ToList();
        }

        private static List<Cuisine> LoadCuisines(string filePath)
        {
            using var streamReader = File.OpenText(filePath);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<CuisineClassMap>();
            return csvReader.GetRecords<Cuisine>().ToList();
        }
    }
}