using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDS.VoPlugin
{
    public static class Actions
    {
        public static string GetMessage(MessageName message) => Enum.GetName(typeof(MessageName), message);
    }
    public enum MessageName
    {
        Create, Update, Delete, Assign, Associate, SetState, SetStateDynamicEntity
    }
}
