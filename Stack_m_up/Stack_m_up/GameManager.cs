using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;

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
        Boolean gameStarted = false;
        float countdown = 10;

        public GameManager( int playFieldAmount )
        {
            this.playFieldAmount = playFieldAmount;
        }
        
        public void Initialize()
        {
            TetrisSet.Initialize();

            var applicationView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            windowHeight = Convert.ToInt32(applicationView.VisibleBounds.Height);
            windowWidth = Convert.ToInt32(applicationView.VisibleBounds.Width);

            playfields = new ArrayList();

            for (int i = 0; i < playFieldAmount; ++i)
            {
                Vector2 position = new Vector2(windowWidth / playFieldAmount * i, 0);
                Vector2 size = new Vector2(windowWidth / playFieldAmount, windowHeight);

                PlayField playfield = new PlayField();
                playfield.Initialize(position, size);

                playfields.Add(playfield);
            }
        }

        public void LoadContent( ContentManager content )
        {
            TetrisSet.LoadContent(content);

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
            int counter = 0;
            foreach (PlayField pf in playfields)
            {
                if (pf.hasLost())
                    counter++;
            }

            if (counter >= playFieldAmount - 1)
            {
                foreach (PlayField pf in playfields)
                {
                    if (!pf.hasLost())
                    {
                        pf.winner();
                    }
                }
                return;
            }
                

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
            if (gameStarted == false)
            {
                var startTimer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                countdown -= startTimer;

                if ((int)countdown/2 > 0)
                {
                    spriteBatch.DrawString(font, "" + (int)countdown/2, new Vector2(windowWidth / 2, 100), Color.Black);
                }
                if ((int)countdown/2 == 0)
                {
                    spriteBatch.DrawString(font, "GO!", new Vector2(windowWidth / 2, 100), Color.Black);
                }
                if (countdown/2 < 0) {
                    spriteBatch.DrawString(font, "", new Vector2(windowWidth / 2, 100), Color.Black);
                    gameStarted = true;
                }
            }
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
