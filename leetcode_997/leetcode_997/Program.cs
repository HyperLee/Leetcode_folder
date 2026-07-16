namespace leetcode_997;

class Program
{
    /// <summary>
    /// 997. Find the Town Judge
    /// https://leetcode.com/problems/find-the-town-judge/description/
    ///
    /// In a town, there are n people labeled from 1 to n. There is a rumor that one of
    /// these people is secretly the town judge.
    ///
    /// If the town judge exists, then:
    /// 1. The town judge trusts nobody.
    /// 2. Everybody (except for the town judge) trusts the town judge.
    /// 3. There is exactly one person that satisfies properties 1 and 2.
    ///
    /// You are given an array trust where trust[i] = [ai, bi] representing that the
    /// person labeled ai trusts the person labeled bi. If a trust relationship does not
    /// exist in the trust array, then such a trust relationship does not exist.
    ///
    /// Return the label of the town judge if the town judge exists and can be identified,
    /// or return -1 otherwise.
    ///
    /// 997. 找到小鎮的法官
    /// https://leetcode.cn/problems/find-the-town-judge/description/
    ///
    /// 在一個小鎮裡，有 n 個人，編號從 1 到 n。傳聞其中一個人祕密地擔任小鎮法官。
    ///
    /// 如果小鎮法官存在，則：
    /// 1. 小鎮法官不信任任何人。
    /// 2. 除了小鎮法官本人以外，每個人都信任小鎮法官。
    /// 3. 恰好只有一個人同時符合條件 1 和條件 2。
    ///
    /// 給定一個陣列 trust，其中 trust[i] = [ai, bi] 表示編號為 ai 的人信任編號為 bi 的人。
    /// 如果某個信任關係不存在於 trust 陣列中，就表示該信任關係不存在。
    ///
    /// 如果小鎮法官存在且能夠被辨識，請回傳小鎮法官的編號；否則回傳 -1。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();

        Console.WriteLine("LeetCode 997 - Find the Town Judge");
        Console.WriteLine("=================================");
        Console.WriteLine();

        RunAllSampleCases(solver);
    }

    /// <summary>
    /// 使用入度與出度陣列尋找小鎮法官。
    /// 將信任關係視為有向邊，分別統計每個人的被信任次數與信任他人的次數；
    /// 法官的入度必須是 <paramref name="n"/> - 1，出度必須是 0。
    /// 輸入須符合題目條件：人物編號介於 1 到 <paramref name="n"/>、信任關係不重複，且沒有人信任自己。
    /// 時間複雜度為 O(n + m)，空間複雜度為 O(n)，其中 m 是信任關係數量。
    /// </summary>
    /// <param name="n">小鎮中的總人數，人物編號從 1 到 <paramref name="n"/>。</param>
    /// <param name="trust">信任關係陣列；每個元素 [a, b] 表示人物 a 信任人物 b。</param>
    /// <returns>若存在唯一的小鎮法官則回傳其編號，否則回傳 -1。</returns>
    public int FindJudge(int n, int[][] trust)
    {
        int[] inDegrees = new int[n + 1];
        int[] outDegrees = new int[n + 1];

        foreach (int[] edge in trust)
        {
            int trustingPerson = edge[0];
            int trustedPerson = edge[1];

            // a -> b 會增加 a 的出度，也會增加 b 的入度。
            outDegrees[trustingPerson]++;
            inDegrees[trustedPerson]++;
        }

        for (int i = 1; i <= n; i++)
        {
            // 法官被其餘 n - 1 人信任，而且不信任任何人。
            if (inDegrees[i] == n - 1 && outDegrees[i] == 0)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 使用單一信任分數陣列尋找小鎮法官。
    /// 對每筆 [a, b] 讓 a 減 1 分、b 加 1 分；法官不信任別人且被其餘所有人信任，
    /// 因此最終分數必須恰好是 <paramref name="n"/> - 1。
    /// 輸入須符合題目條件：人物編號介於 1 到 <paramref name="n"/>、信任關係不重複，且沒有人信任自己。
    /// 時間複雜度為 O(n + m)，空間複雜度為 O(n)，其中 m 是信任關係數量。
    /// </summary>
    /// <param name="n">小鎮中的總人數，人物編號從 1 到 <paramref name="n"/>。</param>
    /// <param name="trust">信任關係陣列；每個元素 [a, b] 表示人物 a 信任人物 b。</param>
    /// <returns>若存在唯一的小鎮法官則回傳其編號，否則回傳 -1。</returns>
    public int FindJudgeByTrustScore(int n, int[][] trust)
    {
        int[] trustScores = new int[n + 1];

        foreach (int[] edge in trust)
        {
            int trustingPerson = edge[0];
            int trustedPerson = edge[1];

            // 信任別人會失去法官資格的分數，被信任則累積候選分數。
            trustScores[trustingPerson]--;
            trustScores[trustedPerson]++;
        }

        for (int i = 1; i <= n; i++)
        {
            // 只有被其餘所有人信任、同時沒有信任別人的人能得到 n - 1 分。
            if (trustScores[i] == n - 1)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 執行五組固定示例，涵蓋單人小鎮、存在法官、信任不足，以及候選人也信任別人的情況。
    /// 此方法負責準備本地教學與驗證資料，沒有輸入，並將每組案例交給共用 runner 輸出結果。
    /// </summary>
    /// <param name="solver">提供兩種小鎮法官解法的程式實例。</param>
    private static void RunAllSampleCases(Program solver)
    {
        RunSampleCase(solver, 1, 1, Array.Empty<int[]>(), 1);
        RunSampleCase(solver, 2, 2, new int[][] { new int[] { 1, 2 } }, 2);
        RunSampleCase(solver, 3, 3, new int[][] { new int[] { 1, 3 }, new int[] { 2, 3 } }, 3);
        RunSampleCase(solver, 4, 3, new int[][] { new int[] { 1, 2 }, new int[] { 2, 3 } }, -1);
        RunSampleCase(
            solver,
            5,
            3,
            new int[][] { new int[] { 1, 3 }, new int[] { 2, 3 }, new int[] { 3, 1 } },
            -1);
    }

    /// <summary>
    /// 執行單筆示例並比較兩種解法與預期答案。
    /// 輸入包含案例編號、人數、合法信任關係與預期結果；方法沒有回傳值，
    /// 而是將格式化輸入、兩種實際結果及 PASS/FAIL 狀態輸出至主控台。
    /// </summary>
    /// <param name="solver">提供兩種小鎮法官解法的程式實例。</param>
    /// <param name="caseNumber">顯示於主控台的案例編號。</param>
    /// <param name="n">此案例的小鎮總人數。</param>
    /// <param name="trust">此案例的信任關係陣列。</param>
    /// <param name="expected">此案例預期回傳的法官編號，無法官時為 -1。</param>
    private static void RunSampleCase(Program solver, int caseNumber, int n, int[][] trust, int expected)
    {
        int degreeResult = solver.FindJudge(n, trust);
        int scoreResult = solver.FindJudgeByTrustScore(n, trust);
        string formattedTrust = trust.Length == 0
            ? "[]"
            : $"[{string.Join(", ", trust.Select(edge => $"[{edge[0]}, {edge[1]}]"))}]";

        Console.WriteLine($"Case {caseNumber}");
        Console.WriteLine($"n: {n}");
        Console.WriteLine($"trust: {formattedTrust}");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"FindJudge (In/Out Degree)     : {degreeResult} {(degreeResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine($"FindJudgeByTrustScore (Score): {scoreResult} {(scoreResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine();
    }
}