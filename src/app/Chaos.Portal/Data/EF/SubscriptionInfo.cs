//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chaos.Portal.Data.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubscriptionInfo
    {
        public System.Guid GUID { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> UserGUID { get; set; }
        public Nullable<long> Permission { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
