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
    public class Character : Sprite
    {
        Vector2 Speed;

        Dictionary<Keys, Vector2> MovementMap;

        LerpData data = new LerpData();
        public Character(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, Vector2 speed, SpriteEffects effect)
            : base(image, position, tint, origin, scale, effect)
        {
            MovementMap = new Dictionary<Keys, Vector2>();
            Speed = speed;

            MovementMap.Add(Keys.Up, new Vector2(0, -1) * Speed);
            MovementMap.Add(Keys.Down, new Vector2(0, 1) * Speed);
            MovementMap.Add(Keys.Left, new Vector2(-1, 0) * Speed);
            MovementMap.Add(Keys.Right, new Vector2(1, 0) * Speed);
        }

        public void Update(KeyboardState ks, MouseState ms)
        {
            Keys[] allPressedKeys = ks.GetPressedKeys();
            for(int i = 0; i < allPressedKeys.Length; i++)
            {
                Keys currentKey = allPressedKeys[i];
                if (MovementMap.ContainsKey(currentKey))
                {
                    data.Stop();


                    Vector2 direction = MovementMap[currentKey];
                    Position += direction;
                }
            }

            if(ms.LeftButton == ButtonState.Pressed)
            {
                data.SetUp(Position, new Vector2(ms.X, ms.Y), 0.01f);
            }

            Vector2? location = data.Update();
            if(location.HasValue)
            {
                Position = location.Value;
            }

            //switch on lerp state
            //case Upodate:
            // if LerpData.Current >= 1, set it to end
        }
    }
}
