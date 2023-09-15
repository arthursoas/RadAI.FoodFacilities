namespace RadAI.FoodFacilities.WebAPI.Settings
{
    public class DomainSettings : IDomainSettings
    {
        public string DatabaseName { get; set; }

        public string DataSFMobileFoodFacilitiesPermitsUrl { get; set; }

        public TimeSpan DataSFCacheExpiration { get; set; }
    }
}
