# LeetCode 230: 二元搜尋樹中第 K 小的元素

## 題目描述

給定一個二元搜尋樹 (BST) 的根節點 root 和一個整數 k，請你設計一個演算法，找出 BST 中第 k 小的元素。k 從 1 開始計算。

你必須在 O (h) 的時間複雜度內完成，h 是樹的高度。

## 題目連結

- [LeetCode](https://leetcode.com/problems/kth-smallest-element-in-a-bst/description/)
- [LeetCode (中文)](https://leetcode.cn/problems/kth-smallest-element-in-a-bst/description/)

## 解題思路

1. 利用二元搜尋樹的特性，中序遍歷 (Inorder Traversal) 會產生由小到大的排序結果
2. 使用計數器記錄當前遍歷到第幾個節點
3. 當計數器等於 k 時，即為所求的第 k 小元素

## 演算法實現

### 遞迴版解法

本解法使用深度優先搜尋 (DFS) 實現中序遍歷：

1. 遞迴遍歷左子樹
2. 訪問當前節點，同時將計數器 k 減 1
3. 若計數器 k 等於 0，表示找到目標節點
4. 遞迴遍歷右子樹

```csharp
// 定義樹節點結構
public class TreeNode 
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) 
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

// 宣告用於儲存結果和計數器的變數
private int res;
private int k;

// 主要函式：尋找二元搜尋樹中第 k 小的元素
public int KthSmallest(TreeNode root, int k) 
{
    this.k = k;
    dfs(root);
    return res;
}

// DFS 中序遍歷
private void dfs(TreeNode node)
{
    if(node == null)
    {
        return;
    }

    // 遞迴遍歷左子樹
    dfs(node.left);
    
    // 訪問當前節點
    if(--k == 0)
    {
        res = node.val;
        return;
    }
    
    // 遞迴遍歷右子樹
    dfs(node.right);
}
```

### 疊代版解法

為了避免系統堆疊溢位，可以使用疊代方式實現中序遍歷：

1. 使用堆疊來模擬遞迴過程
2. 先遍歷到最左邊的節點，同時將路徑上的所有節點存入堆疊
3. 從堆疊中取出節點，模擬回溯過程
4. 使用遞減計數器 `--k` 來追蹤已訪問節點的順序，與遞迴版保持一致的計數邏輯
5. 當 k 減到 0 時，表示找到目標節點，返回其值

```csharp
public int KthSmallestIterative(TreeNode? root, int k) 
{
    // 處理空樹的情況
    if (root == null)
    {
        return -1;
    }
    
    // 建立堆疊用於模擬遞迴呼叫時系統維護的堆疊
    Stack<TreeNode> 堆疊 = new Stack<TreeNode>();
    TreeNode? current = root;
    
    while (current != null || 堆疊.Count > 0)
    {
        // 步驟1: 遍歷到最左邊的節點，同時將路徑上的所有節點存入堆疊
        while (current != null)
        {
            堆疊.Push(current);
            current = current.left;
        }
        
        // 步驟2: 從堆疊中取出節點（相當於回溯到上一層）
        current = 堆疊.Pop();
        
        // 步驟3: 檢查是否是第k個節點 (使用--k替代count++)
        if (--k == 0)
        {
            return current.val; // 找到目標節點，返回其值
        }
        
        // 步驟4: 處理右子樹
        current = current.right;
    }
    
    return -1; // 若找不到第k小的元素
}
```

### 疊代版解法的優點

1. 可以處理較深的樹結構而不會發生堆疊溢位
2. 在某些情況下效能較遞迴實作更佳
3. 可以提前終止，無需遍歷整棵樹
4. 可以處理特殊情況，如空樹或 k 值超出節點數量
5. 使用與遞迴版相同的計數邏輯 (`--k`)，保持一致性

## 時間複雜度

- 最壞情況：O (n)，其中 n 是樹中的節點數量
- 平均情況：O (h + k)，其中 h 是樹的高度，k 是目標元素的排名

## 測試案例

### 測試案例 1

- 輸入: root = \[3,1,4,null,2], k = 1
- 輸出: 1
- 中序遍歷結果: 1 2 3 4

### 測試案例 2

- 輸入: root = \[5,3,6,2,4,null,null,1], k = 3
- 輸出: 3
- 中序遍歷結果: 1 2 3 4 5 6

### 測試案例 3

- 輸入: root = null (空樹), k = 1
- 輸出: -1 (找不到節點)

### 測試案例 4

- 輸入: root = \[5,3,6,2,4,null,null,1], k = 10 (k大於節點數量)
- 輸出: -1 (找不到第10小的節點)

## 備註

- 在二元搜尋樹中，中序遍歷會自然產生有序序列 (由小到大)
- 本題的關鍵是理解 `--k` 操作 (前置遞減) 和中序遍歷的結合
- 使用前置遞減 `--k` 而非後置遞減 `k--` 是關鍵，因為需要先減少計數器再進行比較
- 疊代版解法使用堆疊模擬遞迴過程，適合處理深度較大的樹


# 遞迴與疊代的比較

## 遞迴 (Recursion)

遞迴是一種程式設計方法，函式在其定義中呼叫自身。這種方式通常用於解決可以分解為相似子問題的任務。

### 遞迴的關鍵特性：
- **自我呼叫**：函式在自身內部進行呼叫
- **終止條件**：必須有基礎情況以防止無限遞迴
- **系統堆疊**：每次函式呼叫都會在系統堆疊中建立新的框架

### 範例 (程式碼中的 dfs 函式)：
```csharp
private void dfs(TreeNode node)
{
    if(node == null)  // 終止條件
    {
        return;
    }

    // 遞迴呼叫自身處理左子樹
    dfs(node.left);
    
    // 處理當前節點
    if(--k == 0)
    {
        res = node.val;
        return;
    }
    
    // 遞迴呼叫自身處理右子樹
    dfs(node.right);
}
```

## 疊代 (Iteration)

疊代是使用迴圈結構（如 for、while）重複執行一系列指令的方法，不依賴系統堆疊來追蹤執行狀態。

### 疊代的關鍵特性：
- **使用迴圈**：通過迴圈控制重複執行
- **顯式資料結構**：通常需要手動建立並管理堆疊或佇列等資料結構
- **記憶體效率**：避免系統堆疊溢位風險

### 範例 (程式碼中的 KthSmallestIterative 函式)：
```csharp
public int KthSmallestIterative(TreeNode? root, int k) 
{
    // 處理空樹的情況
    if (root == null) return -1;
    
    // 手動建立堆疊
    Stack<TreeNode> 堆疊 = new Stack<TreeNode>();
    TreeNode? current = root;
    
    while (current != null || 堆疊.Count > 0)
    {
        // 使用迴圈模擬遞迴下降
        while (current != null)
        {
            堆疊.Push(current);
            current = current.left;
        }
        
        current = 堆疊.Pop();
        
        if (--k == 0) return current.val;
        
        current = current.right;
    }
    
    return -1;
}
```

## 遞迴與疊代的比較

| **特性**         | **遞迴方法**                      | **疊代方法**                        |
|----------------|--------------------------------|----------------------------------|
| **程式碼複雜度**   | 通常較簡潔、直觀且易讀              | 通常較複雜，需要額外的資料結構            |
| **記憶體使用**    | 使用系統堆疊，深度大時可能溢位        | 明確控制記憶體使用，避免堆疊溢位          |
| **執行效能**     | 有函式呼叫開銷，可能影響效能          | 無函式呼叫開銷，通常較高效              |
| **適用場景**     | 樹結構處理、分治演算法              | 大型資料集、極深層遞迴問題               |
| **狀態追蹤**     | 系統自動管理函式呼叫和返回           | 需要手動管理狀態和遍歷順序              |

在二元搜尋樹第 k 小元素問題中，我們同時提供了遞迴與疊代兩種解法，展示了兩種方法的實作差異與各自優點。選擇適合的方法應考慮實際場景需求，以平衡程式碼可讀性和執行效率。


| 比較項目 | 遞迴 (Recursion)     | 迭代 (Iteration)          |
| :--- | :----------------- | :---------------------- |
| 重點   | 函式自己呼叫自己           | 用迴圈重複執行                 |
| 主要工具 | 函式呼叫 (要有終止條件)      | `for`、`while`、`foreach` |
| 執行流程 | 每次呼叫都壓到 call stack | 每次循環只是普通執行              |
| 資源消耗 | 可能比較高 (因為堆疊)       | 通常比較省記憶體                |
| 例子   | 樹狀結構遍歷、費波那契數列      | 瀏覽列表、累加總和               |
