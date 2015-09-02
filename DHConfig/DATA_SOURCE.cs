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
    
    public partial class DATA_SOURCE
    {
        public DATA_SOURCE()
        {
            this.DATA_SOURCE_FIELD = new HashSet<DATA_SOURCE_FIELD>();
            this.FACTs = new HashSet<FACT>();
        }
    
        public string CONFIG_COMMON_NAME { get; set; }
        public string DATA_SOURCE_NAME { get; set; }
        public System.Guid DATA_SOURCE_TYPE_GUID { get; set; }
        public string DATA_SOURCE_TABLE_SCHEMA { get; set; }
        public string DATA_SOURCE_TABLE_NAME { get; set; }
        public string DATA_SOURCE_RAW_VIEW_SCHEMA { get; set; }
        public string DATA_SOURCE_RAW_VIEW_NAME { get; set; }
        public string DATA_SOURCE_TABLE_PROC_UPDATE_SCHEMA { get; set; }
        public string DATA_SOURCE_TABLE_PROC_UPDATE_NAME { get; set; }
        public string DATA_SOURCE_TABLE_PROC_INSERT_SCHEMA { get; set; }
        public string DATA_SOURCE_TABLE_PROC_INSERT_NAME { get; set; }
        public string DATA_SOURCE_TABLE_PROC_DELETE_SCHEMA { get; set; }
        public string DATA_SOURCE_TABLE_PROC_DELETE_NAME { get; set; }
        public string DATA_SOURCE_TABLE_PROC_DDL_PARENT_SCHEMA { get; set; }
        public string DATA_SOURCE_TABLE_PROC_DDL_PARENT_NAME { get; set; }
        public string DATA_SOURCE_RAW_UI_VIEW_SCHEMA { get; set; }
        public string DATA_SOURCE_RAW_UI_VIEW_NAME { get; set; }
        public string DATA_SOURCE_FEATURE { get; set; }
        public string DATA_SOURCE_TEST_DATA_PROC_SCHEMA { get; set; }
        public string DATA_SOURCE_TEST_DATA_PROC_NAME { get; set; }
    
        public virtual CONFIG CONFIG { get; set; }
        public virtual ICollection<DATA_SOURCE_FIELD> DATA_SOURCE_FIELD { get; set; }
        public virtual DATA_SOURCE_TYPE DATA_SOURCE_TYPE { get; set; }
        public virtual ICollection<FACT> FACTs { get; set; }
    }
}
