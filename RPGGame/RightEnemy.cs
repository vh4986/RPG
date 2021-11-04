using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class RightEnemy : Enemy
    {
        Random random;
        public RightEnemy(Texture2D image, Texture2D arrow, Vector2 position, Color tint, Vector2 origin, Vector2 scale, Vector2 speed, SpriteEffects effect,
            List<Frames> walkingRightFrames, List<Frames> walkingLeftFrames, List<Frames> walkingDown, List<Frames> walkingUp, List<Frames> fightingRight,
            List<Frames> fightingLeft, List<Frames> fightingForward, List<Frames> fightingBackward, List<Frames> idleRight, List<Frames> idleLeft, List<Frames> idleUp,
            List<Frames> idleDown, Knight knight2, EnemyStates enemyStates, List<Frames> dyingFrames, SpriteFont font)
            : base(image, arrow, position, tint, origin, scale, speed, effect, walkingRightFrames, walkingLeftFrames, walkingDown, walkingUp, fightingRight,
                 fightingLeft, fightingForward, fightingBackward, idleRight, idleLeft, idleUp, idleDown, knight2, enemyStates, dyingFrames, font)
        {
            random = new Random();
        }
        public override void Update(GameTime gameTime, Rectangle boundary)
        {
            if (isEnemyDead) return;
            if (Math.Sqrt(Math.Pow((knight.Position.X - Position.X), 2) + Math.Pow((knight.Position.Y - Position.Y), 2)) <= 200)
            {
                arrowDirection = new Vector2(random.Next(1, 3), random.Next(-1, 3));
                double rotationAngle = Math.Atan(arrowDirection.Y/arrowDirection.X);
                arrowRotation = (float)rotationAngle;
                float xDistance = Math.Abs(knight.Position.X - Position.X);
                float yDistance = Math.Abs(knight.Position.Y - Position.Y);
                //if (Position.X < knight.Position.X && xDistance > yDistance && state != EnemyStates.fightingRight)
                //{
                //    currentFrameIndex = 0;
                //    state = EnemyStates.fightingRight;
                //}
            }
            else
            {
                currentFrameIndex = 0;
                state = EnemyStates.idleRight;
            }
            if(knight.healthPoints == 0)
            {
                currentFrameIndex = 0;
                state = EnemyStates.idleRight;
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
