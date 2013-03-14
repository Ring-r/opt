namespace PolygonPlacingTest
{
    partial class FormMain
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_back = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel_near = new System.Windows.Forms.Panel();
            this.button_start = new System.Windows.Forms.Button();
            this.numericUpDown_timer = new System.Windows.Forms.NumericUpDown();
            this.label_time = new System.Windows.Forms.Label();
            this.checkBox_timer = new System.Windows.Forms.CheckBox();
            this.textBox_eps = new System.Windows.Forms.TextBox();
            this.label_eps = new System.Windows.Forms.Label();
            this.textBox_beta = new System.Windows.Forms.TextBox();
            this.label_beta = new System.Windows.Forms.Label();
            this.textBox_mu = new System.Windows.Forms.TextBox();
            this.label_mu = new System.Windows.Forms.Label();
            this.textBox_width = new System.Windows.Forms.TextBox();
            this.label_width = new System.Windows.Forms.Label();
            this.button_prestart = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panel_back.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel_near.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timer)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 251);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(492, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(492, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.toolStripMenuItem1,
            this.загрузитьToolStripMenuItem,
            this.toolStripMenuItem2,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.toolStripMenuItem3,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.новыйToolStripMenuItem.Text = "Новый";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(170, 6);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить...";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(170, 6);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как...";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(170, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // panel_back
            // 
            this.panel_back.Controls.Add(this.pictureBox);
            this.panel_back.Controls.Add(this.panel_near);
            this.panel_back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_back.Location = new System.Drawing.Point(0, 24);
            this.panel_back.Name = "panel_back";
            this.panel_back.Size = new System.Drawing.Size(492, 227);
            this.panel_back.TabIndex = 4;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(292, 227);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // panel_near
            // 
            this.panel_near.BackColor = System.Drawing.SystemColors.Control;
            this.panel_near.Controls.Add(this.button_start);
            this.panel_near.Controls.Add(this.numericUpDown_timer);
            this.panel_near.Controls.Add(this.label_time);
            this.panel_near.Controls.Add(this.checkBox_timer);
            this.panel_near.Controls.Add(this.textBox_eps);
            this.panel_near.Controls.Add(this.label_eps);
            this.panel_near.Controls.Add(this.textBox_beta);
            this.panel_near.Controls.Add(this.label_beta);
            this.panel_near.Controls.Add(this.textBox_mu);
            this.panel_near.Controls.Add(this.label_mu);
            this.panel_near.Controls.Add(this.textBox_width);
            this.panel_near.Controls.Add(this.label_width);
            this.panel_near.Controls.Add(this.button_prestart);
            this.panel_near.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_near.Location = new System.Drawing.Point(292, 0);
            this.panel_near.Name = "panel_near";
            this.panel_near.Size = new System.Drawing.Size(200, 227);
            this.panel_near.TabIndex = 1;
            // 
            // button_start
            // 
            this.button_start.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_start.Location = new System.Drawing.Point(0, 205);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(200, 23);
            this.button_start.TabIndex = 12;
            this.button_start.Text = "Запустить метод";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // numericUpDown_timer
            // 
            this.numericUpDown_timer.Dock = System.Windows.Forms.DockStyle.Top;
            this.numericUpDown_timer.Location = new System.Drawing.Point(0, 185);
            this.numericUpDown_timer.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_timer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_timer.Name = "numericUpDown_timer";
            this.numericUpDown_timer.Size = new System.Drawing.Size(200, 20);
            this.numericUpDown_timer.TabIndex = 11;
            this.numericUpDown_timer.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_timer.Visible = false;
            this.numericUpDown_timer.ValueChanged += new System.EventHandler(this.numericUpDown_timer_ValueChanged);
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_time.Location = new System.Drawing.Point(0, 172);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(180, 13);
            this.label_time.TabIndex = 10;
            this.label_time.Text = "Время обновления визуализации:";
            this.label_time.Visible = false;
            // 
            // checkBox_timer
            // 
            this.checkBox_timer.AutoSize = true;
            this.checkBox_timer.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBox_timer.Location = new System.Drawing.Point(0, 155);
            this.checkBox_timer.Name = "checkBox_timer";
            this.checkBox_timer.Size = new System.Drawing.Size(200, 17);
            this.checkBox_timer.TabIndex = 9;
            this.checkBox_timer.Text = "Режим визуализации";
            this.checkBox_timer.UseVisualStyleBackColor = true;
            this.checkBox_timer.CheckedChanged += new System.EventHandler(this.checkBox_timer_CheckedChanged);
            // 
            // textBox_eps
            // 
            this.textBox_eps.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_eps.Location = new System.Drawing.Point(0, 135);
            this.textBox_eps.Name = "textBox_eps";
            this.textBox_eps.Size = new System.Drawing.Size(200, 20);
            this.textBox_eps.TabIndex = 8;
            this.textBox_eps.Text = "1e-3";
            // 
            // label_eps
            // 
            this.label_eps.AutoSize = true;
            this.label_eps.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_eps.Location = new System.Drawing.Point(0, 122);
            this.label_eps.Name = "label_eps";
            this.label_eps.Size = new System.Drawing.Size(27, 13);
            this.label_eps.TabIndex = 7;
            this.label_eps.Text = "eps:";
            // 
            // textBox_beta
            // 
            this.textBox_beta.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_beta.Location = new System.Drawing.Point(0, 102);
            this.textBox_beta.Name = "textBox_beta";
            this.textBox_beta.Size = new System.Drawing.Size(200, 20);
            this.textBox_beta.TabIndex = 6;
            this.textBox_beta.Text = "0,5";
            // 
            // label_beta
            // 
            this.label_beta.AutoSize = true;
            this.label_beta.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_beta.Location = new System.Drawing.Point(0, 89);
            this.label_beta.Name = "label_beta";
            this.label_beta.Size = new System.Drawing.Size(31, 13);
            this.label_beta.TabIndex = 5;
            this.label_beta.Text = "beta:";
            // 
            // textBox_mu
            // 
            this.textBox_mu.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_mu.Location = new System.Drawing.Point(0, 69);
            this.textBox_mu.Name = "textBox_mu";
            this.textBox_mu.Size = new System.Drawing.Size(200, 20);
            this.textBox_mu.TabIndex = 4;
            this.textBox_mu.Text = "100";
            // 
            // label_mu
            // 
            this.label_mu.AutoSize = true;
            this.label_mu.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_mu.Location = new System.Drawing.Point(0, 56);
            this.label_mu.Name = "label_mu";
            this.label_mu.Size = new System.Drawing.Size(24, 13);
            this.label_mu.TabIndex = 3;
            this.label_mu.Text = "mu:";
            // 
            // textBox_width
            // 
            this.textBox_width.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_width.Location = new System.Drawing.Point(0, 36);
            this.textBox_width.Name = "textBox_width";
            this.textBox_width.ReadOnly = true;
            this.textBox_width.Size = new System.Drawing.Size(200, 20);
            this.textBox_width.TabIndex = 2;
            // 
            // label_width
            // 
            this.label_width.AutoSize = true;
            this.label_width.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_width.Location = new System.Drawing.Point(0, 23);
            this.label_width.Name = "label_width";
            this.label_width.Size = new System.Drawing.Size(159, 13);
            this.label_width.TabIndex = 1;
            this.label_width.Text = "Длина занятой части полосы:";
            // 
            // button_prestart
            // 
            this.button_prestart.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_prestart.Location = new System.Drawing.Point(0, 0);
            this.button_prestart.Name = "button_prestart";
            this.button_prestart.Size = new System.Drawing.Size(200, 23);
            this.button_prestart.TabIndex = 0;
            this.button_prestart.Text = "Создать начальное размещение";
            this.button_prestart.UseVisualStyleBackColor = true;
            this.button_prestart.Click += new System.EventHandler(this.button_prestart_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 273);
            this.Controls.Add(this.panel_back);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.Text = "Размещение многоугольников";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel_back.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel_near.ResumeLayout(false);
            this.panel_near.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel panel_back;
        private System.Windows.Forms.Panel panel_near;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.NumericUpDown numericUpDown_timer;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.CheckBox checkBox_timer;
        private System.Windows.Forms.TextBox textBox_eps;
        private System.Windows.Forms.Label label_eps;
        private System.Windows.Forms.TextBox textBox_beta;
        private System.Windows.Forms.Label label_beta;
        private System.Windows.Forms.TextBox textBox_mu;
        private System.Windows.Forms.Label label_mu;
        private System.Windows.Forms.TextBox textBox_width;
        private System.Windows.Forms.Label label_width;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Button button_prestart;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

