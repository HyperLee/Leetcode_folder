GitHub Copilot（說明者：Raptor mini (Preview)）

下面依序針對 CountTrapezoids 方法做分區塊詳細說明、數學公式與注意事項，並提供可讀性與穩健性的改善建議與程式碼片段。

1) 功能/高階概念  
- 功能：計算給定點集合能形成多少個「梯形」。梯形定義：恰好一對邊平行（斜率相同但不共線），且非平行四邊形。  
- 演算法概念：  
  1. 列舉所有點對形成線段，記錄斜率與截距（用於查找「平行但不共線」的線段組合）。  
  2. 也記錄線段中點與斜率（用於檢測被誤算為梯形，但實際是平行四邊形的情況）。  
  3. 計算相同斜率但不同截距的配對數（梯形候選），再扣除相同中點但不同斜率的配對（平行四邊形）。

2) 變數與資料結構初始化區塊  
- n: 點數。  
- double MOD = 1e9 + 7: 目前程式把它當作「垂直線」的特殊斜率值。建議改為 double.PositiveInfinity 或使用符號化 key（見下方建議）。  
- slopeToIntercept: Dictionary<double, List<double>> — key：斜率 k，value：該斜率下的截距 b 列表（同斜率但不同截距代表平行且不共線）。  
- midToSlope: Dictionary<double, List<double>> — key：編碼過的中點 mid（以 double 編碼），value：該中點的多個斜率列表（用於檢測平行四邊形，因為平行四邊形的對角線中點相同）。  
- res: 最終結果（梯形數）。

3) 建立線段資訊（兩層 for 迴圈）  
- 列舉每對點 (i, j): 計算 dx = x1 - x2, dy = y1 - y2。  
- 斜率 k 計算與處理垂直線：  
  - 垂直線情況：if (x2 == x1) k = MOD；b = x1（把截距以 x 表示）。  
  - 否則：$k = \dfrac{y_2 - y_1}{x_2 - x_1}$.  
- 截距 b（此方法用代數等價公式提高數值穩定性）：
  - 計算方式：$b = y_1 - k\cdot x_1$，程式中等價寫為 $b = \dfrac{y_1 \cdot dx - x_1 \cdot dy}{dx}$（dx = x1 - x2, dy = y1 - y2，這個寫法與 k 的定義可互換，數學上是對的）。  
- 中點 mid 的編碼（為 hash key）：  
  - 程式中採 $mid = (x_1 + x_2)\cdot 10000.0 + (y_1 + y_2)$ 作為 double 編碼。  
- 把 b 加到 slopeToIntercept[k] 列表，把 k 加到 midToSlope[mid] 列表。

4) 計算梯形數（相同斜率不同截距）  
- 遍歷 slopeToIntercept 的各 group（同斜率的所有線段）：  
  - 統計各截距 b 的出現次數 cnt[b]。  
  - 計算不同 b 之間能配對的數量：等價於 sum_{i<j} cnt_i * cnt_j。程式用累積乘法做：  
    - totalSum 初始為 0；對於每個 cnt 值（count）： res += totalSum * count; totalSum += count。  
  - 這會計算所有在此斜率下 數量大於 1 且截距不同的線段配對數（即候選梯形上平行邊對的數量）。

5) 扣除平行四邊形（相同中點但不同斜率）  
- 遍歷 midToSlope 的各 group（共同中點的線段）：  
  - 統計每種斜率出現的次數 cnt[k]。  
  - 若多個斜率在同一中點出現，代表可能構成平行四邊形（因為平行四邊形的對角線交於同一個中點）。  
  - 這些 pair 在前面步驟被算為「相同斜率不同截距」的候選梯形；但如果是平行四邊形就要扣除，因此計算不同斜率配對數後從 res 扣除（同樣使用累積乘法）。

6) 返回結果 res

7) 時間與空間複雜度  
- 時間：O(n^2)（列舉所有點對），字典統計也取 O(n^2)。  
- 空間：O(n^2)（需要儲存所有線段的斜率、截距、中點資訊）。

