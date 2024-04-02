using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityEventPlanner.Data.Models
{
    public class CommunityEvent
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Summary { get; set; }

        public required DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
