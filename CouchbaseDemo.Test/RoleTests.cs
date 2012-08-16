using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DemoModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CouchbaseDemo.Test
{
    [TestClass]
    public class RoleTests
    {
        [TestMethod]
        public void GetUsersByRoleTest()
        {
            /*
            * The goal of this method is to find users with in a specific role.
            * Remember if you just inserted a role the view may not have indexed the record and this method may fail
            * Read about eventual consistency here: http://en.wikipedia.org/wiki/Eventual_consistency
            */

            try
            {
                var users = Role.GetUsersByRole("Admin");
                Assert.IsTrue(users != null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
