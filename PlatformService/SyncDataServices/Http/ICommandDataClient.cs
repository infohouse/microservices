using System.Threading.Tasks;
using PlatformService.Dtos;

namespace PlaformService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}