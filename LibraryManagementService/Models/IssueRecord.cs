using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementService.Models
{
	public class IssueRecord
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public int MemberId { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime? ReturnDate { get; set; }

		public virtual Book Book { get; set; }
		public virtual Member Member { get; set; }
	}
}