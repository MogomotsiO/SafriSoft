﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SafriSoftv1._3.Models;
using SafriSoftv1._3.Models.Data;
using SafriSoftv1._3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SafriSoftv1._3.Controllers.API
{
    [RoutePrefix("api/SafriSoft")]
    public class SafriSoftController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpPost, Route("GetStarted")]
        public async Task<IHttpActionResult> GetStarted(OrganisationViewModel org)
        {
            ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            string userId = IdentityExtensions.GetUserId(User.Identity);

            var success = false;
            var message = string.Empty;
            var resultl = new Organisation();
            SafriSoftDbContext db = new SafriSoftDbContext();
            var organisation = new Organisation();
            try
            {
                resultl = db.Organisations.FirstOrDefault(x => x.OrganisationName == org.OrganisationName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            if (resultl != null)
            {
                success = false;
                message = "Company name is already taken";
            }

            var result2 = db.Organisations.FirstOrDefault(x => x.OrganisationEmail == org.OrganisationEmail);

            if (result2 != null)
            {
                success = false;
                message = "Company email is already in use";
            }

            if (resultl == null && result2 == null)
            {
                organisation.OrganisationName = org.OrganisationName;
                organisation.OrganisationEmail = org.OrganisationEmail;
                organisation.OrganisationCell = org.OrganisationCell;
                organisation.OrganisationStreet = org.OrganisationStreet;
                organisation.OrganisationProvince = org.OrganisationProvince;
                organisation.OrganisationCity = org.OrganisationCity;
                organisation.OrganisationCode = org.OrganisationCode;
                organisation.PackageId = 1;

                db.Organisations.Add(organisation);
                var rowsAffected = await db.SaveChangesAsync();

                if(organisation.OrganisationId != 0)
                {
                    foreach (var software in org.SelectedSoftwares)
                    {
                        var organisationSoftware = new OrganisationSoftware();
                        organisationSoftware.OrganisationId = organisation.OrganisationId;
                        organisationSoftware.SoftwareId = db.Softwares.Where(x => x.Name == software).Select(x => x.Id).FirstOrDefault();
                        organisationSoftware.Granted = true;

                        if (software == "inventory")
                            organisationSoftware.PackageId = 1;
                        if (software == "rental")
                            organisationSoftware.PackageId = 6;
                        if (software == "ticket")
                            organisationSoftware.PackageId = 0;

                        db.OrganisationSoftwares.Add(organisationSoftware);
                        await db.SaveChangesAsync();
                    }

                    var user = new ApplicationUser { UserName = org.OrganisationEmail, Email = org.OrganisationEmail };
                    var result = await userManager.CreateAsync(user, org.ConfirmedPassword);

                    if (result.Succeeded)
                    {
                        Claim OrganisationClaim = new Claim("Organisation", org.OrganisationName);
                        var saveClaim = await userManager.AddClaimAsync(user.Id, OrganisationClaim);
                        Claim UsernameClaim = new Claim("Username", org.OrganisationEmail);
                        var saveUsernameClaim = await userManager.AddClaimAsync(user.Id, UsernameClaim);
                        var saveRole = userManager.AddToRole(user.Id, "SuperAdmin");

                        try
                        {
                            SafriSoftEmailService ems = new SafriSoftEmailService();
                            string[] to = { org.OrganisationEmail };
                            string[] cc = { };
                            var sb = new StringBuilder();
                            sb.Append("Thank your for choosing SafriSoft.<br /><br />");
                            sb.Append("Please use the below link(s) to access our software(s)<br/><br />");
                            foreach (var s in org.SelectedSoftwares)
                            {
                                if (s == "inventory")
                                    sb.Append("<a href='https://ims.safrisoft.com'>Inventory Management Software</a><br/>");
                                if (s == "rental")
                                    sb.Append("<a href='https://rms.safrisoft.com'>Rental Management Software</a><br/>");
                                if (s == "ticket")
                                    sb.Append("<a href='https://tms.safrisoft.com'>Ticket Management Software</a><br/>");
                            }

                            sb.Append("<br />");
                            sb.Append($"<strong>Username</strong>: {org.OrganisationEmail} <br/>");
                            sb.Append($"<strong>Password</strong>: {org.Password} <br/>");

                            var sendEmailResult = ems.SaveEmail("SafriSoft - Welcome", sb.ToString(), "support@safrisoft.com", to, cc);

                            success = true;
                            message = "All details have been saved successfully.";
                        } catch (Exception ex)
                        {
                            success = false;
                            message = ex.Message;
                        }
                        
                    }
                    else
                    {
                        success = false;
                        message = "Company details have been saved but the user could not be created. Please contact support.";
                    }
                }
                else
                {
                    success = false;
                    message = "Could not save the company details. Please contact support.";
                }
            }            
            
            return Json(new { Success = success, message = message });
        }
    }
}