using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_735
{
    internal class Program
    {
        /// <summary>
        /// 735. Asteroid Collision
        /// https://leetcode.com/problems/asteroid-collision/
        /// 735. 行星碰撞
        /// https://leetcode.cn/problems/asteroid-collision/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] {10, 2, -5 };
            //Console.Write(AsteroidCollision(input));
            AsteroidCollision(input);
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/asteroid-collision/solution/xing-xing-peng-zhuang-by-leetcode-soluti-u3k0/
        /// https://leetcode.cn/problems/asteroid-collision/solution/by-ac_oier-p4qh/
        /// 
        /// asteroids[i]: 大小重量
        /// 碰撞,小的會消失
        /// 相同大小一起消失
        /// 移動方向相同 不會碰撞
        /// 
        /// 左邊正數
        /// 右邊負數
        /// 才會碰撞
        /// 
        /// 正表示往右移動
        /// 負表示往左移動
        /// </summary>
        /// <param name="asteroids"></param>
        /// <returns></returns>

        public static int[] AsteroidCollision(int[] asteroids)
        {
            Stack<int> stack = new Stack<int>();

            foreach (int aster in asteroids)
            {
                bool alive = true;

                // 存活 + 負數行星 + stack不為空 + peek(峰值 > 0) 判斷是不是會碰撞
                while (alive && aster < 0 && stack.Count > 0 && stack.Peek() > 0)
                {
                    int _peek = stack.Peek();
                    int _aa = -aster;

                    alive = stack.Peek() < -aster; // aster 是否存在
                    // 判斷兩兩是否會碰撞
                    if (stack.Peek() <= -aster)
                    {  // 栈顶行星爆炸;
                        stack.Pop();
                    }
                }

                // 存活就塞入stack
                if (alive)
                {
                    stack.Push(aster);
                }
            }

            int count = stack.Count;
            int[] ans = new int[count];
            // stack先進後出, 故解答要反向輸出 
            for (int i = count - 1; i >= 0; i--)
            {
                ans[i] = stack.Pop();
            }

            Console.WriteLine("[");
            foreach(var value in ans)
            {
                Console.WriteLine(value);
            }
            Console.WriteLine("]");

            return ans;
        }


    }
}
