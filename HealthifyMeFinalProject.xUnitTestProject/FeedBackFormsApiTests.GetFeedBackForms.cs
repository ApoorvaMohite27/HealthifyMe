using Castle.Core.Logging;
using FluentAssertions;
using HealthifyMeFinalProject.Controllers;
using HealthifyMeFinalProject.Models;
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
        public void GetFeedBackForms_OkResult()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.GetFeedBackForms_OkResult);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apicontroller = new FeedBackFormsController(dbContext,logger);

            // ACT
            IActionResult actionResult = apicontroller.GetFeedBackForms().Result;

            // ASSERT
            // --- Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);

            // --- Check if the HTTP Response Code is 200 "OK"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            int actualStatusCode = (actionResult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetFeedBackForms_CheckCorrectResult()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.GetFeedBackForms_OkResult);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apicontroller = new FeedBackFormsController(dbContext, logger);

            // ACT: Call the API action
            IActionResult actionResult = apicontroller.GetFeedBackForms().Result;

            // ASSERT: Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);

            // ACT: Extract the OkResult result
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;

            // Assert if the OkResult contains the object of the correct type
            Assert.IsAssignableFrom<List<FeedBackForm>>(okResult.Value);

            // ACT: Extract the Data from the result of the action
            var feedBackFormsApi = okResult.Value.Should().BeAssignableTo<List<FeedBackForm>>().Subject;

            // ASSERT if the FeedBackForm is NOT NULL
            Assert.NotNull(feedBackFormsApi);

            // ASSERT: If the number of categories in the DbContext seed data
            //          is the same as the number of feedBackFormsreturned in the API Result
            Assert.Equal<int>(expected: DbContextMocker.TestData_FeedBackForms.Length,
                                actual: feedBackFormsApi.Count);

            // ASSERT: Test the data received from the API against the seed data
            int ndx = 0;
            foreach(FeedBackForm feedBackForm in DbContextMocker.TestData_FeedBackForms)
            {
                // ASSERT: check if the ID is correct
                Assert.Equal<int>(expected: feedBackForm.FormId,
                                    actual: feedBackFormsApi[ndx].FormId);

                // ASSERT: check if the Name is correct
                Assert.Equal(expected: feedBackForm.CustomerName,
                                    actual: feedBackFormsApi[ndx].CustomerName);

                // ASSERT: check if the Rating is correct
                Assert.Equal(expected: feedBackForm.Rating,
                                    actual: feedBackFormsApi[ndx].Rating);

                // ASSERT: check if the Comments is correct
                Assert.Equal(expected: feedBackForm.Comments,
                                    actual: feedBackFormsApi[ndx].Comments);

                _testOutputHelper.WriteLine($"Compared Row # {ndx} successfully");
                
                ndx++;        // now cpmpare against next variable
            }

        }
    }
}
