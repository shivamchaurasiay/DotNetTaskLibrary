using DotNetsTask.Data.Models;
using DotNetsTask.Repo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace DotNetsTask.Service.DropDown
{

	public class DropdownHelper : IDropdownHelper
	{
		public readonly IRepository<Book> _Book;
		public readonly IRepository<User> _user;
		public readonly IRepository<IssuedBook> _repoIssueBook;
		
		public DropdownHelper( IRepository<Book> book,
			IRepository<User> user, IRepository<IssuedBook> repoIssueBook)
		{
			
			_Book = book;
			_user = user;
			_repoIssueBook = repoIssueBook;
			
		}


		public List<SelectListItem> GetIssueBookByStudentId(int StudentId)
		{
			// Fetch issued books for the specific student
			var issuedBooks = _repoIssueBook
				.Query()
				.Include(a => a.Book) // Include related Book entity
				.Get()
				.Where(a => a.UserId == StudentId && a.IsActive == true && a.Status==0)
				.ToList(); // Get the list of issued books

			// Create a default select list item
			var selectItem = new SelectListItem
			{
				Text = "Select a Book",
				Value = "0"
			};

			// Build the select list items from the issued books
			var selectList = new List<SelectListItem> { selectItem }
				.Concat(issuedBooks.Select(issue => new SelectListItem
				{
					Text = issue.Book.Subject, // Assuming 'Title' is the property for the book's name
					Value = issue.IssueId.ToString() // Assuming 'IssueId' is the property of the issued book
				}))
				.ToList();

			return selectList;
		}






		public async Task<List<SelectListItem>> GetAllStudents()
		{
			var res = await _user.GetAllAsync();
			var selectItem = new SelectListItem
			{
				Text = "Select",
				Value = "0"
			};
			var selectList = new List<SelectListItem> { selectItem }
					.Concat(res.Where(a => a.IsActive == true).Select(i => new SelectListItem
					{
						Text = i.Name,
						Value = i.UserId.ToString(),
					}))
					.ToList();

			return selectList;
		}


		public async Task<List<SelectListItem>> GetAllBooks()
		{
			var res = await _Book.GetAllAsync();
			var selectItem = new SelectListItem
			{
				Text = "Select",
				Value = "0"
			};
			var selectList = new List<SelectListItem> { selectItem }
					.Concat(res.Where(a => a.IsActive == true).Select(i => new SelectListItem
					{
						Text = i.Subject,
						Value = i.BookId.ToString(),
					}))
					.ToList();

			return selectList;
		}
	}
}
