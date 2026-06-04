namespace leetcode_134;

class Program
{
    /// <summary>
    /// 134. Gas Station
    /// https://leetcode.com/problems/gas-station/description/
    ///
    /// There are n gas stations along a circular route, where the amount of gas at the ith station is gas[i].
    /// You have a car with an unlimited gas tank and it costs cost[i] of gas to travel from the ith station
    /// to its next (i + 1)th station. You begin the journey with an empty tank at one of the gas stations.
    /// Given two integer arrays gas and cost, return the starting gas station's index if you can travel around
    /// the circuit once in the clockwise direction, otherwise return -1. If there exists a solution,
    /// it is guaranteed to be unique.
    ///
    /// 134. 加油站
    /// https://leetcode.cn/problems/gas-station/description/
    ///
    /// 沿著一條環形路線有 n 個加油站，第 i 個加油站的汽油量為 gas[i]。
    /// 你的車有一個容量無限的油箱，從第 i 個加油站行駛到下一個第 (i + 1) 個加油站
    /// 需要消耗 cost[i] 的汽油。你會從其中一個加油站出發，且出發時油箱是空的。
    /// 給定兩個整數陣列 gas 和 cost，如果你可以依順時針方向繞行環路一圈，請回傳起始加油站的索引；
    /// 否則回傳 -1。如果存在解，保證該解唯一。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new();
        (string Name, int[] Gas, int[] Cost, int Expected, string Notes)[] testCases =
        [
            (
                "Example 1",
                [1, 2, 3, 4, 5],
                [3, 4, 5, 1, 2],
                3,
                "Start at index 3, then the tank never drops below zero."
            ),
            (
                "Example 2",
                [2, 3, 4],
                [3, 4, 3],
                -1,
                "The total gas is less than the total cost, so no full circuit exists."
            ),
            (
                "Single station success",
                [5],
                [4],
                0,
                "The only station gives enough gas to return to itself."
            ),
            (
                "Single station failure",
                [1],
                [2],
                -1,
                "The only station cannot pay for its outgoing route."
            ),
            (
                "Reset start after failed prefix",
                [5, 1, 2, 3, 4],
                [4, 4, 1, 5, 1],
                4,
                "Prefixes that make the tank negative cannot contain a valid start."
            )
        ];

        Console.WriteLine("LeetCode 134 - Gas Station");
        Console.WriteLine("==========================");

        foreach ((string name, int[] gas, int[] cost, int expected, string notes) in testCases)
        {
            int actual = solution.CanCompleteCircuit(gas, cost);

            Console.WriteLine($"[{name}]");
            Console.WriteLine($"Gas: {FormatArray(gas)}");
            Console.WriteLine($"Cost: {FormatArray(cost)}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Result: {actual}");
            Console.WriteLine($"Pass: {(actual == expected ? "PASS" : "FAIL")}");
            Console.WriteLine($"Notes: {notes}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Finds the only valid starting gas station by using a greedy single pass.
    /// The method assumes <paramref name="gas"/> and <paramref name="cost"/> have the same non-zero length,
    /// as defined by the LeetCode problem constraints. It tracks both the total route balance and the current
    /// candidate route balance. When the current tank becomes negative, every station in that failed prefix is
    /// impossible as a start, so the next station becomes the new candidate. Returns the starting index when the
    /// total gas can cover the total cost; otherwise returns <c>-1</c>.
    /// </summary>
    /// <param name="gas">Amount of gas available at each station.</param>
    /// <param name="cost">Gas required to travel from each station to the next station.</param>
    /// <returns>The valid starting station index, or <c>-1</c> when completing the circuit is impossible.</returns>
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int start = 0;
        int totalBalance = 0;
        int tankBalance = 0;
        int n = gas.Length;

        for (int i = 0; i < n; i++)
        {
            int stationBalance = gas[i] - cost[i];
            totalBalance += stationBalance;
            tankBalance += stationBalance;

            if (tankBalance < 0)
            {
                // 若從目前候選起點到 i 已經欠油，這段區間內任何起點都無法跨過 i。
                start = i + 1;
                tankBalance = 0;
            }
        }

        // 總油量不足時，不論從哪個站出發都不可能完成環路。
        return totalBalance >= 0 ? start : -1;
    }

    /// <summary>
    /// Formats an integer array for readable console sample output.
    /// The method accepts any integer array and returns a bracketed comma-separated string,
    /// making the executable examples match the array notation used in the README.
    /// </summary>
    /// <param name="values">The integer array to format.</param>
    /// <returns>A display string such as <c>[1, 2, 3]</c>.</returns>
    private static string FormatArray(int[] values)
    {
        return $"[{string.Join(", ", values)}]";
    }
}
