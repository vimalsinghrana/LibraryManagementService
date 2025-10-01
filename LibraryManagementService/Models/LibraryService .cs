using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LibraryManagementService.Models
{
	public class LibraryService : ILibraryService
	{
    private readonly LibraryContext _context;

		public LibraryService(LibraryContext context)
		{
			_context = context;
		}

		public Book IssueBook(int memberId, int bookId)
		{
			var book = _context.Books.Find(bookId);
			if (book == null || book.AvailableCopies <= 0)
				throw new Exception("Book not available.");

			var record = new IssueRecord
			{
				MemberId = memberId,
				BookId = bookId,
				IssueDate = DateTime.UtcNow
			};

			book.AvailableCopies -= 1;
			_context.IssueRecords.Add(record);
			_context.SaveChanges();
			return book;
		}

		public void ReturnBook(int memberId, int bookId)
		{
			var record = _context.IssueRecords
				.FirstOrDefault(r => r.MemberId == memberId && r.BookId == bookId && r.ReturnDate == null);

			if (record == null)
				throw new Exception("No active issue found.");

			record.ReturnDate = DateTime.UtcNow;
			var book = _context.Books.Find(bookId);
			if (book != null)
				book.AvailableCopies += 1;

			_context.SaveChanges();
		}

		public IEnumerable<IssueRecord> GetIssuedBooks(int memberId)
		{
			return _context.IssueRecords
				.Where(r => r.MemberId == memberId && r.ReturnDate == null)
				.Include(r => r.Book)
				.ToList();
		}

		public IEnumerable<Book> GetAllBooks()
		{
			return _context.Books
				.ToList();
		}

		public bool AddBook(Book _book)
		{	try
			{
				_context.Books.Add(_book);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}
	}
}