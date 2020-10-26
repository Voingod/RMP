using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace UDS.VoPlugin.Repository
{
    public class VoMainScriptRepository
    {
        private IOrganizationService _service;


        private const string FirstEntityName = "new_vo_main_script";
        private const string SecondEntityName = "new_vo_two_main";
        private const string LinkEntityName = "new_new_vo_two_main_new_vo_main_script";
        public VoMainScriptRepository(IOrganizationService service)
        {
            _service = service;
        }
        public DataCollection<Entity> GetEntities()
        {
            var query = new QueryExpression(FirstEntityName)
            {
                ColumnSet = new ColumnSet("new_name", "createdon", "new_account", "statecode"),
                Criteria = new FilterExpression()
                {
                    Conditions =
                  {
                      new ConditionExpression("createdon",ConditionOperator.OlderThanXDays,2)
                  }
                },
                LinkEntities =
                {
                  new LinkEntity(FirstEntityName, LinkEntityName, "new_vo_main_scriptid", "new_vo_main_scriptid", JoinOperator.Inner)
                  {
                      LinkEntities =
                      {
                        new LinkEntity(LinkEntityName, SecondEntityName, "new_vo_two_mainid", "new_vo_two_mainid", JoinOperator.Inner)
                        {
                            Columns = new ColumnSet("new_name", "createdon", "new_account"),
                        }
                      }
                  }
                }
            };
            var records = _service.RetrieveMultiple(query);
            return records.Entities;
        }




    }
    public class CustomEntity
    {
        public int VoSriptState { get; set; }
        public DateTime? VoScriptData { get; set; }
        public Guid? VoScriptId { get; set; }
        public EntityReference VoScriptAcc { get; set; }
        public string VoScriptName { get; set; }
        public Guid? VoTwoId { get; set; }
        public EntityReference VoTwoAcc { get; set; }
        public string VoTwoIdName { get; set; }
    }
}
