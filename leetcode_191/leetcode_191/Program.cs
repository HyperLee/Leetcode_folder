namespace leetcode_191;

static class Program
{
    /// <summary>
    /// 191. Number of 1 Bits
    /// https://leetcode.com/problems/number-of-1-bits/description/
    /// 191. 位1的个数
    /// https://leetcode.cn/problems/number-of-1-bits/description/
    ///
    /// Given a positive integer n, write a function that returns the number of set bits in its binary representation (also known as the Hamming weight).
    /// 給定一個正整數 n，請撰寫一個函式，回傳它在二進位表示中 1 的個數（也稱為 Hamming weight）。
    /// </summary>
    /// <remarks>
    /// 用途：建立可直接執行的主控台示範流程，逐筆比對三種解法在固定案例上的結果。
    /// 解題概念：以資料驅動方式呈現位元掃描、逐位右移與 n &amp; (n - 1) 三種策略的輸出一致性。
    /// 輸入條件：不使用命令列參數，示範資料由程式內建案例提供。
    /// 輸出結果：將每個案例的 32 位元表示、預期答案與三種解法結果輸出到主控台。
    /// </remarks>
    /// <param name="args">未使用的命令列參數。</param>
    static void Main(string[] args)
    {
        SampleCase[] sampleCases = GetSampleCases();

        Console.WriteLine("LeetCode 191 - Number of 1 Bits");
        Console.WriteLine("比較三種 Hamming Weight 解法的主控台示範");
        Console.WriteLine(new string('-', 72));

        foreach (SampleCase sampleCase in sampleCases)
        {
            PrintCaseResult(sampleCase);
        }
    }

    /// <summary>
    /// 建立主控台示範用的固定測試資料，涵蓋題目案例與補充邊界案例。
    /// 解題概念：使用資料驅動方式集中管理案例，讓 Main 保持簡潔且容易擴充。
    /// 輸入條件：無外部輸入，案例內容由程式內建定義。
    /// 輸出結果：回傳包含輸入值、預期答案與案例分類的不可變案例陣列。
    /// </summary>
    /// <returns>供主控台逐筆驗證的示範案例陣列。</returns>
    private static SampleCase[] GetSampleCases()
    {
        return
        [
            new SampleCase(11U, 3, true),
            new SampleCase(128U, 1, true),
            new SampleCase(2147483645U, 30, true),
            new SampleCase(0U, 0, false)
        ];
    }

    /// <summary>
    /// 將單一案例的輸入、二進位表示、預期答案與三種解法結果輸出到主控台。
    /// 解題概念：集中格式化與比對流程，讓每種解法的驗證方式一致且容易閱讀。
    /// 輸入條件：sampleCase 需提供 32 位元無號整數輸入值與對應的預期答案。
    /// 輸出結果：將格式化後的驗證資訊寫入主控台，不回傳值。
    /// </summary>
    /// <param name="sampleCase">包含輸入值、預期答案與案例分類的示範資料。</param>
    private static void PrintCaseResult(SampleCase sampleCase)
    {
        int result1 = HammingWeight(sampleCase.Value);
        int result2 = HammingWeight2(sampleCase.Value);
        int result3 = HammingWeight3(sampleCase.Value);
        bool allMatch = result1 == sampleCase.Expected
            && result2 == sampleCase.Expected
            && result3 == sampleCase.Expected;
        string caseType = sampleCase.IsPrimaryExample ? "題目案例" : "補充邊界案例";

        Console.WriteLine($"案例類型：{caseType}");
        Console.WriteLine($"輸入數值：{sampleCase.Value}");
        Console.WriteLine($"32 位元二進位：{FormatBinary32(sampleCase.Value)}");
        Console.WriteLine($"預期答案：{sampleCase.Expected}");
        Console.WriteLine($"解法一 HammingWeight  ：{result1}");
        Console.WriteLine($"解法二 HammingWeight2 ：{result2}");
        Console.WriteLine($"解法三 HammingWeight3 ：{result3}");
        Console.WriteLine($"是否全部符合預期：{allMatch}");
        Console.WriteLine(new string('-', 72));
    }

