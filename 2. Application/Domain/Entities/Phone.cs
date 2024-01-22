using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Domain entity.
    /// </summary>
    public class Phone
    {
        public int PhoneID { get; set; }
        public string PhoneName { get; set; }
        public string PhoneDescription { get; set; }
        public string PhoneFabricatorName { get; set; }
        public string PhoneStatus { get; set; }
        public DateTime FabricationDate { get; set; }
        public DateTime LastStatusUpdate { get; set; }

    }
}
