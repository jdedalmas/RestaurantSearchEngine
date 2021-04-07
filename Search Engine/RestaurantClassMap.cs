using CsvHelper.Configuration;

namespace Search_Engine
{
    public class RestaurantClassMap : ClassMap<Restaurant>
    {
        private RestaurantClassMap()
        {
            Map(m => m.Name).Name("name");
            Map(m => m.CustomerRating).Name("customer_rating");
            Map(m => m.Distance).Name("distance");
            Map(m => m.Price).Name("price");
            Map(m => m.CuisineId).Name("cuisine_id");
        }
    }
}