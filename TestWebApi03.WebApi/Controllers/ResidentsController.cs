using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestWebApi03.WebApi.Controllers
{
    public class ResidentsController : ApiController
    {
        private readonly Repos.IRoomManagementRepository _RoomManagement;
        
        public ResidentsController(Repos.IRoomManagementRepository repo)
        {
            this._RoomManagement = repo;
        }
        
        // GET: api/Residents
        public IEnumerable<Contracts.IResident> Get()
        {            
            return _RoomManagement.GetAllResidents().AsEnumerable();
        }

        // GET: api/Residents/5
        public Contracts.IResident Get(int id)
        {
            return _RoomManagement.GetAllResidents().Where(r => r.Id == id).FirstOrDefault();
        }

        // POST: api/Residents
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody]Models.ResidentViewModel resident)
        {
            if (resident != null)
            {
                if (resident.RoomId == 0)
                {
                    return request.CreateResponse(HttpStatusCode.OK, _RoomManagement.AddResident(resident));
                } else {
                    return request.CreateResponse(HttpStatusCode.OK, _RoomManagement.AddInhabitant(resident.RoomId, resident));
                }
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Resident was null");
        }

        // PUT: api/Residents
        public HttpResponseMessage Put(HttpRequestMessage request, [FromBody]Models.ResidentViewModel resident)
        {
            if (resident != null)
            {
                if (resident.RoomId == 0)
                {
                    return request.CreateResponse(HttpStatusCode.OK, _RoomManagement.EditResident(resident));
                } else
                {
                    return request.CreateResponse(HttpStatusCode.OK, _RoomManagement.AddInhabitant(resident.RoomId, resident));
                }
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not update Resident");
        }

        // DELETE: api/Residents/5
        public void Delete(int id)
        {
            
        }
    }
}
