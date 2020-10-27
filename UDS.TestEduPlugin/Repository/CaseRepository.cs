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
            string fetch = @"
                <fetch aggregate='true' >
                  <entity name='incident' >
                    <attribute name='caseorigincode' alias='caseorigincodesum' aggregate='count' />
                    <filter type='and' >
                      <condition attribute='ownerid' operator='in' >
                        <value uitype='systemuser' >" + owner + "</value>" +
                      "</condition>" +
                      "<condition attribute='statecode' operator='eq' value='0' /> " +
                    "</filter> " +
                  "</entity> " +
                "</fetch>";

            //var query = new QueryExpression(FirstEntityName)
            //{
            //    ColumnSet = new ColumnSet(true),
            //    Criteria = new FilterExpression(LogicalOperator.Or)
            //    {
            //        Conditions =
            //      {
            //          new ConditionExpression("ownerid",ConditionOperator.Equal,owner)
            //      }
            //    }
            //};

            int records = (int)_service.RetrieveMultiple(new FetchExpression(fetch))
                                                .Entities.FirstOrDefault()
                                                .GetAttributeValue<AliasedValue>("caseorigincodesum").Value;
            return records;
        }

    }
}