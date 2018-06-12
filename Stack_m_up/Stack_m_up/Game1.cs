using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            game = new GameManager( 4 );
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

    }
}
