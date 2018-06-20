using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Stack_m_up
{
    class PlayField
    {
        Viewport view;
        int windowHeight, windowWidth;
        int amount, place;

        World world;
        DrawablePhysicsObject currentBlock;
        
        Vector2 bodyPosition;
        Texture2D block;
        Texture2D block2;

        const float unitToPixel = 100.0f;
        const float pixelToUnit = 1 / unitToPixel;
        
        List<DrawablePhysicsObject> crateList;
        DrawablePhysicsObject floor;
        KeyboardState prevKeyboardState;
        MouseState prevMouseState;
        TouchCollection touch;
        Random random;

        Vector2 mousePosition;
        SpriteFont font;

        public PlayField( int amount, int place )
        {
            this.amount = amount;
            this.place = place;
        }


        public void Initialize()
        {
            var applicationView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            windowHeight = Convert.ToInt32(applicationView.VisibleBounds.Height);
            windowWidth = Convert.ToInt32(applicationView.VisibleBounds.Width);
        }

        public void LoadContent(ContentManager content)
        {
            createView( (windowWidth / amount) * place, 0, windowHeight, windowWidth / amount );

            world = new World(new Vector2(0, 0.5f));

            Vector2 size = new Vector2(50, 50);
            bodyPosition = new Vector2((view.Width / 2.0f) * pixelToUnit, 0);

            block = content.Load<Texture2D>("Block4");
            block2 = content.Load<Texture2D>("Block2");

            random = new Random(place);

            floor = new DrawablePhysicsObject(world, content.Load<Texture2D>("Platform"), new Vector2(view.Width, 65.0f), 500);
            floor.Position = new Vector2(view.Width - (view.Width / 4.5f), view.Height + 20);
            floor.body.BodyType = BodyType.Static;
            crateList = new List<DrawablePhysicsObject>();
            prevKeyboardState = Keyboard.GetState();

            font = content.Load<SpriteFont>("font");
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime, int rand)
        {
            var applicationView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            if (applicationView.VisibleBounds.Height != windowHeight || applicationView.VisibleBounds.Height != windowWidth)
            {
                windowHeight = Convert.ToInt32(applicationView.VisibleBounds.Height);
                windowWidth = Convert.ToInt32(applicationView.VisibleBounds.Width);

                createView((windowWidth / amount) * place, 0, windowHeight, windowWidth / amount);
            }


            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton != prevMouseState.LeftButton)
            {
                foreach (DrawablePhysicsObject obj in crateList)
                {

                    int clickPosition = Convert.ToInt32(mouseState.Position.ToVector2().X);

                    if (clickPosition < (windowWidth / amount * (place + 1) - (windowWidth / amount / 2)) && clickPosition > windowWidth / amount * (place)) {
                        currentBlock.Position = new Vector2( currentBlock.Position.X - 2, currentBlock.Position.Y );
                    }
                    if (clickPosition > (windowWidth / amount * (place + 1) - (windowWidth / amount / 2)) && clickPosition < windowWidth / amount * (place + 1))
                    {
                        currentBlock.Position = new Vector2(currentBlock.Position.X + 2, currentBlock.Position.Y);
                    }

                }

            }
            prevMouseState = mouseState;

            touch = TouchPanel.GetState();
            foreach (TouchLocation tl in touch)
            {
                if ((tl.State == TouchLocationState.Pressed)
                        || (tl.State == TouchLocationState.Moved))
                {
                    foreach (DrawablePhysicsObject obj in crateList)
                    {
                        int clickPosition = Convert.ToInt32(tl.Position.X);
                        if (clickPosition < (windowWidth / amount * (place + 1) - (windowWidth / amount / 2)) && clickPosition > windowWidth / amount * (place))
                        {
                            currentBlock.Position = new Vector2(currentBlock.Position.X - 2, currentBlock.Position.Y);
                        }
                        if (clickPosition > (windowWidth / amount * (place + 1) - (windowWidth / amount / 2)) && clickPosition < windowWidth / amount * (place + 1))
                        {
                            currentBlock.Position = new Vector2(currentBlock.Position.X + 2, currentBlock.Position.Y);
                        }
                    }
                }
            }

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space) && !prevKeyboardState.IsKeyDown(Keys.Space))
            {
                SpawnBlock(rand);
            }

            prevKeyboardState = keyboardState;
        }

        public void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Viewport original = graphics.GraphicsDevice.Viewport;
            
            graphics.GraphicsDevice.Viewport = view;
            spriteBatch.Begin();

            Vector2 position = bodyPosition * unitToPixel;
            Vector2 scale = new Vector2(50 / (float)block.Width, 50 / (float)block.Height);

            foreach (DrawablePhysicsObject crate in crateList)
            {
                crate.Draw(spriteBatch);
            }

            floor.Draw(spriteBatch);
            spriteBatch.DrawString(font, mousePosition.ToString(), new Vector2(10, windowHeight - 20), Color.Black);
            
            spriteBatch.End();

            graphics.GraphicsDevice.Viewport = original;
        }

        private void createView( int x, int y, int height, int width )
        {
            view = new Viewport();
            view.X = x;
            view.Y = y;
            view.Width = width;
            view.Height = height;
            view.MinDepth = 0;
            view.MaxDepth = 1;
        }

        private void SpawnBlock(int rand)
        {
            DrawablePhysicsObject obj;
            if(rand == 0)
            {
                obj = new DrawablePhysicsObject(world, block, new Vector2(50.0f, 50.0f), 0.1f);
            }
            else
            {
                obj = new DrawablePhysicsObject(world, block2, new Vector2(125.0f, 25.0f), 0.1f);
            }
            obj.Position = new Vector2(random.Next(50, view.Width - 50), 1);
            obj.body.Friction = 1;
            obj.body.Restitution = -0.2f;

            currentBlock = obj;
            crateList.Add(obj);
        }

    }
}
