LeetCode 1382 题目要求将一棵二叉搜索树（BST）变为平衡的二叉搜索树。平衡的二叉搜索树是指每个节点的左右子树的高度差不超过 1。

以下是用 C# 实现该题的解法：

```csharp
using System;
using System.Collections.Generic;

public class TreeNode {
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution {
    public TreeNode BalanceBST(TreeNode root) {
        List<int> nodes = new List<int>();
        InorderTraversal(root, nodes);
        return BuildBalancedBST(nodes, 0, nodes.Count - 1);
    }
    
    private void InorderTraversal(TreeNode node, List<int> nodes) {
        if (node == null) {
            return;
        }
        
        InorderTraversal(node.left, nodes);
        nodes.Add(node.val);
        InorderTraversal(node.right, nodes);
    }
    
    private TreeNode BuildBalancedBST(List<int> nodes, int left, int right) {
        if (left > right) {
            return null;
        }
        
        int mid = left + (right - left) / 2;
        TreeNode root = new TreeNode(nodes[mid]);
        root.left = BuildBalancedBST(nodes, left, mid - 1);
        root.right = BuildBalancedBST(nodes, mid + 1, right);
        
        return root;
    }
}
```

### 解释
1. **InorderTraversal 方法**:
   - 该方法通过中序遍历将二叉搜索树中的节点值存储在一个列表中。中序遍历二叉搜索树会得到一个有序的列表。

2. **BuildBalancedBST 方法**:
   - 该方法通过递归的方式将有序列表转换为平衡的二叉搜索树。它选择列表的中间元素作为当前树的根节点，并递归地构建左子树和右子树。

3. **BalanceBST 方法**:
   - 该方法首先调用 `InorderTraversal` 方法获取有序节点列表，然后调用 `BuildBalancedBST` 方法构建平衡的二叉搜索树。

通过以上步骤，可以将给定的二叉搜索树转换为平衡的二叉搜索树。