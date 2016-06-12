namespace MapEditor
{
    partial class MainWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_wallButton = new System.Windows.Forms.RadioButton();
            this.m_floorButton = new System.Windows.Forms.RadioButton();
            this.m_furnitureButton = new System.Windows.Forms.RadioButton();
            this.m_mapPanel = new MapEditor.MapPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1012, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // m_wallButton
            // 
            this.m_wallButton.AutoSize = true;
            this.m_wallButton.Location = new System.Drawing.Point(892, 31);
            this.m_wallButton.Name = "m_wallButton";
            this.m_wallButton.Size = new System.Drawing.Size(67, 21);
            this.m_wallButton.TabIndex = 2;
            this.m_wallButton.TabStop = true;
            this.m_wallButton.Text = "WALL";
            this.m_wallButton.UseVisualStyleBackColor = true;
            this.m_wallButton.CheckedChanged += new System.EventHandler(this.wallButton_CheckedChanged);
            // 
            // m_floorButton
            // 
            this.m_floorButton.AutoSize = true;
            this.m_floorButton.Location = new System.Drawing.Point(892, 77);
            this.m_floorButton.Name = "m_floorButton";
            this.m_floorButton.Size = new System.Drawing.Size(77, 21);
            this.m_floorButton.TabIndex = 3;
            this.m_floorButton.TabStop = true;
            this.m_floorButton.Text = "FLOOR";
            this.m_floorButton.UseVisualStyleBackColor = true;
            this.m_floorButton.CheckedChanged += new System.EventHandler(this.floorButton_CheckedChanged);
            // 
            // m_furnitureButton
            // 
            this.m_furnitureButton.AutoSize = true;
            this.m_furnitureButton.Location = new System.Drawing.Point(892, 124);
            this.m_furnitureButton.Name = "m_furnitureButton";
            this.m_furnitureButton.Size = new System.Drawing.Size(108, 21);
            this.m_furnitureButton.TabIndex = 4;
            this.m_furnitureButton.TabStop = true;
            this.m_furnitureButton.Text = "FURNITURE";
            this.m_furnitureButton.UseVisualStyleBackColor = true;
            this.m_furnitureButton.CheckedChanged += new System.EventHandler(this.furnitureButton_CheckedChanged);
            // 
            // m_mapPanel
            // 
            this.m_mapPanel.AutoScroll = true;
            this.m_mapPanel.chosenType = MapEditor.Field.Type.FIELD_TYPE_UNKNOWN;
            this.m_mapPanel.Location = new System.Drawing.Point(12, 31);
            this.m_mapPanel.Name = "m_mapPanel";
            this.m_mapPanel.Size = new System.Drawing.Size(855, 480);
            this.m_mapPanel.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 522);
            this.Controls.Add(this.m_furnitureButton);
            this.Controls.Add(this.m_floorButton);
            this.Controls.Add(this.m_wallButton);
            this.Controls.Add(this.m_mapPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private MapPanel m_mapPanel;
        private System.Windows.Forms.RadioButton m_wallButton;
        private System.Windows.Forms.RadioButton m_floorButton;
        private System.Windows.Forms.RadioButton m_furnitureButton;
    }
}

