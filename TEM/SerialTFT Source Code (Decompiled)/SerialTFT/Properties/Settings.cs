using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SerialTFT.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default => defaultInstance;

		[DebuggerNonUserCode]
		[DefaultSettingValue("zh-CN")]
		[UserScopedSetting]
		public string DefaultLanguage
		{
			get
			{
				return (string)this["DefaultLanguage"];
			}
			set
			{
				this["DefaultLanguage"] = value;
			}
		}
	}
}
