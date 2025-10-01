using LibraryManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.DataAnnotations; 


namespace LibraryManagementService.Controllers
{
	[RoutePrefix("api/members")]
	public class MemberController : ApiController
	{
		private readonly LibraryContext _context = new LibraryContext();

		// GET: api/members
		[HttpGet]
		[Route("")]
		public IHttpActionResult GetAllMembers()
		{
			var members = _context.Members.ToList();
			return Ok(members);
		}

		/// GET: api/members/{id}
		[HttpGet]
		[Route("{id:int}")]
		public IHttpActionResult GetMember(int id)
		{
			var member = _context.Members.Find(id);
			if (member == null)
				return NotFound();
			return Ok(member);
		}

		/// POST: api/members
		[HttpPost]
		[Route("")]
		public IHttpActionResult CreateMember([FromBody] Member member)
		{
			if (member == null)
				return BadRequest("Member data is required.");

			if (!IsValidEmail(member.Email))
				return BadRequest("Invalid email address.");

			_context.Members.Add(member);
			_context.SaveChanges();
			return CreatedAtRoute("", new { id = member.Id }, member);
		}

		/// PUT: api/members/{id}
		[HttpPut]
		[Route("{id:int}")]
		public IHttpActionResult UpdateMember(int id, [FromBody] Member updatedMember)
		{
			if (updatedMember == null)
				return BadRequest("Member data is required.");

			if (!IsValidEmail(updatedMember.Email))
				return BadRequest("Invalid email address.");

			var member = _context.Members.Find(id);
			if (member == null)
				return NotFound();

			member.Name = updatedMember.Name;
			member.Email = updatedMember.Email;

			_context.SaveChanges();
			return Ok(member);
		}

		// DELETE: api/members/{id}
		[HttpDelete]
		[Route("{id:int}")]
		public IHttpActionResult DeleteMember(int id)
		{
			var member = _context.Members.Find(id);
			if (member == null)
				return NotFound();

			_context.Members.Remove(member);
			_context.SaveChanges();
			return StatusCode(HttpStatusCode.NoContent);
		}

		private bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			var emailAttribute = new EmailAddressAttribute();
			return emailAttribute.IsValid(email);
		}
	}
}