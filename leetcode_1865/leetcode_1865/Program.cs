namespace leetcode_1865;

class Program
{
    /// <summary>
    /// 題目描述：
    /// 給定兩個整數陣列 nums1 和 nums2，請設計一個資料結構，支援以下兩種查詢：
    /// 1. 對 nums2 的指定索引加上一個正整數。
    /// 2. 計算有多少對 (i, j) 使得 nums1[i] + nums2[j] 等於指定值。
    /// 
    /// FindSumPairs(int[] nums1, int[] nums2)：初始化物件。
    /// void Add(int index, int val)：將 val 加到 nums2[index]。
    /// int Count(int tot)：回傳滿足 nums1[i] + nums2[j] == tot 的組合數。
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
    private readonly int[] nums1;
    // nums2 陣列，允許被修改
    private readonly int[] nums2;
    // cnt: 哈希表，記錄 nums2 各數字出現次數
    private readonly Dictionary<int, int> cnt;

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
