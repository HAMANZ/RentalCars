using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.DomainLayer.LookUpObjects
{
    public class GeneralContents<T>
    {

        public List<T> Contents { get; set; }
        public int TotalContent { get; set; }

    }
}