using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDS.VoPlugin.Repository
{
    public class VoMainScriptDeactivateRepository
    {
        private IOrganizationService _service;
        public VoMainScriptDeactivateRepository(IOrganizationService service)
        {
            _service = service;
        }
    }
}
