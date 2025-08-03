namespace leetcode_2106;

class Program
{
    /// <summary>
    /// 2106. Maximum Fruits Harvested After at Most K Steps
    /// https://leetcode.com/problems/maximum-fruits-harvested-after-at-most-k-steps/description/?envType=daily-question&envId=2025-08-03
    /// 2106. 摘水果
    /// https://leetcode.cn/problems/maximum-fruits-harvested-after-at-most-k-steps/description/?envType=daily-question&envId=2025-08-03
    ///
    /// You are given a 2D integer array fruits where fruits[i] = [positioni, amounti] depicts amounti fruits at the position positioni. fruits is already sorted by positioni in ascending order, and each positioni is unique.
    /// You are also given an integer startPos and an integer k. Initially, you are at the position startPos. From any position, you can either walk to the left or right. It takes one step to move one unit on the x-axis, and you can walk at most k steps in total. For every position you reach, you harvest all the fruits at that position, and the fruits will disappear from that position.
    /// Return the maximum total number of fruits you can harvest.
    ///
    /// 中文描述：
    /// 在一條無限長的 x 軸上，有些位置上有水果。給你一個已排序的二維整數數組 fruits，其中 fruits[i] = [positioni, amounti]，表示在 positioni 位置有 amounti 個水果。每個 positioni 都是唯一的，且按升序排列。
    /// 你還會得到一個整數 startPos 和一個整數 k。你最初位於 startPos 位置。你可以向左或向右移動，每移動一個單位需要一步，你最多可以走 k 步。每到達一個位置，你會收穫該位置上的所有水果，該位置的水果會消失。
    /// 請返回你能收穫的最多水果總數。
    ///
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解題說明：
    /// 本題可用 sliding window（滑動視窗）+ 前綴和優化。由於水果位置已排序，且每次只能走 k 步，
    /// 我們可以枚舉所有可能的收穫區間，並檢查是否能在 k 步內完成。
    /// 主要分兩種策略：
    /// 1. 先往左走到某個點，再往右走到最遠能到的點。
    /// 2. 先往右走到某個點，再往左走到最遠能到的點。
    /// 每次計算區間內水果總數，取最大值。
    /// 使用 prefix sum 優化區間查詢。
    /// </summary>
    /// <param name="fruits">已排序的水果位置與數量陣列</param>
    /// <param name="startPos">起始位置</param>
    /// <param name="k">最多可走步數</param>
    /// <returns>可收穫的最多水果總數</returns>
    public int MaxTotalFruits(int[][] fruits, int startPos, int k)
    {
        // 取得水果數組長度
        int n = fruits.Length;
        // 建立 prefix sum 陣列，方便查詢任意區間水果總數
        int[] prefix = new int[n + 1];
        for (int i = 0; i < n; i++)
        {
            prefix[i + 1] = prefix[i] + fruits[i][1];
        }

        int res = 0;
        // 先往左再往右（滑動視窗）
        int right = 0;
        for (int left = 0; left < n; left++)
        {
            // 移動 right，直到步數超過 k
            while (right < n &&
                Math.Min(Math.Abs(startPos - fruits[left][0]), Math.Abs(startPos - fruits[right][0])) + fruits[right][0] - fruits[left][0] <= k)
            {
                right++;
            }
            // right 指向第一個不合法的位置，區間 [left, right-1] 合法
            if (right > left)
            {
                res = Math.Max(res, prefix[right] - prefix[left]);
            }
        }

        // 先往右再往左（滑動視窗）
        int left2 = 0;
        for (int right2 = 0; right2 < n; right2++)
        {
            // 移動 left2，直到步數超過 k
            while (left2 <= right2 &&
                Math.Min(Math.Abs(startPos - fruits[left2][0]), Math.Abs(startPos - fruits[right2][0])) + fruits[right2][0] - fruits[left2][0] > k)
            {
                left2++;
            }
            // left2~right2 合法
            if (left2 <= right2)
            {
                res = Math.Max(res, prefix[right2 + 1] - prefix[left2]);
            }
        }
        return res;
    }
}
