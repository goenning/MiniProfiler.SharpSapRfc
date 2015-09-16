﻿using SharpSapRfc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Mvc.Models
{
    public class AirlineCompany
    {
        [RfcStructureField("MANDT")]
        public int Client { get; set; }
        [RfcStructureField("CARRID")]
        public string Code { get; set; }
        [RfcStructureField("CARRNAME")]
        public string Name { get; set; }
        [RfcStructureField("CURRCODE")]
        public string Currency { get; set; }
        [RfcStructureField("URL")]
        public string Url { get; set; }
    }
}