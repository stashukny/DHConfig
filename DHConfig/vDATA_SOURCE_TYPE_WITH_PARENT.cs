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
    
    public partial class vDATA_SOURCE_TYPE_WITH_PARENT
    {
        public System.Guid DATA_SOURCE_TYPE_GUID { get; set; }
        public string DATA_SOURCE_TYPE_HID_STRING { get; set; }
        public Nullable<short> DATA_SOURCE_TYPE_HID_LEVEL { get; set; }
        public string DATA_SOURCE_TYPE_NAME { get; set; }
        public string DATA_SOURCE_TYPE_NAME_WITH_PARENT { get; set; }
    
        public virtual DATA_SOURCE_TYPE DATA_SOURCE_TYPE { get; set; }
    }
}
