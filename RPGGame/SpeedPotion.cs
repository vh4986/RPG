using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class SpeedPotion : Item
    {
        public Stopwatch stopwatch = new Stopwatch();
        public SpeedPotion(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects)
             : base(image, position, tint, origin, scale, spriteEffects)
        {

        }
        public override void Init(Knight knight)
        {
            stopwatch.Start();
            knight.Speed = new Vector2(8, 8);
        }

        public override bool EffectFunction(Knight knight)
        {
            if(stopwatch.ElapsedMilliseconds > 5000)
            {
                knight.Speed = new Vector2(4, 4);
                return true;
            }
            return false;
        }
    }
}
