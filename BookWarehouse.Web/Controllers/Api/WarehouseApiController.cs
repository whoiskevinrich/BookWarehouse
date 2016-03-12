using System.Collections.Generic;
using System.Web.Http;
using BookWarehouse.Core.Service;

namespace BookWarehouse.Web.Controllers.Api
{
    [RoutePrefix("api/warehouse")]
    public class WarehouseApiController : ApiController
    {
        private readonly IWarehouseService _warehouse;

        public WarehouseApiController(IWarehouseService warehouse)
        {
            _warehouse = warehouse;
        }

        // GET: api/Warehouse
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Warehouse/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Warehouse
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Warehouse/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Warehouse/5
        public void Delete(int id)
        {
        }
    }
}
