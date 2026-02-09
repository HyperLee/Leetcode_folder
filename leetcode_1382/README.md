# LeetCode 1382: 將二叉搜尋樹變平衡

> 這是一個解決 LeetCode 1382 題目的 C# 實作專案，使用中序走訪和遞迴重建的方法將不平衡的二叉搜尋樹轉換為平衡的二叉搜尋樹。

## 題目說明

### 問題描述

給定一個二叉搜尋樹（Binary Search Tree, BST）的根節點，請回傳一個具有相同節點值的**平衡**二叉搜尋樹。

如果存在多個答案，可回傳任意一個。

### 平衡的定義

一個二叉搜尋樹被視為**平衡的**，當且僅當每個節點的左右子樹深度差不超過 1。

### 題目連結

- [LeetCode 英文版](https://leetcode.com/problems/balance-a-binary-search-tree/description/?envType=daily-question&envId=2026-02-09)
- [LeetCode 中文版](https://leetcode.cn/problems/balance-a-binary-search-tree/description/?envType=daily-question&envId=2026-02-09)

### 範例

**範例 1:**

```
輸入: root = [1,null,2,null,3,null,4,null,null]
       1
        \
         2
          \
           3
            \
             4

輸出: [2,1,3,null,null,null,4]
       2
      / \
     1   3
          \
           4
```

**範例 2:**

```
輸入: root = [2,1,3]
       2
      / \
     1   3

輸出: [2,1,3]
       2
      / \
     1   3
```

### 限制條件

- 樹中節點的數量範圍為 `[1, 10^4]`
- `1 <= Node.val <= 10^5`

## 解題概念與出發點

### 問題分析

1. **二叉搜尋樹（BST）的特性**:
   - 左子樹的所有節點值 < 根節點值
   - 右子樹的所有節點值 > 根節點值
   - 左右子樹本身也是 BST

2. **平衡樹的目標**:
   - 需要確保每個節點的左右子樹高度差 ≤ 1
   - 這樣可以保證樹的高度為 O(log n)，優化查詢效率

3. **核心觀察**:
   - BST 的中序走訪會產生一個遞增的有序序列
   - 從有序序列可以建構平衡的 BST：選擇中間元素作為根，左半部分作為左子樹，右半部分作為右子樹

### 解題策略

本解法採用**兩階段方法**：

1. **第一階段：轉換為有序陣列**
   - 使用中序走訪（In-Order Traversal）將 BST 轉換為有序陣列
   - 時間複雜度：O(n)

2. **第二階段：重建平衡樹**
   - 從有序陣列遞迴地建構平衡 BST
   - 選擇中間元素作為根節點，確保左右子樹大小相近
   - 時間複雜度：O(n)

### 為什麼這個方法有效？

- **中序走訪保證有序性**：BST 的中序走訪天然產生遞增序列
- **中間元素確保平衡**：選擇中間元素作為根，左右子樹元素數量最多相差 1，保證高度平衡
- **遞迴結構優雅**：子問題與原問題結構相同，適合遞迴處理

## 解法詳細說明

### 演算法步驟

#### 步驟 1: 中序走訪 (InOrderTraversal)

```csharp
public static void InOrderTraversal(TreeNode node, List<int> sortedValues) 
{
    if(node is null)
        return;

    InOrderTraversal(node.left, sortedValues);   // 遍歷左子樹
    sortedValues.Add(node.val);                  // 訪問當前節點
    InOrderTraversal(node.right, sortedValues);  // 遍歷右子樹
}
```

**功能**：將 BST 轉換為有序陣列
- 走訪順序：左 → 根 → 右
- 對於 BST，這會產生遞增序列

#### 步驟 2: 建構平衡樹 (BuildBalancedBST)

```csharp
public static TreeNode BuildBalancedBST(int start, int end, List<int> sortedValues)
{
    if(start > end)
        return null;
        
    if(start == end)
        return new TreeNode(sortedValues[start]);

    int mid = start + (end - start) / 2;         // 計算中間索引
    TreeNode node = new TreeNode(sortedValues[mid]);
    node.left = BuildBalancedBST(start, mid - 1, sortedValues);   // 左子樹
    node.right = BuildBalancedBST(mid + 1, end, sortedValues);    // 右子樹
    
    return node;
}
```

**功能**：從有序陣列遞迴建構平衡 BST
- 選擇中間元素作為根節點
- 左半部分建構左子樹
- 右半部分建構右子樹

#### 步驟 3: 主函式 (BalanceBST)

```csharp
public TreeNode BalanceBST(TreeNode root)
{
    List<int> sortedValues = new List<int>();
    InOrderTraversal(root, sortedValues);
    return BuildBalancedBST(0, sortedValues.Count - 1, sortedValues);
}
```

### 複雜度分析

| 指標 | 複雜度 | 說明 |
|------|--------|------|
| **時間複雜度** | O(n) | 中序走訪 O(n) + 建構樹 O(n) |
| **空間複雜度** | O(n) | 儲存有序陣列 O(n) + 遞迴堆疊 O(log n) |

其中 n 為樹中節點的數量。

### 關鍵技巧

1. **避免整數溢位**：使用 `start + (end - start) / 2` 計算中間索引
2. **邊界處理**：正確處理 `start > end`（返回 null）和 `start == end`（葉節點）
3. **BST 特性**：充分利用中序走訪會產生有序序列的特性

## 舉例演示流程

### 完整範例：轉換不平衡樹為平衡樹

#### 初始狀態

```
原始不平衡樹:
    1
     \
      2
       \
        3
         \
          4
```

#### 步驟 1: 中序走訪

```
走訪過程:
1. 訪問節點 1 → sortedValues = [1]
2. 訪問節點 2 → sortedValues = [1, 2]
3. 訪問節點 3 → sortedValues = [1, 2, 3]
4. 訪問節點 4 → sortedValues = [1, 2, 3, 4]

結果: sortedValues = [1, 2, 3, 4]
```

#### 步驟 2: 建構平衡樹

```
BuildBalancedBST(0, 3, [1, 2, 3, 4])

第 1 層 (根節點):
├─ start = 0, end = 3
├─ mid = 1, root = 2
└─ 建立節點 2

第 2 層 (左子樹):
├─ BuildBalancedBST(0, 0, [1, 2, 3, 4])
├─ start == end, 建立葉節點 1
└─ 返回節點 1

第 2 層 (右子樹):
├─ BuildBalancedBST(2, 3, [1, 2, 3, 4])
├─ mid = 2, root = 3
└─ 建立節點 3

第 3 層 (右子樹的左子樹):
├─ BuildBalancedBST(2, 1, [1, 2, 3, 4])
├─ start > end
└─ 返回 null

第 3 層 (右子樹的右子樹):
├─ BuildBalancedBST(3, 3, [1, 2, 3, 4])
├─ start == end, 建立葉節點 4
└─ 返回節點 4
```

#### 最終結果

```
平衡後的樹:
       2
      / \
     1   3
          \
           4

驗證平衡性:
- 節點 2: |height(左) - height(右)| = |1 - 2| = 1 ✓
- 節點 1: |height(左) - height(右)| = |0 - 0| = 0 ✓
- 節點 3: |height(左) - height(右)| = |0 - 1| = 1 ✓
- 節點 4: |height(左) - height(右)| = |0 - 0| = 0 ✓

所有節點的左右子樹高度差 ≤ 1，樹是平衡的！
```

### 視覺化比較

| 指標 | 原始樹 | 平衡樹 |
|------|--------|--------|
| **高度** | 4 | 3 |
| **最小深度** | 4 | 2 |
| **是否平衡** | ✗ | ✓ |
| **查詢效率** | O(n) | O(log n) |

## 執行專案

### 建構專案

```bash
dotnet build
```

### 執行程式

```bash
dotnet run
```

### 預期輸出

```
測試案例 1: 不平衡樹 [1,null,2,null,3,null,4]
原始樹(中序遍歷): 
1, 2, 3, 4
平衡後(中序遍歷): 
1, 2, 3, 4

測試案例 2: 已平衡樹 [2,1,3]
原始樹(中序遍歷): 
1, 2, 3
平衡後(中序遍歷): 
1, 2, 3
```

## 相關資源

### 概念參考

- [AVL Tree — 高度平衡二元搜尋樹](https://tedwu1215.medium.com/avl-tree-%E9%AB%98%E5%BA%A6%E5%B9%B3%E8%A1%A1%E4%BA%8C%E5%85%83%E6%90%9C%E5%B0%8B%E6%A8%B9%E4%BB%8B%E7%B4%B9%E8%88%87%E7%AF%84%E4%BE%8B-15a82c5b778f)
- [二元搜尋樹 - 維基百科](https://zh.wikipedia.org/wiki/%E4%BA%8C%E5%85%83%E6%90%9C%E5%B0%8B%E6%A8%B9)

### 題解參考

- [LeetCode 官方題解](https://leetcode.cn/problems/balance-a-binary-search-tree/solutions/241897/jiang-er-cha-sou-suo-shu-bian-ping-heng-by-leetcod/)
- [AVL 樹實作解法](https://leetcode.cn/problems/balance-a-binary-search-tree/solutions/150820/shou-si-avlshu-wo-bu-guan-wo-jiu-shi-yao-xuan-zhua/)
- [中序走訪解法](https://leetcode.cn/problems/balance-a-binary-search-tree/solutions/342529/xian-yong-zhong-xu-bian-li-na-dao-sheng-xu-de-list/)

## 技術細節

### 開發環境

- **.NET**: 10.0
- **語言**: C# 14
- **專案類型**: 主控台應用程式

### 專案結構

```
leetcode_1382/
├── leetcode_1382.sln
├── README.md
└── leetcode_1382/
    ├── leetcode_1382.csproj
    └── Program.cs
```

## 延伸思考

### 其他解法

1. **AVL 樹旋轉法**：
   - 使用 AVL 樹的旋轉操作（左旋、右旋）維護平衡
   - 時間複雜度：O(n log n)
   - 空間複雜度：O(1)（不含遞迴堆疊）

2. **Day-Stout-Warren 演算法**：
   - 原地轉換，不需額外空間
   - 時間複雜度：O(n)
   - 空間複雜度：O(1)

### 為什麼選擇中序走訪法？

| 優點 | 缺點 |
|------|------|
| ✓ 實作簡單直觀 | ✗ 需要 O(n) 額外空間 |
| ✓ 程式碼易讀易維護 | ✗ 需要兩次遍歷 |
| ✓ 時間複雜度最優 O(n) | |
| ✓ 適合作為學習範例 | |

---

**專案作者**: HyperLee  
**最後更新**: 2026年2月9日
