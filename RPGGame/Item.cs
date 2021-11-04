using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    abstract class Item : Sprite
    {
        public Item(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, SpriteEffects effect)
            : base(image, position, tint, origin, scale, effect)
        {

        }

        public abstract void Init(Knight knight);

        public abstract bool EffectFunction(Knight knight);
    }
}
