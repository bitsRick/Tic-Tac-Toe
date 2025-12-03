using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio
{
    [CreateAssetMenu(fileName = nameof(ConfigSounds), menuName = "GDG/ConfigSounds", order = 1)]
    public class ConfigSounds:ScriptableObject
    {
        [Header("Background")]
        [SerializeField] public AudioClip MetaBackground;
        
        [Header("Sfx")]
        [SerializeField] public AudioClip Click;
    }
}