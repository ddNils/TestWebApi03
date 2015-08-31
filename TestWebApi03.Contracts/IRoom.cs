using System.Collections.Generic;

namespace TestWebApi03.Contracts
{
    public interface IRoom
    {
        int Floor { get; set; }
        int Id { get; set; }
        string Location { get; set; }
        string RoomName { get; set; }
        float SizeM2 { get; set; }
    }
}