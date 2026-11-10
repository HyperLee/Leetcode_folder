namespace leetcode_3693;

internal static class SolutionChecks
{
    internal static void Run()
    {
        // 固定預期值可抓出漏算落點費用、跳躍平方或邊界索引等錯誤。
        (string Name, int[] Costs, int Expected)[] cases =
        [
            ("範例一", [1, 2, 3, 4], 13),
            ("範例二", [5, 1, 6, 2], 11),
            ("範例三／三階跳", [9, 8, 3], 12),
            ("單階／最小費用", [1], 2),
            ("單階／最大費用", [10000], 10001),
            ("兩次一階跳", [1, 1], 4),
            ("直接兩階跳", [10000, 1], 5),
            ("三次一階跳", [1, 1, 1], 6),
            ("重複呼叫使用新快取", [1, 1, 10000], 10005),
            ("最大階數／最小費用", Enumerable.Repeat(1, 100000).ToArray(), 200000),
            // 33332 次三階跳加兩次兩階跳：33332 * 10009 + 2 * 10004。
            ("最大階數／最大費用", Enumerable.Repeat(10000, 100000).ToArray(), 333639996)
        ];

        Program solution = new Program();
        int checks = 0;
        foreach ((string name, int[] costs, int expected) in cases)
        {
            int first = solution.ClimbStairs(costs.Length, costs);
            int second = solution.ClimbStairs2(costs.Length, costs);
            int third = solution.ClimbStairs3(costs.Length, costs);
            if (first != expected || second != expected || third != expected)
            {
                throw new InvalidOperationException(
                    $"{name}：預期 {expected}，解法一 {first}，解法二 {second}，解法三 {third}。");
            }

            checks += 3;
            Console.WriteLine($"PASS {name}：{expected}");
        }

        Console.WriteLine($"全部通過：{cases.Length} 個案例，{checks} 項檢查。");
    }
}