    /// <summary>
    /// 將 32 位元無號整數轉為固定長度的二進位字串，方便對照每一個 bit 的位置。
    /// 解題概念：補齊前導 0 後輸出 32 位內容，可直接搭配 README 的位元示意說明。
    /// 輸入條件：value 需視為 32 位元無號整數。
    /// 輸出結果：回傳長度固定為 32 的二進位字串。
    /// </summary>
    /// <param name="value">要格式化為 32 位元二進位字串的無號整數。</param>
    /// <returns>固定 32 位元長度的二進位字串。</returns>
    private static string FormatBinary32(uint value)
    {
        return Convert.ToString((long)value, 2).PadLeft(32, '0');
    }

    /// <summary>
    /// 解法一：逐位套用位遮罩檢查每個 bit 是否為 1。
    /// 解題概念：從第 0 位到第 31 位依序建立遮罩，利用 AND 運算判斷該位置是否有 set bit。
    /// 輸入條件：n 必須以 32 位元無號整數語意解讀。
    /// 輸出結果：回傳 n 的二進位表示中 1 的總數。
    /// </summary>
    /// <param name="n">要計算 set bits 數量的 32 位元無號整數。</param>
    /// <returns>n 的 Hamming weight。</returns>
    public static int HammingWeight(uint n)
    {
        int result = 0;

        // 題目固定以 32 位元無號整數表示，因此只需要掃描 0 到 31 共 32 個 bit。
        for (int i = 0; i < 32; i++)
        {
            if ((n & (1U << i)) != 0)
            {
                result++;
            }
        }

        return result;
    }

    /// <summary>
    /// 解法二：每次檢查最低位是否為 1，然後將數字右移一位繼續統計。
    /// 解題概念：透過 n &amp; 1 取得最低位，配合無號右移逐步消化所有 bit。
    /// 輸入條件：n 必須以 32 位元無號整數語意解讀，才能保證右移時左側補 0。
    /// 輸出結果：回傳 n 的二進位表示中 1 的總數。
    /// </summary>
    /// <param name="n">要計算 set bits 數量的 32 位元無號整數。</param>
    /// <returns>n 的 Hamming weight。</returns>
    public static int HammingWeight2(uint n)
    {
        int result = 0;

        while (n != 0)
        {
            result += (int)(n & 1U);

            // 使用 uint 右移時，左側會補 0，能避免有號整數的符號位延伸問題。
            n >>= 1;
        }

        return result;
    }

    /// <summary>
    /// 解法三：重複套用 n &amp; (n - 1) 消去最右側的 1。
    /// 解題概念：每做一次 AND 運算就能移除一個 set bit，因此迴圈次數恰好等於 1 的數量。
    /// 輸入條件：n 必須以 32 位元無號整數語意解讀。
    /// 輸出結果：回傳 n 的二進位表示中 1 的總數。
    /// </summary>
    /// <param name="n">要計算 set bits 數量的 32 位元無號整數。</param>
    /// <returns>n 的 Hamming weight。</returns>
    public static int HammingWeight3(uint n)
    {
        int result = 0;

        while (n != 0)
        {
            result++;

            // n & (n - 1) 會把最右側的 1 清掉，其餘較高位保持不變。
            n &= n - 1U;
        }

        return result;
    }

    /// <summary>
    /// 封裝主控台示範案例的輸入值、預期答案與案例分類。
    /// 解題概念：讓範例輸出依資料驅動展開，避免將案例細節散落在 Main 內部。
    /// 輸入條件：Value 需符合 32 位元無號整數語意，Expected 需為對應的 set bits 數量。
    /// 輸出結果：建立一筆不可變案例資料，供主控台驗證流程使用。
    /// </summary>
    /// <param name="Value">要測試的 32 位元無號整數值。</param>
    /// <param name="Expected">預期的 set bits 數量。</param>
    /// <param name="IsPrimaryExample">是否屬於題目原始案例。</param>
    private readonly record struct SampleCase(uint Value, int Expected, bool IsPrimaryExample);
}
