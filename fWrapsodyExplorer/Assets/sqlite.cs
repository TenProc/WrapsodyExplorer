using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace fWrapsodyExplorer.Assets
{
    [Table(Name="Wrapsody_SyncSub")]
    public class DBTableSyncSub
    {
        [Column(Name ="DateTime", IsPrimaryKey =true)]
        public int dateTime { get; set; }
        [Column(Name ="SyncID")]
        public string syncID { get; set; }
        [Column(Name ="FilePath")]
        public string filePath { get; set; }
        [Column(Name="LastViewTime")]
        public int lastViewTime { get; set; }
    }

    class DBTableCommon
    { }
}
