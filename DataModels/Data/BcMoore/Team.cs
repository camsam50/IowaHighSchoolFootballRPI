namespace Models.Data.BcMoore
{
    public record Team
    {

        //EXAMPLE
        //Long name         Short name      Class   District
        //Ackley AGWSR      Ackley AGWSR	8	    2
        //Adel ADM          Adel ADM	    3A	    8


        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Class { get; set; }
        public byte District { get; set; }

        
    }

    //public record TeamRecord
    //{
    //    //EXAMPLE
    //    //Long name         Short name      Class   District
    //    //Ackley AGWSR      Ackley AGWSR	8	    2
    //    //Adel ADM          Adel ADM	    3A	    8

    //    public string LongName { get; init; }
    //    public string ShortName { get; init; }
    //    public string Class { get; init; }
    //    public byte District { get; init; }

    //}

    
}
