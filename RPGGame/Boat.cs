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
    class Boat : Sprite
    {
        Vector2 Speed;
        float TurnSpeed;
        public bool isIntersectingWithGrass = false;
        public Rectangle rightRectangle;
        public Rectangle leftRectangle;
        Vector2 updatePosition;
        public bool isGoingRight;
        float paddleRotatingSpeed;
        List<Sprite> rocks = new List<Sprite>();

        public Rectangle biggerBoatHitBox => new Rectangle((int)((Position.X - 20)- Origin.X * Scale.X), (int)(Position.Y - Origin.Y * Scale.Y), (int)(ScaledWidth + 30), (int)ScaledHeight);
        public Boat(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, Vector2 speed, SpriteEffects effect, float turnSpeed, float PaddleRotatingSpeed, List<Sprite> rock)
            : base(image, position, tint, origin, scale, effect)
        {
            Speed = speed;
            TurnSpeed = turnSpeed;
            Origin = new Vector2(Image.Width / 2, Image.Height / 2);
            paddleRotatingSpeed = PaddleRotatingSpeed;
            rocks = rock;
        }

        public void Move(List<Vector2> edges, Knight knight, Paddle Paddle)// TileTypes tile)
        {
            updatePosition = new Vector2(Speed.X * (float)Math.Cos(Rotation), Speed.Y * (float)Math.Sin(Rotation));
            KeyboardState ks = Keyboard.GetState();
           
            if(Paddle.Rotation > Math.PI || Paddle.Rotation < 0)
            {
                paddleRotatingSpeed = -paddleRotatingSpeed;
            }
            if (ks.IsKeyDown(Keys.A))
            {
                if(isIntersectingWithGrass == false && knight.isIntersectedWithBoat == true)
                {
                    Rotation -= TurnSpeed;
                    knight.Rotation -= TurnSpeed; 
                }
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                if(isIntersectingWithGrass == false && knight.isIntersectedWithBoat == true)
                {
                    Rotation += TurnSpeed;
                    knight.Rotation += TurnSpeed; 
                }
            }
            else if (ks.IsKeyDown(Keys.W))
            {
                isIntersectingWithGrass = false;
                isGoingRight = false;
                leftRectangle = new Rectangle((int)(Position.X + Speed.X - ScaledWidth/2) - 5, (int)(Position.Y + Speed.Y - ScaledHeight/2), 5, (int)ScaledHeight - 5);
                foreach (Vector2 edge in edges)
                {
                    //??
                    if (leftRectangle.Contains(edge))
                    {
                        isIntersectingWithGrass = true;
                    }
                }
                //still check if tile is grassTile
                for (int i = 0; i < rocks.Count; i++)
                {
                    if (leftRectangle.Intersects(rocks[i].HitBox))
                    {
                        isIntersectingWithGrass = true;
                    }
            }
                if (isIntersectingWithGrass == false && knight.isIntersectedWithBoat == true)
                {
                    Position -= updatePosition;
                    knight.Position -= updatePosition;
                    Paddle.Position -= updatePosition;
                    Effect = SpriteEffects.None;
                    knight.state = States.idleLeft;
                    Paddle.Rotation += paddleRotatingSpeed;
                }
            }
            else if (ks.IsKeyDown(Keys.S))
            {
                isIntersectingWithGrass = false;
                isGoingRight = true;
                rightRectangle = new Rectangle((int)(Position.X + Speed.X + ScaledWidth /2) - 15, (int)(Position.Y + Speed.Y - ScaledHeight /2), 5, (int)ScaledHeight - 5);
                foreach (Vector2 edge in edges)
                {
                    if (rightRectangle.Contains(edge))
                    {
                        isIntersectingWithGrass = true;
                    }
                }
                for (int i = 0; i < rocks.Count; i++)
                {
                    if (rightRectangle.Intersects(rocks[i].HitBox))
                    {
                        isIntersectingWithGrass = true;
                    }
                }
                if (isIntersectingWithGrass == false && knight.isIntersectedWithBoat == true)
                {
                    Position += updatePosition;
                    knight.Position += updatePosition;
                    Paddle.Position += updatePosition;
                    Effect = SpriteEffects.FlipHorizontally;
                    knight.state = States.idleRight;
                    Paddle.Rotation += paddleRotatingSpeed;
                }
            }
        }
    }
}
