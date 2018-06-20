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
        ArrayList playfields;
        int playFieldAmount;
        Random random = new Random();
        const float delay = 10;
        float remainingdelay = delay;


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
            foreach (PlayField playfield in playfields)
            {
                playfield.Initialize();
            }
        }

        public void LoadContent( ContentManager content )
        {
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
        }

        private void AddBlock()
        {
            int rand = random.Next(0, 2);
            foreach (PlayField playfield in playfields)
            {
                playfield.AddBlock( rand );
            }
        }

    }
}
