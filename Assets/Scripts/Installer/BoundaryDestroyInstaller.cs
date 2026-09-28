
using Pool;
using Zenject;

namespace Installer
{
    public class BoundaryDestroyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AsteroidPool>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}