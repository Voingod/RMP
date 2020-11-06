using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDS.VoPlugin.Steps
{
    public class RetrieveStep : Plugin
    {
        public RetrieveStep()
: base(typeof(RetrieveMultipleStep))
        {
            base.RegisteredEvents
                .Add(new Tuple<int, string, string, Action<LocalPluginContext>>((int)PluginStage.PreOperation, "Retrieve",
                    "account",
                    Retrieve));
        }
        protected void Retrieve(LocalPluginContext localContext)
        {

            //if (!localContext.PluginExecutionContext.OutputParameters.Contains("BusinessEntity"))
            //{
            //    return;
            //}
            if (!localContext.PluginExecutionContext.InputParameters.Contains("Target"))
            {
                return;
            }
            Entity target = (Entity)localContext.PluginExecutionContext.InputParameters["Target"];
            //Entity businessEntity = (Entity)localContext.PluginExecutionContext.OutputParameters["BusinessEntity"];

            //OptionSetValue caseOriginCode = target.GetAttributeValue<OptionSetValue>("caseorigincode");


        }
    }
}
