using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Core.Dtos
{
    public class NotificationDto
    {
        public DateTime Datetime { get; set; }
        public NotificationType Type { get;  set; }
        public DateTime? OriginalDateTime { get;  set; }
        public string OriginalVenue { get;  set; }
        public GigDto Gig { get;  set; }

    }
}