using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Stack_m_up
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameManager game;
        Song music; // Music var 
        List<SoundEffect> soundEffects; // List for all the sound effects used

        float musicVolume = 0.5f;
        float sfxVolume = 0.5f;

        bool isMusicPlaying = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            game = new GameManager( 4 );
            IsMouseVisible = true;
            soundEffects = new List<SoundEffect>();     // Create new list
        }
        
        protected override void Initialize()
        {
            game.Initialize();
            this.IsMouseVisible = true;
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            game.LoadContent( Content );

            //  Uncomment the following line will also loop the song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            soundEffects.Add(Content.Load<SoundEffect>("buttonClick"));
            soundEffects.Add(Content.Load<SoundEffect>("collisionBlock"));
            soundEffects.Add(Content.Load<SoundEffect>("saveSettings"));
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.Play(music); 
        }

        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            game.Update(gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            game.Draw(gameTime, graphics, spriteBatch);

            base.Draw(gameTime);
        }

        public void incSound()      // Increase the music volume
        {
            musicVolume += 0.1f;
            MediaPlayer.Volume = musicVolume;
        }

        public void decrSound()     // Decrease the music volume
        {
            musicVolume -= 0.1f;
            MediaPlayer.Volume = musicVolume;
        }

        public void incSoundFX()    // Increase the sound effects volume
        {
            sfxVolume += 0.1f;
            MediaPlayer.Volume = sfxVolume;
        }

        public void decrSoundFX()   // Decrease the sound seffects volume
        {
            sfxVolume -= 0.1f;
            MediaPlayer.Volume = sfxVolume;
        }

        public void startMenuMusic()    // Start playing the music 
        {
            this.music = Content.Load<Song>("menu_music");
            MediaPlayer.Play(music);
            MediaPlayer.Volume = 0.1f;
            isMusicPlaying = true;
        }

        public void stopMusic() // Stop all music playing 
        {
            MediaPlayer.Stop();
        }

        public void pauseMenuMusic()    // CAll for pause menu music (is actually just mute)
        {

            if (!isMusicPlaying)
            {
                // MediaPlayer.Resume();
                MediaPlayer.Volume = 0.1f;
                isMusicPlaying = true;
            }
            else
            {
                MediaPlayer.Volume = 0.0f;
                // MediaPlayer.Pause();
                isMusicPlaying = false;
            }
        }

        public void Click()     // Call for playing clicking sound
        {
            // Play that can be manipulated after the fact
            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = false;
            instance.Volume = sfxVolume;
            instance.Play();
        }

        public void settingsSave()  // Call for savesettings sound
        {
            // Fire and forget play
            var instance = soundEffects[2].CreateInstance();
            instance.IsLooped = false;
            instance.Volume = sfxVolume;
            instance.Play();
        }

        public void Collision()    // Call for block collision sound
        {
            // Fire and forget play
            var instance = soundEffects[1].CreateInstance();
            instance.IsLooped = false;
            instance.Volume = sfxVolume;
            instance.Play();
        }

    }
}
