using System;
using System.Collections.Generic;
using System.Linq;

namespace leetcode_1488;

class Program
{
    /// <summary>
    /// 1488. Avoid Flood in The City
    /// https://leetcode.com/problems/avoid-flood-in-the-city/description/?envType=daily-question&envId=2025-10-07
    /// 1488. 避免洪水泛滥
    /// https://leetcode.cn/problems/avoid-flood-in-the-city/description/?envType=daily-question&envId=2025-10-07
    /// 
    /// 您的國家有無限個湖泊。最初，所有湖泊都是空的，但當第 n 個湖泊下雨時，第 n 個湖泊會滿水。
    /// 如果在已經滿水的湖泊上再下雨，就會發生洪水。您的目標是避免任何湖泊發生洪水。
    /// 
    /// 給定一個整數陣列 rains，其中：
    /// 
    /// rains[i] > 0 表示第 rains[i] 個湖泊會下雨。
    /// rains[i] == 0 表示這天沒有雨，您可以選擇這天清空一個湖泊。
    /// 返回一個陣列 ans，其中：
    /// 
    /// ans.length == rains.length
    /// ans[i] == -1 如果 rains[i] > 0。
    /// ans[i] 是您在第 i 天選擇清空的湖泊，如果 rains[i] == 0。
    /// 如果有多個有效答案，返回其中任何一個。如果不可能避免洪水，返回空陣列。
    /// 
    /// 注意，如果您選擇清空一個滿水的湖泊，它會變成空的，但如果您選擇清空一個空的湖泊，什麼都不會改變。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試範例
        int[] rains = { 1, 2, 0, 0, 2, 1 };
        // 呼叫解法1
        int[] result1 = AvoidFlood(rains);
        Console.WriteLine("AvoidFlood: " + string.Join(", ", result1));

