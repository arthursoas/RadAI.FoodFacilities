namespace RadAI.FoodFacilities.WebAPI.Settings
{
    public interface IDomainSettings
    {
        /// <summary>
        /// In memory database name
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// URL to access Mobile Food Facilities Permits from Data San Francisco
        /// </summary>
        public string DataSFMobileFoodFacilitiesPermitsUrl { get; set; }

        /// <summary>
        /// Expiration of cache for data collected from DataSF
        /// </summary>
        public TimeSpan DataSFCacheExpiration { get; set; }
    }
}
