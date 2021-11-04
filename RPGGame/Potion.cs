using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class Potion : Item
    {
        public Potion(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, SpriteEffects effect)
            :base(image, position, tint, origin, scale, effect)
        {

        }
    }
}
