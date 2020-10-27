using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDS.VoPlugin.Repository;

namespace UDS.VoPlugin.Services
{
    public class AssignCaseService
    {
        private IOrganizationService _service;

        public AssignCaseService(IOrganizationService service)
        {
            _service = service;
        }

        public Guid GetOwner(OptionSetValue caseOriginCode)
        {
            TeamRepository teamRepository = new TeamRepository(_service);
            Entity team = teamRepository.GetTeams(caseOriginCode.Value).Entities.FirstOrDefault();

            if (team == null)
            {
                return new Guid();
            }

            EntityReference teamEntityId = team.GetAttributeValue<EntityReference>("new_team");

            UsersRepository usersRepository = new UsersRepository(_service);

            var users = usersRepository.GetUsers(teamEntityId.Id);

            CaseRepository caseRepository = new CaseRepository(_service);

            Dictionary<Guid, int> keyValuePairs = new Dictionary<Guid, int>();

            for (int i = 0; i < users.Count; i++)
            {
                Guid id = users[i].Id;
                int count = caseRepository.GetCasesCountByUsers(id.ToString());
                keyValuePairs.Add(id, count);
            }

            int minValue = keyValuePairs.Values.Min();

            var newOwner = keyValuePairs.Where(q => q.Value == minValue).FirstOrDefault();

            return newOwner.Key;

        }

    }
}
