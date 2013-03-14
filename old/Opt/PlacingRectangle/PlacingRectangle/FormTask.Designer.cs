namespace PlacingRectangle
{
    partial class FormTask
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
            this.miTask = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadTask = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.miClose = new System.Windows.Forms.ToolStripMenuItem();
            this.miWorkWithObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddObjectLast = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddObjectByIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddObjectsFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.miDelObjectLast = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelObjectByIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelObjectsAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miRealization = new System.Windows.Forms.ToolStripMenuItem();
            this.miStep = new System.Windows.Forms.ToolStripMenuItem();
            this.miSteps = new System.Windows.Forms.ToolStripMenuItem();
            this.miStatistic = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miSpeed = new System.Windows.Forms.ToolStripMenuItem();
            this.miStart = new System.Windows.Forms.ToolStripMenuItem();
            this.miStop = new System.Windows.Forms.ToolStripMenuItem();
            this.miOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.miTaskIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.miTaskUpgradeNumber = new System.Windows.Forms.ToolStripMenuItem();
            this.miRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.miObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miPlacementOpt = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlacementLast = new System.Windows.Forms.ToolStripMenuItem();
            this.gbTask = new System.Windows.Forms.GroupBox();
            this.tlpTask = new System.Windows.Forms.TableLayoutPanel();
            this.gbTaskIndex = new System.Windows.Forms.GroupBox();
            this.cbTaskIndex = new System.Windows.Forms.ComboBox();
            this.gbTaskUpgradeNumber = new System.Windows.Forms.GroupBox();
            this.nudTaskUpgradeNumber = new System.Windows.Forms.NumericUpDown();
            this.gbRegion = new System.Windows.Forms.GroupBox();
            this.gbRegionWidth = new System.Windows.Forms.GroupBox();
            this.tbRegionWidth = new System.Windows.Forms.TextBox();
            this.gbRegionHeight = new System.Windows.Forms.GroupBox();
            this.tbRegionHeight = new System.Windows.Forms.TextBox();
            this.gbObjects = new System.Windows.Forms.GroupBox();
            this.dgvObjects = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.gbTask.SuspendLayout();
            this.tlpTask.SuspendLayout();
            this.gbTaskIndex.SuspendLayout();
            this.gbTaskUpgradeNumber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskUpgradeNumber)).BeginInit();
            this.gbRegion.SuspendLayout();
            this.gbRegionWidth.SuspendLayout();
            this.gbRegionHeight.SuspendLayout();
            this.gbObjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjects)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTask,
            this.miWorkWithObjects,
            this.miRealization,
            this.miOptions});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(392, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.TabStop = true;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miTask
            // 
            this.miTask.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoadTask,
            this.miSaveTask,
            this.toolStripMenuItem6,
            this.miClose});
            this.miTask.Name = "miTask";
            this.miTask.Size = new System.Drawing.Size(56, 20);
            this.miTask.Text = "Задача";
            // 
            // miLoadTask
            // 
            this.miLoadTask.Name = "miLoadTask";
            this.miLoadTask.Size = new System.Drawing.Size(152, 22);
            this.miLoadTask.Text = "Загрузить...";
            this.miLoadTask.Click += new System.EventHandler(this.miLoadTask_Click);
            // 
            // miSaveTask
            // 
            this.miSaveTask.Name = "miSaveTask";
            this.miSaveTask.Size = new System.Drawing.Size(152, 22);
            this.miSaveTask.Text = "Сохранить...";
            this.miSaveTask.Click += new System.EventHandler(this.miSaveTask_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(149, 6);
            // 
            // miClose
            // 
            this.miClose.Name = "miClose";
            this.miClose.Size = new System.Drawing.Size(152, 22);
            this.miClose.Text = "Выход";
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // miWorkWithObjects
            // 
            this.miWorkWithObjects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddObjectLast,
            this.miAddObjectByIndex,
            this.miAddObjects,
            this.miAddObjectsFromFile,
            this.toolStripMenuItem3,
            this.miDelObjectLast,
            this.miDelObjectByIndex,
            this.miDelObjects,
            this.miDelObjectsAll});
            this.miWorkWithObjects.Name = "miWorkWithObjects";
            this.miWorkWithObjects.Size = new System.Drawing.Size(131, 20);
            this.miWorkWithObjects.Text = "Объекты размещения";
            // 
            // miAddObjectLast
            // 
            this.miAddObjectLast.Name = "miAddObjectLast";
            this.miAddObjectLast.Size = new System.Drawing.Size(204, 22);
            this.miAddObjectLast.Text = "Добавить последний...";
            this.miAddObjectLast.Click += new System.EventHandler(this.miAddObjectLast_Click);
            // 
            // miAddObjectByIndex
            // 
            this.miAddObjectByIndex.Name = "miAddObjectByIndex";
            this.miAddObjectByIndex.Size = new System.Drawing.Size(204, 22);
            this.miAddObjectByIndex.Text = "Добавить по номеру...";
            this.miAddObjectByIndex.Click += new System.EventHandler(this.miAddObjectByIndex_Click);
            // 
            // miAddObjects
            // 
            this.miAddObjects.Name = "miAddObjects";
            this.miAddObjects.Size = new System.Drawing.Size(204, 22);
            this.miAddObjects.Text = "Добавить несколько...";
            this.miAddObjects.Click += new System.EventHandler(this.miAddObjects_Click);
            // 
            // miAddObjectsFromFile
            // 
            this.miAddObjectsFromFile.Name = "miAddObjectsFromFile";
            this.miAddObjectsFromFile.Size = new System.Drawing.Size(204, 22);
            this.miAddObjectsFromFile.Text = "Добавить из файла...";
            this.miAddObjectsFromFile.Click += new System.EventHandler(this.miAddObjectsFromFile_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(201, 6);
            // 
            // miDelObjectLast
            // 
            this.miDelObjectLast.Name = "miDelObjectLast";
            this.miDelObjectLast.Size = new System.Drawing.Size(204, 22);
            this.miDelObjectLast.Text = "Удалить последний";
            this.miDelObjectLast.Click += new System.EventHandler(this.miDelObjectLast_Click);
            // 
            // miDelObjectByIndex
            // 
            this.miDelObjectByIndex.Name = "miDelObjectByIndex";
            this.miDelObjectByIndex.Size = new System.Drawing.Size(204, 22);
            this.miDelObjectByIndex.Text = "Удалить по номеру...";
            this.miDelObjectByIndex.Click += new System.EventHandler(this.miDelObjectByIndex_Click);
            // 
            // miDelObjects
            // 
            this.miDelObjects.Name = "miDelObjects";
            this.miDelObjects.Size = new System.Drawing.Size(204, 22);
            this.miDelObjects.Text = "Удалить несколько...";
            this.miDelObjects.Click += new System.EventHandler(this.miDelObjects_Click);
            // 
            // miDelObjectsAll
            // 
            this.miDelObjectsAll.Name = "miDelObjectsAll";
            this.miDelObjectsAll.Size = new System.Drawing.Size(204, 22);
            this.miDelObjectsAll.Text = "Удалить все";
            this.miDelObjectsAll.Click += new System.EventHandler(this.miDelObjectsAll_Click);
            // 
            // miRealization
            // 
            this.miRealization.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStep,
            this.miSteps,
            this.miStatistic,
            this.toolStripMenuItem1,
            this.miSpeed,
            this.miStart,
            this.miStop});
            this.miRealization.Name = "miRealization";
            this.miRealization.Size = new System.Drawing.Size(78, 20);
            this.miRealization.Text = "Реализация";
            // 
            // miStep
            // 
            this.miStep.Name = "miStep";
            this.miStep.Size = new System.Drawing.Size(231, 22);
            this.miStep.Text = "Сделать шаг";
            this.miStep.Click += new System.EventHandler(this.miStep_Click);
            // 
            // miSteps
            // 
            this.miSteps.Name = "miSteps";
            this.miSteps.Size = new System.Drawing.Size(231, 22);
            this.miSteps.Text = "Сделать несколько шагов...";
            this.miSteps.Click += new System.EventHandler(this.miSteps_Click);
            // 
            // miStatistic
            // 
            this.miStatistic.Name = "miStatistic";
            this.miStatistic.Size = new System.Drawing.Size(231, 22);
            this.miStatistic.Text = "Провести анализ...";
            this.miStatistic.Click += new System.EventHandler(this.miStatistic_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(228, 6);
            // 
            // miSpeed
            // 
            this.miSpeed.Name = "miSpeed";
            this.miSpeed.Size = new System.Drawing.Size(231, 22);
            this.miSpeed.Text = "Частота обновления...";
            this.miSpeed.Click += new System.EventHandler(this.miSpeed_Click);
            // 
            // miStart
            // 
            this.miStart.Name = "miStart";
            this.miStart.Size = new System.Drawing.Size(231, 22);
            this.miStart.Text = "Запустить";
            this.miStart.Click += new System.EventHandler(this.miStart_Click);
            // 
            // miStop
            // 
            this.miStop.Name = "miStop";
            this.miStop.Size = new System.Drawing.Size(231, 22);
            this.miStop.Text = "Остановить";
            this.miStop.Click += new System.EventHandler(this.miStop_Click);
            // 
            // miOptions
            // 
            this.miOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTaskIndex,
            this.miTaskUpgradeNumber,
            this.miRegion,
            this.miObjects,
            this.toolStripMenuItem2,
            this.miPlacementOpt,
            this.miPlacementLast});
            this.miOptions.Name = "miOptions";
            this.miOptions.Size = new System.Drawing.Size(73, 20);
            this.miOptions.Text = "Настройки";
            // 
            // miTaskIndex
            // 
            this.miTaskIndex.Name = "miTaskIndex";
            this.miTaskIndex.Size = new System.Drawing.Size(271, 22);
            this.miTaskIndex.Text = "+ Показывать тип задачи";
            this.miTaskIndex.Click += new System.EventHandler(this.miTaskIndex_Click);
            // 
            // miTaskUpgradeNumber
            // 
            this.miTaskUpgradeNumber.Name = "miTaskUpgradeNumber";
            this.miTaskUpgradeNumber.Size = new System.Drawing.Size(271, 22);
            this.miTaskUpgradeNumber.Text = "+ Показывать количество итераций";
            this.miTaskUpgradeNumber.Click += new System.EventHandler(this.miTaskUpgradeNumber_Click);
            // 
            // miRegion
            // 
            this.miRegion.Name = "miRegion";
            this.miRegion.Size = new System.Drawing.Size(271, 22);
            this.miRegion.Text = "+ Показывать область размещения";
            this.miRegion.Click += new System.EventHandler(this.miRegion_Click);
            // 
            // miObjects
            // 
            this.miObjects.Name = "miObjects";
            this.miObjects.Size = new System.Drawing.Size(271, 22);
            this.miObjects.Text = "+ Показывать объекты размещения";
            this.miObjects.Click += new System.EventHandler(this.miObjects_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(268, 6);
            // 
            // miPlacementOpt
            // 
            this.miPlacementOpt.Name = "miPlacementOpt";
            this.miPlacementOpt.Size = new System.Drawing.Size(271, 22);
            this.miPlacementOpt.Text = "Показать лучшее размещение";
            this.miPlacementOpt.Click += new System.EventHandler(this.miPlacementOpt_Click);
            // 
            // miPlacementLast
            // 
            this.miPlacementLast.Name = "miPlacementLast";
            this.miPlacementLast.Size = new System.Drawing.Size(271, 22);
            this.miPlacementLast.Text = "Показать последнее размещение";
            this.miPlacementLast.Click += new System.EventHandler(this.miPlacementLast_Click);
            // 
            // gbTask
            // 
            this.gbTask.Controls.Add(this.tlpTask);
            this.gbTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTask.Location = new System.Drawing.Point(0, 24);
            this.gbTask.Name = "gbTask";
            this.gbTask.Size = new System.Drawing.Size(392, 342);
            this.gbTask.TabIndex = 1;
            this.gbTask.TabStop = false;
            this.gbTask.Text = "Задача:";
            // 
            // tlpTask
            // 
            this.tlpTask.ColumnCount = 2;
            this.tlpTask.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpTask.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTask.Controls.Add(this.gbTaskIndex, 1, 0);
            this.tlpTask.Controls.Add(this.gbTaskUpgradeNumber, 1, 1);
            this.tlpTask.Controls.Add(this.gbRegion, 1, 2);
            this.tlpTask.Controls.Add(this.gbObjects, 0, 0);
            this.tlpTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTask.Location = new System.Drawing.Point(3, 16);
            this.tlpTask.Name = "tlpTask";
            this.tlpTask.RowCount = 3;
            this.tlpTask.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpTask.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpTask.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpTask.Size = new System.Drawing.Size(386, 323);
            this.tlpTask.TabIndex = 0;
            // 
            // gbTaskIndex
            // 
            this.gbTaskIndex.AutoSize = true;
            this.gbTaskIndex.Controls.Add(this.cbTaskIndex);
            this.gbTaskIndex.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTaskIndex.Location = new System.Drawing.Point(196, 3);
            this.gbTaskIndex.Name = "gbTaskIndex";
            this.gbTaskIndex.Size = new System.Drawing.Size(187, 40);
            this.gbTaskIndex.TabIndex = 0;
            this.gbTaskIndex.TabStop = false;
            this.gbTaskIndex.Text = "Тип:";
            // 
            // cbTaskIndex
            // 
            this.cbTaskIndex.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbTaskIndex.FormattingEnabled = true;
            this.cbTaskIndex.Items.AddRange(new object[] {
            "Размещение в прямоугольной оболочке",
            "Размещение в полубесконечной полосе",
            "Размещение в прямоугольнике"});
            this.cbTaskIndex.Location = new System.Drawing.Point(3, 16);
            this.cbTaskIndex.Name = "cbTaskIndex";
            this.cbTaskIndex.Size = new System.Drawing.Size(181, 21);
            this.cbTaskIndex.TabIndex = 0;
            this.cbTaskIndex.SelectedIndexChanged += new System.EventHandler(this.cbTaskIndex_SelectedIndexChanged);
            // 
            // gbTaskUpgradeNumber
            // 
            this.gbTaskUpgradeNumber.AutoSize = true;
            this.gbTaskUpgradeNumber.Controls.Add(this.nudTaskUpgradeNumber);
            this.gbTaskUpgradeNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTaskUpgradeNumber.Location = new System.Drawing.Point(196, 49);
            this.gbTaskUpgradeNumber.Name = "gbTaskUpgradeNumber";
            this.gbTaskUpgradeNumber.Size = new System.Drawing.Size(187, 39);
            this.gbTaskUpgradeNumber.TabIndex = 1;
            this.gbTaskUpgradeNumber.TabStop = false;
            this.gbTaskUpgradeNumber.Text = "Количество итераций:";
            // 
            // nudTaskUpgradeNumber
            // 
            this.nudTaskUpgradeNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.nudTaskUpgradeNumber.Location = new System.Drawing.Point(3, 16);
            this.nudTaskUpgradeNumber.Name = "nudTaskUpgradeNumber";
            this.nudTaskUpgradeNumber.Size = new System.Drawing.Size(181, 20);
            this.nudTaskUpgradeNumber.TabIndex = 0;
            this.nudTaskUpgradeNumber.ValueChanged += new System.EventHandler(this.nudTaskUpgradeNumber_ValueChanged);
            // 
            // gbRegion
            // 
            this.gbRegion.AutoSize = true;
            this.gbRegion.Controls.Add(this.gbRegionWidth);
            this.gbRegion.Controls.Add(this.gbRegionHeight);
            this.gbRegion.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRegion.Location = new System.Drawing.Point(196, 94);
            this.gbRegion.Name = "gbRegion";
            this.gbRegion.Size = new System.Drawing.Size(187, 97);
            this.gbRegion.TabIndex = 2;
            this.gbRegion.TabStop = false;
            this.gbRegion.Text = "Область размещения:";
            // 
            // gbRegionWidth
            // 
            this.gbRegionWidth.AutoSize = true;
            this.gbRegionWidth.Controls.Add(this.tbRegionWidth);
            this.gbRegionWidth.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRegionWidth.Location = new System.Drawing.Point(3, 55);
            this.gbRegionWidth.Name = "gbRegionWidth";
            this.gbRegionWidth.Size = new System.Drawing.Size(181, 39);
            this.gbRegionWidth.TabIndex = 1;
            this.gbRegionWidth.TabStop = false;
            this.gbRegionWidth.Text = "Ширина:";
            // 
            // tbRegionWidth
            // 
            this.tbRegionWidth.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbRegionWidth.Location = new System.Drawing.Point(3, 16);
            this.tbRegionWidth.Name = "tbRegionWidth";
            this.tbRegionWidth.Size = new System.Drawing.Size(175, 20);
            this.tbRegionWidth.TabIndex = 0;
            this.tbRegionWidth.Text = "0";
            this.tbRegionWidth.TextChanged += new System.EventHandler(this.tbRegionWidth_TextChanged);
            // 
            // gbRegionHeight
            // 
            this.gbRegionHeight.AutoSize = true;
            this.gbRegionHeight.Controls.Add(this.tbRegionHeight);
            this.gbRegionHeight.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRegionHeight.Location = new System.Drawing.Point(3, 16);
            this.gbRegionHeight.Name = "gbRegionHeight";
            this.gbRegionHeight.Size = new System.Drawing.Size(181, 39);
            this.gbRegionHeight.TabIndex = 0;
            this.gbRegionHeight.TabStop = false;
            this.gbRegionHeight.Text = "Высота:";
            // 
            // tbRegionHeight
            // 
            this.tbRegionHeight.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbRegionHeight.Location = new System.Drawing.Point(3, 16);
            this.tbRegionHeight.Name = "tbRegionHeight";
            this.tbRegionHeight.Size = new System.Drawing.Size(175, 20);
            this.tbRegionHeight.TabIndex = 0;
            this.tbRegionHeight.Text = "0";
            this.tbRegionHeight.TextChanged += new System.EventHandler(this.tbRegionHeight_TextChanged);
            // 
            // gbObjects
            // 
            this.gbObjects.AutoSize = true;
            this.gbObjects.Controls.Add(this.dgvObjects);
            this.gbObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbObjects.Location = new System.Drawing.Point(3, 3);
            this.gbObjects.Name = "gbObjects";
            this.tlpTask.SetRowSpan(this.gbObjects, 3);
            this.gbObjects.Size = new System.Drawing.Size(187, 317);
            this.gbObjects.TabIndex = 3;
            this.gbObjects.TabStop = false;
            this.gbObjects.Text = "Объекты размещения:";
            // 
            // dgvObjects
            // 
            this.dgvObjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObjects.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvObjects.Location = new System.Drawing.Point(3, 16);
            this.dgvObjects.Name = "dgvObjects";
            this.dgvObjects.Size = new System.Drawing.Size(181, 298);
            this.dgvObjects.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Задача|*.task";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Задача|*.task";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 366);
            this.Controls.Add(this.gbTask);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Размещение прямоугольников";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTask_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbTask.ResumeLayout(false);
            this.tlpTask.ResumeLayout(false);
            this.tlpTask.PerformLayout();
            this.gbTaskIndex.ResumeLayout(false);
            this.gbTaskUpgradeNumber.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskUpgradeNumber)).EndInit();
            this.gbRegion.ResumeLayout(false);
            this.gbRegion.PerformLayout();
            this.gbRegionWidth.ResumeLayout(false);
            this.gbRegionWidth.PerformLayout();
            this.gbRegionHeight.ResumeLayout(false);
            this.gbRegionHeight.PerformLayout();
            this.gbObjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjects)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miTask;
        private System.Windows.Forms.GroupBox gbTask;
        private System.Windows.Forms.TableLayoutPanel tlpTask;
        private System.Windows.Forms.GroupBox gbTaskIndex;
        private System.Windows.Forms.ComboBox cbTaskIndex;
        private System.Windows.Forms.GroupBox gbTaskUpgradeNumber;
        private System.Windows.Forms.NumericUpDown nudTaskUpgradeNumber;
        private System.Windows.Forms.GroupBox gbRegion;
        private System.Windows.Forms.GroupBox gbRegionWidth;
        private System.Windows.Forms.TextBox tbRegionWidth;
        private System.Windows.Forms.GroupBox gbRegionHeight;
        private System.Windows.Forms.TextBox tbRegionHeight;
        private System.Windows.Forms.GroupBox gbObjects;
        private System.Windows.Forms.DataGridView dgvObjects;
        private System.Windows.Forms.ToolStripMenuItem miWorkWithObjects;
        private System.Windows.Forms.ToolStripMenuItem miAddObjectLast;
        private System.Windows.Forms.ToolStripMenuItem miAddObjectByIndex;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem miDelObjectLast;
        private System.Windows.Forms.ToolStripMenuItem miDelObjectByIndex;
        private System.Windows.Forms.ToolStripMenuItem miAddObjects;
        private System.Windows.Forms.ToolStripMenuItem miAddObjectsFromFile;
        private System.Windows.Forms.ToolStripMenuItem miDelObjects;
        private System.Windows.Forms.ToolStripMenuItem miDelObjectsAll;
        private System.Windows.Forms.ToolStripMenuItem miLoadTask;
        private System.Windows.Forms.ToolStripMenuItem miSaveTask;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem miClose;
        private System.Windows.Forms.ToolStripMenuItem miOptions;
        private System.Windows.Forms.ToolStripMenuItem miTaskIndex;
        private System.Windows.Forms.ToolStripMenuItem miTaskUpgradeNumber;
        private System.Windows.Forms.ToolStripMenuItem miRegion;
        private System.Windows.Forms.ToolStripMenuItem miObjects;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miPlacementOpt;
        private System.Windows.Forms.ToolStripMenuItem miPlacementLast;
        private System.Windows.Forms.ToolStripMenuItem miRealization;
        private System.Windows.Forms.ToolStripMenuItem miStep;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miSpeed;
        private System.Windows.Forms.ToolStripMenuItem miStart;
        private System.Windows.Forms.ToolStripMenuItem miStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem miSteps;
        private System.Windows.Forms.ToolStripMenuItem miStatistic;
    }
}

