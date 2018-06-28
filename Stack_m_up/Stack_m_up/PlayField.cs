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
        Texture2D block3;
        Texture2D greyOverlay;

        const float unitToPixel = 100.0f;
        const float pixelToUnit = 1 / unitToPixel;
        
        List<DrawablePhysicsObject> crateList;
        DrawablePhysicsObject floor;
        KeyboardState prevKeyboardState;
        TouchCollection touch;
        Random random;

        bool leftSideClicked = false, rightSideClicked = false;

        Vector2 mousePosition;
        SpriteFont font;

        int leftX, midX, rightX;

        bool active = true;

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

            leftX = windowWidth / amount * place;
            midX = (windowWidth / amount * place) + windowWidth / amount / 2;
            rightX = windowWidth / amount * (place + 1);
        }

        public void LoadContent(ContentManager content)
        {
            createView( (windowWidth / amount) * place, 0, windowHeight, windowWidth / amount );

            world = new World(new Vector2(0, 0.5f));

            Vector2 size = new Vector2(50, 50);
            bodyPosition = new Vector2((view.Width / 2.0f) * pixelToUnit, 0);

            block = content.Load<Texture2D>("Block4");
            block2 = content.Load<Texture2D>("Block2");
            block3 = content.Load<Texture2D>("Block6");
            greyOverlay = content.Load<Texture2D>("grey_overlay");

            random = new Random(place);

            floor = new DrawablePhysicsObject(world, content.Load<Texture2D>("Platform"), new Vector2(250, 50.0f), 500);
            floor.Position = new Vector2(view.Width - (view.Width / 2.0f), view.Height - 50);
            floor.body.BodyType = BodyType.Static;
            crateList = new List<DrawablePhysicsObject>();
            prevKeyboardState = Keyboard.GetState();

            font = content.Load<SpriteFont>("font");
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            var applicationView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            if (applicationView.VisibleBounds.Height != windowHeight || applicationView.VisibleBounds.Height != windowWidth)
            {
                windowHeight = Convert.ToInt32(applicationView.VisibleBounds.Height);
                windowWidth = Convert.ToInt32(applicationView.VisibleBounds.Width);
                leftX = windowWidth / amount * place;
                midX = (windowWidth / amount * place) + windowWidth / amount / 2;
                rightX = windowWidth / amount * (place + 1);

                createView(leftX, 0, windowHeight, windowWidth / amount);
            }
            
            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (hasLost())
                return;

            rightSideClicked = false;
            leftSideClicked = false;

            MouseState mouseState = Mouse.GetState();
            if( mouseState.LeftButton == ButtonState.Pressed )
                viewportClicked(Convert.ToInt32(mouseState.Position.ToVector2().X), Convert.ToInt32(mouseState.Position.ToVector2().Y) );

            touch = TouchPanel.GetState();
            foreach (TouchLocation tl in touch)
            {
                if ((tl.State == TouchLocationState.Pressed)
                        || (tl.State == TouchLocationState.Moved))
                {
                    viewportClicked( Convert.ToInt32(tl.Position.X), Convert.ToInt32( tl.Position.Y ) );
                }
            }

            if (crateList.Count > 0)
            {
                if (leftSideClicked && !rightSideClicked)
                {
                    if (currentBlock.body.ContactList == null && currentBlock.Position.X - currentBlock.Size.X / 2 > 0)
                        currentBlock.Position += new Vector2(-1, 0);
                }
                else if (rightSideClicked && !leftSideClicked)
                {
                    if (currentBlock.body.ContactList == null && currentBlock.Position.X + currentBlock.Size.X / 2 < windowWidth / amount)
                        currentBlock.Position += new Vector2(1, 0);
                }
                else if (rightSideClicked && leftSideClicked)
                {
                    currentBlock.body.Rotation += 0.05f;
                }
            }
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

            if(hasLost())
            {
                spriteBatch.Draw(greyOverlay, new Rectangle(new Point(0, 0), new Point(view.Width, view.Height)), Color.White);
            }

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

        public void AddBlock(int rand)
        {
            if (hasLost())
                return;

            DrawablePhysicsObject obj;
            if(rand == 0)
            {
                obj = new DrawablePhysicsObject(world, block, new Vector2(50.0f, 50.0f), 1.0f);
            }
            else if(rand == 1)
            {
                obj = new DrawablePhysicsObject(world, block2, new Vector2(125.0f, 25.0f), 1.0f);
            }
            else
            {
                obj = new DrawablePhysicsObject(world, block3, new Vector2(75.0f, 50.0f), 1.0f);
            }
            Random randomRotation = new Random();
            Debug.WriteLine( floor.Position.X + " " + floor.Size.X );
            obj.Position = new Vector2(random.Next(Convert.ToInt32(floor.Position.X - floor.Size.X / 2 + obj.Size.X / 2), Convert.ToInt32(floor.Position.X + floor.Size.X / 2 - obj.Size.X / 2)), 1);
            obj.body.Rotation = randomRotation.Next(0, 360);
            obj.body.Friction = 10;
            obj.body.Restitution = -0.2f;

            currentBlock = obj;
            crateList.Add(obj);
        }

        public bool hasLost()
        {
            int counter = 0;
            foreach (DrawablePhysicsObject obj in crateList) {
                if (obj.Position.Y >= floor.Position.Y) {
                    counter++;
                }
            }

            return counter >= 3;
        }

        private void viewportClicked( int x, int y )
        {

            if (x > leftX && x < midX)
            {
                leftSideClicked = true;
            }
            if (x < rightX && x > midX)
            {
                rightSideClicked = true;
            }
        }

    }
}
