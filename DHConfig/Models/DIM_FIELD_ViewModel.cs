using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace DHConfig.Models
{
    class DIM_FIELD_ViewModel
    {
        public string CONFIG_COMMON_NAME { get; set; }
        public string DIM_COMMON_NAME { get; set; }
        public string DIM_FIELD_NAME { get; set; }
        public string DIM_FIELD_NAME_CLEAN { get; set; }
        public string DIM_DATA_TYPE { get; set; }
        public string DIM_FIELD_FEATURE { get; set; }
        public string DERIVED_CONFIGURATION { get; set; }

        public string[] SelectedFeatures
        {
            get
            {
                if (DIM_FIELD_FEATURE != null)
                    return (DIM_FIELD_FEATURE.Split(','));
                else
                    return null;
            }
        }  

    }
}
