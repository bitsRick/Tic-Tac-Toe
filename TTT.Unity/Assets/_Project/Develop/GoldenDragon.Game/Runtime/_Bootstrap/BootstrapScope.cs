using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime._Bootstrap.RegistrationView;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData;
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
        [SerializeField] private AudioService _audioService;
        
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_loadingView);
            builder.RegisterComponent(_audioService);

            builder.Register<IPlayerProfile, ProfileService>(Lifetime.Singleton);
            builder.Register<SceneManager>(Lifetime.Scoped);
            builder.Register<LoadingService>(Lifetime.Singleton);
            builder.Register<SaveLoadService>(Lifetime.Singleton);
            builder.Register<PopupService>(Lifetime.Singleton);
            
            builder.Register<AssetCatch>(Lifetime.Singleton);
            builder.Register<AssetInstall>(Lifetime.Singleton);
            builder.Register<AssetLoad>(Lifetime.Singleton);
            builder.Register<AssetRelease>(Lifetime.Singleton);
            builder.Register<AssetService>(Lifetime.Singleton);
            
            builder.Register<FactoryUi>(Lifetime.Singleton);
            builder.Register<FactoryItem>(Lifetime.Singleton);
            builder.Register<ProviderUiFactory>(Lifetime.Singleton);
            
            builder.Register<SessionDataMatch>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BootstrapFlow>().WithParameter(registrationScreenScreen).WithParameter(_audioService);
        }
    }
}
