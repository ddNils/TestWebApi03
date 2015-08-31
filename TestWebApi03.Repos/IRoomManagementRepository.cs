using System.Linq;
using TestWebApi03.Contracts;

namespace TestWebApi03.Repos
{
    public interface IRoomManagementRepository
    {
        IRoom AddInhabitant(int roomId, IResident dweller);
        IRoom AddRoom(IRoom r);
        bool DeleteRoom(int roomId);
        IRoom EditRoom(IRoom r);
        IResident EditResident(IResident r);
        IQueryable<IResident> GetAllResidents();
        IRoom RemoveInhabitant(int roomId, IResident dweller);

        IQueryable<IRoom> GetAllRooms();
        IResident AddResident(IResident r);
    }
}