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
    
    public partial class DATA_SOURCE_TYPE
    {
        public DATA_SOURCE_TYPE()
        {
            this.DATA_SOURCE = new HashSet<DATA_SOURCE>();
            this.vDATA_SOURCE_TYPE_WITH_PARENT = new HashSet<vDATA_SOURCE_TYPE_WITH_PARENT>();
        }
    
        public System.Guid DATA_SOURCE_TYPE_GUID { get; set; }
        public System.DateTime CREATE_DATE_UTC { get; set; }
        public System.DateTime MODIFIED_DATE_UTC { get; set; }
        public string MODIFIED_BY { get; set; }
        public string DATA_SOURCE_TYPE_HID_STRING { get; set; }
        public Nullable<short> DATA_SOURCE_TYPE_HID_LEVEL { get; set; }
        public string DATA_SOURCE_TYPE_NAME { get; set; }
    
        public virtual ICollection<DATA_SOURCE> DATA_SOURCE { get; set; }
        public virtual ICollection<vDATA_SOURCE_TYPE_WITH_PARENT> vDATA_SOURCE_TYPE_WITH_PARENT { get; set; }
    }
}
