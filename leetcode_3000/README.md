# leetcode_3000

簡短說明

本專案收錄針對 LeetCode 題目 "3000. Maximum Area of Longest Diagonal Rectangle" 的示範程式（C#）。程式位於 `leetcode_3000/Program.cs`，並包含題目描述、解題說明、與幾組範例測資。

> [!note]
> 此 README 聚焦於解題思路與基礎數學說明，並示範如何在本專案中執行程式與驗證結果。

## 題目（重點摘錄）

- 輸入：二維整數陣列 `dimensions`，其中 `dimensions[i] = [length, width]` 代表第 i 個長方形。
- 要求：回傳對角線最長的長方形之面積；若存在多個對角線長度相同且為最長，則在這些長方形中回傳面積最大的那個。

## 基本數學（必備知識）

- 對角線（Diagonal）公式（勾股定理）
  - 一個長方形的對角線長度 d 可由長與寬求得：
    d = sqrt(length^2 + width^2)
  - 為避免浮點數比較與效能開銷，通常比較對角線長度時可以比較平方值：
    d^2 = length^2 + width^2

- 面積（Area）公式
  - 長方形面積 S = length * width

註：比較對角線長度時比較 d^2 而非 d，可避免呼叫 sqrt 並避免浮點誤差。

## 演算法說明（直觀）

1. 初始化兩個變數：`maxDiagonalSquared = 0` 與 `maxArea = 0`。
2. 對 `dimensions` 中的每一個 `[length, width]`：
   - 計算 `diagonalSquared = length*length + width*width`。
   - 計算 `area = length * width`。
   - 若 `diagonalSquared > maxDiagonalSquared`：
     - 更新 `maxDiagonalSquared = diagonalSquared`，並把 `maxArea = area`。
   - 否則若 `diagonalSquared == maxDiagonalSquared`：
     - 更新 `maxArea = max(maxArea, area)`（同對角線長度時取面積最大的）。
3. 遍歷結束後，`maxArea` 即為答案。

此方法僅需單趟掃描陣列，且只使用 O(1) 的額外空間。

## 時間與空間複雜度

- 時間複雜度：O(n)，n 為 `dimensions` 的長度（每個輸入元素只處理一次）。
- 空間複雜度：O(1)，僅使用常數數量的額外變數。

## 範例（與程式中測資一致）

- 範例 1
  - 輸入：[[9,3],[8,6]]
  - 對角線平方：9^2+3^2=90；8^2+6^2=100 → 第二個較大
  - 面積回傳：8*6 = 48

- 範例 2（對角線相同，取面積較大）
  - 輸入：[[5,5],[7,1]]
  - 對角線平方：50 與 50，同長 → 面積分別為 25 與 7，回傳 25

- 範例 3（單一長方形）
  - 輸入：[[4,7]] → 回傳 28

- 範例 4（多組輸入，含對角線相同）
  - 輸入：[[3,4],[6,8],[10,0]]
  - 對角線平方：25, 100, 100 → 兩個最大為 100，對應面積 48 與 0 → 回傳 48

## 專案結構（重要檔案）

- `leetcode_3000/Program.cs`：主程式，包含 `AreaOfMaxDiagonal` 方法與 `Main` 裡的示範測資。
- `leetcode_3000/leetcode_3000.csproj`：C# 專案檔。

## 如何執行

在 macOS（或其他環境）使用 dotnet CLI：

```bash
# 1) 建置專案
dotnet build /Users/qiuzili/Leetcode/Leetcode_folder/leetcode_3000/leetcode_3000.csproj

# 2) 執行（會跑 Main 中的示範測資並輸出結果）
dotnet run --project /Users/qiuzili/Leetcode/Leetcode_folder/leetcode_3000/leetcode_3000.csproj
```

## 驗證建議與測試

- 建議加入單元測試（xUnit / NUnit / MSTest）以涵蓋以下情境：
  - 空陣列或 null（視題意決定是否需處理）。
  - 單筆輸入。
  - 多筆輸入且多筆具有相同對角線平方值。
  - 長或寬為 0 的情況。

## 可能的延伸改進

- 加入輸入驗證（若題目保證輸入格式，則可省略）。
- 增加單元測試套件並把常見 edge case 加入 CI。
- 若要處理大量資料，可考慮流式讀取或分批計算，但本題本身為 O(n) 即已相當輕量。

## 結語

此專案示範一個簡潔且效能良好的解法：以平方和比較對角線長度，並在相同對角線長度時以面積作次要比較。若需要，我可以再幫你建立 xUnit 測試範本並加到專案中。
