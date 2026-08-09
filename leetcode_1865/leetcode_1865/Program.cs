namespace leetcode_1865;

class Program
{
    /// <summary>
    /// <para>
    /// 1865. Finding Pairs With a Certain Sum
    /// https://leetcode.com/problems/finding-pairs-with-a-certain-sum/description/
    ///
    /// You are given integer arrays nums1 and nums2. Implement a data structure supporting two query types:
    /// - Add a positive integer to nums2 at a specified index.
    /// - Count pairs (i, j) such that nums1[i] + nums2[j] equals a specified value, where 0 &lt;= i &lt; nums1.length and 0 &lt;= j &lt; nums2.length.
    ///
    /// Implement FindSumPairs:
    /// - FindSumPairs(int[] nums1, int[] nums2) initializes the object.
    /// - void add(int index, int val) applies nums2[index] += val.
    /// - int count(int tot) returns the number of pairs whose sum equals tot.
    ///
    /// Example 1:
    /// Input:
    /// ["FindSumPairs","count","add","count","count","add","add","count"]
    /// [[[1,1,2,2,2,3],[1,4,5,2,5,4]],[7],[3,2],[8],[4],[0,1],[1,1],[7]]
    /// Output: [null,8,null,2,1,null,null,11]
    /// Explanation:
    /// Initialize with nums1 = [1,1,2,2,2,3] and nums2 = [1,4,5,2,5,4].
    /// count(7) returns 8: six pairs make 2 + 5 and two pairs make 3 + 4.
    /// add(3,2) changes nums2 to [1,4,5,4,5,4].
    /// count(8) returns 2; count(4) returns 1.
    /// add(0,1) and add(1,1) change nums2 to [2,5,5,4,5,4].
    /// count(7) returns 11: nine pairs make 2 + 5 and two pairs make 3 + 4.
    ///
    /// Constraints:
    /// - 1 &lt;= nums1.length &lt;= 1000
    /// - 1 &lt;= nums2.length &lt;= 10^5
    /// - 1 &lt;= nums1[i] &lt;= 10^9
    /// - 1 &lt;= nums2[i] &lt;= 10^5
    /// - 0 &lt;= index &lt; nums2.length
    /// - 1 &lt;= val &lt;= 10^5
    /// - 1 &lt;= tot &lt;= 10^9
    /// - At most 1000 calls are made to add and count each.
    /// </para>
    /// <para>
    /// 1865. 尋找和為指定值的下標對
    /// https://leetcode.cn/problems/finding-pairs-with-a-certain-sum/description/
    ///
    /// 給定整數陣列 nums1 和 nums2。請實作支援兩種查詢的資料結構：
    /// - 對 nums2 指定索引的元素加上一個正整數。
    /// - 計算滿足 nums1[i] + nums2[j] 等於指定值的 (i, j) 數量，其中 0 &lt;= i &lt; nums1.length 且 0 &lt;= j &lt; nums2.length。
    ///
    /// 實作 FindSumPairs：
    /// - FindSumPairs(int[] nums1, int[] nums2) 初始化物件。
    /// - void add(int index, int val) 執行 nums2[index] += val。
    /// - int count(int tot) 回傳總和等於 tot 的配對數量。
    ///
    /// 範例 1：
    /// 輸入：
    /// ["FindSumPairs","count","add","count","count","add","add","count"]
    /// [[[1,1,2,2,2,3],[1,4,5,2,5,4]],[7],[3,2],[8],[4],[0,1],[1,1],[7]]
    /// 輸出：[null,8,null,2,1,null,null,11]
    /// 說明：
    /// 以 nums1 = [1,1,2,2,2,3] 與 nums2 = [1,4,5,2,5,4] 初始化。
    /// count(7) 回傳 8：六組配對得到 2 + 5，兩組配對得到 3 + 4。
    /// add(3,2) 將 nums2 改為 [1,4,5,4,5,4]。
    /// count(8) 回傳 2；count(4) 回傳 1。
    /// add(0,1) 與 add(1,1) 將 nums2 改為 [2,5,5,4,5,4]。
    /// count(7) 回傳 11：九組配對得到 2 + 5，兩組配對得到 3 + 4。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums1.length &lt;= 1000
    /// - 1 &lt;= nums2.length &lt;= 10^5
    /// - 1 &lt;= nums1[i] &lt;= 10^9
    /// - 1 &lt;= nums2[i] &lt;= 10^5
    /// - 0 &lt;= index &lt; nums2.length
    /// - 1 &lt;= val &lt;= 10^5
    /// - 1 &lt;= tot &lt;= 10^9
    /// - add 與 count 各最多呼叫 1000 次。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

/// <summary>
/// FindSumPairs 資料結構設計說明：
/// 
/// 本類別支援兩種操作：
/// 1. Add(index, val)：將 val 加到 nums2[index]，並即時更新哈希表 cnt。
/// 2. Count(tot)：計算有多少對 (i, j) 使得 nums1[i] + nums2[j] == tot。
/// 
/// 解題思路：
/// - 由於 nums1 長度 <= nums2，查詢時以 nums1 為外層，nums2 用哈希表 cnt 儲存每個值的出現次數。
/// - Add 操作時，先將舊值在 cnt 中次數減 1，更新 nums2[index]，再將新值在 cnt 中次數加 1。
/// - Count 操作時，枚舉 nums1 的每個元素 num，查詢 cnt[tot - num]，累加所有結果即為答案。
/// 
/// 時間複雜度：
/// - Add: O(1)
/// - Count: O(n1)，n1 為 nums1 長度
/// </summary>
public class FindSumPairs
{
    // nums1 原始陣列，僅查詢不變動
    private int[] nums1;
    // nums2 陣列，允許被修改
    private int[] nums2;
    // cnt: 哈希表，記錄 nums2 各數字出現次數
    private Dictionary<int, int> cnt;

