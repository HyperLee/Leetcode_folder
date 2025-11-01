using System;
using System.Collections.Generic;

namespace leetcode_3217;

class Program
{
    /// <summary>
    /// Definition for singly-linked list.
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode? next;
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    /// <summary>
    /// 3217. Delete Nodes From Linked List Present in Array
    /// 3217. 從鍊錶移除在陣列中存在的節點
    ///
    /// 方法一：哈希表 + 哨兵節點 (dummy node)
    /// 解題說明：
    /// 1. 將陣列 `nums` 中的元素放入一個雜湊集合 (HashSet) 中，以便 O(1) 時間判斷某個值是否需要被移除。
    /// 2. 因為鍊錶的頭節點可能會被移除，為了簡化邊界處理，我們在原鍊錶頭之前建立一個「哨兵節點 (dummy node)」，
    ///    並讓 dummy.next 指向原本的頭節點。
    /// 3. 使用一個指標 `current` 從 dummy 開始遍歷。每次檢查 `current.next` 的值是否存在於集合中：
    ///    - 若存在，表示該節點需要刪除，則把 `current.next` 指向 `current.next.next`，相當於跳過該節點。
    ///    - 若不存在，則把 `current` 往前移動一格 (current = current.next)。
    /// 4. 重複直到遍歷完鍊錶。最後回傳 `dummy.next` 作為新的頭節點。
    ///
    /// 為什麼使用哨兵 (dummy) node？
    /// - 哨兵節點是一個位於鍊錶頭之前的暫存節點，它能統一頭節點與其他節點的刪除邏輯，避免對頭節點被刪除時需要額外處理。
    /// - 使用 dummy 可以讓程式碼更簡潔且不易出錯，尤其在需要改變頭節點的場景下非常有用。
    ///
    /// 複雜度：
    /// - 時間複雜度 O(n + m)，其中 n 為鍊錶長度，m 為陣列 nums 的長度（用於建立 HashSet）。
    /// - 空間複雜度 O(m)，用於 HashSet。
    /// </summary>
    /// <param name="nums">要移除的節點值陣列</param>
    /// <param name="head">鍊錶頭</param>
    /// <returns>移除後的鍊錶頭</returns>
    public static ListNode? ModifiedList(int[] nums, ListNode? head)
    {
        HashSet<int> isExist = new HashSet<int>(nums);
        // 建立哨兵節點並連到 head，統一刪除邊界處理
        ListNode dummy = new ListNode(0, head);
        ListNode current = dummy;
        while (current.next != null)
        {
            if (isExist.Contains(current.next.val))
            {
                // 跳過需要刪除的節點
                current.next = current.next.next;
            }
            else
            {
                current = current.next;
            }
        }
        return dummy.next;
    }

    /// <summary>
    /// 建立鍊錶的輔助方法，從陣列建立鏈結串列
    /// </summary>
    public static ListNode? BuildList(int[] vals)
    {
        ListNode dummy = new ListNode(0);
        ListNode cur = dummy;
        foreach (var v in vals)
        {
            cur.next = new ListNode(v);
            cur = cur.next;
        }
        return dummy.next;
    }

    /// <summary>
    /// 印出鍊錶內容的輔助方法
    /// </summary>
    public static void PrintList(ListNode? head)
    {
        List<string> parts = new List<string>();
        while (head != null)
        {
            parts.Add(head.val.ToString());
            head = head.next;
        }
        Console.WriteLine(string.Join(" -> ", parts));
    }

    /// <summary>
    /// 3217. Delete Nodes From Linked List Present in Array
    /// 3217. 從鍊錶移除在陣列中存在的節點
    ///
    /// Problem (English):
    /// You are given an array of integers nums and the head of a linked list. 
    /// Return the head of the modified linked list after removing all nodes from the linked list that have a value that exists in nums.
    ///
    /// 題目（中文翻譯）：
    /// 給定一個整數陣列 nums 與一個鍊錶的頭節點 head，請移除鍊錶中所有節點，其節點值存在於 nums 中，並回傳移除後的鍊錶頭節點。
    ///
    /// Links:
    /// https://leetcode.com/problems/delete-nodes-from-linked-list-present-in-array/description/?envType=daily-question&envId=2025-11-01
    /// https://leetcode.cn/problems/delete-nodes-from-linked-list-present-in-array/description/?envType=daily-question&envId=2025-11-01
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例 1
        int[] nums = new int[] { 2, 4 };
        ListNode? head = BuildList(new int[] { 1, 2, 3, 4 });
        Console.WriteLine("Original list:");
        PrintList(head);

        ListNode? result = ModifiedList(nums, head);
        Console.WriteLine("After removing nodes present in nums:");
        PrintList(result);

        // 你可以在此新增更多測試案例，程式執行時無需手動輸入名稱。
    }
}
