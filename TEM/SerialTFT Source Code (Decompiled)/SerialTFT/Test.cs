using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SerialTFT
{
	public partial class Test : Form
	{
		private FileStream fs;

		private StreamWriter sw;

		private string filename = "OutputData\\" + DateTime.Now.ToString("TestData yyyy-MM-dd HH-mm-ss") + ".csv";

		private DateTime dtStandard = new DateTime(1970, 1, 1, 0, 0, 0);

		private int Rs232Connect = 0;

		private int FindRs232Conter = 0;

		private uint TickCounter = 0u;

		private uint UpTimes = 0u;

		private uint DownTimes = 0u;

		private uint TimesDecCounter = 0u;

		private uint CurrentTimes = 0u;

		private uint LcdCounter = 0u;

		private float Data1;

		private float Data2;

		private float Data3;

		private float Data4;

		private float Data5;

		private float Data6;

		private float Data7;

		private float Data8;

		private float Data9;

		private float Data10;

		private float Data11;

		private float Data12;

		private float Data13;

		private float Data14;

		private float Data15;

		private float Data16;

		private string Sn16Chart;

		private uint UnitType;

		private uint Time;

		private uint Ptr;

		private uint Model;

		private uint Dir;

		private uint Times;

		private float UnitValue;

		private float SetTorque;

		private float DataLeft = 0f;

		private float DataRight;

		private float Max;

		private float Min;

		private float Avr;

		private uint SnCpuid01;

		private uint SnCpuid02;

		private uint SnCpuid03;

		private DateTime dtTest;

		private string TestDataStrdate;

		private string TestDataStrTime;

		private string TorqueUnit;

		private string TestMode;

		private string TestDir;

		[DllImport("kernel32.dll")]
		private static extern uint SetThreadExecutionState(int flags);

		public Test()
		{
			InitializeComponent();
			Rs232.Connect();
			Rs232.ThreadStart();
			if (!Directory.Exists("OutputData"))
			{
				Directory.CreateDirectory("OutputData");
			}
			ConnectSuccessFail();
			timer1.Enabled = true;
			timer1.Start();
			label1.Visible = false;
		}

		private void Test_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.Visible && MessageBox.Show("Are you sure you want to exit the application?              ", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
			{
				e.Cancel = true;
				return;
			}
			CloseFile();
			Rs232.ThreadStop();
			timer1.Stop();
			base.Visible = false;
		}

		private void ConnectSuccessFail()
		{
			if (Rs232.isConnected)
			{
				if (Rs232Connect != 1)
				{
					toolStripStatusLabel1.Text = "Successful Connection";
					toolStripStatusLabel1.BackColor = Color.Chartreuse;
					Rs232Connect = 1;
				}
			}
			else if (Rs232Connect != 1)
			{
				toolStripStatusLabel1.Text = "Connection Failed";
				toolStripStatusLabel1.BackColor = Color.Yellow;
				Rs232Connect = 2;
			}
		}

		private void CreateFile(string filename)
		{
			fs = new FileStream(filename, FileMode.Create);
			sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
			sw.Write("No.,ID,Load Mode,Direction,\tTest Unit,Data 1\t,Data 2\t,Data 3\t,Data 4\t,Data 5\t,Data 6\t,Data 7\t,Data 8\t,Data 9\t,Data 10\t,Data 11\t,Data 12\t,Data 13\t,Data 14\t,Data 15\t,Test Date,Test Time,Sensor Serial Number\r\n");
			sw.Flush();
			sw.Close();
			fs.Close();
			fs = new FileStream(filename, FileMode.Append);
			sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
		}

		private void CloseFile()
		{
			try
			{
				sw.Flush();
				sw.Close();
				fs.Close();
			}
			catch
			{
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Rs232.WriteData407(1, 41, 0u);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (Rs232.isRunning)
			{
				return;
			}
			if (Rs232Connect != 1)
			{
				MessageBox.Show(" Connection failed. Please check that the USB cable is connected!");
				return;
			}
			if (Rs232.TestTotalTimes < 2)
			{
				MessageBox.Show(" No test data available!");
				return;
			}
			UpTimes = (uint)TimesUp.Value;
			DownTimes = (uint)TimesDown.Value;
			if (UpTimes > Rs232.TestTotalTimes)
			{
				UpTimes = Rs232.TestTotalTimes - 1;
				TimesUp.Value = Rs232.TestTotalTimes - 1;
			}
			if (DownTimes > Rs232.TestTotalTimes)
			{
				DownTimes = Rs232.TestTotalTimes;
				TimesDown.Value = Rs232.TestTotalTimes;
			}
			if (DownTimes > UpTimes)
			{
				filename = "OutputData\\" + DateTime.Now.ToString("yyyy MM dd HH mm ss") + ".csv";
				CreateFile(filename);
				TimesDecCounter = DownTimes - UpTimes;
				progressBar1.Maximum = (int)(TimesDecCounter + 1);
				progressBar1.Value = 0;
				progressBar1.Visible = true;
				Rs232.isRunning = true;
				label1.Text = filename;
				label1.Visible = false;
				CurrentTimes = UpTimes;
			}
			else
			{
				Rs232.isRunning = false;
				progressBar1.Visible = false;
				MessageBox.Show("Invalid parameters!");
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Rs232.Connect();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			timer1.Stop();
			Rs232.isRunning = false;
			progressBar1.Visible = false;
		}

		private string LoadResultUnit(uint Data)
		{
			string text = "";
			switch (Data)
			{
			case 1u:
				return "Nm";
			case 2u:
				return "cNm";
			case 3u:
				return "Kgm";
			case 4u:
				return "cKgm";
			case 5u:
				return "gm";
			case 6u:
				return "Lbft";
			case 7u:
				return "Lbin";
			case 8u:
				return "Ozin";
			default:
				return "Nm";
			}
		}

		private string LoadResultMode(uint Data)
		{
			string result = "";
			if (Data == 2)
			{
				result = "Peak";
			}
			if (Data == 3)
			{
				result = "First Peak";
			}
			return result;
		}

		private string LoadDir(uint Data)
		{
			string result = "";
			if (Data == 1)
			{
				result = "CW";
			}
			if (Data == 2)
			{
				result = "CCW";
			}
			return result;
		}

		private float TorqueUnitResult(uint Unit)
		{
			float num = 1f;
			switch (Unit)
			{
			case 1u:
				return 1f;
			case 2u:
				return 0.01f;
			case 3u:
				return 9.8065f;
			case 4u:
				return 0.098065f;
			case 5u:
				return 0.0098f;
			case 6u:
				return 1.355932f;
			case 7u:
				return 0.112969f;
			case 8u:
				return 0.007042f;
			default:
				return 1f;
			}
		}

		private void ReadFlashToDataValue()
		{
			UnitType = Rs232.ResultTorques[0].UnitType;
			Data1 = Rs232.ResultTorques[0].Data1 / TorqueUnitResult(UnitType);
			Data2 = Rs232.ResultTorques[0].Data2 / TorqueUnitResult(UnitType);
			Data3 = Rs232.ResultTorques[0].Data3 / TorqueUnitResult(UnitType);
			Data4 = Rs232.ResultTorques[0].Data4 / TorqueUnitResult(UnitType);
			Data5 = Rs232.ResultTorques[0].Data5 / TorqueUnitResult(UnitType);
			Data6 = Rs232.ResultTorques[0].Data6 / TorqueUnitResult(UnitType);
			Data7 = Rs232.ResultTorques[0].Data7 / TorqueUnitResult(UnitType);
			Data8 = Rs232.ResultTorques[0].Data8 / TorqueUnitResult(UnitType);
			Data9 = Rs232.ResultTorques[0].Data9 / TorqueUnitResult(UnitType);
			Data10 = Rs232.ResultTorques[0].Data10 / TorqueUnitResult(UnitType);
			Data11 = Rs232.ResultTorques[0].Data11 / TorqueUnitResult(UnitType);
			Data12 = Rs232.ResultTorques[0].Data12 / TorqueUnitResult(UnitType);
			Data13 = Rs232.ResultTorques[0].Data13 / TorqueUnitResult(UnitType);
			Data14 = Rs232.ResultTorques[0].Data14 / TorqueUnitResult(UnitType);
			Data15 = Rs232.ResultTorques[0].Data15 / TorqueUnitResult(UnitType);
			Data16 = Rs232.ResultTorques[0].Data16 / TorqueUnitResult(UnitType);
			Time = Rs232.ResultTorques[0].Time;
			Ptr = Rs232.ResultTorques[0].Ptr;
			Model = Rs232.ResultTorques[0].Model;
			Dir = Rs232.ResultTorques[0].Dir;
			Times = Rs232.ResultTorques[0].Times;
			UnitValue = Rs232.ResultTorques[0].UnitValue;
			SetTorque = Rs232.ResultTorques[0].SetTorque;
			DataLeft = Rs232.ResultTorques[0].Left;
			DataRight = Rs232.ResultTorques[0].Right;
			SnCpuid01 = Rs232.ResultTorques[0].CPUID01;
			SnCpuid02 = Rs232.ResultTorques[0].CPUID02;
			SnCpuid03 = Rs232.ResultTorques[0].CPUID03;
			Max = Rs232.ResultTorques[0].Max;
			Min = Rs232.ResultTorques[0].Min;
			Avr = Rs232.ResultTorques[0].Avr;
			dtTest = dtStandard.AddSeconds(Time);
			TestDataStrdate = Convert.ToDateTime(dtTest).ToShortDateString();
			TestDataStrTime = Convert.ToDateTime(dtTest).ToLongTimeString();
			TorqueUnit = LoadResultUnit(UnitType);
			TestMode = LoadResultMode(Model);
			TestDir = LoadDir(Dir);
			Sn16Chart = Rs232.ResultTorques[0].Sn16Chart;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			TickCounter++;
			FindRs232Conter++;
			if (FindRs232Conter > 10)
			{
				FindRs232Conter = 0;
				ConnectSuccessFail();
				if (Rs232Connect != 1)
				{
					Rs232.Connect();
				}
			}
			if (TickCounter > 2)
			{
				TickCounter = 0u;
				while (Rs232.ResultTorques.Count > 0 && Rs232Connect == 1)
				{
					ReadFlashToDataValue();
					sw.WriteLine(Times.ToString() + "," + Sn16Chart + "," + TestMode + "," + TestDir + "," + TorqueUnit + "," + Data1.ToString("0.000") + "," + Data2.ToString("0.000") + "," + Data3.ToString("0.000") + "," + Data4.ToString("0.000") + "," + Data5.ToString("0.000") + "," + Data6.ToString("0.000") + "," + Data7.ToString("0.000") + "," + Data8.ToString("0.000") + "," + Data9.ToString("0.000") + "," + Data10.ToString("0.000") + "," + Data11.ToString("0.000") + "," + Data12.ToString("0.000") + "," + Data13.ToString("0.000") + "," + Data14.ToString("0.000") + "," + Data15.ToString("0.000") + "," + TestDataStrdate + "," + TestDataStrTime + "," + SnCpuid03);
					if (Rs232.ResultTorques.Count > 0)
					{
						Rs232.ResultTorques.Remove(Rs232.ResultTorques[0]);
					}
				}
				if (Rs232.isRunning && CurrentTimes > DownTimes)
				{
					Rs232.isRunning = false;
					CloseFile();
					progressBar1.Visible = false;
					label1.Text = filename;
					label1.Visible = true;
					Rs232.WriteData407(1, 41, 0u);
				}
				if (Rs232.isRunning && CurrentTimes <= DownTimes)
				{
					Rs232.ReadFlash(CurrentTimes);
					progressBar1.Value = (int)(CurrentTimes - UpTimes);
					toolStripStatusLabel2.Text = CurrentTimes.ToString();
					CurrentTimes++;
				}
			}
			LcdCounter++;
			if (LcdCounter > 30)
			{
				LcdCounter = 0u;
				SetThreadExecutionState(3);
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			Rs232.WriteData407(1, 41, 0u);
		}
	}
}
