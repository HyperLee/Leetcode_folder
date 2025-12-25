# leetcode_3075 — Maximize Happiness of Selected Children (幸福值最大化)

**短述**

此專案包含了對 LeetCode 題目 **3075. Maximize Happiness of Selected Children** 的解法。
程式以 C# 實作，並包含簡潔範例測試與說明，說明如何在 k 回合內選出小孩以最大化被選中小孩的快樂值總和。

---

## 題目說明 ✏️

給定長度為 n 的整數陣列 `happiness` 與正整數 `k`。
有 n 位小孩，第 i 位小孩的快樂值為 `happiness[i]`。
每回合你要選出一位尚未被選中的小孩；當選擇某位小孩後，其他尚未被選中的每位小孩的快樂值都會減少 1（但不會低於 0）。
在恰好進行 k 次選擇下，請回傳被選中的小孩快樂值總和的最大值。

題目連結：
- https://leetcode.com/problems/maximize-happiness-of-selected-children/
- https://leetcode.cn/problems/maximize-happiness-of-selected-children/

---

## 關鍵想法與策略 💡

- 對快樂值陣列由大到小排序（遞減）。
- 在每一回合選擇尚未被選中中目前**最大的快樂值**是最佳策略（貪心）。
- 如果排序後的第 i 個元素（0-index）為 `a[i]`，則當它在第 i 次被選中時，實得值為 `max(0, a[i] - i)`，因為前面 i 次選擇會把它減少 i 次（但不低於 0）。
- 因此僅需對排序後的前 k 個元素，累加 `max(0, a[i] - i)` 即可（當某項為 0 或負數時可以提前停止）。

> [!NOTE]
> 貪心直覺：每次都選最大的能讓總和最大化，因為每次選擇都平均造成剩餘每位 -1 的影響，早選大的能保存更多的價值。

---

## 演算法步驟（實作重點） 🔧

1. 檢查邊界條件：若 `happiness` 為空或 `k <= 0`，回傳 0。
2. 把 `happiness` 排序成遞減。
3. 迴圈 i = 0..k-1（但不超過陣列長度）：
   - 計算 `val = happiness[i] - i`.
   - 若 `val <= 0`，可直接跳出迴圈（後續不會有正值）。
   - 否則把 `val` 累加到答案。
4. 回傳累加結果（使用 long 以避免溢位）。

時間複雜度：O(n log n)（排序）；空間複雜度：O(1)（排序就地）。

---

## 範例與說明 📘

範例測試（程式 `Program.cs` 中包含）：

```text
Test 1: happiness=[5,2,2], k=2, expected=6, actual=6
Test 2: happiness=[1,1,1], k=3, expected=1, actual=1
Test 3: happiness=[5,0,0], k=2, expected=5, actual=5
Test 4: happiness=[4,3,3,2], k=3, expected=7, actual=7
Test 5: happiness=[10,8,6,4,2], k=5, expected=22, actual=22
```

步驟演示（範例：`happiness = [4,3,3,2], k=3`）：

1. 排序（遞減）：`[4,3,3,2]`。
2. i=0：取 4 - 0 = 4 => 累加 4。
3. i=1：取 3 - 1 = 2 => 累加 2（總和 6）。
4. i=2：取 3 - 2 = 1 => 累加 1（總和 7）。
5. 回傳 7。

---

## 如何執行與檢視輸出 ▶️

在專案根目錄下執行：

```bash
dotnet run --project ./leetcode_3075 -c Debug
```

會輸出上面範例的測試結果。

---

## 補充與延伸 💬

- 若題目有更嚴格的限制或需要支援大量資料，可考慮使用線性時間選擇演算法（如部分排序）來找到前 k 大，但簡潔與效能的平衡下，直接排序通常足夠。

---

如果你要我把 README 翻成英語、補上圖示、或加入更多測試案例，我可以再幫你更新。✅
