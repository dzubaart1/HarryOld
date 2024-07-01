using System.Threading.Tasks;

namespace HarryPoter.Core
{
    public interface IService
    {
        public Task Initialize();
        public void Destroy();
    }
}