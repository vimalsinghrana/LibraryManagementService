using LibraryManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryManagementService.Controllers
{
    public class LibraryInventoryController : ApiController
    {
		private readonly ILibraryService _service;
		//* GET: LibraryInventory

		public LibraryInventoryController()
		{
			_service = new LibraryService();
		}
		


		public void POST(Book book)
		{
			bool result = _service.AddBook(book);
			var ListBook = _service.GetAllBooks();
		}
	}
}