using DotNetsTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetsTask.Service.BookServices
{
	public interface IBookIssueService
	{
		List<IssuedBook> GetAllData();
		List<IssuedBook> GetAllIssuedData();
		List<IssuedBook> GetAllReturnData();
		IssuedBook GetById(int IssueBookId);
		void Save(IssuedBook entity);
		//bool IsAdsClientNameExists(string adsClientName);
		bool Delete(int id);
		void Update(IssuedBook entity);
        bool UpdateData(IssuedBook entity);
	}
}
