using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections;

namespace Stack_m_up
{
    class PlayField
    {
        Viewport view;

        Vector2 position, size;

        World world;

        IPhysicsObject currentBlock;
        PlatformObject floor;
        ArrayList blocks;
        
        Texture2D greyOverlay;
        Texture2D gameOver;
        Texture2D winnerScreen;
        Texture2D winnerOverlay;
        
        Random random;

        bool leftSideClicked = false, rightSideClicked = false;
        
        bool hasWon = false;
        
        public PlayField()
        {
        }

        public void Initialize(Vector2 position, Vector2 size)
        {
            updateViewPort(position, size);

            blocks = new ArrayList();
        }

        public void LoadContent(ContentManager content)
        {
            world = new World(new Vector2(0, 0.5f));
            
            greyOverlay = content.Load<Texture2D>("grey_overlay");
            gameOver = content.Load<Texture2D>("gameover_sprite");
            winnerScreen = content.Load<Texture2D>("winner_Screen");
            winnerOverlay = content.Load<Texture2D>("winner_overlay");
            
            random = new Random();

            floor = new PlatformObject(world, new Vector2(size.X / 2, size.Y - 50));
        }

        public void Update(GameTime gameTime)
        {
            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (hasLost())
                return;

            rightSideClicked = false;
            leftSideClicked = false;

            MouseState mouseState = Mouse.GetState();
            if( mouseState.LeftButton == ButtonState.Pressed )
                viewportClicked(mouseState.Position.X, mouseState.Position.Y );

            TouchCollection touch = TouchPanel.GetState();
            foreach (TouchLocation tl in touch)
            {
                if ((tl.State == TouchLocationState.Pressed)
                        || (tl.State == TouchLocationState.Moved))
                {
                    viewportClicked( (int)tl.Position.X, (int)tl.Position.Y );
                }
            }

            if (blocks.Count > 0)
            {
                if (leftSideClicked && !rightSideClicked)
                {
                    if (!currentBlock.hasContact() && currentBlock.getPosition().X - currentBlock.getSize().X / 2 > 0)
                        currentBlock.setPosition(currentBlock.getPosition() + new Vector2(-1.5f, 0));
                }
                else if (rightSideClicked && !leftSideClicked)
                {
                    if (!currentBlock.hasContact() && currentBlock.getPosition().X + currentBlock.getSize().X / 2 < size.X)
                        currentBlock.setPosition(currentBlock.getPosition() + new Vector2(1.5f, 0));
                }
                else if (rightSideClicked && leftSideClicked)
                {
                    currentBlock.setRotation(currentBlock.getRotation() + 0.05f);
                }
            }
        }

        public void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Viewport original = graphics.GraphicsDevice.Viewport;
            
            graphics.GraphicsDevice.Viewport = view;
            spriteBatch.Begin();

            floor.Draw(spriteBatch);

            foreach (IPhysicsObject block in blocks)
            {
                block.Draw(spriteBatch);
            }

            if(hasLost())
            {
                spriteBatch.Draw(greyOverlay, new Rectangle(new Point(0, 0), new Point(view.Width, view.Height)), Color.White);
                spriteBatch.Draw(gameOver, new Rectangle(new Point((view.Width / 2) - 116, (view.Height / 2) - 15), new Point(234, 30)), Color.White);
            }

            if(hasWon)
            {
                spriteBatch.Draw(winnerOverlay, new Rectangle(new Point(0, 0), new Point(view.Width, view.Height)), Color.White);
                spriteBatch.Draw(winnerScreen, new Rectangle(new Point((view.Width / 2) - 142, (view.Height / 2) - 98), new Point(284, 195)), Color.White);
            }

            spriteBatch.End();

            graphics.GraphicsDevice.Viewport = original;
        }

        private void updateViewPort(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
            view = new Viewport((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public void AddBlock(int rand)
        {
            if (hasLost())
                return;

            TetrisSet.Type type;
            if (rand == 0)
                type = TetrisSet.Type.T1x5;
            else if (rand == 1)
                type = TetrisSet.Type.T2x2;
            else
                type = TetrisSet.Type.T2x3;

            Vector2 position = new Vector2(random.Next(Convert.ToInt32(floor.getPosition().X - floor.getSize().X / 2), Convert.ToInt32(floor.getPosition().X + floor.getSize().X / 2)), 1);
            int rotation = new Random((int)this.position.X).Next(0, 360);

            IPhysicsObject block = new TetrisObject(world, type, position, rotation);
            
            currentBlock = block;
            blocks.Add(block);
        }

        public bool hasLost()
        {
            int counter = 0;
            foreach (IPhysicsObject obj in blocks) {
                if (obj.getPosition().Y >= floor.getPosition().Y) {
                    counter++;
                }
            }

            return counter >= 3;
        }

        public void winner()
        {
            hasWon = true;
        }

        private void viewportClicked( int x, int y )
        {
            if (x > (int)position.X && x < (int)position.X + size.X / 2)
            {
                leftSideClicked = true;
            }
            if (x < (int) position.X + size.X && x > (int) position.X + size.X / 2)
            {
                rightSideClicked = true;
            }
        }

    }
}
