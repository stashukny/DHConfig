//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class EXECUTE_SQL_HISTORY
    {
        public int ID { get; set; }
        public string SQL_SCRIPT { get; set; }
        public string SCRIPT_TITLE { get; set; }
        public System.DateTime START_TIME_UTC { get; set; }
        public System.DateTime STOP_TIME_UTC { get; set; }
        public string EXCEPTION { get; set; }
        public Nullable<bool> IS_FAILED { get; set; }
        public bool IS_EXECUTE { get; set; }
    }
}