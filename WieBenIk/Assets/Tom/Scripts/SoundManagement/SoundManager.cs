using UnityEngine;

namespace WieBenIk.Sound
{
    public delegate void MuteBackgroundMusicDelegate(bool value);
    public delegate void MuteMusicDelegate(bool value);
    public delegate void MuteSFX(bool value);

    public sealed class SoundManager : MonoBehaviour
    {
        public event MuteBackgroundMusicDelegate _MuteBackgroundMusicEvent;
        public event MuteMusicDelegate _MuteMusicEvent;
        public event MuteSFX _MuteSFX;

        bool _BackgroundMusicMuted, _MusicMuted, _SFXMuted;


        //Sets the mute values.
        private void Awake()
        {
            _BackgroundMusicMuted = false;
            _MusicMuted = false;
            _SFXMuted = false;
        }


        //Gets called from a toggle.
        public void MuteBackgroundMusic(bool value)
        {
            _BackgroundMusicMuted = value;
            if (_MuteBackgroundMusicEvent != null)
            {
                _MuteBackgroundMusicEvent.Invoke(value);
            }
        }


        //Gets called from a toggle.
        public void MuteSFX(bool value)
        {
            _SFXMuted = value;
            if (_MuteSFX != null)
            {
                _MuteSFX.Invoke(value);
            }
        }


        //Gets called from a toggle.
        public void MuteMusic(bool value)
        {
            _MusicMuted = value;
            if(_MuteMusicEvent != null)
            {
                _MuteMusicEvent.Invoke(value);
            }
        }
    }
}