using System;
using EMarket.Core.Domain.Common;

namespace EMarket.Core.Domain.Entities
{
	public class Advertisement : AuditableBaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public string UserMail { get; set; }
        public string UserPhone { get; set; }

        // Navigation Property
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Navigation Property
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

