using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio
{
    public class AudioContainer:IDisposable
    {
        private AudioClip _clip;
        private readonly TypeSceneAudio _sceneAudio;
        private string _key;

        public string Key => _key;
        public AudioClip AudioClip => _clip;
        public TypeSceneAudio SceneAudio => _sceneAudio;

        public AudioContainer(string key, AudioClip clip,TypeSceneAudio sceneAudio)
        {
            _key = key;
            _clip = clip;
            _sceneAudio = sceneAudio;
        }

        public void Dispose()
        {
            _clip = null;
            _key = null;
        }
    }
}