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
```
