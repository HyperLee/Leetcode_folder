namespace leetcode_232
{
    internal class Program
    {
        /// <summary>
        /// 題目描述：
        /// 使用兩個堆疊（Stack）來實作一個佇列（Queue），需支援 push(x)、pop()、peek()、empty() 四種操作。
        /// 你只能使用標準的堆疊操作（push、pop、peek、empty）。
        /// 
        /// 解題概念與出發點：
        /// 1. 使用兩個堆疊 inStack 與 outStack。
        /// 2. push 時將元素推入 inStack。
        /// 3. pop/peek 時，若 outStack 為空，將 inStack 所有元素依序彈出並推入 outStack，這樣 outStack 的頂端即為佇列開頭。
        /// 4. empty 則判斷兩個堆疊皆為空。
        /// 這樣可確保所有操作皆符合佇列（先進先出）特性。
        /// 
        /// 232. Implement Queue using Stacks
        /// https://leetcode.com/problems/implement-queue-using-stacks/description/
        /// 232. 用栈实现队列
        /// https://leetcode.cn/problems/implement-queue-using-stacks/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 建立兩種佇列實作並執行相同的操作序列，確認它們都符合先進先出規則。
        /// 測試資料只會在佇列非空時呼叫 Pop 或 Peek，最後輸出全部檢查的通過數。
        /// </summary>
        private static void RunSamples()
        {
            int passed = 0;
            int total = 0;

            MyQueue queue = new MyQueue();
            RunSolution(
                "解法一：延遲搬移",
                queue.Push,
                queue.Pop,
                queue.Peek,
                queue.Empty,
                ref passed,
                ref total);

            MyQueue2 queue2 = new MyQueue2();
            RunSolution(
                "解法二：Push 時重排",
                queue2.Push,
                queue2.Pop,
                queue2.Peek,
                queue2.Empty,
                ref passed,
                ref total);

            Console.WriteLine($"總結：{passed}/{total} 項驗證通過");

            if (passed != total)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 對指定佇列實作執行固定的狀態操作，驗證初始空佇列、FIFO、
        /// 搬移後新增資料、完全清空，以及清空後重用等行為。
        /// </summary>
        /// <param name="solutionName">輸出時顯示的解法名稱。</param>
        /// <param name="push">將整數加入佇列尾端的操作。</param>
        /// <param name="pop">移除並回傳佇列前端元素的操作；呼叫時佇列保證非空。</param>
        /// <param name="peek">回傳佇列前端元素的操作；呼叫時佇列保證非空。</param>
        /// <param name="empty">判斷佇列是否為空的操作。</param>
        /// <param name="passed">累計通過的檢查數量。</param>
        /// <param name="total">累計執行的檢查數量。</param>
        private static void RunSolution(
            string solutionName,
            Action<int> push,
            Func<int> pop,
            Func<int> peek,
            Func<bool> empty,
            ref int passed,
            ref int total)
        {
            Console.WriteLine(solutionName);

            RecordCheck("初始 Empty()", true, empty(), ref passed, ref total);

            push(1);
            push(2);
            RecordCheck("Push(1), Push(2), Peek()", 1, peek(), ref passed, ref total);
            RecordCheck("Pop()", 1, pop(), ref passed, ref total);
            RecordCheck("剩餘元素時 Empty()", false, empty(), ref passed, ref total);

            push(3);
            RecordCheck("Pop 後 Push(3), Peek()", 2, peek(), ref passed, ref total);
            RecordCheck("Pop() 取得舊資料", 2, pop(), ref passed, ref total);
            RecordCheck("Pop() 取得新資料", 3, pop(), ref passed, ref total);
            RecordCheck("完全取出後 Empty()", true, empty(), ref passed, ref total);

            push(9);
            RecordCheck("重用後 Peek()", 9, peek(), ref passed, ref total);
            RecordCheck("重用後 Pop()", 9, pop(), ref passed, ref total);
            RecordCheck("重用並清空後 Empty()", true, empty(), ref passed, ref total);

            Console.WriteLine();
        }

        /// <summary>
        /// 比對單一操作的預期值與實際值，輸出 PASS 或 FAIL，
        /// 並更新總檢查數與通過數；支援可由預設相等比較器判斷的型別。
        /// </summary>
        /// <typeparam name="T">預期值與實際值的共同型別。</typeparam>
        /// <param name="operation">本次檢查所代表的操作描述。</param>
        /// <param name="expected">預期結果。</param>
        /// <param name="actual">實際結果。</param>
        /// <param name="passed">累計通過的檢查數量。</param>
        /// <param name="total">累計執行的檢查數量。</param>
        private static void RecordCheck<T>(
            string operation,
            T expected,
            T actual,
            ref int passed,
            ref int total)
        {
            bool isPassed = EqualityComparer<T>.Default.Equals(expected, actual);
            total++;

            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine(
                $"  {operation} | Expected: {expected} | Actual: {actual} | {(isPassed ? "PASS" : "FAIL")}");
        }
    }


    /// <summary>
    /// 232. Implement Queue using Stacks
    /// https://leetcode.com/problems/implement-queue-using-stacks/description/
    /// 232. 用栈实现队列
    /// https://leetcode.cn/problems/implement-queue-using-stacks/description/
    /// 
    /// ref:
    /// https://leetcode.cn/problems/implement-queue-using-stacks/solution/yong-zhan-shi-xian-dui-lie-by-leetcode-s-xnb6/
    /// 當一個棧作為輸入棧，用於壓入 push 傳入的數據；另一個棧作為輸出棧，用於 pop 和 peek 操作。 
    /// 每次 pop 或 peek 時，若輸出棧為空則將輸入棧的全部數據依次彈出並壓入輸出棧，這樣輸出棧從棧頂往棧底的順序就是隊列從隊首往隊尾的順序。
    /// 
    /// </summary>
    public class MyQueue
    {
        /// <summary>
        /// 保存尚未搬到輸出端的新元素；堆疊頂端是最新加入的元素。
        /// </summary>
        private readonly Stack<int> inStack;

        /// <summary>
        /// 保存已反轉順序、可直接讀取的元素；堆疊頂端是目前隊首。
        /// </summary>
        private readonly Stack<int> outStack;

        /// <summary>
        /// 建立不含任何元素的雙堆疊佇列，初始化彼此獨立的輸入與輸出堆疊。
        /// 建構完成後 Empty 會回傳 true。
        /// </summary>
        public MyQueue()
        {
            inStack = new Stack<int>();
            outStack = new Stack<int>();
        }

        /// <summary>
        /// 將整數加入佇列尾端。此解法先把輸入保留在 inStack，
        /// 等到需要讀取隊首時才搬移；輸入值須符合題目限制，方法不回傳結果。
        /// </summary>
        /// <param name="x">要加入佇列尾端的整數。</param>
        public void Push(int x)
        {
            inStack.Push(x);
        }

        /// <summary>
        /// 移除並回傳佇列前端元素。若 outStack 為空，先透過 In2Out
        /// 一次反轉所有待處理元素；呼叫前佇列必須非空，輸出為最早加入且尚未移除的值。
        /// </summary>
        /// <returns>原本位於佇列前端的整數。</returns>
        public int Pop()
        {
            if (outStack.Count == 0)
            {
                // 只有舊的輸出資料耗盡後才搬移，避免新元素插到既有隊首之前。
                In2Out();
            }

            return outStack.Pop();
        }

        /// <summary>
        /// 回傳但不移除佇列前端元素。若 outStack 為空，先透過 In2Out
        /// 建立正確的 FIFO 順序；呼叫前佇列必須非空，佇列內容不會改變。
        /// </summary>
        /// <returns>目前位於佇列前端的整數。</returns>
        public int Peek()
        {
            if (outStack.Count == 0)
            {
                // Peek 與 Pop 共用相同的延遲搬移條件，確保兩者看到同一個隊首。
                In2Out();
            }

            return outStack.Peek();
        }

        /// <summary>
        /// 判斷輸入與輸出堆疊是否都沒有元素。
        /// 此方法沒有輸入；兩個堆疊皆空時回傳 true，否則回傳 false。
        /// </summary>
        /// <returns>佇列沒有元素時為 true，否則為 false。</returns>
        public bool Empty()
        {
            return inStack.Count == 0 && outStack.Count == 0;
        }

        /// <summary>
        /// 將 inStack 的所有元素逐一搬到空的 outStack。
        /// 搬移會反轉後進先出的順序，使最早加入的元素位於 outStack 頂端；
        /// 呼叫前 outStack 必須為空，方法完成後 inStack 會被清空且不回傳結果。
        /// </summary>
        private void In2Out()
        {
            while (inStack.Count > 0)
            {
                // 全部搬移一次即可反轉順序，讓最早加入的元素成為 outStack 頂端。
                outStack.Push(inStack.Pop());
            }
        }
    }

    /// <summary>
    /// 使用主堆疊與暫存堆疊實作先進先出佇列，並在每次 Push 時立即重排。
    /// 重排後主堆疊頂端永遠是隊首，因此 Pop 與 Peek 可直接在 O(1) 時間完成；
    /// Push 需要搬移既有元素，時間複雜度為 O(n)。
    /// 輸入值須符合題目限制，Pop 與 Peek 必須在佇列非空時呼叫；
    /// 各操作結果與一般 FIFO 佇列相同。
    /// </summary>
    public class MyQueue2
    {
        /// <summary>
        /// 保存完成重排後的所有元素；堆疊頂端固定為目前隊首。
        /// </summary>
        private readonly Stack<int> mainStack;

        /// <summary>
        /// Push 重排期間暫存既有元素；每次公開操作完成後皆為空。
        /// </summary>
        private readonly Stack<int> tempStack;

        /// <summary>
        /// 建立不含任何元素的 Push 重排佇列，初始化主堆疊與暫存堆疊。
        /// 建構完成後 Empty 會回傳 true。
        /// </summary>
        public MyQueue2()
        {
            mainStack = new Stack<int>();
            tempStack = new Stack<int>();
        }

        /// <summary>
        /// 將整數加入佇列尾端。方法先移開全部既有元素，再把新值放到主堆疊底部，
        /// 最後還原既有元素，使原隊首仍位於頂端；輸入值須符合題目限制，方法不回傳結果。
        /// </summary>
        /// <param name="x">要加入佇列尾端的整數。</param>
        public void Push(int x)
        {
            while (mainStack.Count > 0)
            {
                tempStack.Push(mainStack.Pop());
            }

            // 新值先進入空的主堆疊，還原舊元素後便會固定在整個佇列尾端。
            mainStack.Push(x);

            while (tempStack.Count > 0)
            {
                mainStack.Push(tempStack.Pop());
            }
        }

        /// <summary>
        /// 直接從主堆疊頂端移除並回傳佇列前端元素。
        /// 呼叫前佇列必須非空，輸出為最早加入且尚未移除的值。
        /// </summary>
        /// <returns>原本位於佇列前端的整數。</returns>
        public int Pop()
        {
            return mainStack.Pop();
        }

        /// <summary>
        /// 直接讀取但不移除主堆疊頂端的隊首元素。
        /// 呼叫前佇列必須非空，輸出為目前隊首且佇列內容不會改變。
        /// </summary>
        /// <returns>目前位於佇列前端的整數。</returns>
        public int Peek()
        {
            return mainStack.Peek();
        }

        /// <summary>
        /// 判斷主堆疊是否沒有元素。
        /// 此方法沒有輸入；佇列為空時回傳 true，否則回傳 false。
        /// </summary>
        /// <returns>佇列沒有元素時為 true，否則為 false。</returns>
        public bool Empty()
        {
            return mainStack.Count == 0;
        }
    }
}
