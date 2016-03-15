using System;
using System.Linq;
using System.Web.Http;
using BookWarehouse.Core.Domain;
using BookWarehouse.Core.Service;

namespace BookWarehouse.Web.Controllers.Api
{
    /// <summary>
    /// Returns information on available titles
    /// </summary>
    [RoutePrefix("api/title")]
    public class TitleApiController : ApiController
    {
        private readonly ITitleService _titles;

        public TitleApiController(ITitleService titles)
        {
            _titles = titles;
        }

        // GET: api/title
        /// <summary>
        /// Returns information on all available titles
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Available Title Information</returns>
        [Route("")]
        public IHttpActionResult Get()
        {
            var results = _titles.GetAll().ToList();
            if (!results.Any()) return NotFound();
            return Ok(results);
        }

        // GET: api/title/Guid
        /// <summary>
        /// Returns information on specific Title
        /// </summary>
        /// <param name="id">The identifier for the title</param>
        /// <returns></returns>
        [Route("{id:Guid}")]
        public IHttpActionResult Get(Guid id)
        {
            var result = _titles.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Returns the availabe on hand for a particular title
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("count/{id:Guid}")]
        public IHttpActionResult GetCount(Guid id)
        {
            var count = _titles.OnHand(id);
            return Ok(count);
        }

        /// <summary>
        /// Returns the available on hand for a particular title at a particular warehouse
        /// </summary>
        /// <param name="id"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        [Route("count/{id:Guid}/{warehouse}")]
        public IHttpActionResult GetCountFromWarehouse(Guid id, string warehouse)
        {
            Guid whguid;
            var parsed = Guid.TryParse(warehouse, out whguid);
            var count = parsed
                ? _titles.OnHand(id, whguid)
                : _titles.OnHand(id);
            return Ok(count);
        }


        /// <summary>
        /// Returns information for a particular title by ISBN
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("isbn")]
        public IHttpActionResult GetIsbn(long id)
        {
            var result = _titles.Find(x => x.Isbn == id);
            if(result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Returns information on titles by Publication Year
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("year")]
        public IHttpActionResult GetYear(int id)
        {
            var results = _titles.Where(x => x.YearPublished == id);
            if(results == null) return NotFound();
            return Ok(results);
        }

        /// <summary>
        /// Returns information by Name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("name")]
        public IHttpActionResult GetName(string id)
        {
            var result = _titles.Find(x => x.Name == id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Create a new Title
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] Title value)
        {
            if (value == null) return BadRequest("null");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _titles.Create(value);
            var result = _titles.Find(x => x.Isbn == value.Isbn);
            return Created($"{Request.RequestUri}/{result.TitleId}", result);
        }

        /// <summary>
        /// Remove a title
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:Guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            var item = _titles.Get(id);
            if (item == null) return NotFound();
            _titles.Delete(id);
            return Ok(id);
        }
    }
}
