using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demoproductos.Models;
using Demoproductos.Web.Service;
using Demoproductos.Web.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace Demoproductos.Controllers
{
    [Route("v1/products")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository ItemRepository;
        private readonly ITestService _testService;

        public ItemController(IItemRepository itemRepository, ITestService testService)
        {
            ItemRepository = itemRepository;
            _testService = testService; 
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TestModel>> List()
        {
            var model = _testService.GetTestList();
            return Ok(model);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestModel> GetItem(int id)
        {
            TestModel item = _testService.GetProductById(id);

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> Create([FromBody] TestModel producto)
        {

            var response = _testService.CreateProduct(producto);

            if (response == false)
                return NotFound();

            return response;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TestModel> Edit(int id,[FromBody] TestModel producto)
        {
            var response = _testService.UpdateProduct(id, producto);

            return response;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TestModel>> Delete(int id)
        {
            var response = _testService.DeleteProduct(id);

            return Ok(response);
        }
    }
}
