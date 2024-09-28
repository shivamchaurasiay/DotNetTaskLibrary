using DotNetsTask.Data.Models;
using DotNetTask.Dto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetstask.Service
{
    public interface IBookService
    {
		List<Book> GetAllData();
		Book GetById(int BookId);
        void Save(Book entity);
        //bool IsAdsClientNameExists(string adsClientName);
        bool Delete(int id);
        void Update(Book entity);
        List<Book> GetBookFilterList(FilterBookDto model);
    }
}
