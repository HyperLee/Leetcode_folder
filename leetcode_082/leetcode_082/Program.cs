using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_082
{
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
        /// 82. Remove Duplicates from Sorted List II
        /// https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/
        /// 82. 删除排序链表中的重复元素 II
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-list-ii/description/
        /// 
        /// 方法二比較簡潔 好理解
        /// 但是兩個方法 其實解題概念差不多
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(3);
            l1.next.next.next = new ListNode(3);
            l1.next.next.next.next = new ListNode(4);
            l1.next.next.next.next.next = new ListNode(4);
            l1.next.next.next.next.next.next = new ListNode(5);

            var res = DeleteDuplicates2(l1);
            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }

            Console.ReadKey();

        }

        /// <summary>
        /// 題目敘述, ListNode 已經經過排序了.
        /// 所以相同的 node val 必定會相鄰
        /// 所以判斷相鄰是不是 相同 val 即可
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode DeleteDuplicates(ListNode head) 
        {
            // 紀錄上一個節點
            ListNode last = null; 
            ListNode curr = head;
            // 是否有相同的節點
            bool issame = false; 

            while(curr != null && curr.next != null)
            {
                if(curr.val == curr.next.val)
                {
                    // 當前節點與下一個節點相同時, 當前節點next指向下下個節點
                    // 再次循環直到不相同為止
                    curr.next = curr.next.next;

                    // 記錄節點需要被刪除
                    issame = true; 
                }
                else
                {
                    // 節點值不同時

                    // 當前節點已經出現過相同節點, 直接替換刪除
                    if(issame)
                    {
                        curr.val = curr.next.val;
                        curr.next = curr.next.next;
                        // 紀錄節點刪除
                        issame = false; 
                    }
                    else
                    {
                        // 更新 上一個節點 val
                        last = curr;
                        // 繼續往下走
                        curr = curr.next;
                    }
                }
            }

            // 由於要等到遇到不同節點時候才替換, 有可能最後節點依舊相同. 下個節點為空就無法刪除
            if(issame)// 節點未刪除
            {
                if(last == null)
                {
                    return null;
                }

                // 上一個節點的 next 放空
                last.next = null; 
            }

            return head;

        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-list-ii/solutions/678122/shan-chu-pai-xu-lian-biao-zhong-de-zhong-oayn/
        /// 
        /// 題目敘述, ListNode 已經經過排序了.
        /// 所以相同的 node val 必定會相鄰
        /// 所以判斷相鄰是不是 相同 val 即可
        /// 
        /// 如果当前 cur.next 与 cur.next.next 对应的元素相同，那么我们就需要将 cur.next 以及所有后面拥有相同元素值的链表节点全部删除。
        /// 我们记下这个元素值 x，随后不断将 cur.next 从链表中移除，直到 cur.next 为空节点或者其元素值不等于 x 为止。
        /// 此时，我们将链表中所有元素值为 x 的节点全部删除。
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode DeleteDuplicates2(ListNode head)
        {
            if(head == null)
            {
                return head;
            }
            // 開頭插入 0, 後續接上 head
            ListNode dummy = new ListNode(0, head);
            ListNode cur = dummy;

            // 下一個與下下個不為空為停止條件
            while(cur.next != null && cur.next.next != null)
            {
                // 現在位置 node val
                int now = cur.val;

                // 下一個與下下個是否相同
                if(cur.next.val == cur.next.next.val)
                {
                    int x = cur.next.val;
                    // 持續往下找, 直到不同 node val 為止
                    while(cur.next != null && cur.next.val == x)
                    {
                        // 有相同,就要找到新的不同 val 來替換
                        // 也可以說是刪除相同 val
                        cur.next = cur.next.next;
                    }
                }
                else
                {
                    // 往下走
                    cur = cur.next;
                }
            }

            // 開頭有插入 0, 所以取 next
            return dummy.next;
        }


    }
}
