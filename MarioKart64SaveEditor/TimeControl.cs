using System.Windows.Forms;

namespace MarioKart64SaveEditor
{
   public partial class TimeControl : UserControl
   {
      private RecordTime _time = null;
      public RecordTime Time
      {
         get { return _time; }
         set
         {
            _time = value;
            numericMinute.DataBindings.Clear();
            numericSecond.DataBindings.Clear();
            numericCentisecond.DataBindings.Clear();
            if (_time != null)
            {
               numericMinute.DataBindings.Add("Value", _time, "Minute");
               numericSecond.DataBindings.Add("Value", _time, "Second");
               numericCentisecond.DataBindings.Add("Value", _time, "Centisecond");
               comboCharacter.SelectedIndex = (int)_time.Character;
            }
         }
      }
      public string Title
      {
         get { return labelTitle.Text; }
         set { labelTitle.Text = value; }
      }
      public TimeControl()
      {
         InitializeComponent();
      }

      private void comboCharacter_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (_time != null)
         {
            _time.Character = (RecordTime.Player)comboCharacter.SelectedIndex;
         }
      }
   }
}
