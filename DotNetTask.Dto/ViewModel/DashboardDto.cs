using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTask.Dto.ViewModel
{
     public class DashboardDto
    {
		public int? TotaslStudentCount { get; set; }
		public int? TotalAddBookCount { get; set; }

		public int? TotalIssueBookCount { get; set; }
		public int? TotalPersentIssueBookCount { get; set; }

		public int? TotalReturnBookCount { get; set; }

	}
}
