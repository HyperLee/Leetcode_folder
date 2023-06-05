using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1232
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1232 Check If It Is a Straight Line
        /// https://leetcode.com/problems/check-if-it-is-a-straight-line/
        /// 缀点成线
        /// https://leetcode.cn/problems/check-if-it-is-a-straight-line/
        /// 
        /// 不規則陣列
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 1, 2 },
                 new int[]{ 2, 3 },
                 new int[]{ 3, 4 },
                 new int[]{ 4, 5 },
                 new int[]{ 5, 6 },
                 new int[]{ 6, 7 }
            };

            Console.WriteLine(CheckStraightLine(input));
            Console.ReadKey();

        }



        /// <summary>
        /// https://leetcode.cn/problems/check-if-it-is-a-straight-line/solution/by-stormsunshine-u8wu/
        /// https://leetcode.cn/problems/check-if-it-is-a-straight-line/solution/jin-liang-bu-yao-yong-pan-duan-xie-lu-sh-9r4r/
        /// 
        /// 本題比較類似 需要知道基本概念 or 數學
        /// 之後使用程式碼實現
        /// 
        /// 簡易提示向量解法:
        /// 已知三點 A(1,2), B(3,4), C(6,7)，求這三點是否在—條直線?
        /// 解：
        /// 向量AB = (3，4)−(1，2)＝(2，2)
        /// 向量AC = (6，7)−(1，2)＝(5，5)
        /// x * yi - xi * y
        /// 向量AB × 向量AC = (2 × 5) - (5 × 2) = 0
        /// 所以AB和AC平行
        /// 所以A,B,C三點共線
        /// 
        /// 兩組座標, 後面扣除 前面
        /// 之後x * yi - xi * y 在相減為 0
        /// 即可 視為一直線上
        /// 
        /// 2個點可以為一直線
        /// 當座標點為三個開始, 
        /// 就需要判斷了
        /// 簡易判斷方式 類似上述方式
        /// 
        /// 本題目delta 為 i = 0,1 兩點座標 判斷
        /// 因 前兩個點 i = 0, 1 為直線
        /// 
        /// 故 curDelta 從 i >=2 開始 也可以稱為第三組座標
        /// 故從 第三個點開始判斷 取出第三個開始往後的座標, 與 i = 0 做判斷 為直線 即可
        ///  i>= 2 每一組座標取出來之後 與 i = 0 座標判斷
        ///  
        /// 似乎斜率也可以當作判斷方式, 不過是除法需要小心有0的問題要額外處理
        /// 不然會出錯
        /// http://math.prhs.ptc.edu.tw/math/rule-by-longtung.htm
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public static bool CheckStraightLine(int[][] coordinates)
        {
            int length = coordinates.Length;
            
            // 2點一線, 3點需要下面判斷方式
            if (length == 2)
            {
                return true;
            }
            
            // x1 - x0, y1 - y0
            int deltaX = coordinates[1][0] - coordinates[0][0], deltaY = coordinates[1][1] - coordinates[0][1];

            // curDelta 從 i >=2 開始
            for (int i = 2; i < length; i++)
            {
                int curDeltaX = coordinates[i][0] - coordinates[0][0], curDeltaY = coordinates[i][1] - coordinates[0][1];
                // x * yi - xi * y
                if (deltaX * curDeltaY - curDeltaX * deltaY != 0)
                {
                    return false;
                }
            }
            
            return true;

        }


    }
}
