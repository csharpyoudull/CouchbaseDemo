using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DemoModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CouchbaseDemo.Test
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUserTestPass()
        {
            /*
             * The goal of this test is to successfully create a user.
             */

            try
            {

                var user = new User
                               {
                                   EMailAddress = "test@somehost.com",
                                   FirstName = "Bob",
                                   LastName = "Marley",
                                   PurchaseCount = 12,
                                   AccountCreateDate = DateTime.Now
                               };

                user.Create();

                var result = User.GetUserByEmailAddress("test@somehost.com");
                Assert.IsTrue(result != null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void CreateUserTestFail()
        {
            /*
             * Execute CreateUserTestPass first.
             * The goal of this test is to successfully create a user.
             */

            try
            {

                var user = new User
                {
                    EMailAddress = "test@somehost.com",
                    FirstName = "Bob",
                    LastName = "Marley",
                    AccountCreateDate = DateTime.Now
                };

                user.Create();
                Assert.Fail("User was successfully created this should have failed.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("exists"),ex.Message);
            }
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            /*
             * The goal of this test is to get a user and add the admin role
             * then check to see if the role was added to the user.
             */

            try
            {
                var user = User.GetUserByEmailAddress("test@somehost.com");
                
                if (user == null)
                    Assert.Fail("User not found!");

                user.Roles.Add(new Role{RoleName = "Admin",RoleDescription = "Network admin role."});
                user.Update();

                user = User.GetUserByEmailAddress("test@somehost.com");
                Assert.IsTrue(user.Roles.Any());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            /*
             * The goal of this method is to delete the user.
             */
            try
            {
                var user = User.GetUserByEmailAddress("test@somehost.com");
                user.Delete();

                Assert.IsTrue(User.GetUserByEmailAddress("test@somehost.com") == null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void GetUsersByPurchaseCountTest()
        {
            /*
             * The goal of this method is to find users with a pruchase count that falls in specific range.
             * Remember if you just inserted a user the view may not have indexed the record and this method may fail
             * Read about eventual consistency here: http://en.wikipedia.org/wiki/Eventual_consistency
             */

            try
            {
                var users = User.GetUsersByPurchaseCountRange(10, 14);
                Assert.IsTrue(users != null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }
}
