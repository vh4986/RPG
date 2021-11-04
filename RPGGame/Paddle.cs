using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class Paddle : Sprite
    {
        
        public Paddle(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, Vector2 speed, SpriteEffects effect)
            : base(image, position, tint, origin, scale, effect)
        {

        }

        public void DrawWithTint(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Image, Position, SourceRectangle, color, Rotation, Origin, Scale, Effect, LayerDepth);
        }
    }
}
