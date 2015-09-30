using System.ComponentModel.DataAnnotations;

namespace DHConfig
{
    [MetadataType(typeof(DIMMetaData))]
    public partial class DIM
    {
        public string[] SelectedItems
        {
            get
            {
                if (DIM_FEATURE != null)
                    return (DIM_FEATURE.Split(','));
                else
                    return null;
            }
        }
    }

    public class DIMMetaData
    {
        [Required]
        public string DIM_COMMON_NAME { get; set; }
    }
}