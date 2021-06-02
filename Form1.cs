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
		private const string VERSION = "フォルダ構成をコピー iwm20210601";

		private readonly int[] DirLevel = { 1, 260 };

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			StartPosition = FormStartPosition.Manual;
			Form1_StartPosition();

			Text = VERSION;

			for (int _i1 = DirLevel[0]; _i1 <= DirLevel[1]; _i1++)
			{
				_ = CbDepth.Items.Add(_i1);
			}
			CbDepth.Text = DirLevel[0].ToString();

			TbInput.Text = "";
			TbOutput.Text = "";
			LblResult.Text = "";

			BtnExec.Enabled = false;

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

		private Point MousePoint;

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				MousePoint = new Point(e.X, e.Y);
			}
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				Left += e.X - MousePoint.X;
				Top += e.Y - MousePoint.Y;
			}
		}

		private void Form1_StartPosition()
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

		private void TbInput_MouseEnter(object sender, EventArgs e)
		{
			ToolTip1.SetToolTip(TbInput, TbInput.Text);
		}

		private void CmsInput_クリア_Click(object sender, EventArgs e)
		{
			TbInput.Text = "";
			_ = TbInput.Focus();
		}

		private void CmsInput_貼り付け_Click(object sender, EventArgs e)
		{
			TbInput.Text = Clipboard.GetText();
			TbInput.Select(TbInput.Text.Length, 0);
			_ = TbInput.Focus();
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

		private void TbOutput_MouseEnter(object sender, EventArgs e)
		{
			ToolTip1.SetToolTip(TbOutput, TbOutput.Text);
		}

		private void CmsOutput_クリア_Click(object sender, EventArgs e)
		{
			TbOutput.Text = "";
			_ = TbOutput.Focus();
		}

		private void CmsOutput_貼り付け_Click(object sender, EventArgs e)
		{
			TbOutput.Text = Clipboard.GetText();
			TbOutput.Select(TbOutput.Text.Length, 0);
			_ = TbOutput.Focus();
		}

		private void SubTextBoxDragEnter(
			DragEventArgs e,
			TextBox tb
		)
		{
			string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			tb.Text = Directory.Exists(fileName[0]) ? fileName[0] : Path.GetDirectoryName(fileName[0]);
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

			TbInput.Text = RtnDirNormalization(TbInput.Text);

			BtnTest.Enabled = false;

			SubBtnExecCount("該当");

			if (GblSubDirList.Count > 0)
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
			TbInput.Text = RtnDirNormalization(TbInput.Text);
			TbOutput.Text = RtnDirNormalization(TbOutput.Text);

			BtnExec.Enabled = false;

			SubBtnExecCount("作成");

			// basename を取得
			string basePath = Path.GetFileName(TbInput.Text.TrimEnd('\\'));
			string copyPath = TbOutput.Text + basePath + @"\";

			// Mkdir
			_ = Directory.CreateDirectory(copyPath);

			foreach (string _s1 in GblSubDirList)
			{
				string s = copyPath + _s1;
				if (!Directory.Exists(s))
				{
					_ = Directory.CreateDirectory(s);
				}
			}

			BtnExec.Enabled = true;
		}

		//------------------
		// button3 Control
		//------------------
		private void SubBtnExecCtrl()
		{
			LblResult.Text = "";
			BtnExec.Enabled = Directory.Exists(TbInput.Text) && TbOutput.Text.Length > 0;
		}

		private void SubBtnExecCount(
			string s = ""
		)
		{
			Cursor = Cursors.WaitCursor;

			LblResult.Text = "処理中...";

			SubDirListInit();
			SubDirList(TbInput.Text);

			LblResult.Text = GblSubDirList.Count + " フォルダ" + s;

			Cursor = Cursors.Default;
		}

		//------------
		// Char 検索
		//------------
		private int RtnSerchCharCnt(string S, char C)
		{
			int rtn = 0;
			foreach (char _c1 in S.ToCharArray())
			{
				if (_c1 == C)
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
			GblSubDirListLevelMax = RtnStrToInt(CbDepth.Text) + RtnSerchCharCnt(TbInput.Text, '\\') - 1;
		}

		// 再帰
		private void SubDirList(
			string path
		)
		{
			try
			{
				DirectoryInfo dirInfo = new DirectoryInfo(path);

				// 子
				foreach (DirectoryInfo DI in dirInfo.EnumerateDirectories("*"))
				{
					Application.DoEvents();// 割込を手抜き実装

					if (GblSubDirListLevelMax >= RtnSerchCharCnt(DI.FullName, '\\'))
					{
						GblSubDirList.Add(DI.FullName.Substring(GblSubDirListBaseLen));
						SubDirList(DI.FullName);
					}
				}

				// ものすごく遅い
				// GblSubDirList.Sort();
			}
			catch
			{
			}
		}

		//-----------
		// Dir 選択
		//-----------
		private void SubDirSelect(
			TextBox tb
		)
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
				_ = tb.Focus();
				tb.Select(0, 0);
			}
		}

		//--------------------------
		// Dir の末尾に "\" を付与
		//--------------------------
		private string RtnDirNormalization(String path)
		{
			return Directory.Exists(path) ? Path.GetFullPath(path).TrimEnd('\\') + @"\" : path;
		}

		//-----------------------
		// String を Int に変換
		//-----------------------
		private int RtnStrToInt(string S)
		{
			try
			{
				return int.Parse(S);
			}
			catch
			{
				return 0;
			}
		}
	}
}
