using System;
using System.Collections.Generic;

namespace leetcode_1792;

class Program
{
    /// <summary>
    /// <para>
    /// 1792. Maximum Average Pass Ratio
    /// https://leetcode.com/problems/maximum-average-pass-ratio/description/
    ///
    /// A school has several classes taking a final exam. You are given classes, where classes[i] = [pass_i, total_i]: total_i students are in class i and pass_i will pass.
    ///
    /// You also have extraStudents brilliant students, each guaranteed to pass any class they join. Assign every extra student to a class to maximize the average pass ratio across all classes. A class's pass ratio is passing students divided by total students; the average pass ratio is the sum of class ratios divided by the number of classes.
    ///
    /// Return the maximum possible average pass ratio. Answers within 10^-5 of the actual answer are accepted.
    ///
    /// Example 1:
    /// Input: classes = [[1,2],[3,5],[2,2]], extraStudents = 2
    /// Output: 0.78333
    /// Explanation: Assign both extra students to the first class. The average is (3/4 + 3/5 + 2/2) / 3 = 0.78333.
    ///
    /// Example 2:
    /// Input: classes = [[2,4],[3,9],[4,5],[2,10]], extraStudents = 4
    /// Output: 0.53485
    ///
    /// Constraints:
    /// - 1 &lt;= classes.length &lt;= 10^5
    /// - classes[i].length == 2
    /// - 1 &lt;= pass_i &lt;= total_i &lt;= 10^5
    /// - 1 &lt;= extraStudents &lt;= 10^5
    /// </para>
    /// <para>
    /// 1792. 最大平均通過率
    /// https://leetcode.cn/problems/maximum-average-pass-ratio/description/
    ///
    /// 一所學校有多個班級參加期末考。給定 classes，其中 classes[i] = [pass_i, total_i]：第 i 個班級共有 total_i 名學生，其中 pass_i 名會通過考試。
    ///
    /// 另有 extraStudents 名優秀學生，每人加入任何班級都保證能通過考試。請將所有額外學生分配到班級，使全部班級的平均通過率最大。一個班級的通過率等於通過人數除以總人數；平均通過率等於各班通過率總和除以班級數。
    ///
    /// 回傳可達到的最大平均通過率。與正確答案相差不超過 10^-5 的答案都會被接受。
    ///
    /// 範例 1：
    /// 輸入：classes = [[1,2],[3,5],[2,2]], extraStudents = 2
    /// 輸出：0.78333
    /// 說明：將兩名額外學生都分配到第一個班級，平均為 (3/4 + 3/5 + 2/2) / 3 = 0.78333。
    ///
    /// 範例 2：
    /// 輸入：classes = [[2,4],[3,9],[4,5],[2,10]], extraStudents = 4
    /// 輸出：0.53485
    ///
    /// 限制條件：
    /// - 1 &lt;= classes.length &lt;= 10^5
    /// - classes[i].length == 2
    /// - 1 &lt;= pass_i &lt;= total_i &lt;= 10^5
    /// - 1 &lt;= extraStudents &lt;= 10^5
    /// </para>
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例測資 1: LeetCode 常見範例
        int[][] classes1 = new int[][]
        {
            new int[] {1, 2},
            new int[] {3, 5},
            new int[] {2, 2}
        };
        int extra1 = 2;

        var program = new Program();
        double result1 = program.MaxAverageRatio(classes1, extra1);
        Console.WriteLine($"Test1 - result: {result1:F6}");

        // 範例測資 2: 進一步測試較大的值
        int[][] classes2 = new int[][]
        {
            new int[] {2, 4},
            new int[] {3, 9},
            new int[] {4, 5}
        };
        int extra2 = 3;
        double result2 = program.MaxAverageRatio(classes2, extra2);
        Console.WriteLine($"Test2 - result: {result2:F6}");
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
