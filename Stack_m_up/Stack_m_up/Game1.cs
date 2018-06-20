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
        AudioManager audio;

        

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            game = new GameManager( 4 );
            audio = new AudioManager();

            IsMouseVisible = true;

            
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
            audio.LoadContent( Content );
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

        public void startMenuMusic()    // Start playing the music
        {
            audio.startMenuMusic();
        }

        public void startMenuMusic(float volume)    // Start playing the music with a given volume settings
        {
            audio.startMenuMusic(volume);
        }

        public void stopMusic() // Stop all music playing 
        {
            audio.stopMusic();
        }

        public void pauseMenuMusic()    // Pause the music (actually mutes it because pause doesn't work)
        {
            audio.pauseMenuMusic();
        }

        public void Click()     // Sound for clicking the buttons
        {
            audio.Click();
        }

        public void settingsSave()      // Play the sound for saving the settings
        {
            audio.settingsSave();
        }

        public void Collision()     // Play sound for colliding blocks
        {
            audio.Collision();
        }

        public void sliderClick(float perc)
        {
            audio.sliderClick(perc);
        }

        public void sliderClickMusic(float perc)
        {
            audio.sliderClickMusic(perc);
        }

        public void updateMasterVolume(float perc)
        {
            audio.updateMasterVolume(perc);
        }

        public void updateMusicVolume(float perc)
        {
            audio.updateMusicVolume(perc);
        }

        public void updateSfxVolume(float perc)
        {
            audio.updateSfxVolume(perc);
        }


    }
}
