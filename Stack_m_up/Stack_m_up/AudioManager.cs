using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_m_up
{
    class AudioManager
    {
        GameManager audio;
        Song music;     // Variable that stores the song
        List<SoundEffect> soundEffects; // List where all the sound effects are stored
        ContentManager Content;

        float masterVolume;  // Global maximum for all the volumes
        float musicVolume;   // Global value for the music volume
        float sfxVolume;     // Global value for the SFX volume

        bool isMusicPlaying = false;

        public AudioManager()
        {

            soundEffects = new List<SoundEffect>(); // Initialize new List
            masterVolume = 1.0f;    // Set base volume
            musicVolume = 1.0f;     // Set base volume
            sfxVolume = 1.0f;       // Set base volume

            //  Uncomment the following line will also loop the song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            


        }

        public void LoadContent( ContentManager manager)
        {
            Content = manager;
            // Adding all the sound effects here
            soundEffects.Add(Content.Load<SoundEffect>("buttonClick"));
            soundEffects.Add(Content.Load<SoundEffect>("collisionBlock"));
            soundEffects.Add(Content.Load<SoundEffect>("saveSettings"));
            startMenuMusic(1.0f);   // Start music full volume
        }



        public void UnloadContent()
        {
        }


        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = musicVolume * (masterVolume / 1.0f);
            MediaPlayer.Play(music);
        }

        public void startMenuMusic()    // Start playing the music
        {
            this.music = Content.Load<Song>("menu_music");
            MediaPlayer.Play(music);
            isMusicPlaying = true;
        }

        public void startMenuMusic(float volume)    // Start playing the music with a given volume settings
        {
            if (volume > 1.0f)
            {
                volume = 1.0f;
            }
            else if (volume < 0.0f)
            {
                volume = 0.0f;
            }

            musicVolume = volume;
            this.music = Content.Load<Song>("menu_music");
            MediaPlayer.Play(music);
            MediaPlayer.Volume = musicVolume * masterVolume;
            isMusicPlaying = true;
        }

        public void stopMusic() // Stop all music playing 
        {
            MediaPlayer.Stop();
        }

        public void pauseMenuMusic()    // Pause the music (actually mutes it because pause doesn't work)
        {

            if (!isMusicPlaying)
            {
                // MediaPlayer.Resume();
                MediaPlayer.Volume = musicVolume;
                isMusicPlaying = true;
            }
            else
            {
                MediaPlayer.Volume = 0.0f;
                // MediaPlayer.Pause();
                isMusicPlaying = false;
            }
        }

        public void Click()     // Sound for clicking the buttons
        {
            // Play that can be manipulated after the fact
            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = false;
            instance.Volume = sfxVolume * masterVolume;
            instance.Pitch = 1.0f;
            instance.Play();
        }

        public void settingsSave()      // Play the sound for saving the settings
        {
            // Fire and forget play
            var instance = soundEffects[2].CreateInstance();
            instance.IsLooped = false;
            instance.Volume = sfxVolume;
            instance.Play();
        }

        public void Collision()     // Play sound for colliding blocks
        {
            // Fire and forget play
            var instance = soundEffects[1].CreateInstance();
            instance.IsLooped = false;
            instance.Volume = sfxVolume;
            instance.Play();
        }

        public void sliderClick(float perc)
        {
            float calcPitch = 2.0f;
            calcPitch = -1.0f + (calcPitch * perc);

            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = false;
            instance.Pitch = calcPitch;
            instance.Volume = sfxVolume * masterVolume;
            instance.Play();
        }

        public void sliderClickMusic(float perc)
        {
            float calcPitch = 2.0f;
            calcPitch = -1.0f + (calcPitch * perc);

            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = false;
            instance.Pitch = calcPitch;
            instance.Volume = 0.2f;
            instance.Play();
        }

        public void updateMasterVolume(float perc)
        {
            masterVolume = perc;
            setMusicVolume();
        }

        public void updateMusicVolume(float perc)
        {
            musicVolume = perc;
            setMusicVolume();
        }

        public void updateSfxVolume(float perc)
        {
            float calcVolume = (1.0f * perc);
            sfxVolume = calcVolume;
        }

        private void setMusicVolume()
        {
            MediaPlayer.Volume = musicVolume * masterVolume;
        }
    }
}
