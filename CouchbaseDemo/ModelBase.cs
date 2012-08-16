using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CouchbaseDemo
{
    public class ModelBase
    {
        public string Type { get { return GetType().Name; } }
    }
}
