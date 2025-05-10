using Dimo.BLL.DTO;
using Dimo.BLL.DTO.DepartmentsDto;
using Dimo.BLL.Services.Interfaces;
using Dimo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dimo.PL.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,ILogger<DepartmentController>_logger,IWebHostEnvironment _environment) : Controller
    {
        //private readonly IDepartmentService _departmentService = departmentService;

        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
        #region Creare Department
        [HttpGet]
		public IActionResult Create()
		{
            return View();
		}
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (ModelState.IsValid) //server side validation
            {
                try
                {
                  int result =  _departmentService.AddDepartment(departmentDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department canot be created !!");
                       // return View(departmentDto);
                    }
                }
                catch (Exception ex)
                {
                    //log exception
                    if(_environment.IsDevelopment())
                    {
                        //1. Development infiron => log Error in Console and return same view with error message
                        ModelState.AddModelError(string.Empty,ex.Message);
                        //return View(departmentDto);
                    }
                    else
                    {
                        //2.Depolyment=>  log error in file | table in database And Return Error View
                        _logger.LogError(ex.Message);
                        //return View(departmentDto);
                    }


                }

            }
            return View(departmentDto);

        }
        #endregion
        #region Details of Department
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(!id.HasValue) return BadRequest(); //400
            //بيكلم ال datails من غير مايكون  باغتلي   Id paramter
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound(); //404
            return View(department);
        }
        #endregion
        #region Edit Department
        [HttpGet]
        public IActionResult Edit(int? id)
        {
           if(!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentEditViewModel()
            {
                Code= department.Code,
                Name= department.Name,
                Description= department.Description,
                DateOfCreation = department.CreatedOn

            };
            return View(departmentViewModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, DepartmentEditViewModel viewModel)
        {
            if(!ModelState.IsValid) return View(viewModel);
            try
            {
                var updatedDepartment = new UpdateDepartmentDto()
                {
                    Id = id.Value,
                    //Id= viewModel.Id,
                    Code = viewModel.Code,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    DateOfCreation = viewModel.DateOfCreation
                };
                int result = _departmentService.UpdateDepartment(updatedDepartment);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department cannot be Created!!");
                }
            }
            catch (Exception ex)
            {
                //log exception
                if (_environment.IsDevelopment())
                {
                    //1. Development infiron => log Error in Console and return same view with error message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    //return View(departmentDto);
                }
                else
                {
                    //2.Depolyment=>  log error in file | table in database And Return Error View
                    _logger.LogError(ex.Message);
                    //return View(departmentDto);
                }
            }
            return View(viewModel);
        }

        #endregion
        #region  Delete Department
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department == null) return NotFound();
        //    return View(department); //هيفتح القايمه بتاعت delete

        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id==0) return BadRequest();
            try
            {
                bool deleted =_departmentService.DeleteDepartment(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));

                else
                {
                    ModelState.AddModelError(string.Empty, "Department is not deleted");
                    //data of department
                    return RedirectToAction(nameof(Delete), new {id} );
                }

            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("Error View");
                }

            }


        }
        #endregion
    }
}
