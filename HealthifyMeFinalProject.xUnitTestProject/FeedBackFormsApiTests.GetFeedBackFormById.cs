using FluentAssertions;
using HealthifyMeFinalProject.Controllers;
using HealthifyMeFinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HealthifyMeFinalProject.xUnitTestProject
{
    public partial class FeedBackFormsApiTests
    {
        [Fact]
        public void GetFeedBackFormById_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.GetFeedBackFormById_NotFoundResult);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();

            // using (var db = DbContextMocker.GetApplicationDbContext(dbName))
            // {
            // }           // db.Dispose(); implicitly called when you exit the USING Block

            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new FeedBackFormsController(dbContext, logger);
            int findFormID = 900;

            // ACT
            IActionResult actionResultGet = apiController.GetFeedBackForm(findFormID).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultGet as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetFeedBackFormById_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.GetFeedBackFormById_BadRequestResult);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new FeedBackFormsController(dbContext, logger);
            int? findFormID = null;

            // ACT
            IActionResult actionResultGet = controller.GetFeedBackForm(findFormID).Result;

            // ASSERT - check if the IActionResult is BadRequest
            Assert.IsType<BadRequestResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultGet as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetFeedBackFormById_OkResult()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.GetFeedBackFormById_OkResult);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new FeedBackFormsController(dbContext, logger);
            int findFormID = 2;

            // ACT
            IActionResult actionResultGet = controller.GetFeedBackForm(findFormID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ASSERT - if Status Code is HTTP 200 (Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultGet as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetFeedBackFormById_CorrectResult()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.GetFeedBackFormById_OkResult);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new FeedBackFormsController(dbContext, logger);
            int findFormID = 2;
            FeedBackForm expectedFeedBackForm = DbContextMocker.TestData_FeedBackForms
                                        .SingleOrDefault(t => t.FormId == findFormID);

            // ACT
            IActionResult actionResultGet = controller.GetFeedBackForm(findFormID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ASSERT - if IActionResult (i.e., OkObjectResult) contains an object of the type FeedBackForm
            OkObjectResult okResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;
            Assert.IsType<FeedBackForm>(okResult.Value);

            // Extract the FeedBackForm object from the result.
            FeedBackForm actualFeedBackForm = okResult.Value.Should().BeAssignableTo<FeedBackForm>().Subject;
            _testOutputHelper.WriteLine($"Found: FormId == {actualFeedBackForm.FormId}");

            // ASSERT - if FeedBackForm is NOT NULL
            Assert.NotNull(actualFeedBackForm);

            // ASSERT: check if the ID is correct
            Assert.Equal<int>(expected: expectedFeedBackForm.FormId,
                                actual: actualFeedBackForm.FormId);

            // ASSERT: check if the Name is correct
            Assert.Equal(expected: expectedFeedBackForm.CustomerName,
                                actual: actualFeedBackForm.CustomerName);

            // ASSERT: check if the Rating is correct
            Assert.Equal(expected: expectedFeedBackForm.Rating,
                                actual: actualFeedBackForm.Rating);

            // ASSERT: check if the Comments is correct
            Assert.Equal(expected: expectedFeedBackForm.Comments,
                                actual: actualFeedBackForm.Comments);

        }
    }
}
