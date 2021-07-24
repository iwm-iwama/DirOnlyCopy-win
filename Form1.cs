using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace iwm_DirOnlyCopy
{
	public partial class Form1 : Form
	{
		private const string VERSION = "フォルダ構成をコピー iwm20210724";

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
			SubForm1_StartPosition();

			Text = VERSION;

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

		private void SubForm1_StartPosition()
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

		private void TbInput_MouseHover(object sender, EventArgs e)
		{
			_ = TbInput.Focus();
			ToolTip1.SetToolTip(TbInput, RtnPathList(TbInput.Text));
		}

		private void TbInput_Enter(object sender, EventArgs e)
		{
			CurOBJ = TbInput;
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

		private void TbOutput_MouseHover(object sender, EventArgs e)
		{
			_ = TbOutput.Focus();
			ToolTip1.SetToolTip(TbOutput, RtnPathList(TbOutput.Text));
		}

		private void TbOutput_Enter(object sender, EventArgs e)
		{
			CurOBJ = TbOutput;
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

			BtnTest.Enabled = false;

			TbInput.Text = RtnDirNormalization(TbInput.Text, false);
			TbInput.SelectionStart = TbInput.TextLength;

			TbOutput.Text = RtnDirNormalization(TbOutput.Text, false);
			TbOutput.SelectionStart = TbOutput.TextLength;

			int iGblSubDirList = RtnBtnExecCount(TbInput.Text, "該当", Color.Orange);

			if (iGblSubDirList > 0)
			{
				using (StreamWriter sw = new StreamWriter(TempFile, false, Encoding.GetEncoding("shift_jis")))
				{
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
			BtnExec.Enabled = false;

			TbInput.Text = RtnDirNormalization(TbInput.Text, false);
			TbInput.SelectionStart = TbInput.TextLength;

			TbOutput.Text = RtnDirNormalization(TbOutput.Text, true);
			TbOutput.SelectionStart = TbOutput.TextLength;

			int iGblSubDirList = RtnBtnExecCount(TbInput.Text, "作成", Color.Red);

			// Mkdir
			_ = Directory.CreateDirectory(TbOutput.Text);
			foreach (string _s1 in GblSubDirList)
			{
				--iGblSubDirList;

				string _s2 = TbOutput.Text + _s1;
				if (!Directory.Exists(_s2))
				{
					_ = Directory.CreateDirectory(_s2);

					// タイトルに「残り」表示
					Text = $"残り {iGblSubDirList}";
				}
			}
			// タイトルを戻す
			Text = VERSION;

			BtnExec.Enabled = true;
		}

		//----------
		// CmsPath
		//----------
		private void CmsPath_全クリア_Click(object sender, EventArgs e)
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

		//-------------------------------
		// [実行]ボタンを使用可否にする
		//-------------------------------
		private void SubBtnExecCtrl()
		{
			LblResult.ForeColor = LblResult.BackColor = BackColor;
			BtnExec.Enabled = Directory.Exists(TbInput.Text) && TbOutput.Text.Length > 0;
		}

		//---------------------------------
		// 該当フォルダ数とコメントを表示
		//---------------------------------
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
		// Dir 再帰
		//-----------
		// Dir リスト
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

		//--------------------------
		// Dir の末尾に "\" を付与
		//--------------------------
		private string RtnDirNormalization(String path, bool mkdirOn)
		{
			if (path.Length == 0)
			{
				return "";
			}

			// 存在しないDirを作成
			if (mkdirOn && Directory.Exists(path))
			{
				_ = Directory.CreateDirectory(path);
			}

			return Path.GetFullPath(path).TrimEnd('\\') + @"\";
		}

		//---------------------------
		// Path の "\" を改行に変換
		//---------------------------
		private string RtnPathList(string path)
		{
			string rtn = "";
			foreach (string _s1 in path.Split('\\'))
			{
				rtn += _s1 + NL;
			}
			return rtn;
		}
	}
}
