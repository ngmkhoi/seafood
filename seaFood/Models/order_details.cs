//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace seaFood.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class order_details
    {
        public int order_detail_id { get; set; }
        public Nullable<int> order_id { get; set; }
        public Nullable<int> product_id { get; set; }
        public string size { get; set; }
        public Nullable<int> quantity_order { get; set; }
        public Nullable<int> total_price { get; set; }
    
        public virtual order order { get; set; }
        public virtual product product { get; set; }
    }
}
