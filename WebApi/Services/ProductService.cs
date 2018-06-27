﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Sync

        #region Return Something
        public int Count()
        {
            return _unitOfWork.Products.Count();
        }

        public Product Get(int id)
        {
            return _unitOfWork.Products.Get(id);
        }

        public List<Product> GetAll()
        {
            return _unitOfWork.Products.GetAll();
        }

        public List<Product> GetLatest(int qt)
        {
            return _unitOfWork.Products.GetAll();
        }

        public List<Product> GetAllIsUseTrue()
        {
            return _unitOfWork.Products.Find(p => p.IsUse == true).ToList();
        }
        #endregion

        #region Change Something
        public void Add(Product obj)
        {
            _unitOfWork.Products.Add(obj);
            _unitOfWork.Save();
        }
        #endregion

        #endregion

        #region Async

        #region Return
        public async Task<Product> GetAsync(int id)
        {
            return await _unitOfWork.Products.GetAsync(id);
        }
        #endregion

        #region Change
        #endregion


        #endregion

    }
}
