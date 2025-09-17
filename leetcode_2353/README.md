# LeetCode 2353: Design a Food Rating System

> **中文名稱**: 設計食物評分系統  
> **難度**: Medium  
> **標籤**: Design, Hash Table, Heap (Priority Queue), Ordered Set

一個高效能的食物評分系統，支援動態更新評分並快速查詢特定料理類型中評分最高的食物。本專案提供兩種不同的解法實作，展示不同資料結構的設計權衡。

## 問題描述

設計一個食物評分系統，可以執行以下操作：

1. **修改評分**: 更新系統中食物項目的評分
2. **查詢最高評分**: 返回指定料理類型中評分最高的食物項目

### API 規格

```csharp
// 初始化系統
FoodRatings(string[] foods, string[] cuisines, int[] ratings)

// 更改食物評分
void changeRating(string food, int newRating)

// 查詢特定料理類型的最高評分食物
string highestRated(string cuisine)
```

### 特殊規則

- 若存在評分相同的食物，返回**字典序較小**的食物名稱
- 所有輸入的食物名稱保證唯一

## 解法概覽

本專案實作了兩種不同的解決方案，各有其優勢與適用場景：

| 解法 | 核心資料結構 | 查詢複雜度 | 更新複雜度 | 空間特性 |
|------|-------------|-----------|-----------|----------|
| **解法一** | `SortedSet` + 自訂比較器 | O(1) | O(log n) | 穩定 |
| **解法二** | `PriorityQueue` + Lazy Deletion | O(1)* | O(log n) | 動態增長 |

*註：解法二在最壞情況下可能需要多次 dequeue 操作

## 解法一：SortedSet + 自訂比較器

### 核心思想

使用 `SortedSet<FoodEntry>` 維護每個料理類型的食物，透過自訂比較器確保集合按評分（高到低）和食物名稱（字典序）排序。

### 資料結構設計

```csharp
class FoodRatings
{
    // 食物 -> 料理類型映射
    private readonly Dictionary<string, string> foodToCuisine;
    
    // 食物 -> 當前評分映射
    private readonly Dictionary<string, int> foodToRating;
    
    // 料理類型 -> 有序食物集合
    private readonly Dictionary<string, SortedSet<FoodEntry>> cuisineSets;
}
```

### 自訂比較器邏輯

```csharp
private class FoodEntryComparer : IComparer<FoodEntry>
{
    public int Compare(FoodEntry? x, FoodEntry? y)
    {
        // 首先比較評分（高分優先）
        int cmp = y.Rating.CompareTo(x.Rating);
        if (cmp != 0) return cmp;
        
        // 評分相同時，字典序小的優先
        return string.CompareOrdinal(x.Name, y.Name);
    }
}
```

### 操作實作

#### 初始化
```csharp
public FoodRatings(string[] foods, string[] cuisines, int[] ratings)
{
    var comparer = new FoodEntryComparer();
    
    for (int i = 0; i < foods.Length; i++)
    {
        string food = foods[i];
        string cuisine = cuisines[i];
        int rating = ratings[i];
        
        foodToCuisine[food] = cuisine;
        foodToRating[food] = rating;
        
        if (!cuisineSets.TryGetValue(cuisine, out var set))
        {
            set = new SortedSet<FoodEntry>(comparer);
            cuisineSets[cuisine] = set;
        }
        
        set.Add(new FoodEntry(food, rating));
    }
}
```

#### 更新評分
```csharp
public void changeRating(string food, int newRating)
{
    var cuisine = foodToCuisine[food];
    var oldRating = foodToRating[food];
    var set = cuisineSets[cuisine];
    
    // 移除舊項目（需建立相同的 FoodEntry 物件）
    set.Remove(new FoodEntry(food, oldRating));
    
    // 更新評分並加入新項目
    foodToRating[food] = newRating;
    set.Add(new FoodEntry(food, newRating));
}
```

#### 查詢最高評分
```csharp
public string highestRated(string cuisine)
{
    if (!cuisineSets.TryGetValue(cuisine, out var set) || set.Count == 0)
        return string.Empty;
    
    // 直接取得集合中的最小元素（因比較器將最高評分放在最前面）
    var first = set.Min;
    return first?.Name ?? string.Empty;
}
```

### 優勢分析

