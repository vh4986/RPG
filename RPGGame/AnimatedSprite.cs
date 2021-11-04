using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{

    class AnimatedSprite : Sprite
    {
        TimeSpan elapsedTime;
        TimeSpan intervalTime;
        int currentFrame;

 
        public AnimatedSprite(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, SpriteEffects effect)
            : base(image, position, tint, origin, scale, effect)
        {
            elapsedTime = TimeSpan.Zero;
            intervalTime = TimeSpan.FromMilliseconds(100);
            currentFrame = 0;
            


        }

        public void drawAnimation(SpriteBatch spriteBatch, Rectangle? sourceRectangle)
        {
            spriteBatch.Draw(Image, Position, sourceRectangle, Tint, Rotation, Origin, Scale, Effect, LayerDepth);
        }

        public void updateAnimation(GameTime gameTime, List<Rectangle> frames)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime >= intervalTime)
            {
                currentFrame++;
                elapsedTime = TimeSpan.Zero;
                if (currentFrame >= frames.Count)
                {
                    currentFrame = 0;
                }
            }
        }
    }
}
