# Leetcode 135: 分發糖果

## 題目說明

有 n 個小孩站成一排。給你一個整數陣列 ratings 表示每個小孩的評分。
你需要給這些小孩分發糖果，需遵守下列規則：

1. 每個小孩至少分到 1 顆糖果。
2. 評分較高的小孩必須比他相鄰的小孩分到更多糖果。
   請返回你需要準備的最少糖果數量，才能分發給這些小孩。

- 題目連結：[Leetcode 135. Candy](https://leetcode.com/problems/candy/description/)
- 中文連結：[Leetcode 135. 分發糖果](https://leetcode.cn/problems/candy/description/?envType=daily-question\&envId=2025-06-02)

## 解題思路

- 先初始化每個小孩 1 顆糖果。
- 從左到右遍歷，若 ratings[i] > ratings[i-1]，則 candies[i] = candies[i-1] + 1。
- 再從右到左遍歷，若 ratings[i] > ratings[i+1]，則 candies[i] = Math.Max(candies[i], candies[i+1] + 1)。
- 最後 candies 陣列總和即為最少所需糖果數。

## 為什麼需要兩次遍歷？

只用一次遍歷 (例如只從左到右) 無法同時滿足「每個小孩比左右鄰居評分高時糖果數都要多」的規則。

- 從左到右遍歷時，只能確保右邊比左邊分高時糖果數增加，無法保證左邊比右邊分高時左邊的糖果數也比右邊多。
- 從右到左遍歷時，只能確保左邊比右邊分高時糖果數增加，無法保證右邊比左邊分高時右邊的糖果數也比左邊多。

因此，必須先從左到右遍歷一次，再從右到左遍歷一次，兩次都檢查並更新糖果數，才能同時滿足所有規則，確保每個小孩都分到正確且最少的糖果。

## 如何判斷要用兩次遍歷？

遇到這類「每個元素要同時滿足左右鄰居」的題目時，通常可以考慮兩次遍歷：

1. **觀察題目規則**
   - 規則要求每個小孩都要同時考慮左邊和右邊的鄰居，這種「雙向依賴」通常需要兩次遍歷來分別處理每個方向的約束。
2. **舉例驗證**
   - 自己寫個小範例（如 [1, 2, 3, 2, 1]），用一次遍歷分配糖果，會發現有一邊的規則無法被滿足。
3. **參考經典解法**
   - 這類「每個元素要同時滿足左右條件」的問題，常見技巧就是「兩次遍歷」：
     - 第一次處理一個方向
     - 第二次反方向補足另一邊的規則

### 小結

當你遇到「每個元素要同時滿足左右鄰居」這種題目時，可以優先考慮「兩次遍歷」這個技巧。這是解決這類問題的常見且高效方法。

多做幾題 Leetcode 的類似題目，會更有感覺！

## 為什麼第二次遍歷要用 Math.Max？

在第二次從右到左遍歷時，必須用 `Math.Max(candies[i], candies[i+1] + 1)`，原因如下：

- 第一次從左到右已經根據「右邊比左邊分高」的規則分配過糖果，這時 candies[i] 可能已經大於 candies[i+1] + 1。
- 如果直接覆蓋 candies[i] = candies[i+1] + 1，會把第一次遍歷時分配的較多糖果覆蓋掉，導致左邊比右邊分高時糖果數不夠。
- 使用 `Math.Max` 可以保留兩次遍歷中較大的糖果數，確保同時滿足「比左鄰居高」和「比右鄰居高」的規則。

### 範例說明

假設 ratings = [1, 2, 3, 2, 1]

- 第一次從左到右：糖果分配 [1, 2, 3, 1, 1]
- 第二次從右到左：
  - i=3，ratings[3]=2 > ratings[4]=1，candies[3]=Math.Max(1,1+1)=2
  - i=2，ratings[2]=3 > ratings[3]=2，candies[2]=Math.Max(3,2+1)=3 (保留原本較大值)

這樣才能確保每個小孩都分到正確且最少的糖果。

### 範例說明

假設 ratings = [1, 3, 2, 2, 1]

- 從左到右：糖果分配 [1, 2, 1, 1, 1]
- 從右到左：糖果分配 [1, 2, 2, 1, 1]
- 最終結果取兩次遍歷的最大值 [1, 2, 2, 1, 1]，總和為 7

如果只用一次遍歷，會漏掉某些情況，導致分配不正確。

## 程式碼片段

```csharp
public int Candy(int[] ratings)
{
    int n = ratings.Length;
    if (n == 0) return 0;
    int[] candies = new int[n];
    for (int i = 0; i < n; i++) candies[i] = 1;
    for (int i = 1; i < n; i++)
        if (ratings[i] > ratings[i - 1]) candies[i] = candies[i - 1] + 1;
    for (int i = n - 2; i >= 0; i--)
        if (ratings[i] > ratings[i + 1]) candies[i] = Math.Max(candies[i], candies[i + 1] + 1);
    return candies.Sum();
}
```

## 測試資料

```
int[] ratings1 = {1, 0, 2}; // 預期: 5
int[] ratings2 = {1, 2, 2}; // 預期: 4
int[] ratings3 = {1, 3, 4, 5, 2}; // 預期: 11
int[] ratings4 = {5, 4, 3, 2, 1}; // 預期: 15
```

## 執行方式

1. 使用 .NET 8.0 執行本專案：
   ```sh
   dotnet run --project leetcode_135/leetcode_135.csproj
   ```
2. 終端機會顯示各組測試資料的最少糖果數。

## 檔案結構

- `Program.cs`：主程式與解題邏輯
- `leetcode_135.csproj`：專案檔

---

本專案僅用於 Leetcode 題目練習與學習參考。
