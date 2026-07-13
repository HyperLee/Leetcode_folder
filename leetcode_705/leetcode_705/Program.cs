using System;
using System.Collections.Generic;
using System.Linq;

namespace leetcode_705;

internal static class Program
{
    /// <summary>
    /// 705. Design HashSet
    /// 705. 設計雜湊集合
    /// https://leetcode.com/problems/design-hashset/
    /// https://leetcode.cn/problems/design-hashset/
    /// Design a HashSet without built-in hash table libraries and support insertion, deletion, and membership queries.
    /// 不使用內建雜湊表函式庫設計雜湊集合，並支援新增、刪除與查詢成員是否存在。
    /// </summary>
    private static void Main()
    {
        HashSetCase[] cases =
        [
            new("Official example", "Add(1), Add(2), Contains(1), Contains(3), Add(2), Contains(2), Remove(2), Contains(2)", "[true, false, true, false]", RunOfficialExample),
            new("Minimum key boundary", "Add(0), Contains(0), Remove(0), Contains(0)", "[true, false]", RunMinimumKeyBoundary),
            new("Maximum key boundary", "Add(1000000), Contains(1000000), Remove(1000000), Contains(1000000)", "[true, false]", RunMaximumKeyBoundary),
            new("Same-bucket collisions", "Add(1), Add(770), Add(1539), Remove(770)", "[true, false, true]", RunCollisionCase),
            new("Duplicate add is idempotent", "Add(42), Add(42), Remove(42), Contains(42)", "[false]", RunDuplicateAddCase),
            new("Missing removal preserves neighbors", "Add(1), Add(770), Remove(1539), Contains(1), Contains(770)", "[true, true]", RunMissingRemovalCase),
            new("Reinsert after removal", "Add(5), Remove(5), Add(5), Contains(5)", "[true]", RunReinsertCase),
            new("10000-operation spot check", "3333 adds, 3333 contains checks, 3332 removals, 2 final contains checks", "true", RunTenThousandOperationSpotCheck)
        ];

        CaseResult[] results = cases.Select(RunCase).ToArray();
        foreach (CaseResult result in results)
        {
            Console.WriteLine($"Case: {result.Name}");
            Console.WriteLine($"Input: {result.InputDescription}");
            Console.WriteLine($"Expected: {result.Expected}");
            Console.WriteLine($"Actual: {result.Actual}");
            Console.WriteLine($"Result: {(result.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int passedCount = results.Count(result => result.Passed);
        Console.WriteLine($"Summary: {passedCount}/{results.Length} checks passed.");
        if (passedCount != results.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult RunCase(HashSetCase testCase)
    {
        MyHashSet hashSet = new();
        string actual = testCase.Execute(hashSet);
        bool passed = actual == testCase.Expected;
        return new CaseResult(testCase.Name, testCase.InputDescription, testCase.Expected, actual, passed);
    }

    private static string RunOfficialExample(MyHashSet hashSet)
    {
        hashSet.Add(1);
        hashSet.Add(2);
        bool containsOne = hashSet.Contains(1);
        bool containsThree = hashSet.Contains(3);
        hashSet.Add(2);
        bool containsTwo = hashSet.Contains(2);
        hashSet.Remove(2);
        bool containsRemovedTwo = hashSet.Contains(2);
        return FormatBooleanResults(containsOne, containsThree, containsTwo, containsRemovedTwo);
    }

    private static string RunMinimumKeyBoundary(MyHashSet hashSet)
    {
        hashSet.Add(0);
        bool containsZero = hashSet.Contains(0);
        hashSet.Remove(0);
        return FormatBooleanResults(containsZero, hashSet.Contains(0));
    }

    private static string RunMaximumKeyBoundary(MyHashSet hashSet)
    {
        hashSet.Add(1_000_000);
        bool containsMaximum = hashSet.Contains(1_000_000);
        hashSet.Remove(1_000_000);
        return FormatBooleanResults(containsMaximum, hashSet.Contains(1_000_000));
    }

    private static string RunCollisionCase(MyHashSet hashSet)
    {
        hashSet.Add(1);
        hashSet.Add(770);
        hashSet.Add(1539);
        hashSet.Remove(770);
        return FormatBooleanResults(hashSet.Contains(1), hashSet.Contains(770), hashSet.Contains(1539));
    }

    private static string RunDuplicateAddCase(MyHashSet hashSet)
    {
        hashSet.Add(42);
        hashSet.Add(42);
        hashSet.Remove(42);
        return FormatBooleanResults(hashSet.Contains(42));
    }

    private static string RunMissingRemovalCase(MyHashSet hashSet)
    {
        hashSet.Add(1);
        hashSet.Add(770);
        hashSet.Remove(1539);
        return FormatBooleanResults(hashSet.Contains(1), hashSet.Contains(770));
    }

    private static string RunReinsertCase(MyHashSet hashSet)
    {
        hashSet.Add(5);
        hashSet.Remove(5);
        hashSet.Add(5);
        return FormatBooleanResults(hashSet.Contains(5));
    }

    private static string RunTenThousandOperationSpotCheck(MyHashSet hashSet)
    {
        for (int key = 0; key < 3333; key++)
        {
            hashSet.Add(key);
        }

        for (int key = 0; key < 3333; key++)
        {
            if (!hashSet.Contains(key))
            {
                return "false";
            }
        }

        for (int key = 0; key < 3332; key++)
        {
            hashSet.Remove(key);
        }

        return (!hashSet.Contains(0) && hashSet.Contains(3332)).ToString().ToLowerInvariant();
    }

    private static string FormatBooleanResults(params bool[] values) => $"[{string.Join(", ", values.Select(value => value.ToString().ToLowerInvariant()))}]";

    private sealed record HashSetCase(string Name, string InputDescription, string Expected, Func<MyHashSet, string> Execute);

    private sealed record CaseResult(string Name, string InputDescription, string Expected, string Actual, bool Passed);
}

/// <summary>
/// 以分離鏈結法實作整數雜湊集合；對題目保證的 <c>0 &lt;= key &lt;= 10^6</c>，提供新增、刪除與存在性查詢。
/// </summary>
public sealed class MyHashSet
{
    private const int BucketCount = 769;
    private readonly LinkedList<int>[] _buckets;

    /// <summary>
    /// 建立空的雜湊集合，並為每個質數桶配置獨立鏈結串列，以保存雜湊值相同但 key 不同的項目。
    /// </summary>
    public MyHashSet()
    {
        _buckets = new LinkedList<int>[BucketCount];
        for (int index = 0; index < BucketCount; index++)
        {
            _buckets[index] = new LinkedList<int>();
        }
    }

    /// <summary>
    /// 將有效範圍內的 key 加入集合；以 key 的餘數定位桶，只有桶內尚未存在時才新增，因此重複呼叫不會改變集合內容。
    /// </summary>
    public void Add(int key)
    {
        LinkedList<int> bucket = GetBucket(key);
        // 同一桶可能有多個 key，先查找可維持集合沒有重複成員的不變量。
        if (!bucket.Contains(key))
        {
            bucket.AddLast(key);
        }
    }

    /// <summary>
    /// 從集合移除有效範圍內的 key；若目標不在對應桶中，移除動作不改變任何既有成員。
    /// </summary>
    public void Remove(int key)
    {
        GetBucket(key).Remove(key);
    }

    /// <summary>
    /// 查詢有效範圍內的 key 是否存在於對應桶的鏈結串列中，存在時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。
    /// </summary>
    public bool Contains(int key)
    {
        return GetBucket(key).Contains(key);
    }

    /// <summary>
    /// 依有效 key 的餘數取得所屬桶；相同餘數的 key 必須在同一條鏈結串列內逐一比對，以正確處理碰撞。
    /// </summary>
    private LinkedList<int> GetBucket(int key)
    {
        return _buckets[key % BucketCount];
    }
}
