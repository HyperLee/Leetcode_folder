# CountTrapezoids 方法說明

*說明者：GitHub Copilot（Raptor mini (Preview)）*

以下說明會依序覆蓋 `CountTrapezoids` 的功能、高階概念、程式細節、注意事項，以及可改善之處與範例程式碼。

---

## 1. 功能 / 高階概念

- **功能**：計算給定點集合中，可以形成多少個「梯形」。梯形（此題定義）要求 **恰好一對邊平行**（斜率相同但不共線），且不是平行四邊形。
- **演算法概念簡述**：
  1. 列舉所有的點對，形成所有線段並記錄其 **斜率**（slope）與 **截距**（intercept）。
  2. 記錄每條線段的 **中點** 與斜率（用以偵測平行四邊形，因為平行四邊形的對角線中點相同）。
  3. 計算在相同斜率下，具有不同截距的線段配對數（這些配對是梯形的一對平行邊候選），之後扣除那些其實構成平行四邊形的配對。

---

## 2. 變數與資料結構（初始化區塊）

- `n`：點的數量。
- `double MOD = 1e9 + 7`：目前程式把它當作「垂直線」的特殊斜率值（建議改為 `double.PositiveInfinity` 或更安全的符號化 key）。
- `slopeToIntercept`：`Dictionary<double, List<double>>` — key：斜率 `k`，value：該斜率下的截距 `b` 列表（同斜率但不同截距代表平行且不共線）。
- `midToSlope`：`Dictionary<double, List<double>>` — key：經編碼的中點 `mid`（目前實作是 `double`），value：該中點的多個斜率列表（用於檢測平行四邊形）。
- `res`：最終結果（梯形數目）。

---

## 3. 建立線段資訊（兩層 for 迴圈）

- 列舉每對點 `(i, j)`，計算 `dx = x1 - x2`, `dy = y1 - y2`。
- 斜率 `k` 的計算與垂直線處理：
  - 垂直線情況：`if (x2 == x1) k = MOD; b = x1`（以 `x` 當作截距表示）。
  - 否則：`k = (y2 - y1) / (x2 - x1)`。
- 截距 `b` 的計算（以代數等價形式提升數值穩定性）：
  - 標準形式：`b = y1 - k * x1`。
  - 另一種等價寫法：`b = (y1 * dx - x1 * dy) / dx`（dx = x1 - x2，dy = y1 - y2，與 `k` 的定義互換也相容）。
- 中點 `mid` 的編碼（當作 hash key）：
  - 程式中曾使用 `mid = (x1 + x2) * 10000.0 + (y1 + y2)`（注意：此編碼有 collision 與精度問題，建議改用 Tuple）。
- 把 `b` 加入 `slopeToIntercept[k]` 的列表，把 `k` 加入 `midToSlope[mid]` 的列表。

---

## 4. 計算梯形數（相同斜率但不同截距）

- 對 `slopeToIntercept` 的每一組同斜率資料：統計各個截距 `b` 的出現次數 `cnt[b]`。
- 兩兩不同 `b` 的配對數總和等於：`sum_{i < j} cnt_i * cnt_j`。
  - 程式上的等價做法：使用累加與乘積，如
    - `totalSum` 初始為 0；對每個 `count` 值：`res += totalSum * count; totalSum += count;`。
- 上述結果即為該斜率下所有不同截距的線段可構成的平行邊配對數（梯形候選）。

---

## 5. 扣除平行四邊形（相同中點但不同斜率）

- 對 `midToSlope` 每一組共同中點資料：統計每種斜率 `cnt[k]` 的出現次數。
- 若在相同中點出現多個斜率，代表有可能是平行四邊形（因為對角線的中點相同）。
- 該類 pair 在前面步驟可能被計為梯形候選，但若是平行四邊形就需要從結果中扣除。
- 扣除方式與上面相同：對斜率數量做 `sum_{i < j} cnt_i * cnt_j` 並從 `res` 減去該值。

---

## 6. 回傳結果

- 回傳變數 `res`（最終梯形數）。

---

## 7. 時間複雜度與空間複雜度

- 時間複雜度：O(n^2)（列舉所有點對，字典統計也至多 O(n^2)）。
- 空間複雜度：O(n^2)（需要儲存所有線段的斜率、截距與中點資訊）。

---

## 8. Gotchas（不那麼明顯但容易出錯的地方）

- **浮點數比較問題**：使用 `double` 做為字典 key（斜率與截距）可能因精度問題而導致相等數值被視為不同 key。
- **垂直線用 `MOD`**：把 `double MOD = 1e9 + 7` 當作垂直線斜率不是直覺做法，也可能造成 collision，建議改為 `double.PositiveInfinity` 或改成整數化 pair key。
- **中點編碼 collision**：`mid = (x1 + x2) * 10000.0 + (y1 + y2)` 可能在座標邊界下不唯一，且仍有精度風險；建議使用 `ValueTuple<int,int>` 或 `long` 做為 key。
- **dx、dy 的符號處理**：為了讓公式一致，可統一使用 `dx = x2 - x1`, `dy = y2 - y1`，避免 sign 的混淆。
- **輸入點數 < 4**：若點數小於 4，可在方法開頭直接 `return 0`（提早返回可節省運算）。

---

## 9. 改善建議（提高正確性與穩健性）

- 建議改用 **整數化**（Rational / fraction）做為鍵：
  - 使用 `g = gcd(dy, dx)` 約分，並把斜率 key 設為 `(dy/g, dx/g)`，避免浮點精度問題。
  - 截距 key 可用整數 pair `(numerator, denom)` 約分後表示。
- 中點建議使用 Tuple：`(x1 + x2, y1 + y2)`（整數化），而非 double 編碼。
- 垂直線處理：設 `slopeKey = (1, 0)`（或任意合法代表），不要用 `MOD` 常數。
- 若要節省記憶體或微優化：在建立 `midToSlope` 與 `slopeToIntercept` 時直接使用計數而不是先收集成 `List` 再統計。

---

## 10. 建議程式碼片段（示範用整數化鍵 & tuple 中點）

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

---

## 11. 其他小提示

- 如果要完全遵循 LeetCode 題意，建議在方法開頭加入 guard：`if (n < 4) return 0`。
- 全程避免用浮點數做為鍵，改用 `ValueTuple<int,int>` 或 `ValueTuple<long,long>` 以增加穩健性。
- 統計變數命名建議：`counts` 或 `interceptCounts` 比單純 `cnt` 更可讀。

---

## 總結

目前算法邏輯正確，可統計梯形數；但是使用 `double` 作為 key 的方法（斜率、截距與中點）會導致精度或碰撞問題。建議改為整數化（約分）或 Tuple 作為鍵，以提升穩健性與可讀性。

