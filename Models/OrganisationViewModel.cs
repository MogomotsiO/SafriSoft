using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafriSoftv1._3.Models
{
    public class OrganisationViewModel
    {
        public int OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public string OrganisationEmail { get; set; }
        public string OrganisationCell { get; set; }
        public string OrganisationLogo { get; set; }
        public string OrganisationStreet { get; set; }
        public string OrganisationSuburb { get; set; }
        public string OrganisationProvince { get; set; }
        public string OrganisationCity { get; set; }
        public int OrganisationCode { get; set; }
        public string AccountName { get; set; }
        public int AccountNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string ClientReference { get; set; }
        public int VATNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string[] SelectedSoftwares { get; set; }

    }

    public class BookingDetailsVm
    {
        public string email { get; set; }
    }
}