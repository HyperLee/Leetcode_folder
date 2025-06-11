namespace leetcode_3445;

class Program
{

    /// <summary>
    /// 3445. Maximum Difference Between Even and Odd Frequency II
    /// https://leetcode.com/problems/maximum-difference-between-even-and-odd-frequency-ii/description/?envType=daily-question&envId=2025-06-11
    /// 3445. 奇偶频次间的最大差值 II
    /// https://leetcode.cn/problems/maximum-difference-between-even-and-odd-frequency-ii/description/?envType=daily-question&envId=2025-06-11
    /// 
    /// 題目描述：
    /// 給定一個字串 s 和一個整數 k。你的任務是找到 s 的子字串 subs 中兩個字元頻率之間的最大差值 freq[a] - freq[b]，其中：
    /// - subs 的長度至少為 k
    /// - 字元 a 在 subs 中的頻率為奇數
    /// - 字元 b 在 subs 中的頻率為偶數
    /// 回傳最大差值。
    /// 注意：subs 可以包含超過 2 個不同的字元。
    /// 
    /// </summary>    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();        // 測試案例 1: 基本測試
        string test1 = "01234";
        int k1 = 3;
        int result1_1 = program.MaxDifference(test1, k1);
        int result1_2 = program.MaxDifference2(test1, k1);
        Console.WriteLine($"測試案例 1: s=\"{test1}\", k={k1}");
        Console.WriteLine($"方法一結果: {result1_1}");
        Console.WriteLine($"方法二結果: {result1_2}");
        Console.WriteLine();

        // 測試案例 2: 重複字元
        string test2 = "001122";
        int k2 = 4;
        int result2_1 = program.MaxDifference(test2, k2);
        int result2_2 = program.MaxDifference2(test2, k2);
        Console.WriteLine($"測試案例 2: s=\"{test2}\", k={k2}");
        Console.WriteLine($"方法一結果: {result2_1}");
        Console.WriteLine($"方法二結果: {result2_2}");
        Console.WriteLine();

        // 測試案例 3: 最小長度測試
        string test3 = "0123";
        int k3 = 2;
        int result3_1 = program.MaxDifference(test3, k3);
        int result3_2 = program.MaxDifference2(test3, k3);
        Console.WriteLine($"測試案例 3: s=\"{test3}\", k={k3}");
        Console.WriteLine($"方法一結果: {result3_1}");
        Console.WriteLine($"方法二結果: {result3_2}");
        Console.WriteLine();        // 測試案例 4: 較長字串測試
        string test4 = "0012301234";
        int k4 = 5;
        int result4_1 = program.MaxDifference(test4, k4);
        int result4_2 = program.MaxDifference2(test4, k4);
        Console.WriteLine($"測試案例 4: s=\"{test4}\", k={k4}");
        Console.WriteLine($"方法一結果: {result4_1}");
        Console.WriteLine($"方法二結果: {result4_2}");
        Console.WriteLine();

        // 測試案例 5: 邊界測試 - k等於字串長度
        string test5 = "01010";
        int k5 = 5;
        int result5_1 = program.MaxDifference(test5, k5);
        int result5_2 = program.MaxDifference2(test5, k5);
        Console.WriteLine($"測試案例 5: s=\"{test5}\", k={k5}");
        Console.WriteLine($"方法一結果: {result5_1}");
        Console.WriteLine($"方法二結果: {result5_2}");
        Console.WriteLine();

        Console.WriteLine("所有測試完成！");
        
