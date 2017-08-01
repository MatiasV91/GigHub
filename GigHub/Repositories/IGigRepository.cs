using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsCreated(string userId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
    }
}