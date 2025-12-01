using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using NUnit.Framework;

namespace _Project.Develop.GoldenDragon.Game.Tests.EditMode
{
    public class Language
    {
        [Test]
        public void Test_Language_Load()
        {
            //arange
            LangFileLoad langFileLoad = new LangFileLoad();
  
            //act
            var ser = langFileLoad.Load(RuntimeConstants.Lang.RusLangFile);
            
            //assert
            Assert.NotNull(ser.GetAwaiter());
        }
    }
}