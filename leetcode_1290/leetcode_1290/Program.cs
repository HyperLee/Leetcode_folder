namespace leetcode_1290;

class Program
{
    /// <summary>
    /// <para>
    /// 1290. Convert Binary Number in a Linked List to Integer
    /// https://leetcode.com/problems/convert-binary-number-in-a-linked-list-to-integer/description/
    ///
    /// Given head which is a reference node to a singly-linked list. The value of each node in the linked list is either 0 or 1.
    /// The linked list holds the binary representation of a number.
    ///
    /// Return the decimal value of the number in the linked list.
    ///
    /// The most significant bit is at the head of the linked list.
    ///
    /// Official illustration: https://assets.leetcode.com/uploads/2019/12/05/graph-1.png
    ///
    /// Example 1:
    /// Input: head = [1,0,1]
    /// Output: 5
    /// Explanation: (101) in base 2 = (5) in base 10
    ///
    /// Example 2:
    /// Input: head = [0]
    /// Output: 0
    ///
    /// Constraints:
    /// - The Linked List is not empty.
    /// - Number of nodes will not exceed 30.
    /// - Each node's value is either 0 or 1.
    /// </para>
    /// <para>
    /// 1290. 將二進位鏈結串列轉換為整數
    /// https://leetcode.cn/problems/convert-binary-number-in-a-linked-list-to-integer/description/
    ///
    /// 給定 head，它是單向鏈結串列的參考節點。鏈結串列中每個節點的值不是 0 就是 1。
    /// 此鏈結串列保存一個數字的二進位表示。
    ///
    /// 回傳鏈結串列中該數字的十進位值。
    ///
    /// 最高有效位元位於鏈結串列的開頭。
    ///
    /// 官方示意圖：https://assets.leetcode.com/uploads/2019/12/05/graph-1.png
    ///
    /// 範例 1：
    /// 輸入：head = [1,0,1]
    /// 輸出：5
    /// 解釋：以 2 為底的 (101) = 以 10 為底的 (5)
    ///
    /// 範例 2：
    /// 輸入：head = [0]
    /// 輸出：0
    ///
    /// 限制條件：
    /// - 鏈結串列不為空。
    /// - 節點數量不會超過 30。
    /// - 每個節點的值不是 0 就是 1。
    /// </para>
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
