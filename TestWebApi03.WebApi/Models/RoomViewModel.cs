using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi03.Contracts;

namespace TestWebApi03.WebApi.Models
{
    public class RoomViewModel : TestWebApi03.Contracts.IRoom
    {
        public int Floor
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public ICollection<IResident> Inhabitants
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public string RoomName
        {
            get;
            set;
        }

        public float SizeM2
        {
            get;
            set;
        }
    }
}
