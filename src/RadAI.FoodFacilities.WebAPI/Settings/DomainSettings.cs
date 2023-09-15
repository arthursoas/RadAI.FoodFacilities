namespace RadAI.FoodFacilities.WebAPI.Settings
{
    public class DomainSettings : IDomainSettings
    {
        /// <summary>
        /// In memory database name
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// URL to access Mobile Food Facilities Permits from Data San Francisco
        /// </summary>
        public string DataSFMobileFoodFacilitiesPermitsUrl { get; set; }
    }
}
