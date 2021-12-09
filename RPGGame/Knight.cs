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
    enum States
    {
        none,
        idleRight,
        idleLeft,
        idleUp,
        idleDown,
        walkingLeft,
        walkingRight,
        walkingUp,
        walkingDown,
        fightingLeft,
        fightingRight,
        fightingForward,
        fightingBackward,
        dying,
    }
    enum Direction
    {
        up,
        down,
        left, 
        right,
    }
    class Knight : Sprite
    {

        public States state = States.idleRight;
        TimeSpan elapsedTime;
        TimeSpan intervalTime;
        public int currentFrameIndex;
        Direction direction;
        public Vector2 Speed;
        bool isDying = false;
        bool isDead = false;
        public bool isIntersected;
        public bool isIntersectedWithBoat = false;
        public Arrow IntersectingArrow = null;
        public List<Func<Knight, bool>> Effects = new List<Func<Knight, bool>>();
        Boat Boat;
        
        public SpriteFont Font { get; set; }
        List<Frames> WalkingLeftFrames = new List<Frames>();
        List<Frames> WalkingRightFrames = new List<Frames>();
        List<Frames> IdleRightFrames = new List<Frames>();
        List<Frames> IdleLeftFrames = new List<Frames>();
        List<Frames> IdleUpFrames = new List<Frames>();
        List<Frames> IdleDownFrames = new List<Frames>();
        List<Frames> WalkingUpFrames = new List<Frames>();
        List<Frames> WalkingDownFrames = new List<Frames>();
        List<Frames> FightingLeftFrames = new List<Frames>();
        List<Frames> FightingRightFrames = new List<Frames>();
        List<Frames> FightingForwardFrames = new List<Frames>();
        List<Frames> FightingBackwardFrames = new List<Frames>();
        List<Frames> DyingFrames = new List<Frames>();

        List<Rectangle> Hitboxes = new List<Rectangle>();
        Dictionary<States, Texture2D> spritesheets = new Dictionary<States, Texture2D>();
        Dictionary<States, List<Frames>> currentFrames;
        Texture2D pixel;

        GraphicsDevice Graphics;
        bool isMoving = true;
        public int healthPoints = 3;
        Stopwatch stopWatch = new Stopwatch();
        Rectangle rightRectangle;
        Rectangle leftRectangle;
        Rectangle upRectangle;
        Rectangle downRectangle;

        public Knight(Dictionary<States, Texture2D> spritesheets, Vector2 position, Color tint, Vector2 scale, Vector2 speed, SpriteFont font,
            List<Frames> walkingLeftFrames, List<Frames> walkingRightFrames, List<Frames> idleRightFrames, List<Frames> idleLeftFrames, List<Frames> idleUpFrames, List<Frames> idleDownFrames, 
            List<Frames> walkingUpFrames, List<Frames> walkingDownFrames, List<Frames> dyingFrames,
            List<Frames> fightingLeftFrames, List<Frames> fightingRight, List<Frames> fightingForward, List<Frames> fightingBackward,
                SpriteEffects effect, GraphicsDevice graphics, Boat boat)
            : base(null, position, tint, idleRightFrames[0].Origin, scale, effect)
        {
            FightingLeftFrames = fightingLeftFrames;
            FightingRightFrames = fightingRight;
            FightingForwardFrames = fightingForward;
            FightingBackwardFrames = fightingBackward;
            WalkingDownFrames = walkingDownFrames;
            DyingFrames = dyingFrames;
            WalkingUpFrames = walkingUpFrames;
            WalkingLeftFrames = walkingLeftFrames;
            WalkingRightFrames = walkingRightFrames;
            IdleRightFrames = idleRightFrames;
            IdleLeftFrames = idleLeftFrames;
            IdleUpFrames = idleUpFrames;
            IdleDownFrames = idleDownFrames;
            Speed = speed;
            Font = font;
            Boat = boat;
            elapsedTime = TimeSpan.Zero;
            intervalTime = TimeSpan.FromMilliseconds(100);
            currentFrameIndex = 0;
            stopWatch.Start();
            Graphics = graphics;
            pixel = new Texture2D(graphics, 1, 1);
            pixel.SetData(new Color[] { Color.Black * 0.3f });
            SourceRectangle = idleRightFrames[0].SourceRectangle;

            Image = spritesheets[state];
            this.spritesheets = spritesheets;

            currentFrames = new Dictionary<States, List<Frames>>()
            {
                [States.idleRight] = IdleRightFrames,
                [States.idleLeft] = IdleLeftFrames,
                [States.idleUp] = IdleUpFrames,
                [States.idleDown] = IdleDownFrames,
                [States.walkingLeft] = WalkingLeftFrames,
                [States.walkingRight] = WalkingRightFrames,
                [States.walkingUp] = WalkingUpFrames,
                [States.walkingDown] = WalkingDownFrames,
                [States.fightingLeft] = FightingLeftFrames,
                [States.fightingRight] = FightingRightFrames,
                [States.fightingForward] = FightingForwardFrames,
                [States.fightingBackward] = FightingBackwardFrames,
                [States.dying] = DyingFrames,
            };
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
        public void drawAnimation(SpriteBatch spriteBatch, Rectangle? sourceRectangle, Vector2 origin)
        {
            spriteBatch.Draw(spritesheets[state], Position, sourceRectangle, Tint, Rotation, origin, Scale, Effect, LayerDepth);
        }
        public void drawPoints(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, $"{healthPoints}", new Vector2(Position.X, Position.Y - Origin.Y - 15), Color.Black);
        }
        KeyboardState lastKeyboardState;


        public void Update(GameTime gameTime, List<Enemy> enemy, List<TileFromSprite> edges, Rectangle screenBoundary, Paddle paddle)
        {
            KeyboardState ks = Keyboard.GetState();
            if (isDead) return;

            for(int i = 0; i < Effects.Count; i++)
            {
                bool isFinished = Effects[i](this);
                if (isFinished)
                {
                    Effects.RemoveAt(i);
                    i--;
                }
            }
            if (Boat.leftCircle.Intersects(HitBox))
            {
                if (ks.IsKeyDown(Keys.B))
                {
                    Position = new Vector2(Boat.Position.X , Boat.Position.Y);
                    Rotation = Boat.Rotation;
                    isIntersectedWithBoat = true;
                    CheckHitboxIntersection(Boat, this);
                }
                Boat.Tint = Color.SlateGray;
                if(isIntersectedWithBoat == true)
                {
                    Boat.Tint = Color.White;
                }
            }
            else
            {
                Boat.Tint = Color.White;
            }
            if(Boat.isIntersectingWithGrass == true && ks.IsKeyDown(Keys.V))
            {

                if (Boat.isGoingRight == true)
                {
                    Position = new Vector2(Boat.Position.X + 50, Boat.Position.Y);
                    Rotation = 0;
                    isIntersectedWithBoat = false;
                }
                if (Boat.isGoingRight == false)
                {
                    Position = new Vector2(Boat.Position.X - 40, Boat.Position.Y);
                    Rotation = 0;
                    isIntersectedWithBoat = false;
                }       
            }
            if (healthPoints == 0)
            {
                if (isDying == false)
                {
                    isDying = true;
                    currentFrameIndex = 0;
                    state = States.dying;
                }
                else if (currentFrameIndex == currentFrames[States.dying].Count - 1)  
                {
                    isDead = true;
                    return;
                }
            }
            if(isIntersectedWithBoat == false)
            {
                if (ks.IsKeyDown(Keys.Right))
                {
                    isMoving = true;

                    rightRectangle = new Rectangle((int)(Position.X + Speed.X) + 5, (int)(Position.Y - SourceRectangle.Value.Height), 5, SourceRectangle.Value.Height - 5);
                    foreach (TileFromSprite edge in edges)
                    {
                        //does this work?

                        if (rightRectangle.Intersects(edge.HitBox) && edge.tileType == TileTypes.waterTile)
                        {
                            isMoving = false;
                        }
                    }
                    if(rightRectangle.X + rightRectangle.Width > screenBoundary.X + screenBoundary.Width)
                    {
                        isMoving = false;
                    }
                    if (isMoving == true)
                    {
                        if (lastKeyboardState.IsKeyUp(Keys.Right))
                        {
                            state = States.walkingRight;
                            Effect = SpriteEffects.None;
                            currentFrameIndex = 0;
                        }
                        direction = Direction.right;
                        Position.X += Speed.X * 1;
                    }
                }
                else if (ks.IsKeyDown(Keys.Left))
                {
                    isMoving = true;
                    leftRectangle = new Rectangle((int)(Position.X - Speed.X) - 10, (int)(Position.Y - SourceRectangle.Value.Height), 5, SourceRectangle.Value.Height - 5);

                    foreach (TileFromSprite edge in edges)
                    {
                        if (leftRectangle.Intersects(edge.HitBox) && edge.tileType == TileTypes.waterTile)
                        {
                            isMoving = false;
                        }
                    }
                    if (leftRectangle.X + leftRectangle.Width < screenBoundary.X)
                    {
                        isMoving = false;
                    }
                    if (isMoving == true)
                    {
                        if (lastKeyboardState.IsKeyUp(Keys.Left))
                        {
                            state = States.walkingLeft;
                            Effect = SpriteEffects.None;
                            currentFrameIndex = 0;
                        }
                        direction = Direction.left;
                        Position.X += Speed.X * -1;
                    }

                }
                else if (ks.IsKeyDown(Keys.Up))
                {
                    isMoving = true;
                    upRectangle = new Rectangle((int)(Position.X - Speed.X) - 10, (int)(Position.Y - SourceRectangle.Value.Height) - 5, SourceRectangle.Value.Width - 10, 3);

                    foreach (TileFromSprite edge in edges)
                    {
                        if (upRectangle.Intersects(edge.HitBox) && edge.tileType == TileTypes.waterTile)
                        {
                            isMoving = false;
                        }
                    }
                    if (upRectangle.Y + upRectangle.Height < screenBoundary.Y)
                    {
                        isMoving = false;
                    }
                    if (isMoving == true)
                    {
                        if (lastKeyboardState.IsKeyUp(Keys.Up))
                        {
                            state = States.walkingUp;
                            Effect = SpriteEffects.FlipHorizontally;
                            currentFrameIndex = 0;
                        }
                        direction = Direction.up;
                        Position.Y += Speed.Y * -1;
                    }

                }
                else if (ks.IsKeyDown(Keys.Down))
                {
                    isMoving = true;
                    downRectangle = new Rectangle((int)(Position.X - Speed.X) - 15, (int)(Position.Y) - 5, SourceRectangle.Value.Width - 5, 5);

                    foreach (TileFromSprite edge in edges)
                    {
                        if (downRectangle.Intersects(edge.HitBox) && edge.tileType == TileTypes.waterTile)
                        {
                            isMoving = false;
                        }
                    }
                    if (downRectangle.Y + downRectangle.Height > screenBoundary.Y + screenBoundary.Height)
                    {
                        isMoving = false;
                    }
                    if (isMoving == true)
                    {
                        if (lastKeyboardState.IsKeyUp(Keys.Down))
                        {
                            state = States.walkingDown;
                            Effect = SpriteEffects.FlipHorizontally;
                            currentFrameIndex = 0;
                        }
                        direction = Direction.down;
                        Position.Y += Speed.Y * 1;
                    }
                }
                else if (ks.IsKeyDown(Keys.Space))
                {
                    if (lastKeyboardState.IsKeyUp(Keys.Space))
                    {
                        if (direction == Direction.right)
                        {
                            state = States.fightingRight;
                            currentFrameIndex = 0;
                        }
                        if (direction == Direction.left)
                        {
                            state = States.fightingLeft;
                            currentFrameIndex = 0;
                        }
                        if (direction == Direction.down)
                        {
                            state = States.fightingForward;
                            currentFrameIndex = 0;
                        }
                        if (direction == Direction.up)
                        {
                            state = States.fightingBackward;
                            currentFrameIndex = 0;
                        }
                    }
                }
                else if (state != States.dying)
                {
                    if (direction == Direction.right)
                    {
                        state = States.idleRight;
                        currentFrameIndex = 0;
                    }
                    if (direction == Direction.left)
                    {
                        state = States.idleLeft;
                        currentFrameIndex = 0;
                    }
                    if (direction == Direction.up)
                    {
                        state = States.idleUp;
                        currentFrameIndex = 0;
                    }
                    if (direction == Direction.down)
                    {
                        state = States.idleDown;
                        currentFrameIndex = 0;
                    }

                    if (!(lastKeyboardState.IsKeyDown(Keys.Space) && ks.IsKeyUp(Keys.Space)))
                    {
                        Effect = SpriteEffects.None;
                    }
                }
            }
            if (isDead == false)
            {
                updateAnimation(gameTime, currentFrames[state]);
            }
            lastKeyboardState = ks;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Frames currentFrame = currentFrames[state][currentFrameIndex];

            Origin = currentFrame.Origin;
            SourceRectangle = currentFrame.SourceRectangle;

            drawAnimation(spriteBatch, SourceRectangle, Origin);
            //spriteBatch.Draw(pixel, new Vector2(downRectangle.X, downRectangle.Y), null, Color.Black, 0f, Vector2.Zero, new Vector2(downRectangle.Width, downRectangle.Height), SpriteEffects.None, 0);
            //spriteBatch.Draw(pixel, new Vector2(upRectangle.X, upRectangle.Y), null, Color.Black, 0f, Vector2.Zero, new Vector2(upRectangle.Width, upRectangle.Height), SpriteEffects.None, 0);
            //spriteBatch.Draw(pixel, new Vector2(leftRectangle.X, leftRectangle.Y), null, Color.Black, 0f, Vector2.Zero, new Vector2(leftRectangle.Width, leftRectangle.Height), SpriteEffects.None, 0);
            //spriteBatch.Draw(pixel, new Vector2(rightRectangle.X, rightRectangle.Y), null, Color.Black, 0f, Vector2.Zero, new Vector2(rightRectangle.Width, rightRectangle.Height), SpriteEffects.None, 0);
            drawPoints(spriteBatch);
        }
    }
}
