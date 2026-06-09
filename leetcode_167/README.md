# LeetCode 167 - Two Sum II

以 C# 實作 LeetCode 167 `Two Sum II - Input Array Is Sorted`，同時保留三種常見思路做對照：

- `Dictionary` 補數查找
- 固定左值 + `Binary Search`
- 符合題目最佳空間條件的 `Two Pointers`

目前主程式會直接執行四組範例資料，輸出三種解法的結果，方便比對實作與 README 說明。

## 題目連結

- LeetCode: <https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/description/>
- LeetCode 中文站: <https://leetcode.cn/problems/two-sum-ii-input-array-is-sorted/description/>

## 題目說明

給定一個 `1-indexed`、且已依照非遞減順序排序的整數陣列 `numbers`，需要找到兩個不同位置的元素，使它們的總和等於 `target`。

如果答案為 `numbers[index1]` 與 `numbers[index2]`，則必須滿足：

- `1 <= index1 < index2 <= numbers.length`
- 不能重複使用同一個元素
- 題目保證恰好只有一組解
- 額外空間必須為常數

回傳格式為長度 2 的陣列 `[index1, index2]`，而且索引必須是 `1-based`。

## 限制條件

題目本身的關鍵限制不是「能不能找到答案」，而是「要利用陣列已排序」以及「空間只能是常數」：

- 陣列已排序，所以可以利用大小關係快速縮小搜尋範圍
- 答案唯一，因此一旦命中就可以直接回傳
- 不能重複使用元素，所以第二個索引一定要落在第一個索引右側
- 若想完全符合題目要求，最終解法應優先選用 `Two Pointers`

## 解題概念與出發點

這題最直觀的想法是「走訪每個數字時，去找另一個能補成 `target` 的值」。差別只在於：

1. 補數存在哪裡？
2. 我們是否有利用「已排序」這個條件？
3. 額外空間是否符合題目要求？

因此這份專案刻意保留三種層次不同的解法：

1. `Dictionary`：最容易理解，但不符合常數空間限制。
2. `Binary Search`：開始利用排序特性，空間符合要求，但時間仍可再優化。
3. `Two Pointers`：同時滿足排序條件、常數空間與最佳時間效率，是本題主解。

## 解法總覽

| 解法 | 核心想法 | 時間複雜度 | 空間複雜度 | 是否符合題目常數空間 |
| --- | --- | --- | --- | --- |
| Dictionary | 一邊掃描一邊查補數 | `O(n)` | `O(n)` | 否 |
| Binary Search | 固定左值，右側做二分搜尋 | `O(n log n)` | `O(1)` | 是 |
| Two Pointers | 左右夾逼，依總和調整指標 | `O(n)` | `O(1)` | 是 |

## 解法一：Dictionary

### 設計說明

這個方法的出發點最直接：

1. 走訪 `numbers[i]`
2. 計算補數 `target - numbers[i]`
3. 看這個補數是否已經在前面出現過
4. 如果出現過，代表答案就是「前面那個補數」與「目前這個數字」

程式裡的 `Dictionary<int, int>` 會記錄：

- `key`：已看過的數值
- `value`：該數值第一次出現時的 `1-based` 索引

這樣做的好處是查詢補數幾乎是常數時間，但代價是必須額外保存最多 `n` 個元素，因此空間複雜度為 `O(n)`，不符合題目「只用常數額外空間」的要求。這個方法保留在專案中，主要是為了對照與教學。

### 範例演示流程

以 `numbers = [2, 7, 11, 15]`、`target = 9` 為例：

1. 看到 `2`，補數是 `7`
   目前 `Dictionary` 還是空的，找不到 `7`
   把 `2 -> 1` 放入表中
2. 看到 `7`，補數是 `2`
   `Dictionary` 已經有 `2 -> 1`
   代表答案成立，直接回傳 `[1, 2]`

### 適用性評估

- 優點：思路直觀、寫法簡潔、時間效率不差
- 缺點：不符合題目常數額外空間限制
- 使用時機：適合先建立解題直覺，不適合作為本題最終最佳解

## 解法二：Binary Search

### 設計說明

這個方法開始利用「陣列已排序」：

1. 先固定左邊的元素 `numbers[i]`
2. 目標就變成在右側區間尋找 `target - numbers[i]`
3. 因為右側區間已排序，可以直接做二分搜尋

這樣的重點是：

- 第二個數字只能在 `i + 1` 到陣列尾端之間尋找
- 每次二分搜尋都能把搜尋區間砍半
- 不需要額外資料結構，因此空間為 `O(1)`

