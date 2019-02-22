using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFun.Web.Options
{
    public class DevFunOptions
    {
        public string ApiUrl { get; internal set; }
        public string DeploymentEnvironment { get; internal set; }
    }
}
