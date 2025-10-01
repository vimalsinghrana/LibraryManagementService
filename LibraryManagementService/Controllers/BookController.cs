using LibraryManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryManagementService.Controllers
{
	public class BookController : ApiController
	{
		private readonly ILibraryService _service;

		public BookController()
		{
			//_service = new VimLibraryService();
			_service = new LibraryService();
		}
		public IEnumerable<IssueRecord> Get()
		{
			return (IEnumerable<IssueRecord>)_service.GetIssuedBooks(1);
			
			//return new string[] { "value1", "value2" };
		}
		
		[HttpPost]
		[Route("api/Book/IssueBook")]
		[ActionName("IssueBook")]
		public Book IssueBook(int memberId, int bookId)
		{
			return _service.IssueBook(memberId, bookId) ;
		}


	    [Route("api/book/GetAllBooks")]
		[ActionName("GetAllBooks")]
		[HttpGet]
		public IEnumerable<Book> GetAllBooks()
		{
			return _service.GetAllBooks();
		}

		[Route("api/book/GetIssuedBooks")]
		[ActionName("GetIssuedBooks")]
		[HttpGet]
		public IEnumerable<IssueRecord> Get(int memberId)
		{
			return _service.GetIssuedBooks(memberId);
		}

		[Route("api/book/ReturnBook")]
		[ActionName("ReturnBook")]
		[HttpPost]
		public void POST(int memberId, int bookId)
		{
			 _service.ReturnBook(memberId, bookId) ;
		}

		// POST api/values
		[HttpPost]
		public IHttpActionResult Post([FromBody] Book book)
		{
			if (book == null)
				return BadRequest("Book data is required.");

			var result = _service.AddBook(book);
			if (result)
				return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);

			return BadRequest("Failed to add book.");
		}

		// PUT api/book/5
		[HttpPut]
		public IHttpActionResult Put(int id, [FromBody] Book updatedBook)
		{
			if (updatedBook == null)
				return BadRequest("Book data is required.");

			var context = new LibraryContext();
			var book = context.Books.Find(id);
			if (book == null)
				return NotFound();

			book.Title = updatedBook.Title;
			book.Author = updatedBook.Author;
			book.PublishedYear = updatedBook.PublishedYear;
			book.AvailableCopies = updatedBook.AvailableCopies;

			context.SaveChanges();
			return Ok(book);
		}

		// DELETE api/book/5
		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			var context = new LibraryContext();
			var book = context.Books.Find(id);
			if (book == null)
				return NotFound();

			context.Books.Remove(book);
			context.SaveChanges();
			return StatusCode(System.Net.HttpStatusCode.NoContent);
		}
	}

}