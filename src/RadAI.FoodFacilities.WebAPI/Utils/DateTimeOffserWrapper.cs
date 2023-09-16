namespace RadAI.FoodFacilities.WebAPI.Utils
{
    public class DateTimeOffserWrapper : IDateTimeOffset
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