不過因為外層仍要逐一枚舉左側元素，所以整體時間是 `O(n log n)`。

### 範例演示流程

同樣以 `numbers = [2, 7, 11, 15]`、`target = 9` 為例：

1. 固定第一個數字 `2`
   補數應為 `7`
2. 在右側區間 `[7, 11, 15]` 做二分搜尋
3. 中間值第一次就命中 `7`
4. 直接回傳 `[1, 2]`

如果第一輪沒有找到，就會改固定下一個元素，再到它右側重複二分搜尋。

### 適用性評估

- 優點：已符合題目常數空間要求，且充分利用排序特性
- 缺點：時間仍慢於雙指標
- 使用時機：當你已經想到「排序 + 查找」，但還沒進一步想到雙指標收斂法時，是很自然的過渡解

## 解法三：Two Pointers

### 設計說明

這是本題最典型也最完整的解法。

做法如下：

1. 左指標 `low` 指向陣列開頭
2. 右指標 `high` 指向陣列結尾
3. 計算 `numbers[low] + numbers[high]`
4. 如果總和太小，左指標右移，讓總和變大
5. 如果總和太大，右指標左移，讓總和變小
6. 如果剛好等於 `target`，直接回傳答案

它能成立的原因，在於陣列已排序：

- 左邊往右走，只會拿到更大或相等的值
- 右邊往左走，只會拿到更小或相等的值

因此每次移動指標都不是亂猜，而是在有方向地縮小搜尋空間。由於每個指標最多只會走完整個陣列一次，所以時間是 `O(n)`，空間是 `O(1)`。

### 範例演示流程

仍以 `numbers = [2, 7, 11, 15]`、`target = 9` 為例：

1. `low = 0` 指向 `2`，`high = 3` 指向 `15`
   總和 `17 > 9`
   表示右值太大，右指標左移
2. `low = 0` 指向 `2`，`high = 2` 指向 `11`
   總和 `13 > 9`
   右指標再左移
3. `low = 0` 指向 `2`，`high = 1` 指向 `7`
   總和 `9 == target`
   回傳 `[1, 2]`

### 適用性評估

- 優點：同時滿足 `O(n)` 時間與 `O(1)` 空間，是本題最佳解
- 缺點：需要先理解排序陣列上的單調性
- 使用時機：本題正式提交時的首選

## 主程式示範資料

`Main` 目前會直接執行以下四組案例：

1. `[2, 7, 11, 15], target = 9`
2. `[2, 3, 4], target = 6`
3. `[-1, 0], target = -1`
4. `[1, 1, 3, 4], target = 2`

這些案例分別覆蓋：

- 一般命中情境
- 解答跨越中段的位置
- 含負數
- 含重複值

## 建置、測試與執行

### 建置

```bash
dotnet build leetcode_167/leetcode_167.csproj
```

### 測試

```bash
dotnet test leetcode_167/leetcode_167.csproj --no-restore -v normal
```

> [!NOTE]
> 目前這個專案還沒有獨立測試專案，因此 `dotnet test` 在此階段主要是驗證專案可成功建置，不代表已有額外的單元測試案例。

### 執行範例

```bash
dotnet run --project leetcode_167/leetcode_167.csproj
```

> [!NOTE]
> 在目前這台 macOS 執行環境上，`dotnet run` 可能會先印出一行 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。這是執行環境額外輸出，不屬於本專案的主程式內容；真正的範例輸出從 `LeetCode 167 - Two Sum II` 開始。

## 實際執行輸出

以下為目前主程式本體的實際輸出內容：

```text
LeetCode 167 - Two Sum II

Case 1
numbers = [2, 7, 11, 15], target = 9
Dictionary    -> [1, 2]
Binary Search -> [1, 2]
Two Pointers  -> [1, 2]

Case 2
numbers = [2, 3, 4], target = 6
Dictionary    -> [1, 3]
Binary Search -> [1, 3]
Two Pointers  -> [1, 3]

Case 3
numbers = [-1, 0], target = -1
Dictionary    -> [1, 2]
Binary Search -> [1, 2]
Two Pointers  -> [1, 2]

Case 4
numbers = [1, 1, 3, 4], target = 2
Dictionary    -> [1, 2]
Binary Search -> [1, 2]
Two Pointers  -> [1, 2]
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_167/
    ├── Program.cs
    └── leetcode_167.csproj
```

## 結論

如果目標是理解題目，可以先從 `Dictionary` 版本建立「補數」概念；如果目標是練習排序陣列上的查找技巧，可以看 `Binary Search`；如果目標是符合 LeetCode 167 的最佳要求，則應採用 `Two Pointers`。
