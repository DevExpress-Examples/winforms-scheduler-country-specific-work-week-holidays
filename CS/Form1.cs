using DevExpress.Schedule;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System;
using System.Drawing;


namespace DisplayCustomHolidays {
    public partial class Form1 : DevExpress.XtraEditors.XtraForm {

        DevExpress.XtraEditors.Controls.DatesCollection selectedDates = new DevExpress.XtraEditors.Controls.DatesCollection();

        public Form1() {
            InitializeComponent();
        }

        void dateNavigator1_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e) {
            #region #CustomDrawDayNumberCell
            // Return if the current date is not a holiday.
            // In this case the default drawing will be performed (e.Handled is false)
            if ((FindHoliday(e.Date) == null) || selectedDates.Contains(e.Date)) return;

            // Set the brush to draw the text with.
            Brush textBrush = Brushes.Green;
            Brush backBrush = Brushes.Gold;
            Rectangle drawBounds = e.Bounds;

            if (e.Selected) {
                // Change the brush used to draw the text.
                textBrush = Brushes.Gold;
                backBrush = Brushes.Green;
            }
            // Highlight the cell.
            e.Graphics.FillRectangle(backBrush, drawBounds);

            // Specify text formatting attributes.
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Far;
            strFormat.LineAlignment = StringAlignment.Far;
            // Draw the day number.
            drawBounds.Offset(1, 1);
            e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, textBrush, drawBounds, strFormat);
            // No default drawing is required.
            e.Handled = true;
            #endregion #CustomDrawDayNumberCell
        }

        private void Form1_Load(object sender, EventArgs e) {
            schedulerControl1.BeginUpdate();
            schedulerControl1.OptionsView.FirstDayOfWeek = FirstDayOfWeek.Sunday;
            schedulerControl1.WorkDays.Clear();
            // Specify working days of a week. Friday and Saturday are weekend holidays.
            schedulerControl1.WorkDays.Add(DevExpress.XtraScheduler.WeekDays.Sunday | DevExpress.XtraScheduler.WeekDays.Monday
                  | DevExpress.XtraScheduler.WeekDays.Tuesday | DevExpress.XtraScheduler.WeekDays.Wednesday
                  | DevExpress.XtraScheduler.WeekDays.Thursday);
            GenerateHolidaysFor2015();
            schedulerControl1.EndUpdate();

            this.schedulerControl1.ActiveViewType = SchedulerViewType.Day;
            this.dateNavigator1.HighlightHolidays = true;
            this.dateNavigator1.DateTime = new DateTime(2015, 02, 26);
            // Handle the event to draw a day number.
            this.dateNavigator1.CustomDrawDayNumberCell += dateNavigator1_CustomDrawDayNumberCell;
            this.dateNavigator1.EditDateModified += DateNavigator1_EditDateModified;
        }

        private void DateNavigator1_EditDateModified(object sender, EventArgs e) {
            selectedDates.Clear();
            selectedDates.AddRange(((DateNavigator)sender).Selection);
        }

        // A collection of Kuwait Holidays for 2015.
        private Holiday[] KuwaitHolidays2015 = { 
            new Holiday(new DateTime(2015, 01, 1), "New Year's Day"),
            new Holiday(new DateTime(2015, 01, 3), "The Prophet's Birthday"),
            new Holiday(new DateTime(2015, 02, 25), "National Day"),
            new Holiday(new DateTime(2015, 02, 26), "Liberation Day"),
            new Holiday(new DateTime(2015, 05, 16), "Isra and Miraj"),
            new Holiday(new DateTime(2015, 07, 18), "Eid al Fitr (End of Ramadan)"),
            new Holiday(new DateTime(2015, 07, 19), "Eid al Fitr holiday"),
            new Holiday(new DateTime(2015, 07, 20), "Eid al Fitr holiday"),
            new Holiday(new DateTime(2015, 09, 23), "Waqfat Arafat Day"),
            new Holiday(new DateTime(2015, 09, 24), "Eid al Adha (Feast of Sacrifice)"),
            new Holiday(new DateTime(2015, 09, 25), "Eid al Adha holiday"),
            new Holiday(new DateTime(2015, 09, 26), "Eid al Adha holiday"),
            new Holiday(new DateTime(2015, 10, 15), "Hejira New Year (Islamic New Year)"),
            new Holiday(new DateTime(2015, 12, 24), "The Prophet's Birthday"),
        };

        // This method adds holidays from the KuwaitHolidays2015 collection
        // to the WorkDays collection of the Scheduler Control.
        private void GenerateHolidaysFor2015() {
            foreach (Holiday item in KuwaitHolidays2015) {
                this.schedulerControl1.WorkDays.Add(item);
            }
        }

        private void schedulerControl1_CustomDrawDayHeader(object sender, CustomDrawObjectEventArgs e) {
            #region #CustomDrawDayHeader
            // Check whether the current object is a Day Header.
            SchedulerHeader header = e.ObjectInfo as SchedulerHeader;
            if (header != null) {
                // Check whether the current date is a known holiday.
                Holiday hol = FindHoliday(header.Interval.Start.Date);
                if (hol != null) {
                    header.Caption = hol.DisplayName;
                    e.DrawDefault();
                    // Add the holiday name to the day header's caption.
                    Image img = Image.FromFile("Kuwait.png");
                    Rectangle imgRect = header.ImageBounds;
                    imgRect.Width = header.ImageBounds.Height * img.Width / img.Height;
                    imgRect.X = header.ImageBounds.X + header.ImageBounds.Width - imgRect.Width;
                    e.Graphics.DrawImage(img, imgRect);
                    e.Handled = true;                    
                }
            }
            #endregion #CustomDrawDayHeader
        }

        // This method finds a holiday for the specified date.
        private Holiday FindHoliday(DateTime date) {
            foreach (WorkDay item in schedulerControl1.WorkDays) {
                if (item is Holiday) {
                    Holiday hol = (Holiday)item;
                    if (hol.Date == date)
                        return hol;
                }
            }
            return null;
        }
    }
}