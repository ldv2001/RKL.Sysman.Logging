using Newtonsoft.Json;
using System;
using System.Management.Automation;

namespace RKL.Sysman.Logging
{
    [Cmdlet(VerbsData.ConvertTo, "SysmanLogRequestBody")]
    [OutputType(typeof(string))]
    public class ConvertToSysmanLogRequestBody : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public SysmanLogMessage LogObject { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (LogObject == null)
            {
                throw new ArgumentNullException(nameof(LogObject));
            }

            var tmpObject = new
            {
                LogObject.Text,
                LogObject.Status,
                LogObject.Source,
                LogObject.Method,
                LogObject.MethodVersion,
                LogObject.ActionId
            };
            WriteObject(JsonConvert.SerializeObject(tmpObject, Formatting.Indented));
        }
    }
}
