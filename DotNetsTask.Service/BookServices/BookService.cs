
using DotNetsTask.Data.Models;
using DotNetsTask.Repo;
using DotNetTask.Dto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetstask.Service
{
    public class BookService : IBookService
	{
		private readonly IRepository<Book> _repoBook;

        public BookService(IRepository<Book> repoBook)
        {
			_repoBook = repoBook;
        }
        public bool Delete(int id)
        {
            var entity = _repoBook.FindById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
            }
            _repoBook.Update(entity);
            return true;
        }

        public List<Book> GetBookFilterList(FilterBookDto model)
        {
            return _repoBook.Query().Filter(s => s.IsDeleted == false && s.IsActive == true && (s.Subject == model.Subject || s.Title == model.Title || s.Author == model.Author
            || s.Genre == model.Genre || s.Isbn==model.Isbn))
                    .Get()
                    .ToList();
        }

        public List<Book> GetAllData()
        {
            return _repoBook.Query().Get().Where(s => s.IsDeleted == false && s.IsActive == true).OrderByDescending(a => a.CreatedOn).ToList();
        }

        public Book GetById(int BookId)
        {
            return _repoBook.Query().AsTracking().Filter(ads => ads.BookId == BookId).Get().FirstOrDefault();
        }

        //public bool IsAdsClientNameExists(string adsClientName)
        //{
        //    return _repoBook.Query().Filter(x => x.Subject.ToLower().Equals(adsClientName.ToLower())).Get().FirstOrDefault() != null;
        //}

        public void Save(Book entity)
        {
            _repoBook.Insert(entity);
        }

        public void Update(Book entity)
        {
            _repoBook.Update(entity);
        }
    }
}
