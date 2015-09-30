using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DHConfig
{
    [MetadataType(typeof(DATA_SOURCEMetaData))]
    public partial class DATA_SOURCE
    {
        public string[] SelectedItems
        {
            get
            {
                if (DATA_SOURCE_FEATURE != null)
                    return (DATA_SOURCE_FEATURE.Split(','));
                else
                    return null;
            }
        }
    }

    public class DATA_SOURCEMetaData
    {

    }

}
