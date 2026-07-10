# LeetCode 380：O(1) 時間插入、刪除和取得隨機元素

這是一個以 C# 撰寫的 .NET 10 主控台專案，示範如何用 `List<int>` 與
`Dictionary<int, int>` 實作 `RandomizedSet`，讓插入、刪除與取得隨機元素
都能在平均 `O(1)` 時間完成。

- [英文題目：380. Insert Delete GetRandom O(1)](https://leetcode.com/problems/insert-delete-getrandom-o1/)
- [中文題目：380. O(1) 時間插入、刪除和取得隨機元素](https://leetcode.cn/problems/insert-delete-getrandom-o1/)

## 題目說明

設計 `RandomizedSet` 類別並支援三個操作：

- `Insert(val)`：當 `val` 尚不存在時加入集合並回傳 `true`；若已存在則回傳 `false`。
- `Remove(val)`：當 `val` 存在時移除並回傳 `true`；若不存在則回傳 `false`。
- `GetRandom()`：從目前集合等機率回傳一個元素。

題目要求每個操作的平均時間複雜度皆為 `O(1)`。一般陣列雖然可以用索引
快速取得隨機元素，但從中間刪除會移動後續元素；雜湊集合雖然能快速新增與
刪除，卻不能用連續整數索引等機率取樣。因此本解法讓兩種資料結構各自負責
最擅長的工作。

## 限制條件

- `-2^31 <= val <= 2^31 - 1`
- `Insert`、`Remove` 與 `GetRandom` 的呼叫總數最多為 `2 * 10^5`。
- 題目保證呼叫 `GetRandom` 時集合至少有一個元素。
- 本實作依照上述保證設計，不另外定義空集合呼叫 `GetRandom` 的行為。

## 核心資料結構與不變量

`RandomizedSet` 維護三個唯讀欄位：

- `_values`：以緊密、無空洞的串列保存所有元素，讓隨機索引能在 `O(1)` 取值。
- `_indices`：記錄「元素 → `_values` 中的索引」，讓搜尋與定位平均為 `O(1)`。
- `_random`：以 `Random.Next(_values.Count)` 產生有效索引。

兩個容器必須始終符合以下不變量：對每個現有元素 `value`，若
`_indices[value] == index`，則 `_values[index]` 必須等於 `value`；而且每個
元素只出現一次。只要插入和刪除同步更新兩邊，便能同時保有雜湊查找與陣列
索引的效率。

## 三個操作的設計

### Insert

1. 用 `_indices.ContainsKey(val)` 判斷元素是否已存在。
2. 若存在，直接回傳 `false`。
3. 將目前 `_values.Count` 記為新元素索引，再把元素附加到串列尾端。
4. 回傳 `true`。

尾端加入與字典寫入的平均成本都是 `O(1)`。

### Remove

直接從串列中間刪除會是 `O(n)`，所以本解法使用 swap-delete（以尾端元素補洞）：

1. 從 `_indices` 取得待刪元素的索引；不存在便回傳 `false`。
2. 讀取 `_values` 的最後一個元素。
3. 用最後一個元素覆寫待刪位置，並修正它在 `_indices` 中的索引。
4. 刪除串列尾端，再從字典刪除目標鍵。

所有步驟都只存取固定數量的位置，因此平均為 `O(1)`。刪除的原本就是尾端
元素時，覆寫與索引更新雖然是自我指定，後續刪除仍然正確。

### GetRandom

`_values` 沒有空洞，所以從 `[0, _values.Count)` 均勻選一個整數索引，再回傳
該位置的元素即可。每個元素各佔一個索引，因此具有相同的被選中機率。

## swap-delete 逐步示範

假設目前狀態為：

- `_values = [10, 20, 30]`
- `_indices = { 10: 0, 20: 1, 30: 2 }`

執行 `Remove(20)` 時，目標索引是 1，尾端元素是 30：

1. 將索引 1 改成 30，暫時得到 `[10, 30, 30]`。
2. 將 `_indices[30]` 修正為 1。
3. 移除串列尾端，得到 `[10, 30]`。
4. 移除 `_indices[20]`，最後字典為 `{ 10: 0, 30: 1 }`。

這個索引修正非常重要；若仍記錄 30 位於索引 2，下一次移除 30 就會使用已
不存在的位置。可執行驗證器會在移除中間元素後繼續移除被搬動的尾端元素，
專門檢查此不變量。

## 複雜度

| 操作 | 平均時間 | 額外空間 |
| --- | --- | --- |
| `Insert` | `O(1)` | 每次成功插入增加 `O(1)` |
| `Remove` | `O(1)` | `O(1)` |
| `GetRandom` | `O(1)` | `O(1)` |

若集合中有 `n` 個元素，`List` 與 `Dictionary` 的總體空間為 `O(n)`。時間複雜度
中的平均情況來自雜湊字典操作的平均 `O(1)` 成本。

## 可執行驗證案例

`Main` 是確定性輸出的 acceptance harness，共執行 26 項檢查：

- LeetCode 官方操作序列。
- 重複插入與移除不存在元素。
- 移除中間元素後的尾端搬移與字典索引修正。
- 多元素集合的隨機結果成員資格；只印布林結果，不輸出隨機抽樣值。
- 單元素集合的精確隨機回傳值。
- `int.MinValue` 與 `int.MaxValue`。

每項檢查都輸出預期值、實際值和 `PASS`／`FAIL`。只要有任何失敗，程式便將
`Environment.ExitCode` 設為 1。此專案沒有獨立測試專案或測試框架；上述可
執行驗證器是目前的主要驗證方式。

## 建置與執行

請從此 README 所在的 `leetcode_380` 外層目錄執行：

```bash
dotnet build leetcode_380/leetcode_380.csproj --nologo
dotnet run --no-build --project leetcode_380/leetcode_380.csproj
```

以下是重新建置後執行第二個命令的完整輸出：

```text
LeetCode 380 acceptance harness

Case 1: official operation sequence
PASS | Insert(1) | Expected: True | Actual: True
PASS | Remove(2) | Expected: False | Actual: False
PASS | Insert(2) | Expected: True | Actual: True
PASS | GetRandom() belongs to {1, 2} | Expected: True | Actual: True
PASS | Remove(1) | Expected: True | Actual: True
PASS | Insert(2) duplicate | Expected: False | Actual: False
PASS | GetRandom() after official sequence | Expected: 2 | Actual: 2

Case 2: duplicate insertion and missing removal
PASS | Insert(7) | Expected: True | Actual: True
PASS | Insert(7) duplicate | Expected: False | Actual: False
PASS | Remove(8) missing | Expected: False | Actual: False
PASS | Remove(7) | Expected: True | Actual: True
PASS | Remove(7) after deletion | Expected: False | Actual: False

Case 3: middle-element removal and index repair
PASS | Insert(10) | Expected: True | Actual: True
PASS | Insert(20) | Expected: True | Actual: True
PASS | Insert(30) | Expected: True | Actual: True
PASS | Remove(20) from middle | Expected: True | Actual: True
PASS | Random member after middle removal | Expected: True | Actual: True
PASS | Remove(30) after index repair | Expected: True | Actual: True
PASS | GetRandom() after repaired removal | Expected: 10 | Actual: 10

Case 4: random membership and exact singleton return
PASS | 64 random draws belong to {-5, 0, 5} | Expected: True | Actual: True
PASS | Single-element GetRandom() | Expected: 42 | Actual: 42

Case 5: integer extremes
PASS | Insert(int.MinValue) | Expected: True | Actual: True
PASS | Insert(int.MaxValue) | Expected: True | Actual: True
PASS | Random extreme membership | Expected: True | Actual: True
PASS | Remove(int.MinValue) | Expected: True | Actual: True
PASS | GetRandom() with int.MaxValue remaining | Expected: 2147483647 | Actual: 2147483647

Summary: 26/26 checks passed.
```

## 專案結構

```
.
├── .editorconfig              # C# 與結構化檔案的格式規範
├── .gitattributes             # 文字與二進位檔案屬性
├── .gitignore                 # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json            # 直接偵錯 net10.0 輸出
│   └── tasks.json             # 預設建置工作
├── docs/
│   └── readme-template.md     # 初次建立 README 的範本
├── leetcode_380/
│   ├── Program.cs             # RandomizedSet 與可執行驗證器
│   └── leetcode_380.csproj    # .NET 10 SDK 專案設定
├── AGENTS.md                  # 本專案協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
