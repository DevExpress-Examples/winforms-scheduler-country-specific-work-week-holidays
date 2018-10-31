using DevExpress.Schedule;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System;
using System.Drawing;


namespace DisplayCustomHolidays {
    public partial class Form1 : DevExpress.XtraEditors.XtraForm {

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // Specify a custom cell style provider to highlight cells that meet certain criteria.
            this.dateNavigator1.CellStyleProvider = new CustomCellStyleProvider(this.dateNavigator1);
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
                    e.Cache.DrawImage(img, imgRect);
                    e.Handled = true;
                }
            }
            #endregion #CustomDrawDayHeader
        }

        Holiday FindHoliday(DateTime date) {
            WorkDaysCollection workDays = this.schedulerControl1.WorkDays;
            foreach (WorkDay item in workDays) {
                if (item is Holiday) {
                    Holiday hol = (Holiday)item;
                    if (hol.Date == date)
                        return hol;
                }
            }
            return null;
        }
    }
    #region #CustomCellStyleProvider
    public class CustomCellStyleProvider : ICalendarCellStyleProvider {
        DateNavigator dateNavigator;

        public CustomCellStyleProvider(DateNavigator calendar) {
            this.dateNavigator = calendar;
        }

        public void UpdateAppearance(CalendarCellStyle cell) {
            WorkDaysCollection workDays = this.dateNavigator.SchedulerControl.WorkDays;
            if (workDays.IsHoliday(cell.Date)) {
                switch (cell.State) {
                    // Highlight dates hovered over with the mouse.
                    case (DevExpress.Utils.Drawing.ObjectState.Hot):
                        cell.Appearance.ForeColor = Color.DarkGoldenrod;
                        cell.Appearance.BackColor = Color.PaleGreen;
                        break;
                    // Highlight selection.
                    case (DevExpress.Utils.Drawing.ObjectState.Selected):
                        cell.Appearance.ForeColor = Color.Gold;
                        cell.Appearance.BackColor = Color.Green;
                        break;
                    default:
                        cell.Appearance.ForeColor = Color.Green;
                        cell.Appearance.BackColor = Color.Gold;
                        break;
                }
            }
            // Display an image for the dates which contains appointments.
            if (cell.IsSpecial) {
                cell.Appearance.Font = new Font(cell.Appearance.Font, FontStyle.Regular);
                cell.Image = Image.FromFile("appointment_icon.png");
            }
        }
    }
    #endregion #CustomCellStyleProvider
}
