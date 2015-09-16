using SharpSapRfc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Mvc.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [RfcStructureField("ACTIVE")]
        public bool IsActive { get; set; }

        public int Age { get; set; }
    }
}