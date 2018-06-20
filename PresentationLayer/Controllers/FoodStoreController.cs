using System;
using System.Linq;
using ApplicationLayer.Services;
using ApplicationLayer.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    public class FoodStoreController : Controller
    {
        private readonly IFoodStoreApplicationService _service;
        private readonly IMapper _mapper;

        public FoodStoreController(IFoodStoreApplicationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("Index")]
        public ActionResult Index()
        {
            var result = _service.GetAll().Select(s => _mapper.Map<FoodStoreVM>(s));
            return Json(result.Where(q=>!q.IsDeleted));
        }

     
        [HttpGet("Get/{id}")]
        public ActionResult Details(Guid id)
        {
            var result = _service.Get(id);
            return Json(_mapper.Map<FoodStoreVM>(result));
        }


        [HttpPost("Create")]
        public ActionResult Create([FromBody]FoodStoreVM item)
        {
            try
            {
                _service.Create(item);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("edit")]
        public ActionResult Edit([FromBody]FoodStoreVM item)
        {
            try
            {
                _service.Update(item);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("delete/{id}")]
        public ActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}