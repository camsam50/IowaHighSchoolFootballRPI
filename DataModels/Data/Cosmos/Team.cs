using Newtonsoft.Json;

namespace Models.Data.Cosmos
{
    public class Team
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return LongName; } }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Class { get; set; }
        public byte District { get; set; }
        public decimal Rank { get; set; }
        public decimal ScheduleAverage { get; set; }
        public decimal OffensiveAverage { get; set; }
        public decimal DefensiveAverage { get; set; }




    }
}
