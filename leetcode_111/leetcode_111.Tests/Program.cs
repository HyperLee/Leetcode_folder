using System.Reflection;

namespace leetcode_111.Tests;

internal static class Program
{
    private static readonly Assembly SolutionAssembly = Assembly.Load("leetcode_111");
    private static readonly Type SolutionType = SolutionAssembly.GetType("leetcode_111.Program", throwOnError: true)!;
    private static readonly Type TreeNodeType = SolutionAssembly.GetType("leetcode_111.Program+TreeNode", throwOnError: true)!;

    private static int Main()
    {
        var tests = new (string Name, Action Test)[]
        {
            ("MinDepth_ReturnsZero_WhenRootIsNull", MinDepth_ReturnsZero_WhenRootIsNull),
            ("Solutions_ReturnExpectedDepth_ForBalancedSample", Solutions_ReturnExpectedDepth_ForBalancedSample),
            ("Solutions_ReturnExpectedDepth_ForRightSkewedTree", Solutions_ReturnExpectedDepth_ForRightSkewedTree),
            ("MinDepth2_DoesNotReusePreviousBestDepth_BetweenCalls", MinDepth2_DoesNotReusePreviousBestDepth_BetweenCalls)
        };

        var failed = 0;

        foreach (var (name, test) in tests)
        {
            try
            {
                test();
                Console.WriteLine($"PASS {name}");
            }
            catch (Exception ex)
            {
                failed++;
                Console.WriteLine($"FAIL {name}");
                Console.WriteLine($"  {ex.Message}");
            }
        }

        return failed == 0 ? 0 : 1;
    }

    private static void MinDepth_ReturnsZero_WhenRootIsNull()
    {
        AssertBothSolutions(null, 0);
    }

    private static void Solutions_ReturnExpectedDepth_ForBalancedSample()
    {
        var root = Node(3, Node(9), Node(20, Node(15), Node(7)));

        AssertBothSolutions(root, 2);
    }

    private static void Solutions_ReturnExpectedDepth_ForRightSkewedTree()
    {
        var root = Node(2, right: Node(3, right: Node(4, right: Node(5, right: Node(6)))));

        AssertBothSolutions(root, 5);
    }

    private static void MinDepth2_DoesNotReusePreviousBestDepth_BetweenCalls()
    {
        var solution = Activator.CreateInstance(SolutionType, nonPublic: true)!;
        var balanced = Node(3, Node(9), Node(20, Node(15), Node(7)));
        var skewed = Node(2, right: Node(3, right: Node(4, right: Node(5, right: Node(6)))));

        AssertMethod(solution, "MinDepth2", balanced, 2);
        AssertMethod(solution, "MinDepth2", skewed, 5);
    }

    private static void AssertBothSolutions(object? root, int expected)
    {
        AssertMethod(Activator.CreateInstance(SolutionType, nonPublic: true)!, "MinDepth", root, expected);
        AssertMethod(Activator.CreateInstance(SolutionType, nonPublic: true)!, "MinDepth2", root, expected);
    }

    private static void AssertMethod(object solution, string methodName, object? root, int expected)
    {
        var method = SolutionType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public)!;
        var actual = (int)method.Invoke(solution, new[] { root })!;

        if (actual != expected)
        {
            throw new InvalidOperationException($"{methodName} expected {expected}, actual {actual}.");
        }
    }

    private static object Node(int value, object? left = null, object? right = null)
    {
        return Activator.CreateInstance(TreeNodeType, new[] { (object)value, left, right })!;
    }
}
