using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebAPI.Models
{
    public class Order
    {
        public int orderid { get; set; }
        public int productid { get; set; }
        public string customer { get; set; }
    }
    public class UpdateStudentMob
    {
        public string stud_id { get; set; }
        public string stud_mobile { get; set; }
    }
}