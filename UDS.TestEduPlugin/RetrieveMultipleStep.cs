using CrmEarlyBound;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UDS.VoPlugin
{
    public class RetrieveMultipleStep : Plugin
    {
        public RetrieveMultipleStep()
           : base(typeof(RetrieveMultipleStep))
        {
            base.RegisteredEvents
                .Add(new Tuple<int, string, string, Action<LocalPluginContext>>((int)PluginStage.PostOperation, "RetrieveMultiple",
                    "new_tocreatecontact",
                    PullThePlugin));
        }
        protected void PullThePlugin(LocalPluginContext localContext)
        {
            if (!localContext.PluginExecutionContext.InputParameters.Contains("Query"))
            {
                return;
            }
            IOrganizationService service = localContext.OrganizationService;
            FetchExpression query = (FetchExpression)localContext.PluginExecutionContext.InputParameters["Query"];

            EntityCollection result = new EntityCollection();
            result.Entities.Add(new Entity()
            {
                Attributes =
                {
                    ["new_simpletext"]="Test"
                }
            });

            localContext.PluginExecutionContext.OutputParameters["BusinessEntityCollection"] = result;


            var fetchExpression = localContext.PluginExecutionContext.InputParameters["Query"] as FetchExpression;
            var fetchXmlToQueryExpressionRequest = new FetchXmlToQueryExpressionRequest()
            {
                FetchXml = fetchExpression.Query
            };
            var fetchXmlToQueryExpressionResponse = (FetchXmlToQueryExpressionResponse)service.Execute(fetchXmlToQueryExpressionRequest);



            var res = ParseQueryData(query);

            //string name = target.GetAttributeValue<string>("new_simpletext");
        }
        private Tuple<string, string> ParseQueryData(FetchExpression fetchExpression)
        {
            XDocument parsedQuery = XDocument.Parse(fetchExpression.Query);

            Dictionary<string, string> requestParameters = parsedQuery
                .Descendants("condition")
                .Where(e =>
                    e.Attribute("attribute") != null &&
                    e.Attribute("operator") != null &&
                    e.Attribute("value") != null &&
                    String.Equals(e.Attribute("operator")?.Value, "eq", StringComparison.InvariantCultureIgnoreCase))
                .ToDictionary(e => e.Attribute("attribute")?.Value, e => e.Attribute("value")?.Value);

            if (requestParameters.TryGetValue("new_name", out string queryName) &&
                !string.IsNullOrEmpty(queryName) &&
                requestParameters.TryGetValue("new_name", out string queryParams))
            {
                queryParams = Encoding.UTF8.GetString(Convert.FromBase64String(queryParams));
                return Tuple.Create(queryName, queryParams);
            }
            return null;
        }
    }
}
