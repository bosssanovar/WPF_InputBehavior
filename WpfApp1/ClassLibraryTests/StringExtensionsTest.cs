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
                Assert.True(c.IsFormatValid(TextFormatType.HalfWidthAlphanumeric));
            }
        }

        [Theory]
        [InlineData("１２３４５６７８９０")]//全角数字
        [InlineData("ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ")]//全角英小文字
        [InlineData("ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ")]//全角英大文字
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
                Assert.False(c.IsFormatValid(TextFormatType.HalfWidthAlphanumeric));
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
            Assert.Equal(output, input.ExtractOnlyAbailableCharacters(TextFormatType.HalfWidthAlphanumeric));
        }

        #endregion

        #region 第1水準漢字まで

        [Theory]
        [InlineData("1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]//半角英数
        [InlineData("１２３４５６７８９０")]//全角数字
        [InlineData("ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ")]//全角英小文字
        [InlineData("ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ")]//全角英大文字
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
                Assert.True(c.IsFormatValid(TextFormatType.UpToJisLevel1KanjiSet));
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
                Assert.False(c.IsFormatValid(TextFormatType.UpToJisLevel1KanjiSet));
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
            Assert.Equal(output, input.ExtractOnlyAbailableCharacters(TextFormatType.UpToJisLevel1KanjiSet));
        }

        #endregion

        #region 数字(0～9)

        [Theory]
        [InlineData("1234567890")]//半角数字
        public void 数字かを判定＿ＯＫ(string str)
        {
            foreach (var c in str)
            {
                Assert.True(c.IsFormatValid(TextFormatType.Number));
            }
        }

        [Theory]
        #region test case
        [InlineData("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]//半角英字
        [InlineData("１２３４５６７８９０")]//全角数字
        [InlineData("ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ")]//全角英小文字
        [InlineData("ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ")]//全角英大文字
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
        #endregion
        public void 数字かを判定＿NG(string str)
        {
            foreach (var c in str)
            {
                Assert.False(c.IsFormatValid(TextFormatType.Number));
            }
        }

        [Theory]
        [InlineData("1234567890", "1234567890")]// 違反文字無し　変化なし
        [InlineData("abcde１２３４ａｂｃｄｅｆＡＢＣＤアイウエオカｱｲｳｴｵｶｷｸｹｺｻｼｽｾあいうえおか!\"#$%&'()*+,-！“#＄％亜唖娃齕齘𪗱齝", "")]//違反文字のみ
        [InlineData("123456丕丗7890", "1234567890")]// 中盤に違反文字
        [InlineData("亂亅123456丕丗7890", "1234567890")]// 冒頭にも違反文字
        [InlineData("123456丕丗7890亂亅", "1234567890")]// 末尾にも違反文字
        public void 数字を抽出(string input, string output)
        {
            Assert.Equal(output, input.ExtractOnlyAbailableCharacters(TextFormatType.Number));
        }

        #endregion

        #region 数値とマイナス(0～9, -)

        [Theory]
        [InlineData("1234567890")]//半角数字
        [InlineData("-")]//マイナス
        public void 数値とマイナスかを判定＿ＯＫ(string str)
        {
            foreach (var c in str)
            {
                Assert.True(c.IsFormatValid(TextFormatType.NumberAndMinus));
            }
        }

        [Theory]
        #region test case
        [InlineData("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]//半角英字
        [InlineData("１２３４５６７８９０")]//全角数字
        [InlineData("ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ")]//全角英小文字
        [InlineData("ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ")]//全角英大文字
        [InlineData("アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン")]//全角カタカナ
        [InlineData("ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝ")]//半角カタカナ
        [InlineData("あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん")]//ひらがな
        [InlineData("!\"#$%&'()*+,./:;<=>?@[\\]^_`{|}~ ")]//半角記号
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
        #endregion
        public void 数値とマイナスかを判定＿NG(string str)
        {
            foreach (var c in str)
            {
                Assert.False(c.IsFormatValid(TextFormatType.NumberAndMinus));
            }
        }

        [Theory]
        #region test case
        [InlineData("-")]
        [InlineData("1")]
        [InlineData("-1")]
        [InlineData("9")]
        [InlineData("-9")]
        [InlineData("99")]
        [InlineData("-99")]
        [InlineData("0")]
        [InlineData("-0")]
        [InlineData("09")]
        [InlineData("-09")]
        [InlineData("009")]
        [InlineData("-009")] 
        #endregion
        public void 数値とマイナスで数値として正しいかを判定＿ＯＫ(string str)
        {
            Assert.True(str.IsFormatValid(TextFormatType.NumberAndMinus));
        }

        [Theory]
        #region test case
        [InlineData("--1")]
        [InlineData("--9")]
        [InlineData("--0")]
        [InlineData("1-")]
        [InlineData("9-")]
        [InlineData("0-")]
        [InlineData("1--")]
        [InlineData("9--")]
        [InlineData("0--")]
        [InlineData("11-")]
        [InlineData("99-")]
        [InlineData("00-")] 
        #endregion
        public void 数値とマイナスで数値として正しいかを判定＿NG(string str)
        {
            Assert.False(str.IsFormatValid(TextFormatType.NumberAndMinus));
        }

        [Theory]
        #region tese case
        [InlineData("", "")]
        [InlineData("-1234567890", "-1234567890")]// 違反文字無し　変化なし
        [InlineData("abcde１２３４ａｂｃｄｅｆＡＢＣＤアイウエオカｱｲｳｴｵｶｷｸｹｺｻｼｽｾあいうえおか!\"#$%&'()*.+,！“#＄％亜唖娃齕齘𪗱齝", "")]//違反文字のみ
        [InlineData("-123456丕丗7890", "-1234567890")]// 中盤に違反文字
        [InlineData("亂亅-123456丕丗7890", "-1234567890")]// 冒頭にも違反文字
        [InlineData("-123456丕丗7890亂亅", "-1234567890")]// 末尾にも違反文字
        [InlineData("1-234567890", "1")]// 形式不正 左有効部抜き出し
        [InlineData("12-34567890", "12")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890-", "1234567890")]// 形式不正 左有効部抜き出し
        [InlineData("1--234567890", "1")]// 形式不正 左有効部抜き出し
        [InlineData("12--34567890", "12")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890--", "1234567890")]// 形式不正 左有効部抜き出し
        [InlineData("-", "-")]
        [InlineData("--", "-")]
        [InlineData("--1234567890", "-")]// 形式不正 左有効部抜き出し
        [InlineData("-1-234567890", "-1")]// 形式不正 左有効部抜き出し
        [InlineData("-123456789-0", "-123456789")]// 形式不正 左有効部抜き出し
        [InlineData("-1234567890-", "-1234567890")]// 形式不正 左有効部抜き出し
        [InlineData("-1--234567890", "-1")]// 形式不正 左有効部抜き出し
        [InlineData("-丕丗", "-")]
        [InlineData("丕丗-", "-")]
        [InlineData("-丕丗-", "-")]
        [InlineData("-丕-丗", "-")]
        [InlineData("丕-丗-", "-")]
        [InlineData("丕丗--", "-")]
        #endregion
        public void 数値とマイナスを抽出(string input, string output)
        {
            Assert.Equal(output, input.ExtractOnlyAbailableCharacters(TextFormatType.NumberAndMinus));
        }

        #endregion

        #region 小数(0～9, 小数点)

        [Theory]
        [InlineData("1234567890")]//半角数字
        [InlineData(".")]//記号
        public void 小数かを判定＿ＯＫ(string str)
        {
            foreach (var c in str)
            {
                Assert.True(c.IsFormatValid(TextFormatType.Decimal));
            }
        }

        [Theory]
        #region test case
        [InlineData("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]//半角英字
        [InlineData("１２３４５６７８９０")]//全角数字
        [InlineData("ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ")]//全角英小文字
        [InlineData("ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ")]//全角英大文字
        [InlineData("アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン")]//全角カタカナ
        [InlineData("ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝ")]//半角カタカナ
        [InlineData("あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん")]//ひらがな
        [InlineData("!\"#$%&'()*+,/:-;<=>?@[\\]^_`{|}~ ")]//半角記号
        [InlineData("！“#＄％＆‘（）＊＋,－．/：；＜＝＞？＠［￥］＾＿‘｛｜｝～　")]//全角記号
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
        #endregion
        public void 小数かを判定＿NG(string str)
        {
            foreach (var c in str)
            {
                Assert.False(c.IsFormatValid(TextFormatType.Decimal));
            }
        }

        [Theory]
        #region test case
        [InlineData(".")]
        [InlineData(".0")]
        [InlineData(".00")]
        [InlineData(".1")]
        [InlineData(".11")]
        [InlineData(".9")]
        [InlineData(".99")]
        [InlineData("1")]
        [InlineData("1.")]
        [InlineData("1.0")]
        [InlineData("1.00")]
        [InlineData("1.1")]
        [InlineData("1.11")]
        [InlineData("1.9")]
        [InlineData("1.99")]
        [InlineData("9")]
        [InlineData("9.")]
        [InlineData("9.0")]
        [InlineData("9.00")]
        [InlineData("9.1")]
        [InlineData("9.11")]
        [InlineData("9.9")]
        [InlineData("9.99")]
        [InlineData("99")]
        [InlineData("99.")]
        [InlineData("99.0")]
        [InlineData("99.00")]
        [InlineData("99.1")]
        [InlineData("99.11")]
        [InlineData("99.9")]
        [InlineData("99.99")]
        [InlineData("0")]
        [InlineData("0.")]
        [InlineData("0.0")]
        [InlineData("0.00")]
        [InlineData("0.1")]
        [InlineData("0.11")]
        [InlineData("0.9")]
        [InlineData("0.99")]
        [InlineData("09")]
        [InlineData("09.")]
        [InlineData("009")]
        [InlineData("009.")]
        [InlineData("099.0")]
        [InlineData("099.00")]
        [InlineData("099.1")]
        [InlineData("099.11")]
        [InlineData("099.9")]
        [InlineData("099.99")] 
        #endregion
        public void 小数で数値として正しいかを判定＿ＯＫ(string str)
        {
            Assert.True(str.IsFormatValid(TextFormatType.Decimal));
        }

        [Theory]
        [InlineData("..")]
        [InlineData("0..")]
        [InlineData("1..")]
        [InlineData("9..")]
        [InlineData(".0.")]
        [InlineData(".1.")]
        [InlineData(".9.")]
        [InlineData("0.0.")]
        [InlineData("1.1.")]
        [InlineData("9.9.")]
        public void 小数で数値として正しいかを判定＿NG(string str)
        {
            Assert.False(str.IsFormatValid(TextFormatType.Decimal));
        }

        [Theory]
        #region test case
        [InlineData("", "")]
        [InlineData("1234567890", "1234567890")]// 違反文字無し　変化なし
        [InlineData(".1234567890", ".1234567890")]// 違反文字無し　変化なし
        [InlineData("1.234567890", "1.234567890")]// 違反文字無し　変化なし
        [InlineData("123456789.0", "123456789.0")]// 違反文字無し　変化なし
        [InlineData("abcde１２３４ａｂｃｄｅｆＡＢＣＤアイウエオカｱｲｳｴｵｶｷｸｹｺｻｼｽｾあいうえおか!\"#$%&'()*-+,！“#＄％亜唖娃齕齘𪗱齝", "")]//違反文字のみ
        [InlineData("1.23456丕丗7890", "1.234567890")]// 中盤に違反文字
        [InlineData("亂亅1.23456丕丗7890", "1.234567890")]// 冒頭にも違反文字
        [InlineData("1.23456丕丗7890亂亅", "1.234567890")]// 末尾にも違反文字
        [InlineData("1..234567890", "1.")]// 形式不正 左有効部抜き出し
        [InlineData(".1.234567890", ".1")]// 形式不正 左有効部抜き出し
        [InlineData("12..34567890", "12.")]// 形式不正 左有効部抜き出し
        [InlineData("1.2.34567890", "1.2")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890..", "1234567890.")]// 形式不正 左有効部抜き出し
        [InlineData("123456789.0.", "123456789.0")]// 形式不正 左有効部抜き出し
        [InlineData(".", ".")]
        [InlineData(".丕丗", ".")]
        [InlineData("丕丗.", ".")]
        [InlineData("..", ".")]
        [InlineData(".丕丗.", ".")]
        [InlineData(".丕.丗", ".")]
        [InlineData("丕.丗.", ".")]
        [InlineData("丕丗..", ".")] 
        #endregion
        public void 小数を抽出(string input, string output)
        {
            Assert.Equal(output, input.ExtractOnlyAbailableCharacters(TextFormatType.Decimal));
        }

        #endregion

        #region 小数とマイナス(0～9, 小数点, -)

        [Theory]
        [InlineData("1234567890")]//半角数字
        [InlineData("-.")]//記号
        public void 小数とマイナスかを判定＿ＯＫ(string str)
        {
            foreach (var c in str)
            {
                Assert.True(c.IsFormatValid(TextFormatType.DecimalAndMinus));
            }
        }

        [Theory]
        #region test case
        [InlineData("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]//半角英字
        [InlineData("１２３４５６７８９０")]//全角数字
        [InlineData("ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ")]//全角英小文字
        [InlineData("ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ")]//全角英大文字
        [InlineData("アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン")]//全角カタカナ
        [InlineData("ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝ")]//半角カタカナ
        [InlineData("あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん")]//ひらがな
        [InlineData("!\"#$%&'()*+,/:;<=>?@[\\]^_`{|}~ ")]//半角記号
        [InlineData("！“#＄％＆‘（）＊＋,－．/：；＜＝＞？＠［￥］＾＿‘｛｜｝～　")]//全角記号
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
        #endregion
        public void 小数とマイナスかを判定＿NG(string str)
        {
            foreach (var c in str)
            {
                Assert.False(c.IsFormatValid(TextFormatType.DecimalAndMinus));
            }
        }

        [Theory]
        #region test case
        [InlineData(".")]
        [InlineData(".0")]
        [InlineData(".00")]
        [InlineData(".1")]
        [InlineData(".11")]
        [InlineData(".9")]
        [InlineData(".99")]
        [InlineData("1")]
        [InlineData("1.")]
        [InlineData("1.0")]
        [InlineData("1.00")]
        [InlineData("1.1")]
        [InlineData("1.11")]
        [InlineData("1.9")]
        [InlineData("1.99")]
        [InlineData("9")]
        [InlineData("9.")]
        [InlineData("9.0")]
        [InlineData("9.00")]
        [InlineData("9.1")]
        [InlineData("9.11")]
        [InlineData("9.9")]
        [InlineData("9.99")]
        [InlineData("99")]
        [InlineData("99.")]
        [InlineData("99.0")]
        [InlineData("99.00")]
        [InlineData("99.1")]
        [InlineData("99.11")]
        [InlineData("99.9")]
        [InlineData("99.99")]
        [InlineData("0")]
        [InlineData("0.")]
        [InlineData("0.0")]
        [InlineData("0.00")]
        [InlineData("0.1")]
        [InlineData("0.11")]
        [InlineData("0.9")]
        [InlineData("0.99")]
        [InlineData("09")]
        [InlineData("09.")]
        [InlineData("009")]
        [InlineData("009.")]
        [InlineData("099.0")]
        [InlineData("099.00")]
        [InlineData("099.1")]
        [InlineData("099.11")]
        [InlineData("099.9")]
        [InlineData("099.99")]
        [InlineData("-")]
        [InlineData("-0")]
        [InlineData("-1")]
        [InlineData("-9")]
        [InlineData("-99")]
        [InlineData("-09")]
        [InlineData("-009")]
        [InlineData("-0.0")]
        [InlineData("-00.0")]
        [InlineData("-1.0")]
        [InlineData("-99.009")]
        [InlineData("-09.")]
        [InlineData("-0.")]
        #endregion
        public void 小数とマイナスで数値として正しいかを判定＿ＯＫ(string str)
        {
            Assert.True(str.IsFormatValid(TextFormatType.DecimalAndMinus));
        }

        [Theory]
        #region test case
        [InlineData("..")]
        [InlineData("0..")]
        [InlineData("1..")]
        [InlineData("9..")]
        [InlineData(".0.")]
        [InlineData(".1.")]
        [InlineData(".9.")]
        [InlineData("0.0.")]
        [InlineData("1.1.")]
        [InlineData("9.9.")]
        [InlineData("--1")]
        [InlineData("--9")]
        [InlineData("--0")]
        [InlineData("1-")]
        [InlineData("9-")]
        [InlineData("0-")]
        [InlineData("1--")]
        [InlineData("9--")]
        [InlineData("0--")]
        [InlineData("11-")]
        [InlineData("99-")]
        [InlineData("00-")]
        [InlineData("-.")]
        [InlineData(".-")]
        [InlineData("-.0")]
        [InlineData(".-0")]
        [InlineData("--.")]
        [InlineData("-..")]
        [InlineData("--..")]
        [InlineData("..-")]
        [InlineData(".--")]
        [InlineData("..--")] 
        #endregion
        public void 小数とマイナスで数値として正しいかを判定＿NG(string str)
        {
            Assert.False(str.IsFormatValid(TextFormatType.DecimalAndMinus));
        }

        [Theory]
        #region test case
        [InlineData("", "")]
        [InlineData("1234567890", "1234567890")]// 違反文字無し　変化なし
        [InlineData(".1234567890", ".1234567890")]// 違反文字無し　変化なし
        [InlineData("1.234567890", "1.234567890")]// 違反文字無し　変化なし
        [InlineData("123456789.0", "123456789.0")]// 違反文字無し　変化なし
        [InlineData("abcde１２３４ａｂｃｄｅｆＡＢＣＤアイウエオカｱｲｳｴｵｶｷｸｹｺｻｼｽｾあいうえおか!\"#$%&'()*+,！“#＄％亜唖娃齕齘𪗱齝", "")]//違反文字のみ
        [InlineData("1.23456丕丗7890", "1.234567890")]// 中盤に違反文字
        [InlineData("亂亅1.23456丕丗7890", "1.234567890")]// 冒頭にも違反文字
        [InlineData("1.23456丕丗7890亂亅", "1.234567890")]// 末尾にも違反文字
        [InlineData("1..234567890", "1.")]// 形式不正 左有効部抜き出し
        [InlineData(".1.234567890", ".1")]// 形式不正 左有効部抜き出し
        [InlineData("12..34567890", "12.")]// 形式不正 左有効部抜き出し
        [InlineData("1.2.34567890", "1.2")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890..", "1234567890.")]// 形式不正 左有効部抜き出し
        [InlineData("123456789.0.", "123456789.0")]// 形式不正 左有効部抜き出し
        [InlineData(".", ".")]
        [InlineData(".丕丗", ".")]
        [InlineData("丕丗.", ".")]
        [InlineData("..", ".")]
        [InlineData(".丕丗.", ".")]
        [InlineData(".丕.丗", ".")]
        [InlineData("丕.丗.", ".")]
        [InlineData("丕丗..", ".")]
        [InlineData("-1234567890", "-1234567890")]// 違反文字無し　変化なし
        [InlineData("-123456丕丗7890", "-1234567890")]// 中盤に違反文字
        [InlineData("亂亅-123456丕丗7890", "-1234567890")]// 冒頭にも違反文字
        [InlineData("-123456丕丗7890亂亅", "-1234567890")]// 末尾にも違反文字
        [InlineData("1-234567890", "1")]// 形式不正 左有効部抜き出し
        [InlineData("12-34567890", "12")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890-", "1234567890")]// 形式不正 左有効部抜き出し
        [InlineData("1--234567890", "1")]// 形式不正 左有効部抜き出し
        [InlineData("12--34567890", "12")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890--", "1234567890")]// 形式不正 左有効部抜き出し
        [InlineData("-", "-")]
        [InlineData("--", "-")]
        [InlineData("--1234567890", "-")]// 形式不正 左有効部抜き出し
        [InlineData("-1-234567890", "-1")]// 形式不正 左有効部抜き出し
        [InlineData("-123456789-0", "-123456789")]// 形式不正 左有効部抜き出し
        [InlineData("-1234567890-", "-1234567890")]// 形式不正 左有効部抜き出し
        [InlineData("-1--234567890", "-1")]// 形式不正 左有効部抜き出し
        [InlineData("-丕丗", "-")]
        [InlineData("丕丗-", "-")]
        [InlineData("-丕丗-", "-")]
        [InlineData("-丕-丗", "-")]
        [InlineData("丕-丗-", "-")]
        [InlineData("丕丗--", "-")]
        [InlineData(".1-234567890", ".1")]// 形式不正 左有効部抜き出し
        [InlineData("1.-234567890", "1.")]// 形式不正 左有効部抜き出し
        [InlineData(".12-34567890", ".12")]// 形式不正 左有効部抜き出し
        [InlineData("12.-34567890", "12.")]// 形式不正 左有効部抜き出し
        [InlineData("12-.34567890", "12")]// 形式不正 左有効部抜き出し
        [InlineData("123456789.0-", "123456789.0")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890.-", "1234567890.")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890-.", "1234567890")]// 形式不正 左有効部抜き出し
        [InlineData(".1--234567890", ".1")]// 形式不正 左有効部抜き出し
        [InlineData("1.--234567890", "1.")]// 形式不正 左有効部抜き出し
        [InlineData("1.2--34567890", "1.2")]// 形式不正 左有効部抜き出し
        [InlineData("12.--34567890", "12.")]// 形式不正 左有効部抜き出し
        [InlineData("12-.-34567890", "12")]// 形式不正 左有効部抜き出し
        [InlineData("12--.34567890", "12")]// 形式不正 左有効部抜き出し
        [InlineData("12--3.4567890", "12")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890.--", "1234567890.")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890-.-", "1234567890")]// 形式不正 左有効部抜き出し
        [InlineData("1234567890--.", "1234567890")]// 形式不正 左有効部抜き出し
        [InlineData(".--1234567890", ".")]// 形式不正 左有効部抜き出し
        [InlineData("-.-1234567890", "-")]// 形式不正 左有効部抜き出し
        [InlineData("--.1234567890", "-")]// 形式不正 左有効部抜き出し
        [InlineData("--1.234567890", "-")]// 形式不正 左有効部抜き出し
        [InlineData(".-1-234567890", ".")]// 形式不正 左有効部抜き出し
        [InlineData("-.1-234567890", "-")]// 形式不正 左有効部抜き出し
        [InlineData("-1.-234567890", "-1.")]// 形式不正 左有効部抜き出し
        [InlineData("-1-.234567890", "-1")]// 形式不正 左有効部抜き出し
        [InlineData("-1-2.34567890", "-1")]// 形式不正 左有効部抜き出し
        [InlineData("-12345678.9-0", "-12345678.9")]// 形式不正 左有効部抜き出し
        [InlineData("-123456789.-0", "-123456789.")]// 形式不正 左有効部抜き出し
        [InlineData("-123456789-.0", "-123456789")]// 形式不正 左有効部抜き出し
        [InlineData("-1234567890.-", "-1234567890.")]// 形式不正 左有効部抜き出し
        [InlineData("-1234567890-.", "-1234567890")]// 形式不正 左有効部抜き出し
        [InlineData("-.1--234567890", "-")]// 形式不正 左有効部抜き出し
        [InlineData("-1.--234567890", "-1.")]// 形式不正 左有効部抜き出し
        [InlineData("-1-.-234567890", "-1")]// 形式不正 左有効部抜き出し
        [InlineData("-1--.234567890", "-1")]// 形式不正 左有効部抜き出し
        [InlineData("-1-..234567890", "-1")]// 形式不正 左有効部抜き出し
        [InlineData("-1..234567890", "-1.")]// 形式不正 左有効部抜き出し
        [InlineData("-1.2.34567890", "-1.2")]// 形式不正 左有効部抜き出し
        [InlineData("-1.-.234567890", "-1.")]// 形式不正 左有効部抜き出し
        #endregion
        public void 小数とマイナスを抽出(string input, string output)
        {
            Assert.Equal(output, input.ExtractOnlyAbailableCharacters(TextFormatType.DecimalAndMinus));
        }

        #endregion

        #region 文字列の長さ制限

        #region Shift-JIS
        [Theory]
        [InlineData("", 0, "")]
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