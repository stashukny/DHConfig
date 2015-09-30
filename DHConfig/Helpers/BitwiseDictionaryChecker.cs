using System;
using System.Linq;

namespace DHConfig
{
    internal static class BitwiseDictionaryChecker
    {
        public static bool IsExists(ref string DIM_FEATURE, string[] SelectedItems, string BITWISE_GROUP, DataHammerConfigEntities db)
        {
            bool exists = false;

            if (SelectedItems.Count() > 1)
            {
                DIM_FEATURE = String.Join(",", SelectedItems);
            }
            else
            {
                DIM_FEATURE = SelectedItems[0];
            }

            int Total = 0;

            var settings = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == BITWISE_GROUP && SelectedItems.Contains(f.BITWISE_KEY))
            .Sum(x => x.BITWISE_VALUE);

            Total = (int)settings;

            //validate sum
            exists = db.BITWISE_DICTIONARY_VALID_VALUES.Any(a => a.BITWISE_VALUE == Total && a.BITWISE_GROUP == BITWISE_GROUP);

            return exists;
        }
    }
}