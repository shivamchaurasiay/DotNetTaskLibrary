using DotNetsTask.Data.Models;
using DotNetsTask.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetsTask.Service.BookServices
{
	public class BookIssueService : IBookIssueService
	{
		private readonly IRepository<IssuedBook> _repoIssuedBook;

		public BookIssueService(IRepository<IssuedBook> repoIssuedBook)
		{
			_repoIssuedBook = repoIssuedBook;
		}
		public bool Delete(int id)
		{
			var entity = _repoIssuedBook.FindById(id);
			if (entity != null)
			{
				entity.IsDeleted = true;
			}
			_repoIssuedBook.Update(entity);
			return true;
		}

		public List<IssuedBook> GetAllData()
		{
			return _repoIssuedBook.Query().Include(s => s.Book).Include(s => s.User).Get().Where(s => s.IsDeleted == false && s.IsActive == true).OrderByDescending(a => a.CreatedOn).ToList();
		}

		public List<IssuedBook> GetAllIssuedData()
		{
			return _repoIssuedBook.Query().Include(s=>s.Book).Include(s => s.User).Get().Where(s => s.IsDeleted == false && s.IsActive == true && s.Status==0).OrderByDescending(a => a.CreatedOn).ToList();
		}
		public List<IssuedBook> GetAllReturnData()
		{
			return _repoIssuedBook.Query().Include(s => s.Book).Include(s => s.User).Get().Where(s => s.IsDeleted == false && s.IsActive == true && s.Status == 1).OrderByDescending(a => a.CreatedOn).ToList();
		}
		public IssuedBook GetById(int IssuedBookId)
		{
			return _repoIssuedBook.Query().AsTracking().Filter(ads => ads.IssueId == IssuedBookId).Get().FirstOrDefault();
		}

		//public bool IsAdsClientNameExists(string adsClientName)
		//{
		//    return _repoIssuedBook.Query().Filter(x => x.Subject.ToLower().Equals(adsClientName.ToLower())).Get().FirstOrDefault() != null;
		//}

		public void Save(IssuedBook entity)
		{
			_repoIssuedBook.Insert(entity);
		}

		public void Update(IssuedBook entity)
		{
			_repoIssuedBook.Update(entity);
		}

       
        public bool UpdateData(IssuedBook entity)
        {
            var data = _repoIssuedBook.FindById(entity.IssueId);
            if (data != null)
            {
                data.ReturnDate = entity.ReturnDate;
                data.Status =1;
            }
            _repoIssuedBook.Update(data);
            return true;
        }


    }
}
