namespace leetcode_150
{
    internal class Program
    {
        /// <summary>
        /// 150. Evaluate Reverse Polish Notation
        /// https://leetcode.com/problems/evaluate-reverse-polish-notation/description/?envType=daily-question&envId=2024-01-30
        /// 150. 逆波兰表达式求值
        /// https://leetcode.cn/problems/evaluate-reverse-polish-notation/description/
        /// 
        /// 本題目重點 要先看懂 表示法
        /// 才知道如何計算
        /// 
        /// 把題目給的字串按順序輸入
        /// 數字直接 push, 符號也是 push 然後把 最上方兩個數字抓出來
        /// 做運算
        /// 大致上是這樣
        /// 詳細看wiki說明
        /// 
        /// 主要是 stack 用法
        /// 
        /// 逆波蘭表示法
        /// https://zh.wikipedia.org/zh-tw/%E9%80%86%E6%B3%A2%E5%85%B0%E8%A1%A8%E7%A4%BA%E6%B3%95
        /// https://en.wikipedia.org/wiki/Reverse_Polish_notation
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "2", "1", "+", "3", "*" };
            Console.WriteLine("res: " + EvalRPN(input));
        }


        /// <summary>
        /// https://leetcode.cn/problems/evaluate-reverse-polish-notation/solutions/1456145/by-stormsunshine-cxqe/
        /// 採用方法一
        /// 比較直覺類似 wiki說明方式
        /// 
        /// 在 C# 中，Stack 是一個後進先出（LIFO, Last In, First Out）的資料結構，常用於處理需要回溯的場景，例如遞迴處理、括號配對或歷史記錄等。
        /// 後進先出： 最後加入的元素會最先被移除。
        /// 動態大小： 棧的容量會根據需要動態調整。
        /// 泛型支持： C# 提供了 Stack<T>，可以存放任意類型的元素，避免了類型轉換的問題。
        /// 非线程安全： 默认的 Stack 和 Stack<T> 不是线程安全的，在多线程環境下需要額外的同步處理。
        /// 
        /// Push： 將元素添加到棧頂。
        /// Pop： 移除並返回棧頂元素。
        /// Peek： 返回棧頂元素，但不移除它。
        /// Contains： 判斷棧中是否包含某個元素。
        /// Clear： 清空棧中的所有元素。
        /// 
        /// 棧的應用場景
        /// 函數調用： 函數的參數、返回值和局部變量通常存儲在棧中。
        /// 表達式求值： 棧可以用来實現後綴表達式求值。
        /// 回溯算法： 棧可以用来實現回溯算法，例如迷宮求解、八皇后問題等, DFS 演算法或找路徑, 深度優先搜尋（DFS），使用 Stack 來追蹤路徑。。
        /// 撤銷操作： 棧可以用来實現撤銷操作，例如文字編輯器中的撤銷功能。
        /// 平衡符號：如括號配對驗證。
        /// 暫存資料：例如實作瀏覽器的前進和後退功能。
        /// 表達式計算：在計算機科學中，Stack 常用於解析和計算數學表達式。
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static int EvalRPN(string[] tokens)
        {
            // 回傳答案是 int 型態
            Stack<int> stack = new Stack<int>();
            int length = tokens.Length;

            for (int i = 0; i < length; i++)
            {
                string token = tokens[i];

                if (IsNumber(token) == true)
                {
                    // 數字就直接 push 進入 stack
                    // 別忘記型態轉換, 初始宣告是 string 要轉 int
                    stack.Push(int.Parse(token));
                }
                else
                {
                    // 遇到符號就要運算( 將數字 pop 出來, 再進行符號運算後.在計算完畢數字 push 進入 stack )
                    // 最上方(後進入)兩個數字抓出來運算
                    // stack 先進後出(後進的會先出去), 所以宣告先大在小合理
                    int num2 = stack.Pop();
                    int num1 = stack.Pop();

                    // 注意 num1 與 num2 順序不能互換. 計算會出錯
                    switch (token)
                    {
                        case "+":
                            stack.Push(num1 + num2);
                            break;
                        case "-":
                            stack.Push(num1 - num2);
                            break;
                        case "*":
                            stack.Push(num1 * num2);
                            break;
                        case "/":
                            stack.Push(num1 / num2);
                            break;
                        default:
                            break;
                    }
                }
            }

            return stack.Pop();
        }


        /// <summary>
        /// 判斷是不是數字
        /// 在 C# 中，char.IsDigit 方法用于判断指定字符是否是一个数字。它返回一个布尔值，表示字符是否是 0 到 9 之间的数字。
        ///
        /// 輸入是 string, 要轉 char 才能判斷
        /// 所以用 陣列方式表達
        /// 陣列從 0 開始, 所以要減 1
        /// 
        /// ex:
        /// string str = "A";
        /// char c = str[0]; 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsNumber(string token)
        {
            // string 要轉 char 判斷是不是數字
            return char.IsDigit(token[token.Length - 1]);
        }
    }
}
