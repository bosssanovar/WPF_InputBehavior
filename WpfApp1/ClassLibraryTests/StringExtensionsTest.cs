using ClassLibrary;

namespace ClassLibraryTests
{
    public class StringExtensionsTest
    {
        #region 半角英数字

        [Theory]
        [InlineData("1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]//半角英数
        public void 半角英数字かを判定＿ＯＫ(string str)
        {
            foreach (var c in str)
            {
                Assert.True(c.IsOnlyAbailableCharacters(AvailableCharactersType.HalfWidthAlphanumeric));
            }
        }

        [Theory]
        [InlineData("アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン")]//全角カタカナ
        [InlineData("ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝ")]//半角カタカナ
        [InlineData("あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん")]//ひらがな
        [InlineData("!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ ")]//半角記号
        [InlineData("！“#＄％＆‘（）＊＋,－./：；＜＝＞？＠［￥］＾＿‘｛｜｝～　")]//全角記号
        [InlineData("亜唖娃阿哀愛挨姶逢葵茜")]//第1水準漢字　面区冒頭
        [InlineData("拭植殖燭織職色触食蝕辱")]//第1水準漢字　面区中盤
        [InlineData("鷲亙亘鰐詫藁蕨椀湾碗腕")]//第1水準漢字　面区末尾
        [InlineData("弌丐丕个丱丶丼丿乂乖乘")]//第2水準漢字　面区冒頭
        [InlineData("狹狷倏猗猊猜猖猝猴猯猩")]//第2水準漢字　面区中盤
        [InlineData("堯槇遙瑤凜熙齷齲齶龕龜龠")]//第2水準漢字　面区末尾
        [InlineData("俱𠀋㐂丨丯丰亍仡份仿伃")]//第3水準漢字　面区冒頭
        [InlineData("琇琊琚琛琢琦琨琪琫琬琮")]//第3水準漢字　面区中盤
        [InlineData("鼹齗龐龔龗龢姸屛幷瘦繫")]//第3水準漢字　面区末尾
        [InlineData("𠂉丂丏丒丩丫丮乀乇么𠂢")]//第4水準漢字　面区冒頭
        [InlineData("𥆩䀹𥇥𥇍睘睠睪𥈞睲睼睽")]//第4水準漢字　面区中盤
        [InlineData("齕齘𪗱齝𪘂齩𪘚齭齰齵𪚲")]//第4水準漢字　面区末尾
        public void 半角英数字かを判定＿NG(string str)
        {
            foreach (var c in str)
            {
                Assert.False(c.IsOnlyAbailableCharacters(AvailableCharactersType.HalfWidthAlphanumeric));
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

        #endregion

        #region 第1水準漢字まで

        [Theory]
        [InlineData("1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]//半角英数
        [InlineData("アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン")]//全角カタカナ
        [InlineData("ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝ")]//半角カタカナ
        [InlineData("あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん")]//ひらがな
        [InlineData("!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ ")]//半角記号
        [InlineData("！“#＄％＆‘（）＊＋,－./：；＜＝＞？＠［￥］＾＿‘｛｜｝～　")]//全角記号
        [InlineData("亜唖娃阿哀愛挨姶逢葵茜")]//第1水準漢字　面区冒頭
        [InlineData("拭植殖燭織職色触食蝕辱")]//第1水準漢字　面区中盤
        [InlineData("鷲亙亘鰐詫藁蕨椀湾碗腕")]//第1水準漢字　面区末尾
        public void JIS第1水準漢字までかを判定＿ＯＫ(string str)
        {
            foreach (var c in str)
            {
                Assert.True(c.IsOnlyAbailableCharacters(AvailableCharactersType.UpToJisLevel1KanjiSet));
            }
        }

        [Theory]
        [InlineData("弌丐丕个丱丶丼丿乂乖乘")]//第2水準漢字　面区冒頭
        [InlineData("狹狷倏猗猊猜猖猝猴猯猩")]//第2水準漢字　面区中盤
        [InlineData("堯槇遙瑤凜熙齷齲齶龕龜龠")]//第2水準漢字　面区末尾
        [InlineData("俱𠀋㐂丨丯丰亍仡份仿伃")]//第3水準漢字　面区冒頭
        [InlineData("琇琊琚琛琢琦琨琪琫琬琮")]//第3水準漢字　面区中盤
        [InlineData("鼹齗龐龔龗龢姸屛幷瘦繫")]//第3水準漢字　面区末尾
        [InlineData("𠂉丂丏丒丩丫丮乀乇么𠂢")]//第4水準漢字　面区冒頭
        [InlineData("𥆩䀹𥇥𥇍睘睠睪𥈞睲睼睽")]//第4水準漢字　面区中盤
        [InlineData("齕齘𪗱齝𪘂齩𪘚齭齰齵𪚲")]//第4水準漢字　面区末尾
        public void JIS第1水準漢字までかを判定＿NG(string str)
        {
            foreach (var c in str)
            {
                Assert.False(c.IsOnlyAbailableCharacters(AvailableCharactersType.UpToJisLevel1KanjiSet));
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

        #endregion

        #region 文字列の切り出し

        #region Shift-JIS

        [Theory]
        [InlineData("", 0,  "")]
        [InlineData("a", 0, "")]
        [InlineData("あ", 0, "")]
        [InlineData("", 1, "")]
        [InlineData("a", 1, "a")]
        [InlineData("あ", 1, "")]
        [InlineData("", 2, "")]
        [InlineData("a", 2, "a")]
        [InlineData("あ", 2, "あ")]
        [InlineData("aa", 2, "aa")]
        [InlineData("aあ", 2, "a")]
        [InlineData("あa", 2, "あ")]
        [InlineData("ああ", 2, "あ")]
        [InlineData("aaa", 2, "aa")]
        [InlineData("aあ", 3, "aあ")]
        [InlineData("あa", 3, "あa")]
        [InlineData("ああ", 3, "あ")]
        [InlineData("aaa", 3, "aaa")]
        public void バイト数指定での文字列切り出し(string input, int maxByte, string expected)
        {
            Assert.Equal(expected, input.SubstringSJisByteCount(maxByte));
        }

        #endregion

        #endregion
    }
}