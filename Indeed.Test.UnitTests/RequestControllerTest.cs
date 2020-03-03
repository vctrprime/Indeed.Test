using Indeed.Test.DataAccess.Repositories;
using Indeed.Test.Factories;
using Indeed.Test.Models.Requests;
using Indeed.Test.UnitTests.Repositories;
using Indeed.Test.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Indeed.Test.UnitTests
{
    public class RequestControllerTest
    {
        RequestController _controller;
        IRepository<Request> _repository;

        public RequestControllerTest()
        {
            _repository = new RequestRepositoryTest();
            _controller = new RequestController(factory: new BaseFactory(), repository: _repository);
        }

        #region GetAll
        [Fact]
        public void GetAllRequestsReturnsOkResult()
        {
            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult);
        }
        [Fact]
        public void GetAllRequests()
        {
            var okResult = _controller.Get() as OkObjectResult;
            var items = Assert.IsType<List<Request>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
        #endregion

        #region Get
        [Fact]
        public void GetReturnsBadResult()
        {
            var badResult = _controller.Get(65);
            Assert.IsType<BadRequestObjectResult>(badResult);
        }
        [Fact]
        public void GetReturnsOkResult()
        {
            var okResult = _controller.Get(1);
            Assert.IsType<OkObjectResult>(okResult);
        }
        [Fact]
        public void GetReturnsRightItem()
        {
            var okResult = _controller.Get(1) as OkObjectResult;
            Assert.IsType<Request>(okResult.Value);
            Assert.Equal(1, (okResult.Value as Request).Id);
        }
        #endregion

        #region Create
        [Fact]
        public void CreateReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Name", "TestError");
            var badResult = _controller.Create(new Request() { });
            Assert.IsType<BadRequestObjectResult>(badResult.Result);
        }
        [Fact]
        public void CreateReturnsOkResult()
        {
            var okResult = _controller.Create(new Request() { });
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
        [Fact]
        public void CreateReturnedResponseHasCreatedItem()
        {
            var testRequest = new Request()
            {
                Name = "Test"
            };
            var createdResponse = _controller.Create(testRequest).Result as OkObjectResult;
            var request = createdResponse.Value as Request;
            Assert.IsType<Request>(request);
            Assert.Equal("Test", request.Name);
            Assert.Equal("unknown", request.CreatedBy);
        }
        #endregion

        #region Delete
        [Fact]
        public void DeleteReturnsBadRequest()
        {
            var badResult = _controller.Delete(1);
            Assert.IsType<BadRequestObjectResult>(badResult.Result);
        }
        [Fact]
        public void DeleteReturnsOkResult()
        {
            var okResult = _controller.Delete(3);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
        [Fact]
        public void DeleteRemovesOneItem()
        {
            var okResult = _controller.Delete(3);
            var allItemsResult = _controller.Get() as OkObjectResult;
            var items = Assert.IsType<List<Request>>(allItemsResult.Value);
            Assert.Equal(2, items.Count);
        }
        #endregion

        
    }
}
