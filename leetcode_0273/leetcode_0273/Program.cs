using System.Text;

namespace leetcode_0273
{
    internal class Program
    {
        /// <summary>
        /// 273. Integer to English Words
        /// https://leetcode.com/problems/integer-to-english-words/description/?envType=daily-question&envId=2024-08-07
        /// 
        /// 273. 整数转换英文表示
        /// https://leetcode.cn/problems/integer-to-english-words/description/
        /// 
        /// 要解這一題, 需要知道 英文數字表達方式
        /// 如過不理解, 會錯誤
        /// 要先枚舉, 會用到的表達文字
        /// 都先宣告放到陣列裡面
        /// 之後取出來
        /// 
        /// 數字轉英文 線上轉換網站, 可以參考
        /// https://tw.piliapp.com/converter/english-numbers/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行十筆固定案例，分別驗證高位分組遞迴與低位分組疊代兩種整數轉英文解法。
        /// 輸入涵蓋題目下界、各種三位數分支、跨區塊數字與 32 位元整數上界；
        /// 輸出每筆案例的預期值、實際值、通過狀態，以及二十項檢查的彙總結果。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] samples =
            {
                new("題目下界", 0, "Zero"),
                new("個位數", 7, "Seven"),
                new("十到十九", 13, "Thirteen"),
                new("整十", 20, "Twenty"),
                new("整百", 100, "One Hundred"),
                new("官方百位範例", 123, "One Hundred Twenty Three"),
                new("原始專案範例", 13401, "Thirteen Thousand Four Hundred One"),
                new("中間三位區塊為零", 1000010, "One Million Ten"),
                new(
                    "官方多區塊範例",
                    1234567,
                    "One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven"),
                new(
                    "32 位元整數上界",
                    int.MaxValue,
                    "Two Billion One Hundred Forty Seven Million Four Hundred Eighty Three Thousand Six Hundred Forty Seven")
            };

            int passedChecks = 0;

            for (int index = 0; index < samples.Length; index++)
            {
                passedChecks += RunSample(index + 1, samples[index]);
            }

            int totalChecks = samples.Length * 2;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        }

        /// <summary>
        /// 針對單筆非負整數案例執行兩種解法，並將結果與預期英文表示逐一比較。
        /// 輸入包含顯示編號及符合題目限制的案例資料；
        /// 輸出格式化的案例資訊，並回傳本案例通過的解法數量。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="sample">包含案例名稱、非負整數輸入與預期英文結果的資料。</param>
        /// <returns>兩種解法中通過預期結果比對的項目數，範圍為 0 到 2。</returns>
        private static int RunSample(int caseNumber, SampleCase sample)
        {
            string recursiveActual = NumberToWords(sample.Input);
            string iterativeActual = NumberToWords2(sample.Input);
            bool recursivePassed = recursiveActual == sample.Expected;
            bool iterativePassed = iterativeActual == sample.Expected;

            Console.WriteLine($"案例 {caseNumber}：{sample.Name}");
            Console.WriteLine($"  輸入：{sample.Input}");
            Console.WriteLine($"  預期：{sample.Expected}");
            Console.WriteLine($"  解法一（高位分組＋遞迴）：{recursiveActual} => {(recursivePassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"  解法二（低位分組＋疊代）：{iterativeActual} => {(iterativePassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (recursivePassed ? 1 : 0) + (iterativePassed ? 1 : 0);
        }


        /// <summary>
        /// 個位數 1 ~ 9
        /// </summary>
        static string[] singles = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };


        /// <summary>
        /// 十位數 10 ~ 19
        /// </summary>
        static string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };


        /// <summary>
        /// 十位數
        /// 10, 20, 30, ... , 90
        /// </summary>
        static string[] tens = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };


        /// <summary>
        /// 千, 百萬, 十億
        /// </summary>
        static string[] thousands = { "", "Thousand", "Million", "Billion" };


        /// <summary>
        /// 將非負整數轉換成英文文字表示。
        /// 解題概念是由 Billion、Million、Thousand 到個位，從高位依序取出每個三位數區塊，
        /// 再以遞迴處理區塊內的百位、十位與個位；輸入需介於 0 到 <see cref="int.MaxValue"/>，
        /// 輸出為單字間只有一個空格且沒有前後空白的英文數字，輸入為 0 時回傳 <c>Zero</c>。
        ///
        /// 參考：
        /// https://leetcode.cn/problems/integer-to-english-words/solutions/1040791/zheng-shu-zhuan-huan-ying-wen-biao-shi-b-ivik/
        /// https://leetcode.cn/problems/integer-to-english-words/solutions/1040978/gong-shui-san-xie-zi-fu-chuan-da-mo-ni-b-0my6/
        /// https://leetcode.cn/problems/integer-to-english-words/solutions/1844462/by-stormsunshine-6a2y/
        /// </summary>
        /// <param name="num">符合題目限制的非負 32 位元整數。</param>
        /// <returns>輸入數值的英文文字表示。</returns>
        public static string NumberToWords(int num)
        {
            if (num == 0)
            {
                return "Zero";
            }

            StringBuilder sb = new StringBuilder();

            // 32 位元非負整數最多需要 Billion、Million、Thousand 與個位四個三位數區塊。
            for (int i = 3, unit = 1000000000; i >= 0; i--, unit /= 1000)
            {
                int currnum = num / unit;

                if (currnum != 0)
                {
                    num -= currnum * unit;
                    StringBuilder curr = new StringBuilder();
                    Recursion(curr, currnum);

                    // 只有非零區塊才附加位階，避免產生「Zero Thousand」一類文字。
                    curr.Append(thousands[i]).Append(" ");
                    sb.Append(curr);
                }
            }

            // 結尾沒有空白
            return sb.ToString().Trim();
        }

