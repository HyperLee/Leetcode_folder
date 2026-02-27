# LeetCode 1022 — Sum of Root To Leaf Binary Numbers

> **Daily Question** · 2026-02-24

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/C%23-14-239120)](https://learn.microsoft.com/zh-tw/dotnet/csharp/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-brightgreen)](https://leetcode.com/problems/sum-of-root-to-leaf-binary-numbers/)

---

## 題目說明

給定一棵二元樹的根節點，其中每個節點的值只有 `0` 或 `1`。從根到任一葉節點所經過的節點序列，可以組成一個二進位數，根節點對應最高位元（MSB）。

**目標**：計算所有根到葉路徑所代表的二進位數之總和，保證答案可用 32 位元整數儲存。

- LeetCode EN：<https://leetcode.com/problems/sum-of-root-to-leaf-binary-numbers/>
- LeetCode CN：<https://leetcode.cn/problems/sum-of-root-to-leaf-binary-numbers/>

---

## 解題概念

### 類比十進制理解二進制的累積方式

以十進制為例，把路徑 `1 → 2 → 3` 還原成整數 `123` 的過程：

```
0  ×10+1→  1  ×10+2→  12  ×10+3→  123
```

二進制的做法完全相同，只是把「×10」換成「×2」，把「+數字」換成「| 位元值」：

```
0  ×2+1→  1  ×2+0→  10  ×2+1→  101  ×2+1→  1011
```

路徑 `1 → 0 → 1 → 1` 對應二進位數 `1011`，即十進制的 **11**。

### 位元運算的等效替換

| 算術運算 | 位元運算 | 說明 |
|---------|---------|------|
| `num * 2` | `num << 1` | 左移一位，為新位元騰出空間 |
| `+ node.val` | `\| node.val` | 填入當前節點的值（0 或 1） |

因此每訪問一個節點，只需執行：

```csharp
num = num << 1 | node.val;
```

---

## 解法：DFS 深度優先搜尋

### 出發點

- 使用**自頂向下**的 DFS，在遞迴過程中隨時維護「從根到當前節點」的累積數值 `num`。
- 到達葉節點時，`num` 即為該條路徑完整的二進位數值，累加至總和 `sum`。

### 演算法流程

1. 呼叫 `DFS(root, 0)` 開始遞迴，初始數值為 `0`。
2. 每進入一個節點，更新 `num = num << 1 | node.val`。
3. 若為葉節點（左右子節點均為 `null`），將 `num` 加入 `sum` 並返回。
4. 否則，繼續遞迴左子樹與右子樹。

### 複雜度

| 類型 | 複雜度 | 說明 |
|------|--------|------|
| 時間 | O(N) | N 為節點數，每個節點恰好訪問一次 |
| 空間 | O(H) | H 為樹的高度，遞迴呼叫堆疊深度 |

---

## 範例演示

### 範例 1

```
      1
     / \
    0   1
   / \ / \
  0  1 0  1
```

| 路徑 | 二進位 | 十進制 |
|------|--------|--------|
| 1→0→0 | `100` | 4 |
| 1→0→1 | `101` | 5 |
| 1→1→0 | `110` | 6 |
| 1→1→1 | `111` | 7 |

**總和 = 4 + 5 + 6 + 7 = 22** ✓

#### DFS 遞迴追蹤（左子樹 1→0→0 路徑）

```
DFS(1, 0)
  num = 0 << 1 | 1 = 1
  DFS(0, 1)
    num = 1 << 1 | 0 = 10 (二進位)
    DFS(0, 10)
      num = 10 << 1 | 0 = 100 (二進位) = 4
      [葉節點] sum += 4
```

### 範例 2

```
  0
```

唯一路徑：`0` → 二進位 `0` = **0** ✓

### 範例 3（題目描述中的範例）

```
  1
 /
0
 \
  1
   \
    1
```

路徑：`1 → 0 → 1 → 1` → 二進位 `1011` = **11** ✓

---

## 程式碼

程式碼位於 [`leetcode_1022/Program.cs`](leetcode_1022/Program.cs)。

```csharp
private void DFS(TreeNode node, int num)
{
    if (node is null) return;

    // num << 1 等同於 * 2；| node.val 等同於 + node.val
    num = num << 1 | node.val;

    if (node.left is null && node.right is null)
    {
        sum += num;
        return;
    }

    DFS(node.left, num);
    DFS(node.right, num);
}
```

> [!NOTE]
> `sum` 為類別層級的欄位。若需重複呼叫 `SumRootToLeaf`，請重新建立實例或在方法開頭將 `sum` 重設為 `0`。

---

## 解法二：迭代（後序 DFS + Stack）

### 解題概念

與遞迴解法的數學核心完全相同：

- **向下**（壓入堆疊）時：`val = (val << 1) | node.val`，逐位元累積路徑數值。
- **向上**（彈出堆疊）時：`val >>= 1`，右移一位還原至父節點層級的數值。

兩個方向的操作恰好是逆運算，因此不需要另外儲存「回溯用的舊值」。

### 出發點

遞迴 DFS 的呼叫堆疊本質上是一個**後序（Post-order）遍歷**：先一路走到最左下，再依序處理右子樹，最後才處理當前節點。利用顯式 `Stack` 和 `prev` 指標可以精確還原這個流程，同時避免遞迴的系統呼叫堆疊開銷。

核心問題：「何時該彈出一個節點？」

> 當其**右子樹為空**，或**右子樹已被訪問完畢**（即 `root.right == prev`），才能彈出。

### 演算法流程

```
初始化：stack 為空，val = 0，ret = 0，prev = null

重複以下步驟直到 root 為 null 且 stack 為空：

  步驟 1 ── 沿左子樹一路向下
  while root 非空:
      val = (val << 1) | root.val   ← 累積當前節點的位元值
      root 壓入 stack
      root = root.left

  步驟 2 ── 查看 stack 頂端，決定是否可以彈出
  root = stack.Peek()

  若 root.right 為空 或 root.right == prev:    ← 右子樹已完成
      若 root 為葉節點: ret += val              ← 累加路徑值
      val >>= 1                                 ← 還原至父節點層級
      彈出 stack
      prev = root                               ← 標記本節點已訪問
      root = null                               ← 回到外層 while 繼續
  否則:
      root = root.right                         ← 轉向右子樹，重複步驟 1
```

### 複雜度

| 類型 | 複雜度 | 說明 |
|------|--------|------|
| 時間 | O(N) | N 為節點數，每個節點各入/出堆疊一次 |
| 空間 | O(H) | H 為樹的高度，堆疊最多同時存放 H 個節點 |

---

## 方法二範例演示

以範例 1 的樹為例，逐步追蹤 `val` 與 `stack` 的狀態變化：

```
      1
     / \
    0   1
   / \ / \
  0  1 0  1
```

| 操作 | root | stack（底→頂） | val（二進位） | ret | prev |
|------|------|--------------|-------------|-----|------|
| 初始 | 1 | \[\] | 0 | 0 | — |
| 壓入 1，往左 | 0 | \[1\] | 1 | 0 | — |
| 壓入 0，往左 | 0 | \[1,0\] | 10 | 0 | — |
| 壓入 0，往左 | null | \[1,0,0\] | 100 | 0 | — |
| 頂=0，葉節點 → ret+=4，右移，彈出 | null | \[1,0\] | 10 | 4 | 0(左下) |
| 頂=0，right 非空且≠prev → 轉右 | 1 | \[1,0\] | 10 | 4 | — |
| 壓入 1，往左 | null | \[1,0,1\] | 101 | 4 | — |
| 頂=1，葉節點 → ret+=5，右移，彈出 | null | \[1,0\] | 10 | 9 | 1(左子) |
| 頂=0，right==prev → 非葉，右移，彈出 | null | \[1\] | 1 | 9 | 0(左) |
| 頂=1，right 非空且≠prev → 轉右 | 1 | \[1\] | 1 | 9 | — |
| 壓入 1，往左 | 0 | \[1,1\] | 11 | 9 | — |
| 壓入 0，往左 | null | \[1,1,0\] | 110 | 9 | — |
| 頂=0，葉節點 → ret+=6，右移，彈出 | null | \[1,1\] | 11 | 15 | 0(右下左) |
| 頂=1，right 非空且≠prev → 轉右 | 1 | \[1,1\] | 11 | 15 | — |
| 壓入 1，往左 | null | \[1,1,1\] | 111 | 15 | — |
| 頂=1，葉節點 → ret+=7，右移，彈出 | null | \[1,1\] | 11 | 22 | 1(右下右) |
| 頂=1，right==prev → 非葉，右移，彈出 | null | \[1\] | 1 | 22 | 1(右) |
| 頂=1，right==prev → 非葉，右移，彈出 | null | \[\] | 0 | 22 | 1(根) |
| stack 空，結束 | — | \[\] | 0 | **22** | — |

最終 `ret = 22` ✓

---

## 方法二程式碼

```csharp
public int SumRootToLeaf2(TreeNode root)
{
    Stack<TreeNode> stack = new Stack<TreeNode>();
    int val = 0;
    int ret = 0;
    TreeNode? prev = null;

    while (root is not null || stack.Count > 0)
    {
        // 步驟 1：沿左子樹向下，同步累積 val
        while (root is not null)
        {
            val = (val << 1) | root.val;
            stack.Push(root);
            root = root.left;
        }

        // 步驟 2：查看頂端，判斷是否可後序離開
        root = stack.Peek();
        if (root.right is null || root.right == prev)
        {
            if (root.left is null && root.right is null)
                ret += val;           // 葉節點：累加路徑值

            val >>= 1;                // 還原至父節點層級
            stack.Pop();
            prev = root;
            root = null;
        }
        else
        {
            root = root.right;        // 轉向右子樹
        }
    }
    return ret;
}
```

---

## 執行方式

```bash
dotnet build
dotnet run --project leetcode_1022
```

預期輸出：

```
範例 1 結果: 22
範例 2 結果: 0
範例 3 結果: 11

=== 方法二：迭代解法驗證 ===
範例 1 結果: 22
範例 2 結果: 0
範例 3 結果: 11
```
