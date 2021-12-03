using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace iwm_DirOnlyCopy
{
	public partial class Form1 : Form
	{
		private const string ProgramID = "フォルダ構成をコピー iwm20211203";

		private const string NL = "\r\n";
		private readonly int[] DirLevel = { 1, 260 };

		private object CurOBJ = null;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			StartPosition = FormStartPosition.Manual;
			SubFormStartPosition();

			Text = ProgramID;

			for (int _i1 = DirLevel[0]; _i1 <= DirLevel[1]; _i1++)
			{
				_ = CbDepth.Items.Add(_i1);
			}
			CbDepth.Text = DirLevel[0].ToString();

			TbInput.Text = TbOutput.Text = "";
			SubBtnExecCtrl();

			CmsDepth_上へ.Text = DirLevel[0].ToString();
			CmsDepth_下へ.Text = DirLevel[1].ToString();
		}

		private readonly string TempFile = Path.Combine(Path.GetTempPath(), Path.GetFileName(Environment.GetCommandLineArgs()[0]) + ".log");
		private Process P = null;

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				P.Kill();
			}
			catch
			{
			}
			File.Delete(TempFile);
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			// 表示位置再調整
			TbInput.SelectionStart = 0;
			TbOutput.SelectionStart = 0;
		}

		private void SubFormStartPosition()
		{
			int WorkingAreaW = Screen.PrimaryScreen.WorkingArea.Width;
			int WorkingAreaH = Screen.PrimaryScreen.WorkingArea.Height;

			int WorkingAreaX = Screen.PrimaryScreen.WorkingArea.X;
			int WorkingAreaY = Screen.PrimaryScreen.WorkingArea.Y;

			int MouseX = Cursor.Position.X;
			int MouseY = Cursor.Position.Y;

			// X = Width
			if (WorkingAreaW < MouseX + Size.Width)
			{
				MouseX -= Size.Width;
				if (MouseX < 0)
				{
					MouseX = WorkingAreaX + 10;
				}
			}

			// Y = Height
			if (WorkingAreaH < MouseY + Size.Height)
			{
				MouseY -= Size.Height;
				if (MouseY < 0)
				{
					MouseY = WorkingAreaY + 10;
				}
			}

			Location = new Point(MouseX, MouseY);
		}

		private void BtnInput_Click(object sender, EventArgs e)
		{
			SubDirSelect(TbInput);
			SubBtnExecCtrl();
		}

		private void TbInput_Enter(object sender, EventArgs e)
		{
			_ = TbInput.Focus();
			CurOBJ = TbInput;
			ToolTip1.SetToolTip(TbInput, Directory.Exists(TbInput.Text) ? TbInput.Text.Replace("\\", NL) : "存在しないフォルダ");
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
			SubBtnExecCtrl();
		}

		private void TbInput_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
		}

		private void TbInput_DragDrop(object sender, DragEventArgs e)
		{
			SubTextBoxDragEnter(e, TbInput);
		}

		private void CmsDepth_上へ_Click(object sender, EventArgs e)
		{
			CbDepth.Text = DirLevel[0].ToString();
		}

		private void CmsDepth_下へ_Click(object sender, EventArgs e)
		{
			CbDepth.Text = DirLevel[1].ToString();
		}

		private void BtnOutput_Click(object sender, EventArgs e)
		{
			SubDirSelect(TbOutput);
			SubBtnExecCtrl();
		}

		private void TbOutput_Enter(object sender, EventArgs e)
		{
			_ = TbOutput.Focus();
			CurOBJ = TbOutput;
			if (Directory.Exists(TbOutput.Text))
			{
				ToolTip1.SetToolTip(TbOutput, TbOutput.Text.Replace("\\", NL));
			}
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
			SubBtnExecCtrl();
		}

		private void TbOutput_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
		}

		private void TbOutput_DragDrop(object sender, DragEventArgs e)
		{
			SubTextBoxDragEnter(e, TbOutput);
		}

		private void SubTextBoxDragEnter(DragEventArgs e, TextBox tb)
		{
			string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			tb.Text = Directory.Exists(fileName[0]) ? fileName[0] : Path.GetDirectoryName(fileName[0]);
			tb.SelectionStart = tb.TextLength;
			SubBtnExecCtrl();
		}

		private void BtnTest_Click(object sender, EventArgs e)
		{
			try
			{
				P.Kill();
			}
			catch
			{
			}

			if (!RtnbBtnTestExecCheck())
			{
				return;
			}

			BtnTest.Enabled = false;

			int iCnt = RtnBtnExecCount(TbInput.Text, "該当", Color.Orange);

			if (iCnt > 0)
			{
				using (StreamWriter sw = new StreamWriter(TempFile, false, Encoding.GetEncoding("shift_jis")))
				{
					sw.WriteLine($"[{TbInput.Text}]\n以下 {CbDepth.Text}階層 {iCnt}フォルダ");

					foreach (string _s1 in GblSubDirList)
					{
						sw.WriteLine(_s1);
					}
				}

				// リスト表示
				P = Process.Start("notepad.exe", TempFile);
			}

			BtnTest.Enabled = true;
		}

		private void BtnExec_Click(object sender, EventArgs e)
		{
			if (!RtnbBtnTestExecCheck())
			{
				return;
			}

			BtnExec.Enabled = false;

			// 存在しない Dir を作成
			_ = Directory.CreateDirectory(TbOutput.Text);

			int iCnt = RtnBtnExecCount(TbInput.Text, "作成", Color.Red);

			foreach (string _s1 in GblSubDirList)
			{
				--iCnt;

				string _s2 = TbOutput.Text + _s1;
				if (!Directory.Exists(_s2))
				{
					_ = Directory.CreateDirectory(_s2);

					// タイトルに「残り」表示
					Text = $"残り {iCnt}";
				}
			}
			// タイトルを戻す
			Text = ProgramID;

			BtnExec.Enabled = true;
		}

		//-----------------------------------------------
		// [テスト][実行]ボタンが押されたときの共通処理
		//-----------------------------------------------
		private bool RtnbBtnTestExecCheck()
		{
			LblResult.Text = "";

			// Dir 整形
			TbInput.Text = RtnDirNormalization(TbInput.Text.Trim());
			TbInput.SelectionStart = TbInput.TextLength;
			TbOutput.Text = RtnDirNormalization(TbOutput.Text.Trim());
			TbOutput.SelectionStart = TbOutput.TextLength;

			// 入力 Dir 不在のとき
			if (!Directory.Exists(TbInput.Text))
			{
				Color foreColorCur = TbInput.ForeColor;
				Color backColorCur = TbInput.BackColor;

				TbInput.ForeColor = Color.White;
				TbInput.BackColor = Color.Red;

				Refresh();
				Thread.Sleep(500);

				TbInput.ForeColor = foreColorCur;
				TbInput.BackColor = backColorCur;

				return false;
			}

			// 出力 Drive 不在のとき
			if (TbOutput.TextLength >= 2 && !Directory.Exists(TbOutput.Text.Substring(0, 2)))
			{
				LblResult.Text = $"[Err] 出力ドライブ ({TbOutput.Text.Substring(0, 2).ToUpper()})";
				LblResult.ForeColor = Color.White;
				LblResult.BackColor = Color.Crimson;
				return false;
			}

			return true;
		}

		//------------------------
		// Dir 末尾に "\" を付与
		//------------------------
		private string RtnDirNormalization(String path)
		{
			return path.Length > 0 ? Path.GetFullPath(path).TrimEnd('\\') + @"\" : "";
		}

		//-------------------------------
		// [実行]ボタンを使用可否にする
		//-------------------------------
		private void SubBtnExecCtrl()
		{
			LblResult.ForeColor = LblResult.BackColor = BackColor;
			BtnExec.Enabled = Directory.Exists(TbInput.Text) && TbOutput.Text.Length > 0;
		}

		//------------------------------
		// 該当 Dir 数とコメントを表示
		//------------------------------
		private int RtnBtnExecCount(string path, string addText, Color addTextColor)
		{
			Cursor = Cursors.WaitCursor;
			LblResult.Enabled = false;

			SubDirListInit();
			SubDirList(path);
			GblSubDirList.Sort();

			LblResult.Text = GblSubDirList.Count + " フォルダ" + addText;
			LblResult.ForeColor = addTextColor;
			LblResult.BackColor = addText.Length > 0 ? Color.Black : BackColor;

			LblResult.Enabled = true;
			Cursor = Cursors.Default;

			return GblSubDirList.Count;
		}

		//-------------
		// Dir リスト
		//-------------
		private readonly List<string> GblSubDirList = new List<string>();

		// 除外する Dir長
		private int GblSubDirListBaseLen = 0;

		// Dir 最大深さ
		private int GblSubDirListLevelMax = 0;

		// 初期化
		private void SubDirListInit()
		{
			GblSubDirList.Clear();
			GblSubDirListBaseLen = TbInput.Text.Length;
			GblSubDirListLevelMax = int.Parse(CbDepth.Text) + RtnSerchCharCnt(TbInput.Text, '\\') - 1;
		}

		// 再帰
		private void SubDirList(string path)
		{
			try
			{
				DirectoryInfo dirInfo = new DirectoryInfo(path);

				// 子
				foreach (DirectoryInfo DI in dirInfo.EnumerateDirectories("*"))
				{
					if (GblSubDirListLevelMax >= RtnSerchCharCnt(DI.FullName, '\\'))
					{
						GblSubDirList.Add(DI.FullName.Substring(GblSubDirListBaseLen));
						SubDirList(DI.FullName);
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
		private int RtnSerchCharCnt(string s, char c)
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
		private void SubDirSelect(TextBox tb)
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

		private void CmsPath_貼り付け_Click(object sender, EventArgs e)
		{
			switch (CurOBJ)
			{
				case TextBox tb:
					tb.Paste();
					break;
			}
		}

		private void CmsPath_カーソルを先頭に移動_Click(object sender, EventArgs e)
		{
			switch (CurOBJ)
			{
				case TextBox tb:
					tb.SelectionStart = 0;
					break;
			}
		}

		private void CmsPath_カーソルを末尾に移動_Click(object sender, EventArgs e)
		{
			switch (CurOBJ)
			{
				case TextBox tb:
					tb.SelectionStart = tb.TextLength;
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
	}
}
