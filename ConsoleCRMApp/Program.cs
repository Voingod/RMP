using System;
using System.IO;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KostenVoranSchlagConsoleParser.Helpers;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using UDS.VoPlugin;
using UDS.VoPlugin.Repository;

namespace ConsoleCRMApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //OrganizationServiceProxy serviceProxy = ConnectHelper.CrmService;
            //var service = (IOrganizationService)serviceProxy;
            //serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());

            IOrganizationService organizationService = null;

            try
            {
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = "trialadmindemo@udstrialsdemo40.onmicrosoft.com";
                clientCredentials.UserName.Password = "EMsBRRH5k5txusuf";
                
                organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri("https://udstrialsdemo40.api.crm4.dynamics.com/XRMServices/2011/Organization.svc"),
                    null,clientCredentials,null);

                RetrieveRecordChangeHistoryRequest changeRequest = new RetrieveRecordChangeHistoryRequest();
                changeRequest.Target = new EntityReference("account", new Guid("{B0B2DD8A-F30D-EB11-A813-000D3A666701}"));

                RetrieveRecordChangeHistoryResponse changeResponse =
                (RetrieveRecordChangeHistoryResponse)organizationService.Execute(changeRequest);

                AuditDetailCollection details = changeResponse.AuditDetailCollection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            #region Task14Testing

            //            TempRepository tempRepository = new TempRepository(service);
            //            var vo = tempRepository.GetVoMainScriptRecors().Entities;
            //            foreach (var item in vo)
            //            {
            //                Console.WriteLine(item.Id+" "+item.Attributes["new_name"]);
            //            }
            //            var entity = vo[0];
            //            var name = entity.LogicalName;
            //            var id = entity.Id;

            //            string path = @"D:\C# Junior собеседование\UDS Consulting (стажировка)\Developer Education\ЛК1.docx";
            //            string fileAsString = GetBase64StringFromFile(path);
            //            string fileName = Path.GetFileName(path);
            //            string mimeType = MimeMapping.GetMimeMapping(fileName);

            //            Entity Note = new Entity("annotation");
            //            Note["objectid"] = new EntityReference(name, id);
            //            Note["objecttypecode"] = name;
            //            Note["subject"] = "Test Subject";
            //            Note["notetext"] = "Test note text";

            //            Note["documentbody"] = fileAsString;
            //            Note["mimetype"] = mimeType;
            //            Note["filename"] = fileName;

            //            service.Create(Note);
            #endregion

            Console.ReadLine();

        }
    }

}
