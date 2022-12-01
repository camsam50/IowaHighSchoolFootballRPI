using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Models.Data.BcMoore;


//Date      Visitor     Visitor Score   Home                            Home Score  Overtime    Location Site    Playoff    EntryType   Notes   Archivist1  Source1
//8/18/2022	Underwood	20	            Council Bluffs Lewis Central    35	        0	        1		        0			                    Bud Legg    iahsaa.org
//8/19/2022	Riceville	34	            Colo-NESCO	                    42	        1	        1		        0			Bud Legg    iahsaa.org
//8/19/2022	Des Moines North	0	Dallas Center-Grimes	41	0	1		0			Bud Legg    iahsaa.org
//8/19/2022	Davenport Assumption    26	Independence	7	0	1		0			Bud Legg    iahsaa.org



public record Composite
{
    public DateTime Date { get; set; }
    public string Visitor { get; set; }
    public int VisitorScore { get; set; }
    public string Home { get; set; }
    public int HomeScore { get; set; }
    public int Overtime { get; set; }
    public int Location { get; set; }
    public string Site { get; set; }
    public int Playoff { get; set; }
    public string EntryType { get; set; }
    public string Notes { get; set; }
    public string Archivist1 { get; set; }
    public string Source1 { get; set; }

}
