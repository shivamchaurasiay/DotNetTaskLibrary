using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetsTask.Service.DashboardServices
{
	public interface IDashboardCountServices
	{
		int TotalStudent();
		int TotalPerIssueBook();
		int TotalIssueBook();
		int TotalAddBook();
		int TitalreturnBook();
	
	}
}
