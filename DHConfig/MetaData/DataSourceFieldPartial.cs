using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DHConfig
{
    [MetadataType(typeof(DATA_SOURCE_FIELDMetaData))]
    public partial class DATA_SOURCE_FIELD
    {
        public string[] SelectedItems
        {
            get
            {
                if (DATA_SOURCE_FIELD_FEATURE != null)
                    return (DATA_SOURCE_FIELD_FEATURE.Split(','));
                else
                    return null;
            }
        }
    }

    public class DATA_SOURCE_FIELDMetaData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string COLUMN_NAME { get; set; }
    }

}
