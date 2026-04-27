# LeetCode 938 — Range Sum of BST

**難度**：Easy  
**題目連結**：[LeetCode EN](https://leetcode.com/problems/range-sum-of-bst/) ／ [LeetCode CN](https://leetcode.cn/problems/range-sum-of-bst/description/)

---

## 題目說明

給定一棵二元搜尋樹（BST）的根節點 `root`，以及兩個整數 `low` 和 `high`，  
回傳所有節點值位於**包含區間 `[low, high]`** 內的節點值總和。

**約束條件**

- 樹中節點數量介於 `[1, 2 * 10^4]`
- `1 <= Node.val <= 10^5`
- `1 <= low <= high <= 10^5`
- 所有節點值**不重複**

---

## 解題概念與出發點

### BST 的核心性質

> 對任意節點 `N`：
> - 左子樹中所有節點的值 **< N.val**
> - 右子樹中所有節點的值 **> N.val**

利用這個性質，在搜尋過程中可以**提前排除整個子樹**，避免不必要的走訪，  
相較於暴力走訪所有節點，可大幅降低實際執行次數。

---

## 解法：深度優先搜尋（DFS）搭配剪枝

遞迴走訪 BST，每次依據當前節點值與 `[low, high]` 的關係決定走向：

| 條件 | 動作 | 理由 |
|------|------|------|
| `root == null` | 回傳 `0` | 到達空節點，無值可加 |
| `root.val > high` | 只遞迴**左子樹** | 右子樹所有值均 `> high`，可剪枝 |
| `root.val < low` | 只遞迴**右子樹** | 左子樹所有值均 `< low`，可剪枝 |
| `low <= root.val <= high` | `root.val` ＋ 左右子樹結果 | 當前節點在範圍內，累加並繼續兩側搜尋 |

```csharp
public int RangeSumBST(TreeNode root, int low, int high)
{
    if (root == null) return 0;

    if (root.val > high)
        return RangeSumBST(root.left, low, high);

    if (root.val < low)
        return RangeSumBST(root.right, low, high);

    return root.val
        + RangeSumBST(root.left, low, high)
        + RangeSumBST(root.right, low, high);
}
```

**時間複雜度**：$O(N)$（最壞情況走訪所有節點）  
**空間複雜度**：$O(H)$（遞迴堆疊深度，$H$ 為樹高）

---

## 演示流程

### 範例 1

```
       10
      /  \
     5    15
    / \     \
   3   7    18
```

`low = 7`，`high = 15`

| 步驟 | 節點 | 動作 |
|------|------|------|
| 1 | `10` | `7 ≤ 10 ≤ 15` → 累加 10，往兩側走 |
| 2 | `5`  | `5 < 7` → 剪左，只往右子樹（`7`）走 |
| 3 | `7`  | `7 ≤ 7 ≤ 15` → 累加 7，左右子樹皆 null |
| 4 | `15` | `7 ≤ 15 ≤ 15` → 累加 15，左子樹 null；右子樹往 `18` |
| 5 | `18` | `18 > 15` → 剪右，往左子樹（null）走，回傳 0 |

**結果**：`10 + 7 + 15 = 32` ✓

---

### 範例 2

```
          10
         /  \
        5    15
       / \   / \
      3   7 13  18
     /   /
    1   6
```

`low = 6`，`high = 10`

| 步驟 | 節點 | 動作 |
|------|------|------|
| 1 | `10` | `6 ≤ 10 ≤ 10` → 累加 10，往兩側走 |
| 2 | `5`  | `5 < 6` → 剪左，只往右子樹（`7`）走 |
| 3 | `7`  | `6 ≤ 7 ≤ 10` → 累加 7，往左子樹（`6`）走 |
| 4 | `6`  | `6 ≤ 6 ≤ 10` → 累加 6，左右子樹皆 null |
| 5 | `15` | `15 > 10` → 剪右，往左子樹（`13`）走 |
| 6 | `13` | `13 > 10` → 剪右，往左子樹（null），回傳 0 |

**結果**：`10 + 7 + 6 = 23` ✓

---

## 專案結構

```
leetcode_938/
├── leetcode_938.sln
└── leetcode_938/
    ├── leetcode_938.csproj
    └── Program.cs          # 解題程式碼與測試資料
```

## 執行方式

```bash
dotnet run --project leetcode_938/leetcode_938.csproj
```
