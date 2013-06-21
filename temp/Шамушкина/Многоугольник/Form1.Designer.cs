namespace Многоугольник
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DrawPanel = new System.Windows.Forms.Panel();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygons1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygons2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygons3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showConvexPartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DrawPanel
            // 
            this.DrawPanel.Location = new System.Drawing.Point(12, 114);
            this.DrawPanel.Name = "DrawPanel";
            this.DrawPanel.Size = new System.Drawing.Size(772, 163);
            this.DrawPanel.TabIndex = 0;
            this.DrawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPanel_Paint);
            // 
            // textBoxLength
            // 
            this.textBoxLength.Location = new System.Drawing.Point(74, 81);
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.Size = new System.Drawing.Size(131, 20);
            this.textBoxLength.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Length";
            // 
            // timer1
            // 
            this.timer1.Interval = 150;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(562, 18);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(94, 83);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(691, 18);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(94, 83);
            this.buttonPause.TabIndex = 5;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readToolStripMenuItem,
            this.showConvexPartsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(797, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.polygons1ToolStripMenuItem,
            this.polygons2ToolStripMenuItem,
            this.polygons3ToolStripMenuItem});
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.readToolStripMenuItem.Text = "Read";
            // 
            // polygons1ToolStripMenuItem
            // 
            this.polygons1ToolStripMenuItem.Name = "polygons1ToolStripMenuItem";
            this.polygons1ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.polygons1ToolStripMenuItem.Text = "Polygons1";
            this.polygons1ToolStripMenuItem.Click += new System.EventHandler(this.polygons1ToolStripMenuItem_Click);
            // 
            // polygons2ToolStripMenuItem
            // 
            this.polygons2ToolStripMenuItem.Name = "polygons2ToolStripMenuItem";
            this.polygons2ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.polygons2ToolStripMenuItem.Text = "Polygons2";
            this.polygons2ToolStripMenuItem.Click += new System.EventHandler(this.polygons2ToolStripMenuItem_Click);
            // 
            // polygons3ToolStripMenuItem
            // 
            this.polygons3ToolStripMenuItem.Name = "polygons3ToolStripMenuItem";
            this.polygons3ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.polygons3ToolStripMenuItem.Text = "Polygons3";
            this.polygons3ToolStripMenuItem.Click += new System.EventHandler(this.polygons3ToolStripMenuItem_Click);
            // 
            // showConvexPartsToolStripMenuItem
            // 
            this.showConvexPartsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yesToolStripMenuItem,
            this.noToolStripMenuItem});
            this.showConvexPartsToolStripMenuItem.Name = "showConvexPartsToolStripMenuItem";
            this.showConvexPartsToolStripMenuItem.Size = new System.Drawing.Size(117, 20);
            this.showConvexPartsToolStripMenuItem.Text = "Show convex parts";
            // 
            // yesToolStripMenuItem
            // 
            this.yesToolStripMenuItem.Name = "yesToolStripMenuItem";
            this.yesToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.yesToolStripMenuItem.Text = "Yes";
            this.yesToolStripMenuItem.Click += new System.EventHandler(this.yesToolStripMenuItem_Click);
            // 
            // noToolStripMenuItem
            // 
            this.noToolStripMenuItem.Name = "noToolStripMenuItem";
            this.noToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.noToolStripMenuItem.Text = "No";
            this.noToolStripMenuItem.Click += new System.EventHandler(this.noToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 290);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLength);
            this.Controls.Add(this.DrawPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DrawPanel;
        private System.Windows.Forms.TextBox textBoxLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygons1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygons2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygons3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showConvexPartsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noToolStripMenuItem;
    }
}

