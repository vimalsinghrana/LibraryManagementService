using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementService.Models
{
	public interface ILibraryService
	{
		Book IssueBook(int memberId, int bookId);
		void ReturnBook(int memberId, int bookId);
		bool AddBook(Book book);
		IEnumerable<IssueRecord> GetIssuedBooks(int memberId);

		IEnumerable<Book> GetAllBooks();
	}
}