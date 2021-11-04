using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class Arrow : Sprite
    {
        public Vector2 Speed;

        public override Rectangle HitBox => new Rectangle((int)(Position.X - Origin.X * Scale.X), (int)(Position.Y - Origin.Y * Scale.Y), (int) ScaledWidth/2, (int) ScaledHeight);

        public Arrow(Texture2D image, Vector2 position, Color tint,  Vector2 origin, Vector2 scale, Vector2 speed, SpriteEffects effect, float rotation)
            :base(image, position, tint, origin, scale, effect)
        {
            Speed = speed;
            Rotation = rotation;
        }
        public void Update()
        {
            Position.X += Speed.X;
            Position.Y += Speed.Y;
        }
    }
}
