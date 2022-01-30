using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace MapEditor
{

    public partial class Form1 : Form
    {
        Bitmap canvas;
        Graphics gfx;
        Image selectedImage;

        int totalWidth = 600;
        int totalHeight = 400;
        int tileSize = 20;

        Terrain[,] Grid;
        List<Decor> decor = new List<Decor>();

        TilePictureBox tilePictureBox;
        Point mouseLocation = new Point(0, 0);

        TileTypes selectedTileType;
        DecorTypes selectedDecorType;
        Point boatPosition;

        Dictionary<string, TileTypes> tileTypeMapping = new Dictionary<string, TileTypes>()
        {
            ["WaterTile"] = TileTypes.Water,
            ["GrassTile"] = TileTypes.Grass,
            ["StoneTile"] = TileTypes.Stone,
            ["SandTile"] = TileTypes.Sand,
        };

        Dictionary<string, DecorTypes> decorTypeMapping = new Dictionary<string, DecorTypes>()
        {
            ["Rock1"] = DecorTypes.Rock1,
            ["Rock2"] = DecorTypes.Rock2,
            ["Rock3"] = DecorTypes.Rock3,
            ["redSquare"] = DecorTypes.redSquare,
        };
        Dictionary<TileTypes, Image> TileToImage;
        Dictionary<DecorTypes, Image> DecorTileToImage;

        public Form1()
        {
            InitializeComponent(); // DO NOT MOVE!!

            Image grassImage = Properties.Resources.grassTile;
            Image waterImage = Properties.Resources.waterTile;
            Image stoneImage = Properties.Resources.stoneTile21;
            Image sandImage = Properties.Resources.sandTile;
            Image rock1Image = Properties.Resources.rock1;
            Image rock2Image = Properties.Resources.rock2;
            Image rock3Image = Properties.Resources.rock3;
            Image redSquareImage = Properties.Resources.redSquare;
            Image black = Properties.Resources.black;

            TileToImage = new Dictionary<TileTypes, Image>()
            {
                [TileTypes.None] = new Bitmap(black, new Size(tileSize, tileSize)),
                [TileTypes.Grass] = new Bitmap(grassImage, new Size(tileSize, tileSize)),
                [TileTypes.Water] = new Bitmap(waterImage, new Size(tileSize, tileSize)),
                [TileTypes.Sand] = new Bitmap(sandImage, new Size(tileSize, tileSize)),
                [TileTypes.Stone] = new Bitmap(stoneImage, new Size(tileSize, tileSize)),
            };

            DecorTileToImage = new Dictionary<DecorTypes, Image>()
            {
                [DecorTypes.None] = new Bitmap(black, new Size(tileSize, tileSize)),
                [DecorTypes.Rock1] = new Bitmap(rock1Image, new Size((int)(rock1.Width * 0.3), (int)(rock1.Height * 0.3))),
                [DecorTypes.Rock2] = new Bitmap(rock2Image, new Size((int)(rock2.Width * 0.3), (int)(rock2.Height * 0.3))),
                [DecorTypes.Rock3] = new Bitmap(rock3Image, new Size((int)(rock3.Width * 0.3), (int)(rock3.Height * 0.3))),
                [DecorTypes.redSquare] = new Bitmap(redSquareImage, new Size((int)(redSquare.Width * 0.3), (int)(redSquare.Height * 0.3)))
            };

            totalWidth = map.Width;
            totalHeight = map.Height;

            widthBox.Value = totalWidth;
            heightBox.Value = totalHeight;
            tileSizeBox.Value = tileSize;

            
            Grid = new Terrain[totalHeight / tileSize, totalWidth / tileSize];

            this.KeyPreview = true;

            this.KeyPress += Form1_KeyPress;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Toggle(e.KeyChar);
            FillToggle(e.KeyChar);
        }


        public void intializeGrid()
        {
            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    Grid[y, x] = new Terrain(x, y, TileTypes.None)
                    {
                        X = x * tileSize,
                        Y = y * tileSize
                    };
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            widthBox.Value = 600;

            canvas = new Bitmap(map.Width, map.Height);
            gfx = Graphics.FromImage(canvas);
            
            for (int i = 0; i < TilesPanel.Controls.Count; i++)
            {
                TilesPanel.Controls[i].MouseClick += Tile_Click;
            }

            intializeGrid();
        }

        private void Tile_Click(object sender, MouseEventArgs e)
        {
            tilePictureBox = (TilePictureBox)sender;
            selectedImage = tilePictureBox.Image;
            
            if(tilePictureBox.ImageType == ImageType.Tile)
            { 
                selectedTileType = tileTypeMapping[(string)((PictureBox)sender).Tag];
            }
            if(tilePictureBox.ImageType == ImageType.Decor)
            {
                selectedDecorType = decorTypeMapping[(string)((PictureBox)sender).Tag];
            }
        }
        private bool isNewSizeAvailable(int width, int height, int tileSize)
        {
            if (height % tileSize != 0 || width % tileSize != 0)
            {
                MessageBox.Show("Map size is not divisible by tilesize");
                return false;
            }

            gfx.Clear(map.BackColor);

            canvas = new Bitmap(width, height);
            gfx = Graphics.FromImage(canvas);
            map.Width = width;
            map.Height = height;

            int numberOfVerticalLines = map.Width / tileSize;
            int numberOfHorizontalLines = map.Height / tileSize;
            for (int x = 0; x < numberOfVerticalLines; x++)
            {
                gfx.DrawLine(Pens.White, new Point(x * tileSize + tileSize, 0), new Point(x * tileSize + tileSize, map.Height));
            }

            for (int y = 0; y < numberOfHorizontalLines; y++)
            {
                gfx.DrawLine(Pens.White, new Point(0, y * tileSize + tileSize), new Point(map.Width, y * tileSize + tileSize));
            }
            map.Image = canvas;

            return true;
        }

        public bool checkValidtile(int x, int y)
        {
            if (x >= 0 && x < Grid.GetLength(1) && y >= 0 && y < Grid.GetLength(0) && Grid[y, x].Type != selectedTileType)
            {
                return true;
            }
            return false;
        }

        public void partialFill(int x, int y)
        {
            List<Point> fillBoxes = new List<Point>();
            fillBoxes.Add(new Point(x, y));
            Grid[y, x].Type = selectedTileType;

            while (fillBoxes.Count > 0)
            {
                Point current = fillBoxes[0];
                fillBoxes.Remove(current);
                if (checkValidtile(current.X + 1, current.Y))
                {
                    fillBoxes.Add(new Point(current.X + 1, current.Y));
                    Grid[current.Y, current.X + 1].Type = selectedTileType;
                }
                if (checkValidtile(current.X - 1, current.Y))
                {
                    fillBoxes.Add(new Point(current.X - 1, current.Y));
                    Grid[current.Y, current.X - 1].Type = selectedTileType;
                }
                if (checkValidtile(current.X, current.Y + 1))
                {
                    fillBoxes.Add(new Point(current.X, current.Y + 1));
                    Grid[current.Y + 1, current.X].Type = selectedTileType;
                }
                if (checkValidtile(current.X, current.Y - 1))
                {
                    fillBoxes.Add(new Point(current.X, current.Y - 1));
                    Grid[current.Y - 1, current.X].Type = selectedTileType;
                }
            }
            //right, left, up, down

        }

        private void Changes_Click(object sender, EventArgs e)
        {
            if (isNewSizeAvailable((int)widthBox.Value, (int)heightBox.Value, (int)tileSizeBox.Value))
            {
                ControlPanel.Location = new Point(map.Right, 0);
                TilesPanel.Location = new Point(map.Right, TilesPanel.Location.Y);
                savingPanel.Location = new Point(ControlPanel.Right, 0);


                totalWidth = map.Width;
                totalHeight = map.Height;

                Grid = new Terrain[totalHeight / tileSize, totalWidth / tileSize];

                intializeGrid();
            }
            else
            {
                return;
            }
        }


        bool shouldFillIn = false;
        bool floodFillIn = false;
        private void Toggle(char keyPress)
        {
            if (keyPress == 'h' || keyPress == 'H')
            {
                if (shouldFillIn == true)
                {
                    ToggleLabel.Text = "Toggle: Off (key:H)";
                    shouldFillIn = false;
                }
                else if (shouldFillIn == false)
                {
                    ToggleLabel.Text = "Toggle: On (key:H)";
                    shouldFillIn = true;
                }
            }
        }

        private void FillToggle(char keyPress)
        {
            if (keyPress == 'f' || keyPress == 'F')
            {
                if (floodFillIn == true)
                {
                    FillToggleLabel.Text = "Fill: Off (key:F)";
                    floodFillIn = false;
                }
                else if (floodFillIn == false)
                {
                    FillToggleLabel.Text = "Fill: On (key:F)";
                    floodFillIn = true;
                }
            }

        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (namingFileTextBox.Text == "")
            {
                MessageBox.Show("Please name your file!");
                return;
            }

            List<Terrain> tTiles = new List<Terrain>();

            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    if (Grid[y, x] != null)
                    {
                        tTiles.Add(Grid[y, x]);
                    }
                }
            }


            MapData data = new MapData(tTiles, decor, boatPosition);

            MessageBox.Show("Saved!");

            string textFormat = JsonConvert.SerializeObject(data);

            File.WriteAllText($"{namingFileTextBox.Text}.json", textFormat);
        }

        private void map_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedTileType == TileTypes.None) return;

            int gridLocationX = e.Location.X / tileSize;
            int gridLocationY = e.Location.Y / tileSize;

            if (floodFillIn)
            {
                partialFill(gridLocationX, gridLocationY);
            }
            else if (shouldFillIn)
            {
                Grid[gridLocationY, gridLocationX].Type = selectedTileType;
            }
        }

        private void mapTimer_Tick(object sender, EventArgs e)
        {
            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    Image tileImage = TileToImage[Grid[y, x].Type];

                    gfx.DrawImage(tileImage, new Point(Grid[y, x].X, Grid[y, x].Y));

                }
            }
            
            for (int i = 0; i < decor.Count; i++)
            {
                Image decorImage = DecorTileToImage[decor[i].Type];

                gfx.DrawImage(decorImage, new Point(decor[i].X, decor[i].Y));
            }
           

            map.Image = canvas;
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectedDecorType == DecorTypes.None) return;
            
            if (tilePictureBox.ImageType == ImageType.Decor)
            {
                if(selectedDecorType == DecorTypes.redSquare)
                { 
                    boatPosition = new Point(e.Location.X, e.Location.Y);
                }
                decor.Add(new Decor(e.Location.X, e.Location.Y, selectedDecorType));
            }
        }

        private void TilesPanel_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
