using System;
using System.Collections.Generic;
using System.Text;

namespace Sabio.Models.Domain
{
    public class UserDashProduct
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public bool IsActive { get; set; }
        public int PrimaryImageId { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Specifications { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public string ProductStatus { get; set; }
        public int ProductTypeId { get; set; }
        public int VendorId { get; set; }
        public int BusinessId { get; set; }
        public int VendorTypeId { get; set; }
        public string VendorName { get; set; }
        public string Url { get; set; }
        public string ProductImages { get; set; }
    }
}
