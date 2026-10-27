using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace SerialTFT
{
	public static class Rs232
	{
		public struct TorqueData
		{
			public float Data1;

			public float Data2;

			public float Data3;

			public float Data4;

			public float Data5;

			public float Data6;

			public float Data7;

			public float Data8;

			public float Data9;

			public float Data10;

			public float Data11;

			public float Data12;

			public float Data13;

			public float Data14;

			public float Data15;

			public float Data16;

			public string Sn16Chart;

			public uint UnitType;

			public uint Time;

			public uint Ptr;

			public uint Model;

			public uint Dir;

			public uint Times;

			public float UnitValue;

			public float SetTorque;

			public float Left;

			public float Right;

			public float Max;

			public float Min;

			public float Avr;

			public uint CPUID01;

			public uint CPUID02;

			public uint CPUID03;
		}

		private const int SendDataLength = 1024;

		private const int ReceiveDataLength = 260;

		private const int ReBufferLength = 1048576;

		public static bool isConnected = false;

		public static bool isRunning = false;

		private static int commError = 0;

		public static uint TestTotalTimes = 0u;

		private static byte[] TorqueName = new byte[16];

		private static byte[] ReceiveBytes = new byte[260];

		private static List<byte> buffer = new List<byte>(1048576);

		public static uint ParaDataLong = 0u;

		private static Thread ReceiveThread = new Thread(Receive);

		private static SerialPort SerialPort1 = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);

		public static TorqueData CurrentTorque = default(TorqueData);

		public static List<TorqueData> ResultTorques = new List<TorqueData>();

		public static void ThreadStart()
		{
			try
			{
				ReceiveThread.Priority = ThreadPriority.Lowest;
				ReceiveThread.Start();
			}
			catch
			{
			}
		}

		public static void Connect()
		{
			isConnected = false;
			if (SerialPort1 != null && SerialPort1.IsOpen)
			{
				SerialPort1.Close();
			}
			string[] portNames = SerialPort.GetPortNames();
			for (int i = 0; i < portNames.Length; i++)
			{
				try
				{
					SerialPort1.PortName = portNames[i];
					SerialPort1.ReadBufferSize = 1048576;
					SerialPort1.Open();
					SerialPort1.DiscardInBuffer();
					ParaDataLong = 0u;
					WriteData407(1, 20, 0u);
					int tickCount = Environment.TickCount;
					while (ParaDataLong == 0 && Math.Abs(Environment.TickCount - tickCount) < 2000)
					{
						Thread.Sleep(30);
						Rs232Read();
					}
					if (ParaDataLong == 23131)
					{
						isConnected = true;
						break;
					}
					isConnected = false;
					if (SerialPort1 != null && SerialPort1.IsOpen)
					{
						SerialPort1.Close();
					}
				}
				catch (Exception)
				{
				}
			}
			if (SerialPort1 != null && SerialPort1.IsOpen)
			{
				isConnected = true;
				ParaDataLong = 0u;
				WriteData407(1, 21, 0u);
				int tickCount = Environment.TickCount;
				while (ParaDataLong == 0 && Math.Abs(Environment.TickCount - tickCount) < 2000)
				{
					Thread.Sleep(30);
					Rs232Read();
				}
				TestTotalTimes = ParaDataLong;
			}
			else
			{
				isConnected = false;
			}
		}

		public static void ThreadStop()
		{
			if (SerialPort1 != null && SerialPort1.IsOpen)
			{
				Thread.Sleep(50);
				SerialPort1.Close();
				Thread.Sleep(100);
			}
			isConnected = false;
			ReceiveThread.Abort();
		}

		public static void Rs232ErrCounter()
		{
			commError++;
			if (commError > 3)
			{
				if (SerialPort1 != null && SerialPort1.IsOpen)
				{
					SerialPort1.Close();
					isConnected = false;
				}
				commError = 0;
				isConnected = false;
			}
		}

		private static void ReadParaValue()
		{
			ParaDataLong = BitConverter.ToUInt32(ReceiveBytes, 4);
		}

		private static void ReadDataValue()
		{
			ushort num = 0;
			CurrentTorque.Times = BitConverter.ToUInt32(ReceiveBytes, 4);
			CurrentTorque.UnitType = BitConverter.ToUInt32(ReceiveBytes, 8);
			CurrentTorque.Ptr = BitConverter.ToUInt32(ReceiveBytes, 12);
			CurrentTorque.Time = BitConverter.ToUInt32(ReceiveBytes, 16);
			CurrentTorque.Data1 = BitConverter.ToSingle(ReceiveBytes, 20);
			CurrentTorque.Data2 = BitConverter.ToSingle(ReceiveBytes, 24);
			CurrentTorque.Data3 = BitConverter.ToSingle(ReceiveBytes, 28);
			CurrentTorque.Data4 = BitConverter.ToSingle(ReceiveBytes, 32);
			CurrentTorque.Data5 = BitConverter.ToSingle(ReceiveBytes, 36);
			CurrentTorque.Data6 = BitConverter.ToSingle(ReceiveBytes, 40);
			CurrentTorque.Data7 = BitConverter.ToSingle(ReceiveBytes, 44);
			CurrentTorque.Data8 = BitConverter.ToSingle(ReceiveBytes, 48);
			CurrentTorque.Data9 = BitConverter.ToSingle(ReceiveBytes, 52);
			CurrentTorque.Data10 = BitConverter.ToSingle(ReceiveBytes, 56);
			CurrentTorque.Data11 = BitConverter.ToSingle(ReceiveBytes, 60);
			CurrentTorque.Data12 = BitConverter.ToSingle(ReceiveBytes, 64);
			CurrentTorque.Data13 = BitConverter.ToSingle(ReceiveBytes, 68);
			CurrentTorque.Data14 = BitConverter.ToSingle(ReceiveBytes, 72);
			CurrentTorque.Data15 = BitConverter.ToSingle(ReceiveBytes, 76);
			CurrentTorque.Data16 = BitConverter.ToSingle(ReceiveBytes, 80);
			CurrentTorque.SetTorque = BitConverter.ToSingle(ReceiveBytes, 84);
			CurrentTorque.Left = BitConverter.ToSingle(ReceiveBytes, 88);
			CurrentTorque.Right = BitConverter.ToSingle(ReceiveBytes, 92);
			CurrentTorque.Max = BitConverter.ToSingle(ReceiveBytes, 96);
			CurrentTorque.Min = BitConverter.ToSingle(ReceiveBytes, 100);
			CurrentTorque.Avr = BitConverter.ToSingle(ReceiveBytes, 104);
			CurrentTorque.Model = BitConverter.ToUInt32(ReceiveBytes, 108);
			CurrentTorque.Dir = BitConverter.ToUInt32(ReceiveBytes, 112);
			CurrentTorque.Time = BitConverter.ToUInt32(ReceiveBytes, 116);
			for (num = 0; num < 16; num++)
			{
				TorqueName[num] = ReceiveBytes[120 + num];
			}
			CurrentTorque.Sn16Chart = Encoding.Default.GetString(TorqueName);
			CurrentTorque.CPUID01 = BitConverter.ToUInt32(ReceiveBytes, 136);
			CurrentTorque.CPUID02 = BitConverter.ToUInt32(ReceiveBytes, 140);
			CurrentTorque.CPUID03 = BitConverter.ToUInt32(ReceiveBytes, 144);
			if (isRunning)
			{
				ResultTorques.Add(CurrentTorque);
			}
		}

		private static void Rs232Read()
		{
			if (SerialPort1 == null || !SerialPort1.IsOpen)
			{
				return;
			}
			int bytesToRead = SerialPort1.BytesToRead;
			byte[] collection = new byte[bytesToRead];
			if (bytesToRead < 260)
			{
				Thread.Sleep(20);
				return;
			}
			commError = 0;
			SerialPort1.Read(collection, 0, bytesToRead);
			buffer.AddRange(collection);
			while (buffer.Count >= 260)
			{
				if (buffer[0] == 15 && buffer[259] == 13)
				{
					buffer.CopyTo(0, ReceiveBytes, 0, 260);
					int num = 0;
					for (int i = 1; i <= 257; i++)
					{
						num += ReceiveBytes[i];
					}
					buffer.RemoveRange(0, 260);
					if (ReceiveBytes[258] == num % 256)
					{
						if (ReceiveBytes[256] == 0)
						{
							ReadDataValue();
						}
						else
						{
							ReadParaValue();
						}
					}
				}
				else
				{
					buffer.RemoveAt(0);
				}
			}
		}

		private static void Receive()
		{
			try
			{
				while (true)
				{
					bool flag = true;
					if (isConnected)
					{
						Rs232Read();
					}
					else
					{
						Thread.Sleep(50);
					}
				}
			}
			catch
			{
			}
		}

		public static void WriteData407(byte SubsAddress, byte Command, uint DataAddress)
		{
			if (SerialPort1 != null && SerialPort1.IsOpen)
			{
				byte[] array = new byte[1040];
				byte[] array2 = new byte[4];
				array[0] = 58;
				array[1] = 1;
				array[2] = SubsAddress;
				array[3] = Command;
				array2 = BitConverter.GetBytes(DataAddress);
				array[4] = array2[0];
				array[5] = array2[1];
				array[6] = array2[2];
				array[7] = array2[3];
				for (int i = 0; i < 1023; i++)
				{
					array[8 + i] = 0;
				}
				uint num = 0u;
				for (int i = 0; i < 1032; i++)
				{
					num += array[i];
				}
				array[1032] = (byte)(num % 256);
				array[1033] = 13;
				array[1034] = 10;
				array[1035] = 0;
				SerialPort1.Write(array, 0, 1036);
				Thread.Sleep(50);
			}
		}

		public static void ReadFlash(uint Times)
		{
			WriteData407(1, 22, Times);
		}
	}
}
