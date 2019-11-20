using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestWebAPI.Models;

namespace TestWebAPI.Controllers
{
    public class OrderController : ApiController
    {

        private List<Order> orderList = new List<Order>();
        
        

        [HttpGet]
        public string Get(string vb)
        {
            orderList.Add(new Order {orderid=1, productid=1, customer="customer1"  });
            return "Hello World " + vb;
        }
        public string Get(Order ordr)
        {
            return "Hello World " + ordr.customer;
        }
        [HttpPost]
        public string Test(Order ordr)
        {
            orderList.Add(ordr);
            return "success";
        }

    }
}
