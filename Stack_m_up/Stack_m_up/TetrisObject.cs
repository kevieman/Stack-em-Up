using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stack_m_up
{
    // one block that falls from the sky
    class TetrisObject : IPhysicsObject
    {
        DrawablePhysicsObject block;

        // some constant fields for the block
        const float friction = 10;
        const float restitution = -0.2f;
        const float mass = 1.0f;
        const BodyType bodyType = BodyType.Dynamic;

        public TetrisObject(World world, TetrisSet.Type type, Vector2 position, float rotation)
        {
            // because it's a TetrisObject we get the size and texture from the TetrisSet
            Vector2 size = TetrisSet.getSize(type);
            Texture2D texture = TetrisSet.getTexture(type);

            // create the real block
            block = new DrawablePhysicsObject(world, texture, size, mass);
            block.Position = position;
            block.body.Rotation = rotation;
            block.body.Friction = friction;
            block.body.Restitution = restitution;
            block.body.BodyType = bodyType;
        }

        public float getRotation()
        {
            return block.body.Rotation;
        }

        // to turn the block
        public void setRotation(float rotation)
        {
            block.body.Rotation = rotation;
        }

        // to move the block
        public void setPosition(Vector2 position)
        {
            block.Position = position;
        }

        public Vector2 getSize()
        {
            return block.Size;
        }

        // position is in the center of the block
        public Vector2 getPosition()
        {
            return block.Position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            block.Draw(spriteBatch);
        }

        // if the block is colliding with something at the moment
        public bool hasContact()
        {
            return block.body.ContactList != null;
        }
    }
}
