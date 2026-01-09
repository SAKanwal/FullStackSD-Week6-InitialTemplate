using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6_RESTFULAPI.Models
{
     public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Program { get; set; }
        public int Year { get; set; }
        public double Gpa { get; set; }
    }
}
