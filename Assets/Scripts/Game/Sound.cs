using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Sound 
    {
        public string name;
        public AudioClip sound;
        public bool loop;
        
        [Range(0f, 1f)]
        public float volume;
        
        [Range(.1f, 3f)]
        public float pitch;

        [HideInInspector]
        public AudioSource source;
    }
}