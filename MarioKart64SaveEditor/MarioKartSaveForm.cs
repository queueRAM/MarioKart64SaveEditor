using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MarioKart64SaveEditor
{
   public partial class MarioKartSaveForm : Form
   {
      private byte[] eepromRaw = null;
      private string savePath = null;
      private MarioKart64Eeprom eeprom = null;
      private int selectedCourseIdx = -1;

      public MarioKartSaveForm()
      {
         InitializeComponent();

         trophy50Mushroom.Tag = new PictureTag { col = 0, row = 0 };
         trophy100Mushroom.Tag = new PictureTag { col = 0, row = 1 };
         trophy150Mushroom.Tag = new PictureTag { col = 0, row = 2 };
         trophyExtraMushroom.Tag = new PictureTag { col = 0, row = 3 };
         trophy50Flower.Tag = new PictureTag { col = 1, row = 0 };
         trophy100Flower.Tag = new PictureTag { col = 1, row = 1 };
         trophy150Flower.Tag = new PictureTag { col = 1, row = 2 };
         trophyExtraFlower.Tag = new PictureTag { col = 1, row = 3 };
         trophy50Star.Tag = new PictureTag { col = 2, row = 0 };
         trophy100Star.Tag = new PictureTag { col = 2, row = 1 };
         trophy150Star.Tag = new PictureTag { col = 2, row = 2 };
         trophyExtraStar.Tag = new PictureTag { col = 2, row = 3 };
         trophy50Special.Tag = new PictureTag { col = 3, row = 0 };
         trophy100Special.Tag = new PictureTag { col = 3, row = 1 };
         trophy150Special.Tag = new PictureTag { col = 3, row = 2 };
         trophyExtraSpecial.Tag = new PictureTag { col = 3, row = 3 };

         // handle arguments passed in the command line
         string[] args = Environment.GetCommandLineArgs();
         if (args.Length > 1)
         {
            LoadFile(args[1]);
         }
      }

      private void LoadFile(string filename)
      {
         FileInfo info = new FileInfo(filename);
         if (info.Exists && info.Length >= 0x200)
         {
            savePath = filename;
            eepromRaw = System.IO.File.ReadAllBytes(filename);
            eeprom = MarioKart64Eeprom.FromBytes(eepromRaw, 0);
            UpdateDisplay();
            filenameLabel.ForeColor = Color.Black;
            filenameLabel.Text = filename;
            saveAsToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
         }
         else
         {
            filenameLabel.ForeColor = Color.Red;
            filenameLabel.Text = "Error loading " + filename;
         }
      }

      private void UpdateDisplay()
      {
         listRecords.Enabled = (eeprom != null);
         if (eeprom != null)
         {
            // Records exist
            for (int i = 0; i < eeprom.Courses.Length; i++)
            {
               if (eeprom.Courses[i].RecordsExist)
               {
                  listRecords.Items[i].Font = new Font(listRecords.Items[i].Font, listRecords.Items[i].Font.Style | FontStyle.Bold);
               }
               else
               {
                  listRecords.Items[i].Font = new Font(listRecords.Items[i].Font, listRecords.Items[i].Font.Style & ~FontStyle.Bold);
               }
            }

            timeControl1st.Enabled = false;
            timeControl1st.Time = null;
            timeControl2nd.Enabled = false;
            timeControl2nd.Time = null;
            timeControl3rd.Enabled = false;
            timeControl3rd.Time = null;
            timeControl4th.Enabled = false;
            timeControl4th.Time = null;
            timeControl5th.Enabled = false;
            timeControl5th.Time = null;
            timeControlLap.Enabled = false;
            timeControlLap.Time = null;

            // Trophies
            // 50cc
            setTrophy(trophy50Mushroom, eeprom.GPTrophies[0].Trophies[0]);
            setTrophy(trophy50Flower, eeprom.GPTrophies[0].Trophies[1]);
            setTrophy(trophy50Star, eeprom.GPTrophies[0].Trophies[2]);
            setTrophy(trophy50Special, eeprom.GPTrophies[0].Trophies[3]);
            // 100cc
            setTrophy(trophy100Mushroom, eeprom.GPTrophies[1].Trophies[0]);
            setTrophy(trophy100Flower, eeprom.GPTrophies[1].Trophies[1]);
            setTrophy(trophy100Star, eeprom.GPTrophies[1].Trophies[2]);
            setTrophy(trophy100Special, eeprom.GPTrophies[1].Trophies[3]);
            // 150cc
            setCup(trophy150Mushroom, eeprom.GPTrophies[2].Trophies[0]);
            setCup(trophy150Flower, eeprom.GPTrophies[2].Trophies[1]);
            setCup(trophy150Star, eeprom.GPTrophies[2].Trophies[2]);
            setCup(trophy150Special, eeprom.GPTrophies[2].Trophies[3]);
            // Extra
            setCup(trophyExtraMushroom, eeprom.GPTrophies[3].Trophies[0]);
            setCup(trophyExtraFlower, eeprom.GPTrophies[3].Trophies[1]);
            setCup(trophyExtraStar, eeprom.GPTrophies[3].Trophies[2]);
            setCup(trophyExtraSpecial, eeprom.GPTrophies[3].Trophies[3]);

            // Audio
            switch (eeprom.audio)
            {
               case MarioKart64Eeprom.AudioSetting.Stereo: comboAudio.SelectedIndex = 0; break;
               case MarioKart64Eeprom.AudioSetting.Headphone: comboAudio.SelectedIndex = 1; break;
               case MarioKart64Eeprom.AudioSetting.Mono: comboAudio.SelectedIndex = 2; break;
            }
         }
      }

      private void setTrophy(PictureBox picture, Trophy trophy)
      {
         switch (trophy)
         {
            case Trophy.None: picture.Image = null; break;
            case Trophy.Bronze: picture.Image = Properties.Resources.BronzeTrophy; break;
            case Trophy.Silver: picture.Image = Properties.Resources.SilverTrophy; break;
            case Trophy.Gold: picture.Image = Properties.Resources.GoldTrophy; break;
         }
      }

      private void setCup(PictureBox picture, Trophy trophy)
      {
         switch (trophy)
         {
            case Trophy.None: picture.Image = null; break;
            case Trophy.Bronze: picture.Image = Properties.Resources.BronzeCup; break;
            case Trophy.Silver: picture.Image = Properties.Resources.SilverCup; break;
            case Trophy.Gold: picture.Image = Properties.Resources.GoldCup; break;
         }
      }

      private void listRecords_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (listRecords.SelectedIndices.Count > 0)
         {
            labelTrackName.Text = listRecords.SelectedItems[0].Text + " Records:";
            if (eeprom != null)
            {
               // sort records and update course list display if valid records change
               if (selectedCourseIdx >= 0)
               {
                  CourseTimes selectedCourse = eeprom.Courses[selectedCourseIdx];
                  // sort
                  Array.Sort(selectedCourse.TrackRecords);
                  // update display
                  bool prevRecordsExist = selectedCourse.RecordsExist;
                  selectedCourse.updateRecordsExist();
                  if (prevRecordsExist != selectedCourse.RecordsExist)
                  {
                     Font curFont = listRecords.Items[selectedCourseIdx].Font;
                     Font recordFont;
                     if (selectedCourse.RecordsExist)
                     {
                        recordFont = new Font(curFont, curFont.Style | FontStyle.Bold);
                     }
                     else
                     {
                        recordFont = new Font(curFont, curFont.Style & ~FontStyle.Bold);
                     }
                     listRecords.Items[selectedCourseIdx].Font = recordFont;
                  }
               }
               selectedCourseIdx = listRecords.SelectedIndices[0];
               CourseTimes course = eeprom.Courses[selectedCourseIdx];
               timeControl1st.Enabled = true;
               timeControl1st.Time = course.TrackRecords[0];
               timeControl2nd.Enabled = true;
               timeControl2nd.Time = course.TrackRecords[1];
               timeControl3rd.Enabled = true;
               timeControl3rd.Time = course.TrackRecords[2];
               timeControl4th.Enabled = true;
               timeControl4th.Time = course.TrackRecords[3];
               timeControl5th.Enabled = true;
               timeControl5th.Time = course.TrackRecords[4];
               timeControlLap.Enabled = true;
               timeControlLap.Time = course.LapRecord;
            }
         }
      }


      private void SaveEeprom(string filename)
      {
         if (eeprom != null)
         {
            if (filename != null)
            {
               byte[] fileBytes = eeprom.ToBytes();
               SaveBinFile(filename, fileBytes, 0, fileBytes.Length);
            }
         }
      }

      private static bool SaveBinFile(string filePath, byte[] data, int start, int end)
      {
         try
         {
            FileStream outStream = File.OpenWrite(filePath);
            outStream.Write(data, start, end - start);
            outStream.Close();
            return true;
         }
         catch
         {
            return false;
         }
      }

      private void openToolStripMenuItem_Click(object sender, EventArgs e)
      {
         OpenFileDialog ofd = new OpenFileDialog();

         ofd.Filter = "EEPROM Save Files (*.eep;*.sav)|*.eep;*.sav|All Files (*.*)|*.*";
         ofd.FilterIndex = 1;

         DialogResult dresult = ofd.ShowDialog();

         if (dresult == DialogResult.OK)
         {
            LoadFile(ofd.FileName);
         }
      }

      private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
      {
         // TODO: confirm save
         this.Close();
      }

      private void saveToolStripMenuItem_Click(object sender, EventArgs e)
      {
         SaveEeprom(savePath);
      }

      private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         SaveFileDialog saveFileDialog = new SaveFileDialog();
         saveFileDialog.Filter = "Mario Kart 64 EEPROM (*.eep;*.sav)|*.eep;*.sav";
         saveFileDialog.Title = "Save As...";
         DialogResult result = saveFileDialog.ShowDialog();

         if (result == DialogResult.OK && saveFileDialog.FileName != "")
         {
            savePath = saveFileDialog.FileName;
            SaveEeprom(savePath);
         }
      }

      private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
      {
         new AboutBox().ShowDialog(this);
      }

      private void cycleTrophy(PictureBox picture, int row, int col)
      {
         if (eeprom != null)
         {
            int trophy = (int)eeprom.GPTrophies[row].Trophies[col];
            trophy = (trophy + 1) & 0x3;
            eeprom.GPTrophies[row].Trophies[col] = (Trophy)trophy;
            if (row < 2)
            {
               setTrophy(picture, eeprom.GPTrophies[row].Trophies[col]);
            }
            else
            {
               setCup(picture, eeprom.GPTrophies[row].Trophies[col]);
            }
         }
      }

      private void trophy_MouseDown(object sender, MouseEventArgs e)
      {
         PictureBox picture = (PictureBox)sender;
         PictureTag tag = (PictureTag)picture.Tag;
         cycleTrophy(picture, tag.row, tag.col);
      }

      private void comboAudio_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (eeprom != null)
         {
            switch (comboAudio.SelectedIndex)
            {
               case 0: eeprom.audio = MarioKart64Eeprom.AudioSetting.Stereo; break;
               case 1: eeprom.audio = MarioKart64Eeprom.AudioSetting.Headphone; break;
               case 2: eeprom.audio = MarioKart64Eeprom.AudioSetting.Mono; break;
            }
         }
      }

      private void checkExist_CheckedChanged(object sender, EventArgs e)
      {
         if (eeprom != null && listRecords.SelectedIndices.Count > 0)
         {
            int idx = listRecords.SelectedIndices[0];
            CourseTimes course = eeprom.Courses[idx];
            Font curFont = listRecords.SelectedItems[0].Font;
            Font recordFont;
            if (course.RecordsExist)
            {
               recordFont = new Font(curFont, curFont.Style | FontStyle.Bold);
            }
            else
            {
               recordFont = new Font(curFont, curFont.Style & ~FontStyle.Bold);
            }
            listRecords.SelectedItems[0].Font = recordFont;
         }
      }
   }

   public class PictureTag
   {
      public int row { get; set; }
      public int col { get; set; }
   }
}
