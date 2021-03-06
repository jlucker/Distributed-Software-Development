﻿using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Assignment3Part1.NewsServiceReference;
using Assignment3Part1.WeatherServiceReference;

namespace Assignment3Part1
{
    public partial class _Default : Page
    {
        private bool initial = true;
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void WeatherButton_OnClick(object sender, EventArgs e)
        {
            var textValue = WeatherTextBox.Text;

            if (!String.IsNullOrEmpty(textValue))
            {
                PopulateWeather();
            }
        }

        private void PopulateWeather()
        {
            string textValue = WeatherTextBox.Text;

            WeatherServiceClient client = new WeatherServiceClient();
            Weather weather = new Weather();

            // Check if the Weather text box is empty
            if (!String.IsNullOrEmpty(textValue))
            {
                weather = client.GetWeather(textValue);
            }

            // Set the day of the week labels
            DateTime dt1 = Convert.ToDateTime(weather.DailyForecasts[0].Date);
            Day1Label.Text = dt1.ToString("dddd");
            DateTime dt2 = Convert.ToDateTime(weather.DailyForecasts[1].Date);
            Day2Label.Text = dt2.ToString("dddd");
            DateTime dt3 = Convert.ToDateTime(weather.DailyForecasts[2].Date);
            Day3Label.Text = dt3.ToString("dddd");
            DateTime dt4 = Convert.ToDateTime(weather.DailyForecasts[3].Date);
            Day4Label.Text = dt4.ToString("dddd");
            DateTime dt5 = Convert.ToDateTime(weather.DailyForecasts[4].Date);
            Day5Label.Text = dt5.ToString("dddd");

            // Set the weather images
            Image1.Text = ImageSelector(weather.DailyForecasts[0].Day.IconPhrase);
            Image2.Text = ImageSelector(weather.DailyForecasts[1].Day.IconPhrase);
            Image3.Text = ImageSelector(weather.DailyForecasts[2].Day.IconPhrase);
            Image4.Text = ImageSelector(weather.DailyForecasts[3].Day.IconPhrase);
            Image5.Text = ImageSelector(weather.DailyForecasts[4].Day.IconPhrase);

            // Set the weather description labels
            DescLabel1.Text = weather.DailyForecasts[0].Day.IconPhrase;
            DescLabel2.Text = weather.DailyForecasts[1].Day.IconPhrase;
            DescLabel3.Text = weather.DailyForecasts[2].Day.IconPhrase;
            DescLabel4.Text = weather.DailyForecasts[3].Day.IconPhrase;
            DescLabel5.Text = weather.DailyForecasts[4].Day.IconPhrase;

            // Set the maximum temperature labels
            MaxLabel1.Text = weather.DailyForecasts[0].Temperature.Maximum.Value + "°";
            MaxLabel2.Text = weather.DailyForecasts[1].Temperature.Maximum.Value + "°";
            MaxLabel3.Text = weather.DailyForecasts[2].Temperature.Maximum.Value + "°";
            MaxLabel4.Text = weather.DailyForecasts[3].Temperature.Maximum.Value + "°";
            MaxLabel5.Text = weather.DailyForecasts[4].Temperature.Maximum.Value + "°";

            // Set the minimum temperature value labels
            MinTemp1Label.Text = weather.DailyForecasts[0].Temperature.Minimum.Value + "°";
            MinTemp2Label.Text = weather.DailyForecasts[1].Temperature.Minimum.Value + "°";
            MinTemp3Label.Text = weather.DailyForecasts[2].Temperature.Minimum.Value + "°";
            MinTemp4Label.Text = weather.DailyForecasts[3].Temperature.Minimum.Value + "°";
            MinTemp5Label.Text = weather.DailyForecasts[4].Temperature.Minimum.Value + "°";

            HighLabel.Text = "High";
            LowLabel.Text = "Low";
        }

        // Selects the image best suited for the current weather conditions
        private string ImageSelector(string description)
        {
            if (description == "Partly sunny")
            {
                return "<img src='Images/partly sunny.png' />";
            }
            else if(description == "Mostly cloudy")
            {
                return "<img src='Images/cloudy.png' />";
            }
            else if (description == "Sunny")
            {
                return "<img src='Images/clear.png' />";
            }
            else if (description == "Partly sunny w/ t-storms")
            {
                return "<img src='Images/partly sunny.png' />";
            }
            else if (description.Contains("rain") || description.Contains("shower"))
            {
                return "<img src='Images/showers.png' />";
            }
            else if (description.Contains("cloud"))
            {
                return "<img src='Images/partly sunny.png' />";
            }
            else
            {
                return "<img src='Images/clear.png' />";
            }
        }
        

        protected void NewsButton_OnClick(object sender, EventArgs e)
        {
            var textValue = NewsTextBox.Text;

            if (!String.IsNullOrEmpty(textValue))
            {
                PopulateNews();
            }
        }

        private void PopulateNews()
        {
            string textValue = NewsTextBox.Text;

            NewsServiceClient client = new NewsServiceClient();
            NewsList newsList = new NewsList();

            char delimiterChar = ' ';
            string[] parts = textValue.Split(delimiterChar);

            if (!String.IsNullOrEmpty(textValue))
            {
                newsList = client.GetNews(textValue);
            }

            // Set the labels
            TopicLabel.Text = "Topic";
            TitleLabel.Text = "Title";

            var i = 0;
            foreach (var news in newsList.newsList)
            {
                var topic = parts[i];

                foreach (var item in news.Response.Results)
                {
                    TableRow row = new TableRow();

                    TableCell cell1 = new TableCell();
                    cell1.Text = topic;
                    row.Cells.Add(cell1);

                    TableCell cell2 = new TableCell();
                    HyperLink h1 = new HyperLink()
                    {
                        Text = item.WebTitle,
                        NavigateUrl = item.WebUrl
                    };
                    cell2.Controls.Add(h1);
                    row.Cells.Add(cell2);

                    NewsTable.Rows.Add(row);
                }
                i++;
            }
            NewsTable.Visible = true;
        }
    }
}