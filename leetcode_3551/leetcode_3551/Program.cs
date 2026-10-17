namespace leetcode_3551;

class Program
{
    /// <summary>
    /// 3551. Minimum Swaps to Sort by Digit Sum
    /// https://leetcode.com/problems/minimum-swaps-to-sort-by-digit-sum/description/
    /// 3551. 数位和排序需要的最小交换次数
    /// https://leetcode.cn/problems/minimum-swaps-to-sort-by-digit-sum/description/
    /// <para>English:</para>
    /// <para>You are given an array <c>nums</c> of distinct positive integers. You need to sort the array in increasing order based on the sum of the digits of each number. If two numbers have the same digit sum, the smaller number appears first in the sorted order.</para>
    /// <para>Return the minimum number of swaps required to rearrange <c>nums</c> into this sorted order.</para>
    /// <para>A swap is defined as exchanging the values at two distinct positions in the array.</para>
    ///
    /// <para>繁體中文：</para>
    /// <para>給定一個由互不相同正整數組成的陣列 <c>nums</c>。你需要依照每個數字的數位總和，以遞增順序排序陣列。如果兩個數字的數位總和相同，較小的數字排在前面。</para>
    /// <para>請回傳將 <c>nums</c> 重新排列為上述排序結果所需的最少交換次數。</para>
    /// <para>交換的定義是：交換陣列中兩個不同位置上的值。</para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 并查集
    /// 1. 计算每个元素的数位和
    /// 2. 按照排序规则对下标进行排序，得到排序后的目标顺序
    /// 3. 对每个原始位置 i，确定其元素在排序后应处于的位置 pos[i]
    /// 4. 初始化并查集，每个位置自成一个集合
    /// 5. 遍历所有位置，将 i 和 pos[i] 合并；若两个位置原本不在同一集合中，连通分量数减一
    /// 6. 返回 n−components
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MinSwaps(int[] nums)
    {
        int n = nums.Length;
        int[] digitSum = new int[n];
        for(int i = 0; i < n; i++)
        {
            int x = nums[i], sum = 0;
            while(x > 0)
            {
                sum += x % 10;
                x /= 10;
            }
            digitSum[i] = sum;
        }

        int[] indices = new int[n];
        for(int i = 0; i < n; i++)
        {
            indices[i] = i;
        }

        Array.Sort(indices, (a, b) =>
        {
            if(digitSum[a] != digitSum[b])
            {
                return digitSum[a].CompareTo(digitSum[b]);
            }
            return nums[a].CompareTo(nums[b]);
        });

        int[] targetPos = new int[n];
        for(int pos = 0; pos < n; pos++)
        {
            targetPos[indices[pos]] = pos;
        }

        int[] parent = new int[n];
        for(int i = 0; i < n; i++)
        {
            parent[i] = i;
        }

        int components = n;
        for(int i = 0; i < n; i++)
        {
            int ri = Find(parent, i);
            int rj = Find(parent, targetPos[i]);
            if(ri != rj)
            {
                parent[ri] = rj;
                components--;
            }
        }

        return n - components;
    }

    private int Find(int[] parent, int x)
    {
        if(parent[x] != x)
        {
            parent[x] = Find(parent, parent[x]);
        }
        return parent[x];
    }

    /// <summary>
    /// 置换环
    /// 1. 计算每个元素的数位和
    /// 2. 按照 (数位和, 数值) 的排序规则对下标进行排序，得到排序后的目标顺序
    /// 3. 对每个原始位置 i，确定其元素在排序后应处于的位置 pos[i]
    /// 4. 循环遍历所有置换环，统计环的数量
    /// 5. 返回 n−cycles
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MinSwaps2(int[] nums)
    {
        int n = nums.Length;
        int[] digitSum = new int[n];
        for (int i = 0; i < n; i++) {
            int x = nums[i], sum = 0;
            while (x > 0) {
                sum += x % 10;
                x /= 10;
            }
            digitSum[i] = sum;
        }

        int[] indices = new int[n];
        for (int i = 0; i < n; i++) {
            indices[i] = i;
        }
        Array.Sort(indices, (a, b) => {
            if (digitSum[a] != digitSum[b]) {
                return digitSum[a].CompareTo(digitSum[b]);
            }
            return nums[a].CompareTo(nums[b]);
        });

        int[] targetPos = new int[n];
        for (int pos = 0; pos < n; pos++) {
            targetPos[indices[pos]] = pos;
        }

        bool[] visited = new bool[n];
        int cycles = 0;
        for (int i = 0; i < n; i++) {
            if (!visited[i]) {
                cycles++;
                int j = i;
                while (!visited[j]) {
                    visited[j] = true;
                    j = targetPos[j];
                }
            }
        }

        return n - cycles;
     
    }
}
