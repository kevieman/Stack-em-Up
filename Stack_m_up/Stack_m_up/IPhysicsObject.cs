using Microsoft.Xna.Framework;

namespace Stack_m_up
{
    // this is the interface for all dynamic blocks, so platform isn't included
    interface IPhysicsObject : IObject
    {
        bool hasContact();
        float getRotation();
        void setPosition( Vector2 position );
        void setRotation(float rotation);
    }
}
