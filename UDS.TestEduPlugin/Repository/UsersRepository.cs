using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDS.VoPlugin.Repository
{
    public class UsersRepository
    {
        private IOrganizationService _service;


        private const string FirstEntityName = "systemuser";
        private const string SecondEntityName = "team";
        private const string LinkEntityName = "teammembership";
        public UsersRepository(IOrganizationService service)
        {
            _service = service;
        }
        public DataCollection<Entity> GetUsers(Guid teamId)
        {
            var query = new QueryExpression(FirstEntityName)
            {
                ColumnSet = new ColumnSet("fullname", "systemuserid"),
                LinkEntities =
                {
                    new LinkEntity(FirstEntityName, LinkEntityName, "systemuserid", "systemuserid", JoinOperator.Inner)
                  {
                      LinkEntities =
                      {
                        new LinkEntity(LinkEntityName, SecondEntityName, "teamid", "teamid", JoinOperator.Inner)
                        {
                            Columns = new ColumnSet("name"),
                            LinkCriteria = new FilterExpression()
                            {
                                  Conditions =
                                  {
                                      new ConditionExpression("teamid",ConditionOperator.Equal,teamId)
                                  }
                            },
                        }
                      }
                  }
                }
            };
            var records = _service.RetrieveMultiple(query);
            return records.Entities;
        }
    }
}
