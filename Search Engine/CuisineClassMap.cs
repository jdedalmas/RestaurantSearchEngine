using CsvHelper.Configuration;

namespace Search_Engine
{
    public class CuisineClassMap : ClassMap<Cuisine>
    {
        private CuisineClassMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.Name).Name("name");
        }
    }
}