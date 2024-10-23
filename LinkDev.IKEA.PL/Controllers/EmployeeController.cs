using AutoMapper;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
	[Authorize]
    public class EmployeeController : Controller
	{
		#region Service

		private readonly IEmployeeService _employeeService;
		private readonly IMapper _mapper;
		private readonly ILogger<EmployeeController> _Logger;
		private readonly IWebHostEnvironment _environment;

		public EmployeeController(
			IEmployeeService employeeService,
			IMapper mapper,
			ILogger<EmployeeController> Logger,
			IWebHostEnvironment webHost)
		{
			_employeeService = employeeService;
			_mapper = mapper;
			_Logger = Logger;
			_environment = webHost;
		}
		
		#endregion

		#region Action Index
		[HttpGet]
		public async Task<IActionResult> Index(string name)
		{
			var Employees = await _employeeService.GetEmployeesAsync(name);

			if (!string.IsNullOrEmpty(name))
			{
				///PartialViewResult PartialView = new PartialViewResult();
				///PartialView.ContentType = "text/html";
				///PartialView.ViewName = "Partials/EmployeesRecordsPartial";
				///
				///var x = new ViewDataDictionary(new EmptyModelMetadataProvider(), ControllerContext.ModelState); ;
				///
				///x.Model = Employees;
				///
				///PartialView.ViewData = x;
				///
				///return PartialView;

				return PartialView("Partials/EmployeesRecordsPartial", Employees);
			}


			return View(Employees);
		} 
		#endregion

		#region Action Create
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateEditViewModel DTO)
		{
			if (!ModelState.IsValid)
				return View(DTO);
			
			var message = string.Empty;
			
			try
			{
				var Emp = _mapper.Map<CreatedEmployeeDTO>(DTO);

				var Result = await _employeeService.CreateEmployeeAsync(Emp);
				if (Result > 0)
				{
					TempData["States"] = "Employe Is Created Successfully :)";
					return RedirectToAction(nameof(Index));
				}
				else
				{
					message = "Employee Is Not Created";
					ModelState.AddModelError(string.Empty, message);
					return View(DTO);
				}
			}
			catch (Exception ex)
			{
				// 1. Log Error
				_Logger.LogError(ex, ex.Message);

				// 2. Set Message
				message = _environment.IsDevelopment() ? ex.Message : "An Error Has Occured During Creating The Employee :(";
			}

			ModelState.AddModelError(string.Empty, message);
			return View(DTO);
		}
		#endregion

		#region Action Details
		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id is null)
				return BadRequest();

			var Employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

			if (Employee is null)
				return NotFound();

			return View(Employee);
		}
		#endregion

		#region Action Edit
		
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null)
				return NotFound();

			var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

			

			if (employee is null)
				return BadRequest();

			var Mapped = _mapper.Map<CreateEditViewModel>(employee);

			return View(Mapped);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] int id, CreateEditViewModel DTO)
		{
			if (!ModelState.IsValid)
				return await Edit(id, DTO);

			var message = string.Empty;

			try
			{
				var Emp = _mapper.Map<UpdatedEmployeeDTO>(DTO);
				
				var Updated = await _employeeService.UpdateEmployeeAsync(Emp) > 0;

				if (Updated)
				{
					TempData["States"] = "Employe Is Updated Successfully :)";
					return RedirectToAction(nameof(Index));
				}

				message = "An Error Has Occured During Updating The Employee :(";
			}
			catch (Exception ex)
			{
				// 1. Log Error
				_Logger.LogError(ex, ex.Message);


				// 2. Set Message
				message = _environment.IsDevelopment() ? ex.Message : "An Error Has Occured During Updating The Employee :(";
			}

			ModelState.AddModelError(string.Empty, message);
			return View(DTO);
		}
		
		#endregion
		
		#region Delete

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int? id)
		{

			if (id is null)
				return BadRequest();

			var message = string.Empty;

			try
			{
				var result = await _employeeService.DeleteEmployeeAsync(id.Value);

				if (result)
				{
					TempData["States"] = "Employe Is Deleted Successfully :)";
					return RedirectToAction(nameof(Index));
				}

				message = "An Error Has Occured During Updating The Employee :(";
			}
			catch (Exception ex)
			{
				// 1. Log Error
				_Logger.LogError(ex, ex.Message);


				// 2. Set Message
				message = _environment.IsDevelopment() ? ex.Message : "An Error Has Occured During Updating The Employee :(";
			}

			TempData["States"] = "Employe Is NOt Delete, An Error occurred :(";
			return RedirectToAction(nameof(Index));
		} 
		
		#endregion
	}
}