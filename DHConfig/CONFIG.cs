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
    
    public partial class CONFIG
    {
        public CONFIG()
        {
            this.DATA_SOURCE = new HashSet<DATA_SOURCE>();
            this.DATA_SOURCE_FIELD = new HashSet<DATA_SOURCE_FIELD>();
            this.FACT_FIELD = new HashSet<FACT_FIELD>();
            this.INDX_FIELD = new HashSet<INDX_FIELD>();
            this.INDXes = new HashSet<INDX>();
            this.METRICs = new HashSet<METRIC>();
            this.SUMMARies = new HashSet<SUMMARY>();
            this.DIMs = new HashSet<DIM>();
            this.DIM_FIELD = new HashSet<DIM_FIELD>();
            this.FACTs = new HashSet<FACT>();
        }
    
        public string CONFIG_COMMON_NAME { get; set; }
        public string CONFIG_DATA_PROCESS_PROC_SCHEMA { get; set; }
        public string CONFIG_DATA_PROCESS_PROC_NAME { get; set; }
        public string CONFIG_DATA_SYNC_PROC_SCHEMA { get; set; }
        public string CONFIG_DATA_SYNC_PROC_NAME { get; set; }
    
        public virtual ICollection<DATA_SOURCE> DATA_SOURCE { get; set; }
        public virtual ICollection<DATA_SOURCE_FIELD> DATA_SOURCE_FIELD { get; set; }
        public virtual ICollection<FACT_FIELD> FACT_FIELD { get; set; }
        public virtual ICollection<INDX_FIELD> INDX_FIELD { get; set; }
        public virtual ICollection<INDX> INDXes { get; set; }
        public virtual ICollection<METRIC> METRICs { get; set; }
        public virtual ICollection<SUMMARY> SUMMARies { get; set; }
        public virtual ICollection<DIM> DIMs { get; set; }
        public virtual ICollection<DIM_FIELD> DIM_FIELD { get; set; }
        public virtual ICollection<FACT> FACTs { get; set; }
    }
}