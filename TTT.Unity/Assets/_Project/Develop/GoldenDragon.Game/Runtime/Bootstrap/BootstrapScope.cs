using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GoldenDragon
{
    public sealed class BootstrapScope : LifetimeScope
    {
        [SerializeField] private LoadingView _loadingView;
        [SerializeField] private RegistrationScreen registrationScreenScreen;
        
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
            builder.Register<IPlayerProgress, ProgressService>(Lifetime.Singleton);
            builder.Register<SaveLoadService>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BootstrapFlow>().WithParameter(registrationScreenScreen);
        }
    }
}
