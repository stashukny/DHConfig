﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DHConfig
{
    [MetadataType(typeof(FACTMetaData))]
    public partial class FACT
    {
        public string[] SelectedItems
        {
            get
            {
                if (FACT_FEATURE != null)
                    return (FACT_FEATURE.Split(','));
                else
                    return null;
            }
        } 
    }

    public class FACTMetaData
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]        
        //public string DISTINCT_TABLE_KEY_SCHEMA { get; set; }        
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public string DISTINCT_TABLE_VALUE_SCHEMA { get; set; }        
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public string DISTINCT_VALUE_PROCEDURE_SCHEMA { get; set; }        
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public string DISTINCT_KEY_PROCEDURE_SCHEMA { get; set; }        
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public string FACT_LOAD_PROCEDURE_SCHEMA { get; set; }        
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public string FACT_PRE_EXEC_SPROC_SCHEMA { get; set; }        
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public string FACT_POST_EXEC_SPROC_SCHEMA { get; set; }
        
        [Required]
        public string DISTINCT_TABLE_KEY_NAME { get; set; }        
        [Required]
        public string DISTINCT_TABLE_VALUE_NAME { get; set; }        
        [Required]
        public string DISTINCT_VALUE_PROCEDURE_NAME { get; set; }                
        [Required]
        public string DISTINCT_KEY_PROCEDURE_NAME { get; set; }        
        [Required]
        public string FACT_LOAD_PROCEDURE_NAME { get; set; }        



    }

}