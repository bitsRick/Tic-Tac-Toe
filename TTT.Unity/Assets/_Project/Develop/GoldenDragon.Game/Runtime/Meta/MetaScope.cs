using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using VContainer;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta
{
    public class MetaScope:LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<FactoryMetaUi>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<MetaFlow>();
        }
    }
}