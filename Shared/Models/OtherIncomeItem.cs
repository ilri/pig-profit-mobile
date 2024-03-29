﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared
{
    [Table("OtherIncomeItems")]
    public class OtherIncomeItem : BaseItem
    {
        public DateTime Date { get; set; }
        public double TotalIncome { get; set; }
        public string? OtherWhatFor { get; set; }
        public double TransportationCosts { get; set; }
        public double? OtherCosts { get; set; }
        public string? Comment { get; set; }
        public virtual string DateNiceFormat { get { return Date.ToString("dd/MMM/yyyy"); } }
    }
}
