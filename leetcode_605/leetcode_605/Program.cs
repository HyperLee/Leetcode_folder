namespace leetcode_605
{
    internal class Program
    {
        /// <summary>
        /// 605. Can Place Flowers
        /// https://leetcode.com/problems/can-place-flowers/
        /// 605. 种花问题
        /// https://leetcode.cn/problems/can-place-flowers/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行固定的種花問題案例，讓兩種解法分別使用獨立輸入副本，
        /// 並同時檢查回傳結果與輸入陣列是否保持不變。
        /// 案例輸入皆符合題目限制，最後輸出通過案例數。
        /// </summary>
        private static void RunSamples()
        {
            (string Name, int[] Flowerbed, int FlowersToPlant, bool Expected)[] samples =
            [
                ("官方範例一", [1, 0, 0, 0, 1], 1, true),
                ("官方範例二", [1, 0, 0, 0, 1], 2, false),
                ("不需要再種花", [1, 0, 1], 0, true),
                ("單一空地可種花", [0], 1, true),
                ("單一土地已有花", [1], 1, false),
                ("全空花圃達到最大容量", [0, 0, 0, 0, 0], 3, true),
                ("左右邊界皆可種花", [0, 0, 1, 0, 0], 2, true),
                ("沒有可用空位", [1, 0, 1, 0, 1], 1, false)
            ];

            int passedCases = 0;

            for (int index = 0; index < samples.Length; index++)
            {
                (string name, int[] flowerbed, int flowersToPlant, bool expected) = samples[index];
                int[] firstInput = [.. flowerbed];
                int[] secondInput = [.. flowerbed];

                bool firstActual = CanPlaceFlowers(firstInput, flowersToPlant);
                bool secondActual = CanPlaceFlowers2(secondInput, flowersToPlant);
                bool firstInputPreserved = firstInput.SequenceEqual(flowerbed);
                bool secondInputPreserved = secondInput.SequenceEqual(flowerbed);
                bool firstPassed = firstActual == expected && firstInputPreserved;
                bool secondPassed = secondActual == expected && secondInputPreserved;
                bool casePassed = firstPassed && secondPassed;

                if (casePassed)
                {
                    passedCases++;
                }

                Console.WriteLine($"案例 {index + 1}：{name}");
                Console.WriteLine($"花圃：{FormatFlowerbed(flowerbed)}，n = {flowersToPlant}");
                Console.WriteLine($"預期：{expected}");
                Console.WriteLine(
                    $"解法一（空白區段計數）：{firstActual}，輸入未修改：{firstInputPreserved} => {(firstPassed ? "PASS" : "FAIL")}");
                Console.WriteLine(
                    $"解法二（索引跳躍模擬）：{secondActual}，輸入未修改：{secondInputPreserved} => {(secondPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"案例結果：{(casePassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCases}/{samples.Length} 筆測試通過");
        }

        /// <summary>
        /// 將只包含 0 與 1 的花圃陣列格式化為容易閱讀的方括號字串。
        /// 輸入必須是有效的花圃陣列，輸出不會改動原陣列。
        /// </summary>
        /// <param name="flowerbed">要顯示的花圃陣列。</param>
        /// <returns>以逗號與空格分隔元素的方括號字串。</returns>
        private static string FormatFlowerbed(int[] flowerbed)
        {
            return $"[{string.Join(", ", flowerbed)}]";
        }

        /// <summary>
        /// 使用空白區段計數判斷是否能種下指定數量的新花。
        /// 掃描既有花朵的位置，分別計算開頭、中間與結尾連續空地可容納的花朵數；
        /// 輸入花圃只能包含 0 與 1，且原本不能有相鄰花朵，方法不會修改輸入陣列。
        /// </summary>
        /// <param name="flowerbed">符合題目限制的花圃陣列，0 表示空地，1 表示已有花朵。</param>
        /// <param name="n">希望新增的花朵數量，必須介於 0 與花圃長度之間。</param>
        /// <returns>若能在不產生相鄰花朵的前提下種下至少 <paramref name="n"/> 朵花則回傳 true，否則回傳 false。</returns>
        public static bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            int count = 0;
            int m = flowerbed.Length;
            int prev = -1;

            for (int i = 0; i < m; i++)
            {
                if (flowerbed[i] == 1)
                {
                    if (prev == -1)
                    {
                        // 第一朵既有花之前的空地只受右側邊界限制。
                        count += i / 2;
                    }
                    else
                    {
                        // 兩朵既有花之間需各保留一格，剩餘空地才能交錯種花。
                        count += (i - prev - 2) / 2;
                    }

                    if (count >= n)
                    {
                        return true;
                    }

                    prev = i;
                }
            }

            if (prev < 0)
            {
                // 全部都是空地時，可從第一格開始交錯種花。
                count += (m + 1) / 2;
            }
            else
            {
                // 最後一朵既有花之後的空地只受左側邊界限制。
                count += (m - prev - 1) / 2;
            }

            return count >= n;
        }

        /// <summary>
        /// 使用索引跳躍直接模擬種花規則。
        /// 由於游標左側始終已完成合法判斷，只需觀察目前位置與右側位置，
        /// 再依 00、01 或 10 的排列跳過下一個不可能種花的位置；
        /// 輸入花圃只能包含 0 與 1，且原本不能有相鄰花朵，方法不會修改輸入陣列。
        /// </summary>
        /// <param name="flowerbed">符合題目限制的花圃陣列，0 表示空地，1 表示已有花朵。</param>
        /// <param name="n">希望新增的花朵數量，必須介於 0 與花圃長度之間。</param>
        /// <returns>若能在不產生相鄰花朵的前提下種下至少 <paramref name="n"/> 朵花則回傳 true，否則回傳 false。</returns>
        public static bool CanPlaceFlowers2(int[] flowerbed, int n)
        {
            int length = flowerbed.Length;
            int i = 0;

            while (i < length)
            {
                if (flowerbed[i] == 0)
                {
                    // 最後一格沒有右側鄰居，且左側已由跳躍規則保證合法，因此可以直接種花。
                    if (i == length - 1)
                    {
                        n--;
                        break;
                    }

                    if (flowerbed[i + 1] == 0)
                    {
                        // 00：在目前位置種花後，下一格必須保留空白，所以前進兩格。
                        n--;
                        i += 2;
                    }
                    else if (flowerbed[i + 1] == 1)
                    {
                        // 01：目前位置與既有花右側都不能種花，直接前進三格。
                        i += 3;
                    }
                }
                else if (flowerbed[i] == 1)
                {
                    // 10：既有花的下一格不能種花，直接前進兩格。
                    i += 2;
                }

                if (n <= 0)
                {
                    // 已找到足夠位置後立即結束，不必掃描剩餘花圃。
                    return true;
                }
            }

            if (n > 0)
            {
                return false;
            }

            return true;
        }
    }
}
