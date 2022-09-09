﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PigTool.Models
{
    public class MembershipItem : BaseItem
    {
        public DateTime Date { get; set; }
        public double? TotalCosts { get; set; }
        public string MembershipType { get; set; }
        public string OtherMembershipType { get; set; }
        public int TimePeriod { get; set; }
        public string TimePeriodUnit { get; set; }
        public double? OtherCosts { get; set; }
        public string? Comment { get; set; }
    }
}
