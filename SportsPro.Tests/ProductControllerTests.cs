//Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
//Workshop 5
//Date May 28, 2021

using Moq;
using System;
using Xunit;
using SportsPro.Models;
using SportsPro.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Tests
{
    public class ProductControllerTests
    {

        [Fact]
        public void Add_GET_ReturnsAViewResult()
        {
            var rep = new Mock<IRepository<Product>>();
            var controller = new ProductController(rep.Object);

            // act            
            var result = controller.Add();

            // assert
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void Add_GET_ValueOfViewBagActionPropertyIsAdd()
        {
            var rep = new Mock<IRepository<Product>>();
            var action = "Add";
            var controller = new ProductController(rep.Object);

            // act
            var result = controller.Add().ViewData["Action"];

            // assert
            Assert.Same(action, result);
        }


        [Fact]
        public void Edit_GET_ValueOfViewBagActionPropertyIsEdit()
        {
            var rep = new Mock<IRepository<Product>>();
            var action = "Edit";
            var controller = new ProductController(rep.Object);

            // act
            var result = controller.Edit(1).ViewData["Action"];

            // assert
            Assert.Same(action, result);
        }



        [Fact]
        public void Delete_GET_ReturnsAViewResult()
        {
            var rep = new Mock<IRepository<Product>>();
            var controller = new ProductController(rep.Object);

            // act
            int idparam = 1;
            var result = controller.Delete(idparam);

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_GET_ModelIsAProductObject()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var controller = new ProductController(rep.Object);

            // act
            var model = controller.Delete(1).ViewData.Model as Product;

            // assert
            Assert.IsType<Product>(model);
        }
    }
}
