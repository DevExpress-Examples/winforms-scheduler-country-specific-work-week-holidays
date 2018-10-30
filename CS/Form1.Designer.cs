namespace DisplayCustomHolidays {
    partial class Form1 : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
            this.schedulerControl1 = new DevExpress.XtraScheduler.SchedulerControl();
            this.schedulerDataStorage1 = new DevExpress.XtraScheduler.SchedulerDataStorage(this.components);
            this.dateNavigator1 = new DevExpress.XtraScheduler.DateNavigator();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.schedulerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerDataStorage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator1.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // schedulerControl1
            // 
            this.schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.WorkWeek;
            this.schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schedulerControl1.Location = new System.Drawing.Point(0, 0);
            this.schedulerControl1.Name = "schedulerControl1";
            this.schedulerControl1.OptionsView.ToolTipVisibility = DevExpress.XtraScheduler.ToolTipVisibility.Always;
            this.schedulerControl1.Size = new System.Drawing.Size(588, 415);
            this.schedulerControl1.Start = new System.DateTime(2015, 2, 23, 0, 0, 0, 0);
            this.schedulerControl1.DataStorage = this.schedulerDataStorage1;
            this.schedulerControl1.TabIndex = 0;
            this.schedulerControl1.Text = "schedulerControl1";
            this.schedulerControl1.Views.DayView.AllDayAreaScrollBarVisible = false;
            this.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1);
            this.schedulerControl1.Views.FullWeekView.AllDayAreaScrollBarVisible = false;
            this.schedulerControl1.Views.TimelineView.TimelineScrollBarVisible = false;
            this.schedulerControl1.Views.WorkWeekView.AllDayAreaScrollBarVisible = false;
            this.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler2);
            this.schedulerControl1.CustomDrawDayHeader += new DevExpress.XtraScheduler.CustomDrawObjectEventHandler(this.schedulerControl1_CustomDrawDayHeader);
            // 
            // dateNavigator1
            // 
            this.dateNavigator1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            //this.dateNavigator1.CellPadding = new System.Windows.Forms.Padding(2);
            this.dateNavigator1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dateNavigator1.FirstDayOfWeek = System.DayOfWeek.Sunday;
            this.dateNavigator1.Location = new System.Drawing.Point(588, 0);
            this.dateNavigator1.Name = "dateNavigator1";
            this.dateNavigator1.SchedulerControl = this.schedulerControl1;
            this.dateNavigator1.Size = new System.Drawing.Size(230, 415);
            this.dateNavigator1.TabIndex = 1;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2013";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 415);
            this.Controls.Add(this.schedulerControl1);
            this.Controls.Add(this.dateNavigator1);
            this.Name = "Form1";
            this.Text = "Display Custom Holidays";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.schedulerControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerDataStorage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraScheduler.SchedulerControl schedulerControl1;
        private DevExpress.XtraScheduler.SchedulerDataStorage schedulerDataStorage1;
        private DevExpress.XtraScheduler.DateNavigator dateNavigator1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}

