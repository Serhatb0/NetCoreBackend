﻿using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        ICarService _carService;

        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }

        //[ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImage entity)
        {
            IResult result = BusinessRules.Run(CheckCarImages(entity.CarId), CheckCarId(entity.CarId), CheckIfImageExtension(file.FileName));
            if (result != null)
            {
                return result;
            }
            entity.ImagePath = FileHelper.AddAsnyc(file);
            entity.Date = DateTime.Today;
            _carImageDal.Add(entity);
            return new SuccessResult("Resim Eklendi");
        }



        public IResult Delete(CarImage entity)
        {
            var delete = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwwroot\\Images")) + _carImageDal.Get(c => c.Id == entity.Id).ImagePath;
            IResult result = BusinessRules.Run(FileHelper.DeleteAsync(delete));
            _carImageDal.Delete(entity);
            return new SuccessResult("Resim Silindi");
        }

        public IDataResult<List<CarImage>> GetAll()
        {

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }
        public IDataResult<CarImage> Get(int id)
        {
            IResult result = BusinessRules.Run(CheckImageNull(id));

            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.Id == id));
        }

        public IDataResult<List<CarImage>> GetCarImages(int id)
        {
            IResult result = BusinessRules.Run(CheckImageNull(id));

            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(result.Message);
            }

            return new SuccessDataResult<List<CarImage>>(CheckImageNull(id).Data);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile file, CarImage entity)
        {
            var update = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwwroot")) + _carImageDal.Get(c => c.Id == entity.Id).ImagePath;
            entity.ImagePath = FileHelper.UpdateAsync(update, file);
            entity.Date = DateTime.Today;
            _carImageDal.Update(entity);
            return new SuccessResult("Resim Güncellendi");
        }

        //[CacheAspect]
        //public IDataResult<List<CarImageDto>> GetCarImagesDetail(int id)
        //{
        //    return new SuccessDataResult<List<CarImageDto>>(_carImageDal.GetCarImagesDetail(p => p.CarId == id), Messages.Listed);
        //}

        private IResult CheckCarImages(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id).Count;
            if (result >= 5)
            {
                return new ErrorResult("5 Ten Fazla Resim Olamaz");
            }
            return new SuccessResult();
        }

        private IResult CheckCarId(int id)
        {
            var result = _carService.GetById(id);
            if (!result.Data.Any())
            {
                return new ErrorResult("Araba id si Lazım");
            }
            return new SuccessResult();
        }
        private IResult CheckIfImageExtension(string file)
        {

            string[] infos = { "jpg", "png", "png", "jfif" };
            string[] parts;
            parts = file.Split('.');
            foreach (var info in infos)
            {
                Console.WriteLine(parts.Last());
                if (parts.Last() == info)
                {
                    return new SuccessResult();
                }
            }
            return new ErrorResult("Dosyanın Uzantısı Desteklenmiyor");

        }

        private IDataResult<List<CarImage>> CheckImageNull(int id)
        {
            string path = @"\Images\default.jpg";

            try
            {

                var result = _carImageDal.GetAll(c => c.CarId == id).Any();
                if (!result)
                {
                    List<CarImage> carImage = new List<CarImage>();
                    carImage.Add(new CarImage { CarId = id, ImagePath = path, Date = DateTime.Today });
                    return new SuccessDataResult<List<CarImage>>(carImage);
                }
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<CarImage>>(ex.Message);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == id).ToList());
        }


    }
}