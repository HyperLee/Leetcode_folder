# LeetCode 328. Odd Even Linked List

[![LeetCode](https://img.shields.io/badge/LeetCode-328-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/odd-even-linked-list/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-yellow?style=flat-square)](https://leetcode.com/problems/odd-even-linked-list/)
[![Language](https://img.shields.io/badge/Language-C%23-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

奇偶鏈表重排問題的 C# 解決方案。

## 題目描述

給定一個單向鏈表的頭節點 `head`，將所有位於**奇數索引**的節點聚集在一起，接著接上所有位於**偶數索引**的節點，並返回重新排序後的鏈表。

- 第一個節點視為**奇數**（索引 1）
- 第二個節點視為**偶數**（索引 2）
- 依此類推

> [!NOTE]
> 在奇數組與偶數組內，節點的相對順序應保持與輸入相同。

### 限制條件

- 時間複雜度：`O(n)`
- 空間複雜度：`O(1)`（只能使用常數額外空間）

### 範例

**範例 1：**

```text
輸入: 1 -> 2 -> 3 -> 4 -> 5
輸出: 1 -> 3 -> 5 -> 2 -> 4
```

**範例 2：**

```text
輸入: 2 -> 1 -> 3 -> 5 -> 6 -> 4 -> 7
輸出: 2 -> 3 -> 6 -> 7 -> 1 -> 5 -> 4
```

## 解題思路

### 核心概念：分離節點後合併

解題的關鍵在於將原始鏈表拆分成兩個獨立的鏈表，最後再合併：

1. **奇數鏈表**：包含所有奇數位置的節點
2. **偶數鏈表**：包含所有偶數位置的節點

### 演算法步驟

```text
原始鏈表:  1 -> 2 -> 3 -> 4 -> 5 -> null
          ↑    ↑
         odd  even (evenHead)
```

#### Step 1：初始化指標

- `evenHead = head.next`：保存偶數鏈表的頭節點
- `odd = head`：奇數指標，初始指向第 1 個節點
- `even = evenHead`：偶數指標，初始指向第 2 個節點

#### Step 2：迭代分離

重複以下操作直到 `even == null` 或 `even.next == null`：

```text
迭代 1:
odd.next = even.next  →  1 -> 3
odd = odd.next        →  odd 指向 3
even.next = odd.next  →  2 -> 4
even = even.next      →  even 指向 4

迭代 2:
odd.next = even.next  →  3 -> 5
odd = odd.next        →  odd 指向 5
even.next = odd.next  →  4 -> null
even = even.next      →  even 指向 null (結束)
```

#### Step 3：合併鏈表

```text
odd.next = evenHead  →  5 -> 2

結果: 1 -> 3 -> 5 -> 2 -> 4 -> null
```

## 完整流程演示

以 `[1, 2, 3, 4, 5]` 為例：

| 步驟 | 操作 | 奇數鏈表 | 偶數鏈表 | odd | even |
| :---: | :---- | :------- | :------- | :---: | :----: |
| 初始 | 設定指標 | 1 | 2 | 1 | 2 |
| 1 | odd.next = even.next | 1→3 | 2 | 1 | 2 |
| 2 | odd = odd.next | 1→3 | 2 | 3 | 2 |
| 3 | even.next = odd.next | 1→3 | 2→4 | 3 | 2 |
| 4 | even = even.next | 1→3 | 2→4 | 3 | 4 |
| 5 | odd.next = even.next | 1→3→5 | 2→4 | 3 | 4 |
| 6 | odd = odd.next | 1→3→5 | 2→4 | 5 | 4 |
| 7 | even.next = odd.next | 1→3→5 | 2→4→null | 5 | 4 |
| 8 | even = even.next | 1→3→5 | 2→4→null | 5 | null |
| 結束 | odd.next = evenHead | 1→3→5→2→4 | - | - | - |

## 複雜度分析

| 複雜度 | 值 | 說明 |
| :----- | :-- | :---- |
| 時間複雜度 | O(n) | 只需遍歷鏈表一次 |
| 空間複雜度 | O(1) | 只使用常數個指標變數 |

## 快速開始

### 環境需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

### 執行程式

```bash
# 複製專案
git clone https://github.com/HyperLee/Leetcode_folder.git
cd Leetcode_folder/leetcode_328

# 建構專案
dotnet build

# 執行測試
dotnet run --project leetcode_328/leetcode_328.csproj
```

### 預期輸出

```text
測試 1:
輸入: [1, 2, 3, 4, 5]
輸出: [1, 3, 5, 2, 4]

測試 2:
輸入: [2, 1, 3, 5, 6, 4, 7]
輸出: [2, 3, 6, 7, 1, 5, 4]

測試 3 (空鏈表):
輸入: []
輸出: []

測試 4 (單一節點):
輸入: [1]
輸出: [1]
```

## 專案結構

```text
leetcode_328/
├── leetcode_328.sln          # 方案檔
├── README.md                 # 說明文件
└── leetcode_328/
    ├── leetcode_328.csproj   # 專案檔
    └── Program.cs            # 主程式與解題實作
```

## 相關題目

- [LeetCode 206. Reverse Linked List](https://leetcode.com/problems/reverse-linked-list/)
- [LeetCode 86. Partition List](https://leetcode.com/problems/partition-list/)
- [LeetCode 725. Split Linked List in Parts](https://leetcode.com/problems/split-linked-list-in-parts/)
