using AutoMapper;
using DotNetstask.Service;
using DotNetsTask.Data.Models;
using DotNetsTask.Service.BookServices;
using DotNetsTask.Service.DropDown;
using DotNetTask.Dto.ViewModel;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace DotNetsTask.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class LibraryController : BaseController
	{
		private readonly IDropdownHelper _dropdownHelper;
		private readonly IBookService _BookService;
		private readonly IBookIssueService _BookIssueService;
		private readonly IMapper _mapper;
		private readonly IToastNotification _toastNotification;
		public LibraryController(IBookService BookService, IToastNotification toastNotification, IMapper mapper, IDropdownHelper dropdownHelper, IBookIssueService bookIssueService)
		{
			_dropdownHelper = dropdownHelper;
			_BookService = BookService;
			_toastNotification = toastNotification;
			_mapper = mapper;
			_BookIssueService = bookIssueService;

		}
		
		[HttpGet]
		public IActionResult AddBook(int? Id)
		{
			BookDto model;
			if (Id.HasValue)
			{
				var entity = _BookService.GetById(Id.Value) ?? new Book();
				model = _mapper.Map<BookDto>(entity);
			}
			else
			{
				model = new BookDto();
			}

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> AddBook(BookDto model)
		{
			try
			{
				if (model == null)
				{
					return View(model);
				}
				ModelState.Remove("BookId");
				if (!ModelState.IsValid)
				{
					return View(model);
				}

				var data = _mapper.Map<Book>(model);
				data.IsActive = true;
				data.IsDeleted = false;
				data.CreatedOn = DateTime.Now;
				data.UpdatedOn = DateTime.Now;
				data.UpdatedBy = 1;

				if (model.BookId == 0)
				{
					_BookService.Save(data);
					_toastNotification.AddSuccessToastMessage("Data Saved Successfully");
				}
				else
				{
					_BookService.Update(data);
					_toastNotification.AddSuccessToastMessage("Data Updated Successfully");
				}

				return RedirectToAction("BookList");
			}
			catch (Exception ex)
			{

				_toastNotification.AddErrorToastMessage("Something went wrong");
				return View(model);
			}
		}
		[HttpGet]
		public async Task<IActionResult> DeleteBooks(int id)
		{
			var data = _BookService.Delete(id);
			if (data == true)
			{

				_toastNotification.AddSuccessToastMessage("Data has been deleted successfully");
				return RedirectToAction("BookList");
			}
			else
			{

				_toastNotification.AddErrorToastMessage("Something went wrong");
				return RedirectToAction("BookList", id);
			}
		}

		//public IActionResult IssueBook()
		//{
		//	return View();
		//}

		[HttpGet]
		public async Task<IActionResult> IssueBook(int? id)
		{
			// Create a new instance of IssuedBookDto
			var issueBookDto = new IssuedBookDto();

			if (id.HasValue)
			{
				// Optional: Logic to fetch existing book details or related data based on the id
				// issueBookDto = await _yourService.GetBookDetailsById(id.Value);
			}

			
			ViewBag.Books = await _dropdownHelper.GetAllBooks();
			ViewBag.Students = await _dropdownHelper.GetAllStudents();

			return View(issueBookDto);
		}
		[HttpPost]
		public async Task<IActionResult> IssueBook(IssuedBookDto model)
		{
			try
			{
				if (model == null)
				{
					return View(model);
				}
				ModelState.Remove("Book");
				ModelState.Remove("Users");
				ModelState.Remove("User");
				ModelState.Remove("MobileNo");
				ModelState.Remove("Books");
				ModelState.Remove("Students");
				ModelState.Remove("ExpectedReturnDate");
				ModelState.Remove("IssueDate");
				if (!ModelState.IsValid)
				{
					return View(model);
				}

				var data = _mapper.Map<IssuedBook>(model);
				data.IsActive = true;
				data.Status = 0;
				data.IsDeleted = false;
				data.CreatedOn = DateTime.Now;
				data.UpdatedOn = DateTime.Now;
				data.UpdatedBy = 1;

				if (model.IssueId == 0)
				{
					_BookIssueService.Save(data);
					_toastNotification.AddSuccessToastMessage("Data Saved Successfully");
				}
				else
				{
					_BookIssueService.Update(data);
					_toastNotification.AddSuccessToastMessage("Data Updated Successfully");
				}

				return RedirectToAction("IssueBook");
			}
			catch (Exception ex)
			{

				_toastNotification.AddErrorToastMessage("Something went wrong");
				return View(model);
			}
		}

        [HttpGet]
        public async Task<IActionResult> ReturnBook(int? id)
        {
            // Create a new instance of IssuedBookDto
            var ReturnDto = new ReturnDto();

            if (id.HasValue)
            {
                // Optional: Logic to fetch existing book details or related data based on the id
                // issueBookDto = await _yourService.GetBookDetailsById(id.Value);
            }


            ViewBag.Books = await _dropdownHelper.GetAllBooks();
            ViewBag.Students = await _dropdownHelper.GetAllStudents();

            return View(ReturnDto);
        }


        [HttpPost]
        public async Task<IActionResult> ReturnBook(ReturnDto model)
        {
            try
            {
                if (model == null)
                {
					return View(model);
				}
				ModelState.Remove("Book");
				ModelState.Remove("Users");
				ModelState.Remove("User");
				ModelState.Remove("MobileNo");
				ModelState.Remove("Books");
				ModelState.Remove("Students");
				ModelState.Remove("ExpectedReturnDate");
				ModelState.Remove("IssueDate");
				ModelState.Remove("ReturnDate");
				if (!ModelState.IsValid)
                {
					return View(model);
				}

                var data = _mapper.Map<IssuedBook>(model);
               

                 _BookIssueService.UpdateData(data); // Make sure this is awaited

                _toastNotification.AddSuccessToastMessage("Data saved successfully");

				return RedirectToAction("ReturnBook");
			}
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

		public IActionResult GetBookByStudentId(int StudentId)
		{
			var branches = _dropdownHelper.GetIssueBookByStudentId(StudentId);
			return Json(branches);
		}




		[HttpGet]
		public IActionResult BookList()
		{
			BookDto BookDto = new BookDto();

			var data = _BookService.GetAllData();

			BookDto.BookList = _mapper.Map<IReadOnlyList<BookDto>>(data);

			return View(BookDto);
		}

		[HttpGet]
		public IActionResult IssuedBookList()
		{
			IssuedBookDto IssuedBookDto = new IssuedBookDto();

			var data = _BookIssueService.GetAllIssuedData();

			IssuedBookDto.IssuedBookList = _mapper.Map<IReadOnlyList<IssuedBookDto>>(data);

			return View(IssuedBookDto);
		}
		[HttpGet]
		public IActionResult ReturnBookList()
		{
			IssuedBookDto IssuedBookDto = new IssuedBookDto();

			var data = _BookIssueService.GetAllReturnData();

			IssuedBookDto.ReturnBookList = _mapper.Map<IReadOnlyList<IssuedBookDto>>(data);

			return View(IssuedBookDto);
		}
		public IActionResult SearchBook()
		{
            FilterBookDto BookDto = new FilterBookDto();

            var data = _BookService.GetAllData();

            BookDto.LstBookFilterData = _mapper.Map<IReadOnlyList<FilterBookDto>>(data);

            return View(BookDto);
        }


        [HttpPost]
        public async Task<IActionResult> SearchBook(FilterBookDto model)
        {
            var resultModel = new FilterBookDto();
			ModelState.Remove("BookId");
            if (!ModelState.IsValid)
            {
                return View(resultModel);
            }

            try
            {
                var bookDetails =  _BookService.GetBookFilterList(model); 
                if (bookDetails == null || !bookDetails.Any()) 
                {
                    _toastNotification.AddErrorToastMessage("No books found matching the criteria.");
                    return View(resultModel);
                }

                // Assuming bookDetails is a list or collection
                resultModel.LstBookFilterData = _mapper.Map<IReadOnlyList<FilterBookDto>>(bookDetails);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("An error occurred while processing your request.");
            }

            return View(resultModel);
        }







    }
}
