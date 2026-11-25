
using Pool;
using Zenject;

namespace Installer
{
    public class PoolInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AsteroidPool>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container.Bind<BulletPool>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}