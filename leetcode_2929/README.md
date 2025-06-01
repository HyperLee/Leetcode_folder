# Leetcode 2929 - 給小朋友們分糖果 II

## 題目說明

給定兩個正整數 n 和 limit，請回傳將 n 顆糖果分給 3 個小朋友，且每個小朋友最多只能拿 limit 顆糖果的所有分配方式總數。

- [LeetCode 英文題目連結](https://leetcode.com/problems/distribute-candies-among-children-ii/description/?envType=daily-question\&envId=2025-06-01)
- [LeetCode 中文題目連結](https://leetcode.cn/problems/distribute-candies-among-children-ii/description/?envType=daily-question\&envId=2025-06-01)

## 解題思路

本題屬於組合數學問題。枚舉第一個小朋友 a 可以拿的糖果數 (0\~min (n, limit))，剩下的糖果分給 b、c 兩人，並且每人最多只能拿 limit 顆。對於每個 a，b 的範圍是 max (0, remaining-limit) 到 min (limit, remaining)，每個合法 b 都對應唯一一個合法 c。將所有情況累加即為答案。

- 時間複雜度：O (min (n, limit))，只需枚舉 a 的所有可能值。
- 空間複雜度：O (1)，只用到常數記憶體。

## 詳細解釋

### 解題說明流程

1. **問題轉換**：
   - 題目要求將 n 顆糖果分給 3 個小朋友，且每人最多只能拿 limit 顆。
   - 這可以視為「有界整數分割」的組合數學問題。

2. **枚舉第一個小朋友 a**：
   - a 可以拿的糖果數為 0 \~ min (n, limit)。
   - 每次固定 a 後，剩下的糖果數為 remaining = n - a。

3. **計算第二個小朋友 b 的範圍**：
   - b 也必須在 0 \~ limit 之間，且 c = remaining - b 也要在 0 \~ limit。
   - 所以 b 的下界是 max (0, remaining - limit)，上界是 min (limit, remaining)。
   - 只要 b 在這個範圍內，c = remaining - b 一定合法。

4. **累加所有合法分配方式**：
   - 對每個 a，合法的 b 數量為 maxB - minB + 1。
   - 將所有 a 的情況累加，就是答案。

5. **程式碼實現**：
   - 使用 for 迴圈枚舉 a。
   - 每次計算 b 的範圍，累加到總數。
   - 時間複雜度 O (min (n, limit))，空間複雜度 O (1)。

---

## 範例程式碼 (C#)

```csharp
public long DistributeCandies(int n, int limit)
{
    long count = 0;
    for (int a = 0; a <= Math.Min(n, limit); a++)
    {
        int remaining = n - a;
        int minB = Math.Max(0, remaining - limit);
        int maxB = Math.Min(limit, remaining);
        long ways = Math.Max(0, maxB - minB + 1);
        count += ways;
    }
    return count;
}
```

## 重要變數說明

- `minB = Math.Max(0, remaining - limit)`：
  - b 的最小值，必須同時滿足 b ≥ 0 以及 c = remaining - b ≤ limit。
  - 取兩者最大值，確保 b 不會小於 0，也不會讓 c 超過 limit。

- `maxB = Math.Min(limit, remaining)`：
  - b 的最大值，不能超過每人上限 limit，也不能超過剩下的糖果數 remaining。
  - 取兩者最小值，確保 b 不會超過規則或剩餘糖果數的限制。

- `ways = Math.Max(0, maxB - minB + 1)`：
  - 合法的 b 的數量，等於從 minB 到 maxB (包含兩端) 之間的整數個數。
  - 若 minB > maxB，代表沒有合法分配，ways 會是 0。
  - 這樣設計可確保累加時不會出現負數。

---

## 測試資料

```
n=5, limit=2 => 6
n=10, limit=5 => 21
n=0, limit=0 => 1
n=7, limit=3 => 8
```

---

本專案為 Leetcode 2929 題的 C# 解法與說明，歡迎參考與討論。
