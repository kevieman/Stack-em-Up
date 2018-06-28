using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stack_m_up
{
    // one block that catches all dynamic blocks
    class PlatformObject : IObject
    {
        DrawablePhysicsObject platform;

        // some constant fields for the platform
        const BodyType bodyType = BodyType.Static;
        const float mass = 500f;

        public PlatformObject(World world, Vector2 position)
        {
            // because it's a TetrisObject we get the size and texture from the TetrisSet
            Vector2 size = TetrisSet.getSize(TetrisSet.Type.Tfloor);
            Texture2D texture = TetrisSet.getTexture(TetrisSet.Type.Tfloor);

            // create the real item
            platform = new DrawablePhysicsObject(world, texture, size, mass);
            platform.Position = position;
            platform.body.BodyType = bodyType;
        }

        public Vector2 getPosition()
        {
            return platform.Position;
        }

        public Vector2 getSize()
        {
            return platform.Size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            platform.Draw(spriteBatch);
        }
    }
}
