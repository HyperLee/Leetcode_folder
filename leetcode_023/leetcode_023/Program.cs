namespace leetcode_023;

class Program
{
     public class ListNode 
     {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null) 
        {
            this.val = val;
            this.next = next;
        }
    }

    /// <summary>
    /// 23. Merge k Sorted Lists
    /// https://leetcode.com/problems/merge-k-sorted-lists/description/
    /// 23. 合并 K 个升序链表
    /// https://leetcode.cn/problems/merge-k-sorts-lists/description/
    /// 
    /// 題目描述：
    /// 給你一個鏈表數組，每個鏈表都已經按升序排列
    /// 請你將所有鏈表合併到一個升序鏈表中，返回合併後的鏈表
    /// 
    /// 解題思路：
    /// 1. 使用分治法（Divide and Conquer）將問題分解為更小的子問題
    /// 2. 將 K 個鏈表分成兩半，分別處理各自的子問題
    /// 3. 使用二分法不斷將鏈表數組對半分割
    /// 4. 當分割到只剩一個或零個鏈表時，開始向上合併
    /// 5. 合併過程使用遞迴方式處理兩個已排序的鏈表
    /// 
    /// 時間複雜度：O(N log k)，其中 k 是鏈表數量，N 是所有節點總數
    /// 空間複雜度：O(log k)，因為遞迴調用的深度為 log k
    /// 
    /// 最一開始解題方法參考:21. Merge Two Sorted Lists 遞迴解法
    /// 是可以解題, 但是效率太差. 改用分治法後, 效率提升很多
    /// </summary>
    static void Main(string[] args)
    {
        // 測試案例 1: [[1,4,5],[1,3,4],[2,6]]
        ListNode[] lists1 = new ListNode[]
        {
            new ListNode(1, new ListNode(4, new ListNode(5))),
            new ListNode(1, new ListNode(3, new ListNode(4))),
            new ListNode(2, new ListNode(6))
        };
        Console.WriteLine("測試案例 1:");
        PrintList(MergeKLists(lists1));  // 預期輸出: 1->1->2->3->4->4->5->6

        // 測試案例 2: 空鏈表數組
        ListNode[] lists2 = new ListNode[] { };
        Console.WriteLine("\n測試案例 2:");
        PrintList(MergeKLists(lists2));  // 預期輸出: null

        // 測試案例 3: [[]]
        ListNode[] lists3 = new ListNode[] { null };
        Console.WriteLine("\n測試案例 3:");
        PrintList(MergeKLists(lists3));  // 預期輸出: null
    }

    /// <summary>
    /// 合併 K 個升序鏈表的解題思路：
    /// 1. 使用分治法（Divide and Conquer）
    /// 2. 將 K 個鏈表分成兩半，分別處理後再合併
    /// 3. 時間複雜度為 O(N log k)，其中 k 是鏈表數量，N 是所有節點總數
    /// 4. 空間複雜度為 O(log k)，因為遞迴調用的深度為 log k
    /// </summary>
    public static ListNode MergeKLists(ListNode[] lists)
    {
        if (lists == null || lists.Length == 0)
            return null;
            
        return MergeSort(lists, 0, lists.Length - 1);
    }

    /// <summary>
    /// 使用分治法合併鏈表：
    /// 基本概念：將大問題分解成小問題，再將小問題的解合併
    /// 
    /// 舉例：合併 [1->4->5], [1->3->4], [2->6] 三個鏈表
    /// 第一層遞迴：將陣列分成 ([1->4->5], [1->3->4]) 和 ([2->6])
    /// 第二層遞迴：將左半部再分成 ([1->4->5]) 和 ([1->3->4])
    /// 然後逐層合併回來
    /// 
    /// 注意:這邊的 left 和 right 是索引，而不是節點
    /// 二分法,分的是輸入的 lists 陣列,把整個輸入陣列分成兩半
    /// 給定鏈表陣列：[1->4->5, 1->3->4, 2->6] 索引位置： [   0   ,    1   ,   2  ]
    /// 
    /// 第一次分割：left = 0, right = 2，mid = 1
    /// 左半部：[1->4->5, 1->3->4]   右半部：[2->6]
    /// 第二次分割（左半部）：left = 0, right = 1，mid = 0
    /// 左半部：[1->4->5]  右半部：[1->3->4]
    /// </summary>
    /// <param name="lists">要處理的鏈表陣列</param>
    /// <param name="left">當前處理的左邊界索引</param>
    /// <param name="right">當前處理的右邊界索引</param>
    /// <returns>合併後的鏈表</returns>
    private static ListNode MergeSort(ListNode[] lists, int left, int right)
    {
        // 步驟 1: 處理邊界條件
        // 當 left == right 時，表示只剩一個鏈表，直接返回
        // 這個判斷是分治法中的重要基礎情況（base case），表示已經分割到最小單位，不需要再進行分割，直接返回該位置的鏈表。
        if (left == right)
        {
            return lists[left];
        }
        // 當 left > right 時，表示無效的範圍，返回 null
        // 左邊不會比右邊大, 所以不會出現 left > right 的情況. 這是無效區間的情況
        if (left > right)
        {
            return null;
        }
            
        // 步驟 2: 計算中間點
        // 使用 (right - left) / 2 避免整數溢位
        int mid = left + (right - left) / 2;
        
        // 步驟 3: 遞迴處理左半部
        // 範圍是 [left, mid]
        ListNode leftList = MergeSort(lists, left, mid);
        
        // 步驟 4: 遞迴處理右半部
        // 範圍是 [mid+1, right]
        ListNode rightList = MergeSort(lists, mid + 1, right);
        
        // 步驟 5: 合併左右兩個已排序的鏈表
        // 使用 merge2Lists 函數將兩個排序好的鏈表合併
        return merge2Lists(leftList, rightList);
    }

    /// <summary>
    /// 合併兩個已排序鏈表的輔助函數：
    /// 1. 採用遞迴方式實作
    /// 2. 比較兩個鏈表當前節點的值，選擇較小的節點
    /// 3. 遞迴處理剩餘部分
    /// 4. base case: 當其中一個鏈表為空時，直接返回另一個鏈表
    /// 
    /// 解法來源參考題目 21. Merge Two Sorted Lists 遞迴解法
    /// </summary>
    /// <param name="l1">第一個已排序鏈表</param>
    /// <param name="l2">第二個已排序鏈表</param>
    /// <returns>合併後的有序鏈表</returns>
    private static ListNode merge2Lists(ListNode l1, ListNode l2) 
    {
        // 若其中一個鏈表為空，返回另一個鏈表
        if (l1 == null) 
        {
            return l2;
        }
        if (l2 == null) 
        {
            return l1;
        }
        
        // 比較當前節點值，選擇較小的節點，並遞迴處理剩餘部分
        if (l1.val < l2.val) 
        {
            // l1 較小，將 l1.next 與 l2 合併的結果接在 l1 後面
            l1.next = merge2Lists(l1.next, l2);
            return l1;
        }
        else
        {
            // l2 較小或相等，將 l1 與 l2.next 合併的結果接在 l2 後面
            l2.next = merge2Lists(l1, l2.next);
            return l2;
        }
    }

    /// <summary>
    /// 輔助方法：打印鏈表內容
    /// </summary>
    private static void PrintList(ListNode head)
    {
        if (head == null)
        {
            Console.WriteLine("null");
            return;
        }

        while (head != null)
        {
            Console.Write(head.val);
            if (head.next != null)
                Console.Write("->");
            head = head.next;
        }
        Console.WriteLine();
    }
}
