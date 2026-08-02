# LeetCode 1460：透過反轉子陣列讓兩陣列相等

> 以排序與字典計數兩種方法，判斷 `arr` 是否能藉由反覆反轉任意連續子陣列而成為 `target`。

- [English problem](https://leetcode.com/problems/make-two-arrays-equal-by-reversing-subarrays/description/?envType=daily-question&envId=2024-08-03)
- [中文題目](https://leetcode.cn/problems/make-two-arrays-equal-by-reversing-subarrays/description/)

## 題意與限制

每次操作可以選擇 `arr` 的任意一段連續子陣列並反轉，且可重複執行。請判斷是否能讓 `arr` 變成 `target`。

題目的正式限制如下：

- `1 <= target.length, arr.length <= 1000`
- `target.length == arr.length`
- `1 <= target[i], arr[i] <= 1000`

> [!NOTE]
> `Main` 額外執行兩個空陣列的案例，用來說明實作行為；它不是題目正式限制內的案例。

## 共同核心觀念

反轉只能改變元素的順序，不能改變任何值或其出現次數。反轉長度為二的子陣列等同相鄰交換，而相鄰交換可實現任意排列；因此，兩個陣列長度相同且每個值的出現頻率都相同時，就能重新排列成相同內容；反之則不可能。

| 方法 | 時間複雜度 | 額外空間 | 是否修改輸入 |
| --- | --- | --- | --- |
| `CanBeEqual`（排序） | `O(n log n)` | 不使用額外資料結構 `O(1)`；排序實作可能使用 `O(log n)` 呼叫堆疊 | 是，會排序 `target` 與 `arr` |
| `CanBeEqual2`（字典計數） | `O(n)` | `O(k)`，`k` 為不同值的數量 | 否 |

## 解法一：排序比較 `CanBeEqual`

執行步驟：

1. 就地排序 `target`。
2. 就地排序 `arr`。
3. 逐一比較相同索引位置；遇到第一個不相同的元素就回傳 `false`。
4. 所有位置都相同時回傳 `true`。

例如 `target = [1, 2, 3, 4]`、`arr = [2, 4, 1, 3]`，排序後兩者都會成為 `[1, 2, 3, 4]`，所以結果為 `true`。

此法時間複雜度為 `O(n log n)`；不使用額外資料結構，輔助空間為 `O(1)`，但排序實作可能使用 `O(log n)` 呼叫堆疊。排序直接在原陣列上進行，若呼叫端需要保留原始順序，請先傳入複本。

## 解法二：字典計數 `CanBeEqual2`

執行步驟：

1. 統計 `target` 中每個值的出現次數。
2. 掃描 `arr`；若值不在字典中，立即回傳 `false`。
3. 找到值後，將對應計數減一。
4. 若計數變成負數，代表該值在 `arr` 出現過量，回傳 `false`。
5. 在兩個陣列等長的前提下，完整消耗 `arr` 後回傳 `true`。

重複值範例：`target = [1, 1, 2, 3]` 先建立 `{1: 2, 2: 1, 3: 1}`；掃描 `arr = [3, 1, 2, 1]` 時，計數依序變為 `{1: 2, 2: 1, 3: 0}`、`{1: 1, 2: 1, 3: 0}`、`{1: 1, 2: 0, 3: 0}`、`{1: 0, 2: 0, 3: 0}`，因此成功並回傳 `true`。

若 `arr = [1, 2, 2, 3]`，處理第二個 `2` 時，`2` 的計數會從 `0` 減為 `-1`，表示頻率不符，結果為 `false`。

此法時間複雜度為 `O(n)`，並使用 `O(k)` 額外空間儲存不同值的頻率。

## 可執行教學案例

`Main` 會執行五個固定案例。每個案例都為兩個演算法傳入新的陣列複本，因此 `CanBeEqual` 的就地排序不會影響 `CanBeEqual2`。

專案結構：

- `leetcode_1460/leetcode_1460.csproj`：.NET 10 主控台專案。
- `leetcode_1460/Program.cs`：進入點、案例輸出與兩個解法。
- `docs/readme-template.md`：README 建立模板。

目前沒有自動化測試專案；以還原、建置與代表性 console 案例作為驗收。

在儲存庫根目錄執行：

```bash
dotnet restore leetcode_1460/leetcode_1460.csproj
dotnet build leetcode_1460/leetcode_1460.csproj --nologo
dotnet run --project leetcode_1460/leetcode_1460.csproj
git diff --check
```

本 README 對應的實際 console 輸出如下：

```text
案例：一般排列順序不同
target = [1, 2, 3, 4]
arr = [2, 4, 1, 3]
Expected: True
CanBeEqual Actual: True
CanBeEqual2 Actual: True
Result: PASS

案例：重複值頻率相同
target = [1, 1, 2, 3]
arr = [3, 1, 2, 1]
Expected: True
CanBeEqual Actual: True
CanBeEqual2 Actual: True
Result: PASS

案例：重複值頻率不同
target = [1, 1, 2, 3]
arr = [1, 2, 2, 3]
Expected: False
CanBeEqual Actual: False
CanBeEqual2 Actual: False
Result: PASS

案例：單一元素邊界
target = [1000]
arr = [1000]
Expected: True
CanBeEqual Actual: True
CanBeEqual2 Actual: True
Result: PASS

案例：空陣列額外案例
target = []
arr = []
Expected: True
CanBeEqual Actual: True
CanBeEqual2 Actual: True
Result: PASS

全部案例: PASS
```
