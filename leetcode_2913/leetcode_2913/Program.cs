namespace leetcode_2913;

class Program
{
    /// <summary>
    /// 2913. Subarrays Distinct Element Sum of Squares I
    /// https://leetcode.com/problems/subarrays-distinct-element-sum-of-squares-i/description/
    /// 2913. 子数组不同元素数目的平方和 I
    /// https://leetcode.cn/problems/subarrays-distinct-element-sum-of-squares-i/description/
    /// <para>Given a 0-indexed integer array <c>nums</c>.</para>
    /// <para>The distinct count of a subarray of <c>nums</c> is defined as:</para>
    /// <para>Let <c>nums[i..j]</c> be a subarray of <c>nums</c> consisting of all the indices from <c>i</c> to <c>j</c> such that <c>0 &lt;= i &lt;= j &lt; nums.length</c>. Then the number of distinct values in <c>nums[i..j]</c> is called the distinct count of <c>nums[i..j]</c>.</para>
    /// <para>Return the sum of the squares of distinct counts of all subarrays of <c>nums</c>.</para>
    /// <para>A subarray is a contiguous non-empty sequence of elements within an array.</para>
    /// <para>繁體中文版本：</para>
    /// <para>給定一個以 0 為起始索引的整數陣列 <c>nums</c>。</para>
    /// <para><c>nums</c> 的子陣列之不同值數量定義如下：令 <c>nums[i..j]</c> 為 <c>nums</c> 的一個子陣列，由索引 <c>i</c> 到 <c>j</c> 的所有元素組成，且 <c>0 &lt;= i &lt;= j &lt; nums.length</c>。<c>nums[i..j]</c> 中不同值的數量，稱為 <c>nums[i..j]</c> 的不同值數量。</para>
    /// <para>請回傳 <c>nums</c> 所有子陣列的不同值數量平方總和。</para>
    /// <para>子陣列是陣列中連續且非空的元素序列。</para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：模拟
    /// 题目要求计算所有连续子数组中不同元素的数目平方和。
    /// 由于数组长度 n 最大只有 100，我们可以直接使用两层循环来枚举所有的连续子数组。
    /// 我们用外层循环变量 i 枚举子数组的起点。
    /// 在每一次确定起点后，初始化一个空的哈希集合 s 用于存储当前子数组内出现过的不同元素。
    /// 然后用内层循环变量 j 从 i 向后枚举子数组的终点。
    /// 每次将新元素 nums[j] 加入集合 s 中，此时集合的大小即为当前子
    /// 数组中不同元素的数目。我们将其平方并累加到结果中。遍历结束后，返回结果即可。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int SumCounts(IList<int> nums)
    {
        int n = nums.Count;
        int res = 0;
        for(int i = 0; i < n; i++)
        {
            HashSet<int> s = new HashSet<int>();
            for(int j = i; j < n; j++)
            {
                s.Add(nums[j]);
                res += s.Count * s.Count;
            }
        }
        return res;
    }

    /// <summary>
    /// 方法二：树状数组
    /// 我们可以借助 支持区间修改和区间查询的树状数组 (Binary Indexed Tree, BIT) 来优化整个流程：
    /// 1.我们用一个哈希表 seen 来记录每个元素 a 上一次出现的下标。
    /// 2. 当遍历到当前元素 a 时，上一次出现的下标之后的区间（即从 j=seen[a]+1 开始，到当前位置 i）内对应的所有的子数组的不同元素个数都会加 1。
    /// 3. 这些子数组平方和的总增加量就是：2×∑x+(i−j+1)。其中 ∑x 也就是区间内原有的不同元素总数和，可以用树状数组的区间查询功能获取，即 bit.query(j,i)。
    /// 4. 获取到 ∑x 后，当前位置 i 所有子数组的平方和增量被加到 cur 中，并立即对 109+7 取模防止溢出。然后将更新后的 cur 累加到最终结果 res 中同样取模。
    /// 最后再用树状数组在对应的区间 [j,i] 加上 1（即 bit.add(j,i,1)）。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int SumCounts2(IList<int> nums)
    {
        int n = nums.Count;
        long res = 0, cur = 0;
        BIT bit = new BIT(n + 10);
        Dictionary<int, int> seen = new Dictionary<int, int>();
        long mod = 1000000007;
        for(int i = 0; i < n; i++)
        {
            int a = nums[i];
            int j = (seen.ContainsKey(a) ? seen[a] : -1) + 1;
            long q = bit.query(j, i);
            cur = (cur + q * 2 + i - j + 1) % mod;
            res = (res + cur) % mod;
            bit.add(j, i, 1);
            seen[a] = i;
        }
        return (int)res;
    }
}

public class BIT
{
    public int size;
    public long[] _tree1;
    public long[] _tree2;

    public BIT(int n)
    {
        this.size = n;
        this._tree1 = new long[n + 2];
        this._tree2 = new long[n + 2];
    }

    private int _lowbit(int index)
    {
        return index & -index;
    }

    public void add(int left, int right, int delta)
    {
        _add(left, delta);
        _add(right + 1, -delta);
    }

    public long query(int left, int right)
    {
        return _query(right) - _query(left - 1);
    }

    private void _add(int index, int delta)
    {
        index += 1;
        long rawIndex = index;
        while(index <= size)
        {
            _tree1[index] += delta;
            _tree2[index] += rawIndex * delta;
            index += _lowbit(index);
        }
    }

    private long _query(int index)
    {
        index += 1;
        long rawIndex = index + 1;
        if(index > size)
        {
            index = size;
        }
        long res = 0;
        while(index > 0)
        {
            res += rawIndex * _tree1[index] - _tree2[index];
            index -= _lowbit(index);
        }
        return res;
    }
}