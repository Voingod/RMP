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
        public Guid? GetCasesCountByUsers(DataCollection<Entity> owner)
        {
            var query = new QueryExpression(FirstEntityName)
            {
                ColumnSet = new ColumnSet("ownerid"),
                Criteria = new FilterExpression(LogicalOperator.Or) { }
            };

            Conditions(ref query, owner);

            var recordsAll = _service.RetrieveMultiple(query);
                
            var records = recordsAll.Entities.GroupBy(u => u.Attributes["ownerid"])
                .Select(s => new
                {
                    MetricKey = (EntityReference)s.Key,
                    MetricCount = s.Count(),

                }).ToList();

            var ownerId = records.Where(q => q.MetricCount == records.Min(a => a.MetricCount)).FirstOrDefault();

            if (ownerId == null)
            {
                throw new InvalidPluginExecutionException("Doesn't have E-mail");
                return null;
            }
            else
            {
                return ownerId.MetricKey.Id;
            }

        }

        public QueryExpression Conditions(ref QueryExpression query, DataCollection<Entity> owner)
        {
            for (int i = 0; i < owner.Count; i++)
            {
                query.Criteria.AddCondition("ownerid", ConditionOperator.Equal, owner[i].Id);
            }
            return query;
        }

    }
}
