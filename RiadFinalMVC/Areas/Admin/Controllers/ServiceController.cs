using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RiadFinalMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        public IActionResult Index() 
        {
            var exist = _service.GetAllService();
            return View(exist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _service.AddServiceAsync(service);
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ImageTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var old = _service.GetService(x=>x.Id == id);
            if(old == null) return NotFound();
            return View(old);
        }

        [HttpPost]

        public IActionResult Update(Service service)
        {
            if(!ModelState.IsValid)
                return View();

            try
            {
                _service.UpdateService(service.Id, service);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ImageTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existBlog = _service.GetService(x => x.Id == id);
            if (existBlog == null) return NotFound();
            return View(existBlog);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {

            try
            {
                _service.DeleteService(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
