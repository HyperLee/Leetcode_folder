# LeetCode 133 - Clone Graph

> 無向圖的深度複製實作

## 題目說明

### 問題描述

給定一個無向連通圖中某個節點的參考，返回該圖的深度拷貝（Deep Copy）。

圖中的每個節點都包含以下資訊：

- `val`：整數，代表節點的值
- `neighbors`：鄰居節點列表（`List<Node>`）

### 題目連結

- [LeetCode 133 - Clone Graph (英文)](https://leetcode.com/problems/clone-graph/description/)
- [LeetCode 133 - 克隆图 (中文)](https://leetcode.cn/problems/clone-graph/description/)

### 什麼是深度拷貝？

**淺拷貝（Shallow Copy）**：

- 只複製物件的參考（reference）
- 原始物件和複製物件指向相同的記憶體位址
- 修改其中一個會影響另一個

**深度拷貝（Deep Copy）**：

- 複製整個物件結構及其所有子物件
- 創建全新的記憶體位址
- 原始物件和複製物件完全獨立

參考資料：

- [Wiki - 克隆 (程式設計)](https://zh.wikipedia.org/zh-tw/%E5%85%8B%E9%9A%86_(%E7%BC%96%E7%A8%8B))
- [Wiki - Object copying](https://en.wikipedia.org/wiki/Object_copying)

## 解題概念與出發點

### 核心挑戰

1. **環形結構問題**：圖中可能存在環（cycle），需要避免無限遞迴
2. **節點重複問題**：同一個節點可能被多個節點參考，需確保每個節點只被複製一次
3. **連接關係維護**：必須保持與原圖相同的連接關係

### 解題策略

選擇 **深度優先搜尋（DFS）** 搭配 **快取機制**：

**為什麼使用 DFS？**

- 需要完整遍歷整個圖結構
- 需要處理循環（環形結構）
- 需要追蹤訪問狀態
- 遞迴處理子結構的特性符合圖的遞迴性質

**為什麼使用 Dictionary？**

- 避免在環形圖中陷入無限遞迴
- 確保相同的節點只被複製一次
- 維護原節點與新節點的對應關係

### 時間與空間複雜度

- **時間複雜度**：`O(N + E)`
  - N：節點數
  - E：邊數
  - 每個節點和每條邊都只訪問一次

- **空間複雜度**：`O(N)`
  - Dictionary 儲存所有節點的對應關係
  - 遞迴呼叫堆疊的空間

## 詳細解法

### 節點結構

```csharp
public class Node 
{
    public int val;
    public IList<Node> neighbors;
    
    public Node(int _val) {
        val = _val;
        neighbors = new List<Node>();
    }
}
```

### 演算法實作

#### 1. 主要函式 - CloneGraph

```csharp
public Node CloneGraph(Node node)
{
    // 處理空節點的情況
    if (node == null) 
    {
        return null;
    }
    
    // 建立字典來儲存已複製的節點
    // Key: 原圖的節點物件，Value: 複製後的新節點物件
    Dictionary<Node, Node> visited = new Dictionary<Node, Node>();
    
    // 使用 DFS 遞迴複製圖
    return DFS(node, visited);
}
```

**關鍵設計**：

- 使用節點物件本身作為 Dictionary 的 key（而非 `node.val`）
- 原因：節點值可能重複，但每個節點物件的記憶體位址是唯一的

#### 2. DFS 遞迴函式

```csharp
private Node DFS(Node node, Dictionary<Node, Node> visited)
{
    // 如果節點已經被訪問過，直接返回對應的新節點
    if (visited.ContainsKey(node))
    {
        return visited[node];
    }
    
    // 建立新節點
    Node clone = new Node(node.val);
    
    // 將新節點加入已訪問字典（在處理鄰居之前）
    visited.Add(node, clone);
    
    // 遞迴處理所有鄰居節點
    foreach (var neighbor in node.neighbors)
    {
        clone.neighbors.Add(DFS(neighbor, visited));
    }
    
    return clone;
}
```

**執行流程**：

1. 檢查節點是否已被訪問（避免重複複製）
2. 創建當前節點的副本
3. **立即**將對應關係存入 Dictionary（重要：必須在遞迴前完成）
4. 遞迴處理所有相鄰節點
5. 將處理好的相鄰節點添加到新節點的 neighbors 列表

> **注意**：必須在遞迴處理鄰居節點之前將新節點加入 Dictionary，否則在環形圖中會造成無限遞迴。

## 範例演示流程

### 輸入範例

```text
adjList = [[2,4],[1,3],[2,4],[1,3]]
```

圖的結構：

```text
1 ---- 2
|      |
4 ---- 3
```

### 步驟演示

#### 建立原始圖

```csharp
Node node1 = new Node(1);
Node node2 = new Node(2);
Node node3 = new Node(3);
Node node4 = new Node(4);

// 建立連接關係
node1.neighbors.Add(node2);  // 1 -> 2
node1.neighbors.Add(node4);  // 1 -> 4
node2.neighbors.Add(node1);  // 2 -> 1
node2.neighbors.Add(node3);  // 2 -> 3
node3.neighbors.Add(node2);  // 3 -> 2
node3.neighbors.Add(node4);  // 3 -> 4
node4.neighbors.Add(node1);  // 4 -> 1
node4.neighbors.Add(node3);  // 4 -> 3
```

#### 複製過程追蹤

從 `node1` 開始執行 `CloneGraph(node1)`：

**Step 1**：處理節點 1

```text
- 建立 clone1（值為 1）
- visited = {node1: clone1}
- 開始處理 neighbors: [node2, node4]
```

**Step 2**：處理節點 2（node1 的第一個鄰居）

```text
- 建立 clone2（值為 2）
- visited = {node1: clone1, node2: clone2}
- 開始處理 neighbors: [node1, node3]
```

**Step 3**：處理節點 1（node2 的第一個鄰居）

```text
- 已在 visited 中，直接返回 clone1
- 避免無限遞迴！
```

**Step 4**：處理節點 3（node2 的第二個鄰居）

```text
- 建立 clone3（值為 3）
- visited = {node1: clone1, node2: clone2, node3: clone3}
- 開始處理 neighbors: [node2, node4]
```

**Step 5**：處理節點 2（node3 的第一個鄰居）

```text
- 已在 visited 中，直接返回 clone2
```

**Step 6**：處理節點 4（node3 的第二個鄰居）

```text
- 建立 clone4（值為 4）
- visited = {node1: clone1, node2: clone2, node3: clone3, node4: clone4}
- 開始處理 neighbors: [node1, node3]
```

**Step 7**：處理節點 1 和 3（node4 的鄰居）

```text
- 都已在 visited 中，直接返回對應的 clone
```

**完成**：所有節點處理完畢，返回 clone1

### 驗證結果

```csharp
Node clonedNode = program.CloneGraph(node1);

// 1. 節點值相同
Console.WriteLine(clonedNode.val);  // 輸出: 1

// 2. 鄰居節點數量和值正確
Console.WriteLine(clonedNode.neighbors.Count);      // 輸出: 2
Console.WriteLine(clonedNode.neighbors[0].val);     // 輸出: 2
Console.WriteLine(clonedNode.neighbors[1].val);     // 輸出: 4

// 3. 驗證是否為深度複製（記憶體位址不同）
Console.WriteLine(node1 != clonedNode);             // 輸出: True
Console.WriteLine(node2 != clonedNode.neighbors[0]); // 輸出: True
Console.WriteLine(node4 != clonedNode.neighbors[1]); // 輸出: True
```

## 執行專案

### 建構專案

```bash
dotnet build
```

### 執行程式

```bash
dotnet run --project leetcode_133/leetcode_133.csproj
```

或使用 VS Code 的內建任務：

- Build: `Cmd+Shift+B`
- Run: 使用 Run Task 選擇 "run"

### 預期輸出

```text
原始圖的第一個節點值: 1
複製圖的第一個節點值: 1
複製圖的鄰居節點數量: 2
複製圖的第一個鄰居節點值: 2
複製圖的第二個鄰居節點值: 4
是否為淺拷貝: False
是否為深度複製: True
```

## 關鍵要點

1. **使用 Dictionary 避免重複**：以節點物件為 key，確保唯一性
2. **先加入 Dictionary 再遞迴**：避免環形結構造成無限遞迴
3. **DFS 遍歷**：適合處理圖的深度結構和循環
4. **深度拷貝驗證**：使用 `!=` 比較物件參考，確認記憶體位址不同

## 技術棧

- .NET 8.0 / .NET 10.0
- C# 語言
- 資料結構：圖（Graph）
- 演算法：深度優先搜尋（DFS）
