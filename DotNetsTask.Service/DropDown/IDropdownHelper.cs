using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetsTask.Service.DropDown
{
	public interface IDropdownHelper
	{
		Task<List<SelectListItem>> GetAllStudents();
		Task<List<SelectListItem>> GetAllBooks();

		List<SelectListItem> GetIssueBookByStudentId(int StudentId);
	}
}
