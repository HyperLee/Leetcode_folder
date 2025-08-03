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
        // 測試資料
        int[][] fruits = new int[][] {
            new int[] {2, 8},
            new int[] {6, 3},
            new int[] {9, 5}
        };
        int startPos = 5;
        int k = 4;

        var solution = new Program();
        int res1 = solution.MaxTotalFruits(fruits, startPos, k);
        int res2 = solution.MaxTotalFruits2(fruits, startPos, k);

        Console.WriteLine($"解法一（滑動視窗+前綴和）：最多可收穫水果數 = {res1}");
        Console.WriteLine($"解法二（滑動視窗+動態步數）：最多可收穫水果數 = {res2}");
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

    /// <summary>
    /// 解法二：滑動視窗 + 動態步數計算
    /// 
    /// 思路與算法：
    /// 假設已知區間 [left, right]，從起點 startPos 出發，最少需要多少步才能遍歷該區間？
    /// 分三種情況：
    /// 1. 區間在 startPos 左側：step = startPos - left
    /// 2. 區間在 startPos 右側：step = right - startPos
    /// 3. startPos 在區間內：
    ///    - 先往左再往右：step = (startPos - left) + (right - left)
    ///    - 先往右再往左：step = (right - startPos) + (right - left)
    ///    - 綜合公式：step = (right - left) + min(|right - startPos|, |startPos - left|)
    /// 
    /// 隨著 left 減小，step 可能會減小但不會增大，因此可用滑動視窗遍歷所有合法區間，
    /// 找到區間內水果數最大值。
    /// 
    /// ref:https://leetcode.cn/problems/maximum-fruits-harvested-after-at-most-k-steps/solutions/2254268/zhai-shui-guo-by-leetcode-solution-4j9v/?envType=daily-question&envId=2025-08-03
    /// 
    /// </summary>
    /// <param name="fruits">已排序的水果位置與數量陣列</param>
    /// <param name="startPos">起始位置</param>
    /// <param name="k">最多可走步數</param>
    /// <returns>可收穫的最多水果總數</returns>
    public int MaxTotalFruits2(int[][] fruits, int startPos, int k)
    {
        int left = 0;
        int right = 0;
        int n = fruits.Length;
        int sum = 0;
        int ans = 0;
        // 每次固定住視窗右邊界
        while (right < n)
        {
            sum += fruits[right][1];
            // 動態調整左邊界，直到步數不超過 k
            while (left <= right && Step(fruits, startPos, left, right) > k)
            {
                sum -= fruits[left][1];
                left++;
            }
            ans = Math.Max(ans, sum);
            right++;
        }
        return ans;
    }

    /// <summary>
    /// 計算從 startPos 覆蓋區間 [left, right] 的最小步數。
    /// step(left, right) = (fruits[right][0] - fruits[left][0]) + min(|fruits[right][0] - startPos|, |startPos - fruits[left][0]|)
    /// </summary>
    /// <param name="fruits">水果位置與數量陣列</param>
    /// <param name="startPos">起始位置</param>
    /// <param name="left">區間左端點</param>
    /// <param name="right">區間右端點</param>
    /// <returns>最小步數</returns>
    private int Step(int[][] fruits, int startPos, int left, int right)
    {
        if (fruits[right][0] <= startPos)
        {
            // 區間全在左側，直接往左
            return startPos - fruits[left][0];
        }
        else if (fruits[left][0] >= startPos)
        {
            // 區間全在右側，直接往右
            return fruits[right][0] - startPos;
        }
        else
        {
            // 區間跨越 startPos，取最小步數
            // step = (right - left) + min(|right - startPos|, |startPos - left|)
            return (fruits[right][0] - fruits[left][0]) + Math.Min(Math.Abs(fruits[right][0] - startPos), Math.Abs(startPos - fruits[left][0]));
        }
    }
}
