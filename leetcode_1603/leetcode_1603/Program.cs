namespace leetcode_1603;

internal static class Program
{
    /// <summary>
    /// LeetCode 1603: Design Parking System / 設計停車系統。
    /// https://leetcode.com/problems/design-parking-system/
    /// https://leetcode.cn/problems/design-parking-system/
    /// Design a parking system with independent big, medium, and small capacities; each
    /// arriving car is accepted only when its matching space remains. 設計一個分別管理大型、
    /// 中型與小型車位容量的停車系統；車輛只有在對應車位尚有餘額時才能停入。
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
