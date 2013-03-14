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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьВсёToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьДанныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.перейтиНаБолееСтарыеДанныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.перейтиНаБолееНовыеДанныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.удалитьДанныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.задачаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьМетодToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.установитьИнтервалToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.сделатьШагToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.запуститьToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.остановитьToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.задачаToolStripMenuItem,
            this.задачаToolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.TabStop = true;
            this.menuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьВсёToolStripMenuItem,
            this.новыйToolStripMenuItem,
            this.toolStripMenuItem1,
            this.загрузитьToolStripMenuItem,
            this.tsmiSave,
            this.toolStripMenuItem2,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // очиститьВсёToolStripMenuItem
            // 
            this.очиститьВсёToolStripMenuItem.Name = "очиститьВсёToolStripMenuItem";
            this.очиститьВсёToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.очиститьВсёToolStripMenuItem.Text = "Очистить всё";
            this.очиститьВсёToolStripMenuItem.Click += new System.EventHandler(this.Очистить_Click);
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.новыйToolStripMenuItem.Text = "Очистить размещение";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.Очистить_размещение_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 6);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить...";
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(199, 22);
            this.tsmiSave.Text = "Сохранить...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.Выход_Click);
            // 
            // задачаToolStripMenuItem
            // 
            this.задачаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьПолосуToolStripMenuItem,
            this.toolStripMenuItem3,
            this.создатьКругToolStripMenuItem,
            this.создатьНаборКруговToolStripMenuItem,
            this.toolStripMenuItem4,
            this.сохранитьДанныеToolStripMenuItem,
            this.toolStripMenuItem6,
            this.перейтиНаБолееСтарыеДанныеToolStripMenuItem,
            this.перейтиНаБолееНовыеДанныеToolStripMenuItem,
            this.toolStripMenuItem5,
            this.удалитьДанныеToolStripMenuItem});
            this.задачаToolStripMenuItem.Name = "задачаToolStripMenuItem";
            this.задачаToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.задачаToolStripMenuItem.Text = "Данные";
            // 
            // создатьПолосуToolStripMenuItem
            // 
            this.создатьПолосуToolStripMenuItem.Name = "создатьПолосуToolStripMenuItem";
            this.создатьПолосуToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.создатьПолосуToolStripMenuItem.Text = "Настроить полосу...";
            this.создатьПолосуToolStripMenuItem.Click += new System.EventHandler(this.Настроить_полосу_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(256, 6);
            // 
            // создатьКругToolStripMenuItem
            // 
            this.создатьКругToolStripMenuItem.Name = "создатьКругToolStripMenuItem";
            this.создатьКругToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.создатьКругToolStripMenuItem.Text = "Создать круг...";
            this.создатьКругToolStripMenuItem.Click += new System.EventHandler(this.Создать_круг_Click);
            // 
            // создатьНаборКруговToolStripMenuItem
            // 
            this.создатьНаборКруговToolStripMenuItem.Name = "создатьНаборКруговToolStripMenuItem";
            this.создатьНаборКруговToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.создатьНаборКруговToolStripMenuItem.Text = "Создать несколько кругов...";
            this.создатьНаборКруговToolStripMenuItem.Click += new System.EventHandler(this.Создать_несколько_кругов_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(256, 6);
            // 
            // сохранитьДанныеToolStripMenuItem
            // 
            this.сохранитьДанныеToolStripMenuItem.Name = "сохранитьДанныеToolStripMenuItem";
            this.сохранитьДанныеToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.сохранитьДанныеToolStripMenuItem.Text = "Сохранить данные";
            this.сохранитьДанныеToolStripMenuItem.Click += new System.EventHandler(this.Сохранить_данные_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(256, 6);
            // 
            // перейтиНаБолееСтарыеДанныеToolStripMenuItem
            // 
            this.перейтиНаБолееСтарыеДанныеToolStripMenuItem.Name = "перейтиНаБолееСтарыеДанныеToolStripMenuItem";
            this.перейтиНаБолееСтарыеДанныеToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.перейтиНаБолееСтарыеДанныеToolStripMenuItem.Text = "Перейти на более старые данные";
            this.перейтиНаБолееСтарыеДанныеToolStripMenuItem.Click += new System.EventHandler(this.Перейти_на_более_старые_данные_Click);
            // 
            // перейтиНаБолееНовыеДанныеToolStripMenuItem
            // 
            this.перейтиНаБолееНовыеДанныеToolStripMenuItem.Name = "перейтиНаБолееНовыеДанныеToolStripMenuItem";
            this.перейтиНаБолееНовыеДанныеToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.перейтиНаБолееНовыеДанныеToolStripMenuItem.Text = "Перейти на более новые данные";
            this.перейтиНаБолееНовыеДанныеToolStripMenuItem.Click += new System.EventHandler(this.Перейти_на_более_новые_данные_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(256, 6);
            // 
            // удалитьДанныеToolStripMenuItem
            // 
            this.удалитьДанныеToolStripMenuItem.Name = "удалитьДанныеToolStripMenuItem";
            this.удалитьДанныеToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.удалитьДанныеToolStripMenuItem.Text = "Удалить данные";
            this.удалитьДанныеToolStripMenuItem.Click += new System.EventHandler(this.Удалить_данные_Click);
            // 
            // задачаToolStripMenuItem1
            // 
            this.задачаToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьМетодToolStripMenuItem,
            this.toolStripMenuItem9,
            this.установитьИнтервалToolStripMenuItem,
            this.toolStripMenuItem10,
            this.сделатьШагToolStripMenuItem,
            this.toolStripMenuItem12,
            this.запуститьToolStripMenuItem3,
            this.остановитьToolStripMenuItem3});
            this.задачаToolStripMenuItem1.Name = "задачаToolStripMenuItem1";
            this.задачаToolStripMenuItem1.Size = new System.Drawing.Size(57, 20);
            this.задачаToolStripMenuItem1.Text = "Задача";
            // 
            // выбратьМетодToolStripMenuItem
            // 
            this.выбратьМетодToolStripMenuItem.Name = "выбратьМетодToolStripMenuItem";
            this.выбратьМетодToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.выбратьМетодToolStripMenuItem.Text = "Выбрать метод...";
            this.выбратьМетодToolStripMenuItem.Click += new System.EventHandler(this.Выбрать_метод_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(196, 6);
            // 
            // установитьИнтервалToolStripMenuItem
            // 
            this.установитьИнтервалToolStripMenuItem.Name = "установитьИнтервалToolStripMenuItem";
            this.установитьИнтервалToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.установитьИнтервалToolStripMenuItem.Text = "Установить интервал...";
            this.установитьИнтервалToolStripMenuItem.Click += new System.EventHandler(this.Установить_интервал_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(196, 6);
            // 
            // сделатьШагToolStripMenuItem
            // 
            this.сделатьШагToolStripMenuItem.Name = "сделатьШагToolStripMenuItem";
            this.сделатьШагToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.сделатьШагToolStripMenuItem.Text = "Сделать шаг";
            this.сделатьШагToolStripMenuItem.Click += new System.EventHandler(this.Сделать_шаг_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(196, 6);
            // 
            // запуститьToolStripMenuItem3
            // 
            this.запуститьToolStripMenuItem3.Name = "запуститьToolStripMenuItem3";
            this.запуститьToolStripMenuItem3.Size = new System.Drawing.Size(199, 22);
            this.запуститьToolStripMenuItem3.Text = "Запустить";
            this.запуститьToolStripMenuItem3.Click += new System.EventHandler(this.Запустить_Click);
            // 
            // остановитьToolStripMenuItem3
            // 
            this.остановитьToolStripMenuItem3.Name = "остановитьToolStripMenuItem3";
            this.остановитьToolStripMenuItem3.Size = new System.Drawing.Size(199, 22);
            this.остановитьToolStripMenuItem3.Text = "Остановить";
            this.остановитьToolStripMenuItem3.Click += new System.EventHandler(this.Остановить_Click);
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
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
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormPlaceCircle_Paint);
            this.Resize += new System.EventHandler(this.FormPlaceCircle_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem выбратьМетодToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem установитьИнтервалToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem сделатьШагToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem запуститьToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem остановитьToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem очиститьВсёToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem сохранитьДанныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem перейтиНаБолееСтарыеДанныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem перейтиНаБолееНовыеДанныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem удалитьДанныеToolStripMenuItem;
    }
}

