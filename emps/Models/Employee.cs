using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emps.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? PositionId { get; set; }
        public Position Position { get; set; }
    }
}