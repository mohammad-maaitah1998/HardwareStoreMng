using HardwareStoreMng.Controllers;
using HardwareStoreMng.DTO;
using HardwareStoreMng.Models;
using HardwareStoreMng.Repositories.PorductRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestHardwareStore
{
    public class TestIndexController
    {
        private IndexController _indexController;
        private readonly ProductInterface _productInterface;
        public TestIndexController(IndexController indexController, ProductInterface productInterface) { 
            _indexController = indexController;
            _productInterface = productInterface;
        }
        [Theory]
        [InlineData(5)]
        public void GetProductById(int productId) {
             
           var res= _indexController.GettProductById(productId);
            Assert.IsType<IActionResult>(res);
        }
        [Theory]
        [InlineData(5)]
        public void GetEmCustomerByID(int customerid) {
            var res = _indexController.GettCustomerById(customerid);
            Assert.IsType<IActionResult>(res);
        }

        [Theory]
        [InlineData(5)]
        public void GetEmployeeById(int employeeID) {
            var res = _indexController.GettEmployeeById(employeeID);
            Assert.IsType<IActionResult>(res);
        }
        [Theory]
        [InlineData(5)]
        public void GetInvoiceById(int invoiceID) {
            var res = _indexController.GettInvoiceById(invoiceID);
            Assert.IsType<IActionResult>(res);
        }
        [Theory]
        [InlineData(5)]
        public void GetInvoiceItemById(int InoiceItemId) {
            var res = _indexController.GettInvoiceItemById(InoiceItemId);
            Assert.IsType<IActionResult>(res);
        }
        [Theory]
        [InlineData(5)]
        public void GetBarCodeById(int BarCodeId) {
            var res = _indexController.GettBarcodesId(BarCodeId);
            Assert.IsType<IActionResult>(res);
        }
    }
    }
    
