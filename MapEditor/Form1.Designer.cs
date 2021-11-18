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
            this.widthBox = new System.Windows.Forms.NumericUpDown();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.heightBox = new System.Windows.Forms.NumericUpDown();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.tileSizeBox = new System.Windows.Forms.NumericUpDown();
            this.TileSize = new System.Windows.Forms.Label();
            this.Changes = new System.Windows.Forms.Button();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.TilesPanel = new System.Windows.Forms.Panel();
            this.ToggleLabel = new System.Windows.Forms.Label();
            this.FillToggleLabel = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.namingFileTextBox = new System.Windows.Forms.TextBox();
            this.NameOfFile = new System.Windows.Forms.Label();
            this.sandTile = new System.Windows.Forms.PictureBox();
            this.stoneTile = new System.Windows.Forms.PictureBox();
            this.grassTile = new System.Windows.Forms.PictureBox();
            this.waterTile = new System.Windows.Forms.PictureBox();
            this.map = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileSizeBox)).BeginInit();
            this.ControlPanel.SuspendLayout();
            this.TilesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sandTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stoneTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grassTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waterTile)).BeginInit();
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
            this.WidthLabel.Location = new System.Drawing.Point(23, 9);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(35, 13);
            this.WidthLabel.TabIndex = 3;
            this.WidthLabel.Text = "Width";
            this.WidthLabel.Click += new System.EventHandler(this.label1_Click);
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
            this.HeightLabel.Location = new System.Drawing.Point(23, 57);
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
            this.ControlPanel.Size = new System.Drawing.Size(200, 256);
            this.ControlPanel.TabIndex = 9;
            // 
            // TilesPanel
            // 
            this.TilesPanel.Controls.Add(this.sandTile);
            this.TilesPanel.Controls.Add(this.stoneTile);
            this.TilesPanel.Controls.Add(this.grassTile);
            this.TilesPanel.Controls.Add(this.waterTile);
            this.TilesPanel.Location = new System.Drawing.Point(606, 263);
            this.TilesPanel.Name = "TilesPanel";
            this.TilesPanel.Size = new System.Drawing.Size(249, 201);
            this.TilesPanel.TabIndex = 10;
            // 
            // ToggleLabel
            // 
            this.ToggleLabel.AutoSize = true;
            this.ToggleLabel.Location = new System.Drawing.Point(836, 9);
            this.ToggleLabel.Name = "ToggleLabel";
            this.ToggleLabel.Size = new System.Drawing.Size(100, 13);
            this.ToggleLabel.TabIndex = 11;
            this.ToggleLabel.Text = "Toggle: Off (key: H)";
            // 
            // FillToggleLabel
            // 
            this.FillToggleLabel.AutoSize = true;
            this.FillToggleLabel.Location = new System.Drawing.Point(836, 62);
            this.FillToggleLabel.Name = "FillToggleLabel";
            this.FillToggleLabel.Size = new System.Drawing.Size(77, 13);
            this.FillToggleLabel.TabIndex = 12;
            this.FillToggleLabel.Text = "Fill: Off (key: F)";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(839, 102);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 13;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // namingFileTextBox
            // 
            this.namingFileTextBox.Location = new System.Drawing.Point(910, 146);
            this.namingFileTextBox.Name = "namingFileTextBox";
            this.namingFileTextBox.Size = new System.Drawing.Size(100, 20);
            this.namingFileTextBox.TabIndex = 14;
            // 
            // NameOfFile
            // 
            this.NameOfFile.AutoSize = true;
            this.NameOfFile.Location = new System.Drawing.Point(836, 149);
            this.NameOfFile.Name = "NameOfFile";
            this.NameOfFile.Size = new System.Drawing.Size(71, 13);
            this.NameOfFile.TabIndex = 15;
            this.NameOfFile.Text = "Name Of File:";
            // 
            // sandTile
            // 
            this.sandTile.Image = global::MapEditor.Properties.Resources.SandTileV2;
            this.sandTile.Location = new System.Drawing.Point(134, 108);
            this.sandTile.Name = "sandTile";
            this.sandTile.Size = new System.Drawing.Size(100, 82);
            this.sandTile.TabIndex = 13;
            this.sandTile.TabStop = false;
            this.sandTile.Tag = "SandTile";
            this.sandTile.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // stoneTile
            // 
            this.stoneTile.Image = global::MapEditor.Properties.Resources.stoneTile21;
            this.stoneTile.Location = new System.Drawing.Point(16, 108);
            this.stoneTile.Name = "stoneTile";
            this.stoneTile.Size = new System.Drawing.Size(100, 82);
            this.stoneTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stoneTile.TabIndex = 12;
            this.stoneTile.TabStop = false;
            this.stoneTile.Tag = "StoneTile";
            this.stoneTile.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // grassTile
            // 
            this.grassTile.Image = global::MapEditor.Properties.Resources.grassTile;
            this.grassTile.Location = new System.Drawing.Point(134, 20);
            this.grassTile.Name = "grassTile";
            this.grassTile.Size = new System.Drawing.Size(100, 82);
            this.grassTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.grassTile.TabIndex = 1;
            this.grassTile.TabStop = false;
            this.grassTile.Tag = "GrassTile";
            // 
            // waterTile
            // 
            this.waterTile.Image = global::MapEditor.Properties.Resources.waterTile;
            this.waterTile.Location = new System.Drawing.Point(16, 20);
            this.waterTile.Name = "waterTile";
            this.waterTile.Size = new System.Drawing.Size(100, 82);
            this.waterTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.waterTile.TabIndex = 0;
            this.waterTile.TabStop = false;
            this.waterTile.Tag = "WaterTile";
            this.waterTile.Click += new System.EventHandler(this.waterTile_Click);
            // 
            // map
            // 
            this.map.BackColor = System.Drawing.SystemColors.Desktop;
            this.map.Location = new System.Drawing.Point(-1, 0);
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(600, 400);
            this.map.TabIndex = 0;
            this.map.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 517);
            this.Controls.Add(this.NameOfFile);
            this.Controls.Add(this.namingFileTextBox);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.FillToggleLabel);
            this.Controls.Add(this.ToggleLabel);
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
            ((System.ComponentModel.ISupportInitialize)(this.sandTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stoneTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grassTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waterTile)).EndInit();
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
        private System.Windows.Forms.PictureBox grassTile;
        private System.Windows.Forms.PictureBox waterTile;
        private System.Windows.Forms.Label ToggleLabel;
        private System.Windows.Forms.PictureBox stoneTile;
        private System.Windows.Forms.Label FillToggleLabel;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TextBox namingFileTextBox;
        private System.Windows.Forms.Label NameOfFile;
        private System.Windows.Forms.PictureBox sandTile;
    }
}

