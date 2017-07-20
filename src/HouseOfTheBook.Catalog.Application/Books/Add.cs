using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using HouseOfTheBook.Catalog.Application.Attributes;
using HouseOfTheBook.Catalog.Infrastructure;
using HouseOfTheBook.Catalog.Model;
using HouseOfTheBook.Common;
using HouseOfTheBook.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HouseOfTheBook.Catalog.Application.Books
{
    public sealed class Add
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
            private readonly IMapper mapper;
            private readonly CatalogContext context;

            public CommandHandler(IMapper mapper, CatalogContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<Response> Handle(Command message)
            {
                var book = mapper.Map<Book>(message.Request);
                var author = context.Auhtors.SingleOrDefaultAsync(a => a.Id == book.AuthorId);
                if (author == null)
                {
                    throw new EntityNotFoundException($"Could not find an author with id {book.AuthorId}");
                }
                context.Books.Add(book);
                await context.SaveChangesAsync();
                return new Response { Id = book.Id};
            }
        }
    }
}

