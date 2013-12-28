using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary.Model
{
    public class Person
    {
        public bool adult { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public double popularity { get; set; }
        public string profile_path { get; set; }
    }
}
