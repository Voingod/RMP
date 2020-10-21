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
            string name = ParseQueryData(query)?.Item1;
            string parametrs = ParseQueryData(query)?.Item2;

            result.Entities.Add(new Entity()
            {
                Attributes =
                {
                    ["new_simpletext"]="Test",
                    ["new_queryname"]=name,
                    ["new_queryparams"]=parametrs
                }
            });
            localContext.PluginExecutionContext.OutputParameters["BusinessEntityCollection"] = result;

            var methods = GetMethods(name, CreateContact);
           
        }
        private Tuple<string, string> ParseQueryData(FetchExpression fetchExpression)
        {
            string querySchemeName = "new_queryname";
            string querySchemeParams = "new_queryparams";

            XDocument parsedQuery = XDocument.Parse(fetchExpression.Query);

            Dictionary<string, string> requestParameters = parsedQuery
                .Descendants("condition")
                .Where(e =>
                    e.Attribute("attribute") != null &&
                    e.Attribute("operator") != null &&
                    e.Attribute("value") != null &&
                    String.Equals(e.Attribute("operator")?.Value, "eq", StringComparison.InvariantCultureIgnoreCase))
                .ToDictionary(e => e.Attribute("attribute")?.Value, e => e.Attribute("value")?.Value);

            if (requestParameters.TryGetValue(querySchemeName, out string queryName) &&
                !string.IsNullOrEmpty(queryName) &&
                requestParameters.TryGetValue(querySchemeParams, out string queryParams))
            {
                if (queryParams.Length % 4 == 0)
                    queryParams = Encoding.UTF8.GetString(Convert.FromBase64String(queryParams));
                else
                {
                    queryParams = queryParams.Trim().Replace(" ", "+");
                    queryParams = queryParams.PadRight(queryParams.Length + 4 - queryParams.Length % 4, '=');
                    try
                    {
                        queryParams = Encoding.UTF8.GetString(Convert.FromBase64String(queryParams));
                    }
                    catch (FormatException)
                    {
                        queryParams = "None";
                    }                    
                }
                return Tuple.Create(queryName, queryParams);
            }
            return null;
        }
        private Dictionary<string, Func<string, string>> GetMethods(string queryName, Func<string, string> create)
        {
            Dictionary<string, Func<string, string>> createContactParametrs = new Dictionary<string, Func<string, string>>
            {
                [queryName] = create
            };
            return createContactParametrs;
        }
        private string CreateContact(string queryParams)
        {
            return queryParams;
        }
    }
}
