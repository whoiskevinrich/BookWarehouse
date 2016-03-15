using System;
using System.Linq;
using System.Web.Http;
using BookWarehouse.Core.Domain;
using BookWarehouse.Core.Service;

namespace BookWarehouse.Web.Controllers.Api
{
    /// <summary>
    /// Manipulate information on inventory items
    /// </summary>
    [RoutePrefix("api/inventory")]
    public class InventoryItemApiController : ApiController
    {
        private readonly IInventoryItemService _inventory;

        public InventoryItemApiController(IInventoryItemService inventory)
        {
            _inventory = inventory;
        }

        // GET: api/inventoryitem
        /// <summary>
        /// Get information about all inventory
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Get()
        {
            var results = _inventory.GetAll().ToList();
            if (!results.Any()) return NotFound();
            return Ok(results);
        }

        // GET: api/inventoryitem/Guid
        /// <summary>
        /// Get information about a single item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:Guid}")]
        public IHttpActionResult Get(Guid id)
        {
            var result = _inventory.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get information on all inventory at a particular warehouse
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("warehouse/{id:Guid}")]
        public IHttpActionResult GetWarehouse(Guid id)
        {
            var results = _inventory.Where(x => x.WarehouseId == id).ToList();
            if (!results.Any()) return NotFound();
            return Ok(results);
        }

        /// <summary>
        /// Get information about inventory by warehouse and by title
        /// </summary>
        /// <param name="id">Warehouse Model</param>
        /// <param name="titleId">Title Model</param>
        /// <returns></returns>
        [Route("warehouse/{id:Guid}/{titleId:Guid}")]
        public IHttpActionResult GetWarehouseTitle(Guid id, Guid titleId)
        {
            var results = _inventory.Where(x => x.WarehouseId == id && x.TitleId == titleId).ToList();
            if(!results.Any()) return NotFound();
            return Ok(results);
        }

        /// <summary>
        /// get information about inventory by title
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("title")]
        public IHttpActionResult GetTitle(Guid id)
        {
            var results = _inventory.Where(x => x.TitleId == id).ToList();
            if(!results.Any()) return NotFound();
            return Ok(results);
        }

        // POST: api/inventoryitem
        /// <summary>
        /// Update an inventory item
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]InventoryItem value)
        {
            if (value == null) return BadRequest("null");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _inventory.Create(value);
            return Created($"{Request.RequestUri}/{value}", value);
        }

        // DELETE: api/inventoryitem/{Guid}
        /// <summary>
        /// Delete a particular item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(Guid id)
        {
            var item = _inventory.Get(id);
            if (item == null) return NotFound();
            _inventory.Delete(id);
            return Ok(id);
        }
    }
}