        /// <summary>
        /// 以由低位到高位的三位數分組法，將非負整數轉換成英文文字表示。
        /// 解題概念是反覆以餘數取得目前三位數區塊，使用疊代流程轉換區塊內容，
        /// 最後反轉有效區塊以恢復閱讀順序；輸入需介於 0 到 <see cref="int.MaxValue"/>，
        /// 輸出為單字間只有一個空格且沒有前後空白的英文數字。
        /// </summary>
        /// <param name="num">符合題目限制的非負 32 位元整數。</param>
        /// <returns>輸入數值的英文文字表示；輸入為 0 時回傳 <c>Zero</c>。</returns>
        public static string NumberToWords2(int num)
        {
            if (num == 0)
            {
                return "Zero";
            }

            List<string> chunks = new List<string>();
            int scaleIndex = 0;

            while (num > 0)
            {
                int chunkValue = num % 1000;

                if (chunkValue != 0)
                {
                    string chunkWords = ConvertChunkIterative(chunkValue);
                    string scale = thousands[scaleIndex];
                    chunks.Add(scale.Length == 0 ? chunkWords : $"{chunkWords} {scale}");
                }

                num /= 1000;
                scaleIndex++;
            }

            // 區塊由低位數依序加入，反轉後才會得到英文由高位到低位的閱讀順序。
            chunks.Reverse();
            return string.Join(" ", chunks);
        }

        /// <summary>
        /// 以疊代判斷將一個非零三位數區塊轉換為英文。
        /// 依序取出百位，再區分 20 以上的整十、10 到 19 的特殊字與剩餘個位；
        /// 輸入需介於 1 到 999，輸出不包含 Thousand、Million、Billion 或前後空白。
        /// </summary>
        /// <param name="num">要轉換的非零三位數區塊，範圍為 1 到 999。</param>
        /// <returns>區塊內百位、十位與個位組成的英文文字。</returns>
        private static string ConvertChunkIterative(int num)
        {
            List<string> words = new List<string>();

            if (num >= 100)
            {
                words.Add(singles[num / 100]);
                words.Add("Hundred");
                num %= 100;
            }

            if (num >= 20)
            {
                words.Add(tens[num / 10]);
                num %= 10;
            }

            if (num >= 10)
            {
                words.Add(teens[num - 10]);
            }
            else if (num > 0)
            {
                words.Add(singles[num]);
            }

            return string.Join(" ", words);
        }

        /// <summary>
        /// 將不超過三位數的區塊遞迴附加到指定的文字建構器。
        /// 解題概念是依數值範圍選擇個位、10 到 19、整十或百位單字，
        /// 並將尚未處理的餘數交給下一層遞迴；輸入需介於 0 到 999，
        /// 輸出會附加至 <paramref name="curr"/>，每個新增單字後保留一個空格。
        /// </summary>
        /// <param name="curr">接收目前三位數區塊英文文字的建構器。</param>
        /// <param name="num">要轉換的三位數區塊，範圍為 0 到 999。</param>
        public static void Recursion(StringBuilder curr, int num)
        {
            if (num == 0)
            {
                return;
            }
            else if (num < 10)
            {
                curr.Append(singles[num]).Append(" ");
            }
            else if (num < 20)
            {
                // 10 到 19 各有獨立單字，減去 10 後即為 teens 的索引。
                curr.Append(teens[num - 10]).Append(" ");
            }
            else if (num < 100)
            {
                curr.Append(tens[num / 10]).Append(" ");
                Recursion(curr, num % 10);
            }
            else
            {
                curr.Append(singles[num / 100]).Append(" Hundred ");
                Recursion(curr, num % 100);
            }
        }

        /// <summary>
        /// 表示一筆可執行的整數轉英文驗證案例。
        /// 輸入包含案例名稱、符合題目限制的非負整數與預期英文文字；
        /// 建立後供測試流程讀取，不會修改其中內容。
        /// </summary>
        /// <param name="Name">案例用途或涵蓋情境。</param>
        /// <param name="Input">要轉換的非負整數。</param>
        /// <param name="Expected">符合題目格式的預期英文表示。</param>
        private sealed record SampleCase(string Name, int Input, string Expected);
    }
}
