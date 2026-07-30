# LeetCode 141：環形鏈結串列

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-141-FFA116)](https://leetcode.com/problems/linked-list-cycle/)

以 C# 與 Floyd 快慢指標（Floyd's Cycle-Finding Algorithm）判斷單向鏈結串列是否存在環。本專案是可直接執行的 .NET 10 console 範例，內建六筆固定案例，會逐筆比較預期與實際結果。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法：Floyd 快慢指標](#解法floyd-快慢指標)
- [六筆範例演示](#六筆範例演示)
- [建置與執行](#建置與執行)

## 題目說明

給定單向鏈結串列的頭節點 `head`，判斷串列中是否存在環：

- 如果沿著 `next` 持續前進，能再次到達先前經過的節點，代表串列有環，回傳 `true`。
- 如果最後能抵達 `null`，代表串列有明確尾端，回傳 `false`。

LeetCode 題目使用 `pos` 表示尾節點連回的位置，但 `pos` 不會傳入 `HasCycle`。本專案的範例建構器使用意義相同的 `cycleIndex`：

- `cycleIndex = -1`：尾節點保持指向 `null`，不建立環。
- `cycleIndex >= 0`：尾節點的 `next` 指向該零起始索引的節點。

> [!NOTE]
> `cycleIndex` 只用來建立可執行的測試串列。演算法本身只接收 `head`，不依賴環的入口索引。

## 限制條件

依照 [LeetCode 141 官方題目](https://leetcode.cn/problems/linked-list-cycle/)：

- 鏈結串列節點數量介於 `0` 與 `10^4`。
- `-10^5 <= Node.val <= 10^5`。
- `pos` 為 `-1` 或串列中的有效索引。
- 進階要求是使用 `O(1)` 額外空間完成判斷。

## 解題概念與出發點

最直觀的方式是把走過的節點放進雜湊集合；再次遇到集合中的節點時即可判定有環。這個方法容易理解，但最壞情況需要保存所有節點，額外空間為 `O(n)`。

本專案採用 Floyd 快慢指標：

1. `slow` 每次沿著 `next` 前進一個節點。
2. `fast` 每次沿著 `next` 前進兩個節點。
3. 如果串列沒有環，速度較快的 `fast` 最終會抵達 `null`。
4. 如果串列存在環，兩個指標進入環後，`fast` 每輪會相對追近 `slow` 一個節點，因此必定在有限步數內相遇。

這個出發點不需要記錄走過的節點，能同時達到線性時間與常數額外空間。

## 解法：Floyd 快慢指標

目前專案只有一種主要解法：`HasCycle(ListNode? head)`。

### 輸入與輸出

- 輸入：鏈結串列頭節點 `head`，空串列可傳入 `null`。
- 輸出：存在環時為 `true`，無環時為 `false`。
- 副作用：不修改節點值，也不改寫任何 `next`。

### 設計步驟

1. 如果 `head` 為 `null`，或 `head.next` 為 `null`，串列不可能形成環，直接回傳 `false`。
2. 將 `slow` 放在 `head`，將 `fast` 放在 `head.next`，讓兩個指標具有一個節點的初始距離。
3. 當 `fast` 與 `fast.next` 都不是 `null` 時：
   - 先檢查 `slow` 與 `fast` 是否參考同一個節點。
   - 若相同，代表兩者已在環內相遇，回傳 `true`。
   - 否則讓 `slow` 前進一步、`fast` 前進兩步。
4. 如果迴圈因 `fast` 或 `fast.next` 為 `null` 而結束，代表存在明確鏈尾，回傳 `false`。

### 為什麼相遇能證明有環

指標比較的是節點參考，而不是節點值。不同節點即使保存相同整數，也不會被誤判為相遇。

假設環長度為 `k`。兩個指標都進入環後，每輪 `fast` 走兩步、`slow` 走一步，所以兩者的相對距離每輪縮短一步。距離在模 `k` 的有限狀態中持續變化，最終必定變成 `0`；此時兩個指標指向同一節點，證明串列有環。

### 複雜度

| 指標 | 複雜度 | 說明 |
| --- | --- | --- |
| 時間 | `O(n)` | 無環時最多走到鏈尾；有環時進入環後最多再走一個環長度便會相遇。 |
| 額外空間 | `O(1)` | 只使用 `slow`、`fast` 等固定數量變數。 |

> [!IMPORTANT]
> 範例用的 `BuildList` 需要 `O(n)` 空間保存節點陣列，以便讓尾端連回指定索引；這是測試資料建構成本，不是 `HasCycle` 演算法的額外空間。

## 六筆範例演示

### 案例 1：空串列

```text
head = []
cycleIndex = -1
```

`head` 是 `null`，第一個邊界判斷立即回傳 `false`。預期與實際結果皆為 `False`。

### 案例 2：單節點無環

```text
[1] -> null
```

唯一節點的 `next` 是 `null`，無法再次到達任何節點，邊界判斷回傳 `false`。

### 案例 3：單節點自環

```text
 ┌───┐
 ▼   │
[1] ─┘
```

`cycleIndex = 0` 讓唯一節點連回自己。初始化後 `slow` 與 `fast` 都指向此節點，第一次比較便相遇並回傳 `true`。

### 案例 4：多節點無環

```text
[1] -> [2] -> [3] -> [4] -> null
```

初始化時 `slow` 在節點 `1`，`fast` 在節點 `2`。移動一輪後，`slow` 在節點 `2`，`fast` 在節點 `4`；由於 `fast.next` 是 `null`，迴圈結束並回傳 `false`。

### 案例 5：尾端連回中間節點

```text
[3] -> [2] -> [0] -> [-4]
        ▲               │
        └───────────────┘
cycleIndex = 1
```

指標流程：

| 比較輪次 | `slow` | `fast` | 結果 |
| --- | --- | --- | --- |
| 1 | `3` | `2` | 不同，繼續移動 |
| 2 | `2` | `-4` | 不同，繼續移動 |
| 3 | `0` | `0` | 指向同一節點，回傳 `true` |

### 案例 6：尾端連回頭節點

```text
 ┌───────────┐
 ▼           │
[1] -> [2] ──┘
cycleIndex = 0
```

初始化時 `slow` 在節點 `1`，`fast` 在節點 `2`。移動一輪後兩者都指向節點 `2`，下一輪比較時相遇並回傳 `true`。

## 建置與執行

需求：

- .NET SDK 10.0

從 `leetcode_141` repository 根目錄執行：

```bash
dotnet restore leetcode_141/leetcode_141.csproj
dotnet build leetcode_141/leetcode_141.csproj --nologo
dotnet run --project leetcode_141/leetcode_141.csproj --no-build
```

專案目前沒有獨立的自動化測試專案。驗收方式是確認建置成功，並執行 console 內建的六筆 Expected/Actual 比對。

## 實際執行結果

以下內容來自目前程式的實際執行：

```text
案例 1：空串列
輸入：head = [], cycleIndex = -1
預期：False
實際：False
結果：PASS

案例 2：單節點無環
輸入：head = [1], cycleIndex = -1
預期：False
實際：False
結果：PASS

案例 3：單節點自環
輸入：head = [1], cycleIndex = 0
預期：True
實際：True
結果：PASS

案例 4：多節點無環
輸入：head = [1, 2, 3, 4], cycleIndex = -1
預期：False
實際：False
結果：PASS

案例 5：尾端連回中間節點
輸入：head = [3, 2, 0, -4], cycleIndex = 1
預期：True
實際：True
結果：PASS

案例 6：尾端連回頭節點
輸入：head = [1, 2], cycleIndex = 0
預期：True
實際：True
結果：PASS

總結：6/6 筆測試通過
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_141/
    ├── Program.cs
    └── leetcode_141.csproj
```

- `Program.cs`：節點型別、Floyd 判圈演算法、測試串列建構器與六筆可執行案例。
- `leetcode_141.csproj`：目標框架為 `net10.0` 的 console 專案設定。
- `docs/readme-template.md`：README 的內容與驗證準則。
