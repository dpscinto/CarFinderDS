using CarFinderDS.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Bing;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace CarFinderDS.Controllers
{
    public class CarFinderController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public class Selected
        {
            public string make { get; set; }
            public string model { get; set; }
            public string trim { get; set; }
            public string year { get; set; }
        }

        /// <summary>
        ///     Retrieve all makes.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetMakes()
        {
            var retval = db.Database.SqlQuery<string>("EXEC GetMakes").ToList();

            return Ok(retval);
        }

        /// <summary>
        ///         Retrieve make for a given model.
        /// </summary>
        /// <param name="make"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetModelByMake(Selected selected)
        {
            var Smake = new SqlParameter("@make", selected.make);
            var retval = db.Database.SqlQuery<string>(
            "EXEC GetModelByMake @make", Smake
            ).ToList();

            return Ok(retval);
        }
        /// <summary>
        ///         Retrieve trim for a given make and model.
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model_name"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetTrimByMakeModel(Selected selected)
        {
            var Smake = new SqlParameter("@make", selected.make);
            var Smodel_name = new SqlParameter("@model_name", selected.model);
            var retval = db.Database.SqlQuery<string>(
            "EXEC GetTrimByMakeModel @make, @model_name", Smake, Smodel_name
            ).ToList();

            return Ok(retval);
        }
        /// <summary>
        ///         Retrieve year for a given make, model, and trim.
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model_name"></param>
        /// <param name="model_trim"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetYearByMakeModelTrim(Selected selected)
        {
            var Smake = new SqlParameter("@make", selected.make);
            var Smodel_name = new SqlParameter("@model_name", selected.model);
            var Smodel_trim = new SqlParameter("@model_trim", selected.trim);
            var retval = db.Database.SqlQuery<string>(
            "EXEC GetYearByMakeModelTrim @make, @model_name, @model_trim", Smake, Smodel_name, Smodel_trim
            ).ToList();

            return Ok(retval);
        }

        /// <summary>
        ///         Retrieve information about a car based on parameters.
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model_name"></param>
        /// <param name="model_trim"></param>
        /// <param name="model_year"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetCarsAlt(Selected selected)
        {
            var Smake = new SqlParameter("@make", selected.make ?? "");
            var Smodel_name = new SqlParameter("@model_name", selected.model ?? "");
            var Smodel_trim = new SqlParameter("@model_trim", selected.trim ?? "");
            var Smodel_year = new SqlParameter("@model_year", selected.year ?? "");
            var retval = db.Database.SqlQuery<Car>(
            "EXEC GetCarsAlt @make, @model_name, @model_trim, @model_year", Smake, Smodel_name, Smodel_trim, Smodel_year
            ).ToList();

            return Ok(retval);
        }
        public class IdParam
        {
            public int id { get; set; }
        }

        /// <summary>
        ///         Access NHTSA WebAPI to retrieve recall info of cars.
        ///         Access Bing Search Images.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> getInfo(IdParam Id)
        {
            HttpResponseMessage response;
            string content = "";
            var Car = db.Cars.Find(Id.id);
            dynamic Recalls = "";
            var Image = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.nhtsa.gov/");
                try
                {
                    response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/" + Car.model_year +
                                                                                "/make/" + Car.make +
                                                                                "/model/" + Car.model_name + "?format=json");
                    content = await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
            Recalls = JsonConvert.DeserializeObject(content);

            var image = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/search/v1/Image"));

            image.Credentials = new NetworkCredential("accountKey", "s5qR9wj4x5Xd/PoZR273bx1paqZcYPyw2HIwGzR6zys=");
            var marketData = image.Composite(
                "image",
                Car.model_year + " " + Car.make + " " + Car.model_name + " " + Car.model_trim + " " + "NOT ebay",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
                ).Execute();

            Image = marketData.First().Image.First().MediaUrl;
            return Ok(new
            {
                car = Car,
                recalls = Recalls,
                image = Image
            });
        }
    }
}


