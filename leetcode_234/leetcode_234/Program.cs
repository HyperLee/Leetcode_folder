namespace leetcode_234;

class Program
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }


    /// <summary>
    /// 234. Palindrome Linked List
    /// https://leetcode.com/problems/palindrome-linked-list/description/
    /// 234. 回文链表
    /// https://leetcode.cn/problems/palindrome-linked-list/description/
    ///
    /// Given the head of a singly linked list, return true if it is a palindrome or false otherwise.
    ///
    /// 給定一個單向連結串列的 head，如果它是回文，則回傳 true；否則回傳 false。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    ListNode realHead;
    bool isCrossed = false;

    /// <summary>
    /// 方法一:
    /// 兩個指標 realHead 會 從頭往後 開始比較；head 會 從尾往前 開始比較
    /// head 從右往左
    /// realhead 從左往右
    /// 前後比對 直到交叉代表到中間位置, 即可停止
    /// 只要有遇到 readhead != head 代表 不一致
    /// 前後比 類似雙指針
    /// 
    /// 
    /// 這次要宣告兩個變數在 method 外面等待呼叫
    /// 1. ListNode realHead
    /// 用來從 head 往後比對的起始點
    /// 2. bool isCrossed = false;
    /// 用來判斷當比對的兩個指標交叉後，可以忽略後面的判斷，直接回傳結果，至於這是什麼意思，我晚點在效能調校時來解釋
    /// 3. 判斷 realHead 為 null，則指向 head
    /// 4. 宣告 bool result 為true
    /// 5. 如果 head.next 不為 null 時
    /// - result = result & IsPalindrome(head.next);
    /// - 直到 IsPalindrome(head.next) 判斷到 head.next 為 null 時開始回傳
    /// 此時兩個指標 realHead 會 從頭往後 開始比較；head 會 從尾往前 開始比較
    /// 只要一個遞迴裡 realHead != head，result & IsPalindrome(head.next) 永遠為 false;
    /// </summary>
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public bool IsPalindrome(ListNode head)
    {
        if(head == null)
        {
            return true;
        }

        if(realHead == null)
        {
            realHead = head;
        }

        bool result = true;

        if(head.next != null)
        {
            result = result & IsPalindrome(head.next);
        }

        if(isCrossed)
        {
            return result;
        }

        if(head == realHead || realHead.next == head)
        {
            isCrossed = true;
        }

        result = result & (head.val == realHead.val);
        realHead = realHead.next;

        return result;
    }

    /// <summary>
    /// 方法二
    /// 将值复制到数组(List)中后用双指针法
    /// 這方法 比 遞迴 好懂 就雙指針 前後比對
    /// 不同就是錯誤
    /// 
    /// 一共为两个步骤：
    /// 1. 复制链表值到数组列表中。
    /// 2. 使用双指针法判断是否为回文。
    /// 
    /// 在编码的过程中，注意我们比较的是节点值的大小，而不是节点本身。正确的比较方式
    /// 是：node_1.val == node_2.val，而 node_1 == node_2 是错误的。
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public bool IsPalindrome2(ListNode head)
    {
        List<int> vals = new List<int>();
        // 把 linklist 的數值複製至 vals 中
        ListNode currentNode = head;
        while(currentNode != null)
        {
            vals.Add(currentNode.val);
            currentNode = currentNode.next;
        }

        // 使用雙指針判斷是否是迴文
        // 左邊界, 開頭
        int front = 0;
        // 右邊界, 結尾
        int back = vals.Count - 1;

        while(front < back)
        {
            if(vals[back] != vals[front])
            {
                return false;
            }

            // 往左比對
            front++;
            // 往右比對
            back--;
        }
        return true;
    }
}
