using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

using System.Collections.Generic;
using Windows.UI.ViewManagement;

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

        Texture2D background;
        Rectangle mainFrame;

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
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
        }
        
        //Load content and set the background for the game
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("background"); //Load background
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            game.LoadContent( Content );
            audio.LoadContent( Content );
        }
        
        protected override void Update(GameTime gameTime)
        {
            game.Update(gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background, mainFrame, Color.White);
            spriteBatch.End();
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

        public void sliderClick(float perc)  // Clicking sound for sliders
        {
            audio.sliderClick(perc);
        }

        public void sliderClickMusic(float perc)    // Clicking sound for music slider
        {
            audio.sliderClickMusic(perc);
        }

        public void updateMasterVolume(float perc)    // Method for updating the mastervolume
        {
            audio.updateMasterVolume(perc);
        }

        public void updateMusicVolume(float perc)    // Method for updating music volume
        {
            audio.updateMusicVolume(perc);
        }

        public void updateSfxVolume(float perc)    // Method for updating SFX volume
        {
            audio.updateSfxVolume(perc);
        }

        public void startGame( int playerAmount )
        {
            game = new GameManager( playerAmount );
            game.Initialize();
            game.LoadContent( Content );
        }


    }
}
