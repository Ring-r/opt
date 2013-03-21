namespace Opt
{
    namespace Pucking_Circle_Strip_FormApp
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
                this.menuStrip1 = new System.Windows.Forms.MenuStrip();
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
                this.запуститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
                this.сделатьШагToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.остановитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
                this.время500ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
                this.сделатьШагToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
                this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
                this.запуститьToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
                this.остановитьToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
                this.методЛокальногоПоискаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.сделатьШагToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
                this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
                this.запуститьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
                this.остановитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
                this.timer1 = new System.Windows.Forms.Timer(this.components);
                this.menuStrip1.SuspendLayout();
                this.SuspendLayout();
                // 
                // menuStrip1
                // 
                this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.задачаToolStripMenuItem,
            this.задачаToolStripMenuItem1});
                this.menuStrip1.Location = new System.Drawing.Point(0, 0);
                this.menuStrip1.Name = "menuStrip1";
                this.menuStrip1.Size = new System.Drawing.Size(617, 24);
                this.menuStrip1.TabIndex = 0;
                this.menuStrip1.TabStop = true;
                this.menuStrip1.Text = "menuStrip1";
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
                // 
                // tsmiSave
                // 
                this.tsmiSave.Name = "tsmiSave";
                this.tsmiSave.Size = new System.Drawing.Size(152, 22);
                this.tsmiSave.Text = "Сохранить...";
                this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
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
            this.методЛокальногоПоискаToolStripMenuItem});
                this.задачаToolStripMenuItem1.Name = "задачаToolStripMenuItem1";
                this.задачаToolStripMenuItem1.Size = new System.Drawing.Size(56, 20);
                this.задачаToolStripMenuItem1.Text = "Задача";
                // 
                // рассчитатьToolStripMenuItem
                // 
                this.рассчитатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.запуститьToolStripMenuItem,
            this.toolStripMenuItem6,
            this.сделатьШагToolStripMenuItem,
            this.остановитьToolStripMenuItem,
            this.toolStripMenuItem4,
            this.время500ToolStripMenuItem});
                this.рассчитатьToolStripMenuItem.Name = "рассчитатьToolStripMenuItem";
                this.рассчитатьToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
                this.рассчитатьToolStripMenuItem.Text = "Метод ПОР";
                // 
                // запуститьToolStripMenuItem
                // 
                this.запуститьToolStripMenuItem.Name = "запуститьToolStripMenuItem";
                this.запуститьToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
                this.запуститьToolStripMenuItem.Text = "Сделать шаг";
                this.запуститьToolStripMenuItem.Click += new System.EventHandler(this.miCreateStep_Click);
                // 
                // toolStripMenuItem6
                // 
                this.toolStripMenuItem6.Name = "toolStripMenuItem6";
                this.toolStripMenuItem6.Size = new System.Drawing.Size(193, 6);
                // 
                // сделатьШагToolStripMenuItem
                // 
                this.сделатьШагToolStripMenuItem.Name = "сделатьШагToolStripMenuItem";
                this.сделатьШагToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
                this.сделатьШагToolStripMenuItem.Text = "Запустить";
                this.сделатьШагToolStripMenuItem.Click += new System.EventHandler(this.miStart_Click);
                // 
                // остановитьToolStripMenuItem
                // 
                this.остановитьToolStripMenuItem.Name = "остановитьToolStripMenuItem";
                this.остановитьToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
                this.остановитьToolStripMenuItem.Text = "Остановить";
                this.остановитьToolStripMenuItem.Click += new System.EventHandler(this.miStop_Click);
                // 
                // toolStripMenuItem4
                // 
                this.toolStripMenuItem4.Name = "toolStripMenuItem4";
                this.toolStripMenuItem4.Size = new System.Drawing.Size(193, 6);
                // 
                // время500ToolStripMenuItem
                // 
                this.время500ToolStripMenuItem.Name = "время500ToolStripMenuItem";
                this.время500ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
                this.время500ToolStripMenuItem.Text = "Установить интервал";
                this.время500ToolStripMenuItem.Click += new System.EventHandler(this.miInterval_Click);
                // 
                // toolStripMenuItem7
                // 
                this.toolStripMenuItem7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сделатьШагToolStripMenuItem2,
            this.toolStripMenuItem8,
            this.запуститьToolStripMenuItem2,
            this.остановитьToolStripMenuItem2});
                this.toolStripMenuItem7.Name = "toolStripMenuItem7";
                this.toolStripMenuItem7.Size = new System.Drawing.Size(218, 22);
                this.toolStripMenuItem7.Text = "Метод ПОР+";
                // 
                // сделатьШагToolStripMenuItem2
                // 
                this.сделатьШагToolStripMenuItem2.Name = "сделатьШагToolStripMenuItem2";
                this.сделатьШагToolStripMenuItem2.Size = new System.Drawing.Size(151, 22);
                this.сделатьШагToolStripMenuItem2.Text = "Сделать шаг";
                this.сделатьШагToolStripMenuItem2.Click += new System.EventHandler(this.miCreateStepUpgrade);
                // 
                // toolStripMenuItem8
                // 
                this.toolStripMenuItem8.Name = "toolStripMenuItem8";
                this.toolStripMenuItem8.Size = new System.Drawing.Size(148, 6);
                // 
                // запуститьToolStripMenuItem2
                // 
                this.запуститьToolStripMenuItem2.Name = "запуститьToolStripMenuItem2";
                this.запуститьToolStripMenuItem2.Size = new System.Drawing.Size(151, 22);
                this.запуститьToolStripMenuItem2.Text = "Запустить";
                // 
                // остановитьToolStripMenuItem2
                // 
                this.остановитьToolStripMenuItem2.Name = "остановитьToolStripMenuItem2";
                this.остановитьToolStripMenuItem2.Size = new System.Drawing.Size(151, 22);
                this.остановитьToolStripMenuItem2.Text = "Остановить";
                // 
                // методЛокальногоПоискаToolStripMenuItem
                // 
                this.методЛокальногоПоискаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сделатьШагToolStripMenuItem1,
            this.toolStripMenuItem5,
            this.запуститьToolStripMenuItem1,
            this.остановитьToolStripMenuItem1});
                this.методЛокальногоПоискаToolStripMenuItem.Name = "методЛокальногоПоискаToolStripMenuItem";
                this.методЛокальногоПоискаToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
                this.методЛокальногоПоискаToolStripMenuItem.Text = "Метод локального поиска";
                // 
                // сделатьШагToolStripMenuItem1
                // 
                this.сделатьШагToolStripMenuItem1.Name = "сделатьШагToolStripMenuItem1";
                this.сделатьШагToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
                this.сделатьШагToolStripMenuItem1.Text = "Сделать шаг";
                this.сделатьШагToolStripMenuItem1.Click += new System.EventHandler(this.miStepLocalSearch_Click);
                // 
                // toolStripMenuItem5
                // 
                this.toolStripMenuItem5.Name = "toolStripMenuItem5";
                this.toolStripMenuItem5.Size = new System.Drawing.Size(148, 6);
                // 
                // запуститьToolStripMenuItem1
                // 
                this.запуститьToolStripMenuItem1.Name = "запуститьToolStripMenuItem1";
                this.запуститьToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
                this.запуститьToolStripMenuItem1.Text = "Запустить";
                // 
                // остановитьToolStripMenuItem1
                // 
                this.остановитьToolStripMenuItem1.Name = "остановитьToolStripMenuItem1";
                this.остановитьToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
                this.остановитьToolStripMenuItem1.Text = "Остановить";
                // 
                // timer1
                // 
                this.timer1.Interval = 500;
                this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
                // 
                // FormMain
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(617, 382);
                this.Controls.Add(this.menuStrip1);
                this.DoubleBuffered = true;
                this.MainMenuStrip = this.menuStrip1;
                this.Name = "FormMain";
                this.Text = "Упаковка кругов в полосу";
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormPlaceCircle_Paint);
                this.Resize += new System.EventHandler(this.FormPlaceCircle_Resize);
                this.menuStrip1.ResumeLayout(false);
                this.menuStrip1.PerformLayout();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.MenuStrip menuStrip1;
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
            private System.Windows.Forms.Timer timer1;
            private System.Windows.Forms.ToolStripMenuItem запуститьToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem сделатьШагToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem остановитьToolStripMenuItem;
            private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
            private System.Windows.Forms.ToolStripMenuItem время500ToolStripMenuItem;
            private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
            private System.Windows.Forms.ToolStripMenuItem методЛокальногоПоискаToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem сделатьШагToolStripMenuItem1;
            private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
            private System.Windows.Forms.ToolStripMenuItem запуститьToolStripMenuItem1;
            private System.Windows.Forms.ToolStripMenuItem остановитьToolStripMenuItem1;
            private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
            private System.Windows.Forms.ToolStripMenuItem сделатьШагToolStripMenuItem2;
            private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
            private System.Windows.Forms.ToolStripMenuItem запуститьToolStripMenuItem2;
            private System.Windows.Forms.ToolStripMenuItem остановитьToolStripMenuItem2;
        }
    }
}