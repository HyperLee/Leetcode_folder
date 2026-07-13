namespace leetcode_735;

internal static class Program
{
    /// <summary>
    /// 735. Asteroid Collision
    /// https://leetcode.com/problems/asteroid-collision/
    /// 735. 小行星碰撞
    /// https://leetcode.cn/problems/asteroid-collision/
    /// English: Given signed asteroid sizes moving at the same speed, return the
    /// asteroids that survive every head-on collision in their original order.
    /// 中文：給定以正負號表示移動方向的小行星，計算所有迎面碰撞結束後，仍以原始
    /// 左至右順序存活的小行星。
    /// </summary>
    /// <param name="args">命令列參數；本驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, int[] Input, int[] Expected)[] testCases =
        {
            ("Case 1: Official example", new[] { 5, 10, -5 }, new[] { 5, 10 }),
            ("Case 2: Official equal-size collision", new[] { 8, -8 }, Array.Empty<int>()),
            ("Case 3: Official chained collision", new[] { 10, 2, -5 }, new[] { 10 }),
            ("Case 4: Official incoming survivor", new[] { 1, -2, -2, -2 }, new[] { -2, -2, -2 }),
            ("Case 5: Minimum valid no-collision input", new[] { 1, 2 }, new[] { 1, 2 }),
            ("Case 6: Same leftward direction", new[] { -2, -1 }, new[] { -2, -1 }),
            ("Case 7: Incoming asteroid defeats several survivors", new[] { 1, 2, 3, -4 }, new[] { -4 }),
            ("Case 8: Equal collision preserves earlier survivor", new[] { 3, 5, -5 }, new[] { 3 })
        };

        List<(string CaseName, string Input, string CheckName, string Expected, string Actual, bool Passed)> checks =
            new List<(string CaseName, string Input, string CheckName, string Expected, string Actual, bool Passed)>();

        foreach ((string caseName, int[] input, int[] expected) in testCases)
        {
            int[] actual = AsteroidCollision(input);
            string expectedText = $"[{string.Join(", ", expected)}]";
            string actualText = $"[{string.Join(", ", actual)}]";

            checks.Add((
                caseName,
                $"[{string.Join(", ", input)}]",
                "Survivors",
                expectedText,
                actualText,
                expected.SequenceEqual(actual)));
        }

        int[] upperBoundInput = Enumerable.Repeat(1_000, 10_000).ToArray();
        bool upperBoundInputIsValid = upperBoundInput.All(asteroid => Math.Abs(asteroid) <= 1_000 && asteroid != 0);
        int[] upperBoundActual = AsteroidCollision(upperBoundInput);

        checks.Add((
            "Case 9: Upper-bound spot checks",
            "10,000 right-moving asteroids (all 1000)",
            "Valid input and survivor count",
            "10000 valid asteroids",
            $"{upperBoundActual.Length} survivors; valid input: {upperBoundInputIsValid}",
            upperBoundInputIsValid && upperBoundActual.Length == 10_000));
        checks.Add((
            "Case 9: Upper-bound spot checks",
            "10,000 right-moving asteroids (all 1000)",
            "First survivor",
            "1000",
            upperBoundActual[0].ToString(),
            upperBoundActual[0] == 1_000));
        checks.Add((
            "Case 9: Upper-bound spot checks",
            "10,000 right-moving asteroids (all 1000)",
            "Last survivor",
            "1000",
            upperBoundActual[^1].ToString(),
            upperBoundActual[^1] == 1_000));

        int passedCount = 0;
        string? previousCaseName = null;

        Console.WriteLine("LeetCode 735 acceptance harness");

        foreach ((string caseName, string input, string checkName, string expected, string actual, bool passed) in checks)
        {
            if (!string.Equals(caseName, previousCaseName, StringComparison.Ordinal))
            {
                Console.WriteLine();
                Console.WriteLine(caseName);
                Console.WriteLine($"Input: {input}");
                previousCaseName = caseName;
            }

            string result = passed ? "PASS" : "FAIL";
            Console.WriteLine($"{result} | {checkName} | Expected: {expected} | Actual: {actual}");

            if (passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{checks.Count} checks passed.");

        if (passedCount != checks.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用堆疊依序模擬小行星碰撞。有效輸入必須符合題目限制：每個元素的正負號代表
    /// 方向且不為零；只有先前向右與目前向左的小行星可能碰撞。回傳所有存活小行星，
    /// 並維持其原始由左至右順序，不會修改輸入陣列或寫入主控台。
    /// </summary>
    /// <param name="asteroids">依原始位置排序的小行星大小與方向。</param>
    /// <returns>所有碰撞結束後仍存在的小行星。</returns>
    public static int[] AsteroidCollision(int[] asteroids)
    {
        Stack<int> survivors = new Stack<int>();

        foreach (int asteroid in asteroids)
        {
            bool isAlive = true;

            // 只有堆疊頂端向右、目前小行星向左時才會迎面碰撞。
            while (isAlive && asteroid < 0 && survivors.Count > 0 && survivors.Peek() > 0)
            {
                int previousAsteroid = survivors.Peek();
                int incomingSize = -asteroid;

                if (previousAsteroid < incomingSize)
                {
                    // 前一顆被摧毀後，存活的 incoming asteroid 必須繼續比較下一顆。
                    survivors.Pop();
                    continue;
                }

                if (previousAsteroid == incomingSize)
                {
                    survivors.Pop();
                }

                isAlive = false;
            }

            if (isAlive)
            {
                survivors.Push(asteroid);
            }
        }

        int[] result = new int[survivors.Count];

        for (int index = result.Length - 1; index >= 0; index--)
        {
            result[index] = survivors.Pop();
        }

        return result;
    }
}
