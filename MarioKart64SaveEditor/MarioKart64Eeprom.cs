using System;

namespace MarioKart64SaveEditor
{
   class MarioKart64Eeprom
   {
      public enum AudioSetting { Stereo, Headphone, Unknown, Mono };
      public CourseTimes[] Courses = new CourseTimes[16];
      public GrandPrix[] GPTrophies = new GrandPrix[4]; // one for each of 50cc, 100cc, 150cc, Extra
      public AudioSetting audio;
      public byte data185;
      public byte[] checksum186 = new byte[2]; // checksum for 0x180-0x184 (stored in 0x186-0x187)
      public RecordTime[] mushroomCup1st = new RecordTime[4];
      public RecordTime[] flowerCup1st = new RecordTime[4];
      public RecordTime[] mushroomCupLap = new RecordTime[4];
      public RecordTime[] flowerCupLap = new RecordTime[4];
      public byte[] data1B8 = new byte[6];     // 0x1B8-0x1BD
      public byte[] checksum1BE = new byte[2]; // checksum for 0x188-0x0x1BE-0x1BF
      public RecordTime[] starCup1st = new RecordTime[4];
      public RecordTime[] specialCup1st = new RecordTime[4];
      public RecordTime[] starCupLap = new RecordTime[4];
      public RecordTime[] specialCupLap = new RecordTime[4];
      public byte[] data1F0 = new byte[6];     // 0x1F0-0x1F5
      public byte[] checksum1F6 = new byte[2]; // 0x1F6-0x1F7
      // 0x1F8-0x1FF: copy of 0x180-0x187

      public static MarioKart64Eeprom FromBytes(byte[] data, int offset)
      {
         MarioKart64Eeprom eeprom = new MarioKart64Eeprom();
         // 0x000-0x17F: course times
         for (int i = 0; i < eeprom.Courses.Length; i++)
         {
            eeprom.Courses[i] = CourseTimes.FromBytes(data, offset + i * 0x18);
         }
         // 0x180-0x183: grand prix trophies
         for (int i = 0; i < eeprom.GPTrophies.Length; i++)
         {
            eeprom.GPTrophies[i] = GrandPrix.FromBytes(data, offset + 0x180 + i);
         }
         // 0x184: audio setting
         eeprom.audio = (AudioSetting)data[offset + 0x184];
         // 0x185: unused/padding
         eeprom.data185 = data[offset + 0x185];

         // 0x186-0x187: checksum of 0x180-0x184
         Array.Copy(data, offset + 0x180, eeprom.checksum186, 0, eeprom.checksum186.Length);
         // TODO: verify checksum
         byte cksum = ComputeChecksum180(data, offset + 0x180);

         // 0x188-0x193: copies of mushroom cup 1st record times
         for (int i = 0; i < eeprom.mushroomCup1st.Length; i++)
         {
            eeprom.mushroomCup1st[i] = RecordTime.FromBytes(data, offset + 0x188 + i * 3);
         }
         // 0x194-0x19F: copies of flower cup 1st record times
         for (int i = 0; i < eeprom.flowerCup1st.Length; i++)
         {
            eeprom.flowerCup1st[i] = RecordTime.FromBytes(data, offset + 0x194 + i * 3);
         }
         // 0x1A0-0x1AB: copies of mushroom cup lap record times
         for (int i = 0; i < eeprom.mushroomCupLap.Length; i++)
         {
            eeprom.mushroomCupLap[i] = RecordTime.FromBytes(data, offset + 0x1A0 + i * 3);
         }
         // 0x1AC-0x1B7: copies of flower cup lap record times
         for (int i = 0; i < eeprom.flowerCupLap.Length; i++)
         {
            eeprom.flowerCupLap[i] = RecordTime.FromBytes(data, offset + 0x1AC + i * 3);
         }
         // 0x1B8-0x1BD: TODO copies of previous unused/padding? (00 or AA)
         Array.Copy(data, offset + 0x1B8, eeprom.data1B8, 0, eeprom.data1B8.Length);
         // 0x1BE-0x1BF: checksum for 0x188-0x1BD
         Array.Copy(data, offset + 0x1BE, eeprom.checksum1BE, 0, eeprom.checksum1BE.Length);
         // 0x1C0-0x1CB: copies of star cup 1st record times
         for (int i = 0; i < eeprom.starCup1st.Length; i++)
         {
            eeprom.starCup1st[i] = RecordTime.FromBytes(data, offset + 0x1C0 + i * 3);
         }
         // 0x1CC-0x1D7: copies of special cup 1st record times
         for (int i = 0; i < eeprom.specialCup1st.Length; i++)
         {
            eeprom.specialCup1st[i] = RecordTime.FromBytes(data, offset + 0x1CC + i * 3);
         }
         // 0x1D8-0x1E3: copies of star cup lap record times
         for (int i = 0; i < eeprom.starCupLap.Length; i++)
         {
            eeprom.starCupLap[i] = RecordTime.FromBytes(data, offset + 0x1D8 + i * 3);
         }
         // 0x1E4-0x1EF: copies of special cup lap record times
         for (int i = 0; i < eeprom.specialCupLap.Length; i++)
         {
            eeprom.specialCupLap[i] = RecordTime.FromBytes(data, offset + 0x1E4 + i * 3);
         }
         // 0x1F0-0x1F5: TODO copies of previous unused/padding? (00 or AA)
         Array.Copy(data, offset + 0x1F0, eeprom.data1F0, 0, eeprom.data1F0.Length);
         // 0x1F6-0x1F7: checksum for 0x1C0-0x1F5
         Array.Copy(data, offset + 0x1F6, eeprom.checksum1F6, 0, eeprom.checksum186.Length);
         // 0x1F8-0x1FF: copy of 0x180-0x187
         return eeprom;
      }
      
