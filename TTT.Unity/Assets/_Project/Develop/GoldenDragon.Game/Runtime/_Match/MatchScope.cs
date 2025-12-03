using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai.CalculationParam;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.Style;
using VContainer;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match
{
    public class MatchScope:LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<RoundManager>(Lifetime.Scoped);
            builder.Register<Convolution>(Lifetime.Scoped);
            builder.Register<Calculation>(Lifetime.Scoped);
            builder.Register<Brains>(Lifetime.Scoped);
            builder.Register<IAi,UtilityAi>(Lifetime.Scoped);

            builder.Register<ModuleMatchView>(Lifetime.Scoped);
            builder.Register<ModuleMatchPlayingField>(Lifetime.Scoped);
            builder.Register<StyleMatchData>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<MatchFlow>();
        }
    }
}