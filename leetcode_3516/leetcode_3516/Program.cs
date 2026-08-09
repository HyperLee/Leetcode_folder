namespace leetcode_3516;

class Program
{
    /// <summary>
    /// 3516. Find Closest Person
    /// https://leetcode.com/problems/find-closest-person/description/
    /// <para>
    /// You are given integers x, y, and z, representing positions on a number line: x is Person 1, y is Person 2, and z is stationary Person 3.
    ///
    /// Person 1 and Person 2 move toward Person 3 at the same speed. Return 1 if Person 1 arrives first, 2 if Person 2 arrives first, or 0 if they arrive simultaneously.
    ///
    /// Example 1:
    /// Input: x = 2, y = 7, z = 4
    /// Output: 1
    /// Explanation: Person 1 travels 2 steps, while Person 2 travels 3 steps, so Person 1 arrives first.
    ///
    /// Example 2:
    /// Input: x = 2, y = 5, z = 6
    /// Output: 2
    /// Explanation: Person 1 travels 4 steps, while Person 2 travels 1 step, so Person 2 arrives first.
    ///
    /// Example 3:
    /// Input: x = 1, y = 5, z = 3
    /// Output: 0
    /// Explanation: Both people travel 2 steps and arrive simultaneously.
    ///
    /// Constraints:
    /// - 1 &lt;= x, y, z &lt;= 100
    /// </para>
    /// <para>
    /// 3516. 找出最近的人
    /// https://leetcode.cn/problems/find-closest-person/description/
    ///
    /// 給定整數 x、y、z，表示數線上的位置：x 是第 1 個人，y 是第 2 個人，z 是不移動的第 3 個人。
    ///
    /// 第 1 個人與第 2 個人以相同速度朝第 3 個人移動。若第 1 個人先到回傳 1，第 2 個人先到回傳 2，同時到達則回傳 0。
    ///
    /// 範例 1：
    /// 輸入：x = 2, y = 7, z = 4
    /// 輸出：1
    /// 解釋：第 1 個人移動 2 步，第 2 個人移動 3 步，因此第 1 個人先到。
    ///
    /// 範例 2：
    /// 輸入：x = 2, y = 5, z = 6
    /// 輸出：2
    /// 解釋：第 1 個人移動 4 步，第 2 個人移動 1 步，因此第 2 個人先到。
    ///
    /// 範例 3：
    /// 輸入：x = 1, y = 5, z = 3
    /// 輸出：0
    /// 解釋：兩人都移動 2 步並同時到達。
    ///
    /// 限制條件：
    /// - 1 &lt;= x, y, z &lt;= 100
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例測試資料
        var tests = new (int x, int y, int z)[]
        {
            (1, 2, 3),
            (2, 1, 3),
            (1, 3, 2),
            (5, 5, 5),
            (-2, 4, 1)
        };

        foreach (var (x, y, z) in tests)
        {
            int result = new Program().FindClosest(x, y, z);
            Console.WriteLine($"x={x}, y={y}, z={z} => result: {result}");
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public int FindClosest(int x, int y, int z)
    {
        int distanceX = Math.Abs(z - x);
        int distanceY = Math.Abs(z - y);

        if (distanceX < distanceY)
        {
            return 1;
        }
        else if (distanceX > distanceY)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}
