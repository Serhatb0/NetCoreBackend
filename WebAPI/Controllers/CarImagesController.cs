using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
        IWebHostEnvironment _webHostEnvironment;
        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnvironment)
        {
            _carImageService = carImageService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getimages")]
        public IActionResult GetCarImages(int id)
        {
            var result = _carImageService.GetCarImages(id);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        //[HttpGet("getimagedetails")]
        //public IActionResult GetImageDetails(int id)
        //{
        //    var result = _carImageService.GetCarImagesDetail(id);
        //    if (result.Success == true)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm(Name = ("Image"))] IFormFile file, [FromForm] CarImage carImage)
        {

            var result = _carImageService.Add(file, carImage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm(Name = ("Id"))] int Id)
        {

            var carImage = _carImageService.Get(Id).Data;

            var result = _carImageService.Delete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("Image"))] IFormFile file, [FromForm(Name = ("Id"))] int id)
        {
            var carImage = _carImageService.Get(id).Data;
            var result = _carImageService.Update(file, carImage);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}