using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityAccounting
{
    internal class BillComparator
    {
        public int Compare(ElectricityBill? x, ElectricityBill? y)
        {
            if (x.AppNo == y.AppNo && x.Surname == y.Surname)
            {
                return 0;
            }
            return 1;
        }
    }
}
