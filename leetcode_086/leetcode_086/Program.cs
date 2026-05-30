namespace leetcode_086;

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
    /// 86. Partition List
    /// https://leetcode.com/problems/partition-list/description/
    ///
    /// Given the head of a linked list and a value x, partition it such that all nodes less than x come before nodes greater than or equal to x.
    /// You should preserve the original relative order of the nodes in each of the two partitions.
    ///
    /// 86. 分隔鏈結串列
    /// https://leetcode.cn/problems/partition-list/description/
    ///
    /// 給定一個鏈結串列的頭節點 head 和一個值 x，請將鏈結串列分隔，使所有小於 x 的節點都出現在大於或等於 x 的節點之前。
    /// 你必須保留兩個分區中各節點原本的相對順序。
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
