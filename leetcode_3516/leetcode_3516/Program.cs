namespace leetcode_3516;

class Program
{
    /// <summary>
    /// 3516. Find Closest Person
    /// https://leetcode.com/problems/find-closest-person/description/?envType=daily-question&envId=2025-09-04
    /// 3516. 找到最近的人
    /// https://leetcode.cn/problems/find-closest-person/description/?envType=daily-question&envId=2025-09-04
    /// 
    /// </summary>
    /// <para>題目說明（中文翻譯）:</para>
    /// <para>給定三個整數 x、y 和 z，表示數線上三個人的位置：</para>
    /// <para>- x 是第 1 個人的位置。</para>
    /// <para>- y 是第 2 個人的位置。</para>
    /// <para>- z 是第 3 個人的位置，且第 3 個人不會移動。</para>
    /// <para>第 1 個人與第 2 個人以相同速度朝向第 3 個人移動，判斷誰會先到達第 3 個人：</para>
    /// <para>- 若第 1 個人先到，回傳 1。</para>
    /// <para>- 若第 2 個人先到，回傳 2。</para>
    /// <para>- 若同時到達，回傳 0。</para>
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
