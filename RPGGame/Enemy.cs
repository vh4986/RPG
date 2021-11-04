using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    enum EnemyStates
    {
        idleRight,
        idleLeft,
        idleUp,
        idleDown,
        walkingRight,
        walkingLeft,
        walkingUp,
        walkingDown,
        fightingRight,
        fightingLeft,
        fightingForward,
        fightingBackward,
        dying,
    }
    class Enemy : Sprite
    {
        public EnemyStates state;
        TimeSpan elapsedTime;
        TimeSpan intervalTime;
        public int currentFrameIndex;
        Vector2 Speed;
        int xDirection = 1;
        List<Frames> idleRightFrames = new List<Frames>();
        List<Frames> idleLeftFrames = new List<Frames>();
        List<Frames> idleDownFrames = new List<Frames>();
        List<Frames> idleUpFrames = new List<Frames>();
        List<Frames> enemyWalkingLeftFrames = new List<Frames>();
        List<Frames> enemyWalkingRightFrames = new List<Frames>();
        List<Frames> walkingDownFrames = new List<Frames>();
        List<Frames> walkingUpFrames = new List<Frames>();
        List<Frames> fightingRightFrames = new List<Frames>();
        List<Frames> fightingLeftFrames = new List<Frames>();
        List<Frames> fightingForwardFrames = new List<Frames>();
        List<Frames> fightingBackwardFrames = new List<Frames>();
        List<Frames> dyingFrames = new List<Frames>();
        List<Arrow> arrows = new List<Arrow>();
        Dictionary<EnemyStates, List<Frames>> currentFrames;
        public Knight knight;
        Texture2D arrowImage;

        Stopwatch stopwatch = new Stopwatch();
        public bool isEnemyDead = false;
        bool isEnemyDying = false;

        TimeSpan elapsedArrowTime;
        TimeSpan intervalArrowTime;
        Frames currentFrame => currentFrames[state][currentFrameIndex];
        protected int enemyHealthPoints = 7;
        public SpriteFont Font;
        bool isSwordIntersecting = false;

        protected Vector2 arrowDirection;
        protected SpriteEffects arrowEffect;
        protected float arrowRotation;
        public Enemy(Texture2D image, Texture2D arrow, Vector2 position, Color tint, Vector2 origin, Vector2 scale, Vector2 speed, SpriteEffects effect,
            List<Frames> walkingRightFrames, List<Frames> walkingLeftFrames, List<Frames> walkingDown, List<Frames> walkingUp, List<Frames> fightingRight, 
            List<Frames> fightingLeft, List<Frames> fightingForward, List<Frames> fightingBackward, List<Frames> idleRight, List<Frames> idleLeft, List<Frames> idleUp,
            List<Frames> idleDown, Knight knight2, EnemyStates enemyStates, List<Frames> dying, SpriteFont font)
            : base(image, position, tint, origin, scale, effect)
        {
            enemyWalkingLeftFrames = walkingLeftFrames;
            enemyWalkingRightFrames = walkingRightFrames;
            walkingDownFrames = walkingDown;
            walkingUpFrames = walkingUp;
            fightingForwardFrames = fightingForward;
            fightingBackwardFrames = fightingBackward;
            fightingRightFrames = fightingRight;
            fightingLeftFrames = fightingLeft;
            idleRightFrames = idleRight;
            idleLeftFrames = idleLeft;
            idleUpFrames = idleUp;
            idleDownFrames = idleDown;
            dyingFrames = dying;
            arrowImage = arrow;
            knight = knight2;
            state = enemyStates;
            elapsedTime = TimeSpan.Zero;
            intervalTime = TimeSpan.FromMilliseconds(100);
            elapsedArrowTime = TimeSpan.Zero;
            intervalArrowTime = TimeSpan.FromMilliseconds(1000);
            currentFrameIndex = 0;
            Speed = speed;
            Font = font;

            currentFrames = new Dictionary<EnemyStates, List<Frames>>()
            {
                [EnemyStates.walkingRight] = enemyWalkingRightFrames,
                [EnemyStates.walkingLeft] = enemyWalkingLeftFrames,
                [EnemyStates.walkingDown] = walkingDownFrames,
                [EnemyStates.walkingUp] = walkingUpFrames,
                [EnemyStates.fightingRight] = fightingRightFrames,
                [EnemyStates.fightingLeft] = fightingLeftFrames,
                [EnemyStates.fightingForward] = fightingForwardFrames,
                [EnemyStates.fightingBackward] = fightingBackwardFrames,
                [EnemyStates.idleRight] = idleRightFrames,
                [EnemyStates.idleLeft] = idleLeftFrames,
                [EnemyStates.idleUp] = idleUpFrames,
                [EnemyStates.idleDown] = idleDownFrames,
                [EnemyStates.dying] = dyingFrames, 
            };
            SourceRectangle = enemyWalkingRightFrames[0].SourceRectangle;
        }

        public void updateAnimation(GameTime gameTime, List<Frames> frames)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime >= intervalTime)
            {
                currentFrameIndex++;
                elapsedTime = TimeSpan.Zero;
                if (currentFrameIndex >= frames.Count)
                {
                    currentFrameIndex = 0;
                }
            }
        }

        public void SpawnArrow()
        {
            //Go from bottom center to top
            //Translate back to center including scale
            //Position.Y - currentFrame.Origin.Y * Scale.Y + currentFrame.SourceRectangle.Height / 2 * Scale.Y
            float calculationForBottomCenterToTop = Position.Y - currentFrame.Origin.Y * Scale.Y;
            float calculationForGoingBackToCenter = currentFrame.SourceRectangle.Height / 2 * Scale.Y - 5;
            Arrow newArrow = new Arrow(arrowImage, new Vector2(Position.X, calculationForBottomCenterToTop + calculationForGoingBackToCenter), Color.White, new Vector2(0,0), 
                new Vector2(0.12f,0.12f), arrowDirection, arrowEffect, arrowRotation);
            arrows.Add(newArrow);
        }
        

        public void drawAnimation(SpriteBatch spriteBatch, Rectangle? sourceRectangle, Vector2 origin)
        {
            spriteBatch.Draw(Image, Position, sourceRectangle, Tint, Rotation, origin, Scale, Effect, LayerDepth);
            for(int i = 0; i < arrows.Count; i++)
            {
                 arrows[i].Draw(spriteBatch);
            }
        }

        KeyboardState lastKeyboardState;
        public virtual void Update(GameTime gameTime, Rectangle boundary)
        {
            if (isEnemyDead) return;
            KeyboardState ks = Keyboard.GetState();

            elapsedArrowTime += gameTime.ElapsedGameTime;

            for(int i = 0; i < arrows.Count; i++)
            {
                arrows[i].Update();
            }
            
            if (knight.IntersectingArrow == null)
            {
                knight.isIntersected = false;
                for (int i = 0; i < arrows.Count; i++)
                {
                    if (arrows[i].HitBox.Intersects(knight.HitBox))
                    {
                        knight.isIntersected = true;
                        knight.IntersectingArrow = arrows[i];
                        knight.intersectingCheck.Add(arrows[i]);
                        arrows.Remove(arrows[i]);
                        knight.healthPoints--;
                        break;
                    }
                }
            }
            else
            {
                if(!knight.IntersectingArrow.HitBox.Intersects(knight.HitBox))
                {
                    knight.IntersectingArrow = null;
                    knight.isIntersected = false;
                }
            }
 
            if((knight.state == States.fightingLeft || knight.state == States.fightingRight || knight.state == States.fightingForward 
                || knight.state == States.fightingBackward) && knight.currentFrameIndex == 5 && HitBox.Intersects(knight.HitBox) && isSwordIntersecting == false)
            {
                isSwordIntersecting = true;
                if (enemyHealthPoints > 0)
                {
                    enemyHealthPoints--;
                }
                
            }
            else
            {
                isSwordIntersecting = false;
            }
            if (enemyHealthPoints == 0)
            {
                if (isEnemyDying == false)
                {           
                    isEnemyDying = true;
                    currentFrameIndex = 0;
                    state = EnemyStates.dying;
                }
                else if (currentFrameIndex == currentFrames[EnemyStates.dying].Count - 1) //checking if you reached the last frame 
                {
                    isEnemyDead = true;

                    return;
                }
            }
          
            if (elapsedArrowTime >= intervalArrowTime && currentFrameIndex == 7)
            {
                SpawnArrow();
                elapsedArrowTime = TimeSpan.Zero;
            }
            if (elapsedTime >= intervalTime)
            {
                elapsedTime = TimeSpan.Zero;
            }
            updateAnimation(gameTime, currentFrames[state]);
            lastKeyboardState = ks;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Frames currentFrame = currentFrames[state][currentFrameIndex];

            Origin = currentFrame.Origin;
            SourceRectangle = currentFrame.SourceRectangle;

            drawAnimation(spriteBatch, SourceRectangle, Origin);
            
        }
    }
}
