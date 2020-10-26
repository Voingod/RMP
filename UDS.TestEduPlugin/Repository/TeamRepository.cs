using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDS.VoPlugin.Repository
{
    public class TeamRepository
    {
        private IOrganizationService _service;


        private const string FirstEntityName = "new_teamandcasetype";
        public TeamRepository(IOrganizationService service)
        {
            _service = service;
        }
        public EntityCollection GetTeams(int origin)
        {
            var query = new QueryExpression(FirstEntityName)
            {
                ColumnSet = new ColumnSet("new_casetype", "new_team"),
                Criteria = new FilterExpression()
                {
                    Conditions =
                  {
                      new ConditionExpression("new_casetype",ConditionOperator.Equal,origin)
                  }
                }
            };
            var records = _service.RetrieveMultiple(query);
            return records;
        }
    }
}
