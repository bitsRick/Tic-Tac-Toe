using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using VContainer;
using VContainer.Unity;

namespace GoldenDragon
{
    public sealed class BootstrapScope : LifetimeScope
    {
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneManager>(Lifetime.Scoped);
        }
    }
}
