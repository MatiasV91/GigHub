using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public ApplicationDbContext _context;
        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNofications(string userId)
        {
            return _context.UserNotifications
                        .Where(u => u.UserId == userId && u.IsRead == false)
                        .Select(un => un.Notification)
                        .Include(n => n.Gig.Artist)
                        .ToList();
        }

        public IEnumerable<UserNotification> GetUserNotifications(string userId)
        {
            return _context.UserNotifications.Where(n => n.UserId == userId && !n.IsRead).ToList();
        }
    }
}