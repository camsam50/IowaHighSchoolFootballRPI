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











    //public class Team
    //{
    //    //EXAMPLE
    //    //Long name         Short name      Class   District
    //    //Ackley AGWSR      Ackley AGWSR	8	    2
    //    //Adel ADM          Adel ADM	    3A	    8

    //    public string LongName { get; set; }
    //    public string ShortName { get; set; }
    //    public string Class { get; set; }
    //    public byte District { get; set; }
    //    public string Conference { get { return $"{Class}-{District}"; } }
    //    public bool IsOutOfState { get { return Class == "ZZ"; } }


    //    public IEnumerable<object> Games { get; set; }
    //    public byte Wins { get; set; } //TODO: get from games
    //    public byte Losses { get; set; }//TODO: get from games
    //    public decimal WinningPercentage
    //    {
    //        get
    //        {
    //            if ((Wins + Losses) == 0) { return 0m; }
    //            else { return ((decimal)Wins / ((decimal)Wins + (decimal)Losses)); }
    //        }
    //    }

    //    public IEnumerable<Team> Opponents { get; set; }
    //    public byte OpponentWins() //TODO: what to do with out of state?
    //    {
    //        return (byte)Opponents.Sum(o => o.Wins);
    //    }
    //    public byte OpponentLosses() //TODO: what to do with out of state?
    //    {
    //        return (byte)Opponents.Sum(o => o.Losses);

    //    }
    //    public decimal OpponentWinningPercentage()
    //    {
    //        if (this.IsOutOfState) { return .5m; }
    //        if ((OpponentWins() + OpponentLosses()) == 0) { return 0m; }
    //        else { return ((decimal)OpponentWins() / ((decimal)OpponentWins() + (decimal)OpponentLosses())); }
    //    }


    //    public byte OpponentsOpponentWins()
    //    {
    //        return (byte)Opponents.Sum(o => o.Opponents.Sum(oo => oo.Wins));
    //    }
    //    public byte OpponentsOpponentLosses()
    //    {
    //        return (byte)Opponents.Sum(o => o.Opponents.Sum(oo => oo.Losses));
    //    }
    //    public decimal OpponentsOpponentWinningPercentage()
    //    {
    //        if (this.IsOutOfState) { return .5m; }
    //        if ((OpponentsOpponentWins() + OpponentsOpponentLosses()) == 0) { return 0m; }
    //        else { return ((decimal)OpponentsOpponentWins() / ((decimal)OpponentsOpponentWins() + (decimal)OpponentsOpponentLosses())); }
    //    }

    //    public decimal RPI()
    //    {
    //        //(.375 x WP) + (.375 x OWP) + (.250 x OOWP)
    //        decimal rpi = (.375m * WinningPercentage) + (.375m * OpponentWinningPercentage()) + (.250m * OpponentsOpponentWinningPercentage());
    //        return decimal.Round(rpi, 4);
    //    }

    //}










    //public class Team2
    //{
    //    public string LongName { get; set; }
    //    public string ShortName { get; set; }
    //    public string Class { get; set; }
    //    public int District { get; set; }
    //    public string Conference { get; set; }
    //    public decimal CurrentRanking { get; set; }
    //    public DateTime LastUpdatedDate { get; set; }
    //    public int Wins { get; set; }
    //    public int Losses { get; set; }
    //    public bool IsOutOfState { get; set; }
    //    public decimal WinningPercentage
    //    {
    //        get
    //        {
    //            if ((Wins + Losses) == 0) { return 0m; }
    //            else { return ((decimal)Wins / ((decimal)Wins + (decimal)Losses)); }
    //        }
    //    }
    //    //public decimal WinningPercentage
    //    //{
    //    //    get
    //    //    {
    //    //        return (decimal)((decimal)3 / ((decimal)3 + (decimal)6));
    //    //    }
    //    //}


    //    public List<Team2> Opponents { get; set; } = new List<Team2>();

    //    public int OpponentWins()
    //    {
    //        return Opponents.Sum(o => o.Wins);
    //    }
    //    public int OpponentLosses()
    //    {
    //        return Opponents.Sum(o => o.Losses);

    //    }
    //    public decimal OpponentWinningPercentage()
    //    {
    //        if (this.IsOutOfState) { return .5m; }
    //        if ((OpponentWins() + OpponentLosses()) == 0) { return 0m; }
    //        else { return ((decimal)OpponentWins() / ((decimal)OpponentWins() + (decimal)OpponentLosses())); }
    //    }


    //    public int OpponentsOpponentWins()
    //    {
    //        return Opponents.Sum(o => o.Opponents.Sum(oo => oo.Wins));
    //    }
    //    public int OpponentsOpponentLosses()
    //    {
    //        return Opponents.Sum(o => o.Opponents.Sum(oo => oo.Losses));
    //    }
    //    public decimal OpponentsOpponentWinningPercentage()
    //    {
    //        if (this.IsOutOfState) { return .5m; }
    //        if ((OpponentsOpponentWins() + OpponentsOpponentLosses()) == 0) { return 0m; }
    //        else { return ((decimal)OpponentsOpponentWins() / ((decimal)OpponentsOpponentWins() + (decimal)OpponentsOpponentLosses())); }
    //    }

    //    public decimal RPI()
    //    {
    //        //(.375 x WP) + (.375 x OWP) + (.250 x OOWP)
    //        return decimal.Round((.375m * WinningPercentage) + (.375m * OpponentWinningPercentage()) + (.250m * OpponentsOpponentWinningPercentage()), 4);
    //    }


    //}








}