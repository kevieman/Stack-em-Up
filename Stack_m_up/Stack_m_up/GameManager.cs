using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Threading;

namespace Stack_m_up
{
    class GameManager
    {
        private SpriteFont font;

        int windowWidth, windowHeight;
        ArrayList playfields;
        int playFieldAmount;
        Random random = new Random();
        const float delay = 10;
        float remainingdelay = delay;

        int leftX, midX, rightX;

        public GameManager( int playFieldAmount )
        {
            this.playFieldAmount = playFieldAmount;
            playfields = new ArrayList();

            playfields.Clear();

            for (int i = 0; i < playFieldAmount; ++i)
            {
                PlayField playfield = new PlayField(playFieldAmount, i);
                playfields.Add(playfield);
            }
        }
        
        public void Initialize()
        {
            var applicationView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            windowHeight = Convert.ToInt32(applicationView.VisibleBounds.Height);
            windowWidth = Convert.ToInt32(applicationView.VisibleBounds.Width);

            foreach (PlayField playfield in playfields)
            {
                playfield.Initialize();
            }
        }

        public void LoadContent( ContentManager content )
        {
            font = content.Load<SpriteFont>("font");

            foreach (PlayField playfield in playfields)
            {
                playfield.LoadContent( content );
            }
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingdelay -= timer;
            if (remainingdelay <= 0)
            {
                AddBlock();
                remainingdelay = delay;
            }


            foreach (PlayField playfield in playfields)
            {
                playfield.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            foreach (PlayField playfield in playfields)
            {
                playfield.Draw(gameTime, graphics, spriteBatch);
            }
            spriteBatch.Begin();

            int countdown = 1;
            spriteBatch.DrawString(font, "" + countdown, new Vector2(windowWidth/2, 100), Color.Black);

            spriteBatch.End();
        }

        private void AddBlock()
        {
            int rand = random.Next(0, 3);
            foreach (PlayField playfield in playfields)
            {
                playfield.AddBlock(rand);
            }
        }

    }
}
