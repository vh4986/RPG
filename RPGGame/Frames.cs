using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class Frames
    {
        public Rectangle SourceRectangle;
        public Vector2 Origin;
        
        Frames(int x, int y, int width, int height)
        {
            SourceRectangle = new Rectangle(x, y, width, height);
        }
        public Frames(int x, int y, int width, int height, Vector2 origin)
            : this(x, y, width, height)
        {
            Origin = origin;
        }

        public Frames(int x, int y, int width, int height, OriginType type)
            : this(x, y, width, height)
        {
            switch (type)
            {
                case OriginType.TopLeft:
                    Origin = Vector2.Zero;
                    break;
                case OriginType.Center:
                    Origin = new Vector2(width / 2f, height / 2f);
                    break;
                case OriginType.BottomCenter:
                    Origin = new Vector2(width / 2f, height);
                    break;
                default:
                    throw new Exception("this is bad");
            }
        }
    }
}
