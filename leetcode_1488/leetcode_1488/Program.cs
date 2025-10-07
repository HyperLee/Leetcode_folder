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
        int[] result = AvoidFlood(rains);
        Console.WriteLine("Result: " + string.Join(", ", result));
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
}
