using System.Runtime.InteropServices;
using System.Text;

namespace leetcode_726
{
    internal class Program
    {

        /// <summary>
        /// 726. Number of Atoms
        /// https://leetcode.com/problems/number-of-atoms/description/?envType=daily-question&envId=2024-07-14
        /// 726. 原子的数量
        /// https://leetcode.cn/problems/number-of-atoms/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "Mg(OH)2";
            Console.WriteLine(CountOfAtoms(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/number-of-atoms/solutions/858790/yuan-zi-de-shu-liang-by-leetcode-solutio-54lv/
        /// https://leetcode.cn/problems/number-of-atoms/solutions/1459206/by-stormsunshine-fc4h/
        /// https://leetcode.cn/problems/number-of-atoms/solutions/859070/gong-shui-san-xie-shi-yong-xiao-ji-qiao-l5ak4/
        /// 
        /// 在 C# 中，堆栈（Stack）是一种后进先出（LIFO，Last In First Out）数据结构。
        /// 
        /// 每一組括號 都是一組(層)
        /// 
        /// 大寫字母 = 元素
        /// 大寫後面接續小寫 = 元素(字串長度較長)
        /// 
        /// 字母後面接續數字 = 元素數量
        /// 如果字母後面沒數字 那就給數量1(保底是1, 沒有0)
        /// 
        /// 層與層的元素的數量計算
        /// 都是乘法, 不是加法...
        /// ex:
        /// (A(OH)2)2
        /// 最裡面括號 O:2, H:2
        /// 外面一層還要乘上2
        /// A:2, 0:4, H:4
        /// 
        /// Push(T item)：將元素 item 推入堆疊的頂部。
        /// Pop()：移除堆疊頂部的元素並返回該元素。
        /// Peek()：返回堆疊頂部的元素而不移除它。
        /// Count：返回堆疊中元素的個數。
        /// Clear()：清除堆疊中的所有元素。
        /// 
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public static string CountOfAtoms(string formula)
        {
            int i = 0;
            int n = formula.Length;

            // stack裡面存 <元素, 數量>
            Stack<Dictionary<string, int>> stack = new Stack<Dictionary<string, int>>();
            stack.Push(new Dictionary<string, int>());

            while( i < n)
            {
                char ch = formula[i];
                if(ch =='(')
                {
                    i++;
                    // 遇到左括號塞一個空進入stack, 接下來開始統計元素數量
                    stack.Push(new Dictionary<string, int>());
                }
                else if(ch == ')')
                {
                    // 遇到右括號代表這一層結束, 先看右括號右邊有沒有數字
                    i++;

                    // 檢測括號右邊的數字
                    ////////////////取出數量////////////////////////////////
                    int num = 0;
                    while (i < n && char.IsNumber(formula[i]))
                    {
                        // 檢測數字
                        num = num * 10 + formula[i++] - '0';
                    }

                    if (num == 0)
                    {
                        // 元素右邊, 不是數字給1
                        num = 1;
                    }

                    // 彈出括號內元素數量
                    Dictionary<string, int> popdic = stack.Pop();
                    Dictionary<string, int> dic = stack.Peek();

                    // 層與層的元素的數量計算, 都是乘法, 不是加法...
                    foreach (KeyValuePair<string, int> pair in popdic)
                    {
                        string atom = pair.Key;
                        int v = pair.Value;
                        // 將括號內的元素數量乘上 num, 並壘加到上一層的元素數量
                        if(dic.ContainsKey(atom))
                        {
                            dic[atom] += v * num;
                        }
                        else
                        {
                            dic.Add(atom, v * num);
                        }
                    }

                }
                else
                {
                    // 非括號, 直接檢測元素, 以及其數量
                    ///////////////取出元素/////////////////////////////////
                    StringBuilder sb_atom = new StringBuilder();
                    // 檢測首個字母
                    sb_atom.Append(formula[i++]);
                    // 檢測首字母後的小寫字母
                    while (i < n && char.IsLower(formula[i]))
                    {
                        
                        sb_atom.Append(formula[i++]);
                    }

                    ////////////////取出數量////////////////////////////////
                    int num = 0;
                    while (i < n && char.IsNumber(formula[i]))
                    {
                        // 檢測數字
                        num = num * 10 + formula[i++] - '0';
                    }

                    if(num == 0)
                    {
                        // 元素右邊, 不是數字給1
                        num = 1;
                    }
                    ////////////////////////////////////////////////////
                    
                    Dictionary<string, int> dic = stack.Peek();
                    // 統計元素 數量
                    if(dic.ContainsKey(sb_atom.ToString()))
                    {
                        dic[sb_atom.ToString()] += num;
                    }
                    else
                    {
                        dic.Add(sb_atom.ToString(), num);
                    }
                }

                int tempcount = stack.Count;

            }

            // 輸出結果
            Dictionary<string, int> dictionary = stack.Pop();
            List<KeyValuePair<string, int>> pairs = new List<KeyValuePair<string, int>>(dictionary);
            // 字母排序(ASCII, 遞增)
            pairs.Sort((p1, p2) => p1.Key.CompareTo(p2.Key));
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> pair in pairs)
            {
                string atom = pair.Key;
                int count = pair.Value;
                sb.Append(atom);
                if (count > 1)
                {
                    // 數量超過1的要額外顯示在字串
                    sb.Append(count);
                }
            }

            return sb.ToString();
        }


        /// <summary>
        /// 檢測元素
        /// 
        /// 大寫字母 = 獨立元素
        /// 大寫後面接續小寫 = 元素(字串長度較長)
        /// </summary>
        /// <returns></returns>
        public static string ParseAtom(int i, int n, string formula)
        {
            StringBuilder sb = new StringBuilder();
            // 檢測首個字母
            sb.Append(formula[i++]);
            while(i < n && char.IsLower(formula[i]))
            {
                // 檢測首字母後的小寫字母
                sb.Append(formula[i++]);
            }

            return sb.ToString();
        }


        /// <summary>
        /// 檢測數量
        /// 字母後面接續數字 = 元素數量
        /// 如果字母後面沒數字 那就給數量1
        /// </summary>
        /// <returns></returns>

        public static int ParseNum(int i, int n, string formula)
        {
            if(i == n - 1 || !char.IsNumber(formula[i]))
            {
                // 不是數字, 給1
                return 1;
            }

            int num = 0;
            while (i < n && char.IsNumber(formula[i]))
            {
                // 檢測數字
                num = num * 10 + formula[i++] - '0';
            }

            return num;
        }
    }
}
