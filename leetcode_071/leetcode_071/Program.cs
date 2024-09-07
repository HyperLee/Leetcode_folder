using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_071
{
    class Program
    {
        /// <summary>
        /// 71. Simplify Path
        /// https://leetcode.com/problems/simplify-path/
        /// 71. 简化路径
        /// https://leetcode-cn.com/problems/simplify-path/
        /// 
        /// https://blog.csdn.net/qq_39643935/article/details/78241171
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string aa = "";
            aa = "/.../a/../b/c/../d/./";

            Console.Write("res: " + SimplifyPath(aa));
            Console.ReadKey();

        }


        /// <summary>
        /// 一个点 '.' 表示当前目录本身。
        /// 此外，两个点 '..' 表示将目录切换到上一级（指向父目录）。 => pass(剔除最後加入的 folder)該層目錄, 返回上一層
        /// 任意多个连续的斜杠（即，'//' 或 '///'）都被视为单个斜杠 '/'。
        /// 任何其他格式的点（例如，'...' 或 '....'）均被视为有效的文件/目录名称。
        /// 
        /// 返回的 简化路径 必须遵循下述格式：
        /// 始终以斜杠 '/' 开头。
        /// 两个目录名之间必须只有一个斜杠 '/' 。
        /// 最后一个目录名（如果存在）不能 以 '/' 结尾。
        /// 此外，路径仅包含从根目录到目标文件或目录的路径上的目录（即，不含 '.' 或 '..'）。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string SimplifyPath(string path)
        {
            // 字串切割區分
            string[] arr = path.Split('/');
            Stack<string> _stack = new Stack<string>();

            // 長度是切割後的 arr.Length
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != "")
                {
                    if (arr[i] == "..")
                    {
                        // 要有資料夾才能 Pop
                        if (_stack.Count > 0)
                        {
                            // 遇到 /../ => 跳回上一層
                            // pop 最後一個資料夾
                            _stack.Pop();
                        }
                    }
                    else if (arr[i] == ".")
                    {
                        // . 代表當前目錄, 故不動作
                    }
                    else
                    {
                        // 資料夾名稱加入 _stack
                        _stack.Push(arr[i]);
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            while (_stack.Count > 0)
            {
                // 每個資料夾名稱之間插入 /
                sb.Insert(0, "/" + _stack.Pop());
            }


            // solve corner case like "/../"
            if (sb.Length == 0)
            {
                sb.Append("/");
            }

            return sb.ToString();

        }


    }
}
