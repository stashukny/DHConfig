//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DHConfig
{
    using System;
    using System.Collections.Generic;
    
    public partial class DIM_TYPE
    {
        public DIM_TYPE()
        {
            this.DIMs = new HashSet<DIM>();
        }
    
        public System.Guid DIM_TYPE_GUID { get; set; }
        public string DIM_TYPE_NAME { get; set; }
        public bool DIM_TYPE_INCLUDE_DATA_SOURCE { get; set; }
        public System.DateTime CREATE_DATE_UTC { get; set; }
        public System.DateTime MODIFIED_DATE_UTC { get; set; }
        public string MODIFIED_BY { get; set; }
    
        public virtual ICollection<DIM> DIMs { get; set; }
    }
}