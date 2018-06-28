using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Stack_m_up
{
    // one of the itemsets the user can choose
    static class TetrisSet
    {
        // every type TetrisObject 
        public enum Type { T2x2, T2x3, T1x5, Tfloor };

        static ArrayList textures = new ArrayList();
        static ArrayList sizes = new ArrayList();

        static public void Initialize()
        {
            // all sizes in a row
            sizes.Insert((int)Type.T2x2, new Vector2(50, 50));
            sizes.Insert((int)Type.T2x3, new Vector2(75, 50));
            sizes.Insert((int)Type.T1x5, new Vector2(125, 25));
            sizes.Insert((int)Type.Tfloor, new Vector2(250, 50));
        }

        static public void LoadContent(ContentManager content)
        {
            // all textures get loaded
            textures.Insert((int)Type.T2x2, content.Load<Texture2D>("2x2"));
            textures.Insert((int)Type.T2x3, content.Load<Texture2D>("2x3"));
            textures.Insert((int)Type.T1x5, content.Load<Texture2D>("1x5"));
            textures.Insert((int)Type.Tfloor, content.Load<Texture2D>("platform"));
        }

        // get the size for a specific type TetrisObject
        static public Vector2 getSize(Type type)
        {
            return (Vector2)sizes[(int)type];
        }

        // get the texture for a specific type TetrisObject
        static public Texture2D getTexture(Type type)
        {
            return (Texture2D)textures[(int)type];
        }

        // how many types does this set have?
        static public int size()
        {
            return sizes.Count;
        }
    }
}
