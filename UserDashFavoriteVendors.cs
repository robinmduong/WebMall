using System;
using System.Collections.Generic;
using System.Text;

namespace Sabio.Models.Domain
{
    public class UserDashFavoriteVendors
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VendorId { get; set; }
    }
}
