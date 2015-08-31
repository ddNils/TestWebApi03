using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi03.Contracts;

namespace TestWebApi03.Entities
{
    public class Resident : IResident
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobDescription { get; set; }
        public string Title { get; set; }
        public string LastName { get; set; }
    }
}
