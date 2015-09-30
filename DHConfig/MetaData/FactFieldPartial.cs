﻿using System.ComponentModel.DataAnnotations;

namespace DHConfig
{
    [MetadataType(typeof(FACT_FIELDMetaData))]
    public partial class FACT_FIELD
    {
        public string[] SelectedItems
        {
            get
            {
                if (FACT_FIELD_FEATURE != null)
                    return (FACT_FIELD_FEATURE.Split(','));
                else
                    return null;
            }
        }
    }

    public class FACT_FIELDMetaData
    {
    }
}