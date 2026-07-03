namespace leetcode_274;

class Program
{
    /// <summary>
    /// 274. H-Index
    /// https://leetcode.com/problems/h-index/description/
    ///
    /// English:
    /// Given an array of integers citations where citations[i] is the number of citations a researcher received for their ith paper, return the researcher's h-index.
    /// According to the definition of h-index on Wikipedia: The h-index is defined as the maximum value of h such that the given researcher has published at least h papers that have each been cited at least h times.
    ///
    /// 274. H 指数
    /// https://leetcode.cn/problems/h-index/description/
    ///
    /// 繁體中文:
    /// 給定一個整數陣列 citations，其中 citations[i] 表示研究者第 i 篇論文被引用的次數，請回傳這位研究者的 h-index。
    /// 根據 Wikipedia 上對 h-index 的定義：h-index 是滿足以下條件的最大值 h：該研究者至少發表了 h 篇論文，而且這 h 篇論文中的每一篇都至少被引用了 h 次。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一:排序
    /// 首先我们可以将初始的 H 指数 h 设为 0，然后将引用次数排序，并且对排序后的数
    /// 组从大到小遍历。
    /// 
    /// 根据 H 指数的定义，如果当前 H 指数为 h 并且在遍历过程中找到当前值 
    /// citations[i]>h，则说明我们找到了一篇被引用了至少 h+1 次的论文，所以将现有
    /// 的 h 值加 1。继续遍历直到 h 无法继续增大。最后返回 h 作为最终答案。
    /// </summary>
    /// <param name="citations"></param>
    /// <returns></returns>
    public int HIndex(int[] citations)
    {
        Array.Sort(citations);
        int h = 0;
        int i = citations.Length - 1;

        while(i >= 0 && citations[i] > h)
        {
            h++;
            i--;
        }
        return h;
    }

    /// <summary>
    /// 解法二:二分法
    /// 我们需要找到一个值 h，它是满足「有 h 篇论文的引用次数至少为 h」的最大
    /// 值。小于等于 h 的所有值 x 都满足这个性质，而大于 h 的值都不满足这个性
    /// 质。同时因为我们可以用较短时间（扫描一遍数组的时间复杂度为 O(n)，其中 n 为
    /// 数组 citations 的长度）来判断 x 是否满足这个性质，所以这个问题可以用二分搜
    /// 索来解决。
    /// 
    /// 设查找范围的初始左边界 left 为 0，初始右边界 right 为 n。每次在查找范围内取
    /// 中点 mid，同时扫描整个数组，判断是否至少有 mid 个数大于 mid。如果有，说
    /// 明要寻找的 h 在搜索区间的右边，反之则在左边。
    /// </summary>
    /// <param name="citations"></param>
    /// <returns></returns>
    public int HIndex2(int[] citations)
    {
        int left = 0;
        int right = citations.Length;
        int mid = 0;
        int count = 0;

        while(left < right)
        {
            // +1 防止死循环
            mid = (left + right + 1) >> 1;
            count = 0;

            for(int i = 0; i < citations.Length; i++)
            {
                if(citations[i] >= mid)
                {
                    count++;
                }
            }

            if(count >= mid)
            {
                // 要找的答案在 [mid,right] 区间内
                left = mid;
            }
            else
            {
                // 要找的答案在 [0,mid) 区间内
                right = mid - 1;
            }
        }
        return left;
    }

    /// <summary>
    /// 解法三:計數排序
    /// 
    /// </summary>
    /// <param name="citations"></param>
    /// <returns></returns>
    public int HIndex3(int[] citations)
    {
        int n = citations.Length, tot = 0;
        int[] counter = new int[n + 1];
        for (int i = 0; i < n; i++) 
        {
            if (citations[i] >= n) 
            {
                counter[n]++;
            } 
            else 
            {
                counter[citations[i]]++;
            }
        }

        for (int i = n; i >= 0; i--) 
        {
            tot += counter[i];
            if (tot >= i) 
            {
                return i;
            }
        }
        return 0;
    }
}
