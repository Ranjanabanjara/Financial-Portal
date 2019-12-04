using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_4.Enumerations
{
    public class TransactionType
    {
        public enum TransType
        {
           deposit,
           payment
        }

    }
    public class AccountType
    {
        public enum AccType
        {
            Checking,
            Investment,
            Saving,
            Credit
           
        }

    }

}

