namespace iwm_DirOnlyCopy
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.BtnInput = new System.Windows.Forms.Button();
			this.BtnOutput = new System.Windows.Forms.Button();
			this.TbInput = new System.Windows.Forms.TextBox();
			this.CmsPath = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CmsPath_クリア = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.CmsPath_コピー = new System.Windows.Forms.ToolStripMenuItem();
			this.CmsPath_貼り付け = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.CmsPath_カーソルを先頭に移動 = new System.Windows.Forms.ToolStripMenuItem();
			this.CmsPath_カーソルを末尾に移動 = new System.Windows.Forms.ToolStripMenuItem();
			this.TbOutput = new System.Windows.Forms.TextBox();
			this.Lbl1 = new System.Windows.Forms.Label();
			this.Lbl3 = new System.Windows.Forms.Label();
			this.BtnExec = new System.Windows.Forms.Button();
			this.Lbl2 = new System.Windows.Forms.Label();
			this.CbDepth = new System.Windows.Forms.ComboBox();
			this.CmsDepth = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CmsDepth_上へ = new System.Windows.Forms.ToolStripMenuItem();
			this.CmsDepth_下へ = new System.Windows.Forms.ToolStripMenuItem();
			this.BtnTest = new System.Windows.Forms.Button();
			this.LblResult = new System.Windows.Forms.Label();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.CmsPath.SuspendLayout();
			this.CmsDepth.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnInput
			// 
			this.BtnInput.BackColor = System.Drawing.Color.DimGray;
			this.BtnInput.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
			this.BtnInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnInput.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.BtnInput.ForeColor = System.Drawing.Color.White;
			this.BtnInput.Location = new System.Drawing.Point(9, 10);
			this.BtnInput.Margin = new System.Windows.Forms.Padding(0);
			this.BtnInput.Name = "BtnInput";
			this.BtnInput.Size = new System.Drawing.Size(40, 20);
			this.BtnInput.TabIndex = 1;
			this.BtnInput.Text = "入力";
			this.BtnInput.UseVisualStyleBackColor = false;
			this.BtnInput.Click += new System.EventHandler(this.BtnInput_Click);
			// 
			// BtnOutput
			// 
			this.BtnOutput.BackColor = System.Drawing.Color.DimGray;
			this.BtnOutput.FlatAppearance.BorderColor = System.Drawing.Color.Red;
			this.BtnOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnOutput.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.BtnOutput.ForeColor = System.Drawing.Color.White;
			this.BtnOutput.Location = new System.Drawing.Point(9, 68);
			this.BtnOutput.Margin = new System.Windows.Forms.Padding(0);
			this.BtnOutput.Name = "BtnOutput";
			this.BtnOutput.Size = new System.Drawing.Size(40, 20);
			this.BtnOutput.TabIndex = 4;
			this.BtnOutput.Text = "出力";
			this.BtnOutput.UseVisualStyleBackColor = false;
			this.BtnOutput.Click += new System.EventHandler(this.BtnOutput_Click);
			// 
			// TbInput
			// 
			this.TbInput.AllowDrop = true;
			this.TbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.TbInput.BackColor = System.Drawing.Color.Black;
			this.TbInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TbInput.ContextMenuStrip = this.CmsPath;
			this.TbInput.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.TbInput.ForeColor = System.Drawing.Color.Lime;
			this.TbInput.Location = new System.Drawing.Point(50, 10);
			this.TbInput.Margin = new System.Windows.Forms.Padding(0);
			this.TbInput.Name = "TbInput";
			this.TbInput.Size = new System.Drawing.Size(245, 20);
			this.TbInput.TabIndex = 2;
			this.TbInput.WordWrap = false;
			this.TbInput.TextChanged += new System.EventHandler(this.TbInput_TextChanged);
			this.TbInput.DragDrop += new System.Windows.Forms.DragEventHandler(this.TbInput_DragDrop);
			this.TbInput.DragEnter += new System.Windows.Forms.DragEventHandler(this.TbInput_DragEnter);
			this.TbInput.Enter += new System.EventHandler(this.TbInput_Enter);
			this.TbInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbInput_KeyPress);
			this.TbInput.MouseEnter += new System.EventHandler(this.TbInput_Enter);
			// 
			// CmsPath
			// 
			this.CmsPath.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.CmsPath_クリア,
			this.toolStripSeparator1,
			this.CmsPath_コピー,
			this.CmsPath_貼り付け,
			this.toolStripSeparator2,
			this.CmsPath_カーソルを先頭に移動,
			this.CmsPath_カーソルを末尾に移動});
			this.CmsPath.Name = "contextMenuStrip1";
			this.CmsPath.Size = new System.Drawing.Size(177, 126);
			// 
			// CmsPath_クリア
			// 
			this.CmsPath_クリア.Image = ((System.Drawing.Image)(resources.GetObject("CmsPath_クリア.Image")));
			this.CmsPath_クリア.Name = "CmsPath_クリア";
			this.CmsPath_クリア.Size = new System.Drawing.Size(176, 22);
			this.CmsPath_クリア.Text = "クリア";
			this.CmsPath_クリア.Click += new System.EventHandler(this.CmsPath_クリア_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
			// 
			// CmsPath_コピー
			// 
			this.CmsPath_コピー.Image = ((System.Drawing.Image)(resources.GetObject("CmsPath_コピー.Image")));
			this.CmsPath_コピー.Name = "CmsPath_コピー";
			this.CmsPath_コピー.Size = new System.Drawing.Size(176, 22);
			this.CmsPath_コピー.Text = "コピー";
			this.CmsPath_コピー.Click += new System.EventHandler(this.CmsPath_コピー_Click);
			// 
			// CmsPath_貼り付け
			// 
			this.CmsPath_貼り付け.Image = ((System.Drawing.Image)(resources.GetObject("CmsPath_貼り付け.Image")));
			this.CmsPath_貼り付け.Name = "CmsPath_貼り付け";
			this.CmsPath_貼り付け.Size = new System.Drawing.Size(176, 22);
			this.CmsPath_貼り付け.Text = "貼り付け";
			this.CmsPath_貼り付け.Click += new System.EventHandler(this.CmsPath_貼り付け_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
			// 
			// CmsPath_カーソルを先頭に移動
			// 
			this.CmsPath_カーソルを先頭に移動.Image = ((System.Drawing.Image)(resources.GetObject("CmsPath_カーソルを先頭に移動.Image")));
			this.CmsPath_カーソルを先頭に移動.Name = "CmsPath_カーソルを先頭に移動";
			this.CmsPath_カーソルを先頭に移動.Size = new System.Drawing.Size(176, 22);
			this.CmsPath_カーソルを先頭に移動.Text = "カーソルを先頭に移動";
			this.CmsPath_カーソルを先頭に移動.Click += new System.EventHandler(this.CmsPath_カーソルを先頭に移動_Click);
			// 
			// CmsPath_カーソルを末尾に移動
			// 
			this.CmsPath_カーソルを末尾に移動.Image = ((System.Drawing.Image)(resources.GetObject("CmsPath_カーソルを末尾に移動.Image")));
			this.CmsPath_カーソルを末尾に移動.Name = "CmsPath_カーソルを末尾に移動";
			this.CmsPath_カーソルを末尾に移動.Size = new System.Drawing.Size(176, 22);
			this.CmsPath_カーソルを末尾に移動.Text = "カーソルを末尾に移動";
			this.CmsPath_カーソルを末尾に移動.Click += new System.EventHandler(this.CmsPath_カーソルを末尾に移動_Click);
			// 
			// TbOutput
			// 
			this.TbOutput.AllowDrop = true;
			this.TbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.TbOutput.BackColor = System.Drawing.Color.Black;
			this.TbOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TbOutput.ContextMenuStrip = this.CmsPath;
			this.TbOutput.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.TbOutput.ForeColor = System.Drawing.Color.Lime;
			this.TbOutput.Location = new System.Drawing.Point(50, 68);
			this.TbOutput.Margin = new System.Windows.Forms.Padding(0);
			this.TbOutput.Name = "TbOutput";
			this.TbOutput.Size = new System.Drawing.Size(245, 20);
			this.TbOutput.TabIndex = 5;
			this.TbOutput.WordWrap = false;
			this.TbOutput.TextChanged += new System.EventHandler(this.TbOutput_TextChanged);
			this.TbOutput.DragDrop += new System.Windows.Forms.DragEventHandler(this.TbOutput_DragDrop);
			this.TbOutput.DragEnter += new System.Windows.Forms.DragEventHandler(this.TbOutput_DragEnter);
			this.TbOutput.Enter += new System.EventHandler(this.TbOutput_Enter);
			this.TbOutput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbOutput_KeyPress);
			this.TbOutput.MouseEnter += new System.EventHandler(this.TbOutput_Enter);
			// 
			// Lbl1
			// 
			this.Lbl1.AutoSize = true;
			this.Lbl1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Lbl1.ForeColor = System.Drawing.Color.White;
			this.Lbl1.Location = new System.Drawing.Point(15, 43);
			this.Lbl1.Margin = new System.Windows.Forms.Padding(0);
			this.Lbl1.Name = "Lbl1";
			this.Lbl1.Size = new System.Drawing.Size(91, 13);
			this.Lbl1.TabIndex = 0;
			this.Lbl1.Text = "以下のフォルダ";
			// 
			// Lbl3
			// 
			this.Lbl3.AutoSize = true;
			this.Lbl3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Lbl3.ForeColor = System.Drawing.Color.White;
			this.Lbl3.Location = new System.Drawing.Point(15, 100);
			this.Lbl3.Margin = new System.Windows.Forms.Padding(0);
			this.Lbl3.Name = "Lbl3";
			this.Lbl3.Size = new System.Drawing.Size(88, 13);
			this.Lbl3.TabIndex = 0;
			this.Lbl3.Text = "に作成します。";
			// 
			// BtnExec
			// 
			this.BtnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExec.BackColor = System.Drawing.Color.Crimson;
			this.BtnExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExec.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.BtnExec.ForeColor = System.Drawing.Color.White;
			this.BtnExec.Location = new System.Drawing.Point(234, 120);
			this.BtnExec.Margin = new System.Windows.Forms.Padding(0);
			this.BtnExec.Name = "BtnExec";
			this.BtnExec.Size = new System.Drawing.Size(60, 22);
			this.BtnExec.TabIndex = 7;
			this.BtnExec.Text = "実行";
			this.ToolTip1.SetToolTip(this.BtnExec, "実際にDir作成");
			this.BtnExec.UseVisualStyleBackColor = false;
			this.BtnExec.Click += new System.EventHandler(this.BtnExec_Click);
			// 
			// Lbl2
			// 
			this.Lbl2.AutoSize = true;
			this.Lbl2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Lbl2.ForeColor = System.Drawing.Color.White;
			this.Lbl2.Location = new System.Drawing.Point(154, 43);
			this.Lbl2.Margin = new System.Windows.Forms.Padding(0);
			this.Lbl2.Name = "Lbl2";
			this.Lbl2.Size = new System.Drawing.Size(52, 13);
			this.Lbl2.TabIndex = 0;
			this.Lbl2.Text = "階層を、";
			// 
			// CbDepth
			// 
			this.CbDepth.BackColor = System.Drawing.Color.DimGray;
			this.CbDepth.ContextMenuStrip = this.CmsDepth;
			this.CbDepth.DropDownHeight = 130;
			this.CbDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbDepth.DropDownWidth = 50;
			this.CbDepth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CbDepth.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.CbDepth.ForeColor = System.Drawing.Color.White;
			this.CbDepth.FormattingEnabled = true;
			this.CbDepth.IntegralHeight = false;
			this.CbDepth.ItemHeight = 12;
			this.CbDepth.Location = new System.Drawing.Point(107, 39);
			this.CbDepth.Margin = new System.Windows.Forms.Padding(0);
			this.CbDepth.Name = "CbDepth";
			this.CbDepth.Size = new System.Drawing.Size(45, 20);
			this.CbDepth.TabIndex = 3;
			// 
			// CmsDepth
			// 
			this.CmsDepth.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.CmsDepth_上へ,
			this.CmsDepth_下へ});
			this.CmsDepth.Name = "contextMenuStrip3";
			this.CmsDepth.Size = new System.Drawing.Size(68, 48);
			// 
			// CmsDepth_上へ
			// 
			this.CmsDepth_上へ.ForeColor = System.Drawing.Color.Black;
			this.CmsDepth_上へ.Image = ((System.Drawing.Image)(resources.GetObject("CmsDepth_上へ.Image")));
			this.CmsDepth_上へ.Name = "CmsDepth_上へ";
			this.CmsDepth_上へ.Size = new System.Drawing.Size(67, 22);
			this.CmsDepth_上へ.Click += new System.EventHandler(this.CmsDepth_上へ_Click);
			// 
			// CmsDepth_下へ
			// 
			this.CmsDepth_下へ.ForeColor = System.Drawing.Color.Black;
			this.CmsDepth_下へ.Image = ((System.Drawing.Image)(resources.GetObject("CmsDepth_下へ.Image")));
			this.CmsDepth_下へ.Name = "CmsDepth_下へ";
			this.CmsDepth_下へ.Size = new System.Drawing.Size(67, 22);
			this.CmsDepth_下へ.Click += new System.EventHandler(this.CmsDepth_下へ_Click);
			// 
			// BtnTest
			// 
			this.BtnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnTest.BackColor = System.Drawing.Color.OrangeRed;
			this.BtnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTest.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.BtnTest.ForeColor = System.Drawing.Color.White;
			this.BtnTest.Location = new System.Drawing.Point(165, 120);
			this.BtnTest.Margin = new System.Windows.Forms.Padding(0);
			this.BtnTest.Name = "BtnTest";
			this.BtnTest.Size = new System.Drawing.Size(60, 22);
			this.BtnTest.TabIndex = 6;
			this.BtnTest.Text = "テスト";
			this.ToolTip1.SetToolTip(this.BtnTest, "作成するDir数を表示／作成はしない");
			this.BtnTest.UseVisualStyleBackColor = false;
			this.BtnTest.Click += new System.EventHandler(this.BtnTest_Click);
			// 
			// LblResult
			// 
			this.LblResult.AutoSize = true;
			this.LblResult.BackColor = System.Drawing.Color.Black;
			this.LblResult.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.LblResult.ForeColor = System.Drawing.Color.Lime;
			this.LblResult.Location = new System.Drawing.Point(9, 124);
			this.LblResult.Margin = new System.Windows.Forms.Padding(0);
			this.LblResult.Name = "LblResult";
			this.LblResult.Padding = new System.Windows.Forms.Padding(1);
			this.LblResult.Size = new System.Drawing.Size(18, 15);
			this.LblResult.TabIndex = 0;
			this.LblResult.Text = "...";
			// 
			// ToolTip1
			// 
			this.ToolTip1.AutomaticDelay = 1000;
			this.ToolTip1.BackColor = System.Drawing.Color.Transparent;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DimGray;
			this.ClientSize = new System.Drawing.Size(304, 151);
			this.Controls.Add(this.LblResult);
			this.Controls.Add(this.BtnExec);
			this.Controls.Add(this.BtnTest);
			this.Controls.Add(this.CbDepth);
			this.Controls.Add(this.TbOutput);
			this.Controls.Add(this.TbInput);
			this.Controls.Add(this.Lbl2);
			this.Controls.Add(this.Lbl3);
			this.Controls.Add(this.Lbl1);
			this.Controls.Add(this.BtnInput);
			this.Controls.Add(this.BtnOutput);
			this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 190);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(320, 190);
			this.Name = "Form1";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ダミータイトル";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.CmsPath.ResumeLayout(false);
			this.CmsDepth.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button BtnInput;
		private System.Windows.Forms.Button BtnOutput;
		private System.Windows.Forms.TextBox TbInput;
		private System.Windows.Forms.TextBox TbOutput;
		private System.Windows.Forms.Label Lbl1;
		private System.Windows.Forms.Label Lbl3;
		private System.Windows.Forms.Button BtnExec;
		private System.Windows.Forms.Label Lbl2;
		private System.Windows.Forms.ComboBox CbDepth;
		private System.Windows.Forms.Button BtnTest;
		private System.Windows.Forms.Label LblResult;
		private System.Windows.Forms.ToolTip ToolTip1;
		private System.Windows.Forms.ContextMenuStrip CmsPath;
		private System.Windows.Forms.ToolStripMenuItem CmsPath_クリア;
		private System.Windows.Forms.ToolStripMenuItem CmsPath_貼り付け;
		private System.Windows.Forms.ContextMenuStrip CmsDepth;
		private System.Windows.Forms.ToolStripMenuItem CmsDepth_上へ;
		private System.Windows.Forms.ToolStripMenuItem CmsDepth_下へ;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem CmsPath_コピー;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem CmsPath_カーソルを先頭に移動;
		private System.Windows.Forms.ToolStripMenuItem CmsPath_カーソルを末尾に移動;
	}
}

