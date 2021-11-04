using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class LeftEnemy : Enemy
    {
        Random random;
        
        public LeftEnemy(Texture2D image, Texture2D arrow, Vector2 position, Color tint, Vector2 origin, Vector2 scale, Vector2 speed, SpriteEffects effect,
            List<Frames> walkingRightFrames, List<Frames> walkingLeftFrames, List<Frames> walkingDown, List<Frames> walkingUp, List<Frames> fightingRight,
            List<Frames> fightingLeft, List<Frames> fightingForward, List<Frames> fightingBackward, List<Frames> idleRight, List<Frames> idleLeft, List<Frames> idleUp,
            List<Frames> idleDown, Knight knight2, EnemyStates enemyStates, List<Frames> dyingFrames, SpriteFont font)
            :base(image, arrow, position, tint, origin, scale, speed, effect, walkingRightFrames, walkingLeftFrames, walkingDown, walkingUp, fightingRight,
                 fightingLeft, fightingForward, fightingBackward, idleRight, idleLeft, idleUp, idleDown, knight2, enemyStates, dyingFrames, font)
        {
            //-3,1
            random = new Random();
            arrowEffect = SpriteEffects.FlipHorizontally;
        }
        public override void Update(GameTime gameTime, Rectangle boundary)
        {if (isEnemyDead) return;
            if (Math.Sqrt(Math.Pow((knight.Position.X - Position.X), 2) + Math.Pow((knight.Position.Y - Position.Y), 2)) <= 200)
            {
                do
                {
                    arrowDirection = new Vector2(random.Next(-3, -1), random.Next(-1, 3));
                    double rotationAngle = Math.Atan(arrowDirection.Y / arrowDirection.X);
                    arrowRotation = (float)rotationAngle;
                } while (arrowDirection.X == 0);
                
                float xDistance = Math.Abs(knight.Position.X - Position.X);
                float yDistance = Math.Abs(knight.Position.Y - Position.Y);
                //if (Position.X > knight.Position.X && xDistance > yDistance && state != EnemyStates.fightingLeft)
                //{
                //    currentFrameIndex = 0;
                //    state = EnemyStates.fightingLeft;
                //}
            }
            else
            {
                currentFrameIndex = 0;
                state = EnemyStates.idleLeft;
            }
            if(knight.healthPoints == 0)
            {
                currentFrameIndex = 0;
                state = EnemyStates.idleLeft;
            }
            base.Update(gameTime, boundary);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(Font, $"{enemyHealthPoints}", new Vector2(Position.X, Position.Y - Origin.Y - 15), Color.Black);
        }
    }
}
