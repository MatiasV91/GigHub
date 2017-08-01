using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Collections;
using GigHub.Repositories;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;
        private AttendanceRepository _attendanceRepository;
        private GigRepository _gigRepository;
        private FollowingRepository _followingRepository;
        private GenreRepository _genreRepository;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
            _gigRepository = new GigRepository(_context);
            _followingRepository = new FollowingRepository(_context);
            _genreRepository = new GenreRepository(_context);
        }
        
        public ActionResult Details(int id)
        {

            var gig = _gigRepository.GetGig(id);

            if(gig == null) { return HttpNotFound(); }

            var viewModel = new DetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsAttending = _attendanceRepository.GetAttendance(gig.Id, userId) != null;
                viewModel.IsFollowing = _followingRepository.GetFollowing(gig.ArtistId, userId) != null;
            }

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _gigRepository.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs",viewModel);
        }


        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Following()
        {
            var followign = _followingRepository.GetFollowings(User.Identity.GetUserId());
            return View(followign);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var gigs = _gigRepository.GetGigsCreated(User.Identity.GetUserId());
            return View(gigs);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _genreRepository.GetGenres(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gig.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _gigRepository.GetGig(id);

            if(gig == null) { return HttpNotFound(); }

            if (gig.ArtistId != User.Identity.GetUserId()) { return new HttpUnauthorizedResult(); }

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Genres = _genreRepository.GetGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig"
            };

            return View("GigForm", viewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _gigRepository.GetGigWithAttendees(viewModel.Id);

            if(gig == null) { return HttpNotFound(); }

            if(gig.ArtistId != User.Identity.GetUserId()) { return new HttpUnauthorizedResult(); }

            gig.Update(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);
                
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        


    }
}