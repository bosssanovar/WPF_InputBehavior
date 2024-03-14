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
                Assert.True(c.ToString().IsOnlyAbailableCharacters(AvailableCharactersType.HalfWidthAlphanumeric));
            }
        }

        [Fact]
        public void 半角英数字かを判定＿NG()
        {
            string testCase = "_ +Ａあアｱ亜Ąａ１";

            foreach (var c in testCase)
            {
                Assert.False(c.ToString().IsOnlyAbailableCharacters(AvailableCharactersType.HalfWidthAlphanumeric));
            }
        }

        [Theory]
        [InlineData("1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]// 違反文字無し　変化なし
        [InlineData("_ +Ａあアｱ亜Ąａ１", "")]//違反文字のみ
        [InlineData("abcあdefg1ｱ亜Ą23", "abcdefg123")]// 中盤に違反文字
        [InlineData("_ +abcあdefg1ｱ亜Ą23", "abcdefg123")]// 冒頭にも違反文字
        [InlineData("abcあdefg1ｱ亜Ą23ａ１", "abcdefg123")]// 末尾にも違反文字
        public void 半角英数を抽出(string input, string output)
        {
            Assert.Equal(output, input.ExtractOnlyAbailableCharacters(AvailableCharactersType.HalfWidthAlphanumeric));
        }


        [Fact]
        public void JIS第1水準漢字までかを判定＿ＯＫ()
        {
            string testCase = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ /+-?あアａＡｱ亜唖娃阿哀愛挨姶逢葵茜穐";

            foreach (var c in testCase)
            {
                Assert.True(c.ToString().IsOnlyAbailableCharacters(AvailableCharactersType.UpToJisLevel1KanjiSet));
            }
        }

        [Fact]
        public void JIS第1水準漢字までかを判定＿NG()
        {
            string testCase = "弌丐丕个丱丶丼丿乂乖乘亂亅豫亊舒弍于亞亟亠亢亰亳亶从仍俱𠀋㐂丨丯丰亍仡份仿伃伋你佈佉佖佟佪佬佾𠂉丂丏丒丩丫丮乀乇么𠂢乑㐆𠂤乚乩亝㐬㐮亹";

            foreach (var c in testCase)
            {
                Assert.False(c.ToString().IsOnlyAbailableCharacters(AvailableCharactersType.UpToJisLevel1KanjiSet));
            }
        }

        [Theory]
        [InlineData("FGHIJKLMNOPQRSTUVWXYZ /+-?あアａＡｱ亜唖娃阿哀愛挨姶逢葵茜穐",
            "FGHIJKLMNOPQRSTUVWXYZ /+-?あアａＡｱ亜唖娃阿哀愛挨姶逢葵茜穐")]// 違反文字無し　変化なし
        [InlineData("丐丕丗个丱丶丼丿乂乕乖乘乢亂亅亊于", "")]//違反文字のみ
        [InlineData("WXYZ /+-丗个丱丶丼丿乂乕?あアａＡｱ亜", "WXYZ /+-?あアａＡｱ亜")]// 中盤に違反文字
        [InlineData("丗个丱丶丼丿乂乕WXYZ /+-?あアａＡｱ亜", "WXYZ /+-?あアａＡｱ亜")]// 冒頭にも違反文字
        [InlineData("WXYZ /+-?あアａＡｱ亜丗个丱丶丼丿乂乕", "WXYZ /+-?あアａＡｱ亜")]// 末尾にも違反文字
        public void JIS第1水準漢字までを抽出(string input, string output)
        {
            Assert.Equal(output, input.ExtractOnlyAbailableCharacters(AvailableCharactersType.UpToJisLevel1KanjiSet));
        }
    }
}