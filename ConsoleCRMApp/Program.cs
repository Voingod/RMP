using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CrmEarlyBound;
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
            string queryParams = "ZXJnZmhmZ";


            if (queryParams.Length % 4 == 0)
                queryParams = Encoding.UTF8.GetString(Convert.FromBase64String(queryParams));
            else
            {
                queryParams = queryParams.Trim().Replace(" ", "+");
                queryParams = queryParams.PadRight(queryParams.Length + 4 - queryParams.Length % 4, '=');
                queryParams = Encoding.UTF8.GetString(Convert.FromBase64String("ZXJnZmhmZ=="));
            }






            var qwe = GetParametrs("qwe", CreateContact);
            var req = qwe["qwe"]?.Invoke("testqwewqewqewqe");
            Console.WriteLine(queryParams);
            //OrganizationServiceProxy serviceProxy = ConnectHelper.CrmService;
            //var service = (IOrganizationService)serviceProxy;
            //serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());

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
        private static Dictionary<string, Func<string, string>> GetParametrs(string queryName, Func<string, string> create)
        {
            Dictionary<string, Func<string, string>> createContactParametrs = new Dictionary<string, Func<string, string>>
            {
                [queryName] = create
            };
            return createContactParametrs;
        }
        private static string CreateContact(string queryParams)
        {
            return queryParams;
        }
    }

}
