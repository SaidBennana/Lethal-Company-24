using System.Collections.Generic;
using UnityEngine;

namespace As_Star
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        [SerializeField] bool User_DontDestroyOnLoad;
        [Space(5)]
        [SerializeField] SoundObject Sounds;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            if (User_DontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            foreach (Sound item in Sounds.Sounds)
            {
                if (item.clip)
                {
                    AudioSource audiosurce = gameObject.AddComponent<AudioSource>();
                    audiosurce.volume = item.volume;
                    
                    audiosurce.playOnAwake = false;
                    audiosurce.loop = false;

                    audiosurce.clip = item.clip;
                    item.source = audiosurce;
                }
            }
        }


        public void PlayeWithIndex(int index)
        {
            Sounds.Sounds[index].source.PlayOneShot(Sounds.Sounds[index].source.clip);
        }

        public void PlayeWithName(string name)
        {
            Sound _Sound = Sounds.Sounds.Find(x => x.nameClip == name);
            _Sound.source.PlayOneShot(_Sound.source.clip);
        }
    }
}
