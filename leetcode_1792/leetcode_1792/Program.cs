using System;
using System.Collections.Generic;

namespace leetcode_1792;

class Program
{
    /// <summary>
    /// 1792. Maximum Average Pass Ratio
    /// https://leetcode.com/problems/maximum-average-pass-ratio/description/?envType=daily-question&envId=2025-09-01
    /// 1792. 最大平均通過率
    /// https://leetcode.cn/problems/maximum-average-pass-ratio/description/?envType=daily-question&envId=2025-09-01
    ///
    /// 題目描述（中文翻譯）:
    /// 有一所學校有多個班級，每個班級都會舉行期末考。你被給予一個二維整數陣列 classes，其中 classes[i] = [passi, totali]。
    /// 你事先知道第 i 個班級有 totali 名學生，但只有 passi 名學生會通過考試。
    ///
    /// 另外給你一個整數 extraStudents。還有 extraStudents 名優秀的學生，他們被分配到任何班級時都能保證通過該班級的考試。
    /// 你想把每一位 extraStudents 分配到某個班級，使得所有班級的平均通過率最大化。
    ///
    /// 一個班級的通過率等於該班級通過考試的學生數除以該班級的總學生數。平均通過率等於所有班級的通過率總和除以班級數。
    ///
    /// 請回傳在分配 extraStudents 之後能達到的最大平均通過率。答案在 1e-5 以內被視為正確。
    ///
    /// 範例輸入/輸出、限制條件等請參見原題鏈接。
    ///
    /// 解題說明:
    /// 本題關鍵在於每次分配一名能保證通過的學生時，應該把他分配到可以帶來最大 "邊際通過率增益" 的班級。
    /// 假設班級當前為 (pass, total)，增加一名保證通過學生後，通過率的增量為:
    /// delta(pass,total) = (pass+1)/(total+1) - pass/total。
    ///
    /// 因此採用貪婪做法並用最大堆 (priority queue) 追蹤每個班級的 delta，每次選擇 delta 最大的班級分配一名學生，更新該班的 delta 並放回堆中。
    /// 這樣能保證在每一步讓總通過率和增加最多，最終得到最大平均通過率（平均為總和除以班級數）。
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
    /// 
    /// </summary>
    /// <param name="classes"></param>
    /// <param name="extraStudents"></param>
    /// <returns></returns>
    public double MaxAverageRatio(int[][] classes, int extraStudents)
    {
        if (classes == null || classes.Length == 0)
        {
            return 0.0;
        }

        // 優先佇列：存放 tuple(pass, total) 並依邊際增益遞減排序
        // 這裡用 Comparer<double> 反轉預設排序，使得 delta 大的項目先被 Dequeue（語意更明確）
        var descComparer = Comparer<double>.Create((a, b) => b.CompareTo(a));
        var pq = new PriorityQueue<(int pass, int total), double>(descComparer);

        // 初始化：把每個班的狀態與其當前的 delta 推進堆中（不需要取負號）
        foreach (var c in classes)
        {
            int p = c[0];
            int t = c[1];
            double delta = Delta(p, t);
            pq.Enqueue((p, t), delta);
        }

        // 每次取出當前增益最大的班級，分配一名學生，計算新的 delta 並放回堆
        for (int i = 0; i < extraStudents; i++)
        {
            var top = pq.Dequeue();
            // 更新該班級的 pass 和 total
            int p = top.pass + 1;
            int t = top.total + 1;
            double delta = Delta(p, t);
            pq.Enqueue((p, t), delta);
        }

        // 將堆中所有班級的最終通過率相加以計算平均
        double sum = 0.0;
        while (pq.Count > 0)
        {
            var item = pq.Dequeue();
            sum += (double)item.pass / item.total;
        }

        return sum / classes.Length;
    }

    /// <summary>
    /// 計算將 1 名保證通過的學生加入班級 (pass, total) 時，班級通過率的增加量。
    /// delta = (pass+1)/(total+1) - pass/total
    /// 此方法用於評估每次分配哪個班級能帶來最大的立即回報。
    /// </summary>
    /// <param name="pass">當前通過人數</param>
    /// <param name="total">當前總人數</param>
    /// <returns>增加一名通過學生後的通過率增量</returns>
    static double Delta(int pass, int total)
    {
        return (double)(pass + 1) / (total + 1) - (double)pass / total;
    }
}
