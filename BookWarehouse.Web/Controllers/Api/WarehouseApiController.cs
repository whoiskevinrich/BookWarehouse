using System;
using System.Linq;
using System.Web.Http;
using BookWarehouse.Core.Domain;
using BookWarehouse.Core.Service;

namespace BookWarehouse.Web.Controllers.Api
{
    /// <summary>
    /// Manipulation of Warehouse Information
    /// </summary>
    [RoutePrefix("api/warehouse")]
    public class WarehouseApiController : ApiController
    {
        private readonly IWarehouseService _warehouse;

        public WarehouseApiController(IWarehouseService warehouse)
        {
            _warehouse = warehouse;
        }

        // GET: api/Warehouse
        /// <summary>
        /// Get information on all warehouses
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Get()
        {
            var results = _warehouse.GetAll().ToList();
            if (!results.Any()) return NotFound();
            return Ok(results);
        }

        /// <summary>
        /// Get information about a partiuclar warehouse
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Warehouse/Guid
        [Route("{id:Guid}")]
        public IHttpActionResult Get(Guid id)
        {
            var result = _warehouse.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/Warehouse
        /// <summary>
        /// Update information on a particular warehouse
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Warehouse value)
        {
            if (value == null) return BadRequest("null");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _warehouse.Create(value);
            var result = _warehouse.Find(x => x.Name == value.Name);
            return Created($"{Request.RequestUri}/{result.WarehouseId}", result);
        }

        // DELETE: api/Warehouse/5
        /// <summary>
        /// Delete a warehouse
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:Guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            var item = _warehouse.Get(id);
            if (item == null) return NotFound();
            _warehouse.Delete(id);
            return Ok(id);
        }
    }
}
