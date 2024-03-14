using ClassLibrary;

namespace ClassLibraryTests
{
    public class StringExtensionsTest
    {
        [Fact]
        public void 半角英数字かを判定＿ＯＫ()
        {
            string testCase = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            foreach (var c in testCase)
            {
                Assert.True(c.ToString().IsOnlyHalfWidthAlphanumericCharacters());
            }
        }

        [Fact]
        public void 半角英数字かを判定＿NG()
        {
            string testCase = "_ +Ａあアｱ亜Ąａ１";

            foreach (var c in testCase)
            {
                Assert.False(c.ToString().IsOnlyHalfWidthAlphanumericCharacters());
            }
        }

        [Theory]
        [InlineData("1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]//変化なし
        [InlineData("_ +Ａあアｱ亜Ąａ１", "")]
        [InlineData("abcあdefg1ｱ亜Ą23", "abcdefg123")]
        [InlineData("_ +abcあdefg1ｱ亜Ą23", "abcdefg123")]
        [InlineData("abcあdefg1ｱ亜Ą23ａ１", "abcdefg123")]
        public void 半角英数を抽出(string input, string output)
        {
            Assert.Equal(output, input.ExtractOnlyHalfWidthAlphanumericCharacters());
        }
    }
}