using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KadGen.ClassTracker.Repository;
using KadGen.ClassTracker.Domain;
using System.Linq;

namespace ClassTracker.Repository.Specs
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDbContext()
        {
            using (var dbContext = new ClassTrackerDbContext())
            {
                var x = dbContext.Organizations.Create();
                dbContext.SaveChanges();
            }
        }

        [TestMethod]
        public void Get_organization()
        {
            var repo = new OrganizationRepository();
            const string orgName = "Fred's Bar and Grill";
            var id = GetOrgIdCreatingIfNeeded(repo, orgName);
            var organizationResult = repo.GetAsync(id);
            Assert.IsNotNull(organizationResult);
            var organization = organizationResult.Data;
            Assert.AreEqual(orgName, organization.Name);
        }

        private int GetOrgIdCreatingIfNeeded(OrganizationRepository repo, string name)
        {
            var orgResult = repo.GetAllAsync();
            Assert.IsTrue(orgResult.IsSuccessful);
            var orgs = orgResult.Data;
            var org = orgs
                            .Where(x => x.Name == name)
                            .SingleOrDefault();
            if (org == null)
            {
                org = new Organization(0, name);
                var createResult= repo.CreateAsync(org);
                Assert.IsTrue(createResult.IsSuccessful);
                return createResult.Data.Value;
            }
            return org.Id;
        }
    }
}
