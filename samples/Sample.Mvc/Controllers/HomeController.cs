using Sample.Mvc.Models;
using SharpSapRfc;
using SharpSapRfc.Profiling;
using SharpSapRfc.Soap;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private SapRfcConnection sap;
        public HomeController()
        {
            this.sap = new ProfiledSapRfcConnection(new SoapSapRfcConnection("SAP"), MiniProfiler.Current);
        }

        protected override void Dispose(bool disposing)
        {
            this.sap.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            var result = this.sap.ExecuteFunction("Z_SSRT_GET_ALL_CUSTOMERS");
            var customers = result.GetTable<Customer>("t_customers");
            return View(customers);
        }

        public ActionResult ShowCustomer(int id)
        {
            var result = this.sap.ExecuteFunction("Z_SSRT_GET_CUSTOMER", new { i_id = id });
            var customer = result.GetOutput<Customer>("e_customer");
            
            return View(customer);
        }

        public ActionResult Debug() 
        {
            this.sap.ExecuteFunction("Z_SSRT_QUERY_CUSTOMERS",
                new RfcParameter("c_customers", new Customer[] { 
                    new Customer() { Id = 1 },
                    new Customer() { Id = 2 }
                })
            );

            this.sap.Table<AirlineCompany>("SCARR")
                    .Select("CARRID", "CURRCODE")
                    .Where("CARRID = 'DL'")
                    .Read();

            this.sap.Table<AirlineCompany>("SCARR")
                    .Select("CARRID", "CURRCODE")
                    .Where("CARRID = 'DL'")
                    .Read();

            return View();
        }

    }
}
