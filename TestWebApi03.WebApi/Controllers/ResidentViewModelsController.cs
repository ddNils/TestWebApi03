using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using TestWebApi03.Repos;
using TestWebApi03.WebApi.Models;

namespace TestWebApi03.WebApi.Controllers
{
    /*
    Die Klasse "WebApiConfig" erfordert ggf. weitere Änderungen zum Hinzufügen einer Route für diesen Controller. Führen Sie diese Anweisungen in der Methode "Register" der Klasse "WebApiConfig" ordnungsgemäß zusammen. Beachten Sie, dass für OData-URLs zwischen Groß- und Kleinschreibung unterschieden wird.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using TestWebApi03.WebApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<ResidentViewModel>("ResidentViewModels");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
        [EnableQuery(MaxNodeCount = 100, PageSize = 20)]
    public class ResidentViewModelsController : ODataController
    {
        private IRoomManagementRepository db;
        
        public ResidentViewModelsController(IRoomManagementRepository rmp)
        {
            db = rmp;
        }

        // GET: odata/ResidentViewModels
        [EnableQuery]
        public IQueryable<Models.ResidentViewModel> GetResidentViewModels()
        {

            //return db.GetAllResidents();

            Mapper.CreateMap<Contracts.IResident, ResidentViewModel>();

            var res = db.GetAllResidents().ProjectTo<ResidentViewModel>();
            //List<ResidentViewModel> list = new List<ResidentViewModel>();

            //foreach (var r in res)
            //{
            //    list.Add(new ResidentViewModel()
            //    {
            //        Id = r.Id,
            //        JobDescription = r.JobDescription,
            //        LastName = r.LastName,
            //        Name = r.Name,
            //        Title = r.Title
            //    });
            //}
            return res;
        }

        // GET: odata/ResidentViewModels(5)
        [EnableQuery]
        public SingleResult<Models.ResidentViewModel> GetResidentViewModel([FromODataUri] int key)
        {
            // Really need to do this via automapper (better in Repo)
            Mapper.CreateMap<Contracts.IResident, ResidentViewModel>();


            var res = db.GetAllResidents().Where(r => r.Id == key).Project().To<ResidentViewModel>();
            //List<ResidentViewModel> list = new List<ResidentViewModel>();

            //foreach (var r in res)
            //{
            //    list.Add(new ResidentViewModel()
            //    {
            //        Id = r.Id,
            //        JobDescription = r.JobDescription,
            //        LastName = r.LastName,
            //        Name = r.Name,
            //        Title = r.Title
            //    });
            //}

            return SingleResult.Create(res);
        }

        // PUT: odata/ResidentViewModels(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Contracts.IResident> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contracts.IResident residentViewModel = db.GetAllResidents().Where(r => r.Id == key).FirstOrDefault();
            if (residentViewModel == null)
            {
                return NotFound();
            }

            patch.Put(residentViewModel);

            //try
            //{
            //    db.EditRoom(residentViewModel);
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ResidentViewModelExists(key))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return Updated(residentViewModel);
        }

        // POST: odata/ResidentViewModels
        public IHttpActionResult Post(int roomId, Contracts.IResident residentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AddInhabitant(roomId, residentViewModel);
            //db.SaveChanges();

            return Created(residentViewModel);
        }

        // PATCH: odata/ResidentViewModels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Contracts.IResident> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contracts.IResident residentViewModel = db.GetAllResidents().Where(r => r.Id == key).FirstOrDefault();
            if (residentViewModel == null)
            {
                return NotFound();
            }

            patch.Patch(residentViewModel);

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ResidentViewModelExists(key))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return Updated(residentViewModel);
        }

        // DELETE: odata/ResidentViewModels(5)
        //public IHttpActionResult Delete([FromODataUri] int key, int roomId)
        //{
        //    ResidentViewModel residentViewModel = db.RemoveInhabitant(roomId, );
        //    if (residentViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    db.ResidentViewModels.Remove(residentViewModel);
        //    db.SaveChanges();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}
        
    }
}
