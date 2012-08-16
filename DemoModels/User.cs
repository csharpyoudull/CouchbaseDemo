using System;
using System.Collections.Generic;

using CouchbaseDemo;

namespace DemoModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class User:ModelBase
    {
        /// <summary>
        /// Gets or sets the E mail address.
        /// </summary>
        /// <value>The E mail address.</value>
        /// <remarks></remarks>
        public string EMailAddress { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        /// <remarks></remarks>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        /// <remarks></remarks>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the account create date.
        /// </summary>
        /// <value>The account create date.</value>
        /// <remarks></remarks>
        public DateTime AccountCreateDate { get; set; }

        /// <summary>
        /// Gets or sets the purchase count.
        /// </summary>
        /// <value>The purchase count.</value>
        /// <remarks></remarks>
        public int PurchaseCount { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        /// <remarks></remarks>
        public List<Role> Roles { get; set; }

        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        /// <remarks></remarks>
        public User()
        {
            Roles = new List<Role>();
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <remarks></remarks>
        public void Create()
        {
            /*
             * First check to see if a user with the same e-mail address exists
             * if a user exists throw an exception.
             */

            var db = new DataAccess();
            var existingUser = db.Get<User>(EMailAddress);

            if (existingUser != null)
            {
                throw new Exception("User with this e-mail address already exists.");
            }

            db.Insert(EMailAddress, this);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <remarks></remarks>
        public void Delete()
        {
            var db = new DataAccess();
            db.Delete(EMailAddress);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <remarks></remarks>
        public void Update()
        {
            var db = new DataAccess();
            db.Update(EMailAddress, this);
        }

        /// <summary>
        /// Gets the user by email address.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static User GetUserByEmailAddress(string emailAddress)
        {
            var db = new DataAccess();
            return db.Get<User>(emailAddress);
        }

        /// <summary>
        /// Gets the users by purchase count range.
        /// </summary>
        /// <param name="minCount">The min count.</param>
        /// <param name="maxCount">The max count.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<User> GetUsersByPurchaseCountRange(int minCount, int maxCount)
        {
            var db = new DataAccess();
            return db.ExecuteView<User>("GetUsersByPurchaseCountRange", minCount, maxCount);
        }
    }
}
