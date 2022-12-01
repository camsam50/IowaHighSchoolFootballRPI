//using System;

//namespace Models.Business
//{
//    public class Game
//    {
//        //schedule 
//        //Date	    Visitor 	Home [long name]	Location (1 is at home, 0 is neutral)

//        //score
//        //Date      Visitor                     Visitor Score   Home                        Home Score      Overtimes
//        //8/21/2020	Greenfield Nodaway Valley	15	            Corning Southwest Valley	22	                0
//        //8/21/2020	Columbus Junction	        26	            Eldon Cardinal	            63	                0

//        public DateTime Date { get; set; }
//        public Team Visitor { get; set; }
//        public Team Home { get; set; }
//        public bool NeutralSite { get; set;}
        
//        public byte? VisitorScore { get; set; }
//        public byte? HomeScore { get; set; }
//        public byte? Overtimes { get; set; }


//        public bool HasGameBeenPlayed { get { return VisitorScore is not null && HomeScore is not null && Overtimes is not null; } }


//    }

//}