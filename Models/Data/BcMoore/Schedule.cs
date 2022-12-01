using System;

namespace Models.Data.BcMoore;

public class Schedule
{

    //Date	Visitor 	Home (West Des Moines Dowling) [long name]	Location (1 is at home, 0 is neutral)

    public DateTime Date { get; set; }
    public string Visitor { get; set; } //uses team's long name
    public string Home { get; set; } //uses team's long name
    public byte Location { get; set; } //1 is home, 0 is neutral


}
