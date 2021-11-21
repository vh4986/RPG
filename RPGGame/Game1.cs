using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace RPGGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TileFromSprite : Sprite
    {
        public TileTypes tileType;

        public TileFromSprite(TileTypes type, Texture2D image, Vector2 position, Color tint, Vector2 origin, Vector2 scale, SpriteEffects effect)
            : base(image, position, tint, origin, scale, effect)
        {
            tileType = type;
        }
    }
    public partial class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Character character;
        List<ItemHolder> square = new List<ItemHolder>();
        List<Item> apple = new List<Item>();
        List<Item> healthPotion = new List<Item>();
        List<Item> speedPotion = new List<Item>();
        List<Enemy> enemies = new List<Enemy>();

        Treasure treasure;
        //Sprite squares;
        //Item apple;
        Knight knight;
        Popup popUp;
        Boat boat;
        Paddle paddle;
        TileFromSprite[,] Grid;
        Inventory inventory;
        Rectangle boundary;
        List<Sprite> rocks = new List<Sprite>();
        //List<Frames> frames = new List<Frames>();
        Random random;
        Random newRandom;
        Color selectedColor;
        int filledPixels = 0;


        HashSet<TwoDArrayIndex> Spots = new HashSet<TwoDArrayIndex>();
        Texture2D pixel;
        SpriteFont font;
        Vector2 scale = new Vector2(0.1f, 0.1f);
        int tileSize = 50;

        List<Tile> tiles;

        Texture2D waterTile;
        Texture2D grassTile;
        Texture2D stoneTile;
        Texture2D sandTile;
        class Tile
        {
            public int X { get; set; }
            public int Y { get; set; }
            public TileTypes Type;

            public Tile(int x, int y, TileTypes type)
            {
                X = x;
                Y = y;
                Type = type;
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        List<TileFromSprite> edges = new List<TileFromSprite>();
        public void floodFill()
        {
            selectedColor = Color.DarkGreen;

            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    Sprite currentSprite = new Sprite(null, new Vector2(x * tileSize, y * tileSize), Color.White, new Vector2(0, 0), new Vector2(tileSize, tileSize), SpriteEffects.None);

                    //Grid[y, x] = currentSprite;

                    Spots.Add(new TwoDArrayIndex(y, x));
                }
            }

            int floodx = random.Next(0, GraphicsDevice.Viewport.Width / tileSize);
            int floody = random.Next(0, GraphicsDevice.Viewport.Height / tileSize);

            List<Color> randomColors = new List<Color>
            {
                Color.DarkGreen,
                Color.DeepSkyBlue,
            };
            int index = random.Next(randomColors.Count);
            //PartialFloodFill(floodx, floody, randomColors[index]);
            //Find empty slot, continue regenerating indexies while the tint is white
            //bool noEmptyTiles = false;
            for (int i = 0; i < 4; i++)
            {
                int tempX = random.Next(0, GraphicsDevice.Viewport.Width / tileSize);
                int tempY = random.Next(0, GraphicsDevice.Viewport.Height / tileSize);

                if (Grid[tempY, tempX].Tint == Color.White)
                {
                    //PartialFloodFill(tempY, tempX, randomColors[random.Next(randomColors.Count)]);
                }
            }


            fillingSpots();
            for (int y = 1; y < Grid.GetLength(0) - 1; y++)
            {
                for (int x = 1; x < Grid.GetLength(1) - 1; x++)
                {
                    if (Grid[y, x].Tint != Color.DarkGreen)
                    {
                        continue;
                    }
                    if (Grid[y - 1, x].Tint == Color.DeepSkyBlue || Grid[y + 1, x].Tint == Color.DeepSkyBlue || Grid[y, x + 1].Tint == Color.DeepSkyBlue ||
                        Grid[y, x - 1].Tint == Color.DeepSkyBlue || Grid[y + 1, x + 1].Tint == Color.DeepSkyBlue || Grid[y + 1, x - 1].Tint == Color.DeepSkyBlue ||
                        Grid[y - 1, x - 1].Tint == Color.DeepSkyBlue || Grid[y - 1, x + 1].Tint == Color.DeepSkyBlue)
                    {
                        edges.Add(Grid[y, x]);
                    }
                }
            }

        }
        public void Deserialize()
        {
            string fileContents = File.ReadAllText(@"C:\Users\Veda.Hingarh\Documents\GitHub\RPG\MapEditor\bin\Debug\tiles.json");
            tiles = JsonConvert.DeserializeObject<List<Tile>>(fileContents);
        }
        Dictionary<TileTypes, Texture2D> tileTypesToImage;
        void checkForSurroundingTiles(int x, int y, TileTypes tile)
        {
            List<bool> tileCheck = new List<bool>();
            if (Grid[y, x].tileType != tile) return;

            if (x - 1 >= 0)
            {
                tileCheck.Add(Grid[y, x - 1].tileType == tile);
            }
            if (x + 1 < Grid.GetLength(1))
            {
                tileCheck.Add(Grid[y, x + 1].tileType == tile);
            }
            if (y - 1 >= 0)
            {
                tileCheck.Add(Grid[y - 1, x].tileType == tile);
            }
            if (y + 1 < Grid.GetLength(0))
            {
                tileCheck.Add(Grid[y + 1, x].tileType == tile);
            }

            int countOfFalses = tileCheck.Where(b => b == false).Count();
            if (countOfFalses > 0)
            {
                edges.Add(Grid[y, x]);
            }
        }
        protected override void LoadContent()
        {
            //tileSize = (int)(255 * scale.X);
            //Grid = new Sprite[GraphicsDevice.Viewport.Height / tileSize + 1, GraphicsDevice.Viewport.Width / tileSize + 1];
            Grid = new TileFromSprite[20, 30];

            Deserialize();

            waterTile = Content.Load<Texture2D>("waterTile");
            grassTile = Content.Load<Texture2D>("grassTile");
            stoneTile = Content.Load<Texture2D>("stoneTile2");
            sandTile = Content.Load<Texture2D>("SandTileV2");

            tileTypesToImage = new Dictionary<TileTypes, Texture2D>
            {
                {TileTypes.grassTile, grassTile},
                {TileTypes.waterTile, waterTile},
                {TileTypes.stoneTile, stoneTile},
                {TileTypes.sandTile, sandTile}
            };


            int tileIndex = 0;
            float tileScale = 0.08f;
            int tileSize = (int)(255 * tileScale);
            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    Tile currTile = tiles[tileIndex];
                    Grid[y, x] = new TileFromSprite(currTile.Type, tileTypesToImage[currTile.Type], new Vector2(currTile.X * tileSize, currTile.Y * tileSize), Color.White, Vector2.Zero, new Vector2(tileScale, tileScale), SpriteEffects.None);
                    tileIndex++;
                }
            }
            // Create a new SpriteBatch, which can be used to draw textures.
            random = new Random(42);
            newRandom = new Random();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            //floodFill();


            //for (int y = 0; y < Grid.GetLength(0); y++)
            //{
            //    for (int x = 0; x < Grid.GetLength(1); x++)
            //    {
            //        Sprite currentSprite = new Sprite(null, new Vector2(x * tileSize, y * tileSize), Color.White, new Vector2(0, 0), scale, SpriteEffects.None); 
            //        Grid[y, x] = currentSprite;

            //        Spots.Add(new TwoDArrayIndex(y, x));
            //    }
            //}

            //int floodx = random.Next(0, GraphicsDevice.Viewport.Width / tileSize);
            //int floody = random.Next(0, GraphicsDevice.Viewport.Height / tileSize);

            //List<Texture2D> randomTextures = new List<Texture2D>
            //{
            //    waterTile,
            //    grassTile,
            //};
            //int index = random.Next(randomTextures.Count);
            //PartialFloodFill(floodx, floody, randomTextures[index]);

            //for (int i = 0; i < 4; i++)
            //{
            //    int tempX = random.Next(0, GraphicsDevice.Viewport.Width / tileSize);
            //    int tempY = random.Next(0, GraphicsDevice.Viewport.Height / tileSize);

            //    if (Grid[tempY, tempX].Image == null)
            //    {
            //        PartialFloodFill(tempY, tempX, randomTextures[random.Next(randomTextures.Count)]);
            //    }
            //}

            //fillingSpots();


            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    checkForSurroundingTiles(x, y, TileTypes.waterTile);
                    checkForSurroundingTiles(x, y, TileTypes.sandTile);
                }
            }
            graphics.PreferredBackBufferWidth = 20 * Grid.GetLength(1);
            graphics.PreferredBackBufferHeight = 20 * Grid.GetLength(0);
            graphics.ApplyChanges();
            OriginType originType = OriginType.BottomCenter;

            #region define frames
            List<Frames> idleRightFrames = new List<Frames>
            {
                new Frames(1,3,34,52, originType),
            };
            List<Frames> idleLeftFrames = new List<Frames>
            {
                new Frames(8,132,25,50, originType),
            };
            List<Frames> idleUpFrames = new List<Frames>
            {
                new Frames(3,67,33,50, originType),
            };
            List<Frames> idleDownFrames = new List<Frames>
            {
                new Frames(4,195,32,53, originType),
            };
            List<Frames> walkingLeftFrames = new List<Frames>
            {
                new Frames(8,132,25,50,   originType),
                new Frames(68,132,29,51,  originType),
                new Frames(135,132,26,50, originType),
                new Frames(198,132,29,50, originType),
                new Frames(263,132,29,50, originType),
                new Frames(327,132,31,50, originType),
                new Frames(391,132,28,50, originType),
                new Frames(455,132,28,50, originType),
                new Frames(520,132,25,50, originType),
            };
            List<Frames> walkingRightFrames = new List<Frames>
            {
                new Frames(19,717,25,50,  originType),
                new Frames(84,717,28,50,  originType),
                new Frames(146,717,27,50, originType),
                new Frames(210,717,27,50, originType),
                new Frames(273,717,28,50, originType),
                new Frames(337,717,28,50, originType),
                new Frames(401,717,28,50, originType),
                new Frames(465,717,28,50, originType),
                new Frames(529,717,28,50, originType),
            };
            List<Frames> walkingUpFrames = new List<Frames>
            {
                new Frames(3,67,33,50,   originType),
                new Frames(68,68,33,50,  originType),
                new Frames(132,67,34,51, originType),
                new Frames(195,67,33,51, originType),
                new Frames(259,67,34,52, originType),
                new Frames(324,67,32,51, originType),
                new Frames(387,67,36,51, originType),
                new Frames(452,68,32,51, originType),
                new Frames(515,67,34,51, originType),
            };
            List<Frames> walkingDownFrames = new List<Frames>
            {
                new Frames(4,195,32,53,   originType),
                new Frames(67,195,34,53,  originType),
                new Frames(131,195,34,53, originType),
                new Frames(197,195,33,53, originType),
                new Frames(259,195,33,53, originType),
                new Frames(324,195,33,53, originType),
                new Frames(387,195,33,53, originType),
                new Frames(452,195,32,50, originType),
                new Frames(516,195,33,53, originType),
            };
            List<Frames> fightingLeftFrames = new List<Frames>
            {
                new Frames(3,218,54,49,   new Vector2(48,48)),
                new Frames(217,220,31,47, new Vector2(23,47)),
                new Frames(416,218,35,49, new Vector2(16,49)),
                new Frames(596,218,57,49, new Vector2(27,49)),
                new Frames(733,218,93,49, new Vector2(80,49)),
                new Frames(927,218,91,49, new Vector2(77,49)),
            };
            List<Frames> fightingRightFrames = new List<Frames>
            {
                new Frames(35,602,55,48,new Vector2(13,48)),
                new Frames(228,604,31,47,new Vector2(15,47)),
                new Frames(408,602,36,49,new Vector2(26,49)),
                new Frames(591,602,57,49,new Vector2(36,49)),
                new Frames(802,602,93,49,new Vector2(18,49)),
                new Frames(994,602,91,49,new Vector2(18,49)),
            };
            List<Frames> fightingBackwardFrames = new List<Frames>
            {
                new Frames(22,26,39,47,  new Vector2(25,47)),
                new Frames(205,27,47,47, new Vector2(34,47)),
                new Frames(398,27,45,47, new Vector2(31,47)),
                new Frames(579,26,58,48, new Vector2(44,48)),
                new Frames(771,7,81,67,  new Vector2(45,67)),
                new Frames(992,10,77,64, new Vector2(15,64)),
            };
            List<Frames> fightingForwardFrames = new List<Frames>
            {
                new Frames(22,409,39,51, new Vector2(26,51)),
                new Frames(205,409,47,50,new Vector2(33,50)),
                new Frames(398,411,46,48,new Vector2(32,48)),
                new Frames(582,410,56,48,new Vector2(42,48)),
                new Frames(771,409,81,67,new Vector2(47,48)),
                new Frames(993,409,76,64,new Vector2(16,49)),
            };
            List<Frames> enemyIdleRightFrames = new List<Frames>
            {
                new Frames(10,715,36,51,  originType),
            };
            List<Frames> enemyIdleLeftFrames = new List<Frames>
            {
                new Frames(16,585,40,54,  originType),
            };
            List<Frames> enemyIdleUpFrames = new List<Frames>
            {
                new Frames(17,523,30,50,  originType),
            };
            List<Frames> enemyIdleDownFrames = new List<Frames>
            {
                new Frames(17,646,30,56,  originType),
            };
            List<Frames> enemyWalkingLeft = new List<Frames>
            {
                new Frames(16,585,40,54,  originType),
                new Frames(80,585,40,54,  originType),
                new Frames(144,585,40,54, originType),
                new Frames(208,585,40,54, originType),
                new Frames(271,585,40,54, originType),
                new Frames(338,585,40,54, originType),
                new Frames(402,585,40,54, originType),
                new Frames(466,585,40,54, originType),
                new Frames(530,585,40,54, originType),
            };
            List<Frames> enemyWalkingRight = new List<Frames>
            {
                new Frames(10,715,36,51,  originType),
                new Frames(74,715,36,51,  originType),
                new Frames(138,715,36,51, originType),
                new Frames(202,715,36,51, originType),
                new Frames(266,715,36,51, originType),
                new Frames(330,715,36,51, originType),
                new Frames(394,715,36,51, originType),
                new Frames(458,715,36,51, originType),
                new Frames(522,715,36,51, originType),
            };
            List<Frames> enemyWalkingUp = new List<Frames>
            {
                new Frames(17,523,30,50,  originType),
                new Frames(81,523,30,50,  originType),
                new Frames(145,523,30,50, originType),
                new Frames(209,523,30,50, originType),
                new Frames(273,523,30,50, originType),
                new Frames(337,523,30,50, originType),
                new Frames(401,523,30,50, originType),
                new Frames(465,523,30,50, originType),
                new Frames(529,523,30,50, originType),
            };
            List<Frames> enemyWalkingDown = new List<Frames>
            {
                new Frames(17,646,30,56,  originType),
                new Frames(81,646,30,56,  originType),
                new Frames(145,646,30,56, originType),
                new Frames(209,646,30,56, originType),
                new Frames(273,646,30,56, originType),
                new Frames(337,646,30,56, originType),
                new Frames(401,646,30,56, originType),
                new Frames(465,646,30,56, originType),
                new Frames(529,646,30,56, originType),
            };
            List<Frames> enemyFightingLeft = new List<Frames>
            {
                new Frames(7, 1099,   48, 51, originType),
                new Frames(76, 1099,  43, 51, originType),
                new Frames(138, 1099, 45, 51, originType),
                new Frames(205, 1099, 42, 51, originType),
                new Frames(269, 1099, 42, 51, originType),
                new Frames(332, 1097, 43, 53, originType),
                new Frames(396, 1095, 43, 55, originType),
                new Frames(461, 1097, 42, 53, originType),
                new Frames(525, 1099, 42, 51, originType),
                new Frames(589, 1099, 42, 51, originType),
                new Frames(653, 1099, 42, 51, originType),
                new Frames(717, 1099, 42, 51, originType),
                new Frames(781, 1099, 42, 51, originType),
            };
            List<Frames> enemyFightingRight = new List<Frames>
            {
                new Frames(9, 1227, 48, 51, originType),
                new Frames(73, 1227, 43, 51, originType),
                new Frames(137, 1227, 45, 51, originType),
                new Frames(201, 1227, 42, 51, originType),
                new Frames(265, 1227, 42, 51, originType),
                new Frames(329, 1225, 43, 53, originType),
                new Frames(393, 1223, 43, 55, originType),
                new Frames(457, 1225, 42, 53, originType),
                new Frames(521, 1227, 42, 51, originType),
                new Frames(585, 1227, 42, 51, originType),
                new Frames(649, 1227, 42, 51, originType),
                new Frames(713, 1227, 42, 51, originType),
                new Frames(777, 1227, 42, 51, originType),
            };
            List<Frames> enemyFightingForward = new List<Frames>
            {
                new Frames(16, 1158, 38, 56, originType),
                new Frames(76, 1158, 34, 55, originType),
                new Frames(141, 1158, 36, 55, originType),
                new Frames(209, 1158, 27, 58, originType),
                new Frames(273, 1158, 27, 58, originType),
                new Frames(337, 1158, 27, 56, originType),
                new Frames(465, 1158, 27, 58, originType),
                new Frames(529, 1158, 27, 56, originType),
                new Frames(593, 1158, 27, 58, originType),
                new Frames(657, 1158, 27, 58, originType),
                new Frames(721, 1158, 27, 58, originType),
                new Frames(785, 1158, 27, 58, originType),
            };
            List<Frames> enemyFightingBackward = new List<Frames>
            {
                new Frames(8, 1035, 38, 51, originType),
                new Frames(75, 1038, 34,50, originType),
                new Frames(143, 1037, 36, 51, originType),
                new Frames(212, 1036, 22, 52, originType),
                new Frames(275, 1033, 28, 55, originType),
                new Frames(339, 1030, 26, 58, originType),
                new Frames(403, 1029, 27, 59, originType),
                new Frames(468, 1028, 27, 60, originType),
                new Frames(532, 1031, 28, 57, originType),
                new Frames(595, 1028, 28, 60, originType),
                new Frames(660, 1028, 22, 60, originType),
                new Frames(724, 1028, 22, 60, originType),
                new Frames(788, 1028, 22, 60, originType),
            };
            List<Frames> knightDying = new List<Frames>
            {
                new Frames(17,1294,30,50,  originType),
                new Frames(82,1295,31,49,  originType),
                new Frames(147,1302,30,42, originType),
                new Frames(211,1302,30,37, originType),
                new Frames(275,1313,33,31, originType),
            };
            List<Frames> enemyDying = new List<Frames>
            {
                new Frames(17, 1286, 30, 56, originType),
                new Frames(82, 1288, 31, 54, originType),
                new Frames(147, 1297, 30, 45, originType),
                new Frames(211, 1301, 30, 40, originType),
                new Frames(274, 1308, 32, 33, originType),
            };
            List<Rectangle> rockFrames = new List<Rectangle>
            {
                new Rectangle(72, 40, 93, 50), // top rock on the left
                new Rectangle(256, 32, 147, 58), // top rock on the right
                new Rectangle(331, 130, 70, 56), // bottom rock on the right (right one)
                new Rectangle(270, 148, 61, 38), // bottom rock on the right (left one)
                new Rectangle(43, 110, 131, 76), // bottom rock on the left (left one)
                new Rectangle(174, 142, 53, 44), // bottom rock on the left (right one)
            };
            #endregion
            for (int i = 0; i < 10; i++)
            {
                square.Add(new ItemHolder(pixel, new Vector2(45 * i + 7, GraphicsDevice.Viewport.Height - 40), Color.Black * 0.2f, new Vector2(0, 0), new Vector2(30, 30), SpriteEffects.None));
            }

            inventory = new Inventory(square);
            Texture2D appleTexture = Content.Load<Texture2D>("transparentapple");
            Texture2D potionTexture = Content.Load<Texture2D>("transparentPotion");
            Texture2D redPotion = Content.Load<Texture2D>("redPotion");
            Texture2D rockImage = Content.Load<Texture2D>("finalrock");

            //apple = new Sprite(appleTexture,new Vector2(100,100), Color.White, new Vector2(0,0), new Vector2(1,1), SpriteEffects.None);
            for (int i = 0; i < 4; i++)
            {
                apple.Add(new Food(appleTexture, new Vector2(random.Next(0, GraphicsDevice.Viewport.Width), random.Next(0, GraphicsDevice.Viewport.Height)),
                    Color.White, new Vector2(0, 0), new Vector2(0.75f, 0.75f), SpriteEffects.None));

            }
            for (int i = 0; i < 3; i++)
            {
                healthPotion.Add(new HealthPotion(potionTexture, new Vector2(random.Next(0, GraphicsDevice.Viewport.Width), random.Next(0, GraphicsDevice.Viewport.Height)),
                    Color.White, new Vector2(0, 0), new Vector2(0.75f, 0.75f), SpriteEffects.None));
            }
            for (int i = 0; i < 3; i++)
            {
                speedPotion.Add(new SpeedPotion(redPotion, new Vector2(random.Next(0, GraphicsDevice.Viewport.Width), random.Next(0, GraphicsDevice.Viewport.Height)),
                    Color.White, new Vector2(0, 0), new Vector2(0.75f, 0.75f), SpriteEffects.None));
            }
            for (int i = 0; i < 1; i++)
            {
                rocks.Add(new Sprite(rockImage, new Vector2(300, 200), Color.White, new Vector2(0, 0), new Vector2(0.6f, 0.6f), SpriteEffects.None, rockFrames[newRandom.Next(0, 5)]));
                rocks.Add(new Sprite(rockImage, new Vector2(100, 100), Color.White, new Vector2(0, 0), new Vector2(0.6f, 0.6f), SpriteEffects.None, rockFrames[newRandom.Next(0, 5)]));
            }
            Texture2D closedTreasureImage = Content.Load<Texture2D>("closedtreasure");
            Texture2D openTreasureImage = Content.Load<Texture2D>("opentreasure");
            treasure = new Treasure(closedTreasureImage, openTreasureImage, new Vector2(500, 100), Color.White, new Vector2(0, 0), new Vector2(0.26f, 0.26f), SpriteEffects.None);
            // 400, 150, 300, 300       
            int boundarySize = 300;
            boundary = new Rectangle((int)treasure.Position.X - 100, (int)treasure.Position.Y + 80, boundarySize, boundarySize);

            Texture2D arrowImage = Content.Load<Texture2D>("newCroppedArrow");
            //arrow = new Arrow(arrowImage, new Vector2(enemies.Position.X /2, enemies.Position.Y /2), Color.White, new Vector2(0, 0), new Vector2(0.5f, 0.5f), SpriteEffects.None);

            var knightSpriteSheets = new Dictionary<States, Texture2D>()
            {
                [States.idleRight] = Content.Load<Texture2D>("walkingKnight"),
                [States.idleLeft] = Content.Load<Texture2D>("walkingKnight"),
                [States.idleUp] = Content.Load<Texture2D>("walkingKnight"),
                [States.idleDown] = Content.Load<Texture2D>("walkingKnight"),
                [States.walkingLeft] = Content.Load<Texture2D>("walkingKnight"),
                [States.walkingRight] = Content.Load<Texture2D>("knightimage"),
                [States.walkingUp] = Content.Load<Texture2D>("walkingKnight"),
                [States.walkingDown] = Content.Load<Texture2D>("walkingKnight"),
                [States.fightingLeft] = Content.Load<Texture2D>("onlyfightingknight"),
                [States.fightingRight] = Content.Load<Texture2D>("onlyfightingknight"),
                [States.fightingBackward] = Content.Load<Texture2D>("onlyfightingknight"),
                [States.fightingForward] = Content.Load<Texture2D>("onlyfightingknight"),
                [States.dying] = Content.Load<Texture2D>("fightingknight")
            };
            font = Content.Load<SpriteFont>("SpriteFont");
            Texture2D enemyImage = Content.Load<Texture2D>("enemyImage");

            Texture2D boatImage = Content.Load<Texture2D>("onlyBoat");
            Texture2D paddleImage = Content.Load<Texture2D>("paddle");

            boat = new Boat(boatImage, new Vector2(385, 370), Color.White, Vector2.Zero, new Vector2(0.35f, 0.35f), new Vector2(1, 1), SpriteEffects.FlipHorizontally, 0.05f, 0.03f, rocks);

            paddle = new Paddle(paddleImage, new Vector2(boat.Position.X, boat.Position.Y - 5), Color.White, Vector2.Zero, new Vector2(0.34f, 0.34f), new Vector2(1, 1), SpriteEffects.FlipHorizontally);
            paddle.Origin = new Vector2(paddle.ScaledWidth / 2, paddle.ScaledHeight / 2);
            knight = new Knight(knightSpriteSheets, position: new Vector2(100, 100), tint: Color.White, scale: new Vector2(1, 1), speed: new Vector2(4, 4), font,
                walkingLeftFrames, walkingRightFrames, idleRightFrames, idleLeftFrames, idleUpFrames, idleDownFrames, walkingUpFrames, walkingDownFrames, knightDying,
                fightingLeftFrames, fightingRightFrames, fightingForwardFrames, fightingBackwardFrames, SpriteEffects.None, GraphicsDevice, boat);


            enemies.Add(new ForwardEnemy(enemyImage, arrowImage, new Vector2(treasure.Position.X + 10, treasure.Position.Y + 80), Color.White, new Vector2(0, 0), new Vector2(1, 1), new Vector2(3, 3), SpriteEffects.None,
            enemyWalkingRight, enemyWalkingLeft, enemyWalkingDown, enemyWalkingUp, enemyFightingRight, enemyFightingLeft, enemyFightingForward, enemyFightingBackward,
            enemyIdleRightFrames, enemyIdleLeftFrames, enemyIdleUpFrames, enemyIdleDownFrames, knight, EnemyStates.idleDown, enemyDying, knight.Font));
            enemies.Add(new RightEnemy(enemyImage, arrowImage, new Vector2(treasure.Position.X + 60, treasure.Position.Y + 25), Color.White, new Vector2(0, 0), new Vector2(1, 1), new Vector2(3, 3), SpriteEffects.None,
            enemyWalkingRight, enemyWalkingLeft, enemyWalkingDown, enemyWalkingUp, enemyFightingRight, enemyFightingLeft, enemyFightingForward, enemyFightingBackward,
            enemyIdleRightFrames, enemyIdleLeftFrames, enemyIdleUpFrames, enemyIdleDownFrames, knight, EnemyStates.idleRight, enemyDying, knight.Font));
            enemies.Add(new BackwardEnemy(enemyImage, arrowImage, new Vector2(treasure.Position.X + 10, treasure.Position.Y - 10), Color.White, new Vector2(0, 0), new Vector2(1, 1), new Vector2(3, 3), SpriteEffects.None,
            enemyWalkingRight, enemyWalkingLeft, enemyWalkingDown, enemyWalkingUp, enemyFightingRight, enemyFightingLeft, enemyFightingForward, enemyFightingBackward,
            enemyIdleRightFrames, enemyIdleLeftFrames, enemyIdleUpFrames, enemyIdleDownFrames, knight, EnemyStates.idleUp, enemyDying, knight.Font));
            enemies.Add(new LeftEnemy(enemyImage, arrowImage, new Vector2(treasure.Position.X - 35, treasure.Position.Y + 25), Color.White, new Vector2(0, 0), new Vector2(1, 1), new Vector2(3, 3), SpriteEffects.None,
            enemyWalkingRight, enemyWalkingLeft, enemyWalkingDown, enemyWalkingUp, enemyFightingRight, enemyFightingLeft, enemyFightingForward, enemyFightingBackward,
            enemyIdleRightFrames, enemyIdleLeftFrames, enemyIdleUpFrames, enemyIdleDownFrames, knight, EnemyStates.idleLeft, enemyDying, knight.Font));


            Texture2D board = Content.Load<Texture2D>("board");

            popUp = new Popup(board, Color.White, new Vector2(0, 0), new Vector2(0.30f, 0.30f), font);

            // TODO: use this.Content to load your game content here

            //create docking point where the boat can only go and the knight can get on and off the boat
        }

        public void PartialFloodFill(int x, int y, Texture2D texture)
        {
            TwoDArrayIndex start = new TwoDArrayIndex(x, y);
            if (InBoundary(start) == false)
            {
                return;
            }
            //Add two variables, Chance and Decay
            double Chance = 1;
            double Decay = 0.9f;

            //Step 1: add starting point to list
            List<TwoDArrayIndex> filler = new List<TwoDArrayIndex>();
            filler.Add(start);

            //Left: (y, x-1)
            //Right: (y, x + 1)
            //Up: (y-1, x)
            //Down: (y+1,x)

            //Step 2: Loop until list is empty
            while (filler.Count > 0)
            {
                //Step 3: Inside loop, store first element, then remove it from list
                TwoDArrayIndex current = filler[0];
                filler.Remove(current);

                if (Grid[current.Y, current.X].Image == null)
                {
                    Grid[current.Y, current.X].Image = texture;
                    filledPixels++;

                    Spots.Remove(current);
                }
                else
                {
                    continue;
                }

                double randomVal = random.NextDouble() * 2;

                if (Chance < randomVal)
                {
                    continue;
                }
                Chance = Chance * Decay;


                //Step 4: Find Neighbors of current element that was removed, and add them to list
                TwoDArrayIndex Left = new TwoDArrayIndex(current.X - 1, current.Y);
                TwoDArrayIndex Right = new TwoDArrayIndex(current.X + 1, current.Y);
                TwoDArrayIndex Up = new TwoDArrayIndex(current.X, current.Y - 1);
                TwoDArrayIndex Down = new TwoDArrayIndex(current.X, current.Y + 1);

                if (InBoundary(Left))
                {
                    filler.Add(Left);
                }
                if (InBoundary(Right))
                {
                    filler.Add(Right);
                }
                if (InBoundary(Up))
                {
                    filler.Add(Up);
                }
                if (InBoundary(Down))
                {
                    filler.Add(Down);
                }
            }
        }
        bool InBoundary(TwoDArrayIndex index)
        {
            if (index.X >= 0 && index.X < Grid.GetLength(1) && index.Y >= 0 && index.Y < Grid.GetLength(0))
            {
                return true;
            }
            return false;
        }
        void fillingSpots()
        {
            Texture2D lastKnownTexture = Grid[0, 0].Image;
            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    if (Grid[y, x].Image == null)
                    {
                        Grid[y, x].Image = lastKnownTexture;
                    }
                    else
                    {
                        lastKnownTexture = Grid[y, x].Image;
                    }
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
            //popUp.Update(gameTime);

            inventory.update(knight);

            string txt = "";
            string secondText = "";
            string thirdText = "";

            for (int i = 0; i < apple.Count; i++)
            {
                if (knight.HitBox.Intersects(apple[i].HitBox))
                {
                    txt = "Item Type: food";
                    secondText = "Level : 3";
                    thirdText = "health points: 5";
                }
                if (knight.HitBox.Intersects(apple[i].HitBox) && ks.IsKeyDown(Keys.E))
                {
                    inventory.AddItem(apple[i]);
                    apple.Remove(apple[i]);
                }
            }
            for (int i = 0; i < healthPotion.Count; i++)
            {
                if (knight.HitBox.Intersects(healthPotion[i].HitBox))
                {
                    txt = "increases health";
                    secondText = "Level : 5";
                    thirdText = "health points: +9";
                }
                if (knight.HitBox.Intersects(healthPotion[i].HitBox) && ks.IsKeyDown(Keys.E))
                {
                    inventory.AddItem(healthPotion[i]);
                    healthPotion.Remove(healthPotion[i]);
                }
            }
            for (int i = 0; i < speedPotion.Count; i++)
            {
                if (knight.HitBox.Intersects(speedPotion[i].HitBox))
                {
                    txt = "increases speed";
                    secondText = "Level : 4";
                    thirdText = "";
                }
                if (knight.HitBox.Intersects(speedPotion[i].HitBox) && ks.IsKeyDown(Keys.E))
                {
                    inventory.AddItem(speedPotion[i]);
                    speedPotion.Remove(speedPotion[i]);
                }
            }

            if (knight.HitBox.Intersects(treasure.HitBox))
            {
                treasure.OpenOrClose(TreasureFrames.open);
            }
            else
            {
                treasure.OpenOrClose(TreasureFrames.closed);
            }
            if (txt != "")
            {
                popUp.displayPopup(txt, secondText, thirdText);
            }
            else
            {
                popUp.ClearPopup();
            }
            knight.Update(gameTime, enemies, edges, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), paddle);
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime, boundary);
            }
            //character.Update(ks, ms);
            boat.Move(edges, knight, paddle);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    Grid[y, x].Draw(spriteBatch);
                }
            }
            for (int i = 0; i < apple.Count; i++)
            {
                //spriteBatch.Draw(pixel, apple[i].Position, null, Color.Black, 0f, Vector2.Zero, new Vector2(30,30),SpriteEffects.None, 0);
                apple[i].Draw(spriteBatch);
            }
            for (int i = 0; i < healthPotion.Count; i++)
            {
                //spriteBatch.Draw(pixel, potion[i].Position, null, Color.Black, 0f, Vector2.Zero, new Vector2(30, 30), SpriteEffects.None, 0);
                healthPotion[i].Draw(spriteBatch);
            }
            for (int i = 0; i < speedPotion.Count; i++)
            {
                //spriteBatch.Draw(pixel, speedPotion[i].Position, null, Color.Black, 0f, Vector2.Zero, new Vector2(30, 30), SpriteEffects.None, 0);
                speedPotion[i].Draw(spriteBatch);
            }
            for (int i = 0; i < square.Count; i++)
            {
                square[i].Draw(spriteBatch);
            }
            for (int i = 0; i < rocks.Count; i++)
            {
                //spriteBatch.Draw(pixel, new Vector2(rocks[i].HitBox.X, rocks[i].HitBox.Y), null, Color.Black, 0f, Vector2.Zero, new Vector2(rocks[i].HitBox.Width, rocks[i].HitBox.Height), SpriteEffects.None, 0);
                rocks[i].Draw(spriteBatch);
            }
            treasure.Draw(spriteBatch);


            //spriteBatch.Draw(pixel, new Vector2(knight.HitBox.X, knight.HitBox.Y), null, Color.Black, 0f, Vector2.Zero, new Vector2(knight.HitBox.Width, knight.HitBox.Height), SpriteEffects.None, 0);

            for (int i = 0; i < enemies.Count; i++)
            {
                //spriteBatch.Draw(pixel, new Vector2(enemies[i].HitBox.X, enemies[i].HitBox.Y), null, Color.Black, 0f, Vector2.Zero, new Vector2(enemies[i].HitBox.Width, enemies[i].HitBox.Height), SpriteEffects.None, 0);
                enemies[i].Draw(spriteBatch);
            }

            spriteBatch.Draw(pixel, new Vector2(boat.rightRectangle.X, boat.rightRectangle.Y), null, Color.Black, 0f, Vector2.Zero, new Vector2(boat.rightRectangle.Width, boat.rightRectangle.Height), SpriteEffects.None, 0);
            if (popUp.Visible == true)
            {
                popUp.Draw(spriteBatch, graphics.GraphicsDevice);
            }
            boat.Draw(spriteBatch);

            knight.Draw(spriteBatch);
            paddle.DrawWithTint(spriteBatch, boat.Tint);
            //foreach (TileFromSprite edge in edges)
            //{
            //    spriteBatch.Draw(pixel, new Rectangle(edge.HitBox.X, edge.HitBox.Y, 20, 20), Color.Red * 0.7f);
            //}

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
