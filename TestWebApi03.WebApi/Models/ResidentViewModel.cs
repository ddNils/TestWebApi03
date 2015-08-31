using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi03.WebApi.Models
{
    public class ResidentViewModel : TestWebApi03.Contracts.IResident
    {
        public int Id { get; set; }

        public string JobDescription
        {
            get;

            set;
        }

        public string LastName
        {
            get;

            set;
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public int RoomId { get; set; }
    }
}
