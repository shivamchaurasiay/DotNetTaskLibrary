using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DotNetTask.Dto.ViewModel;
using AutoMapper;
using DotNetsTask.Service.DashboardServices;

namespace DotNetsTask.Areas.Admin.Controllers

{
	[Area("Admin")]
	public class DashboardController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly IDashboardCountServices _dashboardService;
		public DashboardController(IDashboardCountServices dashboardService, IMapper mapper)
		{
			_dashboardService = dashboardService;
			_mapper = mapper;
		}
		public async Task<IActionResult> Index(DashboardDto model)
		{
			var studentcount = _dashboardService.TotalStudent();
			var addbookcount = _dashboardService.TotalAddBook();
			var isuuPerbookcount = _dashboardService.TotalPerIssueBook();
			var isuubookcount = _dashboardService.TotalIssueBook();
			var returnbookcount = _dashboardService.TitalreturnBook();
			

			if (model != null)
			{

				model.TotaslStudentCount = studentcount;
				model.TotalAddBookCount = addbookcount;
				model.TotalPersentIssueBookCount = isuuPerbookcount;
				model.TotalIssueBookCount = isuubookcount;
				model.TotalReturnBookCount = returnbookcount;
				
			}


			return View(model);
		}




	}
}
