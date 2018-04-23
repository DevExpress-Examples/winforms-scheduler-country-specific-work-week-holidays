Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing


Namespace Holidays_Custom
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			schedulerControl1.BeginUpdate()
			schedulerControl1.WorkDays.Clear()
			schedulerControl1.WorkDays.Add(DevExpress.XtraScheduler.WeekDays.Sunday Or DevExpress.XtraScheduler.WeekDays.Monday Or DevExpress.XtraScheduler.WeekDays.Tuesday Or DevExpress.XtraScheduler.WeekDays.Wednesday Or DevExpress.XtraScheduler.WeekDays.Thursday)

			GenerateHolidaysFor2008()
			schedulerControl1.EndUpdate()

			schedulerControl1.Start = DateTime.Now.Date
			schedulerControl1.ActiveViewType = SchedulerViewType.WorkWeek
		End Sub

		' A collection of Kuwait Holidays for 2008.
		Private KuwaitHolidays2008() As Holiday = { New Holiday(New DateTime(2008, 1, 1), "New Year's Day"), New Holiday(New DateTime(2008, 1, 10), "Hejira New Year (Islamic New Year)"), New Holiday(New DateTime(2008, 2, 24), "Bridge Public Holiday"), New Holiday(New DateTime(2008, 2, 25), "National Day"), New Holiday(New DateTime(2008, 2, 26), "Liberation Day"), New Holiday(New DateTime(2008, 3, 20), "The Prophet's birthday"), New Holiday(New DateTime(2008, 5, 15), "Public Holiday (National mourning for Father Amir"), New Holiday(New DateTime(2008, 5, 16), "Public Holiday (National mourning for Father Amir"), New Holiday(New DateTime(2008, 7, 31), "The Prophet's Ascension"), New Holiday(New DateTime(2008, 9, 30), "Government Holiday"), New Holiday(New DateTime(2008, 10, 1), "Eid al Fitr (End of Ramadan)"), New Holiday(New DateTime(2008, 10, 2), "Eid al Fitr holiday"), New Holiday(New DateTime(2008, 10, 3), "Eid al Fitr holiday"), New Holiday(New DateTime(2008, 10, 4), "Eid al Fitr holiday"), New Holiday(New DateTime(2008, 12, 7), "Eid al Adha holiday"), New Holiday(New DateTime(2008, 12, 8), "Eid al Adha (feast of sacrifice)"), New Holiday(New DateTime(2008, 12, 9), "Eid al Adha holiday"), New Holiday(New DateTime(2008, 12, 10), "Eid al Adha holiday"), New Holiday(New DateTime(2008, 12, 11), "Eid al Adha holiday"), New Holiday(New DateTime(2008, 12, 29), "Hejira New Year (Islamic New Year)") }

		' This method adds holidays from the KuwaitHolidays2007 collection
		' to the WorkDays collection of the Scheduler Control.
		Private Sub GenerateHolidaysFor2008()
			For Each item As Holiday In KuwaitHolidays2008
				Me.schedulerControl1.WorkDays.Add(item)
			Next item
		End Sub

		Private Sub schedulerControl1_CustomDrawDayHeader(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs) Handles schedulerControl1.CustomDrawDayHeader
			' Check whether the current object is a Day Header.
			Dim header As SchedulerHeader = TryCast(e.ObjectInfo, SchedulerHeader)
			If header IsNot Nothing Then
				' Check whether the current date is a known holiday.
				Dim hol As Holiday = FindHoliday(header.Interval.Start.Date)
				If hol IsNot Nothing Then
					' Add the holiday name to the day header's caption.
					header.Caption = String.Format("{0} ({1})", hol.DisplayName, hol.Date.ToShortDateString())
				End If
			End If
		End Sub

		' This method finds a holiday for the specified date.
		Private Function FindHoliday(ByVal [date] As DateTime) As Holiday
			For Each item As WorkDay In schedulerControl1.WorkDays
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
End Namespace