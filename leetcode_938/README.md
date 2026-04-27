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

## 解法一：深度優先搜尋（DFS）搭配剪枝

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

## 解法一演示流程

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

## 解法二：廣度優先搜尋（BFS）搭配剪枝

### 解題概念與出發點

DFS 是一路往子樹深處搜尋；BFS 則是使用**佇列（Queue）**，依照節點所在層級由上到下、由左到右逐層處理。

這題雖然改用 BFS，但核心仍然是 BST 的剪枝性質：

> 對目前節點 `current`：
> - 若 `current.val <= low`，左子樹所有節點值都 `< current.val`，不可能落在 `[low, high]`，可跳過左子樹
> - 若 `current.val >= high`，右子樹所有節點值都 `> current.val`，不可能落在 `[low, high]`，可跳過右子樹

因此 BFS 不需要把所有子節點都放進佇列，而是只把**仍可能包含答案的子樹**加入佇列。

---

### 解法說明

使用一個佇列保存接下來要處理的節點：

| 條件 | 動作 | 理由 |
|------|------|------|
| `root == null` | 回傳 `0` | 空樹沒有任何節點可累加 |
| `low <= current.val <= high` | 累加 `current.val` | 當前節點值落在目標範圍內 |
| `current.val > low` | 將左子節點加入佇列 | 左子樹可能存在 `>= low` 的節點 |
| `current.val < high` | 將右子節點加入佇列 | 右子樹可能存在 `<= high` 的節點 |

```csharp
public int RangeSumBSTBfs(TreeNode? root, int low, int high)
{
    if (root is null)
    {
        return 0;
    }

    int sum = 0;
    Queue<TreeNode> queue = new Queue<TreeNode>();
    queue.Enqueue(root);

    while (queue.Count > 0)
    {
        TreeNode current = queue.Dequeue();

        if (current.val >= low && current.val <= high)
        {
            sum += current.val;
        }

        if (current.val > low && current.left is not null)
        {
            queue.Enqueue(current.left);
        }

        if (current.val < high && current.right is not null)
        {
            queue.Enqueue(current.right);
        }
    }

    return sum;
}
```

**時間複雜度**：$O(N)$（最壞情況仍可能走訪所有節點）  
**空間複雜度**：$O(W)$（佇列最多保存同一層的節點數，$W$ 為樹的最大寬度；最壞情況為 $O(N)$）

---

### 解法二演示流程

### 範例 1

```
       10
      /  \
     5    15
    / \     \
   3   7    18
```

`low = 7`，`high = 15`

| 步驟 | 取出節點 | 動作 | 佇列變化 | 目前總和 |
|------|----------|------|----------|----------|
| 初始 | - | 將根節點 `10` 加入佇列 | `[10]` | `0` |
| 1 | `10` | `7 ≤ 10 ≤ 15` → 累加 10；`10 > 7` 加入左子節點 `5`；`10 < 15` 加入右子節點 `15` | `[5, 15]` | `10` |
| 2 | `5` | `5 < 7` → 不累加；`5 <= 7` 跳過左子樹；`5 < 15` 加入右子節點 `7` | `[15, 7]` | `10` |
| 3 | `15` | `7 ≤ 15 ≤ 15` → 累加 15；`15 > 7` 左子節點為 null；`15 >= 15` 跳過右子樹 `18` | `[7]` | `25` |
| 4 | `7` | `7 ≤ 7 ≤ 15` → 累加 7；左右子節點皆不需要加入 | `[]` | `32` |

**結果**：`10 + 15 + 7 = 32` ✓

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

| 步驟 | 取出節點 | 動作 | 佇列變化 | 目前總和 |
|------|----------|------|----------|----------|
| 初始 | - | 將根節點 `10` 加入佇列 | `[10]` | `0` |
| 1 | `10` | `6 ≤ 10 ≤ 10` → 累加 10；`10 > 6` 加入左子節點 `5`；`10 >= 10` 跳過右子樹 `15` | `[5]` | `10` |
| 2 | `5` | `5 < 6` → 不累加；`5 <= 6` 跳過左子樹；`5 < 10` 加入右子節點 `7` | `[7]` | `10` |
| 3 | `7` | `6 ≤ 7 ≤ 10` → 累加 7；`7 > 6` 加入左子節點 `6`；右子節點為 null | `[6]` | `17` |
| 4 | `6` | `6 ≤ 6 ≤ 10` → 累加 6；左右子節點皆不需要加入 | `[]` | `23` |

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
