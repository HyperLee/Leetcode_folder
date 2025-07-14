namespace leetcode_1290;

class Program
{
    /// <summary>
    /// 1290. Convert Binary Number in a Linked List to Integer
    /// https://leetcode.com/problems/convert-binary-number-in-a-linked-list-to-integer/description/?envType=daily-question&envId=2025-07-14
    /// 
    /// 1290. 二進位鍊錶轉整數
    /// https://leetcode.cn/problems/convert-binary-number-in-a-linked-list-to-integer/description/?envType=daily-question&envId=2025-07-14
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 建立測試鍊錶：1 → 1 → 0
        ListNode head = new ListNode(1, new ListNode(1, new ListNode(0)));
        // 呼叫 GetDecimalValue 並取得十進位結果
        int result = new Program().GetDecimalValue(head);
        // 輸出結果到主控台
        Console.WriteLine($"鍊錶 1→1→0 對應的十進位整數為: {result}");
    }

    public class ListNode
    {
        public int val;
        public ListNode? next;
        public ListNode(int val=0, ListNode? next=null)
        {
            this.val = val;
            this.next = next;
        }
    }


    /// <summary>
    /// 將二進位鍊錶轉換為十進位整數。
    /// 解題思路：
    /// 不需要預先知道鍊錶長度。每次讀取一個節點值時，將目前累積的結果 res 乘以 2（等同於左移一位），
    /// 再加上新節點的值，這樣新值就成為最低位。重複此步驟直到鍊錶結束。
    /// 例如：鍊錶 1→1→0，計算過程如下：
    /// 0×2+1=1，1×2+1=3，3×2+0=6。
    /// <example>
    /// <code>
    /// ListNode head = new ListNode(1, new ListNode(1, new ListNode(0)));
    /// int result = GetDecimalValue(head); // result = 6
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="head">鍊錶的頭節點，代表二進位數的最高位</param>
    /// <returns>轉換後的十進位整數</returns>
    public int GetDecimalValue(ListNode head)
    {
        // currNode 用於遍歷鍊錶
        ListNode? currNode = head;
        // res 用於累積計算結果，初始為 0
        int res = 0;
        // 逐步遍歷每個節點
        while (currNode != null)
        {
            // 將目前結果 res 左移一位（乘以 2），再加上當前節點值
            res = res * 2 + currNode.val;
            // 移動到下一個節點
            currNode = currNode.next;
        }
        // 回傳最終計算結果
        return res;
    }
}
