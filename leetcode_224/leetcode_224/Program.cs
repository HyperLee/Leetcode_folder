namespace leetcode_224;

class Program
{
    /// <summary>
    /// 224. Basic Calculator
    /// https://leetcode.com/problems/basic-calculator/description/
    /// 224. 基本計算器
    /// https://leetcode.cn/problems/basic-calculator/description/
    /// 
    /// 題目描述：
    /// 給你一個字符串表達式 s ，請你實現一個基本計算器來計算並返回它的值。
    /// 注意:
    /// - 整數除法僅保留整數部分。
    /// - 輸入字符串可能包含左括號 ( 和右括號 )、加號 + 和減號 -、非負整數和空格。
    /// - 表達式中可能存在空格。
    /// 
    /// 時間複雜度：O(n)，其中 n 是字符串的長度
    /// 空間複雜度：O(n)，使用了兩個堆棧存儲數字和運算符
    /// </summary>
    static void Main(string[] args)
    {
        // 測試案例1：基本加減法
        string s1 = " 2-1 + 2 ";
        Console.WriteLine($"測試1 輸入: {s1}");
        Console.WriteLine($"結果: {Calculate(s1)}"); // 預期輸出: 3

        // 測試案例2：包含括號的表達式
        string s2 = "(1+(4+5+2)-3)+(6+8)";
        Console.WriteLine($"\n測試2 輸入: {s2}");
        Console.WriteLine($"結果: {Calculate(s2)}"); // 預期輸出: 23

        // 測試案例3：負數處理
        string s3 = "-2+ 1";
        Console.WriteLine($"\n測試3 輸入: {s3}");
        Console.WriteLine($"結果: {Calculate(s3)}"); // 預期輸出: -1

        // 測試案例4：多層括號
        string s4 = "(1-(3-4))";
        Console.WriteLine($"\n測試4 輸入: {s4}");
        Console.WriteLine($"結果: {Calculate(s4)}"); // 預期輸出: 2

        // 測試案例5：空格處理
        string s5 = " 2-1 + 2 ";
        Console.WriteLine($"\n測試5 輸入: {s5}");
        Console.WriteLine($"結果: {Calculate(s5)}"); // 預期輸出: 3
    }

    /// <summary>
    /// ref:
    /// https://leetcode.cn/problems/basic-calculator/solutions/646865/shuang-zhan-jie-jue-tong-yong-biao-da-sh-olym/
    /// https://leetcode.cn/problems/basic-calculator/solutions/1456915/by-stormsunshine-yugl/
    /// https://leetcode.cn/problems/basic-calculator/solutions/646369/ji-ben-ji-suan-qi-by-leetcode-solution-jvir/ 
    /// 基本計算器實現
    /// 解題思路：
    /// 1. 使用兩個堆棧：nums存放數字，ops存放運算符和括號
    /// 2. 遇到左括號直接入棧
    /// 3. 遇到右括號時，計算到對應的左括號為止
    /// 4. 遇到數字時，需要考慮多位數的情況
    /// 5. 遇到運算符時，需要先處理堆棧中的運算，再將新的運算符入棧
    /// </summary>
    /// <param name="s">包含數字、+、-、括號和空格的字符串</param>
    /// <returns>計算結果</returns>
    public static int Calculate(string s)
    {
        // 存放數字
        Stack<int> nums = new Stack<int>();
        // 預處理：補充0，避免第一個數字前面是運算符的情況
        nums.Push(0);
        // 預處理：去除所有空格，簡化後續處理
        s = s.Replace(" ", ""); // remove all spaces
        // 存放運算符與括號(非數字都放這邊)
        Stack<char> ops = new Stack<char>();
        int n = s.Length;
        char[] cs = s.ToCharArray();

        for(int i = 0; i < n; i++)
        {
            char c = cs[i];
            if(c == '(')
            {
                // 左括號：標記新的計算範圍的開始
                ops.Push(c);
            }
            else if (c ==')')
            {
                // 右括號：計算當前括號內的所有運算
                while(ops.Count > 0)
                {
                    char op = ops.Peek();
                    if(op != '(')
                    {
                        Calc(nums,ops);
                    }
                    else
                    {
                        // 如果是左括號，就把左括號移除並結束循環
                        ops.Pop();
                        break;
                    }
                }
            }
            else
            {
                // 遇到數字，先把數字取出來
                if(char.IsDigit(c))
                {
                    // 處理多位數字(連續數字)的情況
                    // 例如："123" 需要轉換為數字123
                    int u = 0, j = i;
                    while(j < n && char.IsDigit(cs[j]))
                    {
                        // u * 10 是位數問題(十進位)
                        u = u * 10 + (cs[j] - '0');
                        // 移動指針, 繼續處理下一位數字
                        j++;
                    }
                    nums.Push(u);
                    // 更新主迴圈的索引，避免重複處理已讀取的數字最後將轉換好的數字壓入 nums 堆疊中
                    i = j - 1;
                }
                else
                {
                    // 處理運算符前的特殊情況
                    
                    // 例如：(-1) 需要補充 0 變成 (0 - 1) ; 特殊情況處理
                    // 簡單說就是()內的第一個數字前面是運算符的情況, 如 "(" or "+" or "-"
                    if(i > 0 && (cs[i - 1] == '(' || cs[i - 1] == '+' || cs[i - 1] == '-'))
                    {
                        nums.Push(0);
                    }

                    // 運算符優先級處理。遇到運算符，先把堆棧中的運算符和數字進行計算，直到堆棧為空或者遇到左括號
                    // 例如：1 + 2 - 3，先計算 1 + 2，再計算 3 - 3
                    // 遇到運算符，先把堆棧中的運算符和數字進行計算，直到堆棧為空或者遇到左括號
                    while(ops.Count > 0 && ops.Peek() != '(')
                    {
                        Calc(nums, ops);
                    }
                    
                    // c 入棧; 
                    // 前面資料計算完畢後, 然後 當下的 c 才放入 stack
                    // 所以外層要加上一個 while 判斷計算
                    ops.Push(c); 
                }
            }
        }

        // 計算最後的結果
        while(ops.Count > 0)
        {
            Calc(nums, ops);
        }

        return nums.Peek();
    }

    /// <summary>
    /// 執行基本的加減運算
    /// 注意: 
    /// Stack（堆疊）確實是一個先進後出（LIFO, Last In First Out）的資料結構。
    /// 所以取出數字時候, 先取出來的是運算符右邊的數字, 後取出來的是運算符左邊的數字
    /// 這裡是基本的加減運算, 所以運算符的左右數字是有順序的 
    /// 例如: 1(a) + 2(b), 先取出來的是2(b), 後取出來的是1(a)
    /// 這樣才能保證運算的正確性
    /// <param name="nums">儲存數字的堆棧</param>
    /// <param name="ops">儲存運算符的堆棧</param>
    private static void Calc(Stack<int> nums, Stack<char> ops) 
    {
        // 確保有足夠的操作數和運算符
        if (nums.Count < 2 || ops.Count == 0) 
        {
            return;
        }
        // 取出兩個數字和運算符
        // 注意：後彈出的數字是運算符右邊的數字
        int b = nums.Pop(), a = nums.Pop();
        char op = ops.Pop();
        // 執行運算並將結果壓回堆棧
        nums.Push(op == '+' ? a + b : a - b);
    }
}