        // 效能測試
        Console.WriteLine("\n=== 效能比較測試 ===");
        TestPerformance();
    }
    
    /// <summary>
    /// 效能比較測試方法
    /// </summary>
    static void TestPerformance()
    {
        Program program = new Program();
        
        // 建立大規模測試資料
        string largeTestCase = new string('0', 10000) + new string('1', 10000) + new string('2', 5000);
        int k = 5000;
        
        Console.WriteLine($"大規模測試：字串長度 = {largeTestCase.Length}, k = {k}");
        
        // 測試方法一的執行時間
        var sw1 = System.Diagnostics.Stopwatch.StartNew();
        int result1 = program.MaxDifference(largeTestCase, k);
        sw1.Stop();
        
        // 測試方法二的執行時間
        var sw2 = System.Diagnostics.Stopwatch.StartNew();
        int result2 = program.MaxDifference2(largeTestCase, k);
        sw2.Stop();
        
        Console.WriteLine($"方法一結果: {result1}, 執行時間: {sw1.ElapsedMilliseconds}ms");
        Console.WriteLine($"方法二結果: {result2}, 執行時間: {sw2.ElapsedMilliseconds}ms");
        
        if (result1 == result2)
        {
            Console.WriteLine("✅ 結果一致性驗證通過");
        }
        else
        {
            Console.WriteLine("❌ 結果不一致！");
        }
        
        double ratio = (double)sw1.ElapsedMilliseconds / sw2.ElapsedMilliseconds;
        if (ratio > 1.1)
        {
            Console.WriteLine($"方法二比方法一快 {ratio:F2} 倍");
        }
        else if (ratio < 0.9)
        {
            Console.WriteLine($"方法一比方法二快 {1/ratio:F2} 倍");
        }
        else
        {
            Console.WriteLine("兩種方法效能接近");
        }
    }

    /// <summary>
    /// ref:https://leetcode.cn/problems/maximum-difference-between-even-and-odd-frequency-ii/solutions/3061845/mei-ju-qian-zhui-he-hua-dong-chuang-kou-6cwsm/?envType=daily-question&envId=2025-06-11
    /// 
    /// 解題方法：使用滑動視窗和前綴和技巧
    /// 解題思路：
    /// 1. 枚舉所有可能的字元對 (x, y)，其中 x 為奇數頻率字元，y 為偶數頻率字元
    /// 2. 使用滑動視窗維護子字串，確保長度至少為 k
    /// 3. 對於每個位置，計算當前字元頻率減去之前最小的字元頻率差值
    /// 4. 使用奇偶性來優化搜索，記錄不同奇偶組合下的最小差值
    /// 時間複雜度：O(n * 字元數^2)，空間複雜度：O(字元數)
    /// </summary>
    /// <param name="s">輸入字串</param>
    /// <param name="k">子字串最小長度</param>
    /// <returns>奇數頻率字元與偶數頻率字元的最大頻率差值</returns>
    public int MaxDifference(string s, int k)
    {
        const int INF = int.MaxValue / 2;
        char[] charArray = s.ToCharArray();
        int ans = -INF;

        // 枚舉所有可能的字元對 (x, y)
        // x 代表奇數頻率的字元，y 代表偶數頻率的字元
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                // 跳過相同字元的情況
                if (y == x)
                {
                    continue;
                }

                // curS: 當前位置(右邊界)的字元頻率統計
                int[] curS = new int[5];
                // preS: 左邊界的字元頻率統計
                int[] preS = new int[5];
                // minS[p][q]: 記錄在 x 字元奇偶性為 p，y 字元奇偶性為 q 時的最小差值
                int[,] minS = { { INF, INF }, { INF, INF } };
                int left = 0;

                // 遍歷字串的每個位置作為右邊界
                for (int i = 0; i < charArray.Length; i++)
                {
                    // 更新當前位置的字元頻率
                    curS[charArray[i] - '0']++;
                    int r = i + 1;

                    // 維護滑動視窗：當視窗長度 >= k 且包含目標字元時，移動左邊界
                    while (r - left >= k && curS[x] > preS[x] && curS[y] > preS[y])
                    {
                        // 計算左邊界字元的奇偶性
                        int p = preS[x] & 1;  // x 字元在左邊界的奇偶性
                        int q = preS[y] & 1;  // y 字元在左邊界的奇偶性

                        // 更新最小差值記錄 - 這裡只是記錄資訊
                        minS[p, q] = Math.Min(minS[p, q], preS[x] - preS[y]);

                        // 移動左邊界，更新前綴頻率統計
                        preS[charArray[left] - '0']++;
                        left++;
                    }                    // 計算當前的最大差值 - 這裡才真正處理奇偶性要求
                    // curS[x] - curS[y]: 當前 x 和 y 的頻率差
                    // minS[curS[x] & 1 ^ 1, curS[y] & 1]: 對應奇偶組合的最小歷史差值
                    // curS[x] & 1 ^ 1：當前 x 字元的互補奇偶性
                    if (minS[curS[x] & 1 ^ 1, curS[y] & 1] != INF)
                    {
                        ans = Math.Max(ans, curS[x] - curS[y] - minS[curS[x] & 1 ^ 1, curS[y] & 1]);
                    }
                    //                                        ↑關鍵：透過 & 1 ^ 1 找互補狀態
                }            }
        }

        return ans == -INF ? -1 : ans;
    }

    /// <summary>
    /// ref:https://leetcode.cn/problems/maximum-difference-between-even-and-odd-frequency-ii/solutions/3694397/qi-ou-pin-ci-jian-de-zui-da-chai-zhi-ii-vnz1n/?envType=daily-question&envId=2025-06-11
    /// 
    /// 解題方法：枚舉字元 + 雙指針技巧
    /// 解題思路：
    /// 1. 枚舉所有可能的字元對 (a, b)，其中 a 為奇數頻率字元，b 為偶數頻率字元
    /// 2. 使用奇偶性狀態編碼：00(偶偶), 01(偶奇), 10(奇偶), 11(奇奇)，目標是找到 10 的情況
    /// 3. 使用雙指針維護滑動視窗：right 指針擴展範圍，left 指針在滿足條件時移動
    /// 4. 利用異或運算找到互補狀態，計算最大差值
    /// 
    /// 狀態編碼公式：status = (cntA % 2) × 2 + (cntB % 2)
    /// 答案計算公式：(cntA - cntB) - best[statusRight ⊕ 0b10]
    /// 
    /// 時間複雜度：O(n × 字元數^2)，空間複雜度：O(1)
    /// </summary>
    /// <param name="s">輸入字串，只包含數字字元 '0' 到 '4'</param>
    /// <param name="k">子字串最小長度</param>
    /// <returns>奇數頻率字元與偶數頻率字元的最大頻率差值</returns>
    public int MaxDifference2(string s, int k)    {
        int n = s.Length;
        int ans = int.MinValue;
        
        // 枚舉所有可能的字元對 (a, b)，其中 a 需要奇數頻率，b 需要偶數頻率
        foreach (char a in new char[] { '0', '1', '2', '3', '4' })
        {
            foreach (char b in new char[] { '0', '1', '2', '3', '4' })
            {
                // 跳過相同字元的情況
                if (a == b)
                {
                    continue;
                }
                
                // best[status]: 記錄每種奇偶性組合下 (prevA - prevB) 的最小值
                // status 編碼：00(偶偶), 01(偶奇), 10(奇偶), 11(奇奇)
                int[] best = new int[4];
                Array.Fill(best, int.MaxValue);
                
                // cntA, cntB: 從字串開始到 right 位置，字元 a 和 b 的出現次數
                int cntA = 0, cntB = 0;
                // prevA, prevB: 從字串開始到 left 位置，字元 a 和 b 的出現次數
                int prevA = 0, prevB = 0;
                int left = -1;  // 左指針，初始化為 -1
                
                // 右指針遍歷整個字串
                for (int right = 0; right < n; right++)
                {
                    // 更新字元計數
                    if (s[right] == a)
                    {
                        cntA++;
                    }
                    if (s[right] == b)
                    {
                        cntB++;
                    }
                    
                    // 移動左指針的條件：
                    // 1. 子字串長度 >= k
                    // 2. 字元 b 在子字串中出現次數 >= 2（確保為偶數且非0）
                    while (right - left >= k && cntB - prevB >= 2)
                    {
                        // 計算左邊界的奇偶性狀態
                        int leftStatus = GetStatus(prevA, prevB);
                        
                        // 更新該狀態下的最小差值
                        best[leftStatus] = Math.Min(best[leftStatus], prevA - prevB);
                        
                        // 移動左指針
                        left++;
                        if (s[left] == a)
                        {
                            prevA++;
                        }
                        if (s[left] == b)
                        {
                            prevB++;
                        }
                    }
                    
                    // 計算當前右邊界的奇偶性狀態
                    int rightStatus = GetStatus(cntA, cntB);
                    
                    // 我們需要找狀態為 10 的子字串（a奇數，b偶數）
                    // 因此需要左端點的狀態是 rightStatus ⊕ 0b10
                    // 0b10 = 2（二進制：10）
                    if (best[rightStatus ^ 0b10] != int.MaxValue)
                    {
                        // 計算答案：(cntA - cntB) - best[complementStatus]
                        // 這裡減去最小的歷史差值來獲得最大的當前差值
                        ans = Math.Max(ans, (cntA - cntB) - best[rightStatus ^ 0b10]);
                    }
                }
            }        }
        return ans == int.MinValue ? -1 : ans;
    }

    /// <summary>
    /// 獲取字元頻率的奇偶性狀態
    /// 狀態編碼公式：status = (cntA % 2) × 2 + (cntB % 2)
    /// 使用位元運算實現：((cntA & 1) << 1) | (cntB & 1)
    /// 
    /// 四種可能的狀態：
    /// - 00 (0): cntA偶數, cntB偶數
    /// - 01 (1): cntA偶數, cntB奇數  
    /// - 10 (2): cntA奇數, cntB偶數 ← 目標狀態
    /// - 11 (3): cntA奇數, cntB奇數
    /// </summary>
    /// <param name="cntA">字元 A 的出現次數</param>
    /// <param name="cntB">字元 B 的出現次數</param>
    /// <returns>奇偶性狀態編碼 (0-3)</returns>
    private int GetStatus(int cntA, int cntB)
    {
        return ((cntA & 1) << 1) | (cntB & 1);
    }

}