using System;
using System.Management.Automation;

namespace RKL.Sysman.Logging
{
    [Cmdlet(VerbsCommon.Add, "SysmanErrorMessage")]
    [OutputType(typeof(SysmanLogMessage))]
    public class AddSyslogErrorMessage : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        public string Message { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public SysmanLogMessage LogObject { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (LogObject == null)
            {
                throw new ArgumentNullException(nameof(LogObject));
            }
            LogObject.AddErrorEntry(Message);
            if (MyInvocation.PipelinePosition < MyInvocation.PipelineLength)
            {
                WriteObject(LogObject);
            }

        }
    }
}
