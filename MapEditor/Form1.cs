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

    public enum TileTypes
    {
        waterTile,
        grassTile,
        stoneTile,
    };
    
    public partial class Form1 : Form
    {
        class Tile
        {
            public int X { get; set; }
            public int Y { get; set; }
            public TileTypes Type { get; set; }

            public Tile(int x, int y, TileTypes type)
            {
                X = x;
                Y = y;
                Type = type;
            }
        }
        Bitmap canvas;
        Graphics gfx;
        Image selectedImage;

        int totalWidth = 600;
        int totalHeight = 400;
        int tileSize = 20;

        PictureBox[,] Grid;

        Image grassImage;
        Image waterImage;
        Image stoneImage;

        TileTypes selectedTileType;

        Dictionary<string, TileTypes> tileTypeMapping = new Dictionary<string, TileTypes>()
        {
            ["WaterTile"] = TileTypes.waterTile,
            ["GrassTile"] = TileTypes.grassTile,
            ["StoneTile"] = TileTypes.stoneTile,
        };
        public Form1()
        {
            grassImage = Properties.Resources.grassTile;
            waterImage = Properties.Resources.waterTile;
            stoneImage = Properties.Resources.stoneTile2;
            InitializeComponent();

            totalWidth = map.Width;
            totalHeight = map.Height;

            widthBox.Value = totalWidth;
            heightBox.Value = totalHeight;
            tileSizeBox.Value = tileSize;

            Grid = new PictureBox[totalHeight / tileSize, totalWidth / tileSize];
           

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
            map.Controls.Clear();
            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    Grid[y, x] = new PictureBox();
                    Grid[y, x].Image = null;
                    Grid[y, x].BackColor = Color.Blue;
                    Grid[y, x].Location = new Point(x * tileSize, y * tileSize);
                    Grid[y, x].Size = new Size(tileSize, tileSize);
                    Grid[y, x].SizeMode = PictureBoxSizeMode.StretchImage;
                    Grid[y, x].MouseEnter += Cell_Click;

                    map.Controls.Add(Grid[y, x]);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            widthBox.Value = 600;

            canvas = new Bitmap(map.Width, map.Height);
            gfx = Graphics.FromImage(canvas);


            //Get all the controls inside the TilesPanel
            //subscribe their clickevent to tile_click

            for (int i = 0; i < TilesPanel.Controls.Count; i++)
            {
                TilesPanel.Controls[i].Click += Tile_Click;
            }
            intializeGrid();
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            selectedImage = ((PictureBox)sender).Image;
            selectedTileType = tileTypeMapping[(string)((PictureBox)sender).Tag];
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            if (shouldFillIn == true)
            {
                ((PictureBox)sender).Image = selectedImage;
                ((PictureBox)sender).Tag = selectedTileType;
            }
            if(floodFillIn == true)
            {
                partialFill(pictureBox.Location.X / tileSize, pictureBox.Location.Y / tileSize);
                floodFillIn = false;
            }
        }
        private bool isNewSizeAvailable(int width, int height, int tileSize)
        {
            //Make a variable representing tile size
            //Divide the width by tilesize and draw that many vertical lines
            //Divide the height by tilesize and draw that many horizontal lines
            //Make sure that the width and height of the picturebox are divisible by the tilesize

            //Make sure width and height are divisible
            //if not return

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
            if (x >= 0 && x < Grid.GetLength(1) && y >= 0 && y < Grid.GetLength(0) && Grid[y, x].Image != selectedImage)
            {
                return true;
            }
            return false;
         }
        public void partialFill(int x, int y)
        {
            List<Point> fillBoxes = new List<Point>();
            fillBoxes.Add(new Point(x, y));
            Grid[y, x].Image = selectedImage;
            Grid[y, x].Tag = selectedTileType;

            while (fillBoxes.Count > 0)
            {
                Point current = fillBoxes[0];
                fillBoxes.Remove(current);
                if (checkValidtile(current.X + 1, current.Y) && (Grid[current.Y, current.X + 1].Tag == null || (TileTypes)Grid[current.Y, current.X + 1].Tag != selectedTileType))
                { 
                    fillBoxes.Add(new Point(current.X + 1, current.Y));
                    Grid[current.Y, current.X + 1].Image = selectedImage;
                    Grid[current.Y, current.X + 1].Tag = selectedTileType;
                }
                if (checkValidtile(current.X - 1, current.Y) && (Grid[current.Y, current.X - 1].Tag == null || (TileTypes)Grid[current.Y, current.X - 1].Tag != selectedTileType))
                {
                    fillBoxes.Add(new Point(current.X - 1, current.Y));
                    Grid[current.Y, current.X - 1].Image = selectedImage;
                    Grid[current.Y, current.X - 1].Tag = selectedTileType;
                }
                if (checkValidtile(current.X, current.Y + 1) && (Grid[current.Y + 1, current.X].Tag == null || (TileTypes)Grid[current.Y + 1, current.X].Tag != selectedTileType))
                {
                    fillBoxes.Add(new Point(current.X, current.Y + 1));
                    Grid[current.Y + 1, current.X].Image = selectedImage;
                    Grid[current.Y + 1, current.X].Tag = selectedTileType;
                }
                if (checkValidtile(current.X, current.Y - 1) && (Grid[current.Y - 1, current.X].Tag == null || (TileTypes)Grid[current.Y - 1, current.X].Tag != selectedTileType))
                {
                    fillBoxes.Add(new Point(current.X, current.Y - 1 ));
                    Grid[current.Y - 1, current.X].Image = selectedImage;
                    Grid[current.Y - 1, current.X].Tag = selectedTileType;
                }
            }

            
            //right, left, up, down

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Changes_Click(object sender, EventArgs e)
        {
            if (isNewSizeAvailable((int)widthBox.Value, (int)heightBox.Value, (int)tileSizeBox.Value))
            {
                ControlPanel.Location = new Point(map.Right, 0);
                TilesPanel.Location = new Point(map.Right, TilesPanel.Location.Y);

                totalWidth = map.Width;
                totalHeight = map.Height;

                Grid = new PictureBox[totalHeight / tileSize, totalWidth / tileSize];

                intializeGrid();
            }
            else
            {
                return;
            }
        }

        private void waterTile_Click(object sender, EventArgs e)
        {

        }
        bool shouldFillIn = false;
        bool floodFillIn = false;
        private void Toggle(char keyPress)
        {
            if (keyPress == 'h')
            {
                if (shouldFillIn == true)
                {
                    ToggleLabel.Text = "Toggle: Off";
                    shouldFillIn = false;
                }
                else if (shouldFillIn == false)
                {
                    ToggleLabel.Text = "Toggle: On";
                    shouldFillIn = true;
                }
            }
        }

        private void FillToggle(char keyPress)
        {
            if(keyPress == 'f')
            {
                if (floodFillIn == true)
                {
                    FillToggleLabel.Text = "Fill: Off";
                    floodFillIn = false;
                }
                else if (floodFillIn == false)
                {
                    FillToggleLabel.Text = "Fill: On";
                    floodFillIn = true;
                }
            }
           
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            List<Tile> tiles = new List<Tile>();

            for(int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    tiles.Add(new Tile(x, y, (TileTypes)Grid[y, x].Tag));
                }
            }

            MessageBox.Show("Saved!");

            string textFormat = JsonConvert.SerializeObject(tiles);

            File.WriteAllText($"{namingFileTextBox.Text}.json", textFormat);
        }
    }
}
