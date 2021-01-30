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
        public string VisitorTeam { get; set; } //uses team's long name
        public byte VisitorScore { get; set; }
        public string HomeTeam { get; set; } //uses team's long name
        public byte HomeScore { get; set; }
        public byte Overtimes { get; set; }
    }
}
