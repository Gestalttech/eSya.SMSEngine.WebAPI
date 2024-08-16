﻿using System;
using System.Collections.Generic;

namespace eSya.SMSEngine.DL.Entities
{
    public partial class GtSmsloc
    {
        public int BusinessKey { get; set; }
        public int FormId { get; set; }
        public string Smsid { get; set; } = null!;
        public bool ActiveStatus { get; set; }
        public string FormId1 { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}
