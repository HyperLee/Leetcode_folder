# LeetCode 944 — Delete Columns to Make Sorted（刪列造序）

> [!NOTE]
> 題目連結： [Delete Columns to Make Sorted](https://leetcode.com/problems/delete-columns-to-make-sorted/)

## 簡介 ✅

給定一個包含 n 個字串的陣列 `strs`，所有字串長度相同，把每個字串按行排列成一個網格（每列是一個字串，每欄為該位置的字元）。

要刪除所有「不按字典序由上至下排序（非遞減）」的欄位，回傳需要刪除的欄位數量。

## 題目描述（繁體中文）

- 輸入：string[] strs（長度為 n，且每個字串長度相同）
- 輸出：整數，表示需要刪除的欄數

範例：

```text
strs = ["abc", "bce", "cae"]
排列為：
abc
bce
cae
```

第 0 欄 (a,b,c) 與 第 2 欄 (c,e,e) 是排序的；第 1 欄 (b,c,a) 不是，因此需刪除 1 個欄位。

## 解題想法與出發點 💡

- 觀察：每一欄的排序與其他欄互相獨立。
- 目標：對每一欄從上到下檢查是否**非遞減**（亦即每個相鄰 pair 均滿足 `上 <= 下`）。
- 若某欄出現違反（`上 > 下`），該欄必須被刪除，並可提前停止檢查該欄。

這樣的做法直覺且有效：逐欄檢查，時間複雜度 O(rows * cols)，空間複雜度 O(1)。

---

## 詳細解法（C# 範例） 🔧

```csharp
public int MinDeletionSize(string[] strs)
{
    int rows = strs.Length;
    int cols = strs[0].Length;
    int res = 0;

    for (int j = 0; j < cols; j++)
    {
        for (int i = 1; i < rows; i++)
        {
            if (strs[i - 1][j] > strs[i][j])
            {
                res++;
                break; // 這欄已確定需刪除，跳到下一欄
            }
        }
    }

    return res;
}
```

- 外層迴圈：遍歷每個欄位 j
- 內層迴圈：檢查該欄每對相鄰字元是否遞增（非遞減）
- 一旦發現違反 (`上 > 下`)，計數並對該欄提前中斷

### 複雜度

- 時間：O(rows * cols)
- 空間：O(1)

---

## 範例推演（逐步說明）

以 `strs = ["abc", "bce", "cae"]` 為例：

網格（每列為一字串）：

```text
col: 0 1 2
row0: a b c
row1: b c e
row2: c a e
```

逐欄檢查：

- 欄 0: 檢查 (a,b) 與 (b,c) -> a <= b, b <= c，欄 0 合格
- 欄 1: 檢查 (b,c) 與 (c,a) -> b <= c 成立，但 c > a（違反） => 欄 1 必須刪除
- 欄 2: 檢查 (c,e) 與 (e,e) -> c <= e, e <= e，欄 2 合格

結果：需要刪除的欄數 = 1

---

## 處理順序圖示（流程描述）

```text
Start
  └─> for j in 0..cols-1 (每一欄)
        └─> for i in 1..rows-1 (每一列的相鄰 pair)
              └─> if strs[i-1][j] > strs[i][j]
                    └─> res++ (標記此欄要刪除)
                    └─> break (提前跳出內層，進入下一欄)
        └─> (若全檢查完無違反，此欄保留)
  └─> End -> return res
```

> [!TIP]
> 當資料量很大時（列數與欄數都大），此方法仍為最有力且直接的方案，因為它只需檢查必要的相鄰字元對，且在發現違規時可以提前中斷。

---


## 小結 ✨

- 策略：逐欄檢查相鄰字元，若發現遞減則該欄需刪除。
- 優點：實作簡潔、時間與空間效率良好。

如果你想要，我可以把 README 翻成英文版或加入更多範例測試與視覺化圖示。
