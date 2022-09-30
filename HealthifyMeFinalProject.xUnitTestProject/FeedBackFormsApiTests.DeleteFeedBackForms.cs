using HealthifyMeFinalProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HealthifyMeFinalProject.xUnitTestProject
{
    public partial class FeedBackFormsApiTests
    {
        [Fact]
        public void DeleteFeedBackForm_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.DeleteFeedBackForm_NotFoundResult);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new FeedBackFormsController(dbContext, logger);
            int findFormID = 900;

            // ACT
            IActionResult actionResultDelete = apiController.DeleteFeedBackForm(findFormID).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultDelete);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultDelete as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteFeedBackForm_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.DeleteFeedBackForm_BadRequestResult);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new FeedBackFormsController(dbContext, logger);
            int? findFormID = null;

            // ACT
            IActionResult actionResultDelete = apiController.DeleteFeedBackForm(findFormID).Result;

            // ASSERT - check if the IActionResult is BadRequest 
            Assert.IsType<BadRequestResult>(actionResultDelete);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultDelete as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteFeedBackForm_OkResult()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.DeleteFeedBackForm_OkResult);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new FeedBackFormsController(dbContext, logger);
            int findFormID = 1;

            // ACT
            IActionResult actionResultDelete = apiController.DeleteFeedBackForm(findFormID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultDelete);

            // ASSERT - if Status Code is HTTP 200 (Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultDelete as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }
    }
}
