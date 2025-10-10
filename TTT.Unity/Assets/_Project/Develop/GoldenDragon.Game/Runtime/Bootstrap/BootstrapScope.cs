using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GoldenDragon
{
    public sealed class BootstrapScope : LifetimeScope
    {
        [SerializeField] private LoadingView _loadingView;
        
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_loadingView);
            
            builder.Register<SceneManager>(Lifetime.Scoped);
            builder.Register<LoadingService>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BootstrapFlow>();
        }
    }
}
