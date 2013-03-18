namespace Opt.Algorithms.WFAT
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
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.задачаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьПолосуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.создатьКругToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьНаборКруговToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.задачаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.рассчитатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.методБарьеровклассическийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.методБарьеровмодифицированныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(452, 22);
            this.toolStripMenuItem7.Text = "Метод последовательно-одиночного размещения (модифицированный)";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.miStepSearchWithCloseModel_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.задачаToolStripMenuItem,
            this.задачаToolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point2d(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.TabStop = true;
            this.menuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.toolStripMenuItem1,
            this.загрузитьToolStripMenuItem,
            this.tsmiSave,
            this.toolStripMenuItem2,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.новыйToolStripMenuItem.Text = "Очистить";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.miClear_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить...";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.miLoad_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(152, 22);
            this.tsmiSave.Text = "Сохранить...";
            this.tsmiSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.miExit_Click);
            // 
            // задачаToolStripMenuItem
            // 
            this.задачаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьПолосуToolStripMenuItem,
            this.toolStripMenuItem3,
            this.создатьКругToolStripMenuItem,
            this.создатьНаборКруговToolStripMenuItem});
            this.задачаToolStripMenuItem.Name = "задачаToolStripMenuItem";
            this.задачаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.задачаToolStripMenuItem.Text = "Данные";
            // 
            // создатьПолосуToolStripMenuItem
            // 
            this.создатьПолосуToolStripMenuItem.Name = "создатьПолосуToolStripMenuItem";
            this.создатьПолосуToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.создатьПолосуToolStripMenuItem.Text = "Создать полосу";
            this.создатьПолосуToolStripMenuItem.Click += new System.EventHandler(this.miStripInfo_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(196, 6);
            // 
            // создатьКругToolStripMenuItem
            // 
            this.создатьКругToolStripMenuItem.Name = "создатьКругToolStripMenuItem";
            this.создатьКругToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.создатьКругToolStripMenuItem.Text = "Создать круг";
            this.создатьКругToolStripMenuItem.Click += new System.EventHandler(this.miCircleInfo_Click);
            // 
            // создатьНаборКруговToolStripMenuItem
            // 
            this.создатьНаборКруговToolStripMenuItem.Name = "создатьНаборКруговToolStripMenuItem";
            this.создатьНаборКруговToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.создатьНаборКруговToolStripMenuItem.Text = "Создать набор кругов";
            this.создатьНаборКруговToolStripMenuItem.Click += new System.EventHandler(this.miCirclesInfo_Click);
            // 
            // задачаToolStripMenuItem1
            // 
            this.задачаToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.рассчитатьToolStripMenuItem,
            this.toolStripMenuItem7,
            this.методБарьеровклассическийToolStripMenuItem,
            this.методБарьеровмодифицированныйToolStripMenuItem});
            this.задачаToolStripMenuItem1.Name = "задачаToolStripMenuItem1";
            this.задачаToolStripMenuItem1.Size = new System.Drawing.Size(56, 20);
            this.задачаToolStripMenuItem1.Text = "Задача";
            // 
            // рассчитатьToolStripMenuItem
            // 
            this.рассчитатьToolStripMenuItem.Name = "рассчитатьToolStripMenuItem";
            this.рассчитатьToolStripMenuItem.Size = new System.Drawing.Size(452, 22);
            this.рассчитатьToolStripMenuItem.Text = "Метод последовательно-одиночного размещения (классический)";
            this.рассчитатьToolStripMenuItem.Click += new System.EventHandler(this.miStepSearchClassic_Click);
            // 
            // методБарьеровклассическийToolStripMenuItem
            // 
            this.методБарьеровклассическийToolStripMenuItem.Name = "методБарьеровклассическийToolStripMenuItem";
            this.методБарьеровклассическийToolStripMenuItem.Size = new System.Drawing.Size(452, 22);
            this.методБарьеровклассическийToolStripMenuItem.Text = "Метод барьеров (классический)";
            this.методБарьеровклассическийToolStripMenuItem.Click += new System.EventHandler(this.miStepLocalSearchClassic_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // методБарьеровмодифицированныйToolStripMenuItem
            // 
            this.методБарьеровмодифицированныйToolStripMenuItem.Name = "методБарьеровмодифицированныйToolStripMenuItem";
            this.методБарьеровмодифицированныйToolStripMenuItem.Size = new System.Drawing.Size(452, 22);
            this.методБарьеровмодифицированныйToolStripMenuItem.Text = "Метод барьеров (модифицированный)";
            this.методБарьеровмодифицированныйToolStripMenuItem.Click += new System.EventHandler(this.miStepLocalSearchWithCloseModel_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Name = "FormMain";
            this.Text = "Упаковка кругов в полосу";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormPlaceCircle_Paint);
            this.Resize += new System.EventHandler(this.FormPlaceCircle_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem задачаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьПолосуToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem создатьКругToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьНаборКруговToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem задачаToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem рассчитатьToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem методБарьеровклассическийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem методБарьеровмодифицированныйToolStripMenuItem;
    }
}

