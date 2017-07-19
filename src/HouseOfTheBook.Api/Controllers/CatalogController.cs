using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using HouseOfTheBook.Catalog.Application.Books;

namespace HouseOfTheBook.Api.Controllers
{
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator mediator;

        public CatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("books")]
        public async Task<IActionResult> AddBook(Create.Request request)
        {
            var response = await mediator.Send(new Create.Command(request));
            return Created("/books", response);
        }
    }
}
