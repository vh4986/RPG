using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    enum TreasureFrames
    {
        open,
        closed,
    };
    
    class Treasure : Sprite
    {
        TreasureFrames frame = TreasureFrames.closed;
        public Texture2D closedTreasure;
        public Texture2D openTreasure;

        public Treasure(Texture2D closedTreasure, Texture2D openTreasure, Vector2 position, Color tint, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects)
            :base(closedTreasure, position, tint, origin, scale, spriteEffects)
        {
            this.closedTreasure = closedTreasure;
            this.openTreasure = openTreasure;
        }

        public void OpenOrClose(TreasureFrames tFrame)
        {
            frame = tFrame;
            switch(frame)
            {
                case TreasureFrames.closed:
                    Image = closedTreasure;
                    break;
                case TreasureFrames.open:
                    Image = openTreasure;
                    break;
            }
        }
    }
}
