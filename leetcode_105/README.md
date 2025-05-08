# LeetCode 105：從前序與中序遍歷序列構造二叉樹

## 題目連結
- [LeetCode 英文版](https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/description/)
- [LeetCode 中文版](https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/description/)

## 題目描述
根據前序和中序遍歷序列構造二叉樹。

## 解題思路

### 前序遍歷與中序遍歷的特點
- **前序遍歷特點**：
  - 順序為：根節點 → 左子樹 → 右子樹
  - 第一個元素必定是根節點
- **中序遍歷特點**：
  - 順序為：左子樹 → 根節點 → 右子樹
  - 根節點左邊是左子樹所有節點
  - 根節點右邊是右子樹所有節點

### 演算法步驟
1. 從前序遍歷中取第一個元素作為根節點
2. 在中序遍歷中找到該根節點位置
3. 根據中序遍歷中根節點的位置，確定：
   - 左子樹的節點數量
   - 左右子樹在兩個陣列中的範圍
4. 遞迴處理左右子樹

## 演算法分析
- **時間複雜度**：O(n)，其中 n 為節點數量
- **空間複雜度**：O(n)，主要是遞迴呼叫堆疊的空間

## 程式碼結構

### TreeNode 類別
定義二叉樹節點的結構，包含值和左右子節點指標。

### 主要函式
- `BuildTree`: 主要建樹函式，處理初始參數檢查
- `BuildTreeHelper`: 遞迴輔助函式，實際執行建樹邏輯

### 輔助函式
- `PreorderTraversal`: 前序遍歷二叉樹
- `InorderTraversal`: 中序遍歷二叉樹

## 測試案例

### 測試案例 1
- 前序遍歷：`[3, 9, 20, 15, 7]`
- 中序遍歷：`[9, 3, 15, 20, 7]`
- 期望建立的二叉樹結構：
  ```
      3
     / \
    9  20
      /  \
     15   7
  ```

### 測試案例 2
- 前序遍歷：`[1, 2, 3]`
- 中序遍歷：`[2, 1, 3]`
- 期望建立的二叉樹結構：
  ```
      1
     / \
    2   3
  ```

## 效能優化參考
可以使用雜湊表存儲中序遍歷中每個元素的索引位置，從而將在中序遍歷中尋找根節點的時間從 O(n) 優化為 O(1)。

## 參考資料
- [圖解從 O(n²) 到 O(n) 的優化思路](https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/solutions/2646359/tu-jie-cong-on2-dao-onpythonjavacgojsrus-aob8/)
- [從前序與中序遍歷序列構造二叉樹的詳細解析](https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/solutions/255811/cong-qian-xu-yu-zhong-xu-bian-li-xu-lie-gou-zao-9/)