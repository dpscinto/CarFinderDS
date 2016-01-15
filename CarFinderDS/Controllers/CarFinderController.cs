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

            public IHttpActionResult GetYears()
            {
            var retval = db.Database.SqlQuery<string>(
            "EXEC GetYears"
            ).ToList();

            return Ok(retval);
            }

            public IHttpActionResult GetMakesByYear(string model_year)
            {
                var Smodel_year = new SqlParameter("@model_year", model_year);
                var retval = db.Database.SqlQuery<string>(
                "EXEC GetMakesByYear @model_year", Smodel_year
                ).ToList();

                return Ok(retval);
            }

            public IHttpActionResult GetModelByYearMake(string model_year, string make)
            {
                var Smodel_year = new SqlParameter("@model_year", model_year);
                var Smake = new SqlParameter("@make", make);
                var retval = db.Database.SqlQuery<string>(
                "EXEC GetModelByYearMake @model_year, @make", Smodel_year, Smake
                ).ToList();

            return Ok(retval);
            }

            public IHttpActionResult GetTrimByYearMakeModel(string model_year, string make, string model_name)
            {
                var Smodel_year = new SqlParameter("@model_year", model_year);
                var Smake = new SqlParameter("@make", make);
                var Smodel_name = new SqlParameter("@model_name", model_name);
                var retval = db.Database.SqlQuery<string>(
                "EXEC GetTrimByYearMakeModel @model_year, @make, @model_name", Smodel_year, Smake, Smodel_name
                ).ToList();

            return Ok(retval);
            }
            public IHttpActionResult GetCars(string model_year="", string make="", string model_name="", string model_trim="")
            {
                var Smodel_year = new SqlParameter("@model_year", model_year??"");
                var Smake = new SqlParameter("@make", make??"");
                var Smodel_name = new SqlParameter("@model_name", model_name??"");
                var Smodel_trim = new SqlParameter("@model_trim", model_trim??"");
                var retval = db.Database.SqlQuery<Car>(
                "EXEC GetCars @model_year, @make, @model_name, @model_trim", Smodel_year, Smake, Smodel_name, Smodel_trim
                ).ToList();

            return Ok(retval);
            }

    }
}

