using EMarket.Core.Domain.Common;
using System.Collections.Generic;

namespace EMarket.Core.Domain.Entities
{
    public class Category : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation Property
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
