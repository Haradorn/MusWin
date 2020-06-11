using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MusWin
{
    public class Sound
    {
        private String PlayerCommand;
        private bool isFileOpen;
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        private static Sound sound;
        private int leftVolume;
        private int rightVolume;
        private int masterVolume;

        public Sound()
        {
            this.LeftVolume = 10 * 100;
            this.MasterVolume = 10 * 100;
            this.RightVolume = 10 * 100;
        }
        public static Sound GetSound()
        {
            if (sound == null)
                sound = new Sound();
            return sound;
        }
        public bool IsPaused()
        {
            return PlayerCommand == "pause MediaFile";
        }
        public bool IsPlaying()
        {
            return Status() == "playing";
        }
        public bool IsOpen()
        {
            return isFileOpen;
        }
        public String Status()
        {
            int i = 128;
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(i);
            mciSendString("status MediaFile mode", stringBuilder, i, IntPtr.Zero);
            return stringBuilder.ToString();
        }
        public void Play(bool loop)
        {
            if (isFileOpen)
            {
                PlayerCommand = "play MediaFile";
                if (loop)
                    PlayerCommand += " REPEAT";
                mciSendString(PlayerCommand, null, 0, IntPtr.Zero);
                this.LeftVolume = this.LeftVolume;
                this.MasterVolume = this.MasterVolume;
                this.RightVolume = this.RightVolume;
            }
        }
        public void Play(string FileName)
        {
            if (isFileOpen == true)
            {
                Close();
            }
            Open(FileName);
            Play(false);
        }
        public void Pause()
        {
            PlayerCommand = "pause MediaFile";
            mciSendString(PlayerCommand, null, 0, IntPtr.Zero);
        }
        public int LeftVolume
        {
            get
            {
                return leftVolume;
            }
            set
            {
                mciSendString(string.Concat("setaudio MediaFile left volume to ", value), null, 0, IntPtr.Zero);
                leftVolume = value;
            }
        }
        public int RightVolume
        {
            get
            {
                return rightVolume;
            }
            set
            {
                mciSendString(string.Concat("setaudio MediaFile right volume to ", value), null, 0, IntPtr.Zero);
                rightVolume = value;
            }
        }
        public int MasterVolume
        {
            get
            {
                return masterVolume;
            }
            set
            {
                mciSendString(string.Concat("setaudio MediaFile volume to ", value), null, 0, IntPtr.Zero);
                masterVolume = value;
            }
        }
        public void Close()
        {
            PlayerCommand = "close MediaFile";
            mciSendString(PlayerCommand, null, 0, IntPtr.Zero);
            isFileOpen = false;
        }
        public void Open(string sFileName)
        {
            PlayerCommand = "open \"" + sFileName + "\" type mpegvideo alias MediaFile";
            mciSendString(PlayerCommand, null, 0, IntPtr.Zero);
            isFileOpen = true;
        }
    }
}