      public byte[] ToBytes()
      {
         byte[] serialized = new byte[0x200];
         // 0x000-0x17F: course times
         for (int i = 0; i < Courses.Length; i++)
         {
            Courses[i].ToBytes(serialized, 0x18 * i);
         }
         // 0x180-0x183: grand prix trophies
         for (int i = 0; i < GPTrophies.Length; i++)
         {
            serialized[0x180 + i] = GPTrophies[i].ToByte();
         }
         // 0x184: Audio setting
         serialized[0x184] = (byte)audio;
         // 0x185: unused/padding
         serialized[0x185] = data185;

         // 0x186-0x187: checksum of 0x180-0x184
         serialized[0x186] = ComputeChecksum180(serialized, 0x180);
         serialized[0x187] = (byte)((serialized[0x186] + 0x5A) & 0xFF);

         // 0x188-0x193: copies of mushroom cup 1st record times
         for (int i = 0; i < mushroomCup1st.Length; i++)
         {
            mushroomCup1st[i] = Courses[0 + i].TrackRecords[0];
            mushroomCup1st[i].ToBytes(serialized, 0x188 + i * 3);
         }
         // 0x194-0x19F: copies of flower cup 1st record times
         for (int i = 0; i < flowerCup1st.Length; i++)
         {
            flowerCup1st[i] = Courses[4 + i].TrackRecords[0];
            flowerCup1st[i].ToBytes(serialized, 0x194 + i * 3);
         }
         // 0x1A0-0x1AB: copies of mushroom cup lap record times
         for (int i = 0; i < mushroomCupLap.Length; i++)
         {
            mushroomCupLap[i] = Courses[0 + i].LapRecord;
            mushroomCupLap[i].ToBytes(serialized, 0x1A0 + i * 3);
         }
         // 0x1AC-0x1B7: copies of flower cup lap record times
         for (int i = 0; i < flowerCupLap.Length; i++)
         {
            flowerCupLap[i] = Courses[4 + i].LapRecord;
            flowerCupLap[i].ToBytes(serialized, 0x1AC + i * 3);
         }
         // 0x1B8-0x1BD: TODO copies of previous unused/padding? (00 or AA)
         Array.Copy(data1B8, 0, serialized, 0x1B8, data1B8.Length);
         // 0x1BE-0x1BF: checksum for 0x188-0x1BD
         serialized[0x1BE] = ComputeChecksum1F6(serialized, 0x188);
         serialized[0x1BF] = (byte)((serialized[0x1BE] + 0x5A) & 0xFF);

         // 0x1C0-0x1CB: copies of star cup 1st record times
         for (int i = 0; i < starCup1st.Length; i++)
         {
            starCup1st[i] = Courses[8 + i].TrackRecords[0];
            starCup1st[i].ToBytes(serialized, 0x1C0 + i * 3);
         }
         // 0x1CC-0x1D7: copies of special cup 1st record times
         for (int i = 0; i < specialCup1st.Length; i++)
         {
            specialCup1st[i] = Courses[12 + i].TrackRecords[0];
            specialCup1st[i].ToBytes(serialized, 0x1CC + i * 3);
         }
         // 0x1D8-0x1E3: copies of star cup lap record times
         for (int i = 0; i < starCupLap.Length; i++)
         {
            starCupLap[i] = Courses[8 + i].LapRecord;
            starCupLap[i].ToBytes(serialized, 0x1D8 + i * 3);
         }
         // 0x1E4-0x1EF: copies of special cup lap record times
         for (int i = 0; i < specialCupLap.Length; i++)
         {
            specialCupLap[i] = Courses[12 + i].LapRecord;
            specialCupLap[i].ToBytes(serialized, 0x1E4 + i * 3);
         }
         // 0x1F0-0x1F5: TODO copies of previous unused/padding? (00 or AA)
         Array.Copy(data1F0, 0, serialized, 0x1F0, data1F0.Length);
         // 0x1F6-0x1F7: checksum for 0x1C0-0x1F5
         serialized[0x1F6] = ComputeChecksum1F6(serialized, 0x1C0);
         serialized[0x1F7] = (byte)((serialized[0x1F6] + 0x5A) & 0xFF);
         // 0x1F8-0x1FF: copy of 0x180-0x187
         Array.Copy(serialized, 0x180, serialized, 0x1F8, 8);
         return serialized;
      }

