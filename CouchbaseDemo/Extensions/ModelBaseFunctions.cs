using Newtonsoft.Json;

namespace CouchbaseDemo.Extensions
{
    public static class ModelBaseFunctions
    {
        /// <summary>
        /// Serializes any object that inherits ModelBase to json.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ToJson(this ModelBase model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
