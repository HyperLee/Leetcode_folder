# Leetcode 440: 字典序的第K小數字

## 題目說明
給定兩個整數 n 和 k，返回範圍 [1, n] 中字典序第 k 小的整數。

- LeetCode 英文題目連結：[K-th Smallest in Lexicographical Order](https://leetcode.com/problems/k-th-smallest-in-lexicographical-order/description/?envType=daily-question&envId=2025-06-09)
- LeetCode 中文題目連結：[字典序的第K小数字](https://leetcode.cn/problems/k-th-smallest-in-lexicographical-order/description/?envType=daily-question&envId=2025-06-09)

## 解題思路
本題若直接將所有數字轉成字串排序，效率會很低。最佳解法是利用**字典樹（Trie）**的結構特性，將所有數字視為一棵十叉樹（每個節點有 0~9 個子節點），根據字典序進行遍歷。

### 字典樹說明
- 每個節點代表一個數字。
- 例如節點 1 的下一層是 10, 11, 12, ..., 19。
- 這種結構可以高效地計算以某個數字為開頭的所有數字個數。

### 演算法流程
1. 從 1 開始，因為字典序第一個數字是 1。
2. 每次計算以當前數字為根的子樹（所有以 curr 為開頭的數字）在 [1, n] 範圍內有多少個數字。
3. 如果這個數量小於等於 k，代表第 k 小的數字不在這棵子樹裡，移動到同層下一個節點（curr++），並將 k 減去這棵子樹的節點數。
4. 否則，代表第 k 小的數字在這棵子樹裡，往下進入子節點（curr *= 10），k 減 1。
5. 重複步驟 2~4，直到找到第 k 小的數字。

### 主要程式碼片段
```csharp
public int FindKthNumber(int n, int k)
{
    int curr = 1;
    k--;
    while (k > 0)
    {
        int steps = GetSteps(curr, n);
        if (steps <= k)
        {
            curr++;
            k -= steps;
        }
        else
        {
            curr *= 10;
            k--;
        }
    }
    return curr;
}

public int GetSteps(int curr, int n)
{
    int steps = 0;
    long first = curr, last = curr + 1;
    while (first <= n)
    {
        steps += (int)Math.Min(n + 1, last) - (int)first;
        first *= 10;
        last *= 10;
    }
    return steps;
}
```

## 複雜度分析
- 時間複雜度：O(log n * log n)
- 空間複雜度：O(1)

## 測試範例
```csharp
Program p = new Program();
Console.WriteLine(p.FindKthNumber(13, 2)); // 輸出: 10
Console.WriteLine(p.FindKthNumber(100, 10)); // 輸出: 17
Console.WriteLine(p.FindKthNumber(1000, 100)); // 輸出: 117
```

---

## 字典樹（Trie）補充說明
字典樹是一種多叉樹結構，常用於字串檢索。這題將每個數字視為字串，利用字典樹的層級結構，可以高效地計算以某個數字為開頭的所有數字個數，進而快速定位到第 k 小的字典序數字。

## 字典樹（Trie）階層圖示範例

假設 n = 23，則字典樹的部分結構如下：

```
根
├─ 1
│  ├─ 10
│  ├─ 11
│  ├─ 12
│  ├─ 13
│  ├─ 14
│  ├─ 15
│  ├─ 16
│  ├─ 17
│  ├─ 18
│  ├─ 19
│  └─ 1x ...
├─ 2
│  ├─ 20
│  ├─ 21
│  ├─ 22
│  └─ 23
└─ 3 ...
```

- 每個節點代表一個數字，子節點為其後再加一位數。
- 例如節點 1 的下一層有 10~19，節點 2 的下一層有 20~23。
- 這種結構可以高效地遍歷所有數字，並根據字典序快速定位第 k 小的數字。

---
