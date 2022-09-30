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
    ///     Bad update data scenarios:
    ///     - Name is NULL
    ///     - Name is EMPTY STRING
    ///     - Name contains more characters than what is allowed
    ///     - NULL Category object
    ///     - ID not matching with the ID of the row identified to be updated.
    ///     - ID replaced with a negative value
    ///     - Replacing the object retrieved before the update, with a completely new object
    ///     - Since the HTTP PUT receives a Nullable INT as first parameter, pass a NULL value
    ///
    ///     LIMITATIONS OF WORKING WITH InMemory Database
    ///     - Relationships between Tables are not supported.
    ///     - EF Core DataAnnotation Validations are not supported.
    /// </remarks>
    
    public partial class FeedBackFormsApiTests
    {
        [Fact]
        public async void UpdateFeedBackForm_OkResult01()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.UpdateFeedBackForm_OkResult01);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new FeedBackFormsController(dbContext, logger);
            int editFormId = 2;
            FeedBackForm originalFeedBackForm, changedFeedBackForm;
            changedFeedBackForm = new FeedBackForm
            {
                FormId = editFormId,
                CustomerName = "--Changed Customer Name",
                Rating = "--Changed Rating",
                Comments = "--Changed Comments"
            };

            // ACT #1: Get the Record to Edit.

            // (a) Get the FeedBackForm to edit (to ensure that the row exists before editing it)
            IActionResult actionResultGet = await apiController.GetFeedBackForm(editFormId);

            // (b) Check if HTTP 200 "Ok" 
            OkObjectResult OkResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;

            // (c) Extract the FeedBackForm Object from the OkObjectResult
            originalFeedBackForm = OkResult.Value.Should().BeAssignableTo<FeedBackForm>().Subject;

            // (d) Check if the data to be edited was received from the API
            Assert.NotNull(originalFeedBackForm);

            _testOutputHelper.WriteLine("Retrieved the Data from the API.");

            try
            {
                // ACT #2: Try to update the data, using a completely new object
                //         NOTE: This will throw the InvalidOperationException!
                var actionResultPutAttempt1 = await apiController.PutFeedBackForm(editFormId, changedFeedBackForm);

                // ASSERT - if the update was successfull.
                Assert.IsType<OkResult>(actionResultPutAttempt1);

                _testOutputHelper.WriteLine("Updated the changes back to the API - using a new object.");
            }
            catch (System.InvalidOperationException exp)
            {
                // The PUT operation should throw this exception,
                // because it is an object that does not carry change tracking information.

                _testOutputHelper.WriteLine("Failed to update the change back to the API - using a new object");
                _testOutputHelper.WriteLine($"-- Exception Type: {exp.GetType()}");
                _testOutputHelper.WriteLine($"-- Exception Message: {exp.Message}");
                _testOutputHelper.WriteLine($"-- Exception Source: {exp.Source}");
                _testOutputHelper.WriteLine($"-- Exception TargetSite: {exp.TargetSite}");
            }
        }

        [Fact]
        public async void UpdateFeedBackForm_OkResult02()
        {
            // ARRANGE
            var dbName = nameof(FeedBackFormsApiTests.UpdateFeedBackForm_OkResult02);
            var logger = Mock.Of<ILogger<FeedBackFormsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new FeedBackFormsController(dbContext, logger);
            int editFormId = 2;
            FeedBackForm originalFeedBackForm;
            string changedCustomerName = "--- CHANGED Customer Name";

            // ACT #1: Get the Record to Edit.

            // (a) Get the FeedBackForm to edit (to ensure that the row exists before editing it)
            IActionResult actionResultGet = await apiController.GetFeedBackForm(editFormId);

            // (b) Check if HTTP 200 "Ok" 
            OkObjectResult OkResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;

            // (c) Extract the Category Object from the OkObjectResult
            originalFeedBackForm = OkResult.Value.Should().BeAssignableTo<FeedBackForm>().Subject;

            // (d) Check if the data to be edited was received from the API
            Assert.NotNull(originalFeedBackForm);

            _testOutputHelper.WriteLine("Retrieved the Data from the API.");

            // ACT #2: Now, try to update the data
            // SOLUTION: The following lines would work!
            //           Reason, we are modifying the data originally received.
            //           And then, calling the PUT operation.
            //           So, the API is able to find the ChangeTracking data associated to the object.

            // (a) Change the data of the object that was received from the API.
            originalFeedBackForm.CustomerName = changedCustomerName;

            // (b) Call the HTTP PUT API to update the changes (with the updated object)
            var actionResultPutAttempt2 = await apiController.PutFeedBackForm(editFormId, originalFeedBackForm);

            // ASSERT - if the update was successfull.
            Assert.IsType<NoContentResult>(actionResultPutAttempt2);

            _testOutputHelper.WriteLine("Updated the changes back to the API - using the object received");
        }

    }
}
