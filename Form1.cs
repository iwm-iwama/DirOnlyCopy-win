using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace iwm_DirOnlyCopy
{
	public partial class Form1: Form
	{
		///private const string COPYRIGHT = "(C)2018-2024 iwm-iwama";
		private const string VERSION = "iwm_DirOnlyCopy_20240605";

		private const string NL = "\r\n";
		private readonly int[] DirLevel = { 1, 260 };
		private const string DirLevelAll = "全";

		private object CurOBJ = null;

		[DllImport("user32.dll")]
		private static extern bool MoveWindow(
			IntPtr hwnd,
			int x,
			int y,
			int nWidth,
			int nHeight,
			int bRepaint
		);

		private Process GblPS = null;

		private readonly string TempFile = Path.Combine(Path.GetTempPath(), Path.GetFileName(Environment.GetCommandLineArgs()[0]) + ".log");

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			StartPosition = FormStartPosition.CenterScreen;
			///StartPosition = FormStartPosition.Manual;
			///Location = new Point(Cursor.Position.X - (Width / 2), Cursor.Position.Y - (SystemInformation.CaptionHeight / 2));

			Text = VERSION;

			_ = CbDepth.Items.Add(DirLevelAll);

			for (int _i1 = DirLevel[0]; _i1 <= DirLevel[1]; _i1++)
			{
				_ = CbDepth.Items.Add(_i1);
			}

			CbDepth.Text = DirLevelAll;

			TbInput.Text = TbOutput.Text = "";
			Sub_BtnExec_Enabled();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				GblPS.Kill();
			}
			catch
			{
			}

			try
			{
				File.Delete(TempFile);
			}
			catch
			{
			}
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			// 表示位置再調整
			TbInput.SelectionStart = 0;
			TbOutput.SelectionStart = 0;
		}

		private void BtnInput_Click(object sender, EventArgs e)
		{
			Sub_Dir_Select(TbInput);
		}

		private void TbInput_Enter(object sender, EventArgs e)
		{
			CurOBJ = TbInput;
		}

		private void TbInput_KeyPress(object sender, KeyPressEventArgs e)
		{
			// ビープ音抑制
			if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Escape)
			{
				e.Handled = true;
			}
		}

		private void TbInput_TextChanged(object sender, EventArgs e)
		{
			ToolTip1.SetToolTip(TbInput, Directory.Exists(TbInput.Text) ? TbInput.Text.Replace("\\", NL) : "存在しないフォルダ");
		}

		private void TbInput_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
		}

		private void TbInput_DragDrop(object sender, DragEventArgs e)
		{
			Sub_TextBox_DragEnter(e, TbInput);
		}

		private void BtnOutput_Click(object sender, EventArgs e)
		{
			Sub_Dir_Select(TbOutput);
		}

		private void TbOutput_Enter(object sender, EventArgs e)
		{
			CurOBJ = TbOutput;
		}

		private void TbOutput_KeyPress(object sender, KeyPressEventArgs e)
		{
			// ビープ音抑制
			if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Escape)
			{
				e.Handled = true;
			}
		}

		private void TbOutput_TextChanged(object sender, EventArgs e)
		{
			Sub_BtnExec_Enabled();
			ToolTip1.SetToolTip(TbOutput, Directory.Exists(TbOutput.Text) ? TbOutput.Text.Replace("\\", NL) : "存在しないフォルダ");
		}

		private void TbOutput_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
		}

		private void TbOutput_DragDrop(object sender, DragEventArgs e)
		{
			Sub_TextBox_DragEnter(e, TbOutput);
		}

		private void Sub_TextBox_DragEnter(DragEventArgs e, TextBox tb)
		{
			string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			tb.Text = Directory.Exists(fileName[0]) ? fileName[0] : Path.GetDirectoryName(fileName[0]);
			tb.SelectionStart = tb.TextLength;
		}

		private bool Rtn_Dir_Format()
		{
			// Dir 整形
			TbInput.Text = Rtn_Dir_WithYen(TbInput.Text);
			TbInput.SelectionStart = TbInput.TextLength;
			TbOutput.Text = Rtn_Dir_WithYen(TbOutput.Text);
			TbOutput.SelectionStart = TbOutput.TextLength;

			// 入力 Dir 不在のとき
			if (!Directory.Exists(TbInput.Text))
			{
				Color foreColorCur = TbInput.ForeColor;
				Color backColorCur = TbInput.BackColor;

				TbInput.ForeColor = Color.White;
				TbInput.BackColor = Color.Red;

				Refresh();
				Thread.Sleep(1000);

				TbInput.ForeColor = foreColorCur;
				TbInput.BackColor = backColorCur;

				return false;
			}

			return true;
		}

		private void BtnTest_Click(object sender, EventArgs e)
		{
			try
			{
				GblPS.Kill();
			}
			catch
			{
			}

			if (!Rtn_Dir_Format())
			{
				return;
			}

			BtnTest.Enabled = false;

			Sub_GblDirList(TbInput.Text, "該当", Color.Orange);
			Thread.Sleep(500);
			LblResult.Visible = false;

			int iCnt = GblDirList.Count;
			if (iCnt > 0)
			{
				// UF-8 NoBOM で書込
				StreamWriter sw = new StreamWriter(TempFile);
				sw.WriteLine($"[{TbInput.Text}]\n[{CbDepth.Text}階層 {iCnt}フォルダ]");
				foreach (string _s1 in GblDirList)
				{
					sw.WriteLine(_s1);
				}
				sw.Close();

				// リスト表示／using()不可
				GblPS = Process.Start("notepad.exe", TempFile);
				_ = MoveWindow(GblPS.MainWindowHandle, 0, 0, 0, 0, 1);
				_ = GblPS.WaitForInputIdle();
				_ = MoveWindow(GblPS.MainWindowHandle, 100, 100, (Screen.PrimaryScreen.Bounds.Width / 3), (Screen.PrimaryScreen.Bounds.Height - 200), 1);
			}

			BtnTest.Enabled = true;
		}

		private void BtnExec_Click(object sender, EventArgs e)
		{
			if (!Rtn_Dir_Format())
			{
				return;
			}

			BtnExec.Enabled = false;

			Sub_GblDirList(TbInput.Text, "作成中...", Color.Lime);

			foreach (string _s1 in GblDirList)
			{
				string _s2 = TbOutput.Text + _s1;
				if (!Directory.Exists(_s2))
				{
					_ = Directory.CreateDirectory(_s2);
				}
			}

			Thread.Sleep(2000);
			LblResult.Visible = false;

			BtnExec.Enabled = true;
		}

		//------------------------
		// Dir 末尾に "\" を付与
		//------------------------
		private string Rtn_Dir_WithYen(String path)
		{
			path = path.Trim().TrimEnd('\\');

			if (path.Length == 0)
			{
				return "";
			}

			//【重要】"\" で階層検査
			// "C:" "D:" 等
			if (path.Length >= 2 && path.Substring(1, 1) == ":" && Directory.Exists(path))
			{
				path += "\\";
			}
			else if (Directory.Exists(path))
			{
				path += "\\";
			}

			return path;
		}

		//-------------------------------
		// [実行]ボタンを使用可否にする
		//-------------------------------
		private void Sub_BtnExec_Enabled()
		{
			LblResult.Visible = false;
			BtnExec.Enabled = Directory.Exists(TbInput.Text) && Directory.Exists(TbOutput.Text);
		}

		//------------------------------
		// 該当 Dir 数とコメントを表示
		//------------------------------
		private void Sub_GblDirList(string path, string addText, Color addTextColor)
		{
			Cursor = Cursors.WaitCursor;
			LblResult.Enabled = false;

			Sub_DirList_Init();
			Sub_DirList(path);
			GblDirList.Sort();

			LblResult.Visible = true;
			LblResult.Text = GblDirList.Count + " フォルダ " + addText;
			LblResult.ForeColor = addTextColor;

			LblResult.Enabled = true;
			Cursor = Cursors.Default;
		}

		//-------------
		// Dir リスト
		//-------------
		private readonly List<string> GblDirList = new List<string>();

		// 除外する Dir長
		private int GblDirListBaseLen = 0;

		// Dir 最大深さ
		private int GblDirListLevelMax = 0;

		// 初期化
		private void Sub_DirList_Init()
		{
			GblDirList.Clear();
			GblDirListBaseLen = TbInput.Text.Length;
			GblDirListLevelMax = Rtn_Serch_Char_Cnt(TbInput.Text, '\\') - 1 + (CbDepth.Text == DirLevelAll ? DirLevel[1] : int.Parse(CbDepth.Text));
		}

		// 再帰
		private void Sub_DirList(string path)
		{
			try
			{
				DirectoryInfo dirInfo = new DirectoryInfo(path);

				// 子
				foreach (DirectoryInfo DI in dirInfo.EnumerateDirectories("*"))
				{
					if (GblDirListLevelMax >= Rtn_Serch_Char_Cnt(DI.FullName, '\\'))
					{
						GblDirList.Add(DI.FullName.Substring(GblDirListBaseLen));
						Sub_DirList(DI.FullName);
					}
				}
			}
			catch
			{
			}
		}

		//------------
		// Char 検索
		//------------
		private int Rtn_Serch_Char_Cnt(string s, char c)
		{
			int rtn = 0;
			foreach (char _c1 in s.ToCharArray())
			{
				if (_c1 == c)
				{
					++rtn;
				}
			}
			return rtn;
		}

		//-----------
		// Dir 選択
		//-----------
		private void Sub_Dir_Select(TextBox tb)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog()
			{
				Description = "フォルダを指定してください。",
				RootFolder = Environment.SpecialFolder.MyComputer,
				SelectedPath = tb.Text,
				ShowNewFolderButton = true
			};

			if (fbd.ShowDialog(this) == DialogResult.OK)
			{
				tb.Text = fbd.SelectedPath;
				tb.SelectionStart = tb.TextLength;
				_ = tb.Focus();
			}
		}

		//----------
		// CmsPath
		//----------
		private void CmsPath_クリア_Click(object sender, EventArgs e)
		{
			switch (CurOBJ)
			{
				case TextBox tb:
					tb.Text = "";
					_ = tb.Focus();
					break;
			}
		}

		private void CmsPath_コピー_Click(object sender, EventArgs e)
		{
			switch (CurOBJ)
			{
				case TextBox tb:
					tb.Copy();
					break;
			}
		}

		private void CmsPath_ペースト_Click(object sender, EventArgs e)
		{
			switch (CurOBJ)
			{
				case TextBox tb:
					tb.Paste();
					break;
			}
		}

		private static class Program
		{
			/// <summary>
			/// アプリケーションのメイン エントリ ポイントです。
			/// </summary>
			[STAThread]
			private static void Main()
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Form1());
			}
		}

		//--------------------------------------------------------------------------------
		// Debug
		//--------------------------------------------------------------------------------
		private void D(object obj, [CallerLineNumber] int line = 0)
		{
			_ = MessageBox.Show(
				$"L{line}:\n    {obj}",
				AppDomain.CurrentDomain.FriendlyName
			);
		}

		//--------------------------------------------------------------------------------
		// MessageBox
		//--------------------------------------------------------------------------------
		private void M(object obj)
		{
			_ = MessageBox.Show(
				obj.ToString(),
				AppDomain.CurrentDomain.FriendlyName
			);
		}
	}
}
