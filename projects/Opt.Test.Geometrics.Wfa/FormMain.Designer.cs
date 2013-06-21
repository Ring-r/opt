namespace Opt.Geometrics.WFAT
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createPolygonVisualMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сделатьШагToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.запуститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.остановитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.установитьИнтервалToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemCreateDividePlane = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.dataMenuItem,
            this.видToolStripMenuItem,
            this.taskMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьToolStripMenuItem,
            this.toolStripMenuItem1,
            this.загрузитьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.toolStripMenuItem2,
            this.выходToolStripMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileMenuItem.Text = "Файл";
            // 
            // очиститьToolStripMenuItem
            // 
            this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
            this.очиститьToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.очиститьToolStripMenuItem.Text = "Очистить";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 6);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить...";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(138, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // dataMenuItem
            // 
            this.dataMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createPolygonVisualMenuItem,
            this.toolStripMenuItem6,
            this.toolStripMenuItemCreateDividePlane});
            this.dataMenuItem.Name = "dataMenuItem";
            this.dataMenuItem.Size = new System.Drawing.Size(62, 20);
            this.dataMenuItem.Text = "Данные";
            // 
            // createPolygonVisualMenuItem
            // 
            this.createPolygonVisualMenuItem.Name = "createPolygonVisualMenuItem";
            this.createPolygonVisualMenuItem.Size = new System.Drawing.Size(206, 22);
            this.createPolygonVisualMenuItem.Text = "Создать многоугольник";
            // 
            // taskMenuItem
            // 
            this.taskMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сделатьШагToolStripMenuItem,
            this.toolStripMenuItem3,
            this.запуститьToolStripMenuItem,
            this.остановитьToolStripMenuItem,
            this.toolStripMenuItem4,
            this.установитьИнтервалToolStripMenuItem});
            this.taskMenuItem.Name = "taskMenuItem";
            this.taskMenuItem.Size = new System.Drawing.Size(57, 20);
            this.taskMenuItem.Text = "Задача";
            // 
            // сделатьШагToolStripMenuItem
            // 
            this.сделатьШагToolStripMenuItem.Name = "сделатьШагToolStripMenuItem";
            this.сделатьШагToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.сделатьШагToolStripMenuItem.Text = "Сделать шаг";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(187, 6);
            // 
            // запуститьToolStripMenuItem
            // 
            this.запуститьToolStripMenuItem.Name = "запуститьToolStripMenuItem";
            this.запуститьToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.запуститьToolStripMenuItem.Text = "Запустить";
            // 
            // остановитьToolStripMenuItem
            // 
            this.остановитьToolStripMenuItem.Name = "остановитьToolStripMenuItem";
            this.остановитьToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.остановитьToolStripMenuItem.Text = "Остановить";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(187, 6);
            // 
            // установитьИнтервалToolStripMenuItem
            // 
            this.установитьИнтервалToolStripMenuItem.Name = "установитьИнтервалToolStripMenuItem";
            this.установитьИнтервалToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.установитьИнтервалToolStripMenuItem.Text = "Установить интервал";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(584, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(203, 6);
            // 
            // toolStripMenuItemCreateDividePlane
            // 
            this.toolStripMenuItemCreateDividePlane.Name = "toolStripMenuItemCreateDividePlane";
            this.toolStripMenuItemCreateDividePlane.Size = new System.Drawing.Size(206, 22);
            this.toolStripMenuItemCreateDividePlane.Text = "Создать разделяющие";
            this.toolStripMenuItemCreateDividePlane.Click += new System.EventHandler(this.toolStripMenuItemCreateDividePlane_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMain_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseUp);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очиститьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createPolygonVisualMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taskMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сделатьШагToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem запуститьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem остановитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem установитьИнтервалToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCreateDividePlane;
    }
}

