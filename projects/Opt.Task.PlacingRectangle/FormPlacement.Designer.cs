namespace PlacingRectangle
{
    partial class FormPlacement
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
            this.gbPlacement = new System.Windows.Forms.GroupBox();
            this.tlpPlacement = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gbObjectFunction = new System.Windows.Forms.GroupBox();
            this.tbObjectFunction = new System.Windows.Forms.TextBox();
            this.gbVisual = new System.Windows.Forms.GroupBox();
            this.pbVisual = new System.Windows.Forms.PictureBox();
            this.tcObjects = new System.Windows.Forms.TabControl();
            this.tpObjects = new System.Windows.Forms.TabPage();
            this.dgvObjects = new System.Windows.Forms.DataGridView();
            this.tpObjectsBusy = new System.Windows.Forms.TabPage();
            this.dgvObjectsPlaced = new System.Windows.Forms.DataGridView();
            this.tpObjectsFree = new System.Windows.Forms.TabPage();
            this.dgvObjectsUnplaced = new System.Windows.Forms.DataGridView();
            this.gbPlacement.SuspendLayout();
            this.tlpPlacement.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbObjectFunction.SuspendLayout();
            this.gbVisual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVisual)).BeginInit();
            this.tcObjects.SuspendLayout();
            this.tpObjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjects)).BeginInit();
            this.tpObjectsBusy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectsPlaced)).BeginInit();
            this.tpObjectsFree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectsUnplaced)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPlacement
            // 
            this.gbPlacement.Controls.Add(this.tlpPlacement);
            this.gbPlacement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPlacement.Location = new System.Drawing.Point(0, 0);
            this.gbPlacement.Name = "gbPlacement";
            this.gbPlacement.Size = new System.Drawing.Size(792, 366);
            this.gbPlacement.TabIndex = 1;
            this.gbPlacement.TabStop = false;
            this.gbPlacement.Text = "Размещение:";
            // 
            // tlpPlacement
            // 
            this.tlpPlacement.ColumnCount = 2;
            this.tlpPlacement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpPlacement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tlpPlacement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPlacement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPlacement.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tlpPlacement.Controls.Add(this.tcObjects, 0, 0);
            this.tlpPlacement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPlacement.Location = new System.Drawing.Point(3, 16);
            this.tlpPlacement.Name = "tlpPlacement";
            this.tlpPlacement.RowCount = 1;
            this.tlpPlacement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPlacement.Size = new System.Drawing.Size(786, 347);
            this.tlpPlacement.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.gbObjectFunction, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.gbVisual, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(265, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(518, 341);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // gbObjectFunction
            // 
            this.gbObjectFunction.AutoSize = true;
            this.gbObjectFunction.Controls.Add(this.tbObjectFunction);
            this.gbObjectFunction.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbObjectFunction.Location = new System.Drawing.Point(3, 3);
            this.gbObjectFunction.Name = "gbObjectFunction";
            this.gbObjectFunction.Size = new System.Drawing.Size(512, 39);
            this.gbObjectFunction.TabIndex = 1;
            this.gbObjectFunction.TabStop = false;
            this.gbObjectFunction.Text = "Значение функции цели:";
            // 
            // tbObjectFunction
            // 
            this.tbObjectFunction.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbObjectFunction.Location = new System.Drawing.Point(3, 16);
            this.tbObjectFunction.Name = "tbObjectFunction";
            this.tbObjectFunction.ReadOnly = true;
            this.tbObjectFunction.Size = new System.Drawing.Size(506, 20);
            this.tbObjectFunction.TabIndex = 0;
            // 
            // gbVisual
            // 
            this.gbVisual.Controls.Add(this.pbVisual);
            this.gbVisual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbVisual.Location = new System.Drawing.Point(3, 48);
            this.gbVisual.Name = "gbVisual";
            this.gbVisual.Size = new System.Drawing.Size(512, 290);
            this.gbVisual.TabIndex = 2;
            this.gbVisual.TabStop = false;
            this.gbVisual.Text = "Графическое представление:";
            // 
            // pbVisual
            // 
            this.pbVisual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbVisual.Location = new System.Drawing.Point(3, 16);
            this.pbVisual.Name = "pbVisual";
            this.pbVisual.Size = new System.Drawing.Size(506, 271);
            this.pbVisual.TabIndex = 0;
            this.pbVisual.TabStop = false;
            this.pbVisual.Paint += new System.Windows.Forms.PaintEventHandler(this.pbVisual_Paint);
            this.pbVisual.Resize += new System.EventHandler(this.pbVisual_Resize);
            // 
            // tcObjects
            // 
            this.tcObjects.Controls.Add(this.tpObjects);
            this.tcObjects.Controls.Add(this.tpObjectsBusy);
            this.tcObjects.Controls.Add(this.tpObjectsFree);
            this.tcObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcObjects.Location = new System.Drawing.Point(3, 3);
            this.tcObjects.Name = "tcObjects";
            this.tcObjects.SelectedIndex = 0;
            this.tcObjects.Size = new System.Drawing.Size(256, 341);
            this.tcObjects.TabIndex = 4;
            // 
            // tpObjects
            // 
            this.tpObjects.Controls.Add(this.dgvObjects);
            this.tpObjects.Location = new System.Drawing.Point(4, 22);
            this.tpObjects.Name = "tpObjects";
            this.tpObjects.Padding = new System.Windows.Forms.Padding(3);
            this.tpObjects.Size = new System.Drawing.Size(248, 315);
            this.tpObjects.TabIndex = 0;
            this.tpObjects.Text = "Объекты размещения:";
            this.tpObjects.UseVisualStyleBackColor = true;
            // 
            // dgvObjects
            // 
            this.dgvObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvObjects.Location = new System.Drawing.Point(3, 3);
            this.dgvObjects.Name = "dgvObjects";
            this.dgvObjects.Size = new System.Drawing.Size(242, 309);
            this.dgvObjects.TabIndex = 1;
            // 
            // tpObjectsBusy
            // 
            this.tpObjectsBusy.Controls.Add(this.dgvObjectsPlaced);
            this.tpObjectsBusy.Location = new System.Drawing.Point(4, 22);
            this.tpObjectsBusy.Name = "tpObjectsBusy";
            this.tpObjectsBusy.Padding = new System.Windows.Forms.Padding(3);
            this.tpObjectsBusy.Size = new System.Drawing.Size(247, 315);
            this.tpObjectsBusy.TabIndex = 1;
            this.tpObjectsBusy.Text = "Размещённые объекты:";
            this.tpObjectsBusy.UseVisualStyleBackColor = true;
            // 
            // dgvObjectsPlaced
            // 
            this.dgvObjectsPlaced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObjectsPlaced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvObjectsPlaced.Location = new System.Drawing.Point(3, 3);
            this.dgvObjectsPlaced.Name = "dgvObjectsPlaced";
            this.dgvObjectsPlaced.Size = new System.Drawing.Size(241, 309);
            this.dgvObjectsPlaced.TabIndex = 1;
            // 
            // tpObjectsFree
            // 
            this.tpObjectsFree.Controls.Add(this.dgvObjectsUnplaced);
            this.tpObjectsFree.Location = new System.Drawing.Point(4, 22);
            this.tpObjectsFree.Name = "tpObjectsFree";
            this.tpObjectsFree.Size = new System.Drawing.Size(247, 315);
            this.tpObjectsFree.TabIndex = 2;
            this.tpObjectsFree.Text = "Неразмещённые объекты:";
            this.tpObjectsFree.UseVisualStyleBackColor = true;
            // 
            // dgvObjectsUnplaced
            // 
            this.dgvObjectsUnplaced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObjectsUnplaced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvObjectsUnplaced.Location = new System.Drawing.Point(0, 0);
            this.dgvObjectsUnplaced.Name = "dgvObjectsUnplaced";
            this.dgvObjectsUnplaced.Size = new System.Drawing.Size(247, 291);
            this.dgvObjectsUnplaced.TabIndex = 1;
            // 
            // FormPlacement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 366);
            this.Controls.Add(this.gbPlacement);
            this.Name = "FormPlacement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPlacement";
            this.gbPlacement.ResumeLayout(false);
            this.tlpPlacement.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gbObjectFunction.ResumeLayout(false);
            this.gbObjectFunction.PerformLayout();
            this.gbVisual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbVisual)).EndInit();
            this.tcObjects.ResumeLayout(false);
            this.tpObjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjects)).EndInit();
            this.tpObjectsBusy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectsPlaced)).EndInit();
            this.tpObjectsFree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectsUnplaced)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPlacement;
        private System.Windows.Forms.TableLayoutPanel tlpPlacement;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox gbObjectFunction;
        private System.Windows.Forms.TextBox tbObjectFunction;
        private System.Windows.Forms.GroupBox gbVisual;
        private System.Windows.Forms.PictureBox pbVisual;
        private System.Windows.Forms.TabControl tcObjects;
        private System.Windows.Forms.TabPage tpObjects;
        private System.Windows.Forms.DataGridView dgvObjects;
        private System.Windows.Forms.TabPage tpObjectsBusy;
        private System.Windows.Forms.DataGridView dgvObjectsPlaced;
        private System.Windows.Forms.TabPage tpObjectsFree;
        private System.Windows.Forms.DataGridView dgvObjectsUnplaced;
    }
}