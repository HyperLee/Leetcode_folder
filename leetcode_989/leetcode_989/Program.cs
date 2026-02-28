using System.Globalization;

namespace leetcode_989;

class Program
{
    /// <summary>
    /// 989. Add to Array-Form of Integer
    /// https://leetcode.com/problems/add-to-array-form-of-integer/description/
    /// 989. 数组形式的整数加法
    /// https://leetcode.cn/problems/add-to-array-form-of-integer/description/
    /// 
    /// The array-form of an integer num is an array representing its digits in left to right order.
    /// For example, for num = 1321, the array form is [1,3,2,1].
    /// Given num, the array-form of an integer, and an integer k, return the array-form of the integer num + k.
    /// 
    /// 整數 num 的陣列形式是表示其數字的陣列，順序為從左到右。
    /// 例如，對於 num = 1321，其陣列形式為 [1,3,2,1]。
    /// 給定 num（整數的陣列形式）和一個整數 k，返回整數 num + k 的陣列形式。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // ── 測試資料 ──────────────────────────────────────────────────
        // 測試案例 1: [1,2,0,0] + 34  → 期望 [1,2,3,4]
        int[] num1 = [1, 2, 0, 0];
        int   k1   = 34;

        // 測試案例 2: [2,7,4] + 181  → 期望 [4,5,5]
        int[] num2 = [2, 7, 4];
        int   k2   = 181;

        // 測試案例 3: [2,1,5] + 806  → 期望 [1,0,2,1]
        int[] num3 = [2, 1, 5];
        int   k3   = 806;

        // ── 極端測試資料 ──────────────────────────────────────────────
        // 測試案例 4: num 長度(3位) < k 位數(4位): [9,9,9] + 9001 → 期望 [1,0,0,0,0]
        int[] num4 = [9, 9, 9];
        int   k4   = 9001;

        // 測試案例 5: 單元素陣列 + 大數: [0] + 10000 → 期望 [1,0,0,0,0]
        int[] num5 = [0];
        int   k5   = 10000;

        // 測試案例 6: 全 9 進位連鎖: [9,9,9,9] + 9999 → 期望 [1,9,9,9,8]
        int[] num6 = [9, 9, 9, 9];
        int   k6   = 9999;

        // 測試案例 7: 單元素 + 比 num 位數多的 k: [5] + 995 → 期望 [1,0,0,0]
        int[] num7 = [5];
        int   k7   = 995;

        // 測試案例 8: k = 0，結果應與 num 相同: [1,2,3] + 0 → 期望 [1,2,3]
        //            ※ 注意：方法二（AddToArrayForm2）在此案例有潛在空結果問題
        int[] num8 = [1, 2, 3];
        int   k8   = 0;

        Program p = new Program();

        // ── 方法一：逐位相加 ─────────────────────────────────────────
        Console.WriteLine("=== 方法一：AddToArrayForm ===");
        Console.WriteLine($"[1,2,0,0] + 34   = [{string.Join(",", p.AddToArrayForm(num1, k1))}]");    // [1,2,3,4]
        Console.WriteLine($"[2,7,4]   + 181  = [{string.Join(",", p.AddToArrayForm(num2, k2))}]");    // [4,5,5]
        Console.WriteLine($"[2,1,5]   + 806  = [{string.Join(",", p.AddToArrayForm(num3, k3))}]");    // [1,0,2,1]
        Console.WriteLine($"[9,9,9]   + 9001 = [{string.Join(",", p.AddToArrayForm(num4, k4))}]");    // [1,0,0,0,0]
        Console.WriteLine($"[0]       +10000 = [{string.Join(",", p.AddToArrayForm(num5, k5))}]");    // [1,0,0,0,0]
        Console.WriteLine($"[9,9,9,9] + 9999 = [{string.Join(",", p.AddToArrayForm(num6, k6))}]");    // [1,9,9,9,8]
        Console.WriteLine($"[5]       + 995  = [{string.Join(",", p.AddToArrayForm(num7, k7))}]");    // [1,0,0,0]
        Console.WriteLine($"[1,2,3]   + 0    = [{string.Join(",", p.AddToArrayForm(num8, k8))}]");    // [1,2,3]

        // ── 方法二：陣列轉整數相加（示意用，有精度限制） ─────────────
        Console.WriteLine("\n=== 方法二：AddToArrayForm2 ===");
        Console.WriteLine($"[1,2,0,0] + 34   = [{string.Join(",", AddToArrayForm2(num1, k1))}]");
        Console.WriteLine($"[2,7,4]   + 181  = [{string.Join(",", AddToArrayForm2(num2, k2))}]");
        Console.WriteLine($"[2,1,5]   + 806  = [{string.Join(",", AddToArrayForm2(num3, k3))}]");
        Console.WriteLine($"[9,9,9]   + 9001 = [{string.Join(",", AddToArrayForm2(num4, k4))}]");
        Console.WriteLine($"[0]       +10000 = [{string.Join(",", AddToArrayForm2(num5, k5))}]");
        Console.WriteLine($"[9,9,9,9] + 9999 = [{string.Join(",", AddToArrayForm2(num6, k6))}]");
        Console.WriteLine($"[5]       + 995  = [{string.Join(",", AddToArrayForm2(num7, k7))}]");
        Console.WriteLine($"[1,2,3]   + 0    = [{string.Join(",", AddToArrayForm2(num8, k8))}]");     // [1,2,3]（⚠ 若 num=[0] 且 k=0，finalScore=0 導致迴圈不執行，回傳空列表）

