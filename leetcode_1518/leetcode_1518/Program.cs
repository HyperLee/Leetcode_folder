namespace leetcode_1518;

class Program
{
    /// <summary>
    /// 1518. Water Bottles
    /// https://leetcode.com/problems/water-bottles/description/?envType=daily-question&envId=2025-10-01
    /// 1518. 换水问题
    /// https://leetcode.cn/problems/water-bottles/description/?envType=daily-question&envId=2025-10-01
    /// 
    /// 有 numBottles 瓶水，最初都是滿的。你可以用 numExchange 個空瓶從市場換一瓶滿的水。喝一瓶滿的水會變成空瓶。給定兩個整數 numBottles 和 numExchange，返回你可以喝的最大水瓶數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料範例
        var tests = new (int numBottles, int numExchange)[]
        {
            (9, 3),   // 範例：9 瓶, 3 個空瓶換 1 瓶 -> 可喝 13 瓶
            (15, 4),  // 範例：15 瓶, 4 個空瓶換 1 瓶 -> 可喝 19 瓶
            (0, 3),   // 邊界：沒有初始水 -> 0
            (2, 3),   // 不夠換一次 -> 2
        };

        Console.WriteLine("測試結果1：");
        foreach (var (numBottles, numExchange) in tests)
        {
            int result = NumWaterBottles(numBottles, numExchange);
            Console.WriteLine($"numBottles={numBottles}, numExchange={numExchange} => drank={result}");
        }


        Console.WriteLine("------------------");
        Console.WriteLine("測試結果2：");
        foreach (var (numBottles, numExchange) in tests)
        {
            int result = NumWaterBottles2(numBottles, numExchange);
            Console.WriteLine($"numBottles={numBottles}, numExchange={numExchange} => drank={result}");
        }        
    }

    /// <summary>
    /// 模擬解法，依題目規則反覆交換空瓶為滿瓶並飲用。
    /// 
    /// 解題說明（中文）：
    /// 初始時有 numBottles 瓶滿水，喝一瓶會得到一個空瓶；當你累積到 numExchange 個空瓶時
    /// 可以在市場換到一瓶滿水，然後繼續喝並取得空瓶。重複此過程直到無法再交換為止。
    /// 方法透過模擬「空瓶數」隨時間變化來計算總共能喝到的水瓶數。
    /// 
    /// 輸入：
    /// - numBottles: 初始滿水瓶數（整數，>= 1）
    /// - numExchange: 兌換所需的空瓶數（整數，>= 2）
    /// 輸出：
    /// - 回傳可以喝到的最大水瓶數（整數）。
    /// 
    /// 時間複雜度：O(k)，其中 k 為喝到的總瓶數（每次迭代至少使空瓶數減少 numExchange-1）。
    /// 空間複雜度：O(1)。
    /// 
    /// 邊界情況：
    /// - 若 numBottles 為 0，回傳 0。
    /// - 若 numExchange <= 1，理論上可以無限兌換但根據題意 numExchange >= 2，這裡不做無限迴圈處理。
    /// </summary>
    /// <param name="numBottles">初始滿瓶數</param>
    /// <param name="numExchange">兌換所需空瓶數</param>
    /// <returns>可以喝到的最大水瓶數</returns>
    public static int NumWaterBottles(int numBottles, int numExchange)
    {
        // 當前手上的空瓶數（開始時喝完初始滿瓶會形成空瓶）
        int empty = 0;
        // 總共喝到的瓶數
        int totalDrank = 0;

        // 先喝掉初始的滿瓶
        totalDrank += numBottles;
        empty += numBottles;

        // 當空瓶數夠換新的一瓶時，持續兌換並喝
        while (empty >= numExchange)
        {
            // 用空瓶兌換到的新瓶數
            int newFull = empty / numExchange;
            // 更新總喝瓶數
            totalDrank += newFull;
            // 兌換後剩下的空瓶數（餘數）加上新喝完會得到的空瓶
            empty = (empty % numExchange) + newFull;
        }

        return totalDrank;
    }

    /// <summary>
    /// 數學推導解法（O(1) 時間）：
    /// 
    /// 思路：
    /// 1. 先喝掉初始的 b 瓶（得到 b 個空瓶）。
    /// 2. 若 b < e（numExchange），則無法進行任何兌換，答案為 b。
    /// 3. 若 b >= e，每次用 e 個空瓶換 1 瓶滿水，喝掉後會再得到 1 個空瓶，
    ///    等同於每次凈損失 e-1 個空瓶。因此當我們能夠進行 n 次兌換（並喝掉這 n 瓶）時，
    ///    空瓶數會變為 b - n*(e-1)。當這個值小於 e 時就不能再兌換了。
    ///    因此要求最小的 n 使得 b - n*(e-1) < e。解得
    ///
    ///    n = floor((b - e) / (e - 1)) + 1
    ///
    /// 4. 最後總共喝到的瓶數為 b + n。
    /// 
    /// 邊界與輸入檢查：
    /// - 若 numExchange <= 1，題目並不定義此情況（會導致無限兌換），此處視為不合法參數並拋出例外。
    /// - 若 numBottles < 0，視為不合法參數並拋出例外。
    /// 
    /// 範例：b=9, e=3 -> n = floor((9-3)/(3-1)) + 1 = floor(6/2)+1 = 3+1 = 4 -> total = 9+4 = 13
    ///
    /// </summary>
    /// <param name="numBottles">初始滿瓶數 b（整數，應 >= 0）</param>
    /// <param name="numExchange">兌換所需空瓶數 e（整數，應 >= 2）</param>
    /// <returns>可以喝到的最大水瓶數</returns>
    public static int NumWaterBottles2(int numBottles, int numExchange)
    {
        // 參數檢查：確保輸入合理
        if (numBottles < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numBottles), "numBottles must be >= 0");
        }
        if (numExchange <= 1)
        {
            throw new ArgumentException("numExchange must be >= 2 (invalid or would allow infinite exchanges).", nameof(numExchange));
        }

        // 若初始瓶數不足以換一次，直接回傳初始瓶數
        if (numBottles < numExchange)
        {
            return numBottles;
        }

        // 使用數學公式計算可額外獲得的瓶數 n
        // 注意整數除法自動為向下取整（floor）
        int n = (numBottles - numExchange) / (numExchange - 1) + 1;

        // 總共喝到的瓶數為初始的 b 加上可以換到並喝掉的 n 瓶
        return numBottles + n;
    }
}
