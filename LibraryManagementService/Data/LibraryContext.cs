using System.Data.Entity;
using LibraryManagementService.Models;

public class LibraryContext : DbContext
{
	public LibraryContext() : base("LibraryDbConnection") { }

	public DbSet<Book> Books { get; set; }
	public DbSet<Member> Members { get; set; }
	public DbSet<IssueRecord> IssueRecords { get; set; }

	protected override void OnModelCreating(DbModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<IssueRecord>()
			.HasRequired(i => i.Book)
			.WithMany()
			.HasForeignKey(i => i.BookId);

		modelBuilder.Entity<IssueRecord>()
			.HasRequired(i => i.Member)
			.WithMany()
			.HasForeignKey(i => i.MemberId);
	}
}
