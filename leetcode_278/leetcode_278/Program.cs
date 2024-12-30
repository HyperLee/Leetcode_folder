namespace leetcode_278
{
    internal class Program
    {
        /// <summary>
        /// 278. First Bad Version
        /// https://leetcode.cn/problems/first-bad-version/description/
        /// 
        /// 278. 第一个错误的版本
        /// https://leetcode.cn/problems/first-bad-version/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 5;
            Console.WriteLine("res: " + FirstBadVersion(n));
        }



        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/first-bad-version/solutions/824522/di-yi-ge-cuo-wu-de-ban-ben-by-leetcode-s-pf8h/
        /// https://leetcode.cn/problems/first-bad-version/solutions/1594074/by-stormsunshine-wu83/
        /// 二分法,
        /// 輸入範圍[1, n]
        /// 由于 bad 一定在范围 [1,n] 内，因此初始时二分查找的范围的下界和上界是 low=1，high=n。
        /// 每次查找时，取 mid 为 left 和 right 的平均数向下取整，对 mid 调用接口，根据结果调整二分查找的范围。
        /// 如果 mid 是错误的，则 bad 小于等于 mid，因此在范围 [left,mid] 中继续查找。
        /// 如果 mid 是正确的，则 bad 大于 mid，因此在范围 [mid + 1,high] 中继续查找。
        /// 当 left == right 时，结束查找，此时 left 即为 bad，返回 low。
        /// 
        /// 錯誤版本會連續, 假如第四筆開始錯誤, 那之後的 也都會是錯誤
        /// 遇到 API 回傳錯誤的, 哪就要把左邊界右移. (因為先前版本都是正確)
        /// 反之
        /// 遇到 API 回傳正確, 那就是要把右邊界左移. (因為右邊都是錯誤版本)
        /// 
        /// 此題目描述不好, 去找其他題目
        /// 同樣是二分法的會比較好理解
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int FirstBadVersion(int n)
        {
            int left = 1;
            int right = n;

            // 循环直至区间左右端点相同
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if(IsBadVersion(mid))
                {
                    // 答案在区间 [left, mid] 中
                    // 右邊界往左
                    right = mid;
                }
                else
                {
                    // 答案在区间 [mid + 1, right] 中
                    // 左邊界往右
                    left = mid + 1;
                }
            }

            return left;
        }


        /// <summary>
        /// 這個 API 呼叫是參考題目測試資料第一筆 寫死的
        /// 實際上, 要在 LEETCODE 呼叫才對
        /// 才是實際 測試資料
        /// 
        /// 第 n == 4 筆開始版本都是錯誤的.
        /// 
        /// 上述輸入範圍是 [1, n]
        /// 但是陣列是從 0 開始
        /// 所以取資料要變成 n - 1 才對
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsBadVersion(int n)
        {
            bool[] input = { false, false, false, true, true};

            bool value = input[n - 1];

            return value;
        }
    }
}
