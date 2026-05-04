namespace leetcode_002;

class Program
{

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }

    /// <summary>
    /// 2. Add Two Numbers
    /// You are given two non-empty linked lists representing two non-negative integers.
    /// The digits are stored in reverse order, and each of their nodes contains a single digit.
    /// Add the two numbers and return the sum as a linked list.
    ///
    /// You may assume the two numbers do not contain any leading zero, except the number 0 itself.
    ///
    /// 2. 兩數相加
    /// 給定兩個非空的 linked list，分別表示兩個非負整數。
    /// 數字以反向順序儲存，且每個節點只包含一個數字。
    /// 請將兩數相加，並以 linked list 形式回傳其總和。
    ///
    /// 你可以假設這兩個數字除了數字 0 本身之外，都不會有前導零。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一:
    /// 題目會給兩個 linklist 將他們相加
    /// 每個 node 指儲存一位數字 (0 ~ 9)
    /// 逆向儲存(由右至左方向)
    /// 如果 sum 超過 10 往右 node 進位
    /// 
    /// 最後輸出是 head.next
    /// 因為 head 宣告時候是 0
    /// 從下一個 node 才是 相加之後的新 node
    /// 
    /// 先判斷是否進位
    /// 再來把 node value 加總
    /// 接續就把 node 指向下個 node
    /// </summary>
    /// <param name="l1"></param>
    /// <param name="l2"></param>
    /// <returns></returns>
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        // 暫存
        ListNode l3 = new ListNode(0);
        // 輸出答案
        ListNode head = l3;
        int sum = 0;

        while(l1 != null || l2 != null)
        {
            // 判斷是否進位
            sum = sum > 9 ? 1 : 0;

            if(l1 != null)
            {
                sum += l1.val;
                l1 = l1.next;
            }

            if(l2 != null)
            {
                sum += l2.val;
                l2 = l2.next;
            }

            // 儲存在 l3 中
            l3.next = new ListNode(sum % 10);
            l3 = l3.next;
        }

        // 判斷最後一項是否和大於 9，大於則需要再添加一個 1.
        if(sum > 9)
        {
            l3.next = new ListNode(1);
        }

        return head.next;
    }


    /// <summary>
    /// 解法二
    /// 最後輸出是 resultnode.next
    /// 因為 resultnode 宣告時候是 0
    /// 從下一個 node 才是 相加之後的新 node
    /// 
    /// 本方法是把 相加 先做
    /// 再來判斷 是否進位
    /// 之後再把 node 指向 下一個 node
    /// </summary>
    /// <param name="l1"></param>
    /// <param name="l2"></param>
    /// <returns></returns>
    public ListNode AddTwoNumbers2(ListNode l1, ListNode l2)
    {
        ListNode resultnode = new ListNode(0);
        ListNode curr = resultnode;

        int carry = 0;
        while(l1 != null || l2 != null)
        {
            int d1 = l1 == null ? 0 : l1.val;
            int d2 = l2 == null ? 0 : l2.val;

            int sum = d1 + d2 + carry;
            carry = sum > 9 ? 1 : 0;

            curr.next = new ListNode(sum % 10);
            curr = curr.next;

            if(l1 != null)
            {
                l1 = l1.next;
            }

            if(l2 != null)
            {
                l2 = l2.next;
            }
        }

        if(carry == 1)
        {
            curr.next = new ListNode(1);
        }

        return resultnode.next;
    }
}
