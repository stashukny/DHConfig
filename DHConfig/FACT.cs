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
    
    public partial class FACT
    {
        public FACT()
        {
            this.FACT_FIELD = new HashSet<FACT_FIELD>();
            this.SUMMARies = new HashSet<SUMMARY>();
        }
    
        public string CONFIG_COMMON_NAME { get; set; }
        public string DATA_SOURCE_NAME { get; set; }
        public string FACT_COMMON_NAME { get; set; }
        public string FACT_TABLE_SCHEMA { get; set; }
        public string FACT_TABLE_NAME { get; set; }
        public string FACT_FEATURE { get; set; }
        public string DISTINCT_TABLE_KEY_SCHEMA { get; set; }
        public string DISTINCT_TABLE_KEY_NAME { get; set; }
        public string DISTINCT_TABLE_VALUE_SCHEMA { get; set; }
        public string DISTINCT_TABLE_VALUE_NAME { get; set; }
        public string DISTINCT_VALUE_PROCEDURE_SCHEMA { get; set; }
        public string DISTINCT_VALUE_PROCEDURE_NAME { get; set; }
        public string DISTINCT_KEY_PROCEDURE_SCHEMA { get; set; }
        public string DISTINCT_KEY_PROCEDURE_NAME { get; set; }
        public string FACT_LOAD_PROCEDURE_SCHEMA { get; set; }
        public string FACT_LOAD_PROCEDURE_NAME { get; set; }
        public string FACT_PRE_EXEC_SPROC_SCHEMA { get; set; }
        public string FACT_PRE_EXEC_SPROC_NAME { get; set; }
        public string FACT_POST_EXEC_SPROC_SCHEMA { get; set; }
        public string FACT_POST_EXEC_SPROC_NAME { get; set; }
        public bool IS_AUTO_GENERATED_FACT_TABLE { get; set; }
        public bool IS_AUTO_GENERATED_LOAD_PROCEDURE { get; set; }
    
        public virtual CONFIG CONFIG { get; set; }
        public virtual DATA_SOURCE DATA_SOURCE { get; set; }
        public virtual ICollection<FACT_FIELD> FACT_FIELD { get; set; }
        public virtual ICollection<SUMMARY> SUMMARies { get; set; }
    }
}
