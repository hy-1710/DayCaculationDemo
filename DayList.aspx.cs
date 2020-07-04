using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DayCaculationDemo
{
    public partial class DayList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                startDate.SelectedDate = new DateTime(DateTime.Now.Year, 4, 1);
                endDate.SelectedDate = new DateTime(DateTime.Now.Year, 7, 31);
                rbAlter.Checked = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //1.alternate date bw two dates
            //2.working days bw two dates
            //3. first and last day bw to dates

            if (rbAlter.Checked == true)
            {
                getAlternateDay(Convert.ToInt32(ddlDays.SelectedValue));
            }
            else if (rbWorking.Checked == true)
            {
                getTotalWorkingDayCount();
            }
            else
            {
                firstandLastDay(Convert.ToInt32(ddlDays.SelectedValue));
            }



        }

        public void getAlternateDay(int day)
        {
            DayOfWeek dayOfWeek = GetDayOfWeek(day);

            DateTime StartDate = Convert.ToDateTime(startDate.SelectedDate.ToShortDateString());
            DateTime enndDate = Convert.ToDateTime(endDate.SelectedDate.ToShortDateString());
            //find first monday
            DateTime firstMonday = Enumerable.Range(0, 14)
                .SkipWhile(x => StartDate.AddDays(x).DayOfWeek != dayOfWeek)
                .Select(x => StartDate.AddDays(x))
                .First();
            //get count of days
            TimeSpan ts = (TimeSpan)(enndDate - firstMonday);
            DataTable dt = new DataTable();
            dt.Columns.Add("AlternateDate");
            //add dates to list
            for (int i = 0; i < ts.Days; i += 14)
            {

                dt.Rows.Add(firstMonday.AddDays(i).ToShortDateString());
            }



            dayslist.DataSource = dt;
            dayslist.DataBind();

            rptfstLstDay.DataSource = null;
            rptfstLstDay.DataBind();



        }

        public void getTotalWorkingDayCount()
        {
            DateTime StartDate = Convert.ToDateTime(startDate.SelectedDate.ToShortDateString());
            DateTime enndDate = Convert.ToDateTime(endDate.SelectedDate.ToShortDateString());
            DataTable dt = new DataTable();
            dt.Columns.Add("AlternateDate");

            int days = 0;
            while (StartDate <= enndDate)
            {
                if (StartDate.DayOfWeek != DayOfWeek.Saturday && StartDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++days;
                    dt.Rows.Add(StartDate.ToShortDateString());
                }
                StartDate = StartDate.AddDays(1);

            }
            lblTotalWorkingDay.Text = "Total Working Days :" + days;


            dayslist.DataSource = dt;
            dayslist.DataBind();

            rptfstLstDay.DataSource = null;
            rptfstLstDay.DataBind();




        }

        public void firstandLastDay(int day)
        {
            DayOfWeek dayOfWeek = GetDayOfWeek(day);

            DateTime StartDate = Convert.ToDateTime(startDate.SelectedDate.ToShortDateString());
            DateTime enndDate = Convert.ToDateTime(endDate.SelectedDate.ToShortDateString());


            int datediff = (enndDate - StartDate).Days + 1;
            var lastThursdaysBetweenDates = Enumerable.Range(0, datediff)
                .Select(x => StartDate.AddDays(x))
                .Where(d => d.DayOfWeek == dayOfWeek)
                .GroupBy(d => d.Month)
                .Select(grp => grp.Max(d => d)).ToList();

            var firstThursdaysBetweenDates = Enumerable.Range(0, datediff)
              .Select(x => StartDate.AddDays(x))
              .Where(d => d.DayOfWeek == dayOfWeek)
              .GroupBy(d => d.Month)
              .Select(grp => grp.Min(d => d)).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("FirstLastDate", typeof(DateTime));

            for (int i = 0; i < lastThursdaysBetweenDates.Count; i++)
            {
                dt.Rows.Add(lastThursdaysBetweenDates[i]);
            }

            for (int i = 0; i < firstThursdaysBetweenDates.Count; i++)
            {
                dt.Rows.Add(firstThursdaysBetweenDates[i]);
            }


            var orderedRows = from row in dt.AsEnumerable()
                              orderby row.Field<DateTime>("FirstLastDate")
                              select row;
            DataTable tblOrdered = orderedRows.CopyToDataTable();
            rptfstLstDay.DataSource = tblOrdered;
            rptfstLstDay.DataBind();

            dayslist.DataSource = null;
            dayslist.DataBind();


        }

        public DayOfWeek GetDayOfWeek(int day)
        {
            DayOfWeek dayOfWeek = DayOfWeek.Sunday;
            if (day == 0)
            {
                dayOfWeek = DayOfWeek.Sunday;
            }
            else if (day == 1)
            {
                dayOfWeek = DayOfWeek.Monday;
            }
            else if (day == 2)
            {
                dayOfWeek = DayOfWeek.Tuesday;
            }
            else if (day == 3)
            {
                dayOfWeek = DayOfWeek.Wednesday;
            }
            else if (day == 4)
            {
                dayOfWeek = DayOfWeek.Thursday;
            }
            else if (day == 5)
            {
                dayOfWeek = DayOfWeek.Friday;
            }
            else if (day == 6)
            {
                dayOfWeek = DayOfWeek.Saturday;
            }

            return dayOfWeek;

        }



    }
}