using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    public class Sprite
    {
        public Texture2D Image;
        public Vector2 Position;
        public Color Tint;
        public Vector2 Origin;
        public Vector2 Scale;
        public float Rotation;
        public float LayerDepth;
        public SpriteEffects Effect;
        public Rectangle? SourceRectangle;
        public virtual Rectangle HitBox
        {
            get
            {
                if(SourceRectangle == null)
                {
                    return new Rectangle((int)(Position.X - Origin.X * Scale.X), (int)(Position.Y - Origin.Y * Scale.Y), (int)(ScaledWidth), (int)(ScaledHeight));
                }
                return new Rectangle((int)(Position.X - Origin.X * Scale.X), (int)(Position.Y - Origin.Y * Scale.Y), (int)(SourceRectangle.Value.Width * Scale.X), (int)(SourceRectangle.Value.Height * Scale.Y));
            }
        }

        public float ScaledWidth
        {
            get
            {
                return Image.Width * Scale.X;
            }
        }
        public float ScaledHeight
        {
            get
            {
                return Image.Height * Scale.Y;
            }
        }

        public List<Sprite> intersectingCheck = new List<Sprite>();
        public List<Sprite> previouslyIntersectingCheck = new List<Sprite>();
        public bool CheckHitboxIntersection(Sprite sprite1, Sprite sprite2)
        {
            if (sprite1.HitBox.Intersects(sprite2.HitBox))
            {
                sprite1.intersectingCheck.Add(sprite2);
                sprite2.intersectingCheck.Add(sprite1);
                return true;
            }
            return false;
        }

        public Sprite(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, SpriteEffects effect, Rectangle? sourceRectangle = null)
        {
            Image = image;
            Position = position;
            Tint = tint;
            Origin = origin;
            Scale = scale;
            Effect = effect;
            Rotation = 0;
            LayerDepth = 0;
            SourceRectangle = sourceRectangle;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(Image == null)
            {
                return;
            }
            spriteBatch.Draw(Image, Position, SourceRectangle, Tint, Rotation, Origin, Scale, Effect, LayerDepth);
        }

    }
}
