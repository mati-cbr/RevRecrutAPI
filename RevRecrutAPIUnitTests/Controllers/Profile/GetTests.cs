using Microsoft.AspNetCore.Mvc;
using Moq;
using RevRecrutAPI.Controllers;
using RevRecrutAPI.DTOs.Candidate.Profile;
using RevRecrutAPI.Services.Candidate.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevRecrutAPIUnitTests.Controllers.Profile;

public class ProfileController_GetProfileTests
{
    ProfileResponse pr;

    [SetUp]
    public void Setup()
    { 
        pr = new ProfileResponse
        {
            Id = 1,
            FirstName = "Marian",
            LastName = "Testowy",
            ContactEMail = "marian@test.pl",
            ContactPhone = "111222333",
            Address1 = "test",
            Address2 = "test"
        };
    }

    [Test]
    public async Task Test1()
    {
        // Arrange
        var serviceMock = new Mock<IProfileService>();
        //serviceMock
        //    .Setup(s => s.GetProfileById(1))
        //    .Returns(Task.FromResult(pr));

        var controller = new ProfileController(serviceMock.Object);

        // Act
        var pr_response = await controller.GetProfile(1);

        // Assert
        //serviceMock.Verify(s => s.GetProfileByIdAsync(1), Times.Once);

        //var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //var returnedProfile = Assert.IsType<Profile>(okResult.Value);

        //Assert.Equals(pr, returnedProfile);
    }
}