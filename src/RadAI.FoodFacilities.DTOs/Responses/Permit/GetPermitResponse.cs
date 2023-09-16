namespace RadAI.FoodFacilities.DTOs.Responses.Permit
{
    public class GetPermitResponse
    {
        public string Id { get; set; }

        public string Applicant { get; set; }

        public string FacilityType { get; set; }

        public string CNN { get; set; }

        public string LocationDescription { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public static GetPermitResponse FromPermit(Entities.Permit permit)
        {
            return new GetPermitResponse
            {
                Id = permit.Id,
                Applicant = permit.Applicant,
                FacilityType = permit.FacilityType,
                CNN = permit.CNN,
                LocationDescription = permit.LocationDescription,
                Address = permit.Address,
                Status = permit.Status,
                Latitude = permit.Latitude,
                Longitude = permit.Longitude
            };
        }
    }
}
