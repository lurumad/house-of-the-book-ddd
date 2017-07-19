using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using HouseOfTheBook.Catalog.Application.Attributes;
using HouseOfTheBook.Catalog.Infrastructure;
using HouseOfTheBook.Catalog.Model;
using MediatR;

namespace HouseOfTheBook.Catalog.Application.Books
{
    public sealed class Create
    {
        public class Request
        {
            [Required]
            [StringLength(100)]
            public string Title { get; set; }
            [Required]
            [StringLength(1000)]
            public string Description { get; set; }
            [Required]
            [Isbn]
            public string Isbn { get; set; }
            [Range(0,2000)]
            public int Pages { get; set; }
            [Range(0, 50)]
            public int AvailableStock { get; set; }
            [Range(0, Int32.MaxValue)]
            public int AuthorId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
        }

        public class Command : IRequest<Response>
        {
            public Request Request { get; }

            public Command(Request request)
            {
                Request = request;
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command, Response>
        {
            private readonly CatalogContext context;

            public CommandHandler(CatalogContext context)
            {
                this.context = context;
            }

            public async Task<Response> Handle(Command message)
            {
                var book = Mapper.Map<Book>(message.Request);
                context.Books.Add(book);
                await context.SaveChangesAsync();
                return new Response { Id = book.Id};
            }
        }
    }
}
