namespace Lab4_23
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
            this.components = new System.ComponentModel.Container();

            this.lblTitle = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.btnComp = new System.Windows.Forms.Button();
            this.btnReach = new System.Windows.Forms.Button();
            this.lblSectionAnalysis = new System.Windows.Forms.Label();
            this.btnDfs = new System.Windows.Forms.Button();
            this.btnBfs = new System.Windows.Forms.Button();
            this.lblSectionTraversal = new System.Windows.Forms.Label();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.panelInput = new System.Windows.Forms.Panel();
            this.cmbEnd = new System.Windows.Forms.ComboBox();
            this.lblEndVertex = new System.Windows.Forms.Label();
            this.cmbStart = new System.Windows.Forms.ComboBox();
            this.lblStartVertex = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.lblStatus = new System.Windows.Forms.Label();

            this.panelButtons.SuspendLayout();
            this.panelInput.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(60)))), ((int)(((byte)(100)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Граф энергосистемы — Вариант 23";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.White;
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButtons.Controls.Add(this.btnClear);
            this.panelButtons.Controls.Add(this.lblSeparator);
            this.panelButtons.Controls.Add(this.btnComp);
            this.panelButtons.Controls.Add(this.btnReach);
            this.panelButtons.Controls.Add(this.lblSectionAnalysis);
            this.panelButtons.Controls.Add(this.btnDfs);
            this.panelButtons.Controls.Add(this.btnBfs);
            this.panelButtons.Controls.Add(this.lblSectionTraversal);
            this.panelButtons.Controls.Add(this.btnInfo);
            this.panelButtons.Controls.Add(this.btnLoad);
            this.panelButtons.Location = new System.Drawing.Point(12, 50);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(220, 590);
            this.panelButtons.TabIndex = 1;
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Location = new System.Drawing.Point(8, 10);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(200, 38);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Загрузить граф";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.FlatAppearance.BorderSize = 0;
            this.btnInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInfo.ForeColor = System.Drawing.Color.White;
            this.btnInfo.Location = new System.Drawing.Point(8, 56);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(200, 38);
            this.btnInfo.TabIndex = 1;
            this.btnInfo.Text = "Информация о графе";
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // lblSectionTraversal
            // 
            this.lblSectionTraversal.AutoSize = true;
            this.lblSectionTraversal.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSectionTraversal.ForeColor = System.Drawing.Color.Gray;
            this.lblSectionTraversal.Location = new System.Drawing.Point(8, 104);
            this.lblSectionTraversal.Name = "lblSectionTraversal";
            this.lblSectionTraversal.Size = new System.Drawing.Size(80, 13);
            this.lblSectionTraversal.TabIndex = 2;
            this.lblSectionTraversal.Text = "── Обходы ──";
            // 
            // btnBfs
            // 
            this.btnBfs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnBfs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBfs.FlatAppearance.BorderSize = 0;
            this.btnBfs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBfs.ForeColor = System.Drawing.Color.White;
            this.btnBfs.Location = new System.Drawing.Point(8, 124);
            this.btnBfs.Name = "btnBfs";
            this.btnBfs.Size = new System.Drawing.Size(200, 38);
            this.btnBfs.TabIndex = 3;
            this.btnBfs.Text = "BFS (обход в ширину)";
            this.btnBfs.UseVisualStyleBackColor = false;
            this.btnBfs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBfs.Click += new System.EventHandler(this.btnBfs_Click);
            // 
            // btnDfs
            // 
            this.btnDfs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnDfs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDfs.FlatAppearance.BorderSize = 0;
            this.btnDfs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDfs.ForeColor = System.Drawing.Color.White;
            this.btnDfs.Location = new System.Drawing.Point(8, 170);
            this.btnDfs.Name = "btnDfs";
            this.btnDfs.Size = new System.Drawing.Size(200, 38);
            this.btnDfs.TabIndex = 4;
            this.btnDfs.Text = "DFS (обход в глубину)";
            this.btnDfs.UseVisualStyleBackColor = false;
            this.btnDfs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDfs.Click += new System.EventHandler(this.btnDfs_Click);
            // 
            // lblSectionAnalysis
            // 
            this.lblSectionAnalysis.AutoSize = true;
            this.lblSectionAnalysis.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSectionAnalysis.ForeColor = System.Drawing.Color.Gray;
            this.lblSectionAnalysis.Location = new System.Drawing.Point(8, 218);
            this.lblSectionAnalysis.Name = "lblSectionAnalysis";
            this.lblSectionAnalysis.Size = new System.Drawing.Size(80, 13);
            this.lblSectionAnalysis.TabIndex = 5;
            this.lblSectionAnalysis.Text = "── Анализ ──";
            // 
            // btnReach
            // 
            this.btnReach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnReach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReach.FlatAppearance.BorderSize = 0;
            this.btnReach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnReach.ForeColor = System.Drawing.Color.White;
            this.btnReach.Location = new System.Drawing.Point(8, 238);
            this.btnReach.Name = "btnReach";
            this.btnReach.Size = new System.Drawing.Size(200, 38);
            this.btnReach.TabIndex = 6;
            this.btnReach.Text = "Достижимость (BFS)";
            this.btnReach.UseVisualStyleBackColor = false;
            this.btnReach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReach.Click += new System.EventHandler(this.btnReach_Click);
            // 
            // btnComp
            // 
            this.btnComp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.btnComp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComp.FlatAppearance.BorderSize = 0;
            this.btnComp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnComp.ForeColor = System.Drawing.Color.White;
            this.btnComp.Location = new System.Drawing.Point(8, 284);
            this.btnComp.Name = "btnComp";
            this.btnComp.Size = new System.Drawing.Size(200, 38);
            this.btnComp.TabIndex = 7;
            this.btnComp.Text = "Компоненты связности";
            this.btnComp.UseVisualStyleBackColor = false;
            this.btnComp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnComp.Click += new System.EventHandler(this.btnComp_Click);
            // 
            // lblSeparator
            // 
            this.lblSeparator.AutoSize = true;
            this.lblSeparator.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSeparator.ForeColor = System.Drawing.Color.Gray;
            this.lblSeparator.Location = new System.Drawing.Point(8, 332);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(100, 13);
            this.lblSeparator.TabIndex = 8;
            this.lblSeparator.Text = "──────────────";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(8, 352);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(200, 38);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Очистить вывод";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelInput
            // 
            this.panelInput.BackColor = System.Drawing.Color.White;
            this.panelInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInput.Controls.Add(this.cmbEnd);
            this.panelInput.Controls.Add(this.lblEndVertex);
            this.panelInput.Controls.Add(this.cmbStart);
            this.panelInput.Controls.Add(this.lblStartVertex);
            this.panelInput.Location = new System.Drawing.Point(244, 50);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(730, 50);
            this.panelInput.TabIndex = 2;
            // 
            // lblStartVertex
            // 
            this.lblStartVertex.AutoSize = true;
            this.lblStartVertex.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStartVertex.Location = new System.Drawing.Point(8, 14);
            this.lblStartVertex.Name = "lblStartVertex";
            this.lblStartVertex.Size = new System.Drawing.Size(130, 15);
            this.lblStartVertex.TabIndex = 0;
            this.lblStartVertex.Text = "Начальная вершина:";
            // 
            // cmbStart
            // 
            this.cmbStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbStart.FormattingEnabled = true;
            this.cmbStart.Location = new System.Drawing.Point(150, 11);
            this.cmbStart.Name = "cmbStart";
            this.cmbStart.Size = new System.Drawing.Size(230, 23);
            this.cmbStart.TabIndex = 1;
            // 
            // lblEndVertex
            // 
            this.lblEndVertex.AutoSize = true;
            this.lblEndVertex.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEndVertex.Location = new System.Drawing.Point(400, 14);
            this.lblEndVertex.Name = "lblEndVertex";
            this.lblEndVertex.Size = new System.Drawing.Size(125, 15);
            this.lblEndVertex.TabIndex = 2;
            this.lblEndVertex.Text = "Конечная вершина:";
            // 
            // cmbEnd
            // 
            this.cmbEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbEnd.FormattingEnabled = true;
            this.cmbEnd.Location = new System.Drawing.Point(550, 11);
            this.cmbEnd.Name = "cmbEnd";
            this.cmbEnd.Size = new System.Drawing.Size(170, 23);
            this.cmbEnd.TabIndex = 3;
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtOutput.Location = new System.Drawing.Point(244, 112);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(730, 510);
            this.txtOutput.TabIndex = 3;
            this.txtOutput.Text = "";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblStatus.Location = new System.Drawing.Point(244, 628);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(730, 18);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Граф не загружен. Нажмите «Загрузить граф».";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(988, 651);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лабораторная работа №4 — Граф энергосистемы (вариант 23)";

            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Label lblSectionTraversal;
        private System.Windows.Forms.Button btnBfs;
        private System.Windows.Forms.Button btnDfs;
        private System.Windows.Forms.Label lblSectionAnalysis;
        private System.Windows.Forms.Button btnReach;
        private System.Windows.Forms.Button btnComp;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Label lblStartVertex;
        private System.Windows.Forms.ComboBox cmbStart;
        private System.Windows.Forms.Label lblEndVertex;
        private System.Windows.Forms.ComboBox cmbEnd;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Label lblStatus;
    }
}
