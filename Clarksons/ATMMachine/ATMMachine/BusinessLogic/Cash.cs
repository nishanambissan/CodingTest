using System;
using System.Collections.Generic;

namespace ATMMachine.BusinessLogic
{
    public class Cash
    {
        public Cash()
        {
            CoinOrNotes = new List<Denomination>();
        }

        public List<Denomination> CoinOrNotes { get; set; } 
    }
}
