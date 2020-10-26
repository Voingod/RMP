using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDS.VoPlugin.Repository;
using UDS.VoPlugin.Services;

namespace UDS.VoPlugin.Steps
{
    public class CaseCreateStep : Plugin
    {
        public CaseCreateStep()
        : base(typeof(RetrieveMultipleStep))
        {
            base.RegisteredEvents
                .Add(new Tuple<int, string, string, Action<LocalPluginContext>>((int)PluginStage.PostOperation, "Create",
                    "incident",
                    CreateStep));
        }
        protected void CreateStep(LocalPluginContext localContext)
        {

            Entity target = (Entity)localContext.PluginExecutionContext.InputParameters["Target"];
            OptionSetValue caseOriginCode = target.GetAttributeValue<OptionSetValue>("caseorigincode");

            if (caseOriginCode == null)
            {
                return;
            }

            IOrganizationService service = localContext.OrganizationService;
            AssignCaseService assignCaseService = new AssignCaseService(service);

            var newOwner = assignCaseService.GetOwner(caseOriginCode);

            localContext.Trace("AAAAAAAAAAAAAAAAAAAAAAAA");
            localContext.Trace("ttttttttttttttt"+newOwner.ToString()+"ttttttttttttttttttttt");

            Entity entity = new Entity(target.LogicalName, target.Id);
            entity["ownerid"] = new EntityReference("systemuser", newOwner);
            service.Update(entity);

            //target["ownerid"] = new EntityReference("systemuser", newOwner);

        }
    }
}
