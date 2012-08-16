using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CouchbaseDemo;

namespace DemoModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class Role:ModelBase
    {
        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>The name of the role.</value>
        /// <remarks></remarks>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the role description.
        /// </summary>
        /// <value>The role description.</value>
        /// <remarks></remarks>
        public string RoleDescription { get; set; }

        /// <summary>
        /// Gets the users by role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<User> GetUsersByRole(string roleName)
        {
            var db = new DataAccess();
            return db.ExecuteView<User>("GetUsersByRole", roleName);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <remarks></remarks>
        public void Create()
        {
            var db = new DataAccess();

            var existingRole = db.Get<Role>(RoleName);
            if (existingRole != null)
                throw new Exception("A role with that name already exists.");

            db.Insert(RoleName,this);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <remarks></remarks>
        public void Delete()
        {
            var db = new DataAccess();
            db.Delete(RoleName);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <remarks></remarks>
        public void Update()
        {
            var db = new DataAccess();
            db.Update(RoleName, this);
        }
    }
}
