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
    /// <remarks>
    ///     Bad insertion data scenarios:
    ///     - Name is NULL
    ///     - Name is EMPTY STRING
    ///     - Name contains more characters than what is allowed
    ///     - NULL Category object
    ///     
    ///     LIMITATIONS OF WORKING WITH InMemory Database
    ///     - Relationships between Tables are not supported.
    ///     - EF Core DataAnnotation Validations are not supported.
    /// </remarks>

    public partial class FeedBackFormsApiTests
    {
        [Fact]
        public void InsertFeedBackForm_OkResult()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.InsertFeedBackForm_OkResult);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new FeedBackFormsController(dbContext, logger);
            FeedBackForm feedBackFormToAdd = new FeedBackForm
            {
                FormId = 5,
                CustomerName = "New Category",            // IF = null, then: INVALID!  CategoryName is REQUIRED
                Rating = "Good",
                Comments = "Best"
            };

            // ACT
            IActionResult actionResultPost = apiController.PostFeedBackForm(feedBackFormToAdd).Result;

            // ASSERT - check if the IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultPost);

            // ASSERT - check if the Status Code is (HTTP 200) "Ok", (HTTP 201 "Created")
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultPost as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);

            // Extract the result from the IActionResult object.
            var postResult = actionResultPost.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT - if the result is a CreatedAtActionResult
            Assert.IsType<CreatedAtActionResult>(postResult.Value);

            // Extract the inserted Category object
            FeedBackForm actualFeedBackForm = (postResult.Value as CreatedAtActionResult).Value
                                      .Should().BeAssignableTo<FeedBackForm>().Subject;

            // ASSERT - if the inserted Category object is NOT NULL
            Assert.NotNull(actualFeedBackForm);

            Assert.Equal(feedBackFormToAdd.FormId, actualFeedBackForm.FormId);
            Assert.Equal(feedBackFormToAdd.CustomerName, actualFeedBackForm.CustomerName);
            Assert.Equal(feedBackFormToAdd.Rating, actualFeedBackForm.Rating);
            Assert.Equal(feedBackFormToAdd.Comments, actualFeedBackForm.Comments);
        }
    }
}
