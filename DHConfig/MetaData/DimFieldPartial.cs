using System.ComponentModel.DataAnnotations;

namespace DHConfig
{
    [MetadataType(typeof(DIM_FIELDMetaData))]
    public partial class DIM_FIELD
    {
        public string[] SelectedItems
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

    public class DIM_FIELDMetaData
    {
    }
}