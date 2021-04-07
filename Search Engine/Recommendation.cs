#nullable enable
namespace Search_Engine
{
    public class Recommendation
    {
        public Recommendation(int? price, string? name, int? customerRating, int? distance, string? cuisine)
        {
            price ??= 10;
            Price = (int) price;
            name ??= "";
            Name = name;
            customerRating ??= 1;
            CustomerRating = (int) customerRating;
            distance ??= 10;
            Distance = (int) distance;
            cuisine ??= "";
            Cuisine = cuisine;
        }

        public Recommendation()
        {
            Price = 10;
            Name = "";
            CustomerRating = 1;
            Distance = 10;
            Cuisine = "";
        }

        public string Name { get; set; }
        public int CustomerRating { get; set; }
        public int Distance { get; set; }
        public int Price { get; set; }
        public string Cuisine { get; set; }
    }
}