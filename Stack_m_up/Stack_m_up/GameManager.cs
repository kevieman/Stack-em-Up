using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;

namespace Stack_m_up
{
    class GameManager
    {
        ArrayList playfields;
        int playFieldAmount;

        double windowHeight, windowWidth;
        Texture2D image;

        public GameManager( int playFieldAmount )
        {
            this.playFieldAmount = playFieldAmount;
            playfields = new ArrayList();
        }
        
        public void Initialize()
        {
            var applicationView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            windowHeight = applicationView.VisibleBounds.Height;
            windowWidth = applicationView.VisibleBounds.Width;

        }

        public void LoadContent( ContentManager content )
        {
            createPlayFields(playFieldAmount);

            image = content.Load<Texture2D>("logo");
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            var applicationView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            if (applicationView.VisibleBounds.Height != windowHeight || applicationView.VisibleBounds.Height != windowWidth)
            {
                windowHeight = applicationView.VisibleBounds.Height;
                windowWidth = applicationView.VisibleBounds.Width;

                createPlayFields(playFieldAmount);
            }
        }

        public void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Viewport original = graphics.GraphicsDevice.Viewport;

            foreach (Viewport view in playfields)
            {
                graphics.GraphicsDevice.Viewport = view;
                spriteBatch.Begin();
                spriteBatch.Draw(image, new Vector2(0, 200), Color.AliceBlue);
                spriteBatch.End();
            }

            graphics.GraphicsDevice.Viewport = original;
        }

        private void createPlayFields( int amount )
        {
            playfields.Clear();

            for (int i = 0; i < amount; ++i)
            {
                Viewport view = new Viewport();
                view.X = Convert.ToInt32((windowWidth / amount) * i);
                view.Y = 0;
                view.Width = Convert.ToInt32(windowWidth / amount);
                view.Height = Convert.ToInt32(windowHeight);
                view.MinDepth = 0;
                view.MaxDepth = 1;

                playfields.Add(view);
            }
        }

    }
}
