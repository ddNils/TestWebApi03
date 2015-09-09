using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi03.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using TestWebApi03.Contracts;

// Use of Automapper might be advised :)

namespace TestWebApi03.Repos
{
    // Alternatively save this to Web.config
    public class RoomDBConfiguration : DbConfiguration
    {
        public RoomDBConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new System.Data.Entity.SqlServer.SqlAzureExecutionStrategy());
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));
        }
    }

    public class RoomManagementContext: DbContext
    {
        public RoomManagementContext()
            :base("RoomsContext")
        {

        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Resident> Residents { get; set; }
        
    }


    // Nicht Repository - eher Unit of Work oder Service oder Domain Layer - DbSet ist das repository
    // Also gehört die Business Logik auch hier her!
    public class RoomManagementRepository : IDisposable, IRoomManagementRepository
    {
        private RoomManagementContext _db = new RoomManagementContext();

        public IQueryable<IResident> GetAllResidents()
        {
            //List<Resident> res = new List<Resident>();

            //var res1 = new Resident()
            //{
            //    Id = -1,
            //    JobDescription = "BPM Entwickler",
            //    LastName = "Sennewald",
            //    Name = "Christian",
            //    Title = "Herr"
            //};

            //res.Add(res1);
            //return res.AsQueryable();

            return _db.Residents.SqlQuery("SELECT * FROM Residents WHERE Room_Id IS NULL").AsQueryable<IResident>();

            //return _db.Residents.AsQueryable<IResident>();
        }

        public IQueryable<IRoom> GetAllRooms()
        {
            var rooms = _db.Rooms;
            //var iRooms = new List<IRoom>();

            //foreach(Room r in rooms)
            //{
            //    iRooms.Add(new Room()
            //    {
            //        Floor = r.Floor,
            //        Id = r.Id,
            //        Location = r.Location,
            //        RoomName = r.RoomName,
            //        SizeM2 = r.SizeM2
            //    });
            //}

            return rooms;
        }

        public IRoom AddRoom(IRoom r)
        {
            var room = new Room()
            {
                Id = r.Id,
                Floor = r.Floor,
                Location = r.Location,
                RoomName = r.RoomName,
                SizeM2 = r.SizeM2
            };
            
            var ret = _db.Rooms.Add(room);
            _db.SaveChanges();
            return ret;
        }

        public IRoom EditRoom(IRoom r)
        {
            var ret = _db.Rooms.Where(ro => ro.Id == r.Id).FirstOrDefault();
            if (ret == null)
                return null;

            var room = new Room()
            {
                Id = r.Id,
                Floor = r.Floor,
                Location = r.Location,
                RoomName = r.RoomName,
                SizeM2 = r.SizeM2
            };
            
            _db.Rooms.AddOrUpdate(room);
            _db.SaveChanges();
            return room;
        }

        public IResident EditResident(IResident r)
        {
            var res = _db.Residents.Where(re => re.Id == r.Id).FirstOrDefault();

            if (res == null)
                return null;

            var resident = new Resident()
            {
                Id = res.Id,
                JobDescription = r.JobDescription,
                LastName = r.LastName,
                Name = r.Name,
                Title = r.Title
            };

            _db.Residents.AddOrUpdate(resident);
            _db.SaveChanges();
            return resident;
        }

        public bool DeleteRoom(int roomId)
        {
            var room = _db.Rooms.Where(r => r.Id == roomId).FirstOrDefault();
            if (room == null)
                return false;

            if (room.Inhabitants.Count > 0)
            {
                throw new Exception("Could not delete Room, it still has inhabitants");
            }

            // Was passiert mit den Bewohnern? - Logik hier implementieren!
            _db.Rooms.Remove(room);
            _db.SaveChanges();
            return true;
        }

        public IResident AddResident(IResident dweller)
        {
            if (dweller == null)
                return null;

            var res = new Resident()
            {
                JobDescription = dweller.JobDescription,
                LastName = dweller.LastName,
                Name = dweller.Name,
                Title = dweller.Title
            };

            _db.Residents.Add(res);
            _db.SaveChanges();
            return res;
        }

        public IRoom AddInhabitant(int roomId, IResident dweller)
        {
            var room = _db.Rooms.Where(r => r.Id == roomId).FirstOrDefault();
            if (room == null)
                return null;

            Resident res = _db.Residents.Where(r => r.Id == dweller.Id).FirstOrDefault();

            if (res != null)
            {
                //if (!room.Inhabitants.Contains(res))
                    room.Inhabitants.Add(res);
            } else
            {
                //var res2 = AddResident(dweller);
                res = new Resident()
                {
                    Id = dweller.Id,
                    JobDescription = dweller.JobDescription,
                    LastName = dweller.LastName,
                    Name = dweller.Name,
                    Title = dweller.Title
                };
                room.Inhabitants.Add(res);
            }
            _db.SaveChanges();

            return room;
        }

        public IRoom RemoveInhabitant(int roomId, IResident dweller)
        {
            var room = _db.Rooms.Where(r => r.Id == roomId).FirstOrDefault();
            if (room == null)
                return null;

            var res = _db.Residents.Where(r => r.Id == dweller.Id).FirstOrDefault();

            if (res != null)
            {
                //if (!room.Inhabitants.Contains(res))
                room.Inhabitants.Remove(res);
            }
            _db.SaveChanges();
            return room;
        }

        public bool DeleteInhabitant(int inhabitantId)
        {
            var res = _db.Residents.Where(r => r.Id == inhabitantId).FirstOrDefault();

            if (res == null)
            {
                return false;
            }

            _db.Residents.Remove(res);
            _db.SaveChanges();

            return true;
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //public System.Data.Entity.DbSet<TestWebApi03.WebApi.Models.ResidentViewModel> ResidentViewModels { get; set; }
    }
}
