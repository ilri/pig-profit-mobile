﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared
{
    [Table("LoanRepaymentItems")]
    public class LoanRepaymentItem : BaseItem
    {
        public DateTime Date { get; set; }
        public double TotalAmountRepaid { get; set; }
        public string? LoanProvider { get; set; }
        public string? OtherLoanProvider { get; set; }
        public double OtherCosts { get; set; }
        public string? Comment { get; set; }
        [JsonIgnore]
        public virtual Translation? DisplayTypeTranslation { get; set; }
        public virtual string? DisplayTypeTranslationString { get; set; }
    }
}
