using DotNetsTask.Data.Models;
using DotNetsTask.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetsTask.Service.DashboardServices
{
	public class DashboardCountServices: IDashboardCountServices
	{

		private readonly IRepository<User> _User;
		private readonly IRepository<Book> _Book;
		private readonly IRepository<IssuedBook> _IssuedBook;
		
		
		public DashboardCountServices(IRepository<IssuedBook> IssuedBook, IRepository<Book> Book, IRepository<User> User)
		{
			_User = User;
			_Book = Book;
			_IssuedBook = IssuedBook;
			
		
		}

		public int TotalStudent()
		{
			var totalStudents = _User?.Query()
				?.Get()
				?.Count(a => a.IsActive==true) ?? 0;

			return totalStudents;
		}
		public int TotalAddBook()
		{
			var totalBook = _Book?.Query()
				?.Get()
				?.Count(a => a.IsActive == true) ?? 0;

			return totalBook;
		}
		public int TotalIssueBook()
		{
			var totalBook = _IssuedBook?.Query()
				?.Get()
				?.Count(a => a.IsActive == true) ?? 0;

			return totalBook;
		}
		public int TotalPerIssueBook()
		{
			var totalBook = _IssuedBook?.Query()
				?.Get()
				?.Count(a => a.IsActive == true && a.Status == 0) ?? 0;

			return totalBook;
		}
		public int TitalreturnBook()
		{
			var totalBook = _IssuedBook?.Query()
				?.Get()
				?.Count(a => a.IsActive == true && a.Status == 1) ?? 0;

			return totalBook;
		}

	}
}
