using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using System.Security.Claims;
using GigHub.Controllers.Api;
using Moq;
using GigHub.Core;

namespace GigHub.Test.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        public GigsControllerTests()
        {
            var identity = new GenericIdentity("user1@domain.com");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "user1@domain.com"));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));

            var mockUoW = new Mock<IUnitOfWork>();
            var principal = new GenericPrincipal(identity, null);
            var controller = new GigsController(mockUoW.Object);
            controller.User = principal;
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
