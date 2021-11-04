using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class Food : Item
    {
        public Food(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, SpriteEffects effect)
            :base(image, position, tint, origin, scale, effect)
        {

        }

        public override bool EffectFunction(Knight knight)
        {
            return true;
        }

        public override void Init(Knight knight)
        {
            knight.healthPoints += 5;
        }

    }
}
