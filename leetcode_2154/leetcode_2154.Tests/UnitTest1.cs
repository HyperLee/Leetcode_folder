using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace leetcode_2154.Tests;

public class KeepMultiplyingTests
{
    private readonly Program solver = new();

    public static IEnumerable<object[]> ExtremeTestCases()
    {
        yield return new object[]
        {
            Enumerable.Range(0, 12).Select(i => 1 << i).ToArray(),
            1,
            1 << 12
        };

        yield return new object[]
        {
            Enumerable.Range(1, 2000).Select(i => (i * 2) + 1).ToArray(),
            2048,
            2048
        };

        yield return new object[]
        {
            new[] { 999, 3, 12, 6, 3, 48, 24 },
            3,
            96
        };
    }

    [Theory]
    [MemberData(nameof(ExtremeTestCases))]
    public void HashSetSolution_ComputesExpectedValue(int[] nums, int original, int expected)
    {
        int result = solver.FindFinalValue(nums, original);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(ExtremeTestCases))]
    public void SortedSolution_ComputesExpectedValue(int[] nums, int original, int expected)
    {
        int[] numsCopy = (int[])nums.Clone();
        int result = solver.FindFinalValue_Array(numsCopy, original);

        Assert.Equal(expected, result);
    }
}
