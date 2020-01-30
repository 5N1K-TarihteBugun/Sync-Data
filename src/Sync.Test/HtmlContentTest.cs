using Sync.Common;
using Xunit;

namespace Sync.Test
{
    public class HtmlContentTest
    {
        [Theory]
        [InlineData("https://tr.wikipedia.org/wiki/1_Ocak", ProjectConst.ActionConst.Event)]
        [InlineData("https://tr.wikipedia.org/wiki/1_Ocak", ProjectConst.ActionConst.Birth)]
        [InlineData("https://tr.wikipedia.org/wiki/1_Ocak", ProjectConst.ActionConst.Death)]
        [InlineData("https://tr.wikipedia.org/wiki/1_Ocak", ProjectConst.ActionConst.Other)]
        public void Get_Data(string url, string regex)
        {
            // Arrange
            var htmlDoc = new WebDocument();

            // Act
            var itemList = htmlDoc.GetSelectNodes(url, regex);

            // Assert
            Assert.NotEmpty(itemList);
        }
    }
}