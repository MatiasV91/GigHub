using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using GigHub.Persistence;
using GigHub.Core;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig == null || gig.IsCanceled) { return NotFound(); }
            if (gig.ArtistId != userId) { return Unauthorized(); }
            gig.Cancel();
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
