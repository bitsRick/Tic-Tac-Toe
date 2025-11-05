using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style;
using VContainer;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta
{
    public class MetaScope:LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PopupService>(Lifetime.Singleton);
            builder.Register<StyleDataLoad>(Lifetime.Singleton);
            builder.Register<MetaFactoryUi>(Lifetime.Scoped);
            builder.Register<MetaFactoryItem>(Lifetime.Scoped);
            builder.Register<MetaProviderFacadeFactory>(Lifetime.Scoped);
            builder.Register<SessionDataMatch>(Lifetime.Singleton);

            builder.Register<Model>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<MetaFlow>();
            
        }
    }
}