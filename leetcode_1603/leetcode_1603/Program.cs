namespace leetcode_1603;

internal static class Program
{
    /// <summary>
    /// <para>
    /// 1603. Design Parking System
    /// https://leetcode.com/problems/design-parking-system/description/
    ///
    /// Design a parking system for a parking lot with three kinds of spaces: big, medium, and small, each with a fixed number
    /// of slots. Implement the ParkingSystem class:
    /// - ParkingSystem(int big, int medium, int small) initializes the object with the slot counts for each size.
    /// - bool addCar(int carType) checks whether a matching space is available. Types 1, 2, and 3 represent big, medium, and
    ///   small. A car may use only its own type. If no space is available, return false; otherwise park it and return true.
    ///
    /// Example 1:
    /// Input:
    /// ["ParkingSystem","addCar","addCar","addCar","addCar"]
    /// [[1,1,0],[1],[2],[3],[1]]
    /// Output: [null,true,true,false,false]
    /// Explanation:
    /// ParkingSystem parkingSystem = new ParkingSystem(1, 1, 0);
    /// parkingSystem.addCar(1); // true: 1 big slot was available
    /// parkingSystem.addCar(2); // true: 1 medium slot was available
    /// parkingSystem.addCar(3); // false: no small slot was available
    /// parkingSystem.addCar(1); // false: the big slot is already occupied
    ///
    /// Constraints:
    /// - 0 &lt;= big, medium, small &lt;= 1000
    /// - carType is 1, 2, or 3
    /// - At most 1000 calls will be made to addCar
    /// </para>
    /// <para>
    /// 1603. 設計停車系統
    /// https://leetcode.cn/problems/design-parking-system/description/
    ///
    /// 為停車場設計一個停車系統。停車場有大型、中型、小型三種車位，每種都有固定數量。實作 ParkingSystem：
    /// - ParkingSystem(int big, int medium, int small) 以各尺寸的車位數初始化物件。
    /// - bool addCar(int carType) 檢查是否有相符車位。類型 1、2、3 分別表示大型、中型、小型車；車輛只能停
    ///   在自己的類型。若無空位回傳 false；否則停入並回傳 true。
    ///
    /// 範例 1：
    /// 輸入：
    /// ["ParkingSystem","addCar","addCar","addCar","addCar"]
    /// [[1,1,0],[1],[2],[3],[1]]
    /// 輸出：[null,true,true,false,false]
    /// 解釋：
    /// ParkingSystem parkingSystem = new ParkingSystem(1, 1, 0);
    /// parkingSystem.addCar(1); // true：有 1 個大型車位
    /// parkingSystem.addCar(2); // true：有 1 個中型車位
    /// parkingSystem.addCar(3); // false：沒有小型車位
    /// parkingSystem.addCar(1); // false：大型車位已被占用
    ///
    /// 限制條件：
    /// - 0 &lt;= big, medium, small &lt;= 1000
    /// - carType 為 1、2 或 3
    /// - addCar 最多呼叫 1000 次
    /// </para>
    /// </summary>
    private static void Main()
    {
        List<CaseResult> cases =
        [
            RunSequenceCase(
                "Official example",
                "capacities=(1,1,0), operations=[1,2,3,1]",
                new ParkingSystem(1, 1, 0),
                [1, 2, 3, 1],
                [true, true, false, false]),
            RunSequenceCase(
                "All zero",
                "capacities=(0,0,0), operations=[1,2,3]",
                new ParkingSystem(0, 0, 0),
                [1, 2, 3],
                [false, false, false]),
            RunSequenceCase(
                "Mixed independent counters",
                "capacities=(1,2,1), operations=[2,1,2,3,2,1,3]",
                new ParkingSystem(1, 2, 1),
                [2, 1, 2, 3, 2, 1, 3],
                [true, true, true, true, false, false, false]),
            RunSequenceCase(
                "Exhausted type does not affect others",
                "capacities=(1,1,1), operations=[1,1,2,3]",
                new ParkingSystem(1, 1, 1),
                [1, 1, 2, 3],
                [true, false, true, true]),
            RunSequenceCase(
                "Repeated zero-capacity rejection is stable",
                "capacities=(0,0,1), operations=[1,1,3,3,3]",
                new ParkingSystem(0, 0, 1),
                [1, 1, 3, 3, 3],
                [false, false, true, false, false]),
            RunInstanceIsolationCase(),
            RunSequenceCase(
                "Maximum capacities",
                "capacities=(1000,1000,1000), operations=[1,2,3]",
                new ParkingSystem(1000, 1000, 1000),
                [1, 2, 3],
                [true, true, true]),
            RunCallLimitCase()
        ];

        foreach (CaseResult caseResult in cases)
        {
            Console.WriteLine($"Case: {caseResult.Name}");
            Console.WriteLine($"Input: {caseResult.Input}");
            Console.WriteLine($"Expected: {caseResult.Expected}");
            Console.WriteLine($"Actual: {caseResult.Actual}");
            Console.WriteLine($"Result: {(caseResult.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int passedCount = cases.Count(caseResult => caseResult.Passed);
        Console.WriteLine($"Summary: {passedCount}/{cases.Count} checks passed.");

        if (passedCount != cases.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult RunSequenceCase(
        string name,
        string input,
        ParkingSystem parkingSystem,
        int[] operations,
        bool[] expected)
    {
        bool[] actual = operations.Select(parkingSystem.AddCar).ToArray();
        return new CaseResult(name, input, FormatBooleans(expected), FormatBooleans(actual), expected.SequenceEqual(actual));
    }

    private static CaseResult RunInstanceIsolationCase()
    {
        ParkingSystem first = new(1, 0, 0);
        ParkingSystem second = new(1, 0, 0);
        bool[] firstActual = [first.AddCar(1), first.AddCar(1)];
        bool secondActual = second.AddCar(1);
        bool[] expected = [true, false, true];
        bool[] actual = [firstActual[0], firstActual[1], secondActual];

        return new CaseResult(
            "Instance isolation",
            "two capacities=(1,0,0) instances; first operations=[1,1], second operations=[1]",
            "first=[true,false], second first add=true",
            $"first={FormatBooleans(firstActual)}, second first add={FormatBoolean(secondActual)}",
            expected.SequenceEqual(actual));
    }

    private static CaseResult RunCallLimitCase()
    {
        ParkingSystem parkingSystem = new(999, 0, 0);
        bool[] actual = Enumerable.Range(0, 1000).Select(_ => parkingSystem.AddCar(1)).ToArray();
        bool passed = actual.Take(999).All(result => result) && !actual[999];

        return new CaseResult(
            "Exact call-limit spot check",
            "capacities=(999,0,0), operation=1 repeated 1000 times",
            "first 999=true; call 1000=false",
            $"first 999={FormatBoolean(actual.Take(999).All(result => result))}; call 1000={FormatBoolean(actual[999])}",
            passed);
    }

    private static string FormatBooleans(IEnumerable<bool> values) => $"[{string.Join(',', values.Select(FormatBoolean))}]";

    private static string FormatBoolean(bool value) => value ? "true" : "false";

    private sealed record CaseResult(string Name, string Input, string Expected, string Actual, bool Passed);
}

public sealed class ParkingSystem
{
    private int bigSlots;
    private int mediumSlots;
    private int smallSlots;

    /// <summary>
    /// 以題目保證有效的三種車位容量建立停車系統；每個欄位獨立保存剩餘車位，供後續
    /// <see cref="AddCar(int)"/> 依車種扣減。輸入的 big、medium、small 皆為 0 至 1000，
    /// 建立後不產生主控台輸出，僅初始化可觀察的可停車狀態。
    /// </summary>
    public ParkingSystem(int big, int medium, int small)
    {
        bigSlots = big;
        mediumSlots = medium;
        smallSlots = small;
    }

    /// <summary>
    /// 嘗試將指定車種停入其對應的車位計數器；有效輸入 carType 為 1、2 或 3，分別代表
    /// 大型、中型與小型車。若對應剩餘車位大於零便扣減一次並回傳 true，否則保留狀態並
    /// 回傳 false。
    /// </summary>
    public bool AddCar(int carType)
    {
        // 三個計數器各自代表一種車位；拒絕時不扣減，因此永遠不會低於零。
        return carType switch
        {
            1 when bigSlots > 0 => Reserve(ref bigSlots),
            2 when mediumSlots > 0 => Reserve(ref mediumSlots),
            3 when smallSlots > 0 => Reserve(ref smallSlots),
            _ => false
        };
    }

    /// <summary>
    /// 對有效且為正數的剩餘車位計數器扣減一次，並回傳 <c>true</c>。
    /// </summary>
    private static bool Reserve(ref int slots)
    {
        slots--;
        return true;
    }
}
