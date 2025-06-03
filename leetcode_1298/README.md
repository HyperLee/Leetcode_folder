# LeetCode 1298: Maximum Candies You Can Get from Boxes

## 題目描述

你有 n 個盒子，標記從 0 到 n-1。給你四個陣列：`status`、`candies`、`keys` 和 `containedBoxes`，其中：

- `status[i]` 為 1 表示第 i 個盒子是開著的，為 0 表示第 i 個盒子是關著的
- `candies[i]` 是第 i 個盒子中的糖果數量
- `keys[i]` 是你開啟第 i 個盒子後可以開啟的其他盒子標籤列表
- `containedBoxes[i]` 是你在第 i 個盒子內找到的其他盒子列表

你會得到一個整數陣列 `initialBoxes`，包含你最初擁有的盒子標籤。

你可以拿取任何開著的盒子中的所有糖果，使用其中的鑰匙開啟新盒子，也可以使用你在其中找到的盒子。

**返回遵循上述規則後你能獲得的最大糖果數量。**

## 解法概述

本專案提供兩種解法來解決此問題：

### 解法 1：廣度優先搜尋 (BFS)

使用佇列來進行廣度優先搜尋，追蹤三個重要狀態：

- `canOpen[]`：記錄每個盒子是否可以開啟 (有鑰匙或本身就是開啟的)
- `hasBox[]`：記錄是否擁有該盒子
- `used[]`：記錄該盒子是否已經被使用過

**核心邏輯**：只有當「擁有盒子」且「能開啟盒子」且「尚未使用」時，才能取得糖果。

### 解法 2：深度優先搜尋 (DFS)

使用遞迴方式處理每個盒子，將開著的盒子視為已有鑰匙。

**核心邏輯**：

- 遞迴處理：先找到鑰匙再開盒子，或先找到盒子再使用鑰匙
- 使用 `hasBox[x] = false` 避免重複訪問
- 使用 `ref int ans` 參數累積糖果總數，避免全域變數
- 將 `status` 陣列複製為 `hasKey` 陣列，把開著的盒子當作有鑰匙

**DFS 策略**：

1. 對每個可開啟的盒子呼叫 `Dfs()` 方法
2. 在 `Dfs()` 中累加糖果並標記為已使用
3. 遞迴處理新獲得的鑰匙和盒子
4. 使用 `ref int ans` 參數在遞迴中傳遞累計值

## 核心狀態管理說明

無論是 BFS 還是 DFS 解法，都需要正確管理三個關鍵狀態。理解這些狀態是解決此問題的核心：

### 🔑 三大核心狀態

#### 1. **鑰匙狀態 (Key State)**

- **BFS 中**：`canOpen[]` 陣列記錄每個盒子是否可以開啟
- **DFS 中**：`hasKey[]` 陣列記錄是否有某個盒子的鑰匙
- **作用**：決定我們是否有能力開啟特定盒子

#### 2. **盒子擁有狀態 (Box Possession State)**

- **兩種解法共用**：`hasBox[]` 陣列記錄是否實際擁有某個盒子
- **重要性**：即使有鑰匙，沒有盒子本體也無法取得糖果
- **來源**：初始盒子 + 從其他盒子中獲得的盒子

#### 3. **處理狀態 (Processing State)**

- **BFS 中**：`used[]` 陣列明確記錄已處理的盒子
- **DFS 中**：透過 `hasBox[x] = false` 隱式標記已處理
- **目的**：避免重複處理同一個盒子，防止無限循環

### 🎯 狀態交互邏輯

```text
能取得糖果的條件：
鑰匙狀態 ✓ AND 盒子擁有狀態 ✓ AND 未處理狀態 ✓
```

### 📊 狀態更新時機

#### **BFS 狀態更新流程**

```csharp
// 1. 初始化三種狀態
bool[] canOpen = new bool[n];    // 根據 status 初始化
bool[] hasBox = new bool[n];     // 根據 initialBoxes 初始化
bool[] used = new bool[n];       // 全部初始化為 false

// 2. 處理過程中更新狀態
canOpen[key] = true;             // 獲得新鑰匙
hasBox[newBox] = true;           // 獲得新盒子
used[currentBox] = true;         // 標記已處理
```

#### **DFS 狀態更新流程**

