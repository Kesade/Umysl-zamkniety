using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Common.DomainEntities;
using Common.Services;

namespace BlogUI.Controllers.Api
{
    public class GenericApiController<T> : ApiController where T : class, IDomainEntity
    {
        private readonly IService<T> _service;

        public GenericApiController(IService<T> service)
        {
            _service = service;
        }

        public async Task<IHttpActionResult> Get()
        {
            var objects = await _service.GetRepository();

            if (!objects.Any())
                return NotFound();

            return Ok(objects);
        }

        public async Task<IHttpActionResult> GetById(int id)
        {
            return Ok(await _service.GetById(id));
        }

        public async Task<IHttpActionResult> GetByParrent(int parrentId)
        {
            var objects = (await _service.GetRepository()).Where(x => x.ParrentId == parrentId);

            return Ok(objects);
        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(T obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Put(obj);


            return StatusCode(HttpStatusCode.NoContent);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            await _service.Delete(id);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _service != null)
                _service.Dispose();
            base.Dispose(disposing);
        }

        public async Task<bool> Exists(int id)
        {
            return await _service.GetById(id) != null;
        }
    }
}