using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi03.Contracts;

namespace TestWebApi03.Entities
{
    public class Room : IRoom
    {
        public Room()
        {
            Inhabitants = new List<Resident>();
        }

        public int Id { get; set; }
        public string RoomName { get; set; }
        public float SizeM2 { get; set; }
        public int Floor { get; set; }
        public string Location { get; set; }
        public virtual ICollection<Resident> Inhabitants { get; set; }

    }
}
