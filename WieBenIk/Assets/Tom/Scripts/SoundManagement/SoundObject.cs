using UnityEngine;

namespace WieBenIk.Sound
{
    public enum AudioType
    {
        Music,
        BackgroundMusic,
        SFX
    };

    [RequireComponent(typeof(AudioSource))]
    public class SoundObject : MonoBehaviour
    {
        protected AudioSource _AudioSource;
        public AudioSource AudioSource
        {
            get { return _AudioSource; }
        }

        [SerializeField]
        protected AudioType _AudioType;

        protected SoundManager _SoundManager;


        protected void Start()
        {
            _AudioSource = GetComponent<AudioSource>();
            _SoundManager = FindObjectOfType<SoundManager>();

            //Set the subscribtions to the soundmanager.
            switch(_AudioType)
            {
                case AudioType.BackgroundMusic:
                    _SoundManager._MuteBackgroundMusicEvent += MuteAudio;
                    break;
                case AudioType.Music:
                    _SoundManager._MuteMusicEvent += MuteAudio;
                    break;
                case AudioType.SFX:
                    _SoundManager._MuteSFX += MuteAudio;
                    break;
            }
        }


        //Plays the audio.
        public void PlayAudio()
        {
            _AudioSource.Play();
        }

        //Mute the sound effect.
        private void MuteAudio(bool mute)
        {
            _AudioSource.mute = mute;
        }


        //End the subscribtions.
        public void OnDestroy()
        {
            switch (_AudioType)
            {
                case AudioType.BackgroundMusic:
                    _SoundManager._MuteBackgroundMusicEvent += MuteAudio;
                    break;
                case AudioType.Music:
                    _SoundManager._MuteMusicEvent += MuteAudio;
                    break;
                case AudioType.SFX:
                    _SoundManager._MuteSFX += MuteAudio;
                    break;
            }
        }
    }
}