✅ **查詢效能優異**: `highestRated` 操作為 O(1)，直接存取集合首元素  
✅ **記憶體穩定**: 每個食物只佔用一個 `FoodEntry`，無冗余資料  
✅ **實作直觀**: 利用 .NET 內建的 `SortedSet` 自動維護排序  
✅ **強型別安全**: 自訂類別提供良好的封裝性

### 挑戰與注意事項

⚠️ **物件等價性**: `Remove` 操作需建立具有相同評分的 `FoodEntry` 物件  
⚠️ **更新成本**: 每次評分更新需要兩次 O(log n) 操作（移除 + 插入）  
⚠️ **比較器複雜度**: 需正確實作比較邏輯以避免集合行為異常

## 解法二：PriorityQueue + Lazy Deletion

### 核心思想

使用 `PriorityQueue` 為每個料理類型維護食物優先佇列，採用「延遲刪除」策略避免直接從佇列中移除過期項目。

### 資料結構設計

```csharp
public class FoodRatings2
{
    // 食物 -> (評分, 料理類型) 映射
    private Dictionary<string, (int Rating, string Cuisine)> foodMap;
    
    // 料理類型 -> 優先佇列
    private Dictionary<string, PriorityQueue<(string Food, int Rating), (int Rating, string Food)>> ratingMap;
}
```

### 優先佇列比較器

```csharp
// 建立優先佇列時的比較器
Comparer<(int Rating, string Food)>.Create((a, b) =>
{
    if (a.Rating != b.Rating)
    {
        return b.Rating.CompareTo(a.Rating); // 高評分優先
    }
    return a.Food.CompareTo(b.Food); // 字典序小的優先
})
```

### 操作實作

#### 初始化
```csharp
public FoodRatings2(string[] foods, string[] cuisines, int[] ratings)
{
    foodMap = new Dictionary<string, (int Rating, string Cuisine)>();
    ratingMap = new Dictionary<string, PriorityQueue<(string Food, int Rating), (int Rating, string Food)>>();
    
    for (int i = 0; i < foods.Length; i++)
    {
        string food = foods[i];
        string cuisine = cuisines[i];
        int rating = ratings[i];
        
        foodMap[food] = (rating, cuisine);
        
        if (!ratingMap.ContainsKey(cuisine))
        {
            ratingMap[cuisine] = new PriorityQueue<(string Food, int Rating), (int Rating, string Food)>(comparer);
        }
        
        ratingMap[cuisine].Enqueue((food, rating), (rating, food));
    }
}
```

#### 更新評分（無刪除策略）
```csharp
public void ChangeRating(string food, int newRating)
{
    var (oldRating, cuisine) = foodMap[food];
    
    // 直接加入新的評分項目（不移除舊項目）
    ratingMap[cuisine].Enqueue((food, newRating), (newRating, food));
    
    // 更新當前評分記錄
    foodMap[food] = (newRating, cuisine);
}
```

#### 查詢最高評分（延遲清理）
```csharp
public string HighestRated(string cuisine)
{
    var q = ratingMap[cuisine];
    
    while (q.Count > 0)
    {
        var top = q.Peek();
        string food = top.Food;
        int rating = top.Rating;
        
        // 檢查佇列頂端的評分是否為當前有效評分
        if (foodMap[food].Rating == rating)
        {
            return food; // 找到有效的最高評分食物
        }
        
        // 移除過期項目並繼續搜尋
        q.Dequeue();
    }
    
    return string.Empty;
}
```

### 延遲刪除機制

這是解法二的核心創新點：

1. **更新時**: 不刪除舊的佇列項目，僅加入新項目
2. **查詢時**: 檢查佇列頂端項目是否有效，無效則移除並繼續
3. **記憶體管理**: 透過查詢操作逐步清理過期項目

### 優勢分析

✅ **實作簡潔**: 避免複雜的佇列內刪除操作  
✅ **更新高效**: `ChangeRating` 只需單次 O(log n) 插入  
✅ **穩定性佳**: 不需處理複雜的物件等價性問題  
✅ **官方推薦**: LeetCode 官方解法，經過廣泛驗證

### 挑戰與權衡

⚠️ **記憶體開銷**: 可能累積大量過期項目  
⚠️ **查詢延遲**: 最壞情況下需要多次 dequeue 操作  
⚠️ **不可預測性**: 查詢時間取決於過期項目數量

