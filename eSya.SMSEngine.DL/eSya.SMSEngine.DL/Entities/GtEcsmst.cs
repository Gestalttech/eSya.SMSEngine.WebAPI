using System;
using System.Collections.Generic;

namespace eSya.SMSEngine.DL.Entities
{
    public partial class GtEcsmst
    {
        public int TeventId { get; set; }
        public string TeventDesc { get; set; } = null!;
        public string FormId { get; set; } = null!;
        public bool ActiveStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}
