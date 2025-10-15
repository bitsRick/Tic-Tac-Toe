using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using VContainer;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta
{
    public class MetaScope:LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Model>(Lifetime.Singleton);
            builder.Register<PopupService>(Lifetime.Singleton);
            builder.Register<FactoryMetaUi>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<MetaFlow>();
        }
    }
}