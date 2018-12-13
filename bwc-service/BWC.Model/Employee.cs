using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class Employee:Base
    {
        public string Company { get; set; }
        public string Email { get; set; }
        public string WebPage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string BusinessPhone { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }
        public int? TaxFileNumber { get; set; }
        public string SuperannuationName { get; set; }
        public string FundABN { get; set; }
        public string FundAddress { get; set; }
        public string FundUSI { get; set; }
        public int? SuperannuationMemberNumber{ get; set; }
        public double? SalaryPerHours { get; set; }
        public double? SickLeaveRate { get; set; }
        public double? AnnualLeaveRate { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? LastWorkingDate { get; set; }
    }
}
