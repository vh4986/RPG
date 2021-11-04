using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class ItemHolder : Sprite
    {
        public Item item;
        MouseState lastMouseState;
        public ItemHolder(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, SpriteEffects effect)
            :base(image, position, tint, origin, scale, effect)
        {

        }

        public void SetContainedItem(Item containedItem)
        {
            item = containedItem;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if(item != null)
            {
                item.Position = Position;
                item.Draw(spriteBatch);
            }
        }
        public bool Clicked(Knight knight)
        {
            if (item == null)
            {
                return false;
            }
            MouseState ms = Mouse.GetState();
            
            if(ms.LeftButton == ButtonState.Pressed && HitBox.Contains(ms.X, ms.Y))
            {
                if (lastMouseState.LeftButton == ButtonState.Released)
                {
                    item.Init(knight);
                    knight.Effects.Add(item.EffectFunction);

                    item = null;
                }
                return true;    
            }
            lastMouseState = ms;
            return false;
        }  
        
    }
}
