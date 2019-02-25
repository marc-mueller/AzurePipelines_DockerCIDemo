using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFun.Api.DTOs
{
    public class StatusResponse
    {
        public string AssemblyInfoVersion { get; set; }
        public string MachineName { get; set; }
        public string AssemblyVersion { get; set; }
        public string AssemblyFileVersion { get; set; }
        public string DeploymentEnvironment { get; set; }
    }
}
