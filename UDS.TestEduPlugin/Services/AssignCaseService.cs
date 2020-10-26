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

        public KeyValuePair<AliasedValue, int> GetOwner(OptionSetValue caseOrigin)
        {
            TeamRepository teamRepository = new TeamRepository(_service);
            Entity team = teamRepository.GetTeams(caseOrigin.Value).Entities[0];
            EntityReference teamEntity = (EntityReference)team["new_team"];

            UsersRepository usersRepository = new UsersRepository(_service);
            var users = usersRepository.GetUsers(teamEntity.Id);

            CaseRepository caseRepository = new CaseRepository(_service);

            Dictionary<AliasedValue, int> keyValuePairs = new Dictionary<AliasedValue, int>();

            for (int i = 0; i < users.Count; i++)
            {
                AliasedValue id = (AliasedValue)users[i].Attributes["systemuser2.systemuserid"];
                int count = caseRepository.GetCasesCountByUsers(id.Value.ToString());
                keyValuePairs.Add(id, count);
            }

            int minValue = keyValuePairs.Values.Min();

            var newOwner = keyValuePairs.Where(q => q.Value == minValue).FirstOrDefault();

            return newOwner;
        }

    }
}
