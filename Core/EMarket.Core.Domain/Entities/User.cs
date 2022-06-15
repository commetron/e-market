using System.Collections.Generic;
using EMarket.Core.Domain.Common;

namespace EMarket.Core.Domain.Entities
{
	public class User : AuditableBaseEntity
	{
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Navigation Property
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}

