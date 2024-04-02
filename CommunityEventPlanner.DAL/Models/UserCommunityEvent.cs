using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityEventPlanner.Data.Models
{
    public class UserCommunityEvent
    {
        public int Id { get; set; }

        public int CommunityEventId { get; set; }

        public Guid UserId { get; set; }

        public CommunityEvent CommunityEvent { get; set; }
    }
}
