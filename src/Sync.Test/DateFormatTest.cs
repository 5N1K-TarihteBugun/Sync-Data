using System;
using System.Collections.Generic;
using Sync.Common;
using Xunit;

namespace Sync.Test
{
    public class DateFormatTest
    {
        [Fact]
        public void Get_Day_1January2012()
        {
            // Arrange
            var dateFormat = new CustomDateFormat("tr-Tr");

            // Act
            var date = dateFormat.GetDay(2012, 1, 1);

            // Assert
            Assert.Equal("1_Ocak", date);
            Assert.NotEqual("1_January", date);
        }
        
        [Fact]
        public void Get_Days_DaysOf2012()
        {
            // Arrange
            var dateFormat = new CustomDateFormat("tr-Tr");

            // Act
            IList<DateTime> days = dateFormat.GetDaysOfYear(year: 2012);

            // Assert
            Assert.Equal(366, days.Count);
        }
        
        [Fact]
        public void Get_Days_DaysOf2013()
        {
            // Arrange
            var dateFormat = new CustomDateFormat("tr-Tr");

            // Act
            IList<DateTime> days = dateFormat.GetDaysOfYear(year: 2013);

            // Assert
            Assert.Equal(365, days.Count);
        }
    }
}