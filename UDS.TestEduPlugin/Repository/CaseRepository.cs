using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDS.VoPlugin.Repository
{
    public class CaseRepository
    {
        private IOrganizationService _service;


        private const string FirstEntityName = "incident";
        public CaseRepository(IOrganizationService service)
        {
            _service = service;
        }
        public int GetCasesCountByUsers(string owner)
        {
            var query = new QueryExpression(FirstEntityName)
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression(LogicalOperator.Or)
                {
                    Conditions =
                  {
                      new ConditionExpression("ownerid",ConditionOperator.Equal,owner)
                  }
                }
            };
            var records = _service.RetrieveMultiple(query);
            return records.Entities.Count;
        }

    }
}