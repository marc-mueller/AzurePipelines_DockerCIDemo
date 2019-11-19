namespace DevFun.Web.Options
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "ok for sample")]
    public class DevFunOptions
    {
        public string ApiUrl { get; internal set; }
        public string DeploymentEnvironment { get; internal set; }
        public string AlternateTestingUrl { get; internal set; }
        public bool FlagEnableAlternateUrl { get; internal set; }
    }
}