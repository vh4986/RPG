using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System;
using System.Collections.Generic;

namespace RPGGame
{
    class Boat : Sprite
    {
        Vector2 Speed;
        float TurnSpeed;
        public bool isIntersectingWithGrass = false;
        public int boatHitBoxRadius;
        public Rectangle rightCircle;
        public Rectangle leftCircle;
        Vector2 updatePosition;
        public bool isGoingRight;
        float paddleRotatingSpeed;
        List<Sprite> rocks = new List<Sprite>();
        Texture2D pixel;
        GraphicsDevice Graphics;

        //12/8/21- fix the way the knight gets on the boat and logic with the boat intersecting the land
        //or make the knight get on the boat if he's within a certain distance

        //public Rectangle biggerBoatHitBox => new Rectangle((int)((Position.X - 20) - Origin.X * Scale.X), (int)(Position.Y - Origin.Y * Scale.Y), (int)(ScaledWidth + 30), (int)ScaledHeight);
        public Boat(Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, Vector2 speed, SpriteEffects effect, float turnSpeed, float PaddleRotatingSpeed, List<Sprite> rock,
                GraphicsDevice graphics)
            : base(image, position, tint, origin, scale, effect)
        {
            Speed = speed;
            TurnSpeed = turnSpeed;
            Origin = new Vector2(Image.Width / 2, Image.Height / 2);
            paddleRotatingSpeed = PaddleRotatingSpeed;
            rocks = rock;
            Graphics = graphics;


            boatHitBoxRadius = (int)ScaledWidth / 2;

            pixel = new Texture2D(graphics, 1, 1);
            pixel.SetData(new Color[] { Color.Black * 0.3f});
        }

        public void Move(List<TileFromSprite> edges, Knight knight, Paddle Paddle)
        {
            isIntersectingWithGrass = false;
            updatePosition = new Vector2(Speed.X * (float)Math.Cos(Rotation), Speed.Y * (float)Math.Sin(Rotation));
            rightCircle = new Rectangle((int)(Position.X) - 15, (int)(Position.Y), boatHitBoxRadius, boatHitBoxRadius);
            leftCircle = new Rectangle((int)(Position.X) + 10, (int)Position.Y, boatHitBoxRadius, boatHitBoxRadius);

            KeyboardState ks = Keyboard.GetState();

            if (Paddle.Rotation > Math.PI || Paddle.Rotation < 0)
            {
                paddleRotatingSpeed = -paddleRotatingSpeed;
            }
            if (ks.IsKeyDown(Keys.W))
            {
                if (isIntersectingWithGrass == false && knight.isIntersectedWithBoat == true)
                {
                    Rotation -= TurnSpeed;
                    knight.Rotation -= TurnSpeed;
                }
            }
            else if (ks.IsKeyDown(Keys.S))
            {
                if (isIntersectingWithGrass == false && knight.isIntersectedWithBoat == true)
                {
                    Rotation += TurnSpeed;
                    knight.Rotation += TurnSpeed;
                }
            }
            else if (ks.IsKeyDown(Keys.A))
            {
                isGoingRight = false;
                //leftRectangle = new Rectangle((int)(Position.X + Speed.X - ScaledWidth / 2) - 5, (int)(Position.Y + Speed.Y - ScaledHeight / 2), 5, (int)ScaledHeight - 5);

                leftCircle = new Rectangle((int)(Position.X) +10, (int)Position.Y, boatHitBoxRadius, boatHitBoxRadius);
                
                foreach (TileFromSprite edge in edges)
                {
                    if (leftCircle.Intersects(edge.HitBox) && edge.tileType == TileTypes.waterTile)
                    {
                        isIntersectingWithGrass = true;
                    }
                }
                for (int i = 0; i < rocks.Count; i++)
                {
                    if (leftCircle.Intersects(rocks[i].HitBox))
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

            else if (ks.IsKeyDown(Keys.D))
            {
                isGoingRight = true;
                //rightRectangle = new Rectangle((int)(Position.X + Speed.X + ScaledWidth / 2) - 15, (int)(Position.Y + Speed.Y - ScaledHeight / 2), 5, (int)ScaledHeight - 5);


                foreach (TileFromSprite edge in edges)
                {
                    if (rightCircle.Intersects(edge.HitBox) && edge.tileType == TileTypes.waterTile)
                    {
                        isIntersectingWithGrass = true;
                    }
                }
                for (int i = 0; i < rocks.Count; i++)
                {
                    if (rightCircle.Intersects(rocks[i].HitBox))
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
