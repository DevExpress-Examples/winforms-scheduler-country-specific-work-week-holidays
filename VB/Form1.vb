Imports DevExpress.Schedule
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports System
Imports System.Drawing


Namespace DisplayCustomHolidays
    Partial Public Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ' Specify a custom cell style provider to highlight cells that meet certain criteria.
            Me.dateNavigator1.CellStyleProvider = New CustomCellStyleProvider(Me.dateNavigator1)
            schedulerControl1.BeginUpdate()
            schedulerControl1.OptionsView.FirstDayOfWeek = FirstDayOfWeek.Sunday
            schedulerControl1.WorkDays.Clear()
            ' Specify working days of a week. Friday and Saturday are weekend holidays.
            schedulerControl1.WorkDays.Add(DevExpress.XtraScheduler.WeekDays.Sunday Or DevExpress.XtraScheduler.WeekDays.Monday Or DevExpress.XtraScheduler.WeekDays.Tuesday Or DevExpress.XtraScheduler.WeekDays.Wednesday Or DevExpress.XtraScheduler.WeekDays.Thursday)
            GenerateHolidaysFor2015()
            schedulerControl1.EndUpdate()
            Me.schedulerControl1.ActiveViewType = SchedulerViewType.Day
            Me.dateNavigator1.HighlightHolidays = True
            Me.dateNavigator1.DateTime = New Date(2015, 02, 26)

        End Sub

        ' A collection of Kuwait Holidays for 2015.
        Private KuwaitHolidays2015() As Holiday = { _
            New Holiday(New Date(2015, 01, 1), "New Year's Day"), _
            New Holiday(New Date(2015, 01, 3), "The Prophet's Birthday"), _
            New Holiday(New Date(2015, 02, 25), "National Day"), _
            New Holiday(New Date(2015, 02, 26), "Liberation Day"), _
            New Holiday(New Date(2015, 05, 16), "Isra and Miraj"), _
            New Holiday(New Date(2015, 07, 18), "Eid al Fitr (End of Ramadan)"), _
            New Holiday(New Date(2015, 07, 19), "Eid al Fitr holiday"), _
            New Holiday(New Date(2015, 07, 20), "Eid al Fitr holiday"), _
            New Holiday(New Date(2015, 09, 23), "Waqfat Arafat Day"), _
            New Holiday(New Date(2015, 09, 24), "Eid al Adha (Feast of Sacrifice)"), _
            New Holiday(New Date(2015, 09, 25), "Eid al Adha holiday"), _
            New Holiday(New Date(2015, 09, 26), "Eid al Adha holiday"), _
            New Holiday(New Date(2015, 10, 15), "Hejira New Year (Islamic New Year)"), _
            New Holiday(New Date(2015, 12, 24), "The Prophet's Birthday") _
        }

        ' This method adds holidays from the KuwaitHolidays2015 collection
        ' to the WorkDays collection of the Scheduler Control.
        Private Sub GenerateHolidaysFor2015()
            For Each item As Holiday In KuwaitHolidays2015
                Me.schedulerControl1.WorkDays.Add(item)
            Next item
        End Sub

        Private Sub schedulerControl1_CustomDrawDayHeader(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs) Handles schedulerControl1.CustomDrawDayHeader
'            #Region "#CustomDrawDayHeader"
            ' Check whether the current object is a Day Header.
            Dim header As SchedulerHeader = TryCast(e.ObjectInfo, SchedulerHeader)
            If header IsNot Nothing Then
                ' Check whether the current date is a known holiday.
                Dim hol As Holiday = FindHoliday(header.Interval.Start.Date)
                If hol IsNot Nothing Then
                    header.Caption = hol.DisplayName
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
'            #End Region ' #CustomDrawDayHeader
        End Sub

        Private Function FindHoliday(ByVal [date] As Date) As Holiday
            Dim workDays As WorkDaysCollection = Me.schedulerControl1.WorkDays
            For Each item As WorkDay In workDays
                If TypeOf item Is Holiday Then
                    Dim hol As Holiday = CType(item, Holiday)
                    If hol.Date = [date] Then
                        Return hol
                    End If
                End If
            Next item
            Return Nothing
        End Function
    End Class
    #Region "#CustomCellStyleProvider"
    Public Class CustomCellStyleProvider
        Implements ICalendarCellStyleProvider

        Private dateNavigator As DateNavigator

        Public Sub New(ByVal calendar As DateNavigator)
            Me.dateNavigator = calendar
        End Sub

        Public Sub UpdateAppearance(ByVal cell As CalendarCellStyle)
            Dim workDays As WorkDaysCollection = Me.dateNavigator.SchedulerControl.WorkDays
            If workDays.IsHoliday(cell.Date) Then
                Select Case cell.State
                    ' Highlight dates hovered over with the mouse.
                    Case (DevExpress.Utils.Drawing.ObjectState.Hot)
                        cell.Appearance.ForeColor = Color.DarkGoldenrod
                        cell.Appearance.BackColor = Color.PaleGreen
                    ' Highlight selection.
                    Case (DevExpress.Utils.Drawing.ObjectState.Selected)
                        cell.Appearance.ForeColor = Color.Gold
                        cell.Appearance.BackColor = Color.Green
                    Case Else
                        cell.Appearance.ForeColor = Color.Green
                        cell.Appearance.BackColor = Color.Gold
                End Select
            End If
            ' Display an image for the dates which contains appointments.
            If cell.IsSpecial Then
                cell.Appearance.Font = New Font(cell.Appearance.Font, FontStyle.Regular)
                cell.Image = Image.FromFile("appointment_icon.png")
            End If
        End Sub

        Private Sub ICalendarCellStyleProvider_UpdateAppearance(cell As CalendarCellStyle) Implements ICalendarCellStyleProvider.UpdateAppearance

        End Sub
    End Class
    #End Region ' #CustomCellStyleProvider
End Namespace
