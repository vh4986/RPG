namespace MapEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.widthBox = new System.Windows.Forms.NumericUpDown();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.heightBox = new System.Windows.Forms.NumericUpDown();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.tileSizeBox = new System.Windows.Forms.NumericUpDown();
            this.TileSize = new System.Windows.Forms.Label();
            this.Changes = new System.Windows.Forms.Button();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.TilesPanel = new System.Windows.Forms.Panel();
            this.redSquare = new MapEditor.TilePictureBox();
            this.rock2 = new MapEditor.TilePictureBox();
            this.rock3 = new MapEditor.TilePictureBox();
            this.sandTile = new MapEditor.TilePictureBox();
            this.grassTile = new MapEditor.TilePictureBox();
            this.stoneTile = new MapEditor.TilePictureBox();
            this.rock1 = new MapEditor.TilePictureBox();
            this.waterTile = new MapEditor.TilePictureBox();
            this.ToggleLabel = new System.Windows.Forms.Label();
            this.FillToggleLabel = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.namingFileTextBox = new System.Windows.Forms.TextBox();
            this.NameOfFile = new System.Windows.Forms.Label();
            this.savingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.mapTimer = new System.Windows.Forms.Timer(this.components);
            this.map = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileSizeBox)).BeginInit();
            this.ControlPanel.SuspendLayout();
            this.TilesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.redSquare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sandTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grassTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stoneTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waterTile)).BeginInit();
            this.savingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.map)).BeginInit();
            this.SuspendLayout();
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(64, 7);
            this.widthBox.Maximum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(120, 20);
            this.widthBox.TabIndex = 2;
            this.widthBox.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // WidthLabel
            // 
            this.WidthLabel.AutoSize = true;
            this.WidthLabel.Location = new System.Drawing.Point(14, 9);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(35, 13);
            this.WidthLabel.TabIndex = 3;
            this.WidthLabel.Text = "Width";
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(64, 55);
            this.heightBox.Maximum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(120, 20);
            this.heightBox.TabIndex = 4;
            this.heightBox.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Location = new System.Drawing.Point(14, 57);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(38, 13);
            this.HeightLabel.TabIndex = 5;
            this.HeightLabel.Text = "Height";
            // 
            // tileSizeBox
            // 
            this.tileSizeBox.Location = new System.Drawing.Point(64, 102);
            this.tileSizeBox.Name = "tileSizeBox";
            this.tileSizeBox.Size = new System.Drawing.Size(120, 20);
            this.tileSizeBox.TabIndex = 6;
            this.tileSizeBox.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // TileSize
            // 
            this.TileSize.AutoSize = true;
            this.TileSize.Location = new System.Drawing.Point(14, 104);
            this.TileSize.Name = "TileSize";
            this.TileSize.Size = new System.Drawing.Size(47, 13);
            this.TileSize.TabIndex = 7;
            this.TileSize.Text = "Tile Size";
            // 
            // Changes
            // 
            this.Changes.Location = new System.Drawing.Point(42, 149);
            this.Changes.Name = "Changes";
            this.Changes.Size = new System.Drawing.Size(129, 23);
            this.Changes.TabIndex = 8;
            this.Changes.Text = "Apply Changes";
            this.Changes.UseVisualStyleBackColor = true;
            this.Changes.Click += new System.EventHandler(this.Changes_Click);
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.WidthLabel);
            this.ControlPanel.Controls.Add(this.Changes);
            this.ControlPanel.Controls.Add(this.widthBox);
            this.ControlPanel.Controls.Add(this.TileSize);
            this.ControlPanel.Controls.Add(this.heightBox);
            this.ControlPanel.Controls.Add(this.tileSizeBox);
            this.ControlPanel.Controls.Add(this.HeightLabel);
            this.ControlPanel.Location = new System.Drawing.Point(605, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(200, 197);
            this.ControlPanel.TabIndex = 9;
            // 
            // TilesPanel
            // 
            this.TilesPanel.Controls.Add(this.redSquare);
            this.TilesPanel.Controls.Add(this.rock2);
            this.TilesPanel.Controls.Add(this.rock3);
            this.TilesPanel.Controls.Add(this.sandTile);
            this.TilesPanel.Controls.Add(this.grassTile);
            this.TilesPanel.Controls.Add(this.stoneTile);
            this.TilesPanel.Controls.Add(this.rock1);
            this.TilesPanel.Controls.Add(this.waterTile);
            this.TilesPanel.Location = new System.Drawing.Point(606, 263);
            this.TilesPanel.Name = "TilesPanel";
            this.TilesPanel.Size = new System.Drawing.Size(405, 242);
            this.TilesPanel.TabIndex = 10;
            // 
            // redSquare
            // 
            this.redSquare.Image = global::MapEditor.Properties.Resources.redSquare;
            this.redSquare.ImageType = MapEditor.ImageType.Decor;
            this.redSquare.Location = new System.Drawing.Point(334, 8);
            this.redSquare.Name = "redSquare";
            this.redSquare.Size = new System.Drawing.Size(53, 50);
            this.redSquare.TabIndex = 22;
            this.redSquare.TabStop = false;
            this.redSquare.Tag = "redSquare";
            // 
            // rock2
            // 
            this.rock2.Image = global::MapEditor.Properties.Resources.rock2;
            this.rock2.ImageType = MapEditor.ImageType.Decor;
            this.rock2.Location = new System.Drawing.Point(209, 64);
            this.rock2.Name = "rock2";
            this.rock2.Size = new System.Drawing.Size(145, 58);
            this.rock2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rock2.TabIndex = 19;
            this.rock2.TabStop = false;
            this.rock2.Tag = "Rock2";
            // 
            // rock3
            // 
            this.rock3.Image = global::MapEditor.Properties.Resources.rock3;
            this.rock3.ImageType = MapEditor.ImageType.Decor;
            this.rock3.Location = new System.Drawing.Point(209, 128);
            this.rock3.Name = "rock3";
            this.rock3.Size = new System.Drawing.Size(188, 76);
            this.rock3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rock3.TabIndex = 20;
            this.rock3.TabStop = false;
            this.rock3.Tag = "Rock3";
            // 
            // sandTile
            // 
            this.sandTile.Image = global::MapEditor.Properties.Resources.SandTileV2;
            this.sandTile.ImageType = MapEditor.ImageType.Tile;
            this.sandTile.Location = new System.Drawing.Point(109, 122);
            this.sandTile.Name = "sandTile";
            this.sandTile.Size = new System.Drawing.Size(87, 82);
            this.sandTile.TabIndex = 21;
            this.sandTile.TabStop = false;
            this.sandTile.Tag = "SandTile";
            // 
            // grassTile
            // 
            this.grassTile.Image = global::MapEditor.Properties.Resources.grassTile;
            this.grassTile.ImageType = MapEditor.ImageType.Tile;
            this.grassTile.Location = new System.Drawing.Point(5, 122);
            this.grassTile.Name = "grassTile";
            this.grassTile.Size = new System.Drawing.Size(86, 82);
            this.grassTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.grassTile.TabIndex = 20;
            this.grassTile.TabStop = false;
            this.grassTile.Tag = "GrassTile";
            // 
            // stoneTile
            // 
            this.stoneTile.Image = global::MapEditor.Properties.Resources.stoneTile21;
            this.stoneTile.ImageType = MapEditor.ImageType.Tile;
            this.stoneTile.Location = new System.Drawing.Point(109, 8);
            this.stoneTile.Name = "stoneTile";
            this.stoneTile.Size = new System.Drawing.Size(87, 82);
            this.stoneTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stoneTile.TabIndex = 19;
            this.stoneTile.TabStop = false;
            this.stoneTile.Tag = "StoneTile";
            // 
            // rock1
            // 
            this.rock1.Image = global::MapEditor.Properties.Resources.rock1;
            this.rock1.ImageType = MapEditor.ImageType.Decor;
            this.rock1.Location = new System.Drawing.Point(209, 8);
            this.rock1.Name = "rock1";
            this.rock1.Size = new System.Drawing.Size(93, 50);
            this.rock1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rock1.TabIndex = 18;
            this.rock1.TabStop = false;
            this.rock1.Tag = "Rock1";
            // 
            // waterTile
            // 
            this.waterTile.Image = global::MapEditor.Properties.Resources.waterTile;
            this.waterTile.ImageType = MapEditor.ImageType.Tile;
            this.waterTile.Location = new System.Drawing.Point(5, 8);
            this.waterTile.Name = "waterTile";
            this.waterTile.Size = new System.Drawing.Size(87, 82);
            this.waterTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.waterTile.TabIndex = 18;
            this.waterTile.TabStop = false;
            this.waterTile.Tag = "WaterTile";
            // 
            // ToggleLabel
            // 
            this.ToggleLabel.AutoSize = true;
            this.ToggleLabel.Location = new System.Drawing.Point(19, 12);
            this.ToggleLabel.Name = "ToggleLabel";
            this.ToggleLabel.Size = new System.Drawing.Size(100, 13);
            this.ToggleLabel.TabIndex = 11;
            this.ToggleLabel.Text = "Toggle: Off (key: H)";
            // 
            // FillToggleLabel
            // 
            this.FillToggleLabel.AutoSize = true;
            this.FillToggleLabel.Location = new System.Drawing.Point(19, 57);
            this.FillToggleLabel.Name = "FillToggleLabel";
            this.FillToggleLabel.Size = new System.Drawing.Size(77, 13);
            this.FillToggleLabel.TabIndex = 12;
            this.FillToggleLabel.Text = "Fill: Off (key: F)";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(70, 149);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 13;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // namingFileTextBox
            // 
            this.namingFileTextBox.Location = new System.Drawing.Point(93, 101);
            this.namingFileTextBox.Name = "namingFileTextBox";
            this.namingFileTextBox.Size = new System.Drawing.Size(100, 20);
            this.namingFileTextBox.TabIndex = 14;
            // 
            // NameOfFile
            // 
            this.NameOfFile.AutoSize = true;
            this.NameOfFile.Location = new System.Drawing.Point(19, 104);
            this.NameOfFile.Name = "NameOfFile";
            this.NameOfFile.Size = new System.Drawing.Size(71, 13);
            this.NameOfFile.TabIndex = 15;
            this.NameOfFile.Text = "Name Of File:";
            // 
            // savingPanel
            // 
            this.savingPanel.Controls.Add(this.namingFileTextBox);
            this.savingPanel.Controls.Add(this.NameOfFile);
            this.savingPanel.Controls.Add(this.ToggleLabel);
            this.savingPanel.Controls.Add(this.FillToggleLabel);
            this.savingPanel.Controls.Add(this.Save);
            this.savingPanel.Location = new System.Drawing.Point(811, 0);
            this.savingPanel.Name = "savingPanel";
            this.savingPanel.Size = new System.Drawing.Size(200, 197);
            this.savingPanel.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(619, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "*When filling the map, make sure to press F again before exiting the area you fil" +
    "led*";
            // 
            // mapTimer
            // 
            this.mapTimer.Enabled = true;
            this.mapTimer.Interval = 50;
            this.mapTimer.Tick += new System.EventHandler(this.mapTimer_Tick);
            // 
            // map
            // 
            this.map.BackColor = System.Drawing.SystemColors.Desktop;
            this.map.Location = new System.Drawing.Point(-1, 0);
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(600, 400);
            this.map.TabIndex = 0;
            this.map.TabStop = false;
            this.map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.map_MouseClick);
            this.map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.map_MouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(610, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(411, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "*Put the red square where the grass and water meet. This will be your boat\'s loca" +
    "tion.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 517);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.savingPanel);
            this.Controls.Add(this.TilesPanel);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.map);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileSizeBox)).EndInit();
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.TilesPanel.ResumeLayout(false);
            this.TilesPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.redSquare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sandTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grassTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stoneTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rock1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waterTile)).EndInit();
            this.savingPanel.ResumeLayout(false);
            this.savingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.map)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox map;
        private System.Windows.Forms.NumericUpDown widthBox;
        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.NumericUpDown heightBox;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.NumericUpDown tileSizeBox;
        private System.Windows.Forms.Label TileSize;
        private System.Windows.Forms.Button Changes;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Panel TilesPanel;
        private System.Windows.Forms.Label ToggleLabel;
        private System.Windows.Forms.Label FillToggleLabel;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TextBox namingFileTextBox;
        private System.Windows.Forms.Label NameOfFile;
        private System.Windows.Forms.Panel savingPanel;
        private System.Windows.Forms.Timer mapTimer;
        private System.Windows.Forms.Label label1;
        private TilePictureBox waterTile;
        private TilePictureBox grassTile;
        private TilePictureBox stoneTile;
        private TilePictureBox sandTile;
        private TilePictureBox rock2;
        private TilePictureBox rock1;
        private TilePictureBox rock3;
        private TilePictureBox redSquare;
        private System.Windows.Forms.Label label2;
    }
}