```csharp
// 1. 初始化兩種主要狀態
int[] hasKey = (int[])status.Clone();  // 初始鑰匙狀態
bool[] hasBox = new bool[n];            // 初始盒子擁有狀態

// 2. 處理過程中更新狀態
hasKey[y] = 1;                  // 獲得新鑰匙
hasBox[newBox] = true;          // 獲得新盒子
hasBox[x] = false;              // 隱式標記已處理
```

### 💡 關鍵洞察

1. **狀態分離的重要性**：鑰匙和盒子是兩個獨立的資源，必須同時擁有才能取得糖果
2. **處理狀態的必要性**：避免在有循環依賴時 (A 盒子包含 B 盒子，B 盒子包含 A 盒子的鑰匙) 造成無限處理
3. **狀態同步**：所有狀態更新必須保持一致性，確保邏輯正確

## 兩種解法詳細差異分析

### 🔍 核心演算法策略差異

#### **方法一：廣度優先搜尋 (BFS)**

```csharp
// 使用佇列進行層次遍歷
Queue<int> queue = new Queue<int>();
while (queue.Count > 0)
{
    int box = queue.Dequeue(); // 先進先出
    // 處理當前盒子的所有鑰匙和包含盒子
}
```

#### **方法二：深度優先搜尋 (DFS)**

```csharp
// 使用遞迴進行深度遍歷
private void Dfs(int x, ...)
{
    ans += candies[x];
    // 立即遞迴處理找到的鑰匙和盒子
    foreach (int y in keys[x])
    {
        if (hasBox[y]) Dfs(y, ...); // 遞迴呼叫
    }
}
```

### 🏗️ 狀態管理策略差異

#### **方法一：三狀態分離管理**

```csharp
bool[] canOpen = new bool[n];    // 是否可開啟
bool[] hasBox = new bool[n];     // 是否擁有盒子
bool[] used = new bool[n];       // 是否已使用
```

- **優點**：狀態清晰分離，邏輯易懂
- **特點**：需要三個陣列追蹤不同狀態

#### **方法二：簡化狀態管理**

```csharp
int[] hasKey = (int[])status.Clone(); // 鑰匙狀態(包含初始開啟狀態)
bool[] hasBox = new bool[n];           // 盒子擁有狀態
// 透過 hasBox[x] = false 避免重複處理
```

- **優點**：狀態管理更簡潔
- **特點**：透過修改 `hasBox` 避免重複訪問

### ⚡ 處理時機差異

#### **方法一：批次處理**

```csharp
// 先收集所有可處理的盒子到佇列
if (canOpen[box] && !used[box])
{
    queue.Enqueue(box); // 加入等待處理
}

// 然後統一批次處理
while (queue.Count > 0)
{
    // 處理佇列中的所有盒子
}
```

#### **方法二：即時處理**

```csharp
// 找到可處理的盒子立即遞迴處理
foreach (int y in keys[x])
{
    hasKey[y] = 1;
    if (hasBox[y]) 
    {
        Dfs(y, ...); // 立即遞迴處理，不等待
    }
}
```

### 🧠 記憶體使用模式差異

#### **方法一：佇列空間**

- **空間使用**：需要維護佇列儲存待處理盒子
- **記憶體模式**：廣度展開，同時在記憶體中保存多個待處理項目
- **記憶體峰值**：可能同時儲存多個層級的盒子

#### **方法二：堆疊空間**

- **空間使用**：遞迴呼叫使用系統堆疊
- **記憶體模式**：深度展開，一次只處理一條路徑
- **記憶體峰值**：遞迴深度決定堆疊使用量

### 🔄 狀態更新時機差異

#### **方法一：延遲更新**

```csharp
// 狀態更新與處理分離
canOpen[key] = true;              // 先更新狀態
if (hasBox[key] && !used[key])    // 再檢查是否處理
{
    queue.Enqueue(key);           // 延遲到下一輪處理
}
```

#### **方法二：即時更新與處理**

```csharp
// 狀態更新與處理同步
hasKey[y] = 1;        // 更新狀態
if (hasBox[y])        // 立即檢查
{
    Dfs(y, ...);      // 立即處理，無延遲
}
```

### 📈 執行流程差異

#### **方法一：層次式處理**

```text
初始盒子 → 佇列
├─ 處理第1層所有盒子
├─ 收集第2層待處理盒子 → 佇列
├─ 處理第2層所有盒子
└─ 繼續直到佇列為空
```

