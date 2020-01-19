using System;
using System.Collections;
using System.Collections.Generic;
using SyncData;
using SyncData.Common;
using Xunit;

namespace SyncDataTest
{
    public class HtmlContentTest
    {
        [Theory]
        [InlineData("https://tr.wikipedia.org/wiki/1_Ocak", ActionRegex.Events)]
        public void Get_Events(string url, string regex)
        {
            // Arrange
            var htmlDoc = new WebDocument(url: url);

            // Act
            var itemList = htmlDoc.GetSelectNodes(regex);

            // Assert
            Assert.NotEmpty(itemList);
        }
    }
}