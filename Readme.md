<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128634456/18.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E27)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# How to display a custom work week and holidays


<p>Problem:</p>
<p>In Kuwait weekly holidays are Friday and Saturday. Sunday to Thursday are working days. How can I implement this in the XtraScheduler? Also I'd like to show official holidays in the Scheduler and in the DateNavigator and highlight them.</p>
<p>Solution:</p>
<p>You should use the <a href="http://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraSchedulerWorkDaysCollectiontopic">SchedulerControl.WorkDays</a> collection to accomplish this task. Add the required weekdays and holidays to the collection. <br>To change the highlight color of the date in the <a href="http://help.devexpress.com/#WindowsForms/CustomDocument1740">DateNavigator control</a>, handle its <strong>CustomDrawDayNumberCell</strong> event or specify a custom cell style provider with the <strong>DateNavigator.CellStyleProvider</strong> property for version 15.2 and higher.<br> To change the header captions for holidays handle the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerControl_CustomDrawDayHeadertopic">SchedulerControl.CustomDrawDayHeader</a> event. Consider also using <a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument4747">Formatting Services</a> as an alternative technique.<br><br>The application looks like as shown below.<br><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-display-a-custom-work-week-and-holidays-e27/15.2.4+/media/5f1d0e44-95d3-11e5-80bf-00155d62480c.png"></p>


<h3>Description</h3>

Create the&nbsp;<strong>CustomCellStyleProvider</strong> class which implements the &nbsp;<strong>ICalendarCellStyleProvider</strong> interface. The&nbsp;<strong>UpdateAppearance</strong> method changes cell colors as required. Use the <strong>DateNavigator.CellStyleProvider</strong> property to specify the instance of a&nbsp;custom style provider&nbsp;that will be used to color the cells and display icons.

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-country-specific-work-week-holidays&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-country-specific-work-week-holidays&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
