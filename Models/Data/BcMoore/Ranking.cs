namespace Models.Data.BcMoore;

public record Ranking
{
    public string LongName { get; set; }
    public string ShortName { get; set; }
    public decimal Rank { get; set; }
    public decimal ScheduleAverage { get; set; }
    public decimal OffensiveAverage { get; set; }
    public decimal DefensiveAverage { get; set; }

}
