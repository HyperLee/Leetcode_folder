namespace leetcode_380;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 380. Insert Delete GetRandom O(1)
    /// https://leetcode.com/problems/insert-delete-getrandom-o1/
    /// 380. O(1) 時間插入、刪除和取得隨機元素
    /// https://leetcode.cn/problems/insert-delete-getrandom-o1/
    /// Design a set that supports inserting, removing, and returning a random element in average O(1) time.
    /// 設計一個集合，在平均 O(1) 時間內完成插入、刪除，以及隨機回傳現有元素。
    /// </summary>
    private static void Main()
    {
        Console.WriteLine("LeetCode 380 acceptance harness");
        Console.WriteLine();

        RunOfficialSequence();
        RunDuplicateAndMissingCases();
        RunMiddleRemovalCase();
        RunRandomCases();
        RunExtremeValueCase();

        Console.WriteLine();
        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    private static void RunOfficialSequence()
    {
        Console.WriteLine("Case 1: official operation sequence");
        RandomizedSet set = new();

        Check("Insert(1)", true, set.Insert(1));
        Check("Remove(2)", false, set.Remove(2));
        Check("Insert(2)", true, set.Insert(2));
        Check("GetRandom() belongs to {1, 2}", true, IsRandomMember(set, [1, 2]));
        Check("Remove(1)", true, set.Remove(1));
        Check("Insert(2) duplicate", false, set.Insert(2));
        Check("GetRandom() after official sequence", 2, set.GetRandom());
        Console.WriteLine();
    }

    private static void RunDuplicateAndMissingCases()
    {
        Console.WriteLine("Case 2: duplicate insertion and missing removal");
        RandomizedSet set = new();

        Check("Insert(7)", true, set.Insert(7));
        Check("Insert(7) duplicate", false, set.Insert(7));
        Check("Remove(8) missing", false, set.Remove(8));
        Check("Remove(7)", true, set.Remove(7));
        Check("Remove(7) after deletion", false, set.Remove(7));
        Console.WriteLine();
    }

    private static void RunMiddleRemovalCase()
    {
        Console.WriteLine("Case 3: middle-element removal and index repair");
        RandomizedSet set = new();

        Check("Insert(10)", true, set.Insert(10));
        Check("Insert(20)", true, set.Insert(20));
        Check("Insert(30)", true, set.Insert(30));
        Check("Remove(20) from middle", true, set.Remove(20));
        Check("Random member after middle removal", true, IsRandomMember(set, [10, 30]));
        Check("Remove(30) after index repair", true, set.Remove(30));
        Check("GetRandom() after repaired removal", 10, set.GetRandom());
        Console.WriteLine();
    }

    private static void RunRandomCases()
    {
        Console.WriteLine("Case 4: random membership and exact singleton return");
        RandomizedSet multiple = new();
        multiple.Insert(-5);
        multiple.Insert(0);
        multiple.Insert(5);

        Check("64 random draws belong to {-5, 0, 5}", true, IsRandomMember(multiple, [-5, 0, 5], 64));

        RandomizedSet singleton = new();
        singleton.Insert(42);
        Check("Single-element GetRandom()", 42, singleton.GetRandom());
        Console.WriteLine();
    }

    private static void RunExtremeValueCase()
    {
        Console.WriteLine("Case 5: integer extremes");
        RandomizedSet set = new();

        Check("Insert(int.MinValue)", true, set.Insert(int.MinValue));
        Check("Insert(int.MaxValue)", true, set.Insert(int.MaxValue));
        Check("Random extreme membership", true, IsRandomMember(set, [int.MinValue, int.MaxValue]));
        Check("Remove(int.MinValue)", true, set.Remove(int.MinValue));
        Check("GetRandom() with int.MaxValue remaining", int.MaxValue, set.GetRandom());
    }

    private static bool IsRandomMember(RandomizedSet set, int[] expectedValues, int draws = 1)
    {
        HashSet<int> expected = [.. expectedValues];

        for (int i = 0; i < draws; i++)
        {
            if (!expected.Contains(set.GetRandom()))
            {
                return false;
            }
        }

        return true;
    }

    private static void Check<T>(string description, T expected, T actual)
    {
        s_checks++;
        bool passed = EqualityComparer<T>.Default.Equals(expected, actual);

        if (passed)
        {
            s_passed++;
        }

        Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | {description} | Expected: {expected} | Actual: {actual}");
    }
}

public class RandomizedSet
{
    private readonly List<int> _values;
    private readonly Dictionary<int, int> _indices;
    private readonly Random _random;

    public RandomizedSet()
    {
        _values = [];
        _indices = [];
        _random = new Random();
    }

    public bool Insert(int val)
    {
        if (_indices.ContainsKey(val))
        {
            return false;
        }

        _indices[val] = _values.Count;
        _values.Add(val);
        return true;
    }

    public bool Remove(int val)
    {
        if (!_indices.TryGetValue(val, out int index))
        {
            return false;
        }

        int lastIndex = _values.Count - 1;
        int lastValue = _values[lastIndex];
        _values[index] = lastValue;
        _indices[lastValue] = index;
        _values.RemoveAt(lastIndex);
        _indices.Remove(val);
        return true;
    }

    public int GetRandom()
    {
        return _values[_random.Next(_values.Count)];
    }
}
