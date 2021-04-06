using System.Collections.Generic;

namespace Models.Business
{
    public class Team
    {

        public string LongName { get; set; } //this is the value that links all the data together
        public string ShortName { get; set; }  //this is the better name to show to user and how to link to bcmoore rankings
        public string Class { get; set; }
        public byte District { get; set; }
        public string Conference { get { return $"{Class}-{District}"; } }
        //public bool IsOutOfState { get { return Class == "ZZ"; } } ////OR HANDLE VIA A DIFFERENT CLASS???

        public byte BcMooreRanking { get; set; }


        public IEnumerable<Game> Schedule { get; set;} = new List<Game>();


    }

}