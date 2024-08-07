using System.Management.Automation;

namespace RKL.Sysman.Logging
{
    [Cmdlet(VerbsCommon.New,"SysmanLogMessage")]
    [OutputType(typeof(SysmanLogMessage))]
    public class TestSampleCmdletCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        public string Source { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
        public string Method { get; set; }

        [Parameter(Mandatory = true, Position = 2, ValueFromPipelineByPropertyName = true)]
        public string Version { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public long? ActionId { get; set; }


        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            WriteObject(new SysmanLogMessage(Source, Method, Version, ActionId));
        }
    }
}
