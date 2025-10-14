using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View
{
    public class MetaRoot : MonoBehaviour
    {
        public async UniTask Initialized()
        {
            await UniTask.CompletedTask;
        }

        public async UniTask Release()
        {
            await UniTask.CompletedTask;
        }
    }
}
