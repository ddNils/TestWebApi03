using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace TestWebApi03.WebApi.Controllers
{
    public class RoomsController : ApiController
    {
        private Repos.IRoomManagementRepository _RoomRepo;
        public RoomsController(Repos.IRoomManagementRepository repo)
        {
            // Gets in via dependency injection
            _RoomRepo = repo;
        }

        // GET: api/Rooms
        public IEnumerable<Contracts.IRoom> Get()
        {
            return _RoomRepo.GetAllRooms();
        }

        //GET: api/Rooms/5
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            var room = _RoomRepo.GetAllRooms().Where(r => r.Id == id).FirstOrDefault();
            if (room != null)
                return request.CreateResponse(System.Net.HttpStatusCode.OK, room);
            else
                return request.CreateResponse(System.Net.HttpStatusCode.NoContent, room);

        }

        // Will be possible in MVC6 only
        //public IHttpActionResult Get(int id)
        //{
        //    return new ObjectResult(_RoomRepo.GetAllRooms().Where(r => r.Id == id).FirstOrDefault());
        //}

        //public Contracts.IRoom Get(HttpRequestMessage request, int id)
        //{
        //    return _RoomRepo.GetAllRooms().Where(r => r.Id == id).FirstOrDefault();            
        //}


        // POST: api/Rooms
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody]Models.RoomViewModel room)
        {
            if (room != null)
            {
                return request.CreateResponse(System.Net.HttpStatusCode.Created,  _RoomRepo.AddRoom(room));
            }
            return null;
        }


        // PUT: api/Rooms/5
        public void Put(int id, [FromBody]Models.RoomViewModel room)
        {
            if (room != null)
            {
                _RoomRepo.EditRoom(room);
            }
        }

        // DELETE: api/Rooms/5
        public void Delete(int id)
        {
            _RoomRepo.DeleteRoom(id);
        }
    }
}
