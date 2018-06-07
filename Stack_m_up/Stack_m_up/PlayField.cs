using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Stack_m_up
{
    class PlayField
    {
        Viewport view;
        int windowHeight, windowWidth;
        int amount, place;

        World world;

        Body body;
        Texture2D block;
        const float unitToPixel = 100.0f;
        const float pixelToUnit = 1 / unitToPixel;
        
        List<DrawablePhysicsObject> crateList;
        DrawablePhysicsObject floor;
        KeyboardState prevKeyboardState;
        Random random;

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

            world = new World(new Vector2(0, 9.8f));

            Vector2 size = new Vector2(50, 50);
            body = BodyFactory.CreateRectangle(world, size.X * pixelToUnit, size.Y * pixelToUnit, 1);
            body.BodyType = BodyType.Dynamic;
            body.Position = new Vector2((view.Width / 2.0f) * pixelToUnit, 0);

            block = content.Load<Texture2D>("logo");

            random = new Random();

            floor = new DrawablePhysicsObject(world, content.Load<Texture2D>("floor"), new Vector2(view.Width, 100.0f), 1000);
            floor.Position = new Vector2(view.Width / 2.0f, view.Height - 50);
            floor.body.BodyType = BodyType.Static;
            crateList = new List<DrawablePhysicsObject>();
            prevKeyboardState = Keyboard.GetState();
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

                createView((windowWidth / amount) * place, 0, windowHeight, windowWidth / amount);
            }


            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space) && !prevKeyboardState.IsKeyDown(Keys.Space))
            {
                SpawnBlock();
            }

            prevKeyboardState = keyboardState;
        }

        public void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Viewport original = graphics.GraphicsDevice.Viewport;
            
            graphics.GraphicsDevice.Viewport = view;
            spriteBatch.Begin();

            Vector2 position = body.Position * unitToPixel;
            Vector2 scale = new Vector2(50 / (float)block.Width, 50 / (float)block.Height);
            spriteBatch.Draw(block, position, null, Color.White, body.Rotation, new Vector2(block.Width / 2.0f, block.Height / 2.0f), scale, SpriteEffects.None, 0);

            foreach (DrawablePhysicsObject crate in crateList)
            {
                crate.Draw(spriteBatch);
            }

            floor.Draw(spriteBatch);
            
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

        private void SpawnBlock()
        {
            DrawablePhysicsObject obj;
            obj = new DrawablePhysicsObject(world, block, new Vector2(50.0f, 50.0f), 0.1f);
            obj.Position = new Vector2(random.Next(50, view.Width - 50), 1);

            crateList.Add(obj);
        }

    }
}
