namespace leetcode_605
{
    internal class Program
    {
        /// <summary>
        /// 605. Can Place Flowers
        /// https://leetcode.com/problems/can-place-flowers/
        /// 605. 种花问题
        /// https://leetcode.cn/problems/can-place-flowers/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] flow = new int[] { 1, 0, 0, 0, 1 };
            int n = 1;

            Console.WriteLine("res: " + CanPlaceFlowers2(flow, n));
        }



        /// <summary>
        /// https://leetcode.cn/problems/can-place-flowers/solution/chong-hua-wen-ti-by-leetcode-solution-sojr/
        /// </summary>
        /// <param name="flowerbed"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            int count = 0;
            int m = flowerbed.Length;
            int prev = -1;

            for(int i = 0; i < m; i++)
            {
                if (flowerbed[i] == 1)
                {
                    if(prev == -1)
                    {
                        count += i / 2;
                    }
                    else
                    {
                        count += (i - prev - 2) / 2;
                    }

                    if(count >= 0)
                    {
                        return true;
                    }

                    prev = i;
                }
            }

            if(prev < 0)
            {
                count += (m + 1) / 2;
            }
            else
            {
                count += (m - prev - 1) / 2;
            }

            return count >= n;
        }


        /// <summary>
        /// https://leetcode.cn/problems/can-place-flowers/solution/tu-jie-leetcode-chong-hua-wen-ti-bian-ch-1gdu/
        /// 個人偏好此方法
        /// 比較好懂
        /// 直接模擬 題目描述方法
        /// 
        /// 因陣列只有 0, 1 兩種數字
        /// 故可以區分下列幾種相鄰方式
        /// 00, 01, 10, 11. ==> 11不存在因不能連續種花
        /// 故排除11.
        /// 
        /// 当 flowerbed[i] = 0 时：
        /// 若 flowerbed[i + 1] = 0，即为情况“00”，则当前位置 flowerbed[i] = 0 可以种花，根据规则 flowerbed[i + 1] 则不可以种花。
        /// 若 flowerbed[i + 1] = 1，即为情况“01”，则当前位置 flowerbed[i] = 0 不可以种花，根据规则flowerbed[i + 2] 也不可以种花。
        /// 
        /// 当 flowerbed[i] = 1 时：
        /// 若 flowerbed[i + 1] = 0，即为情况“10”，根据规则 flowerbed[i + 1] 不可以种花。
        /// 若 flowerbed[i + 1] = 1，即为情况“11”，根据规则不存在这种情况。
        /// 
        /// 時間複雜度: O(n), n為陣列長度
        /// 空間複雜度: O(1), 只使用常數空間
        /// 
        /// 當下位置是 flowerbed[i], 左方為合乎規則之位置,
        /// 所以只需要考慮往右邊界遍歷
        /// 
        /// </summary>
        /// <param name="flowerbed"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool CanPlaceFlowers2(int[] flowerbed, int n)
        {
            int length = flowerbed.Length;
            int i = 0;

            while(i < length)
            {
                // 如果當前地塊沒有種花
                if (flowerbed[i] == 0)
                {
                    // 由於先前位置都為合法, 所以此位置如果為 0, 就可以直接種植
                    // 邊界條件(沒有右邊界了, 不用考慮 n + 1 位置), 如果當前為最後一塊且沒有種花, 直接種花
                    if(i == length - 1)
                    {
                        n--;
                        break;
                    }

                    // 如果下一個位置沒有種花
                    // 即 00 狀態
                    if (flowerbed[i + 1] == 0)
                    {
                        // 則當前位置可以種花
                        n--;
                        // 根據規則, 此時下一個位置就沒辦法種花, 直接跳到下下個位置
                        i += 2;
                    }
                    else if (flowerbed[i + 1] == 1)
                    {
                        // 如果下一個位置種花了, 即 010_ 狀態
                        // 根據規則, 則當前位置不能種花, 下一個位置的下一個位置也不能種花, 直接跳到下下下個位置
                        i += 3;
                    }
                }
                else if (flowerbed[i] == 1)
                {
                    // 如果當前地塊種花了, 即 10 狀態
                    // 根據規則, 下個位置不能種花, 直接跳到下下個位置
                    i += 2;
                }

                // 當 n <= 0 代表結束, 不用跑完全部
                // 提早結束
                if(n <= 0)
                {
                    return true;
                }
            }

            if(n > 0)
            {
                return false;
            }

            return true;
        }
    }
}
