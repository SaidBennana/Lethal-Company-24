using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace As_Star
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        [SerializeField] bool User_DontDestroyOnLoad;
        [Space(5)]
        [SerializeField] SoundObject Sounds;

        #region  controlleSound Varibals
        bool CanPlaySound = true;

        #endregion

        [SerializeField] GameObject MusicObj;

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
            if (GameManager.instance)
            {
                MusicObj.SetActive(!GameManager.instance.LoadData<bool>(SaveKeys.MusicKey, true));
                CanPlaySound = !GameManager.instance.LoadData<bool>(SaveKeys.SoundKey, true);

                if (UI_Game.Instance)
                {
                    UI_Game.Instance.MusicUI();
                    UI_Game.Instance.SoundUI();
                }
            }


        }


        public void PlayeWithIndex(int index)
        {
            if (!CanPlaySound) return;
            if (Sounds.Sounds.ElementAtOrDefault(index) != null)
            {
                Sounds.Sounds[index].source.PlayOneShot(Sounds.Sounds[index].source.clip);
            }
        }

        public void PlayeWithName(string name)
        {
            if (!CanPlaySound) return;
            Sound _Sound = Sounds.Sounds.Find(x => x.nameClip == name);
            if (_Sound != null)
                _Sound.source.PlayOneShot(_Sound.source.clip);
        }

        // sound Controlle
        public bool SoundControlle()
        {
            CanPlaySound = !CanPlaySound;
            ES3.Save(SaveKeys.SoundKey, CanPlaySound);
            return CanPlaySound;
        }
        public bool MusicControlle()
        {
            MusicObj.SetActive(!MusicObj.activeInHierarchy);
            ES3.Save(SaveKeys.MusicKey, MusicObj.activeInHierarchy);
            return MusicObj.activeInHierarchy;
        }
    }
}