    /// <summary>
    /// 初始化 FindSumPairs 物件，儲存 nums1、nums2 並建立 nums2 的計數哈希表。
    /// </summary>
    /// <param name="nums1">第一個整數陣列</param>
    /// <param name="nums2">第二個整數陣列</param>
    public FindSumPairs(int[] nums1, int[] nums2)
    {
        this.nums1 = nums1;
        this.nums2 = nums2;
        this.cnt = new Dictionary<int, int>();
        // 初始化 cnt，記錄 nums2 各數字出現次數
        foreach (int num in nums2)
        {
            if (cnt.ContainsKey(num))
            {
                cnt[num]++;
            }
            else
            {
                cnt[num] = 1;
            }
        }
    }

    /// <summary>
    /// 將 val 加到 nums2[index]，並即時更新哈希表 cnt。
    /// 若舊值次數減為 0，仍保留 key 但值為 0（可依需求移除）。
    /// </summary>
    /// <param name="index">nums2 的索引</param>
    /// <param name="val">要加的正整數</param>
    public void Add(int index, int val)
    {
        int oldVal = nums2[index];
        // 將舊值在 cnt 中次數減 1
        cnt[oldVal]--;
        // 更新 nums2[index]
        nums2[index] += val;
        int newVal = nums2[index];
        // 新值在 cnt 中次數加 1
        if (cnt.ContainsKey(newVal))
        {
            cnt[newVal]++;
        }
        else
        {
            cnt[newVal] = 1;
        }
    }

    /// <summary>
    /// 計算有多少對 (i, j) 使得 nums1[i] + nums2[j] == tot。
    /// </summary>
    /// <param name="tot">目標和</param>
    /// <returns>滿足條件的組合數</returns>
    public int Count(int tot)
    {
        int res = 0;
        // 枚舉 nums1 的每個元素 num
        foreach (int num in nums1)
        {
            int rest = tot - num;
            // 查詢 cnt[rest]，累加所有結果
            if (cnt.TryGetValue(rest, out int count))
            {
                res += count;
            }
        }
        return res;
    }
}
