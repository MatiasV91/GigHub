using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using System.Security.Claims;
using GigHub.Controllers.Api;
using Moq;
using GigHub.Core;
using GigHub.Test.Extensions;
using GigHub.Core.Repositories;
using FluentAssertions;
using System.Web.Http.Results;
using GigHub.Core.Models;

namespace GigHub.Test.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);
            _controller = new GigsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }
        [TestMethod]
        public void Cancel_NoGigWithgivenIdExists_ShoudReturnNotFound()
        {
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUserGigs_ShouldReturnUnauthorized()
        {
            var gig = new Gig { ArtistId = _userId + "-"};
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }
        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOK()
        {
            var gig = new Gig { ArtistId = _userId };
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _controller.Cancel(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
