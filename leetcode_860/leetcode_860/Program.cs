namespace leetcode_860
{
    internal class Program
    {
        /// <summary>
        /// 860. Lemonade Change
        /// https://leetcode.com/problems/lemonade-change/description/?envType=daily-question&envId=2024-08-15
        /// 
        /// 860. 柠檬水找零
        /// https://leetcode.cn/problems/lemonade-change/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 5, 5, 5, 10, 20 };

            Console.WriteLine(LemonadeChange(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 根據題目原意
        /// 每一杯檸檬水都是 5 塊錢
        /// 顧客只會買一杯
        /// 會拿 以下 三種面額來購買
        /// 1. 5
        /// 2. 10
        /// 3. 20
        /// 
        /// ----- 因為每杯單價固定, 面額不同而已
        /// 所以 統計收入, 再來判斷 顧客給予哪一種面額
        /// 來決定 能不能找零
        /// 
        /// 還有題目有說顧客是按照順序 ( bills ) 的消費
        /// 所以不能找時候 就是錯誤
        /// 
        /// 能夠用來找零的面額就兩種
        /// 1. 5
        /// 2. 10
        /// 
        /// -> 面額 20 無法找零. 顧客最大面額就 20 而已
        /// 所以找零面額需要小於 20
        /// 
        /// 收到 5 塊, 不用找零
        /// 收到 10 塊, 找 5 塊
        /// 收到 20 塊, 可區分兩種找零方式
        /// 1. 3 個 5 塊錢
        /// 2. 10 塊 + 5 塊
        /// 
        /// 初始化 手上無任何面額零錢
        /// 所以要先收錢, 才能找零
        /// 
        /// </summary>
        /// <param name="bills"></param>
        /// <returns></returns>
        public static bool LemonadeChange(int[] bills)
        {
            // 能夠用來找零的面額
            int five = 0, ten = 0;

            foreach (int b in bills) 
            {
                if (b == 5)
                {
                    // 收到 5 塊, 無須找零
                    five++;
                }
                else if (b == 10)
                {
                    // 收到 10塊, 找 5 塊
                    five--;
                    ten++;
                }
                else if (ten > 0)
                {
                    // 有 10 塊面額
                    // 收到 20, 找 10, 5 塊
                    five--;
                    ten--;
                }
                else
                {
                    // 沒有 10 塊面額
                    // 收到 20, 找 5, 5, 5 塊
                    five -= 3;
                }

                if(five < 0)
                {
                    // 無法找零
                    return false;
                }
            }

            return true;
        }
    }
}
