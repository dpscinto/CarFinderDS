using CarFinderDS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarFinderDS.Controllers
{
    public class CarFinderController : ApiController
    {
            private ApplicationDbContext db = new ApplicationDbContext();

            public IHttpActionResult GetMakes()
            {
            var retval = db.Database.SqlQuery<string>(
            "EXEC GetMakes"
            ).ToList();

            return Ok(retval);
            }

            public IHttpActionResult GetModelByMake(string make)
            {
                var Smake = new SqlParameter("@make", make);
                var retval = db.Database.SqlQuery<string>(
                "EXEC GetModelByMake @make", Smake
                ).ToList();

                return Ok(retval);
            }

            public IHttpActionResult GetTrimByMakeModel(string make, string model_name)
            {
                var Smake = new SqlParameter("@make", make);
                var Smodel_name = new SqlParameter("@model_name", model_name);
                var retval = db.Database.SqlQuery<string>(
                "EXEC GetTrimByMakeModel @make, @model_name", Smake, Smodel_name
                ).ToList();

            return Ok(retval);
            }

            public IHttpActionResult GetYearByMakeModelTrim(string make, string model_name, string model_trim)
            {
                var Smake = new SqlParameter("@make", make);
                var Smodel_name = new SqlParameter("@model_name", model_name);
                var Smodel_trim = new SqlParameter("@model_trim", model_trim);
                var retval = db.Database.SqlQuery<string>(
                "EXEC GetYearByMakeModelTrim @make, @model_name, @model_trim", Smake, Smodel_name, Smodel_trim
                ).ToList();

            return Ok(retval);
            }
            public IHttpActionResult GetCarsAlt(string make="", string model_name="", string model_trim="", string model_year="")
            {        
                var Smake = new SqlParameter("@make", make??"");
                var Smodel_name = new SqlParameter("@model_name", model_name??"");
                var Smodel_trim = new SqlParameter("@model_trim", model_trim??"");
                var Smodel_year = new SqlParameter("@model_year", model_year ?? "");
                var retval = db.Database.SqlQuery<Car>(
                "EXEC GetCarsAlt @make, @model_name, @model_trim, @model_year", Smake, Smodel_name, Smodel_trim, Smodel_year
                ).ToList();

            return Ok(retval);
            }

    }
}

