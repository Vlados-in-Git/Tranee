using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Tranee
{
    public class LocalTrainningItem
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public int UserId { get; set; } // to know who is trainging

       // public int


    }

}