#### **方法二：路徑式處理**

```text
初始盒子 → 遞迴
├─ 處理盒子A → 找到鑰匙B → 立即處理B
│   └─ 處理盒子B → 找到盒子C → 立即處理C
└─ 回到上層繼續處理其他路徑
```

### ⚖️ 優缺點比較

| 特性         | BFS 方法一     | DFS 方法二    |
| ---------- | ----------- | ---------- |
| **程式碼複雜度** | 較複雜 (三狀態管理) | 較簡潔 (遞迴實作) |
| **記憶體使用**  | 佇列空間        | 堆疊空間       |
| **處理順序**   | 層次遍歷        | 深度遍歷       |
| **狀態追蹤**   | 明確分離        | 簡化管理       |
| **參數傳遞**   | 不涉及         | ref 參數傳遞    |
| **執行緒安全**  | 較低          | 較高 (無全域變數) |
| **除錯難度**   | 較容易追蹤狀態     | 遞迴除錯較複雜    |
| **執行效率**   | 相近          | 相近         |

### 🎯 適用場景建議

#### **選擇 BFS (方法一) 當**

- 需要明確的狀態追蹤
- 希望程式碼邏輯更清晰
- 需要容易除錯和維護

#### **選擇 DFS (方法二) 當**

- 偏好簡潔的程式碼實作
- 熟悉遞迴程式設計模式
- 需要執行緒安全的設計（避免全域變數）
- 記憶體限制較嚴格 (避免佇列額外空間)

兩種方法在時間複雜度和空間複雜度上都是  **O(n + k)**   和  **O(n)** ，主要差異在於實作風格和處理策略的不同。

## 複雜度分析

### 時間複雜度

- **O(n + k)** ：其中 n 是盒子數量，k 是所有鑰匙和包含盒子的總數

### 空間複雜度

- **BFS 解法**：O (n)，用於儲存狀態陣列和佇列
- **DFS 解法**：O (n)，用於儲存狀態陣列和遞迴堆疊

## 題目連結

