using System;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.Schedule;


namespace Holidays_Custom {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            schedulerControl1.BeginUpdate();
            schedulerControl1.WorkDays.Clear();
            schedulerControl1.WorkDays.Add(DevExpress.XtraScheduler.WeekDays.Sunday | DevExpress.XtraScheduler.WeekDays.Monday
                  | DevExpress.XtraScheduler.WeekDays.Tuesday | DevExpress.XtraScheduler.WeekDays.Wednesday
                  | DevExpress.XtraScheduler.WeekDays.Thursday);

            GenerateHolidaysFor2008();
            schedulerControl1.EndUpdate();

            schedulerControl1.Start = DateTime.Now.Date;
            schedulerControl1.ActiveViewType = SchedulerViewType.WorkWeek;
        }

        // A collection of Kuwait Holidays for 2008.
        private Holiday[] KuwaitHolidays2008 = { 
            new Holiday(new DateTime(2008, 1, 1), "New Year's Day"),
            new Holiday(new DateTime(2008, 1, 10), "Hejira New Year (Islamic New Year)"),
            new Holiday(new DateTime(2008, 2, 24), "Bridge Public Holiday"),
            new Holiday(new DateTime(2008, 2, 25), "National Day"),
            new Holiday(new DateTime(2008, 2, 26), "Liberation Day"),
            new Holiday(new DateTime(2008, 3, 20), "The Prophet's birthday"),
            new Holiday(new DateTime(2008, 5, 15), "Public Holiday (National mourning for Father Amir"),
            new Holiday(new DateTime(2008, 5, 16), "Public Holiday (National mourning for Father Amir"),
            new Holiday(new DateTime(2008, 7, 31), "The Prophet's Ascension"),
            new Holiday(new DateTime(2008, 9, 30), "Government Holiday"),
            new Holiday(new DateTime(2008, 10, 1), "Eid al Fitr (End of Ramadan)"),
            new Holiday(new DateTime(2008, 10, 2), "Eid al Fitr holiday"),
            new Holiday(new DateTime(2008, 10, 3), "Eid al Fitr holiday"),
            new Holiday(new DateTime(2008, 10, 4), "Eid al Fitr holiday"),
            new Holiday(new DateTime(2008, 12, 7), "Eid al Adha holiday"),
            new Holiday(new DateTime(2008, 12, 8), "Eid al Adha (feast of sacrifice)"),
            new Holiday(new DateTime(2008, 12, 9), "Eid al Adha holiday"),
            new Holiday(new DateTime(2008, 12, 10), "Eid al Adha holiday"),
            new Holiday(new DateTime(2008, 12, 11), "Eid al Adha holiday"),
            new Holiday(new DateTime(2008, 12, 29), "Hejira New Year (Islamic New Year)"),
        };

        // This method adds holidays from the KuwaitHolidays2007 collection
        // to the WorkDays collection of the Scheduler Control.
        private void GenerateHolidaysFor2008() {
            foreach (Holiday item in KuwaitHolidays2008) {
                this.schedulerControl1.WorkDays.Add(item);
            }
        }

        private void schedulerControl1_CustomDrawDayHeader(object sender, CustomDrawObjectEventArgs e) {
            // Check whether the current object is a Day Header.
            SchedulerHeader header = e.ObjectInfo as SchedulerHeader;
            if (header != null) {
                // Check whether the current date is a known holiday.
                Holiday hol = FindHoliday(header.Interval.Start.Date);
                if (hol != null) {
                    // Add the holiday name to the day header's caption.
                    header.Caption = String.Format("{0} ({1})", hol.DisplayName, hol.Date.ToShortDateString());
                }
            }
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