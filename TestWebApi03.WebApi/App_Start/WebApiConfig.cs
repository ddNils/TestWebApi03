using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Batch;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.Data.Edm;
using TestWebApi03.WebApi.Models;

namespace TestWebApi03.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(System.Web.Http.HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste

            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            //ODataModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Contracts.IResident>("Residents");
            //config.AddODataQueryFilter(new System.Web.Http.Filters.ActionFilterAttribute() );
            //config.Routes.MapODataRoute("ODataRoute", "odata", GetEdmModel());

            //ODataModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Models.ResidentViewModel>("Residents");
            //config.MapODataServiceRoute(
            //    routeName: "ODataRoute",
            //    routePrefix: "odata",
            //    model: builder.GetEdmModel());

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Models.ResidentViewModel>("ResidentViewModels");
            config.AddODataQueryFilter();
            //config.MapODataServiceRoute(routeName: "oDataRoute", routePrefix: "odata", model: builder.GetEdmModel());
            config.MapODataServiceRoute(routeName: "oDataRoute", routePrefix: "odata", model: builder.GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.MapODataServiceRoute( "odata", null, GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));



            config.EnsureInitialized();
        }

    }

}
