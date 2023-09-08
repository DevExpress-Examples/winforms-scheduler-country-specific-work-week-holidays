Imports DevExpress.Schedule
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports System
Imports System.Drawing

Namespace DisplayCustomHolidays

    Public Partial Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            ' Specify a custom cell style provider to highlight cells that meet certain criteria.
            schedulerControl1.BeginUpdate()
            schedulerControl1.OptionsView.FirstDayOfWeek = FirstDayOfWeek.Sunday
            schedulerControl1.WorkDays.Clear()
            ' Specify working days of a week. Friday and Saturday are weekend holidays.
            schedulerControl1.WorkDays.Add(WeekDays.Sunday Or WeekDays.Monday Or WeekDays.Tuesday Or WeekDays.Wednesday Or WeekDays.Thursday)
            GenerateHolidaysFor2015()
            schedulerControl1.EndUpdate()
            schedulerControl1.ActiveViewType = SchedulerViewType.Day
            dateNavigator1.CellStyleProvider = New CustomCellStyleProvider(schedulerControl1.WorkDays)
            dateNavigator1.HighlightHolidays = True
            dateNavigator1.DateTime = New DateTime(2015, 02, 26)
            For day As Integer = 9 To 14 - 1
                Dim startDate As Date = New DateTime(2015, 02, day, 12, 0, 0)
                Dim endDate As Date = startDate.AddHours(2)
                Dim appointment = schedulerDataStorage1.Appointments.CreateAppointment(AppointmentType.Normal, startDate, endDate, startDate.ToLongDateString())
                schedulerDataStorage1.Appointments.Add(appointment)
            Next
        End Sub

        ' A collection of Kuwait Holidays for 2015.
        Private KuwaitHolidays2015 As Holiday() = {New Holiday(New DateTime(2015, 01, 1), "New Year's Day"), New Holiday(New DateTime(2015, 01, 3), "The Prophet's Birthday"), New Holiday(New DateTime(2015, 02, 25), "National Day"), New Holiday(New DateTime(2015, 02, 26), "Liberation Day"), New Holiday(New DateTime(2015, 05, 16), "Isra and Miraj"), New Holiday(New DateTime(2015, 07, 18), "Eid al Fitr (End of Ramadan)"), New Holiday(New DateTime(2015, 07, 19), "Eid al Fitr holiday"), New Holiday(New DateTime(2015, 07, 20), "Eid al Fitr holiday"), New Holiday(New DateTime(2015, 09, 23), "Waqfat Arafat Day"), New Holiday(New DateTime(2015, 09, 24), "Eid al Adha (Feast of Sacrifice)"), New Holiday(New DateTime(2015, 09, 25), "Eid al Adha holiday"), New Holiday(New DateTime(2015, 09, 26), "Eid al Adha holiday"), New Holiday(New DateTime(2015, 10, 15), "Hejira New Year (Islamic New Year)"), New Holiday(New DateTime(2015, 12, 24), "The Prophet's Birthday")}

        ' This method adds holidays from the KuwaitHolidays2015 collection
        ' to the WorkDays collection of the Scheduler Control.
        Private Sub GenerateHolidaysFor2015()
            For Each item As Holiday In KuwaitHolidays2015
                schedulerControl1.WorkDays.Add(item)
            Next
        End Sub

        Private Sub schedulerControl1_CustomDrawDayHeader(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
'#Region "#CustomDrawDayHeader"
            ' Check whether the current object is a Day Header.
            Dim header As SchedulerHeader = TryCast(e.ObjectInfo, SchedulerHeader)
            If header IsNot Nothing Then
                ' Check whether the current date is a known holiday.
                Dim holiday As Holiday = FindHoliday(header.Interval.Start.Date)
                If holiday IsNot Nothing Then
                    header.Caption = holiday.DisplayName
                    e.DrawDefault()
                    ' Add the holiday name to the day header's caption.
                    Dim img As Image = Image.FromFile("Kuwait.png")
                    Dim imgRect As Rectangle = header.ImageBounds
                    imgRect.Width = header.ImageBounds.Height * img.Width \ img.Height
                    imgRect.X = header.ImageBounds.X + header.ImageBounds.Width - imgRect.Width
                    e.Cache.DrawImage(img, imgRect)
                    e.Handled = True
                End If
            End If
'#End Region  ' #CustomDrawDayHeader
        End Sub

        Private Function FindHoliday(ByVal [date] As Date) As Holiday
            Dim workDays As WorkDaysCollection = schedulerControl1.WorkDays
            For Each item As WorkDay In workDays
                If TypeOf item Is Holiday Then
                    Dim holiday As Holiday = CType(item, Holiday)
                    If holiday.Date = [date] Then Return holiday
                End If
            Next

            Return Nothing
        End Function
    End Class

'#Region "#CustomCellStyleProvider"
    Public Class CustomCellStyleProvider
        Implements ICalendarCellStyleProvider

        Private workDays As WorkDaysCollection

        Public Sub New(ByVal workDays As WorkDaysCollection)
            Me.workDays = workDays
        End Sub

        Public Sub UpdateAppearance(ByVal cell As CalendarCellStyle) Implements ICalendarCellStyleProvider.UpdateAppearance
            If workDays Is Nothing Then Return
            If workDays.IsHoliday(cell.Date) Then
                Select Case cell.State
                    ' Highlight dates hovered over with the mouse.
                    Case(DevExpress.Utils.Drawing.ObjectState.Hot)
                        cell.Appearance.ForeColor = Color.DarkGoldenrod
                        cell.Appearance.BackColor = Color.PaleGreen
                    ' Highlight selection.
                    Case(DevExpress.Utils.Drawing.ObjectState.Selected)
                        cell.Appearance.ForeColor = Color.Gold
                        cell.Appearance.BackColor = Color.Green
                    Case Else
                        cell.Appearance.ForeColor = Color.Green
                        cell.Appearance.BackColor = Color.Gold
                End Select
            End If

            ' Display images for dates that contain appointments.
            If cell.IsSpecial Then
                cell.Appearance.FontStyleDelta = FontStyle.Regular
                cell.Image = Image.FromFile("appointment_icon.png")
            End If
        End Sub
    End Class
'#End Region  ' #CustomCellStyleProvider
End Namespace
