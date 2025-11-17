# leetcode_1437

1437. Check If All 1's Are at Least Length K Places Away

## 簡介
給定一個二進位陣列 `nums` 和整數 `k`，若所有值為 `1` 的元素彼此間至少相隔 `k` 個元素，回傳 `true`；否則回傳 `false`。

Problem link: https://leetcode.com/problems/check-if-all-1s-are-at-least-length-k-places-away/

---

## 解法摘要（方法一：遍歷）
「所有 1 都至少相隔 k 個元素」等價於「任意兩個相鄰的 1 都至少相隔 k 個元素」。因此可以從左到右遍歷陣列，並記錄上一次出現 `1` 的索引 `prev`：

- 當遇到新的一個 `1`，若 `prev != -1`（表示先前已經出現過 `1`），則計算 `i - prev - 1`（兩個 `1` 之間的 `0` 的數量），若小於 `k` 則返回 `false`。
- 否則更新 `prev` 為目前索引，並繼續遍歷。

時間複雜度: O(n), 空間複雜度: O(1)

### 為何使用 i - prev - 1 計算兩個 1 中間的 0

簡要說明：若上一個 1 在索引 `prev`，目前 1 在索引 `i`，兩者之間包含的索引數（含端點）為 `i - prev`。
要單獨計算兩個 1 之間的元素數（即中間的 0 的數量），需要排除端點的 1，
因此為 `(i - prev) - 1`，即 `i - prev - 1`。例如：

- `prev = 0, i = 3` -> 中間的 0 數量 = `3 - 0 - 1 = 2`（索引 1 與 2）
- `prev = 2, i = 5` -> 中間的 0 數量 = `5 - 2 - 1 = 2`（索引 3 與 4）

判斷邏輯：若 `i - prev - 1 < k`，表示中間的 0 不到 k 個，兩個相鄰的 1 不滿足間隔 k 的條件。

---

## 範例
輸入: `nums = [1,0,0,0,1,0,0,1]`, `k = 2`
輸出: `true`

輸入: `nums = [1,0,0,1,0,1]`, `k = 2`
輸出: `false`

---

## 程式碼 (重點片段)
```csharp
public bool KLengthApart(int[] nums, int k)
{
    int prev = -1;
    for (int i = 0; i < nums.Length; i++)
    {
        if (nums[i] != 1) continue;
        if (prev != -1 && i - prev - 1 < k) return false;
        prev = i;
    }
    return true;
}
```

---

## 執行與建置
此專案為簡單的 .NET console 專案。

建置
```pwsh
dotnet build ./leetcode_1437/leetcode_1437.csproj -c Debug
```

執行
```pwsh
# 於專案資料夾下執行
dotnet run --project ./leetcode_1437/leetcode_1437.csproj -c Debug
```

---

## 範例測試
在 `Program.cs` 的 `Main` 中有簡單的測試範例，可在本地直接執行程式檢視結果。

---

## 備註
- 本檔案簡明說明如何在本專案中理解與執行題目解法。若需要新增測試或單元測試，建議加入 xUnit 或 MSTest 範例以便自動化驗證。
