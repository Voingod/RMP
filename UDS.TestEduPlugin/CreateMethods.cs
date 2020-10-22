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
            Entity contact = new Entity("contact");
            contact["lastname"] = "FirstName";
            contact["firstname"] = "SecondName";
            contact["parentcustomerid"] = new EntityReference("account", new Guid(queryParams));
            return contact;
        }
        private Entity CreateAccount(string queryParams)
        {
            Console.WriteLine(queryParams);
            return new Entity();
        }

        //public Dictionary<string, Func<string, Entity>> AddMethod(string methodName, Func<string, Entity> methodAction)
        //{
        //    Dictionary<string, Func<string, Entity>> methods = new Dictionary<string, Func<string, Entity>>
        //    {
        //        [methodName] = methodAction
        //    };
        //    return methods;
        //}
    }

}
