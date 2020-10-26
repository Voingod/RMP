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


        private const string FirstEntityName = "team";
        private const string SecondEntityName = "systemuser";
        private const string LinkEntityName = "teammembership";
        public UsersRepository(IOrganizationService service)
        {
            _service = service;
        }
        public DataCollection<Entity> GetUsers(Guid teamId)
        {
            var query = new QueryExpression(FirstEntityName)
            {
                ColumnSet = new ColumnSet("name"),
                Criteria = new FilterExpression()
                {
                    Conditions =
                  {
                      new ConditionExpression("teamid",ConditionOperator.Equal,teamId)
                  }
                },
                LinkEntities =
                {
                  new LinkEntity(FirstEntityName, LinkEntityName, "teamid", "teamid", JoinOperator.Inner)
                  {
                      LinkEntities =
                      {
                        new LinkEntity(LinkEntityName, SecondEntityName, "systemuserid", "systemuserid", JoinOperator.Inner)
                        {
                            Columns = new ColumnSet("fullname","systemuserid"),
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
