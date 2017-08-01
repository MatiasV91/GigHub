using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GigHub.Repositories
{
    public class FollowingRepository
    {
        private ApplicationDbContext _context;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Following> GetFollowings(string userId)
        {
            return _context.Followings.Include(f => f.Followee).Where(f => f.FollowerId == userId);
        }

        public Following GetFollowing(string artistId, string userId)
        {
            return _context.Followings.SingleOrDefault(f => f.FolloweeId == artistId && f.FollowerId == userId);
        }
    }
}