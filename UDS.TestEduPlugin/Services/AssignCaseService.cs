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

            Guid? newOwner = caseRepository.GetCasesCountByUsers(users);

            if (newOwner == null)
            {
                throw new InvalidPluginExecutionException("Doesn't have E-mail");
            }

            return newOwner == null ? users.FirstOrDefault().Id : (Guid)newOwner;

        }

    }
}