8) Gotchas（不那麼明顯但容易出錯的地方）
- 浮點比對問題：使用 double 作為 dictionary 的 key（slope 與截距）會有精度問題，可能使實際數值相等但在 double 比較下被視為不同 key。  
- 垂直線 k 使用 MOD（1e9+7）代表並不夠直觀；而且若 x 欄位很大有 collision 風險。建議用 double.PositiveInfinity 或利用整數化（有向分數 / 約分）建立有理數 key。  
- 中點編碼可能發生 collision：mid = (x1+x2)*10000.0 + (y1+y2) 在座標上限不保證唯一；還有 double 也有精度問題。使用 ValueTuple<int,int> 或長整數 (long) 組成鍵會安全很多。  
- dx 與 slope 計算的 sign：程式內在 slope 與 dx,dy 的正負處理上是正確的（因為用代數等價），但容易混淆，建議統一使用 (dx = x2 - x1, dy = y2 - y1) 使公式語意直觀。  
- 若輸入點數 < 4，要考慮早回傳 0（雖然目前算法因為雙重迴圈不會出錯，但清楚的 early return 可以節省運算）。

9) 改善建議（可提高正確性與穩健性）
- 使用整數化的鍵（使用值整除 gcd，建立 slopeKey 為 (dy/g, dx/g)，interceptKey 為 (numerator, denominator) 經約分後的 pair），避免浮點精度問題。  
- 中點使用 tuple 鍵 (x1+x2, y1+y2) 或 long Key（按位合併）代替 double。  
- 把垂直線直接對應 slopeKey = (1,0) 或 dx == 0 設定（不使用 MOD）。  
- 如果需要更高效率可以在建置 midToSlope 或 slopeToIntercept 時直接用 Count 統計（避免 list 再次掃描）。

10) 建議程式碼片段（示範用整數化鍵 & tuple 中點）  
```csharp
// ...existing code...
// 建議替換原本 slope 與 mid 的處理，使用整數 pair key 避免 double 精度問題
(int dy, int dx) NormalizeSlope(int dy, int dx)
{
    if (dx == 0) return (1, 0); // 垂直線
    if (dy == 0) return (0, 1); // 水平線
    int g = Math.Abs(System.Numerics.BigInteger.GreatestCommonDivisor(dy, dx));
    dy /= g;
    dx /= g;
    if (dx < 0) { dy = -dy; dx = -dx; } // 統一符號
    return (dy, dx);
}

long InterceptKey(int x1, int y1, int dx, int dy) {
    // 把截距 b 的分子表示成整數：b = (x1*y2 - x2*y1)/(x1 - x2) 等價
    // 這裡使用 "cross" 方式： interceptNumerator = (long)x1 * dy - (long) y1 * dx
    // 以 (numerator, dx) 綁成 pair 或做約分表示。示例把 pair 組成 long key（小心 overflow）
    long numerator = (long)y1 * dx - (long)x1 * dy; // numerator，與 dx 共用分母
    long denom = dx; // 不是零
    if (denom < 0) { numerator = -numerator; denom = -denom; } // 統一分母正號
    long g = (long)System.Numerics.BigInteger.GreatestCommonDivisor(Math.Abs(numerator), Math.Abs(denom));
    numerator /= g; denom /= g;
    // 任意把 pair 打包成 long key（下列僅示例，實際要避免 overflow）
    return (numerator << 32) ^ denom;
}
// ...existing code...
```

11) 其他小提示  
- 如果要遵從 LeetCode 題意，應在 method 開頭加入若 n < 4 return 0 的 guard。  
- 程式內多處以 double 當 key，可改用 ValueTuple<int,int> 或 ValueTuple<long,long>，提高確定性。  
- 在統計的地方，請特別注意變數命名：如 cnt (dictionary) 可改為 counts 或 interceptCounts 以提高可讀性。

總結：目前程式邏輯正確且能計數；主風險在於 double 做為 key（斜率/截距/中點），可能因數值精度與編碼碰撞而導致結果不穩定。建議採用整數化（約分）或 Tuple 作為鍵值以提高穩健性與可讀性。