## 深度比較分析

### 時間複雜度分析

| 操作 | 解法一 (SortedSet) | 解法二 (PriorityQueue) |
|------|-------------------|----------------------|
| **初始化** | O(n log n) | O(n log n) |
| **changeRating** | O(log n) | O(log n) |
| **highestRated** | O(1) | O(1) 平均，O(k log n) 最壞* |

*其中 k 為過期項目數量

### 空間複雜度分析

| 解法 | 空間使用 | 記憶體特性 |
|------|----------|-----------|
| **解法一** | O(n) | 穩定，每食物一項 |
| **解法二** | O(n + m) | 動態，m 為歷史更新次數 |

### 效能特性對比

#### 查詢效能
- **解法一**: 保證 O(1) 查詢，適合查詢頻繁的場景
- **解法二**: 平均 O(1)，但可能因過期項目導致延遲

#### 更新效能
- **解法一**: 穩定的 O(log n)，但需要兩次樹操作
- **解法二**: 單次 O(log n) 插入，效率略高

#### 記憶體使用
- **解法一**: 記憶體使用穩定且可預測
- **解法二**: 記憶體使用隨更新頻率增長

### 程式碼品質比較

#### 可讀性
- **解法一**: 自訂比較器邏輯清晰，物件導向設計
- **解法二**: 延遲刪除概念需要註釋說明

#### 可維護性  
- **解法一**: 封裝性良好，容易除錯和測試
- **解法二**: 邏輯相對簡單，但記憶體行為較難預測

#### 擴展性
- **解法一**: 易於加入新功能（如範圍查詢）
- **解法二**: 適合需要頻繁更新的場景

### 適用場景建議

#### 選擇解法一的情況
- 查詢操作遠多於更新操作
- 對記憶體使用有嚴格要求
- 需要穩定可預測的效能
- 系統長時間運行

#### 選擇解法二的情況  
- 更新操作頻繁
- 實作簡潔性優先
- 記憶體充足的環境
- 短期運行或週期性重啟

## 執行與測試

### 建置專案

```bash
# 複製專案
git clone <repository-url>
cd leetcode_2353

# 建置專案
dotnet build

# 執行範例
dotnet run
```

### 執行結果

程式會執行兩種解法的測試案例：

```
kimchi       # 解法一：韓式料理最高評分
ramen        # 解法一：日式料理最高評分  
sushi        # 解法一：更新壽司評分後的結果

-- 解法二 開始 --
kimchi       # 解法二：相同的測試結果
ramen
sushi
-- 解法二 結束 --
```

### 測試案例說明

```csharp
string[] foods = { "kimchi", "miso", "sushi", "ramen", "bulgogi" };
string[] cuisines = { "korean", "japanese", "japanese", "japanese", "korean" };
int[] ratings = { 9, 12, 8, 15, 7 };

// 初始狀態：
// Korean: kimchi(9), bulgogi(7) -> kimchi 獲勜
// Japanese: miso(12), sushi(8), ramen(15) -> ramen 獲勝

// 更新 sushi 評分為 16 後：
// Japanese: miso(12), sushi(16), ramen(15) -> sushi 獲勝
```

## 技術細節

### 開發環境
- **.NET 8.0**: 使用最新的 C# 13 功能
- **Nullable Reference Types**: 啟用空值安全檢查
- **Implicit Usings**: 簡化 using 語句

### 關鍵設計決策

1. **字串比較**: 使用 `string.CompareOrdinal` 確保穩定的字典序排序
2. **空值處理**: 完整的 null 檢查和空集合處理
3. **封裝性**: 適當的存取修飾符和 readonly 欄位
4. **效能最佳化**: 避免不必要的物件分配

## 總結

兩種解法各有優勢，選擇應基於具體需求：

- **追求穩定效能和記憶體效率** → 選擇解法一
- **偏好實作簡潔和高更新頻率** → 選擇解法二

這個專案展示了在面對同一問題時，不同資料結構選擇如何影響系統的效能特性和設計複雜度。理解這些權衡對於設計高品質的系統架構至關重要。

---

> **LeetCode 連結**: [2353. Design a Food Rating System](https://leetcode.com/problems/design-a-food-rating-system/)
