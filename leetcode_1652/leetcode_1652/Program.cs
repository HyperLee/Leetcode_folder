namespace leetcode_1652
{
    internal class Program
    {
        /// <summary>
        /// 1652. Defuse the Bomb
        /// https://leetcode.com/problems/defuse-the-bomb/?envType=daily-question&envId=2024-11-18
        /// 
        /// 1652. 拆炸弹
        /// https://leetcode.cn/problems/defuse-the-bomb/description/
        /// 
        /// 方法2: 基礎解法, 將輸入的 code 變成兩倍長度來處理  時間複雜度: O(n)  空間複雜度: O(n), n: code長度
        /// 方法1: 進階方法, 用 mod n 去計算 時間複雜度: O(n)  空間複雜度: O(1), n: code長度
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] code = { 2, 4, 9, 3 };
            int k = -2;

            //Console.WriteLine(Decrypt(code, k));
            var res1 = Decrypt(code, k);
            Console.Write("方法1: ");
            foreach (int i in res1)
            {
                Console.Write(i + ", ");
            }

            Console.WriteLine(" ");
            var res2 = Decrypt2(code, k);
            Console.Write("方法2: ");
            foreach (int i in res2) 
            {
                Console.Write(i + ", ");
            }

            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/defuse-the-bomb/solutions/2765762/on-ding-chang-hua-dong-chuang-kou-python-y2py/  << 
        /// https://leetcode.cn/problems/defuse-the-bomb/solutions/1845161/by-ac_oier-osbg/
        /// 
        /// 簡單描述題目要求
        /// 1. 當 k > 0, 第 i 個位置往後取 k 個 index 做加總
        /// 2. 當 k < 0, 第 i 個位置往前取 k 個 index 做加總
        /// 3. 當 k = 0, 全部位置數值都用 0 取代
        /// 
        /// 滑動視窗解法
        /// 注意无论 k > 0 还是 k < 0，窗口都是在向右移动的，所以确定好第一个窗口的位置
        /// ，就可以把 k > 0 和 k < 0 两种情况合并起来了。
        /// 
        /// k > 0，第一个窗口的的下标范围为 [1, k + 1)。
        /// k < 0，第一个窗口的的下标范围为 [n -∣k∣, n)。
        /// 无论 k 是正是负，窗口的大小都是 ∣k∣。
        /// 在窗口向右滑动时，设移入窗口的元素下标为 r mod n，则移出窗口的元素下标为 (r - ∣k∣) mod n。
        /// 
        /// 循環 = 滑動視窗 + 指針取 mode 計算
        /// 
        /// </summary>
        /// <param name="code">循環陣列</param>
        /// <param name="k">key 位置</param>
        /// <returns></returns>
        public static int[] Decrypt(int[] code, int k)
        {
            int n = code.Length;
            int[] res = new int[n];
            int r = k > 0 ? k + 1 : n;
            k = Math.Abs(k);
            int s = 0;

            for (int i =  r - k; i < r; i++)
            {
                // 計算 res[0]
                s += code[i];
            }

            // 計算 res[0] 之後的 index 位置數值
            for(int i = 0; i < n; i++)
            {
                res[i] = s;
                s += code[r % n] - code[(r - k) % n];
                r++;
            }

            return res;
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/defuse-the-bomb/solutions/1843157/chai-zha-dan-by-leetcode-solution-01x3/
        /// 
        /// 簡單描述題目要求
        /// 1. 當 k > 0, 第 i 個位置往後取 k 個 index 做加總
        /// 2. 當 k < 0, 第 i 個位置往前取 k 個 index 做加總
        /// 3. 當 k = 0, 全部位置數值都用 0 取代
        /// 
        /// 滑動視窗
        /// 兩倍長度當作循環
        /// 
        /// 为了说明和编码方便，我们将原数组进行拼接操作 code = code + code，
        /// 如果不进行拼接，我们也仅需要在维护区间端点时进行取模映射操作
        /// ，即 li + 1 ​= (li ​+ 1)(mod n)，ri + 1 ​= (ri + 1)(mod n)，使空间复杂度降到 O(1)。
        /// 上面方法一 就是沒有拼接兩段長度解法
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int[] Decrypt2(int[] code, int k)
        {
            int n = code.Length;
            if(k == 0)
            {
                // 直接回傳 0
                return new int[n];
            }
            int[] res = new int[n];
            // code 的兩倍大, 直接延長. 以利計算
            int[] newcode = new int[n * 2];
            // 將 code 資料 複製到 newcode 前半部
            Array.Copy(code, 0, newcode, 0, n);
            // 將 code 資料 複製到 newcode 後半部
            Array.Copy(code, 0, newcode, n, n);
            // 用兩倍長度的來計算
            code = newcode;
            // 左邊界
            int left = k > 0 ? 1 : n + k;
            // 右邊界
            int right = k > 0 ? k : n - 1;
            int w = 0;

            for(int i = left; i <= right; i++)
            {
                // 計算 res[0]
                w += code[i];
            }

            // 計算 res[0] 之後的 index 位置數值
            // 視窗滑動, 扣除原先左邊界, 加入新的右邊界
            // 左右邊界範圍整體往右移動
            for (int i = 0; i < n; i++)
            {
                // 更新 res[i]
                res[i] = w;
                // 扣除左邊界 index 位置數值
                w -= code[left];
                // 右邊界右移, 加入新的 index 數值
                w += code[right + 1];
                // 左邊界往右
                left++;
                // 右邊界往右
                right++;
            }

            return res;

        }
    }
}
