<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128634456/18.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E27)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# WinForms Scheduler - Customize work week and display country-specific holidays

This example demonstrates how to add country-specific holidays (Kuwait), customize the work week (from Sunday to Thursday), and change appearance settings of cells within the [Date Navigator](https://docs.devexpress.com/WindowsForms/1740/controls-and-libraries/scheduler/visual-elements/date-navigator).

![](https://raw.githubusercontent.com/DevExpress-Examples/how-to-display-a-custom-work-week-and-holidays-e27/15.2.4+/media/5f1d0e44-95d3-11e5-80bf-00155d62480c.png)

Use the [SchedulerControl.WorkDays](https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.WorkDays) property to add weekdays and holidays:

```csharp
private void Form1_Load(object sender, EventArgs e) {
  schedulerControl1.BeginUpdate();
  schedulerControl1.OptionsView.FirstDayOfWeek = FirstDayOfWeek.Sunday;
  schedulerControl1.WorkDays.Clear();
  schedulerControl1.WorkDays.Add(WeekDays.Sunday | WeekDays.Monday | WeekDays.Tuesday | WeekDays.Wednesday | WeekDays.Thursday);
  GenerateHolidaysFor2015();
  schedulerControl1.EndUpdate();
}
```

Use the `DateNavigator.CellStyleProvider` property to specify a custom cell style provider (v15.2+) to customize appearance settings of Date Navigator cells. The `CustomCellStyleProvider` class implements `ICalendarCellStyleProvider`. The `UpdateAppearance` method changes the background and foreground color of date cells and displays an icon.

> **Note**
>
> You can also use [Formatting Services](https://docs.devexpress.com/WindowsForms/4747/controls-and-libraries/scheduler/services/formatting-services). Scheduler Services allow you to quickly implement common tasks (navigation, selection, formatting of certain captions, keyboard and mouse event handling).
>
> Read the following help topic for additional information: [Scheduler Services](https://docs.devexpress.com/WindowsForms/4106/controls-and-libraries/scheduler/services).



