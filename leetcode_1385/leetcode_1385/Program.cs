namespace leetcode_1385;

class Program
{
    /// <summary>
    /// 1385. Find the Distance Value Between Two Arrays
    /// https://leetcode.com/problems/find-the-distance-value-between-two-arrays/description/
    /// 1385. 兩個陣列間的距離值
    /// https://leetcode.cn/problems/find-the-distance-value-between-two-arrays/description/
    ///
    /// English:
    /// Given two integer arrays arr1 and arr2, and the integer d,
    /// return the distance value between the two arrays.
    /// The distance value is defined as the number of elements arr1[i]
    /// such that there is not any element arr2[j] where |arr1[i]-arr2[j]| &lt;= d.
    ///
    /// 繁體中文：
    /// 給定兩個整數陣列 arr1 和 arr2，以及整數 d，回傳這兩個陣列之間的距離值。
    /// 距離值定義為：arr1 中符合以下條件的元素 arr1[i] 的數量：
    /// 不存在任何 arr2[j]，使得 |arr1[i]-arr2[j]| &lt;= d。
    /// 換句話說，計算 arr1 中與 arr2 所有元素的絕對差皆大於 d 的元素數量。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一:模擬
    /// </summary>
    /// <param name="arr1"></param>
    /// <param name="arr2"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public int FindTheDistanceValue(int[] arr1, int[] arr2, int d)
    {
        int cnt = 0;
        foreach(int x in arr1)
        {
            bool isValid = true;
            foreach(int y in arr2)
            {
                if(Math.Abs(x - y) <= d)
                {
                    isValid = false;
                    break;
                }
            }
            if(isValid)
            {
                cnt++;
            }
        }
        return cnt;
    }

    /// <summary>
    /// 解法二:模擬 採用 邏輯運算
    /// </summary>
    /// <param name="arr1"></param>
    /// <param name="arr2"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public int FindTheDistanceValue2(int[] arr1, int[] arr2, int d)
    {
        int cnt = 0;
        foreach(int x in arr1)
        {
            bool isValid = true;
            foreach(int y in arr2)
            {
                isValid &= Math.Abs(x - y) > d;
            }

            cnt += isValid ? 1 : 0;
        }
        return cnt;
    }

    /// <summary>
    /// 解法三：二分法查找
    /// </summary>
    /// <param name="arr1"></param>
    /// <param name="arr2"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public int FindTheDistanceValue3(int[] arr1, int[] arr2, int d)
    {
        Array.Sort(arr2);
        int cnt = 0;

        foreach(int x in arr1)
        {
            int p = Array.BinarySearch(arr2, x);
            if(p < 0)
            {
                p = ~p;
            }

            bool isValid = true;
            if(p < arr2.Length)
            {
                isValid &= Math.Abs(arr2[p] - x) > d;
            }

            if(p - 1 >= 0 && p - 1 < arr2.Length)
            {
                isValid &= Math.Abs(arr2[p - 1] - x) > d;
            }
            cnt += isValid ? 1 : 0;
        }
        return cnt;
    }
}
