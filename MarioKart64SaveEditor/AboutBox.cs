using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace MarioKart64SaveEditor
{
   partial class AboutBox : Form
   {
      public AboutBox()
      {
         var thanks = "Special thanks to :\r\n" +
            "  \u2022 shygoo for Mario Kart 64 documentation and testing\r\n" +
            "  \u2022 abney317 for MK64 EEPROM notes\r\n" +
            "  \u2022 mib_f8sm9c for the MK64 Pitstop";
         InitializeComponent();
         this.Text = String.Format("About {0}", AssemblyTitle);
         this.labelProductName.Text = AssemblyProduct;
         this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
         this.labelCopyright.Text = AssemblyCopyright;
         this.textBoxDescription.Text = thanks;
         setUpdateUrl("https://github.com/queueRAM/MarioKart64SaveEditor");
      }

      private void setUpdateUrl(string url)
      {
         const string updates = "Updates: ";
         this.linkLabelUrl.Text = updates + url;
         LinkLabel.Link link = new LinkLabel.Link();
         link.LinkData = url;
         this.linkLabelUrl.Links.Add(updates.Length, url.Length, link);
      }

      #region Assembly Attribute Accessors

      public string AssemblyTitle
      {
         get
         {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
               AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
               if (titleAttribute.Title != "")
               {
                  return titleAttribute.Title;
               }
            }
            return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
         }
      }

      public string AssemblyVersion
      {
         get
         {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
         }
      }

      public string AssemblyDescription
      {
         get
         {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 0)
            {
               return "";
            }
            return ((AssemblyDescriptionAttribute)attributes[0]).Description;
         }
      }

      public string AssemblyProduct
      {
         get
         {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0)
            {
               return "";
            }
            return ((AssemblyProductAttribute)attributes[0]).Product;
         }
      }

      public string AssemblyCopyright
      {
         get
         {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
            {
               return "";
            }
            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
         }
      }

      public string AssemblyCompany
      {
         get
         {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length == 0)
            {
               return "";
            }
            return ((AssemblyCompanyAttribute)attributes[0]).Company;
         }
      }
      #endregion

      private void linkLabelUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         // Pass the URL to the OS
         string url = linkLabelUrl.Text.Substring(e.Link.Start, e.Link.Length);
         Process.Start(new ProcessStartInfo(url));
         linkLabelUrl.LinkVisited = true;
      }
   }
}