      // calculate checksum for trophies and audio setting (0x180 - 0x184)
      public static byte ComputeChecksum180(byte[] data, int offset)
      {
         int checksum = 0;
         for (int i = 0; i < 5; i++)
         {
            checksum += (data[offset + i] + 1) * (i + 1) + i;
         }
         return (byte)(checksum & 0xFF);
      }

      // calculate checksum for copies of 1st and Lap record times
      public static byte ComputeChecksum1F6(byte[] data, int offset)
      {
         int checksum = 0;
         int curOffset = offset;
         int recOffset = curOffset;
         for (int record = 0; record < 3; record++)
         {
            int loopOffset = curOffset + 1;
            curOffset += 0x11;
            checksum += (data[recOffset] + 1) * (record + 1);
            for (int byteOff = 1; byteOff != 0x11; byteOff += 0x4)
            {
               int mult2 = (data[loopOffset] + 1) * (record + 1);
               checksum += mult2 + byteOff;
               int mult3 = (data[loopOffset + 1] + 1) * (record + 1);
               checksum += mult3 + byteOff + 1;
               int mult4 = (data[loopOffset + 2] + 1) * (record + 1);
               checksum += mult4 + byteOff + 2;
               int mult5 = (data[loopOffset + 3] + 1) * (record + 1);
               checksum += mult5 + byteOff + 3;
               loopOffset += 4;
            }
            recOffset += 0x11;
         }
         return (byte)(checksum & 0xFF);
      }
   }

   class CourseTimes
   {
      public RecordTime[] TrackRecords = new RecordTime[5];
      public RecordTime LapRecord = new RecordTime();
      public bool RecordsExist { get; set; }
      public uint Unused { get; set; }
      public byte Checksum { get; set; }

      private static string[] ranking = new string[] { "1st", "2nd", "3rd", "4th", "5th" };

      public static CourseTimes FromBytes(byte[] data, int offset)
      {
         CourseTimes times = new CourseTimes();
         for (int i = 0; i < times.TrackRecords.Length; i++)
         {
            times.TrackRecords[i] = RecordTime.FromBytes(data, offset + i * 3);
            times.TrackRecords[i].Name = ranking[i];
         }
         times.LapRecord = RecordTime.FromBytes(data, offset + 0xF);
         times.LapRecord.Name = "Lap";
         times.RecordsExist = data[offset + 0x12] > 0;
         times.Unused = LE.U32(data, offset + 0x13);
         times.Checksum = data[offset + 0x17];

         // TODO: verify checksum
         byte cksum = ComputeChecksum(data, offset);

         return times;
      }

