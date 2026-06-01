# LeetCode 108 - 將有序陣列轉換為二元搜尋樹

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/language-C%23-239120)

這個專案是 LeetCode 108「Convert Sorted Array to Binary Search Tree」的 C# console 解法。程式會把嚴格遞增排序的整數陣列轉換成高度平衡的二元搜尋樹，並在 `Main` 中執行範例資料，輸出 level-order 結果與中序走訪驗證。

## 題目說明

給定一個整數陣列 `nums`，其中元素已按照升冪排列，請將它轉換為一棵高度平衡的二元搜尋樹。

高度平衡二元樹的定義是：對樹中每一個節點來說，左子樹與右子樹的深度差不超過 1。

題目連結：

- [LeetCode 108 - Convert Sorted Array to Binary Search Tree](https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree/description/)
- [LeetCode 中文站 - 108. 將有序陣列轉換為二元搜尋樹](https://leetcode.cn/problems/convert-sorted-array-to-binary-search-tree/description/)

## 限制條件

- `1 <= nums.length <= 10^4`
- `-10^4 <= nums[i] <= 10^4`
- `nums` 以嚴格遞增順序排序

> [!NOTE]
> LeetCode 原題保證 `nums.length >= 1`。本專案實作若收到空陣列，會回傳 `null`，讓方法在一般呼叫情境下也能安全處理沒有節點的輸入。

## 解題概念與出發點

排序陣列可以視為二元搜尋樹的中序走訪結果，因為 BST 的中序走訪順序會由小到大排列。因此，問題不是如何維持排序，而是如何在維持 BST 性質的同時，讓樹的高度盡量平衡。

直覺上，如果每次都拿陣列最左或最右的元素當根節點，樹會退化成鏈狀結構，高度變成 `O(n)`。要避免退化，應該每次選目前區間的中間元素當根節點，讓左半部建立左子樹，右半部建立右子樹。這樣左右子樹的節點數最多只差 1，可以自然形成高度平衡的 BST。

## 解法一：中點遞迴分治

目前專案實作一種解法：中點遞迴分治。

主要流程：

1. `SortedArrayToBST(int[] nums)` 接收完整排序陣列，呼叫 `Helper(nums, 0, nums.Length - 1)`。
2. `Helper` 負責建立指定索引區間 `[left, right]` 對應的子樹。
3. 如果 `left > right`，代表目前區間沒有任何元素，回傳 `null`。
4. 用 `left + ((right - left) / 2)` 取得中間索引，避免直接相加造成整數溢位。
5. 將 `nums[mid]` 建成目前子樹的根節點。
6. 遞迴使用 `[left, mid - 1]` 建立左子樹，使用 `[mid + 1, right]` 建立右子樹。
7. 回傳目前根節點。

BST 性質成立的原因：

- 原陣列已嚴格遞增排序。
- 中點左側的所有值都小於 `nums[mid]`，因此可放入左子樹。
- 中點右側的所有值都大於 `nums[mid]`，因此可放入右子樹。
- 每一層遞迴都套用同樣規則，所以整棵樹都維持 BST 性質。

高度平衡成立的原因：

- 每次都以中點切分區間。
- 左右兩邊元素數量最多只差 1。
- 子問題也用相同方式切分，因此每個節點的左右子樹高度差會被控制在平衡範圍內。

複雜度：

- 時間複雜度：`O(n)`，每個陣列元素都會建立成一個節點。
- 空間複雜度：`O(log n)` 遞迴呼叫堆疊，不含輸出樹本身；輸出樹需要 `O(n)` 節點空間。

## 範例演示流程

### 範例一

輸入：

```text
[-10, -3, 0, 5, 9]
```

建立流程：

1. 區間 `[0, 4]` 的中點是索引 `2`，值 `0` 成為根節點。
2. 左區間 `[0, 1]` 的中點是索引 `0`，值 `-10` 成為左子樹根節點。
3. `-10` 的右區間 `[1, 1]` 建立節點 `-3`。
4. 右區間 `[3, 4]` 的中點是索引 `3`，值 `5` 成為右子樹根節點。
5. `5` 的右區間 `[4, 4]` 建立節點 `9`。

輸出樹的 level-order 表示：

```text
[0, -10, 5, null, -3, null, 9]
```

中序走訪結果：

```text
[-10, -3, 0, 5, 9]
```

中序走訪與原排序陣列一致，代表 BST 排序性質正確。

### 範例二

輸入：

```text
[1, 3]
```

建立流程：

1. 區間 `[0, 1]` 的中點是索引 `0`，值 `1` 成為根節點。
2. 左區間為空，左子樹是 `null`。
3. 右區間 `[1, 1]` 建立節點 `3`。

輸出樹的 level-order 表示：

```text
[1, null, 3]
```

### 範例三

輸入：

```text
[0]
```

建立流程：

1. 區間 `[0, 0]` 的中點是索引 `0`，值 `0` 成為根節點。
2. 左右區間皆為空，因此根節點沒有子樹。

輸出樹的 level-order 表示：

```text
[0]
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_108/
    ├── Program.cs
    └── leetcode_108.csproj
```

`leetcode_108/bin/` 與 `leetcode_108/obj/` 是 .NET 建置產生的輸出目錄，不需要提交。

## 執行方式

從此資料夾執行：

```bash
dotnet build leetcode_108/leetcode_108.csproj
dotnet run --project leetcode_108/leetcode_108.csproj
```

目前 `dotnet run` 的程式標準輸出：

```text
Example 1
Input: [-10, -3, 0, 5, 9]
Level-order: [0, -10, 5, null, -3, null, 9]
In-order: [-10, -3, 0, 5, 9]

Example 2
Input: [1, 3]
Level-order: [1, null, 3]
In-order: [1, 3]

Example 3
Input: [0]
Level-order: [0]
In-order: [0]
```

> [!NOTE]
> 在目前 macOS 開發環境中，`dotnet build` 或 `dotnet run` 可能會先輸出 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。這是本機 .NET/Keychain 相關訊息，不是此 console app 的輸出；專案仍可正常建置與執行。

## 驗證指令

目前沒有獨立測試專案，可用建置、範例執行、格式檢查與 diff 檢查驗證：

```bash
dotnet build leetcode_108/leetcode_108.csproj
dotnet run --project leetcode_108/leetcode_108.csproj
dotnet format leetcode_108/leetcode_108.csproj --verify-no-changes
git diff --check
```
