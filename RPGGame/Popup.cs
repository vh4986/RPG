using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class Popup : Sprite
    {
        SpriteFont Font { get; set; }
        TimeSpan elapsedTime;
        TimeSpan intervalTime;
        public bool Visible { get; set; } = false;
        Item itemToDisplay;
        string firstLine;
        string secondLine;
        string thirdLine;
        public Popup(Texture2D image, Color tint, Vector2 origin, Vector2 scale, SpriteFont font)
            :base(image, Vector2.Zero, tint, origin, scale, SpriteEffects.None)
        {
            Font = font;
            elapsedTime = TimeSpan.Zero;
            intervalTime = TimeSpan.FromSeconds(3);
        }

        public void Update(GameTime gameTime)
        {
            if (Visible == true)
            {
                elapsedTime += gameTime.ElapsedGameTime;
                if (elapsedTime >= intervalTime)
                {
                    Visible = false;
                    elapsedTime = TimeSpan.Zero;
                }
            }
        }
        public void displayPopup(string text, string secondText, string thirdText)
        {
            Visible = true;
            firstLine = text;
            secondLine = secondText;
            thirdLine = thirdText;
        }
        
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if(Visible == true)
            {
                var size = Font.MeasureString(firstLine);
                Position = new Vector2(graphics.Viewport.Width - (Image.Width * Scale.X), 0);

                base.Draw(spriteBatch);
                spriteBatch.DrawString(Font, firstLine, new Vector2(Position.X + 25, Position.Y + 5), Color.Black);
                spriteBatch.DrawString(Font, secondLine, new Vector2(Position.X + 25, Position.Y + 35), Color.Black);
                spriteBatch.DrawString(Font, thirdLine, new Vector2(Position.X + 25, Position.Y + 60), Color.Black);
                if (itemToDisplay == null)
                {
                    return;
                }
            }
        }
        public void ClearPopup()
        {
            Visible = false;
        }
    }
}
