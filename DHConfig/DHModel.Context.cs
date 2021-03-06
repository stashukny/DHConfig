﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataHammerConfigEntities : DbContext
    {
        public DataHammerConfigEntities()
            : base("name=DataHammerConfigEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CONFIG> CONFIGs { get; set; }
        public virtual DbSet<DATA_SOURCE> DATA_SOURCE { get; set; }
        public virtual DbSet<DATA_SOURCE_FIELD> DATA_SOURCE_FIELD { get; set; }
        public virtual DbSet<FACT_FIELD> FACT_FIELD { get; set; }
        public virtual DbSet<INDX> INDXes { get; set; }
        public virtual DbSet<METRIC> METRICs { get; set; }
        public virtual DbSet<SUMMARY> SUMMARies { get; set; }
        public virtual DbSet<INDX_FIELD> INDX_FIELD { get; set; }
        public virtual DbSet<BITWISE_DICTIONARY> BITWISE_DICTIONARY { get; set; }
        public virtual DbSet<BITWISE_DICTIONARY_VALID_VALUES> BITWISE_DICTIONARY_VALID_VALUES { get; set; }
        public virtual DbSet<DATA_SOURCE_TYPE> DATA_SOURCE_TYPE { get; set; }
        public virtual DbSet<DIM_TYPE> DIM_TYPE { get; set; }
        public virtual DbSet<METRIC_TYPE> METRIC_TYPE { get; set; }
        public virtual DbSet<BATCH> BATCHes { get; set; }
        public virtual DbSet<BATCH_STEP> BATCH_STEP { get; set; }
        public virtual DbSet<EXECUTE_SQL_HISTORY> EXECUTE_SQL_HISTORY { get; set; }
        public virtual DbSet<MODIFIED_TABLE> MODIFIED_TABLE { get; set; }
        public virtual DbSet<SERVICE_BROKER_HISTORY> SERVICE_BROKER_HISTORY { get; set; }
        public virtual DbSet<USER_CHANGE> USER_CHANGE { get; set; }
        public virtual DbSet<DIM_DATE> DIM_DATE { get; set; }
        public virtual DbSet<TALLY> TALLies { get; set; }
        public virtual DbSet<EXCEPTION> EXCEPTIONs { get; set; }
        public virtual DbSet<IMPORT_XML> IMPORT_XML { get; set; }
        public virtual DbSet<SYS_PROPERTY> SYS_PROPERTY { get; set; }
        public virtual DbSet<DIM> DIMs { get; set; }
        public virtual DbSet<DIM_FIELD> DIM_FIELD { get; set; }
        public virtual DbSet<FACT> FACTs { get; set; }
        public virtual DbSet<vSCHEMA> vSCHEMAS { get; set; }
        public virtual DbSet<vDATA_TYPES> vDATA_TYPES { get; set; }
        public virtual DbSet<vDATA_SOURCE_TYPE_WITH_PARENT> vDATA_SOURCE_TYPE_WITH_PARENT { get; set; }
    }
}