        // 呼叫解法2（優先隊列）
        int[] result2 = AvoidFlood2(rains);
        Console.WriteLine("AvoidFlood2: " + string.Join(", ", result2));
    }


    /// <summary>
    /// 思路簡述：
    /// - 用一個字典 fullDay 記錄每個湖泊最後一次被下雨（變滿）發生的下標。
    /// - 用一個有序集合 dryDay 保存所有可用來抽水（rains[i]==0）的日期索引。
    /// 當遇到某個已滿的湖泊再次下雨時，必須在該湖上次變滿之後、當前下雨之前，
    /// 選擇一個最早可用的抽水日來清空該湖（這樣策略保留較晚的抽水日給之後更晚變滿的湖）。
    /// 若找不到這樣的抽水日則無解，返回空陣列。
    /// </summary>
    /// <param name="rains"></param>
    /// <returns></returns>
    public static int[] AvoidFlood(int[] rains)
    {
        int n = rains.Length;
        int[] ans = new int[n];

        // lake -> last day it became full
        var fullDay = new Dictionary<int, int>();

        // 有序集合，保存所有下雨前可用來抽水的日期索引（rains[i] == 0）
        var dryDay = new SortedSet<int>();

        for (int i = 0; i < n; i++)
        {
            int lake = rains[i];
            if (lake == 0)
            {
                // 預設填 1（任意不會造成洪水的值），實際值會在需要時被覆蓋為抽乾的湖編號
                ans[i] = 1;
                dryDay.Add(i);
                continue;
            }

            // 當下雨時，該天輸出必為 -1
            ans[i] = -1;

            if (fullDay.TryGetValue(lake, out int lastFullDay))
            {
                // 需要找出一個 dryDay 中大於 lastFullDay 的最小索引
                // 使用 GetViewBetween(lastFullDay + 1, int.MaxValue) 並取最小值
                var view = dryDay.GetViewBetween(lastFullDay + 1, int.MaxValue);
                if (view.Count == 0)
                {
                    // 找不到可用的抽水日，必定發生洪水
                    return Array.Empty<int>();
                }
                int d = view.Min; // 最早可用的抽水日
                ans[d] = lake; // 在那天抽乾這個 lake
                dryDay.Remove(d); // 這天已被使用
            }

            // 更新該湖最近一次變滿的日子
            fullDay[lake] = i;
        }

        return ans;
    }

    /// <summary>
    /// 解法2（優先隊列 / 貪心）
    ///
    /// 思路和演算法：
    /// 當沒有湖泊下雨的時候，可以選擇抽乾一個湖泊。根據貪心策略，為了避免任一湖泊發生洪水，每次選擇抽乾湖泊的方法如下：
    /// - 如果沒有裝滿水的湖泊，則可以抽乾任何一個湖泊（這裡預設抽乾編號為 1）。
    /// - 如果有裝滿水的湖泊，則應該抽乾一個裝滿水的湖泊中「下次下雨日期最近」的那個湖泊。
    ///
    /// 實作細節：
    /// 1. 反向遍歷 rains，為每個下雨的日子計算該湖泊的下次下雨日期（若無則為 int.MaxValue 或留為 length 表示無下一次下雨）。
    /// 2. 正向遍歷 rains，維護一個集合 fullLakes 保存目前已經裝滿水的湖泊，並用優先隊列 pq 儲存 (lake, nextRainIndex)，優先依 nextRainIndex 最小排序。
    /// 3. 遇到 rains[i] == 0 時：若 pq 為空則任意抽乾湖泊（填 1），否則從 pq 取出 nextRain 最近的湖泊並抽乾它，同時從 fullLakes 移除該湖。
    /// 4. 遇到 rains[i] > 0 時：若該 lake 已在 fullLakes 中，表示會發生洪水，回傳空陣列；否則將該 lake 加入 fullLakes，並把 (lake, nextRain) 推入 pq（如果存在下一次下雨）。
    ///
    /// 此解法運用了貪心：每次抽乾最緊迫（下次下雨最近）的湖泊，可以保證給後面更晚下雨的湖泊更多彈性，避免不必要的洪水。
    /// </summary>
    /// <param name="rains">輸入陣列</param>
    /// <returns>回傳 ans 陣列或空陣列表示無解</returns>
    public static int[] AvoidFlood2(int[] rains)
    {
        int length = rains.Length;
        int[] ans = new int[length];
        Array.Fill(ans, -1);

        int[] nextRains = new int[length];
        IDictionary<int, int> lakeDays = new Dictionary<int, int>();

        // 反向遍歷，計算每個下雨日的下一次下雨位置
        for (int i = length - 1; i >= 0; i--)
        {
            int lake = rains[i];
            if (lake > 0)
            {
                // 若 dictionary 中不存在，先插入一個預設值（代表無下一次下雨）
                if (!lakeDays.ContainsKey(lake))
                {
                    lakeDays[lake] = int.MaxValue;
                }
                nextRains[i] = lakeDays[lake] == int.MaxValue ? length : lakeDays[lake];
                lakeDays[lake] = i;
            }
        }

        ISet<int> fullLakes = new HashSet<int>();
        // 優先佇列元素為 int[] { lake, nextIndex }，優先權為 nextIndex（越小越先出列）
        var pq = new PriorityQueue<int[], int>();

        for (int i = 0; i < length; i++)
        {
            int lake = rains[i];
            if (lake > 0)
            {
                // 如果該湖已經是滿的，代表洪水發生
                if (!fullLakes.Add(lake))
                {
                    return new int[0];
                }
                // 若該湖之後還會下雨，則將 (lake, nextRains[i]) 放入優先隊列
                if (nextRains[i] < length)
                {
                    pq.Enqueue(new int[] { lake, nextRains[i] }, nextRains[i]);
                }
            }
            else
            {
                // 今天可以抽乾一個湖泊
                if (pq.Count == 0)
                {
                    ans[i] = 1; // 任意抽乾第1個湖泊（無害）
                }
                else
                {
                    int[] pair = pq.Dequeue();
                    int nextLake = pair[0];
                    ans[i] = nextLake;
                    fullLakes.Remove(nextLake);
                }
            }
        }

        return ans;
    }
}
