using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class TimeSheet:Base
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public double? TotalHoursInWork { get; set; }
        public double? TotalAnnualLeaveHours { get; set; }
        public double? TotalSickLeaveHours { get; set; }
        public double? TotalPublishHolidayHours { get; set; }
        public double? TotalHoursInWorkYTD { get; set; }
        public double? TotalAnnualLeaveHoursYTD { get; set; }
        public double? TotalSickLeaveHoursYTD { get; set; }
        public double? TotalPublishHolidayHoursYTD { get; set; }
        //public double? SalaryInGrossBased { get; set; }
        //public double? SocialInsurance { get; set; }
        //public double? HealthInsurance { get; set; }
        //public double? UnEmployementInsurance { get; set; }
        //public double? PaidByEmployee { get; set; }
        //public double? PaidByEmployer { get; set; }
        //public double? AdditionDeduction { get; set; }
        //public double? IncomeForTaxCalculation { get; set; }
        //public double? NetIncome { get; set; }
        public double? SalaryPerHours { get; set; }
        public string Month { get; set; }
        public string JobTitle { get; set; }
        public double Rate { get; set; }
        public double? NormalAmount { get; set; }
        public double? PublicHolidayAmount { get; set; }
        public double? SickLeaveAmount { get; set; }
        public double? AnnualLeaveAmount { get; set; }
        public double? NormalAmountYTD { get; set; }
        public double? PublicHolidayAmountYTD { get; set; }
        public double? SickLeaveAmountYTD { get; set; }
        public double? AnnualLeaveAmountYTD { get; set; }
        public double? LessPAYG { get; set; }
        public double? TaxableIncome { get; set; }
        public double? NetPaidAmount { get; set; }
        public double? LessPAYGYTD { get; set; }
        public double? TaxableIncomeYTD { get; set; }
        public double? NetPaidAmountYTD { get; set; }
        public double? SickLeaveYTD { get; set; }
        public double? AnnualLeaveYTD { get; set; }
        public double? Contribution { get; set; }
        public double? ContributionYTD { get; set; }
        public DateTime? SalaryPaidDate { get; set; }
        public string LessPAYGStatus { get; set; }
        public string SuperannuationName { get; set; }
        public string SuperannuationMemberNumber { get; set; }
    }
}
