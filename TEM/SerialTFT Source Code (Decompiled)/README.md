# SerialTFT — decompiled source

Decompiled from `\\alicatfiles\public\Production\Tools\##NEW TOOLS FOLDER##\Gearwrench Torque Checker Software\TEM\TFT Update\TFT读数据\SerialTFT.exe`
(v1.0.0.0, © 2019, a .NET Framework 2.0 Windows Forms app, Debug build) plus its three language packs
(`en-US`, `zh-CN`, `zh-Hans` `SerialTFT.resources.dll`). Decompiled with ILSpy 10.1 on 2026-10-23.

The other files in that `TEM` folder are not part of the program and were not decompiled:
`CH341SER.EXE` is WCH's USB-to-serial driver installer (it makes the torque checker's USB cable show up as a COM port),
and the PDF is the GearWrench manual.

## Layout

| File | What it is |
|---|---|
| `SerialTFT.sln` | Visual Studio solution |
| `SerialTFT/Rs232.cs` | Serial-port protocol: finding the tester, sending commands, parsing records |
| `SerialTFT/Test.cs` | Main window logic: Read button, timer loop, CSV export, unit conversion |
| `SerialTFT/Test.Designer.cs` | Window controls and layout (`InitializeComponent`) |
| `SerialTFT/Test.resx`, `Test.*.resx` | Window text/layout: neutral, English, Chinese |
| `SerialTFT/MultiLanguage.cs` | English/Chinese menu switch |
| `SerialTFT/Program.cs` | Entry point |
| `SerialTFT/Properties/*` | Assembly info, saved language setting, empty resources |

Method, field and class names are the originals. Local variable names (`num`, `array`, …) and all comments were not in the exe,
so ILSpy invented or dropped them. The form was split into `Test.cs` / `Test.Designer.cs` so the Visual Studio designer can open it.

## Building

- **Visual Studio 2022** (".NET desktop development" workload): open `SerialTFT.sln` and build.
- **Command line**: `dotnet build SerialTFT\SerialTFT.csproj -c Release` (needs the .NET SDK). Command-line builds also output
  `System.Resources.Extensions.dll` and a few `System.*.dll` files; keep them next to `SerialTFT.exe`.

The project targets .NET Framework 4.8 instead of the original 2.0, because 4.8 ships with Windows 10/11 and 2.0 needs the optional
".NET 3.5" Windows feature. The code is unchanged.

## How faithful is it

The project was built, and the rebuilt exe was decompiled again and compared to the original:

- **Code**: same logic. The only differences come from the original being a Debug build (a few unused variables and `= 0`
  field initializers that a Release build drops) and from member order after splitting the form into two files.
- **Window resources**: all 225 entries (text, sizes, positions, fonts, layout, in all three languages) match exactly.

The rebuilt exe was **not** run: on startup it opens every COM port on the PC and sends a probe frame to each one.

## How the program works

**Connecting** (`Rs232.Connect`): tries every COM port at 115200 baud, 8N1. On each it sends command 20 and waits up to 2 s for a
parameter reply of `23131` (0x5A5B). The first port that answers is used, and command 21 then returns the number of records stored
in the tester. The window retries every ~1 s while disconnected.

**Reading** (the Read / 读取 button): reads records *start*..*end* (the two number boxes) by sending command 22 with the record number,
one record about every 300 ms, and appends each reply to the CSV. When done it sends command 41. The Reset button also sends command 41;
the code doesn't say what the tester does with it.

**Command frame (PC → tester)**, 1036 bytes:

| Offset | Value |
|---|---|
| 0 | `0x3A` (`:`) |
| 1 | `0x01` |
| 2 | sub-address (always 1) |
| 3 | command (20 handshake, 21 record count, 22 read record, 41 reset) |
| 4–7 | uint32 parameter, little-endian (record number for command 22) |
| 8–1031 | zeros |
| 1032 | checksum: sum of bytes 0–1031, mod 256 |
| 1033–1035 | `0x0D 0x0A 0x00` |

**Reply frame (tester → PC)**, 260 bytes: byte 0 = `0x0F`, byte 259 = `0x0D`, byte 258 = sum of bytes 1–257 mod 256.
If byte 256 is non-zero it's a parameter reply (uint32 at offset 4). If it's 0 it's a torque record (all little-endian):

| Offset | Field | Offset | Field |
|---|---|---|---|
| 4 | record number (uint32) | 96 / 100 / 104 | Max / Min / Avg (float) |
| 8 | unit code (uint32) | 108 | mode: 2 = peak, 3 = first peak |
| 12 | Ptr (uint32) | 112 | direction: 1 = CW, 2 = CCW |
| 20–80 | Data1–Data16 (float, N·m) | 116 | timestamp, seconds since 1970 |
| 84 | set torque (float) | 120–135 | serial number, 16 ASCII chars |
| 88 / 92 | Left / Right (float) | 136 / 140 / 144 | CPUID 1 / 2 / 3 (uint32) |

Unit codes, as the program labels them: 1 Nm, 2 cNm, 3 Kgm, 4 cKgm, 5 gm, 6 Lbft, 7 Lbin, 8 Ozin. The readings arrive in N·m and the program divides by
the unit factor in `Test.TorqueUnitResult` before writing them.

**CSV output**: `OutputData\测试数据 <yyyy年MM月dd日 HH时mm分ss秒>.csv` under the program's working folder, GB2312-encoded,
with Chinese headers: 序号 record no., 编号 serial no., 加载模式 mode, 方向 direction, 测试单位 unit, 数据1–15 readings,
测试日期 date, 测试时间 time, 传感器系列号 sensor serial. Only Data1–15 are written (Data16 is read but dropped),
and the "sensor serial" column is just CPUID 3 as a decimal number. Set torque, Left/Right and Max/Min/Avg are read but not exported.
