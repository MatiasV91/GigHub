﻿using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private IApplicationDbContext _context;
        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public void Add(Gig gig)
        {
            _context.Gig.Add(gig);
        }

        public IEnumerable<Gig> GetFutureGigs(string searchString = null)
        {
            var upcomingGigs = _context.Gig.Include(g => g.Artist).Include(g => g.Genre)
                                            .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                upcomingGigs = upcomingGigs.Where(g =>
                        g.Venue.Contains(searchString) ||
                        g.Artist.Name.Contains(searchString) ||
                        g.Genre.Name.Contains(searchString));
            }

            return upcomingGigs;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetGigsCreated(string userId)
        {
            return _context.Gig 
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gig
                        .Include(g => g.Attendances.Select(a => a.Attendee))
                        .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gig.Include(g => g.Artist).Include(g => g.Genre).SingleOrDefault(g => g.Id == gigId);
        }


    }
}