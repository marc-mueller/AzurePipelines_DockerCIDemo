namespace DevFun.Web.Options
{
    public class DevFunOptions
    {
        public string ApiUrl { get; internal set; }
        public string DeploymentEnvironment { get; internal set; }
        public string AlternateTestingUrl { get; internal set; }
        public bool FlagEnableAlternateUrl { get; internal set; }
    }
}