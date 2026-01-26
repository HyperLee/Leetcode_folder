# LeetCode 700 - Search in a Binary Search Tree

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-700-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/search-in-a-binary-search-tree/)

C# solution for LeetCode Problem 700: Search in a Binary Search Tree (二元搜尋樹中的搜尋).

## Problem Description

Given the root of a binary search tree (BST) and an integer `val`, find the node in the BST that the node's value equals `val` and return the subtree rooted with that node. If such a node does not exist, return `null`.

**Example:**

```text
Input: root = [4,2,7,1,3], val = 2
Output: [2,1,3]

        4
       / \
      2   7
     / \
    1   3
```

## Solution Approach

### Recursive Method

The solution leverages the fundamental properties of a Binary Search Tree:

- All nodes in the left subtree have values **less than** the root
- All nodes in the right subtree have values **greater than** the root

**Algorithm:**

1. If `root` is null, return null
2. If `val == root.val`, return `root`
3. If `val < root.val`, recursively search the left subtree
4. If `val > root.val`, recursively search the right subtree

**Complexity Analysis:**

| Metric           | Value                                   |
| ---------------- | --------------------------------------- |
| Time Complexity  | O(H), where H is the height of the tree |
| Space Complexity | O(H), for recursive call stack          |

> [!NOTE]
> In the worst case (skewed tree), the time complexity becomes O(N), where N is the number of nodes.

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) or later

### Run the Solution

```bash
# Clone the repository
git clone https://github.com/HyperLee/Leetcode_folder.git

# Navigate to the project directory
cd Leetcode_folder/leetcode_700/leetcode_700

# Build and run
dotnet run
```

### Expected Output

```text
搜尋值 2：找到節點，值為 2
搜尋值 7：找到節點，值為 7
搜尋值 5：未找到
搜尋值 4：找到節點，值為 4
空樹搜尋值 1：未找到
```

## Project Structure

```text
leetcode_700/
├── leetcode_700.slnx          # Solution file
├── README.md
└── leetcode_700/
    ├── leetcode_700.csproj    # Project file
    └── Program.cs             # Main solution code
```

## References

- [LeetCode 700 - English](https://leetcode.com/problems/search-in-a-binary-search-tree/description/)
- [LeetCode 700 - 中文](https://leetcode.cn/problems/search-in-a-binary-search-tree/description/)
