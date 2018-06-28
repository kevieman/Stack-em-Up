using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stack_m_up
{
    // this is the interface for all blocks, platforms included
    interface IObject
    {
        void Draw(SpriteBatch spriteBatch);
        Vector2 getPosition();
        Vector2 getSize();
    }
}
