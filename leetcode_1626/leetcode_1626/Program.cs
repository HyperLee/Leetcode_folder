namespace leetcode_1626;

/// <summary>
/// 球員類別，用於儲存球員的分數與年齡，並實作比較介面以支援排序
/// </summary>
class Player : IComparable<Player>
{
    /// <summary>
    /// 球員的分數
    /// </summary>
    public int score;

    /// <summary>
    /// 球員的年齡
    /// </summary>
    public int age;

    /// <summary>
    /// 比較方法：先依年齡排序，若年齡相同則依分數排序（皆為升序）
    /// </summary>
    /// <param name="o">要比較的另一位球員</param>
    /// <returns>比較結果：負數表示當前物件較小，正數表示較大，零表示相等</returns>
    public int CompareTo(Player? o)
    {
        if (o is null)
        {
            return 1;
        }

        if (this.age == o.age)
        {
            return this.score - o.score;
        }
        else
        {
            return this.age - o.age;
        }
    }
}

class Program
{
    /// <summary>
    /// 1626. Best Team With No Conflicts
    /// https://leetcode.com/problems/best-team-with-no-conflicts/description/
    /// 1626. 无矛盾的最佳球队
    /// https://leetcode.cn/problems/best-team-with-no-conflicts/description/
    /// 
    /// 你是籃球隊的經理。為了即將到來的比賽，你想挑選一支總分最高的隊伍。
    /// 隊伍的分數為隊內所有球員分數之和。
    /// 
    /// 但隊伍中不得出現衝突：當年輕球員的分數嚴格高於年長球員時，視為衝突。同齡球員之間不算衝突。
    /// 
    /// 給定兩個陣列 `scores` 和 `ages`，其中 `scores[i]` 與 `ages[i]` 分別表示第 i 位球員的分數與年齡，請回傳所有可能隊伍中能取得的最高總分。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：預期輸出 34
        // 選擇所有球員：1 + 3 + 5 + 10 + 15 = 34
        // 因為年齡由小到大對應的分數也是由小到大，不會產生衝突
        int[] scores1 = [1, 3, 5, 10, 15];
        int[] ages1 = [1, 2, 3, 4, 5];
        Console.WriteLine($"測試案例 1: {solution.BestTeamScore(scores1, ages1)}"); // 預期: 34

        // 測試案例 2：預期輸出 16
        // 選擇年齡為 2 的兩位球員 (分數 4, 5) 與年齡為 3 的球員 (分數 6)：4 + 5 + 6 = 15，但最佳為選擇 4 + 6 + 6 = 16？
        // 實際：年齡 [1,2,2,3]，分數 [4,4,5,6]，最佳選擇 4+5+6 = 15 或 4+4+6 = 14... 需驗證
        int[] scores2 = [4, 5, 6, 5];
        int[] ages2 = [2, 1, 2, 1];
        Console.WriteLine($"測試案例 2: {solution.BestTeamScore(scores2, ages2)}"); // 預期: 16

        // 測試案例 3：預期輸出 6
        // 只能選擇一位球員，最高分為 6
        int[] scores3 = [1, 2, 3, 5];
        int[] ages3 = [8, 9, 10, 1];
        Console.WriteLine($"測試案例 3: {solution.BestTeamScore(scores3, ages3)}"); // 預期: 6
    }

    /// <summary>
    /// 計算無衝突最佳球隊的最高總分
    /// 
    /// <para><b>解題思路：排序 + 動態規劃</b></para>
    /// 
    /// <para><b>核心概念：</b></para>
    /// <list type="bullet">
    ///   <item>衝突定義：年輕球員的分數「嚴格高於」年長球員時產生衝突</item>
    ///   <item>關鍵觀察：若將球員依年齡排序，則問題轉化為「選擇一個分數非遞減的子序列，使其總和最大」</item>
    ///   <item>這本質上是「最長遞增子序列 (LIS)」的變形，但目標是最大總和而非最大長度</item>
    /// </list>
    /// 
    /// <para><b>演算法步驟：</b></para>
    /// <list type="number">
    ///   <item>將所有球員依年齡升序排列，若年齡相同則依分數升序排列</item>
    ///   <item>使用 DP 陣列，dp[i] 表示以第 i 位球員結尾的最大分數總和</item>
    ///   <item>對於每位球員 i，檢查所有在其之前的球員 j，若 j 的分數 ≤ i 的分數，則可將 j 的隊伍延伸</item>
    ///   <item>最終答案為 dp 陣列中的最大值</item>
    /// </list>
    /// 
    /// <para><b>時間複雜度：</b>O(n²)，其中 n 為球員數量</para>
    /// <para><b>空間複雜度：</b>O(n)，用於儲存 DP 陣列與排序後的球員資料</para>
    /// </summary>
    /// <param name="scores">每位球員的分數陣列</param>
    /// <param name="ages">每位球員的年齡陣列</param>
    /// <returns>無衝突隊伍能取得的最高總分</returns>
    /// <example>
    /// <code>
    ///  範例：scores = [1,3,5,10,15], ages = [1,2,3,4,5]
    ///  排序後：(age=1,score=1), (age=2,score=3), (age=3,score=5), (age=4,score=10), (age=5,score=15)
    ///  分數序列為遞增，所有球員皆可選擇
    ///  答案 = 1 + 3 + 5 + 10 + 15 = 34
    /// var result = BestTeamScore([1,3,5,10,15], [1,2,3,4,5]); // 回傳 34
    /// </code>
    /// </example>
    public int BestTeamScore(int[] scores, int[] ages)
    {
        int n = scores.Length;

        // 步驟 1：建立球員物件陣列，將分數與年齡配對
        Player[] players = new Player[n];
        for (int i = 0; i < n; i++)
        {
            players[i] = new Player()
            {
                score = scores[i],
                age = ages[i]
            };
        }

        // 步驟 2：依年齡升序排列，若年齡相同則依分數升序排列
        // 這樣排序後，只需確保選擇的球員分數是非遞減的即可避免衝突
        Array.Sort(players);

        // 步驟 3：動態規劃
        // dp[i] 表示以 players[i] 結尾的隊伍所能獲得的最大分數總和
        int[] dp = new int[n];
        dp[0] = players[0].score; // 初始化：第一位球員單獨成隊

        for (int i = 1; i < n; i++)
        {
            // 嘗試將 players[i] 加入以 players[j] 結尾的隊伍
            int maxPrevSum = 0;
            for (int j = 0; j < i; j++)
            {
                // 由於已按年齡排序，players[j].age <= players[i].age
                // 只需檢查分數：若 players[j].score <= players[i].score，則不會產生衝突
                // （年輕或同齡者的分數不高於當前者）
                if (players[i].score >= players[j].score)
                {
                    maxPrevSum = Math.Max(maxPrevSum, dp[j]);
                }
            }

            // 當前球員的分數 + 之前最佳隊伍的分數總和
            dp[i] = maxPrevSum + players[i].score;
        }

        // 步驟 4：回傳所有可能隊伍中的最大分數
        return dp.Max();
    }
}
