namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory
{
    public class MetaProviderFacadeFactory
    {
        public readonly MetaFactoryItem MetaFactoryItem;
        public readonly MetaFactoryUi MetaFactoryUi;

        public MetaProviderFacadeFactory(MetaFactoryItem metaFactoryItem, MetaFactoryUi metaFactoryUi)
        {
            MetaFactoryItem = metaFactoryItem;
            MetaFactoryUi = metaFactoryUi;
        }
    }
}