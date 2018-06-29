using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Windows.UI.ViewManagement;

namespace Stack_m_up
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Controller : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameManager game;

        Texture2D background;
        Rectangle mainFrame;

        public Controller()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            game = new GameManager( 4 );

            IsMouseVisible = true;
        }
        
        protected override void Initialize()
        {
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;

            game.Initialize();
            base.Initialize();
        }
        
        //Load content and set the background for the game
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("background"); //Load background
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            game.LoadContent( Content );
            AudioManager.LoadContent( Content );
            base.LoadContent();
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

        public void startGame( int playerAmount )
        {
            game = new GameManager( playerAmount );
            game.Initialize();
            game.LoadContent( Content );
        }


    }
}
