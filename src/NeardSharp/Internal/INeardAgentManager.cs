using System.Threading.Tasks;
using Tmds.DBus;

namespace NeardSharp.Internal
{
    [DBusInterface("org.neard.AgentManager")]
    public interface INeardAgentManager : IDBusObject
    {
        Task RegisterHandoverAgentAsync(ObjectPath Path, string Type);
        Task UnregisterHandoverAgentAsync(ObjectPath Path, string Type);
        Task RegisterNDEFAgentAsync(ObjectPath Path, string Type);
        Task UnregisterNDEFAgentAsync(ObjectPath Path, string Type);
    }
}
