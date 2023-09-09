Imports DevExpress.Schedule
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports System
Imports System.Drawing

Namespace DisplayCustomHolidays

    Public Partial Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Private selectedDates As DevExpress.XtraEditors.Controls.DatesCollection = New DevExpress.XtraEditors.Controls.DatesCollection()

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub dateNavigator1_CustomDrawDayNumberCell(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs)
'#Region "#CustomDrawDayNumberCell"
            ' Return if the current date is not a holiday.
            ' In this case the default drawing will be performed (e.Handled is false)
            If FindHoliday(e.Date) Is Nothing OrElse selectedDates.Contains(e.Date) Then Return
            ' Set the brush to draw the text with.
            Dim textBrush As Brush = Brushes.Green
            Dim backBrush As Brush = Brushes.Gold
            Dim drawBounds As Rectangle = e.Bounds
            If e.Selected Then
                ' Change the brush used to draw the text.
                textBrush = Brushes.Gold
                backBrush = Brushes.Green
            End If

            ' Highlight the cell.
            e.Graphics.FillRectangle(backBrush, drawBounds)
            ' Specify text formatting attributes.
            Dim strFormat As StringFormat = New StringFormat()
            strFormat.Alignment = StringAlignment.Far
            strFormat.LineAlignment = StringAlignment.Far
            ' Draw the day number.
            drawBounds.Offset(1, 1)
            e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, textBrush, drawBounds, strFormat)
            ' No default drawing is required.
            e.Handled = True
'#End Region  ' #CustomDrawDayNumberCell
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            schedulerControl1.BeginUpdate()
            schedulerControl1.OptionsView.FirstDayOfWeek = FirstDayOfWeek.Sunday
            schedulerControl1.WorkDays.Clear()
            ' Specify working days of a week. Friday and Saturday are weekend holidays.
            schedulerControl1.WorkDays.Add(WeekDays.Sunday Or WeekDays.Monday Or WeekDays.Tuesday Or WeekDays.Wednesday Or WeekDays.Thursday)
            GenerateHolidaysFor2015()
            schedulerControl1.EndUpdate()
            schedulerControl1.ActiveViewType = SchedulerViewType.Day
            dateNavigator1.HighlightHolidays = True
            dateNavigator1.DateTime = New DateTime(2015, 02, 26)
            ' Handle the event to draw a day number.
            AddHandler dateNavigator1.CustomDrawDayNumberCell, AddressOf dateNavigator1_CustomDrawDayNumberCell
            AddHandler dateNavigator1.EditDateModified, AddressOf DateNavigator1_EditDateModified
        End Sub

        Private Sub DateNavigator1_EditDateModified(ByVal sender As Object, ByVal e As EventArgs)
            selectedDates.Clear()
            selectedDates.AddRange(CType(sender, DateNavigator).Selection)
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
                Dim hol As Holiday = FindHoliday(header.Interval.Start.Date)
                If hol IsNot Nothing Then
                    header.Caption = hol.DisplayName
                    e.DrawDefault()
                    ' Add the holiday name to the day header's caption.
                    Dim img As Image = Image.FromFile("Kuwait.png")
                    Dim imgRect As Rectangle = header.ImageBounds
                    imgRect.Width = header.ImageBounds.Height * img.Width \ img.Height
                    imgRect.X = header.ImageBounds.X + header.ImageBounds.Width - imgRect.Width
                    e.Graphics.DrawImage(img, imgRect)
                    e.Handled = True
                End If
            End If
'#End Region  ' #CustomDrawDayHeader
        End Sub

        ' This method finds a holiday for the specified date.
        Private Function FindHoliday(ByVal [date] As Date) As Holiday
            For Each item As WorkDay In schedulerControl1.WorkDays
                If TypeOf item Is Holiday Then
                    Dim hol As Holiday = CType(item, Holiday)
                    If hol.Date = [date] Then Return hol
                End If
            Next

            Return Nothing
        End Function
    End Class
End Namespace