- [LeetCode 英文版](https://leetcode.com/problems/maximum-candies-you-can-get-from-boxes/description/?envType=daily-question\&envId=2025-06-03)
- [LeetCode 中文版](https://leetcode.cn/problems/maximum-candies-you-can-get-from-boxes/description/?envType=daily-question\&envId=2025-06-03)

## 解題思路

### 核心概念

1. **三種狀態追蹤**：
   - `canOpen[i]`：是否能開啟第 i 個盒子 (有鑰匙或本身就是開啟的)
   - `hasBox[i]`：是否擁有第 i 個盒子
   - `used[i]`：第 i 個盒子是否已經被使用過

2. **開啟條件**：只有當「擁有盒子」且「能開啟盒子」且「尚未使用」時，才能取得糖果

3. **BFS 策略**：使用佇列進行廣度優先搜尋，每次處理一個可開啟的盒子

### 演算法步驟

1. **初始化**：建立三個布林陣列來追蹤盒子狀態
2. **預處理**：處理初始擁有的盒子，將可開啟的加入佇列
3. **BFS 遍歷**：
   - 處理佇列中的盒子
   - 獲得鑰匙：更新 `canOpen` 狀態，檢查是否可以開啟新盒子
   - 獲得新盒子：更新 `hasBox` 狀態，檢查是否可以立即開啟
4. **累計結果**：計算所有能開啟盒子的糖果總數

### DFS 演算法特點

1. **遞迴結構**：每個盒子的處理都是獨立的遞迴過程
2. **狀態管理**：透過修改 `hasBox` 和 `hasKey` 陣列管理狀態
3. **避免重複**：使用 `hasBox[x] = false` 防止重複處理

## 程式碼實作

### BFS 解法核心

```csharp
public int MaxCandies(int[] status, int[] candies, int[][] keys, int[][] containedBoxes, int[] initialBoxes)
{
    int n = status.Length;
    
    // 建立三個狀態追蹤陣列
    bool[] canOpen = new bool[n];
    bool[] hasBox = new bool[n];
    bool[] used = new bool[n];
    
    // 初始化可開啟狀態
    for (int i = 0; i < n; i++)
    {
        canOpen[i] = (status[i] == 1);
    }
    
    // BFS 處理
    Queue<int> queue = new Queue<int>();
    int res = 0;
    
    // ... 詳細實作請參考 Program.cs
    
    return res;
}
```

### DFS 解法核心

```csharp
public int MaxCandies2(int[] status, int[] candies, int[][] keys, int[][] containedBoxes, int[] initialBoxes)
{
    int ans = 0; // 使用區域變數取代全域變數
    int[] hasKey = (int[])status.Clone(); // 把開著的盒子當作有鑰匙
    bool[] hasBox = new bool[status.Length]; // 記錄是否擁有該盒子
    
    // 標記初始擁有的盒子
    foreach (int x in initialBoxes)
    {
        hasBox[x] = true;
    }

    // 處理初始擁有且可以開啟的盒子
    foreach (int x in initialBoxes)
    {
        if (hasBox[x] && hasKey[x] == 1)
        {
            Dfs(x, candies, keys, containedBoxes, hasKey, hasBox, ref ans);
        }
    }
    
    return ans;
}

private void Dfs(int x, int[] candies, int[][] keys, int[][] containedBoxes, 
                 int[] hasKey, bool[] hasBox, ref int ans)
{
    ans += candies[x]; // 使用 ref 參數累加糖果
    hasBox[x] = false; // 避免重複訪問
    
    // ... 詳細實作請參考 Program.cs
}
```

## 專案結構

```text
leetcode_1298/
├── README.md                    # 專案說明文件
├── leetcode_1298.sln           # Visual Studio 解決方案檔案
└── leetcode_1298/
    ├── leetcode_1298.csproj    # 專案設定檔案
    ├── Program.cs              # 主要程式碼實作
    ├── bin/                    # 編譯輸出目錄
    └── obj/                    # 建構輸出目錄
```

## 執行方式

### 前置需求

- .NET 8.0 或更高版本
- C# 編譯器

### 編譯和執行

```bash
# 進入專案目錄
cd leetcode_1298

# 建構專案
dotnet build

# 執行專案
dotnet run
```

或者使用 Visual Studio：

1. 開啟 `leetcode_1298.sln`
2. 按 F5 執行專案

## 測試範例

### 範例 1

**輸入**：

```text
status = [1,0,1,0], candies = [7,5,4,9], keys = [[],[],[1],[]], containedBoxes = [[1,2],[3],[],[]], initialBoxes = [0]
```

**輸出**：`16`

**說明**：

1. 開始時擁有盒子 0，可以開啟 (status \[0] = 1)
2. 從盒子 0 獲得：7 個糖果、盒子 1 和 2
3. 盒子 2 可以開啟，獲得：4 個糖果、盒子 1 的鑰匙
4. 用鑰匙開啟盒子 1，獲得：5 個糖果、盒子 3
5. 盒子 3 無法開啟 (沒有鑰匙)
6. 總糖果數：7 + 4 + 5 = 16

### 範例 2

**輸入**：

```text
status = [1,0,0,0], candies = [1,1,1,1], keys = [[],[],[],[]], containedBoxes = [[],[],[],[]], initialBoxes = [0]
```

**輸出**：`1`

**說明**：只能開啟盒子 0，獲得 1 個糖果。

## 關鍵程式設計要點

1. **狀態管理**：使用三個布林陣列清楚分離不同的盒子狀態
2. **BFS 策略**：確保所有可開啟的盒子都會被處理
3. **重複處理避免**：使用 `used` 陣列防止重複計算同一個盒子
4. **變數命名**：使用 `boxx` 避免與外層 `box` 變數衝突
5. **DFS 遞迴**：透過 `ref` 參數和狀態修改實現深度優先搜尋
6. **執行緒安全**：DFS 解法使用區域變數和 `ref` 參數，避免全域狀態

## 參考資料

- [LeetCode 官方解題](https://leetcode.cn/problems/maximum-candies-you-can-get-from-boxes/solutions/101813/ni-neng-cong-he-zi-li-huo-de-de-zui-da-tang-guo-2/?envType=daily-question\&envId=2025-06-03)
- [廣度優先搜尋演算法](https://zh.wikipedia.org/wiki/广度优先搜索)
- [深度優先搜尋演算法](https://zh.wikipedia.org/wiki/深度优先搜索)

## 授權

此專案僅用於學習目的。

---

**開發日期**：2025 年 6 月 3 日\
**語言**：C# (.NET 8.0)\
**難度**：Hard\
**標籤**：Array, BFS, DFS, Graph
