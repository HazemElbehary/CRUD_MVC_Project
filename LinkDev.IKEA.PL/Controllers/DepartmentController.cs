using AutoMapper;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
	[Authorize]
    public class DepartmentController : Controller
	{
		#region Service

		private readonly IDepartmentService _departmentService;
		private readonly ILogger<DepartmentController> _Logger;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _environment;

		public DepartmentController(
			IDepartmentService departmentService,
			ILogger<DepartmentController> Logger,
			IMapper mapper,
			IWebHostEnvironment webHost)
		{
			_departmentService = departmentService;
			_Logger = Logger;
			_environment = webHost;
			_mapper = mapper;
		}
		#endregion

		#region Action Index
		[HttpGet]
		public async Task<IActionResult> Index(string name)
		{
			var departments = await _departmentService.GetAllAsIQueryableAsync(name);

			if (!string.IsNullOrEmpty(name))
			{
				return PartialView("Partials/DepartmentsRecordsPartial", departments);
			}

			return View(departments);
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
		public async Task<IActionResult> Create(DepartmentEditViewModel DTO)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError(string.Empty, "There Is Error");
				return View(DTO);
			}

			string message = string.Empty;
			try
			{

				var CreatedDepartment = _mapper.Map<DepartmentEditViewModel, CreatedDepartmentDTO>(DTO);

				var Created = await _departmentService.CreateDepartmentAsync(CreatedDepartment) > 0;
				
				if (!Created)
				{
					message = "Department Is Not Created";
					ModelState.AddModelError(string.Empty, message);
					return View(DTO);
				}

			}
			catch (Exception ex)
			{
				// 1. Log Error
				_Logger.LogError(ex, ex.Message);

				// 2. Set Message
				message = _environment.IsDevelopment() ? ex.Message : "An Error Has Occured During Creating The Department :(";

				ViewData["message"] = message;
				return RedirectToAction(nameof(Index));
			}

			TempData["States"] = string.IsNullOrEmpty(ViewBag.message) ? "Department Is Created Successfully :)" : message ;
			return RedirectToAction(nameof(Index));
		}

		#endregion

		#region Action Edit

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null)
				return NotFound();

			var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

			if (department is null)
				return BadRequest();

			return View(_mapper.Map<DepartmentDetailsDTO, DepartmentEditViewModel>(department));
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] int id, DepartmentEditViewModel department)
		{

			if (!ModelState.IsValid)
				return await Edit(id, department);

			var message = string.Empty;

			try
			{
				var UpdatedDepartment = _mapper.Map<DepartmentEditViewModel, UpdatedDepartmentDTO>(department);

				var Updated = await _departmentService.UpdateDepartmentAsync(UpdatedDepartment) > 0;

				if (!Updated)
				{
					message = "An Error Has Occured During Updating The Department :(";
					ModelState.AddModelError(string.Empty, message);
					return View(department);
				}

			}
			catch (Exception ex)
			{
				// 1. Log Error
				_Logger.LogError(ex, ex.Message);


				// 2. Set Message
				message = _environment.IsDevelopment() ? ex.Message : "An Error Has Occured During Updating The Department :(";
			}

			TempData["States"] = string.IsNullOrEmpty(ViewBag.message) ? "Department Is Updated Successfully :)" : message;
			return RedirectToAction(nameof(Index));
		}

		#endregion

		#region Action Details

		[HttpGet]
		public IActionResult Details(int? id)
		{
			if (id is null)
				return BadRequest();

			var department = _departmentService.GetDepartmentByIdAsync(id.Value);

			if (department is null)
				return NotFound();

			return View(department);
		} 
		#endregion

		#region Action Delete
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int? id)
		{

			if (id is null)
				return BadRequest();

			var message = string.Empty;

			try
			{
				var Deleted = await _departmentService.DeleteDepartmentAsync(id.Value);

				if (!Deleted)
				{
					message = "An Error Has Occured During Delete The Department :(";
					ModelState.AddModelError(string.Empty, message);
					return View(id);
				}

			}
			catch (Exception ex)
			{
				// 1. Log Error
				_Logger.LogError(ex, ex.Message);


				// 2. Set Message
				message = _environment.IsDevelopment() ? ex.Message : "An Error Has Occured During Updating The Department :(";
			}

			TempData["States"] = string.IsNullOrEmpty(ViewBag.message) ? "Department Is Deleted Successfully :)" : message;
			return RedirectToAction(nameof(Index));
		} 
		
		#endregion
	}
}
