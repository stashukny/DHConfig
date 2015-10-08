using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public string SOURCE_COLUMN_NAME { get; set; }
        [Required]
        public string RAW_VIEW_COLUMN_NAME { get; set; }
    }
}