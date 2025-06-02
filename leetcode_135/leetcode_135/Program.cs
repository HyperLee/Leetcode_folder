namespace leetcode_135;

class Program
{
    /// <summary>
    /// 135. Candy
    /// https://leetcode.com/problems/candy/description/
    /// 135. 分发糖果
    /// https://leetcode.cn/problems/candy/description/?envType=daily-question&envId=2025-06-02
    /// 
    /// 有 n 個小孩站成一排。給你一個整數陣列 ratings 表示每個小孩的評分。
    /// 你需要給這些小孩分發糖果，需遵守下列規則：
    /// 1. 每個小孩至少分到 1 顆糖果。
    /// 2. 評分較高的小孩必須比他相鄰的小孩分到更多糖果。
    /// 請返回你需要準備的最少糖果數量，才能分發給這些小孩。
    /// 
    /// 解題提示：
    /// 1. 可以用兩次遍歷（從左到右、從右到左）來確保每個小孩都滿足規則。
    /// 2. 先從左到右，若 ratings[i] > ratings[i-1]，則 candies[i] = candies[i-1] + 1。
    /// 3. 再從右到左，若 ratings[i] > ratings[i+1]，則 candies[i] = Math.Max(candies[i], candies[i+1] + 1)。
    /// 4. 最後將 candies 陣列總和即為答案。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] ratings1 = { 1, 0, 2 };
        int[] ratings2 = { 1, 2, 2 };
        int[] ratings3 = { 1, 3, 4, 5, 2 };
        int[] ratings4 = { 5, 4, 3, 2, 1 };
        int[] ratings5 = { 1, 2, 3, 2, 1 };
        var solver = new Program();
        Console.WriteLine($"方法1-測試1: {solver.Candy(ratings1)} (預期: 5)");
        Console.WriteLine($"方法1-測試2: {solver.Candy(ratings2)} (預期: 4)");
        Console.WriteLine($"方法1-測試3: {solver.Candy(ratings3)} (預期: 11)");
        Console.WriteLine($"方法1-測試4: {solver.Candy(ratings4)} (預期: 15)");
        Console.WriteLine($"方法1-測試5: {solver.Candy(ratings5)} (預期: 9)");
        Console.WriteLine($"方法2-測試1: {solver.Candy2(ratings1)} (預期: 5)");
        Console.WriteLine($"方法2-測試2: {solver.Candy2(ratings2)} (預期: 4)");
        Console.WriteLine($"方法2-測試3: {solver.Candy2(ratings3)} (預期: 11)");
        Console.WriteLine($"方法2-測試4: {solver.Candy2(ratings4)} (預期: 15)");
        Console.WriteLine($"方法2-測試5: {solver.Candy2(ratings5)} (預期: 9)");
    }


    /// <summary>
    /// 根據 ratings 分發糖果，確保每個小孩至少 1 顆，且評分高者比鄰居多。
    /// 解題說明：
    /// 1. 先初始化每個小孩 1 顆糖果。
    /// 2. 從左到右遍歷，若 ratings[i] > ratings[i-1]，則 candies[i] = candies[i-1] + 1。
    /// 3. 再從右到左遍歷，若 ratings[i] > ratings[i+1]，則 candies[i] = Math.Max(candies[i], candies[i+1] + 1)。
    /// 4. 最後 candies 陣列總和即為最少所需糖果數。
    /// 
    /// 上述 2, 3 點的簡易思考就是要比前一個 index 大就好
    /// 左到右 前一個就是 i 比 i - 1 大
    /// 右到左 後一個就是 i 比 i + 1 大
    /// </summary>
    /// <param name="ratings">每個小孩的評分陣列</param>
    /// <returns>最少所需糖果數</returns>
    public int Candy(int[] ratings)
    {
        int n = ratings.Length;
        if (n == 0) return 0;

        int[] candies = new int[n];
        // 初始化每個小孩至少 1 顆糖果
        for (int i = 0; i < n; i++)
        {
            candies[i] = 1;
        }

        // 從左到右遍歷，確保右邊比左邊分高時糖果數增加
        for (int i = 1; i < n; i++)
        {
            // 比前一個大， i 的前一個就是 i - 1
            if (ratings[i] > ratings[i - 1])
            {
                candies[i] = candies[i - 1] + 1;
            }
        }

        // 從右到左遍歷，確保左邊比右邊分高時糖果數增加
        for (int i = n - 2; i >= 0; i--)
        {
            // 比後一個大， i 的後一個就是 i + 1
            if (ratings[i] > ratings[i + 1])
            {
                // 注意這是關鍵點: 取最大值，確保不會因為右邊的增加而影響左邊的糖果數
                candies[i] = Math.Max(candies[i], candies[i + 1] + 1);
            }
        }

        // 計算總糖果數量
        int totalCandies = 0;
        foreach (var candy in candies)
        {
            totalCandies += candy;
        }

        return totalCandies;
    }


    /// <summary>
    /// 方法二：優化的單次遍歷
    /// 解題說明：
    /// 這個演算法只需一次遍歷即可計算最少糖果數。
    /// 主要思路：
    /// 1. 用 inc 記錄當前遞增序列長度，dec 記錄遞減序列長度，pre 記錄前一位分到的糖果數。
    /// 2. 若 ratings[i] >= ratings[i-1]，代表遞增或持平，重設 dec，pre 根據是否持平決定是 1 或 pre+1，並加到總數。
    /// 3. 若 ratings[i] < ratings[i-1]，代表遞減，dec 累加，若 dec == inc 需額外補一顆糖果給遞增序列起點，避免違規。
    /// 4. 最後回傳總糖果數。
    /// ref: https://leetcode.cn/problems/candy/solutions/533150/fen-fa-tang-guo-by-leetcode-solution-f01p/?envType=daily-question&envId=2025-06-02
    /// </summary>
    /// <param name="ratings">每個小孩的評分陣列</param>
    /// <returns>最少所需糖果數</returns>
    public int Candy2(int[] ratings)
    {
        int n = ratings.Length;
        int res = 1; // 總糖果數，第一個小孩至少 1 顆
        int inc = 1, dec = 0, pre = 1; // inc: 遞增序列長度, dec: 遞減序列長度, pre: 前一位分到的糖果數

        for (int i = 1; i < n; i++)
        {
            if (ratings[i] >= ratings[i - 1]) // 遞增或持平
            {
                dec = 0; // 遞減序列歸零
                pre = ratings[i] == ratings[i - 1] ? 1 : pre + 1; // 持平給 1，否則 +1
                res += pre; // 累加糖果數
                inc = pre; // 更新遞增序列長度
            }
            else // 遞減
            {
                dec++; // 遞減序列長度 +1
                if (dec == inc) // 若遞減長度等於遞增長度，需補一顆給遞增起點
                {
                    dec++;
                }
                res += dec; // 累加遞減序列糖果數
                pre = 1; // 遞減時當前只給 1 顆
            }
        }
        return res;
    }
}
