namespace leetcode_1921;

internal static class Program
{
    /// <summary>
    /// LeetCode 1921. Eliminate Maximum Number of Monsters.
    /// LeetCode 1921. 消滅怪物的最大數量。
    /// English: Monsters move toward the city at constant speeds. Starting at minute zero, eliminate
    /// one monster per minute before any monster reaches the city; return the maximum eliminations.
    /// 中文：怪物以固定速度朝城市移動。從第零分鐘開始，每分鐘可消滅一隻怪物；在任何怪物
    /// 抵達城市前，求最多能消滅的怪物數量。
    /// English: https://leetcode.com/problems/eliminate-maximum-number-of-monsters/
    /// 中文：https://leetcode.cn/problems/eliminate-maximum-number-of-monsters/
    /// </summary>
    private static void Main()
    {
        int[] maximumDistanceWithUnitSpeed = Enumerable.Repeat(100000, 100000).ToArray();
        int[] unitSpeed = Enumerable.Repeat(1, 100000).ToArray();
        int[] maximumSpeed = Enumerable.Repeat(100000, 100000).ToArray();

        TestCase[] testCases =
        [
            new("Official example", "dist=[1, 3, 4], speed=[1, 1, 1]", [1, 3, 4], [1, 1, 1], 3),
            new("Official second example", "dist=[1, 1, 2, 3], speed=[1, 1, 1, 1]", [1, 1, 2, 3], [1, 1, 1, 1], 1),
            new("Official third example", "dist=[3, 2, 4], speed=[5, 3, 2]", [3, 2, 4], [5, 3, 2], 1),
            new("Minimum input", "dist=[1], speed=[100000]", [1], [100000], 1),
            new("Ceiling regression", "dist=[1, 3], speed=[1, 2]", [1, 3], [1, 2], 2),
            new("Arrival order", "dist=[3, 4], speed=[1, 4]", [3, 4], [1, 4], 2),
            new("General partial loss", "dist=[1, 2, 2, 10], speed=[1, 1, 1, 1]", [1, 2, 2, 10], [1, 1, 1, 1], 2),
            new("Maximum count, unit speed", "dist=[100000 x 100000], speed=[1 x 100000]", maximumDistanceWithUnitSpeed, unitSpeed, 100000),
            new("Maximum count, maximum speed", "dist=[100000 x 100000], speed=[100000 x 100000]", maximumDistanceWithUnitSpeed, maximumSpeed, 1)
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck("EliminateMaximum result", result.Expected, result.Actual));
            Console.WriteLine(PrintCheck("Input preserved", true, result.InputPreserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 18;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string PrintCheck<T>(string checkName, T expected, T actual)
    {
        string status = EqualityComparer<T>.Default.Equals(expected, actual) ? "PASS" : "FAIL";
        return $"{status} {checkName} | Expected: {expected} | Actual: {actual}";
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int[] dist = [.. testCase.Dist];
        int[] speed = [.. testCase.Speed];
        int[] originalDist = [.. dist];
        int[] originalSpeed = [.. speed];
        int actual = EliminateMaximum(dist, speed);
        bool inputPreserved = dist.SequenceEqual(originalDist) && speed.SequenceEqual(originalSpeed);

        return new CaseResult(testCase.Name, testCase.Input, testCase.Expected, actual, inputPreserved);
    }

    /// <summary>
    /// 以整數向上取整計算每隻怪物抵達城市所需的分鐘數，再由早到晚安排每分鐘的一次攻擊。
    /// 對題目保證的有效輸入（dist 與 speed 長度相同且介於 1 至 100000，每個值介於 1 至
    /// 100000），若第 i 個最早抵達的怪物在第 i 分鐘或更早抵達，便無法再消滅它；回傳此前
    /// 已能消滅的數量。方法只建立並排序新的抵達時間陣列，不修改 dist、speed 或主控台狀態。
    /// </summary>
    /// <param name="dist">各怪物到城市的初始距離。</param>
    /// <param name="speed">各怪物每分鐘朝城市移動的距離。</param>
    /// <returns>城市首次被怪物抵達前，最多可消滅的怪物數量。</returns>
    public static int EliminateMaximum(int[] dist, int[] speed)
    {
        int[] arrivalTimes = new int[dist.Length];
        for (int index = 0; index < dist.Length; index++)
        {
            // 第 index 隻怪物的抵達分鐘數必須向上取整，才能與整數分鐘的攻擊時刻正確比較。
            arrivalTimes[index] = (dist[index] - 1) / speed[index] + 1;
        }

        Array.Sort(arrivalTimes);

        for (int index = 0; index < arrivalTimes.Length; index++)
        {
            // 在第 index 分鐘前必須先處理第 index 個最早抵達者；否則城市已先失守。
            if (arrivalTimes[index] <= index)
            {
                return index;
            }
        }

        return arrivalTimes.Length;
    }

    private sealed record TestCase(string Name, string Input, int[] Dist, int[] Speed, int Expected);

    private sealed record CaseResult(string Name, string Input, int Expected, int Actual, bool InputPreserved)
    {
        public int PassedCheckCount =>
            (Actual == Expected ? 1 : 0) +
            (InputPreserved ? 1 : 0);
    }
}