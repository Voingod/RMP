using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace UDS.VoPlugin
{
    public class CreateMethods
    {

        public Dictionary<string, Func<string, Entity>> methods = new Dictionary<string, Func<string, Entity>>();

        public CreateMethods()
        {
            methods.Add("CreateContact", CreateContact);
            methods.Add("CreateAccount", CreateAccount);
        }
        private Entity CreateContact(string queryParams)
        {
            Random random = new Random();
            
            Entity contact = new Entity("contact");
            contact["lastname"] = "Surname_" + random.Next(0, 500);
            contact["firstname"] = "Name_" + random.Next(0, 500); 
            contact["parentcustomerid"] = new EntityReference("account", new Guid(queryParams));
            return contact;
        }
        private Entity CreateAccount(string queryParams)
        {
            Console.WriteLine(queryParams);
            return new Entity();
        }
    }

}
