# 🔢 LeetCode 2154 – Keep Multiplying Found Values by Two

> [!NOTE]
> 此專案以 .NET 8 為基礎，示範如何在 C# 13 中實作 LeetCode 2154 題目「Keep Multiplying Found Values by Two」。

## 專案亮點

- 以簡潔程式碼實作兩種演算法（HashSet 與排序掃描）。
- `Program.Main` 內建測試資料，方便即時檢驗答案。
- 具備完整說明：題目背景、解題直覺、時間/空間複雜度與方案比較。

## 題目說明

給定整數陣列 `nums` 與起始值 `original`，每當 `original` 出現在陣列中就將其乘以 2，並用新的值持續搜尋；直到 `original` 不再被找到為止，回傳當下的 `original`。題目核心在於如何高效率偵測「目前的 `original` 是否存在」。

## 解題概念

1. 將陣列轉換為快速查找資料結構（HashSet）或透過排序改以線性掃描。
2. 每次命中即時倍增 `original`，並立即再次檢查。
3. 無須記錄元素使用次數，因為倍增後的新值會重新觸發搜尋。

## 解法一：HashSet（`FindFinalValue`）

- **思路**：把 `nums` 建立成 `HashSet<int>`，利用平均 O(1) 的查找時間，持續檢查 `original` 是否存在；存在就倍增並重複。
- **時間複雜度**：O(n) 建表 + O(k) 查找，k 為倍增次數（最多 log₂(max(nums))+1）。
- **空間複雜度**：O(n) 用於 HashSet。
- **適用場景**：需要最快查找速度、可接受額外記憶體成本。

## 解法二：排序掃描（`FindFinalValue_Array`）

- **思路**：先就地排序 `nums`，再以單次 for 迴圈掃描。若掃描到的值等於 `original` 就倍增，並繼續往後，比對過的值不需回頭。
- **時間複雜度**：O(n log n) 排序 + O(n) 掃描。
- **空間複雜度**：視排序實作而定（.NET Array.Sort 為 O(log n) stack）。
- **適用場景**：空間受限、已經需要排序的工作流程。

## 方案比較

| 重點 | HashSet | 排序掃描 |
| --- | --- | --- |
| 建立成本 | 需要額外記憶體建立 HashSet | 就地排序即可 |
| 查找速度 | O(1) 平均時間 | O(1) 但元素需先排序 |
| 適合情境 | 追求極速查找 | 須節省空間或已排序資料 |
| 實作難度 | 簡潔，偏向資料結構導向 | 需注意排序後的掃描邏輯 |

## 執行方式

```bash
cd leetcode_2154
dotnet restore
dotnet build
dotnet run --project leetcode_2154/leetcode_2154.csproj
```

> [!TIP]
> `Program.Main` 目前示範兩組測試資料，可直接執行觀察輸出。

## 測試資料

| 測試 | `nums` | `original` | 預期輸出 | 說明 |
| --- | --- | --- | --- | --- |
| A | [5, 3, 6, 1, 12] | 3 | 24 | 3→6→12→24，24 不在陣列，停止 |
| B | [2, 7, 9] | 4 | 4 | 起始值未命中，直接回傳 |

## 後續延伸

- 加入隨機測試或單元測試，覆蓋極端數據量與重複值情境。
- 對照其他語言（Java、Python）實作，分析跨語言效能差異。
- 拓展為可視化或 API 服務，輸入任意資料集即可取得結果。