      // calculate course record checksum
      public static byte ComputeChecksum(byte[] data, int offset)
      {
         int checksum = 0;
         for (int rec = 0; rec < 7; rec++)
         {
            for (int i = 0; i < 3; i++)
            {
               byte val = data[offset + rec * 3 + i];
               checksum += (val * (i + 1)) + rec;
            }
         }
         return (byte)(checksum & 0xFF);
      }

      // serialize to byte stream
      public void ToBytes(byte[] data, int offset)
      {
         // sort track records and update RecordsExist field before serializing
         Array.Sort(TrackRecords);
         updateRecordsExist();
         for (int i = 0; i < TrackRecords.Length; i++)
         {
            TrackRecords[i].ToBytes(data, offset + i * 3);
         }
         LapRecord.ToBytes(data, offset + 0xF);
         data[offset + 0x12] = (byte)(RecordsExist ? 0x01 : 0x00);
         LE.ToBytes(Unused, data, offset + 0x13);
         data[offset + 0x17] = ComputeChecksum(data, offset);
      }

      // update RecordsExist based on record times
      public void updateRecordsExist()
      {
         foreach (RecordTime time in TrackRecords)
         {
            if (time.Minute < 100)
            {
               RecordsExist = true;
               return;
            }
         }
         RecordsExist = (LapRecord.Minute < 100);
      }
   }

   public class RecordTime : IComparable<RecordTime>
   {
      public string Name { get; set; }
      public enum Player { Mario, Luigi, Yoshi, Toad, DK, Wario, Peach, Bowser, Unused, None };
      public Player Character { get; set; }
      public uint TotalTime { get; set; }

      // breakdown of TotalTime into Minute/Second/CentiSecond properties
      public uint Minute
      {
         get
         {
            return TotalTime / 6000;
         }
         set
         {
            uint tmp = TotalTime % 6000;
            TotalTime = tmp + value * 6000;
         }
      }
      public uint Second
      {
         get
         {
            return (TotalTime / 100) % 60;
         }
         set
         {
            uint tmp = Centisecond + Minute * 6000;
            TotalTime = tmp + value * 100;
         }
      }

      public uint Centisecond
      {
         get
         {
            return TotalTime % 100;
         }
         set
         {
            uint tmp = Second * 100 + Minute * 6000;
            TotalTime = tmp + value;
         }
      }

      public int CompareTo(RecordTime p)
      {
         return this.TotalTime.CompareTo(p.TotalTime);
      }

      public static RecordTime FromBytes(byte[] data, int offset)
      {
         RecordTime time = new RecordTime();

         // records are stored as little endian 24-bit:
         // T2 T1 CT: time = T T1 T2, character = C
         // e.g. default value: C0 27 09 = 927C0 (600000 = 10 minutes), character = 0 (Mario)

         uint combined = LE.U32(data, offset);
         time.TotalTime = 0x0FFFFF & combined;
         uint character = 0x7 & (combined >> 20);

         time.Character = (Player)character;
         time.Name = "";

         return time;
      }

      public void ToBytes(byte[] data, int offset)
      {
         uint combined = (TotalTime & 0x0FFFFF) | (uint)Character << 20;
         data[offset] = (byte)(combined & 0xFF);
         data[offset + 1] = (byte)((combined >> 8) & 0xFF);
         data[offset + 2] = (byte)((combined >> 16) & 0xFF);
      }

      public override string ToString()
      {
         if (Minute < 100)
         {
            return String.Format("{0}: {1:D2}\'{2:D2}\"{3:D2}  {4}", Name, Minute, Second, Centisecond, Character.ToString());
         }
         else
         {
            return Name + ": --\'--\"--  -----";
         }
      }
   }

   public enum Trophy { None, Bronze, Silver, Gold }

   // Types: 50cc, 100cc, 150cc, Extra
   public class GrandPrix
   {
      // one trophy each for Special, Star, Flower, Mushroom
      public Trophy[] Trophies = new Trophy[4];

      public static GrandPrix FromBytes(byte[] data, int offset)
      {
         GrandPrix prix = new GrandPrix();

         for (int i = 0; i < 4; i++)
         {
            prix.Trophies[i] = (Trophy)((data[offset] >> (i * 2)) & 0x3);
         }

         return prix;
      }

      public byte ToByte()
      {
         byte data = 0x0;
         for (int i = 0; i < 4; i++)
         {
            data = (byte)(data | ((int)Trophies[i]) << (i * 2));
         }
         return data;
      }
   }
}
