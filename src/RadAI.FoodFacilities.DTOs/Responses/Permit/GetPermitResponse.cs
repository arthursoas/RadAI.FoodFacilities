using System.Text.Json.Serialization;

namespace RadAI.FoodFacilities.DTOs.Responses.Permit
{
    public class GetPermitResponse
    {
        public string Id { get; set; }

        public string? Applicant { get; set; }

        public string? FacilityType { get; set; }

        public string? CNN { get; set; }

        public string? LocationDescription { get; set; }

        public string? Address { get; set; }

        public string? BlockLot { get; set; }

        public string? Block { get; set; }

        public string? Lot { get; set; }

        public string? CurrentPermit { get; set; }

        public string? Status { get; set; }

        public string? X { get; set; }

        public string? Y { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? Schedule { get; set; }

        public string? Received { get; set; }

        public string? PriorPermit { get; set; }

        public string? ExpirationDate { get; set; }

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
                BlockLot = permit.BlockLot,
                Block = permit.Block,
                Lot = permit.Lot,
                CurrentPermit = permit.CurrentPermit,
                Status = permit.Status,
                X = permit.X,
                Y = permit.Y,
                Latitude = permit.Latitude,
                Longitude = permit.Longitude,
                Schedule = permit.Schedule,
                Received = permit.Received,
                PriorPermit = permit.PriorPermit,
                ExpirationDate = permit.ExpirationDate
            };
        }
    }
}