        // ── 方法三：將 k 直接加至最低位進位展開 ─────────────────────
        Console.WriteLine("\n=== 方法三：AddToArrayForm3 ===");
        Console.WriteLine($"[1,2,0,0] + 34   = [{string.Join(",", AddToArrayForm3(num1, k1))}]");    // [1,2,3,4]
        Console.WriteLine($"[2,7,4]   + 181  = [{string.Join(",", AddToArrayForm3(num2, k2))}]");    // [4,5,5]
        Console.WriteLine($"[2,1,5]   + 806  = [{string.Join(",", AddToArrayForm3(num3, k3))}]");    // [1,0,2,1]
        Console.WriteLine($"[9,9,9]   + 9001 = [{string.Join(",", AddToArrayForm3(num4, k4))}]");    // [1,0,0,0,0]
        Console.WriteLine($"[0]       +10000 = [{string.Join(",", AddToArrayForm3(num5, k5))}]");    // [1,0,0,0,0]
        Console.WriteLine($"[9,9,9,9] + 9999 = [{string.Join(",", AddToArrayForm3(num6, k6))}]");    // [1,9,9,9,8]
        Console.WriteLine($"[5]       + 995  = [{string.Join(",", AddToArrayForm3(num7, k7))}]");    // [1,0,0,0]
        Console.WriteLine($"[1,2,3]   + 0    = [{string.Join(",", AddToArrayForm3(num8, k8))}]");    // [1,2,3]
    }

    /// <summary>
    /// 方法一：逐位相加（Digit-by-Digit Addition）
    ///
    /// ── 解題說明 ─────────────────────────────────────────────────────
    /// 模擬手算加法：從陣列最右邊（個位數）往左逐位與 k 的對應位相加。
    /// 每次只取 k 最低位（k % 10）進行相加，相加後將 k 右移一位（k /= 10）。
    /// 若當前位相加結果 ≥ 10，則將進位（1）累加到 k 的末尾（k++），並讓當前位減 10。
    /// 陣列走完後，若 k 仍有剩餘位數（k > 0），繼續將 k 的每一位加入結果。
    /// 最後反轉結果列表，因為是從個位數開始存入的。
    ///
    /// ── 時間複雜度 ───────────────────────────────────────────────────
    /// O(max(n, logK))，n 為陣列長度，logK 為 k 的位數。
    ///
    /// ── 空間複雜度 ───────────────────────────────────────────────────
    /// O(max(n, logK))，輸出結果所需空間。
    /// </summary>
    /// <param name="num">整數的陣列形式，num[0] 為最高位。</param>
    /// <param name="k">要加上的整數。</param>
    /// <returns>num + k 的陣列形式。</returns>
    public IList<int> AddToArrayForm(int[] num, int k)
    {
        // 初始化結果集與陣列長度
        List<int> res = new List<int>();
        int n = num.Length;

        // 1. 從個位數（索引 n-1）往最高位逐位相加
        for (int i = n - 1; i >= 0; i--)
        {
            // 1.1 取 k 最低位與陣列當前位相加
            //     k % 10：取 k 的個位數
            //     k /= 10：右移一位，準備下一輪取新的個位數
            int sum = num[i] + k % 10;
            k /= 10;

            // 1.2 處理進位：若 sum ≥ 10，進位 1 累加回 k 末尾
            //     k++ 等同於在 k 的最低位補上進位的 1
            //     sum -= 10 取得當前位實際值
            if (sum >= 10)
            {
                k++;       // 進位加回 k（下一輪 k % 10 時會被取出）
                sum -= 10; // 當前位只保留個位值
            }

            // 1.3 當前位結果加入列表（目前是反序：個位在前）
            res.Add(sum);
        }

        // 2. 若 k 的位數比陣列長，繼續展開 k 的剩餘位數
        for (; k > 0; k /= 10)
        {
            res.Add(k % 10); // 每次取 k 最低位加入結果
        }

        // 3. 反轉結果：因從個位數開始存入，反轉後才符合高位在前的格式
        res.Reverse();

        return res;
    }

    /// <summary>
    /// 方法二：陣列轉整數後直接相加（Array-to-Int Conversion）
    ///
    /// ── 解題說明 ─────────────────────────────────────────────────────
    /// 先將 num 陣列還原為整數，再與 k 相加，最後將結果逐位拆解存入 List。
    /// 還原方式：每個元素乘以對應的權重 10^(length-i-1) 再累加。
    ///
    /// ── 注意事項 ─────────────────────────────────────────────────────
    /// 使用 long 避免 int 溢位（int 最大約 2.1×10⁹，超過 10 位陣列即可能溢位）。
    /// 若需支援更大的數字，可改用 BigInteger。
    ///
    /// 參考：https://stackoverflow.com/questions/9564800/how-to-convert-int-array-to-int
    ///
    /// ── 時間複雜度 ───────────────────────────────────────────────────
    /// O(n)，n 為陣列長度（或結果位數，取較大者）。
    ///
    /// ── 空間複雜度 ───────────────────────────────────────────────────
    /// O(max(n, logK))，輸出結果所需空間。
    /// 
    /// 備註: 此解法在 k=0 且 num=[0] 的情況下會回傳空列表，因為 finalScore=0 導致迴圈不執行。實際使用時需注意此邊界條件。
    /// 解題不會過於此方法，僅作為示意用，實際上不建議使用此方法處理大數問題。
    /// </summary>
    /// <param name="num">整數的陣列形式，num[0] 為最高位。</param>
    /// <param name="k">要加上的整數。</param>
    /// <returns>num + k 的陣列形式（每位分開存放）。</returns>
    public static IList<int> AddToArrayForm2(int[] num, int k)
    {
        List<int> res = new List<int>();

        // 使用 long 避免大數溢位
        long finalScore = 0;

        // 將陣列各位還原為整數：num[i] × 10^(length-i-1)
        // 例如 [1,2,0,0] → 1×1000 + 2×100 + 0×10 + 0×1 = 1200
        for (int i = 0; i < num.Length; i++)
        {
            finalScore += num[i] * (long)Math.Pow(10, num.Length - i - 1);
        }

        // 整數相加
        finalScore += k;

        // 將結果逐位拆解存入 List（從最低位開始）
        // 例如 1234 → 先存 [4,3,2,1]，最後反轉為 [1,2,3,4]
        for (; finalScore > 0; finalScore /= 10)
        {
            res.Add((int)(finalScore % 10));
        }

        // 反轉結果：因從個位數開始存入，反轉後才符合高位在前的格式
        res.Reverse();

        return res;
    }

    /// <summary>
    /// 方法三：將 k 直接加至最低位，逐步進位展開（Carry-Propagation）
    ///
    /// ── 解題說明 ─────────────────────────────────────────────────────
    /// 核心思路：把「整個 k」加到陣列最低位，讓進位自然向高位擴散。
    ///
    /// 以 [1,2,3] + 912 為例，演示流程：
    ///   初始  k = 912
    ///   i=2：k += num[2]=3  → k=915    → 存入 915%10=5，k=915/10=91
    ///   i=1：k += num[1]=2  → k=93     → 存入 93%10=3，k=93/10=9
    ///   i=0：k += num[0]=1  → k=10     → 存入 10%10=0，k=10/10=1
    ///   k=1>0：             存入 1%10=1，k=1/10=0
    ///   res（反轉前）= [5,3,0,1] → 反轉後 = [1,0,3,5]
    ///
    /// 每輪迭代：
    ///   1. 若陣列還有元素，將當前位加入 k（k += num[i--]）。
    ///   2. 取 k 最低位（k % 10）存入結果。
    ///   3. k /= 10 右移，剩餘部分作為進位帶入下一輪。
    ///
    /// ── 時間複雜度 ───────────────────────────────────────────────────
    /// O(max(n, logK))。
    ///
    /// ── 空間複雜度 ───────────────────────────────────────────────────
    /// O(max(n, logK))。
    /// </summary>
    /// <param name="num">整數的陣列形式，num[0] 為最高位。</param>
    /// <param name="k">要加上的整數；同時作為進位暫存器使用。</param>
    /// <returns>num + k 的陣列形式。</returns>
    public static IList<int> AddToArrayForm3(int[] num, int k)
    {
        List<int> res = new List<int>();
        int n = num.Length;

        // 從最低位（i = n-1）開始，只要陣列未走完或 k（含進位）仍有值就繼續
        for (int i = n - 1; i >= 0 || k > 0; k /= 10)
        {
            // 若陣列還有元素，將當前位的值累加進 k
            // k 此時代表「進位 + 當前陣列位」的總和
            if (i >= 0)
            {
                k += num[i--]; // 加入當前位後，i 向左移動一位
            }

            // 取 k 最低位作為當前結果位（與方法一相同原理）
            res.Add(k % 10);
            // loop update：k /= 10，移除已處理的最低位，剩餘部分作為下一輪進位
        }

        // 結果是從低位往高位存入的，反轉後得到正確順序
        res.Reverse();

        return res;
    }
}
