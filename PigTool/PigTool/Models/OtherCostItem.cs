﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PigTool.Models
{
    public class OtherCostItem : BaseItem
    {
        public DateTime Date { get; set; }
        public double? TotalCosts { get; set; }
        public string OtherWhatFor { get; set; }
        public double? TransportationCosts { get; set; }
        public double? OtherCosts { get; set; }
        public string? Comment { get; set; }
    }
}