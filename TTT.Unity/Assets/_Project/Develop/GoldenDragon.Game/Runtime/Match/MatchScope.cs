using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai;
using VContainer;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match
{
    public class MatchScope:LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<RoundManager>(Lifetime.Singleton);

            builder.Register<Convolution>(Lifetime.Singleton);
            builder.Register<Calculation>(Lifetime.Singleton);
            builder.Register<Brains>(Lifetime.Singleton);
            builder.Register<IAi,UtilityAi>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<MatchFlow>();
        }
    }
}