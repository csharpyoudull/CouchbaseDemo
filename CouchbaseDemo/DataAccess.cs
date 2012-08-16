using System;
using System.Collections.Generic;
using System.Linq;
using Couchbase;
using CouchbaseDemo.Extensions;
using Enyim.Caching.Memcached;

namespace CouchbaseDemo
{

    /*
     * To run this demo you will need Couchbase 2.0 preview 4 available at http://www.couchbase.com/download
     * 
     * I'm using version 1.2 of the couchbase client available at http://www.couchbase.com/develop/net/next along with some great documentation.
     *
     * The map / reduce functions are available in the MapReduce.js file,
     * for information on creating views and indexes http://www.couchbase.com/docs/couchbase-manual-2.0/couchbase-views.html.
     * 
     * Code by: Steve Ruben, @CSharpYouDull - twitter
     * 
     * This code and information are provided "as is" without warranty of any kind, either expressed or implied, 
     * including but not limited to the implied warranties of merchantability and/or fitness for a particular purpose.
     */

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class DataAccess
    {
        /// <summary>
        /// The singleton couchbase client instance.
        /// </summary>
        private static CouchbaseClient _client;

        /// <summary>
        /// Gets the couchbase client. Client is a static singleton, since the client is a 'smart client' each time the client is
        /// created it maps the nodes of the couchbase cluster the operation can take time so we want to ensure this is not happening
        /// over and over killing performance.
        /// </summary>
        /// <remarks></remarks>
        private static CouchbaseClient Client
        {
            get { return _client ?? (_client = new CouchbaseClient()); }
        }

        /// <summary>
        /// Inserts the model with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="model">The model.</param>
        /// <remarks></remarks>
        public void Insert(string key, ModelBase model)
        {
            Client.Store(StoreMode.Add, key, model.ToJson());
        }

        /// <summary>
        /// Updates the data stored for given key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="model">The model.</param>
        /// <remarks></remarks>
        public void Update(string key, ModelBase model)
        {
            Client.Store(StoreMode.Set, key, model.ToJson());
        }

        /// <summary>
        /// Gets the model by the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public T Get<T>(string key) where T: class 
        {
            var json = Client.Get(key) as string;
            return string.IsNullOrEmpty(json) ? null : json.ToObject<T>();
        }

        /// <summary>
        /// Deletes the data for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <remarks></remarks>
        public void Delete(string key)
        {
            Client.Remove(key);
        }

        /// <summary>
        /// Executes the view.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="viewKey">The view key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public IEnumerable<T> ExecuteView<T>(string viewName, object viewKey)
        {
            var viewResults = Client.GetView(viewName, viewName).Key(viewKey);
            if (!viewResults.Any())
                return null;

            return (from result in viewResults select result.GetItem() as string).Select(item => item.ToObject<T>());
        }

        /// <summary>
        /// Executes the view.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="viewKeyStart">The view key start.</param>
        /// <param name="viewKeyEnd">The view key end.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public IEnumerable<T> ExecuteView<T>(string viewName, object viewKeyStart, object viewKeyEnd)
        {
            var viewResults = Client.GetView(viewName, viewName).StartKey(viewKeyStart).EndKey(viewKeyEnd);
            if (!viewResults.Any())
                return null;

            return (from result in viewResults select result.GetItem() as string).Select(item => item.ToObject<T>());
        }
    }
}
