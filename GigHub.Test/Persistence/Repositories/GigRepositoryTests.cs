using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Persistence.Repositories;
using Moq;
using GigHub.Persistence;
using GigHub.Core.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using GigHub.Test.Extensions;
using FluentAssertions;

namespace GigHub.Test.Persistence.Repositories
{

    [TestClass]
    public class GigRepositoryTests
    {
        //private GigRepository _repository;
        //private Mock<DbSet<Gig>> _mockGigs;
        //[TestInitialize]
        //public void TestInitilize()
        //{
        //    _mockGigs = new Mock<DbSet<Gig>>();
        //    var mockContext = new Mock<IApplicationDbContext>();
        //    mockContext.SetupGet(c => c.Gig).Returns(_mockGigs.Object);
        //    _repository = new GigRepository(mockContext.Object);
        //}

        //[TestMethod]
        //public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        //{
        //    var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };
        //    _mockGigs.SetSource(new[] { gig });
        //    var result = _repository.GetGigsCreated("1");
        //    result.Should().BeEmpty();
        //}
    }
}
