namespace PresentationLayer.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        
        public List<City> RegisterCities()
        {
            List<City> cities = new List<City>();
            cities.Add(new City { CityId = 1, CityName = "Yardımlı" });
            cities.Add(new City { CityId = 5, CityName = "Bakı" });
            cities.Add(new City { CityId = 2, CityName = "Gence" });
            cities.Add(new City { CityId = 3, CityName = "Sumqayit" });
            return cities;
        }
    }
}
