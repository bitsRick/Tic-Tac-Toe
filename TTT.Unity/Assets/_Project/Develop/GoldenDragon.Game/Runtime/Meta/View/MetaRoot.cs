using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GoldenDragon
{
    public class ViewHudRoot : MonoBehaviour
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
