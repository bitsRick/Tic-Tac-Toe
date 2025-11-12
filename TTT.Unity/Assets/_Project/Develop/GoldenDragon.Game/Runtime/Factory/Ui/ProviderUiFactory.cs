using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui
{
    public class ProviderUiFactory
    {
        public readonly FactoryItem FactoryItem;
        public readonly FactoryUi FactoryUi;
        
        [Inject]
        public ProviderUiFactory(FactoryItem factoryItem, FactoryUi factoryUi)
        {
            FactoryItem = factoryItem;
            FactoryUi = factoryUi;
        }
    }
}