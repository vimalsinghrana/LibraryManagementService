using LibraryManagementService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
			_service = new LibraryService();
		}

		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				var issuedBooks = _service.GetIssuedBooks(1);
				return Ok(issuedBooks);
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		/// <summary>
		/// Issues a book to a member.
		/// </summary>
		[HttpPost]
		[Route("api/Book/IssueBook")]
		[ActionName("IssueBook")]
		public IHttpActionResult IssueBook(int memberId, int bookId)
		{
			try
			{
				_service.IssueBook(memberId, bookId);
				return Ok("Book issued successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("api/book/GetAllBooks")]
		[ActionName("GetAllBooks")]
		[HttpGet]
		public IHttpActionResult GetAllBooks()
		{
			try
			{
				var books = _service.GetAllBooks();
				return Ok(books);
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		[Route("api/book/GetIssuedBooks")]
		[ActionName("GetIssuedBooks")]
		[HttpGet]
		public IHttpActionResult GetIssuedBooks(int memberId)
		{
			try
			{
				var issuedBooks = _service.GetIssuedBooks(memberId);
				return Ok(issuedBooks);
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		[Route("api/book/ReturnBook")]
		[ActionName("ReturnBook")]
		[HttpPost]
		public IHttpActionResult ReturnBook(int memberId, int bookId)
		{
			try
			{
				_service.ReturnBook(memberId, bookId);
				return Ok("Book returned successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// POST api/book
		[HttpPost]
		public IHttpActionResult Post([FromBody] Book book)
		{
			if (book == null)
				return BadRequest("Book data is required.");

			try
			{
				var result = _service.AddBook(book);
				if (result)
					return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);

				return BadRequest("Failed to add book.");
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// PUT api/book/5
		[HttpPut]
		public IHttpActionResult Put(int id, [FromBody] Book updatedBook)
		{
			if (updatedBook == null)
				return BadRequest("Book data is required.");

			try
			{
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
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// DELETE api/book/5
		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			try
			{
				var context = new LibraryContext();
				var book = context.Books.Find(id);
				if (book == null)
					return NotFound();

				context.Books.Remove(book);
				context.SaveChanges();
				return StatusCode(System.Net.HttpStatusCode.NoContent);
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}
	}

	public class MemberDto
	{
		[Required]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}