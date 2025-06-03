# LeetCode 1298 - Maximum Candies You Can Get from Boxes

## 題目描述

你有 n 個盒子，標記從 0 到 n-1。給你四個陣列：status、candies、keys 和 containedBoxes，其中：

- `status[i]` 為 1 表示第 i 個盒子是開著的，為 0 表示第 i 個盒子是關著的
- `candies[i]` 是第 i 個盒子中的糖果數量
- `keys[i]` 是你開啟第 i 個盒子後可以開啟的其他盒子標籤列表
- `containedBoxes[i]` 是你在第 i 個盒子內找到的其他盒子列表

你會得到一個整數陣列 `initialBoxes`，包含你最初擁有的盒子標籤。

你可以拿取任何開著的盒子中的所有糖果，使用其中的鑰匙開啟新盒子，也可以使用你在其中找到的盒子。

**目標**：返回遵循上述規則後你能獲得的最大糖果數量。

## 題目連結

- [LeetCode 英文版](https://leetcode.com/problems/maximum-candies-you-can-get-from-boxes/description/?envType=daily-question&envId=2025-06-03)
- [LeetCode 中文版](https://leetcode.cn/problems/maximum-candies-you-can-get-from-boxes/description/?envType=daily-question&envId=2025-06-03)

## 解題思路

### 核心概念

1. **三種狀態追蹤**：
   - `canOpen[i]`：是否能開啟第 i 個盒子（有鑰匙或本身就是開啟的）
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

### 複雜度分析

- **時間複雜度**：O(n + k)，其中 n 是盒子數量，k 是所有鑰匙和包含盒子的總數
- **空間複雜度**：O(n)，用於儲存狀態陣列和 BFS 佇列

## 程式碼實作

### 核心函式

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

## 專案結構

```
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
```
status = [1,0,1,0], candies = [7,5,4,9], keys = [[],[],[1],[]], containedBoxes = [[1,2],[3],[],[]], initialBoxes = [0]
```

**輸出**：`16`

**說明**：
1. 開始時擁有盒子 0，可以開啟（status[0] = 1）
2. 從盒子 0 獲得：7 個糖果、盒子 1 和 2
3. 盒子 2 可以開啟，獲得：4 個糖果、盒子 1 的鑰匙
4. 用鑰匙開啟盒子 1，獲得：5 個糖果、盒子 3
5. 盒子 3 無法開啟（沒有鑰匙）
6. 總糖果數：7 + 4 + 5 = 16

### 範例 2

**輸入**：
```
status = [1,0,0,0], candies = [1,1,1,1], keys = [[],[],[],[]], containedBoxes = [[],[],[],[]], initialBoxes = [0]
```

**輸出**：`1`

**說明**：只能開啟盒子 0，獲得 1 個糖果。

## 關鍵程式設計要點

1. **狀態管理**：使用三個布林陣列清楚分離不同的盒子狀態
2. **BFS 策略**：確保所有可開啟的盒子都會被處理
3. **重複處理避免**：使用 `used` 陣列防止重複計算同一個盒子
4. **變數命名**：使用 `boxx` 避免與外層 `box` 變數衝突

## 參考資料

- [LeetCode 官方解題](https://leetcode.cn/problems/maximum-candies-you-can-get-from-boxes/solutions/101813/ni-neng-cong-he-zi-li-huo-de-de-zui-da-tang-guo-2/?envType=daily-question&envId=2025-06-03)
- [廣度優先搜尋演算法](https://zh.wikipedia.org/wiki/广度优先搜索)

## 授權

此專案僅用於學習目的。

---

**開發日期**：2025年6月3日  
**語言**：C# (.NET 8.0)  
**難度**：Hard  
**標籤**：Array, BFS, Graph
