using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Stack_m_up
{
    static class AudioManager
    {
        static Song music;     // Variable that stores the song\
        static List<Audio> audioList = new List<Audio>();
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

            audioList.Add(new Audio("click", Audio.typeEnum.sfx, false, "buttonClick"));
            audioList.Add(new Audio("music", Audio.typeEnum.music, false, "menu_music"));

            //  Uncomment the following line will also loop the song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }

        static public void LoadContent(ContentManager manager)
        {
            // Add all the sounds to list
            foreach (Audio a in audioList)
            {
                if (a.getType() == Audio.typeEnum.sfx)
                {
                    soundEffects.Add(manager.Load<SoundEffect>(a.getPath()));
                }
                else if (a.getType() == Audio.typeEnum.music)
                {
                    music = manager.Load<Song>(a.getPath());
                }
            }
        }


        static void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = musicVolume * (masterVolume / 1.0f);
            MediaPlayer.Play(music);
        }

        // Start playing the music with a given volume settings
        static public void startMenuMusic(float volume)    
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

        static public void PlaySound(string soundName)
        {
            // Play a instance of a soundEffect
            var instance = soundEffects[getAudioID(soundName)].CreateInstance();
            instance.IsLooped = audioList[getAudioID(soundName)].isLoop();
            instance.Volume = sfxVolume * masterVolume;
            instance.Pitch = 1.0f;
            instance.Play();
        }

        // Returns the id of a Audio object in the audioList
        static private int getAudioID(string soundName)
        {
            for (int i = 0; i < audioList.Count; i++)
            {
                if (audioList[i].getName() == soundName)
                {
                    return i;
                }
            }

            return -1;
        }

        // Starts playing the slider click sound effect with given perc
        static public void sliderClick(float perc)  
        {
            // Perc is percentage of volume
            float calcPitch = 2.0f;
            calcPitch = -1.0f + (calcPitch * perc);

            var instance = soundEffects[getAudioID("click")].CreateInstance();
            instance.IsLooped = false;
            instance.Pitch = calcPitch;
            instance.Volume = sfxVolume * masterVolume;
            instance.Play();
        }

        // Starts playing the sound effect of the music slider with given perc
        static public void sliderClickMusic(float perc) 
        {
            // Perc is percentage of volume
            float calcPitch = 2.0f;
            calcPitch = -1.0f + (calcPitch * perc);

            var instance = soundEffects[getAudioID("click")].CreateInstance();
            instance.IsLooped = false;
            instance.Pitch = calcPitch;
            instance.Volume = 0.2f;
            instance.Play();
        }

        // Updates the master volume to the give volume percentage
        static public void updateMasterVolume(float perc)  
        {
            masterVolume = perc;
            setMusicVolume();
        }

        // Updates the music volume to the give volume percentage
        static public void updateMusicVolume(float perc)   
        {
            musicVolume = perc;
            setMusicVolume();
        }

        // Updates the SFX volume to the give volume percentage
        static public void updateSfxVolume(float perc)   
        {
            float calcVolume = (1.0f * perc);
            sfxVolume = calcVolume;
        }

        // Method that is used by other methods for settings the music volume
        static private void setMusicVolume()  
        {
            MediaPlayer.Volume = musicVolume * masterVolume;
        }
    }
}
