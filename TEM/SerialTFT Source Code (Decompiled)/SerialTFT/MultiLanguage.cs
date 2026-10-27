using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using SerialTFT.Properties;

namespace SerialTFT
{
	internal static class MultiLanguage
	{
		public static string DefaultLanguage = "zh-CN";

		public static void SetDefaultLanguage(string lang)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
			DefaultLanguage = lang;
			Settings.Default.DefaultLanguage = lang;
			Settings.Default.Save();
		}

		public static void LoadLanguage(Form form, Type formType)
		{
			if (form != null)
			{
				ComponentResourceManager componentResourceManager = new ComponentResourceManager(formType);
				componentResourceManager.ApplyResources(form, "$this");
				Loading(form, componentResourceManager);
			}
		}

		private static void Loading(Control control, ComponentResourceManager resources)
		{
			if (control is MenuStrip)
			{
				resources.ApplyResources(control, control.Name);
				MenuStrip menuStrip = (MenuStrip)control;
				if (menuStrip.Items.Count > 0)
				{
					foreach (ToolStripMenuItem item in menuStrip.Items)
					{
						Loading(item, resources);
					}
				}
			}
			foreach (Control control2 in control.Controls)
			{
				resources.ApplyResources(control2, control2.Name);
				Loading(control2, resources);
			}
		}

		private static void Loading(ToolStripMenuItem item, ComponentResourceManager resources)
		{
			if (item == null)
			{
				return;
			}
			resources.ApplyResources(item, item.Name);
			if (item.DropDownItems.Count <= 0)
			{
				return;
			}
			foreach (ToolStripMenuItem dropDownItem in item.DropDownItems)
			{
				Loading(dropDownItem, resources);
			}
		}
	}
}
