using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDS.VoPlugin
{
    class RetrieveMultipleStep : Plugin
    {
        public RetrieveMultipleStep()
           : base(typeof(RetrieveMultipleStep))
        {
            base.RegisteredEvents
                .Add(new Tuple<int, string, string, Action<LocalPluginContext>>((int)PluginStage.PreOperation, "RetrieveMultiple",
                    "new_tocreatecontact",
                    PullThePlugin));
        }
        protected void PullThePlugin(LocalPluginContext localContext)
        {
            if (!localContext.PluginExecutionContext.InputParameters.Contains("Target"))
            {
                return;
            }
            Entity target = (Entity)localContext.PluginExecutionContext.InputParameters["Target"];
            //string name = target.GetAttributeValue<string>("new_simpletext");
            target["new_simpletext"] = "Test";
        }
    }
}
