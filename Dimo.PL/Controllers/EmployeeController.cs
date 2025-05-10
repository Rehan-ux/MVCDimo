using Dimo.BLL.DTO.EmployeesDto;
using Dimo.BLL.Services.AttatchMentServices;
using Dimo.BLL.Services.Interfaces;
using Dimo.DAL.Models.EmployeeModels;
using Dimo.PL.ViewModels;
using Dimo.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dimo.PL.Controllers
{
	public class EmployeeController(IEmployeeService _employeeService,IDepartmentService departmentService,
		 ILogger<EmployeeController> _logger,IWebHostEnvironment _environment, IAttachmentService attachmentService) : Controller
	{
		public IActionResult Index(string? EmployeeSearchName)
		{
			//TempData.Keep();
			var Employees = _employeeService.GetAllEmployees();
			//Binding through views dictionary : transfar data from action to view
			//1- view data 
			//ViewData["Message"] = "Hello ViewDate ";
			//2- view bag
			//ViewBag.Message = "Hello ViewBag";
			//dynamic Employees = null;
			if (string.IsNullOrEmpty(EmployeeSearchName))
			{
			     Employees = _employeeService.GetAllEmployees();

            }
            else
			{
				 Employees = _employeeService.SearchEmployeeByName(EmployeeSearchName);

            }
            return View(Employees);
		}
		#region Create Employee
		[HttpGet]
		public IActionResult Create(/*[FromServices]IDepartmentService _departmentService*/)
		{
			//ViewData["Departments"] = _departmentService.GetAllDepartments();
			return View();
		} 
		//هنا لاان الاكشن سطر واحد عوضت عنه ب =>   وشلت كمان return 

		[HttpPost]
        public IActionResult Create(EmployeeViewModel employeesDto)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var employeeCreatedDto = new CreatedEmployeeDto()
					{
						Name = employeesDto.Name,
						Address = employeesDto.Address,
						Age = employeesDto.Age,
						IsActive = employeesDto.IsActive,
						Email = employeesDto.Email,
						EmployeeType = employeesDto.EmployeeType,
						PhoneNumber = employeesDto.PhoneNumber,
						Salary = employeesDto.Salary,
						HiringDate = employeesDto.HiringDate,
						Gender = employeesDto.Gender,
						DepartmentId = employeesDto.DepartmentId,
						Image = employeesDto.Image,	
					};
					int result = _employeeService.CreateEmployee(employeeCreatedDto);
					//3- Temp data 

					if (result > 0)
					{
						TempData["Message"] = "Employee Created Successfuly";
						return RedirectToAction(nameof(Index));
					}
					//Save Change


					else
					{
                        TempData["Message"] = "Employee Creation failed";

                        ModelState.AddModelError(string.Empty, "Employee cannot be created!!");
                        return RedirectToAction(nameof(Index));

                    }
                }
				catch (Exception ex)
				{
					if (_environment.IsDevelopment())
					{
						ModelState.AddModelError(string.Empty ,ex.Message);
					}
					else
					{
						_logger.LogError(ex.Message);
					}

				}
			}
			return View(employeesDto);

		}
		#endregion
		#region Details of Employee
		[HttpGet]
		public IActionResult Details(int? id )
		{
			if (!id.HasValue) return BadRequest();
			var employee = _employeeService.GetEmployeeById(id.Value);
			if (employee is null) return NotFound();
			return View(employee);
		}
		#endregion
		#region Edit Employee
		[HttpGet]
		public IActionResult Edit(int? id/*,[FromServices] IDepartmentService _departmentService*/)
		{
            if (!id.HasValue) return BadRequest();
			var employee = _employeeService.GetEmployeeById(id.Value);
			if (employee is null) return NotFound();
			var employeeDto = new EmployeeViewModel()
			{
				//Id = employee.Id,
				Name = employee.Name,
				Address = employee.Address,
				Age = employee.Age,
				Email = employee.Email,
				PhoneNumber = employee.PhoneNumber,
				IsActive = employee.IsActive,
				HiringDate = employee.HiringDate,
				Gender = Enum.Parse<Gender>(employee.Gender),
				EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
			};
            //ViewData["Departments"] = _departmentService.GetAllDepartments();
            return View(employeeDto);
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Edit([FromRoute] int? id, EmployeeViewModel viewModel)
		{
			if (!ModelState.IsValid) return View(viewModel);
			try
			{
                //var UpdateEmployee = new UpdatedEmployeeDto()
                //{
                //	Id = id.Value,
                //	Name = viewModel.Name,
                //	Address = viewModel.Address,
                //	Age = viewModel.Age,
                //	Email = viewModel.Email,
                //	PhoneNumber = viewModel.PhoneNumber,
                //	IsActive = viewModel.IsActive,
                //	HiringDate = viewModel.HiringDate,

                //};
                var employeeUpdatedDto = new UpdatedEmployeeDto()
                {
					Id=id.Value,
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    Age = viewModel.Age,
                    IsActive = viewModel.IsActive,
                    Email = viewModel.Email,
                    EmployeeType = viewModel.EmployeeType,
                    PhoneNumber = viewModel.PhoneNumber,
                    Salary = viewModel.Salary,
                    HiringDate = viewModel.HiringDate,
                    Gender = viewModel.Gender,
                };
                int result = _employeeService.UpdateEmployee(employeeUpdatedDto);
				if (result > 0)
					return RedirectToAction(nameof(Index));
				else
				{
					ModelState.AddModelError(string.Empty, "Employee cannot be Updated!!");
				}

			}
			catch (Exception ex)
			{
				if (_environment.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
				else
				{
					_logger.LogError(ex.Message);
				}

			}
			return View(viewModel);
		}
		#endregion
		#region Delete Employee
		[HttpPost]
		public IActionResult Delete(int id)
		{
			if (id == 0) return BadRequest();
			try
			{
				var deleted = _employeeService.DeleteEmployee(id);
				if (deleted)
				{
                    //attachmentService.Delete()
                    return RedirectToAction(nameof(Index));
                }
					
				else
				{
					ModelState.AddModelError(string.Empty, "Employee is not Deleted");
					return RedirectToAction(nameof(Index));
				}
			}
			catch(Exception ex)
			{
				if (_environment.IsDevelopment())
				{
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
				else
				{
					_logger.LogError(ex.Message);
				}

			}
			return RedirectToAction(nameof(Index));
			

		}
        #endregion
    }
}
