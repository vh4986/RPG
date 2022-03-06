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
        public Rectangle rightRectangle;
        public Rectangle leftRectangle;
        Vector2 updatePosition;
        public bool isGoingRight;
        float paddleRotatingSpeed;
        List<Sprite> rocks = new List<Sprite>();
        Texture2D pixel;
        GraphicsDevice Graphics;
        Vector2 centerOfBoat;

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
            double rotationAngle = Rotation;

            double deltaX = (double)(boatHitBoxRadius * Math.Cos(rotationAngle));
            double deltaY = (double)(boatHitBoxRadius * Math.Sin(rotationAngle));
            centerOfBoat = new Vector2((int)(HitBox.X + ScaledWidth / 2), (int)(HitBox.Y + ScaledHeight / 2));

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
                leftRectangle = new Rectangle((int)(Position.X - deltaX), (int)(Position.Y - deltaY), 7, (int)ScaledHeight - 10);

                foreach (TileFromSprite edge in edges)
                {
                    if (leftRectangle.Intersects(edge.HitBox) && edge.tileType == TileTypes.waterTile)
                    {
                        isIntersectingWithGrass = true;
                    }
                }
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

            else if (ks.IsKeyDown(Keys.D))
            {
                isGoingRight = true;
                rightRectangle = new Rectangle((int)(Position.X + deltaX), (int)(Position.Y + deltaY), 7, (int)ScaledHeight - 10);


                foreach (TileFromSprite edge in edges)
                {
                    if (rightRectangle.Intersects(edge.HitBox) && edge.tileType == TileTypes.waterTile)
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
