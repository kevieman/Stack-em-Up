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
    static class AudioManager
    {
        static Song music;     // Variable that stores the song
        static List<SoundEffect> soundEffects; // List where all the sound effects are stored

        static float masterVolume;  // Global maximum for all the volumes
        static float musicVolume;   // Global value for the music volume
        static float sfxVolume;     // Global value for the SFX volume

        static AudioManager()
        {
            soundEffects = new List<SoundEffect>(); // Initialize new List
            masterVolume = 1.0f;    // Set base volume
            musicVolume = 1.0f;     // Set base volume
            sfxVolume = 1.0f;       // Set base volume

            //  Uncomment the following line will also loop the song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            
        }

        static public void LoadContent(ContentManager manager)
        {
            // Adding all the sound effects here
            soundEffects.Add(manager.Load<SoundEffect>("buttonClick"));
            soundEffects.Add(manager.Load<SoundEffect>("collisionBlock"));
            soundEffects.Add(manager.Load<SoundEffect>("saveSettings"));
            music = manager.Load<Song>("menu_music");
        }


        static void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = musicVolume * (masterVolume / 1.0f);
            MediaPlayer.Play(music);
        }

        static public void startMenuMusic(float volume)    // Start playing the music with a given volume settings
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
            MediaPlayer.Play(music);
            MediaPlayer.Volume = musicVolume * masterVolume;
        }

        static public void stopMusic() // Stop all music playing 
        {
            MediaPlayer.Stop();
        }

        static public void Click()     // Sound for clicking the buttons
        {
            // Play that can be manipulated after the fact
            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = false;
            instance.Volume = sfxVolume * masterVolume;
            instance.Pitch = 1.0f;
            instance.Play();
        }

        static public void settingsSave()      // Play the sound for saving the settings
        {
            // Fire and forget play
            var instance = soundEffects[2].CreateInstance();
            instance.IsLooped = false;
            instance.Volume = sfxVolume;
            instance.Play();
        }

        static public void Collision()     // Play sound for colliding blocks
        {
            // Fire and forget play
            var instance = soundEffects[1].CreateInstance();
            instance.IsLooped = false;
            instance.Volume = sfxVolume;
            instance.Play();
        }

        static public void sliderClick(float perc)
        {
            float calcPitch = 2.0f;
            calcPitch = -1.0f + (calcPitch * perc);

            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = false;
            instance.Pitch = calcPitch;
            instance.Volume = sfxVolume * masterVolume;
            instance.Play();
        }

        static public void sliderClickMusic(float perc)
        {
            float calcPitch = 2.0f;
            calcPitch = -1.0f + (calcPitch * perc);

            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = false;
            instance.Pitch = calcPitch;
            instance.Volume = 0.2f;
            instance.Play();
        }

        static public void updateMasterVolume(float perc)
        {
            masterVolume = perc;
            setMusicVolume();
        }

        static public void updateMusicVolume(float perc)
        {
            musicVolume = perc;
            setMusicVolume();
        }

        static public void updateSfxVolume(float perc)
        {
            float calcVolume = (1.0f * perc);
            sfxVolume = calcVolume;
        }

        static private void setMusicVolume()
        {
            MediaPlayer.Volume = musicVolume * masterVolume;
        }
    }
}
