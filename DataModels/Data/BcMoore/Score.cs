using System;

namespace Models.Data.BcMoore
{
    public class Score
    {
        //EXAMPLE
        //Date      Visitor                     Visitor Score   Home                        Home Score      Overtimes
        //8/21/2020	Greenfield Nodaway Valley	15	            Corning Southwest Valley	22	                0
        //8/21/2020	Columbus Junction	        26	            Eldon Cardinal	            63	                0


        public DateTime Date { get; set; }
        public string Visitor { get; set; } //uses team's long name
        public byte VisitorScore { get; set; }
        public string Home { get; set; } //uses team's long name
        public byte HomeScore { get; set; }
        public byte Overtimes { get; set; }
    }
}


//schedule
//Date	Visitor 	Home (West Des Moines Dowling) [long name]	Location (1 is at home, 0 is neutral)
