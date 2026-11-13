# 3420. 統計 K 次操作以內得到非遞減子陣列的數目

本專案以 C# 實作兩種解法，並在 `Main` 中執行固定案例及暴力交叉驗證。

題目連結：[LeetCode 3420 — Count Non-Decreasing Subarrays After K Operations](https://leetcode.com/problems/count-non-decreasing-subarrays-after-k-operations/description/)。

## 1. 題目說明

給定整數陣列 `nums` 與整數 `k`。對每一個**連續且非空**的子陣列，可以最多執行 `k` 次操作；每次操作只能將其中一個元素加 `1`。

請計算有多少個子陣列，能在預算內變成非遞減陣列，也就是相鄰元素皆滿足「前一個小於等於後一個」。

每個子陣列都獨立計算。對某個子陣列的調整不會影響其他子陣列，也不會消耗其他子陣列的預算。不同位置即使有相同內容，仍是不同子陣列。

### 範例一

```text
nums = [6, 3, 1, 2, 4, 4]
k = 7
答案 = 17
```

共有 `6 × 7 / 2 = 21` 個子陣列。以下四個超出預算：

| 子陣列 | 最低操作次數 |
|---|---:|
| `[6,3,1]` | 8 |
| `[6,3,1,2]` | 12 |
| `[6,3,1,2,4]` | 14 |
| `[6,3,1,2,4,4]` | 16 |

因此答案是 `21 - 4 = 17`。

### 範例二

```text
nums = [6, 3, 1, 3, 6]
k = 4
答案 = 12
```

合法子陣列包括 5 個長度一、4 個長度二、2 個長度三，以及 `[3,1,3,6]` 這個長度四的子陣列，合計 `5 + 4 + 2 + 1 = 12`。

### 限制條件

- `1 <= nums.Length <= 100000`。
- `1 <= nums[i] <= 1000000000`。
- `1 <= k <= 1000000000`。

兩種正式解法都以符合上述限制的輸入為前提，不修改 `nums`。長度為一的子陣列不需要任何操作，一定合法。

## 2. 出發點：如何算出一個子陣列的最低成本？

只能增加元素，因此由左往右看時，較小的元素必須補到前面已經出現過的最大值。

固定子陣列 `[left, right]`，令：

```text
target[i] = max(nums[left], nums[left + 1], ..., nums[i])
cost(left, right) = sum(target[i] - nums[i]), left <= i <= right
```

這是最低成本，理由如下：

1. 第一個元素維持原值即可，額外提高只會增加後面元素的負擔。
2. 每個後續元素至少要等於前一個調整後的元素，而且不能低於自己的原值。
3. 所以最小可行值就是目前的前綴最大值；逐一取這個最小值便得到最小總成本。

例如整個範例一：

```text
index     0  1  2  3  4  5
nums      6  3  1  2  4  4
target    6  6  6  6  6  6
increase  0  3  5  4  2  2

cost = 0 + 3 + 5 + 4 + 2 + 2 = 16
```

將每個數字畫成直條，`X` 代表原有高度，`+` 代表必須補上的高度：

```text
height
  6 | X  +  +  +  +  +
  5 | X  +  +  +  +  +
  4 | X  +  +  +  X  X
  3 | X  X  +  +  X  X
  2 | X  X  +  X  X  X
  1 | X  X  X  X  X  X
    +------------------
index 0  1  2  3  4  5
```

直接枚舉所有起點與終點、逐步累計成本，可以做到 `O(n²)`，適合拿來驗證小陣列，但無法處理題目上限。兩種正式解法都利用以下單調性縮小搜尋範圍：

- 固定起點，向右延長不會降低最低成本。
- 固定終點，向右移動起點不會增加最低成本。

因此合法視窗的更短子視窗一定合法；關鍵在於如何快速更新擴窗與縮窗的成本。

## 3. 方法一：由左往右的滑動視窗＋優先佇列＋單調堆疊

對應函式：`CountNonDecreasingSubarrays(int[] nums, int k)`。

### 3.1 設計概念

依序把 `nums[right]` 加入視窗。新元素需要補到視窗最大值，所以擴窗成本容易計算。

真正的困難是移除左端點：如果移除的是一個高峰，後方很多元素的目標高度都可能降低。若每次重新掃描，就會退化為平方時間。因此，本解法用單調堆疊建立覆蓋關係，將受影響的位置分成幾個平台，按區段一次扣除成本。

### 3.2 資料結構與不變量

| 變數 | 用途 |
|---|---|
| `left`、`right` | 目前視窗的左右端點，皆只向右移動 |
| `operations` | 目前視窗調整成非遞減所需的最低操作次數 |
| `ans` | 已處理終點的合法子陣列總數 |
| `q` | 優先佇列，儲存原值與索引，以負的原值為優先權，取得視窗最大值 |
| `st` | 原值嚴格遞減的索引堆疊，用來建立覆蓋關係 |
| `cnt[p]` | 以 `p` 為左側最近且嚴格較大元素的索引集合 |
| `pos[i]` | 已掃描範圍中，`i` 右側第一個大於等於 `nums[i]` 的索引；尚未遇到時為 `n` |

每輪結束時，視窗成本不超過 `k`，`left` 是此終點最早的合法起點，而且堆頂已清除視窗外的過期項目。下一輪擴窗時，堆頂就是新視窗的最大值。

單調堆疊描述的是掃描過程的覆蓋關係，不是目前視窗內的元素集合，因此不因 `left` 移動而刪除堆疊底部。

### 3.3 建立覆蓋關係

加入索引 `right` 時：

1. 持續彈出堆疊中小於等於 `nums[right]` 的頂端，並設定其 `pos` 為 `right`。
2. 若堆疊仍非空，頂端就是新元素左側最近的嚴格較大元素；把 `right` 記入它的 `cnt`。
3. 將 `right` 放入堆疊。

這裡使用 `>=` 彈出相等值，所以堆疊值嚴格遞減，而 `pos` 的邊界是「大於等於」。這兩個條件互相配合，讓相等高度的平台邊界也能正確處理。

### 3.4 擴展右端點

新元素不會改變左邊元素已經需要的提升量，只需計算自己的提升量：

```text
operations += max(0, windowMax - nums[right])
```

`q` 先加入新元素，再讀取最大值。若新元素本身就是最大值，增加的成本為零。

### 3.5 縮小左端點：為什麼可以按區段扣除？

當 `operations > k`，必須移除 `nums[left]`。對 `cnt[left]` 中的每個索引 `i`，以下區段會由 `nums[i]` 接手作為目標高度：

```text
[i, min(pos[i], right + 1))
```

這是左閉右開區間。在 `pos[i]` 之前沒有另一個大於等於 `nums[i]` 的元素；遇到邊界後，應由下一個平台接手。若邊界尚未掃描到，則以目前視窗末端 `right + 1` 截斷即可。

區段原本的目標高度為 `nums[left]`，移除左端點後降為 `nums[i]`，所以扣除：

```text
heightDrop = nums[left] - nums[i]
width = min(pos[i], right + 1) - i
operations -= (long)heightDrop * width
```

例如掃描到 `right = 2` 時：

```text
index           0    1    2
nums            6    3    1
old target      6    6    6     cost = 0 + 3 + 5 = 8
                ^    +----+
              remove |    |   cnt[0] = [1]
                     +----+   child 1 covers [1, 3)

after removal:
index                1    2
nums                 3    1
new target           3    3     cost = 0 + 2 = 2

saved cost = (6 - 3) * (3 - 1) = 6
new cost   = 8 - 6 = 2
```

索引 `2` 是索引 `1` 的子項，並非 `cnt[0]` 的直接子項。移除 `0` 後仍有高度 `3` 的元素擋在前面，所以整段先降為 `3`；之後若再移除 `1`，才會處理下一層下降。

移除左端點本身不需額外扣除，因為視窗第一個元素原本的提升量就是零。

### 3.6 優先佇列為何在縮窗後清理？

縮窗時只使用覆蓋關係更新成本，不讀取堆頂。因此可以等 `left` 移動完畢，再持續移除 `idx < left` 的堆頂。

埋在堆內的過期元素暫時保留，等它們成為堆頂時才移除，稱為延遲刪除。只要真正讀取最大值時堆頂有效，就不會影響結果。

### 3.7 完整演示：範例一

`nums = [6,3,1,2,4,4]`，`k = 7`。表中的「擴窗後成本」尚未扣除本輪縮窗的節省量。

| right | 新值 | 擴窗後成本 | 縮窗動作 | 最終 left | 最終成本 | 新增數量 | 累計答案 |
|---:|---:|---:|---|---:|---:|---:|---:|
| 0 | 6 | 0 | 無 | 0 | 0 | 1 | 1 |
| 1 | 3 | 3 | 無 | 0 | 3 | 2 | 3 |
| 2 | 1 | 8 | 移除索引 0，扣除 6 | 1 | 2 | 2 | 5 |
| 3 | 2 | 3 | 無 | 1 | 3 | 3 | 8 |
| 4 | 4 | 3 | 無 | 1 | 3 | 4 | 12 |
| 5 | 4 | 3 | 無 | 1 | 3 | 5 | 17 |

最後一輪的合法視窗如下，任選 `left` 到 `right` 之間的起點，都能得到一個合法子陣列：

```text
index    0 | 1  2  3  4  5
nums     6 | 3  1  2  4  4
            L           R
target      3  3  3  4  4
increase    0  2  1  0  0   -> cost = 3 <= 7

valid starts for right = 5: 1, 2, 3, 4, 5
new subarrays = right - left + 1 = 5
```

### 3.8 正確性與複雜度

- 擴窗時，舊位置的前綴最大值不變，新位置的成本由堆頂正確計算。
- 縮窗時，`cnt` 與 `pos` 切出的區段恰好表示移除左端點後的目標高度下降，因此 `operations` 持續等於最低成本。
- 成本超支就持續移動 `left`，直到第一次合法。被排除的起點已經超支，之後延長終點也不會恢復合法。
- 每個合法起點都在 `[left, right]`，所以本輪恰好加入 `right - left + 1` 個答案；各輪終點不同，不會重複計數。

每個索引在堆疊至多進出一次，每個覆蓋關係至多被縮窗流程走訪一次；優先佇列中每個索引至多加入、移除一次。堆操作各需 `O(log n)`，總時間為 **`O(n log n)`**，額外空間為 **`O(n)`**。

## 4. 方法二：由右往左的滑動視窗＋單調雙端佇列

對應函式：`CountNonDecreasingSubarrays2(int[] nums, int k)`。

核心是：**從右往左加入元素，把需要提升到相同高度的位置視為平台；加入較高元素時整段抬高，超出預算時從右端逐個移除。**

以下全程使用相同範例，索引從 `0` 開始：

```text
index =  0  1  2  3  4  5
nums  = [6, 3, 1, 2, 4, 4]
k     = 7
```

### 4.1 為什麼換個方向？平台代表什麼？

由左往右擴窗容易，但移除左端的高峰需要處理高度下降。換成由右往左加入元素，新的左端若比較高，只會把後方較低的平台一起抬高；若成本超支，則從右端刪除元素。

移除最後一個元素不會改變任何更早位置的前綴最大值，因此縮窗只需扣掉該元素自己的提升量。這讓兩個方向的成本更新都能透過平台完成。

對任一子陣列，要以最低成本變成非遞減，每個位置的目標高度就是「從子陣列起點到目前位置的最大值」。例如視窗 `[1,5]`：

```text
index      1  2  3 | 4  5
nums       3  1  2 | 4  4
target     3  3  3 | 4  4
increase   0  2  1 | 0  0

cost = 0 + 2 + 1 + 0 + 0 = 3
```

索引 `1、2、3` 的目標高度都是 `3`，可以視為一個平台：

```text
target height

4 |          +-----+
3 | +--------+
2 | |
1 | |
  +-----------------
      1  2  3  4  5     index

platform [1,4): height 3
platform [4,6): height 4
```

程式會保留相等高度的相鄰平台，所以高度 `4` 的部分實際儲存成 `[4,5)`、`[5,6)` 兩段，不影響成本。

### 4.2 雙端佇列儲存什麼？為什麼方向相反？

佇列儲存**各平台的起始索引**，平台高度可以透過 `nums[index]` 取得。程式使用陣列 `deque` 及左右界 `head`、`tail` 表示有效範圍 `[head, tail)`；`tail` 指向下一個可寫入的位置，最後一筆有效資料位於 `tail - 1`。

為配合由右往左掃描，儲存方向如下。以下使用 `index(height)` 表示一個佇列項目：

```text
window from left to right:

index             1  2  3 | 4 | 5
target            3  3  3 | 4 | 4
platform start    1         4   5

deque from head to tail:

head                              last entry
  |                                   |
  v                                   v
[5(4)] --------> [4(4)] --------> [1(3)]
rightmost                       leftmost
platform                        platform
```

- **尾端**是最左邊的平台，用來合併被新左端點抬高的區段。
- **前端**是最右邊的平台，用來取得目前 `right` 的目標高度。

從 `head` 到 `tail`，索引嚴格遞減、原值不遞增。程式只合併**嚴格較低**的平台，相等高度可以保留成相鄰的兩段。

每輪縮窗結束後，佇列前端所代表的平台覆蓋目前 `right`，`operations` 等於 `[left, right]` 的最低成本，而且 `right` 是此起點最遠的合法終點。程式只計算提升量，不會真的修改 `nums`。

### 4.3 平台合併的成本公式

假設新元素值為 `nums[left]`，尾端平台的起點為 `index`、高度為 `nums[index]`。

- 若平台高度大於等於新元素，就不需提升，合併流程結束。
- 若平台高度小於新元素，整段都要補到新高度，將平台從尾端彈出並增加成本。

彈出後，新的尾端索引就是該平台右邊相鄰平台的起點，也就是目前區段的右邊界；若佇列已空，右邊界就是 `right + 1`。對應程式：

```csharp
int index = deque[--tail];
int segmentRight = head < tail ? deque[tail - 1] : right + 1;
operations += (long)(nums[left] - nums[index]) * (segmentRight - index);
```

這是左閉右開區間 `[index, segmentRight)`，因此：

```text
height difference = nums[left] - nums[index]
platform width    = segmentRight - index
added cost        = height difference * platform width
```

持續合併較低平台後，將 `left` 放入尾端。新左端點自身不需提升，因此不增加自己的成本。以下依照程式執行順序，逐輪追蹤。

### 4.4 前四輪：left = 5、4、3、2

初始狀態：

```text
right = 5
operations = 0
answer = 0
deque = []
```

第一輪 `left = 5`，加入 `4`。原本沒有平台，直接放入佇列：

```text
index      5
nums      [4]
target    [4]
           L
           R

deque = [5(4)]
cost  = 0
new subarrays = 5 - 5 + 1 = 1
answer = 1
```

第二輪 `left = 4`，加入 `4`。相等高度不合併，因為程式使用嚴格小於的條件 `nums[deque[tail - 1]] < nums[left]`：

```text
index      4  5
nums      [4, 4]
target    [4, 4]
           L  R

deque = [5(4), 4(4)]
cost  = 0
new subarrays = 5 - 4 + 1 = 2
answer = 3
```

第三輪 `left = 3`，加入 `2`。尾端平台高度為 `4`，已經大於新元素，不需提升：

```text
index      3  4  5
nums      [2, 4, 4]
target    [2, 4, 4]
           L     R

deque = [5(4), 4(4), 3(2)]
cost  = 0
new subarrays = 3
answer = 6
```

第四輪 `left = 2`，加入 `1`。同樣沒有較低的平台要合併：

```text
index      2  3  4  5
nums      [1, 2, 4, 4]
target    [1, 2, 4, 4]
           L        R

deque = [5(4), 4(4), 3(2), 2(1)]
cost  = 0
new subarrays = 4
answer = 10
```

到這裡，每個視窗原本就已經非遞減，成本始終為零，`right` 一直維持在 `5`。

### 4.5 第五輪：left = 1，加入 3，開始合併平台

加入前的狀態：

```text
index        2 | 3 | 4 | 5
nums         1 | 2 | 4 | 4
target       1 | 2 | 4 | 4

deque = [5(4), 4(4), 3(2), 2(1)]
                             ^
                          last entry
cost = 0
```

新的左端點是 `3`，後方低於 `3` 的平台都需要補到 `3`。

先彈出索引 `2`，平台高度為 `1`。彈出後，新的尾端是索引 `3`，因此原平台涵蓋 `[2,3)`：

```text
index        1 | 2 | 3 | 4 | 5
new value    3 |   |   |   |
old target     | 1 | 2 | 4 | 4
                 ^
              raise to 3

width      = 3 - 2 = 1
height gap = 3 - 1 = 2
added cost = 2 * 1 = 2

deque = [5(4), 4(4), 3(2)]
cost  = 2
```

尾端高度 `2` 仍低於 `3`，繼續彈出索引 `3`。新的尾端是索引 `4`，所以這段是 `[3,4)`：

```text
width      = 4 - 3 = 1
height gap = 3 - 2 = 1
added cost = 1 * 1 = 1

deque = [5(4), 4(4)]
cost  = 2 + 1 = 3
```

現在尾端高度為 `4`，不低於新元素 `3`，合併結束。將索引 `1` 放入尾端後：

```text
index        1  2  3 | 4 | 5
nums         3  1  2 | 4 | 4
target       3  3  3 | 4 | 4
increase     0  2  1 | 0 | 0

deque = [5(4), 4(4), 1(3)]
cost  = 3
```

**索引 `2、3` 雖然已從佇列彈出，仍留在視窗內。** 它們現在由起點為 `1`、高度為 `3` 的平台一起代表：

```text
platform start 1
    |
    v
   [3  3  3] | 4 | 4
    <----->
    width 3
```

成本 `3 <= 7`，不需縮窗。從索引 `1` 開始，可以選擇五個終點，分別得到 `[3]`、`[3,1]`、`[3,1,2]`、`[3,1,2,4]`、`[3,1,2,4,4]`。

```text
new subarrays = right - left + 1 = 5 - 1 + 1 = 5
answer = 10 + 5 = 15
```

### 4.6 第六輪：left = 0，加入 6，抬高整個平台

加入前：

```text
index         1  2  3 | 4 | 5
nums          3  1  2 | 4 | 4
target        3  3  3 | 4 | 4

deque = [5(4), 4(4), 1(3)]
cost  = 3
```

新的 `6` 高於所有平台，因此依序從尾端合併。

第一個彈出索引 `1`，高度為 `3`。彈出後，新的尾端是索引 `4`，所以這個平台是 `[1,4)`，包含三個位置：

```text
index         0 | 1  2  3 | 4 | 5
new value     6 |
old target      | 3  3  3 | 4 | 4
raised segment  | 6  6  6 |

height gap = 6 - 3 = 3
width      = 4 - 1 = 3
added cost = 3 * 3 = 9
```

寬度是 `3`，因為索引 `1` 已經代表上一輪合併好的整個平台。

**原有成本 `3` 要保留，再加上新的提升成本。** 索引 `2、3` 先前補到高度 `3` 的成本，已經算在 `operations` 裡面；這次只額外計算整段從高度 `3` 升到 `6` 的成本。

例如索引 `2` 的原值為 `1`：

```text
original value 1
       |
       | +2, already counted in the previous round
       v
     height 3
       |
       | +3, added in this round
       v
     height 6

total increase = 2 + 3 = 5
```

接著彈出索引 `4`，高度為 `4`。新的尾端是索引 `5`，所以範圍是 `[4,5)`，新增成本為 `(6 - 4) * (5 - 4) = 2`。

最後彈出索引 `5`，高度也是 `4`。這次佇列已空，右邊界改用 `right + 1 = 6`，範圍為 `[5,6)`，新增成本為 `(6 - 4) * (6 - 5) = 2`。

| 合併的平台 | 高度變化 | 寬度 | 新增成本 | 暫存累計成本 |
|---|---|---:|---:|---:|
| 原有成本 | — | — | — | 3 |
| `[1,4)` | `3 → 6` | 3 | 9 | 12 |
| `[4,5)` | `4 → 6` | 1 | 2 | 14 |
| `[5,6)` | `4 → 6` | 1 | 2 | 16 |

合併過程中的數字是逐段更新的暫存值；所有較低平台處理完畢後，才得到新視窗完整的最低成本。將索引 `0` 放入佇列後：

```text
index       0  1  2  3  4  5
nums        6  3  1  2  4  4
target      6  6  6  6  6  6
increase    0  3  5  4  2  2

deque = [0(6)]
cost  = 3 + 9 + 2 + 2 = 16
```

### 4.7 成本超支：從右端逐個移除

目前 `cost = 16 > k = 7`，必須縮窗。

每個位置的目標高度只取決於它左邊的元素。移除最後一個元素，不會影響更早位置的前綴最大值，因此只需扣掉被移除元素自己的提升量。

佇列前端是覆蓋目前 `right` 的平台起點，所以右端元素的目標高度為 `nums[deque[head]]`，提升量為：

```csharp
operations -= (long)nums[deque[head]] - nums[right];
```

本例佇列是 `[0(6)]`，因此每個被移除元素的目標高度都為 `6`。

```text
right = 5: remove original value 4
saved cost = 6 - 4 = 2
cost = 16 - 2 = 14

remaining nums    [6, 3, 1, 2, 4]
remaining target  [6, 6, 6, 6, 6]
```

```text
right = 4: remove original value 4
saved cost = 6 - 4 = 2
cost = 14 - 2 = 12

remaining nums    [6, 3, 1, 2]
remaining target  [6, 6, 6, 6]
```

```text
right = 3: remove original value 2
saved cost = 6 - 2 = 4
cost = 12 - 4 = 8

remaining nums    [6, 3, 1]
remaining target  [6, 6, 6]
```

```text
right = 2: remove original value 1
saved cost = 6 - 1 = 5
cost = 8 - 5 = 3

remaining nums    [6, 3]
remaining target  [6, 6]
```

整個縮窗流程可整理為：

| 被移除的 right | 原值 | 目標高度 | 扣除成本 | 剩餘成本 | 新 right |
|---:|---:|---:|---:|---:|---:|
| 5 | 4 | 6 | 2 | 14 | 4 |
| 4 | 4 | 6 | 2 | 12 | 3 |
| 3 | 2 | 6 | 4 | 8 | 2 |
| 2 | 1 | 6 | 5 | 3 | 1 |

現在成本 `3 <= 7`，停止縮窗：

```text
index      0  1 | 2  3  4  5
nums       6  3 | 1  2  4  4
           L  R   <--------->
                   removed

valid window: [0,1]
target:      [6,6]
cost:        0 + 3 = 3 <= 7
```

程式另外處理平台完全離開視窗的情況：

```csharp
if (deque[head] == right)
{
    head++;
}

right--;
```

當被移除的位置就是最右平台的起點，代表該平台已沒有任何元素留在視窗內，便將它從佇列前端移除；若起點仍在視窗內，就繼續保留該平台。

本例縮窗期間，平台起點始終為 `0`，被移除的索引則為 `5、4、3、2`，因此不會觸發 `head++`，佇列一直維持 `[0(6)]`。

### 4.8 為什麼每輪加上 right - left + 1？完整答案追蹤

固定起點 `left`，若 `[left, right]` 合法，選擇更早的終點也一定合法，因為少處理幾個元素不會增加成本。因此本輪合法終點為 `left` 到 `right`，共有 `right - left + 1` 個：

```csharp
answer += right - left + 1;
```

最後一輪 `left = 0`、`right = 1`，有兩個合法子陣列：

```text
[6]     cost = 0
[6,3]   cost = 3

new subarrays = 1 - 0 + 1 = 2
answer = 15 + 2 = 17
```

以下佇列以 `head -> tail` 表示，內容只列索引；成本與視窗均為本輪縮窗完成後的狀態：

| left | 新值 | 最終 right | 佇列 | 最終視窗 | 成本 | 新增數量 | 累計答案 |
|---:|---:|---:|---|---|---:|---:|---:|
| 5 | 4 | 5 | `[5]` | `[4]` | 0 | 1 | 1 |
| 4 | 4 | 5 | `[5,4]` | `[4,4]` | 0 | 2 | 3 |
| 3 | 2 | 5 | `[5,4,3]` | `[2,4,4]` | 0 | 3 | 6 |
| 2 | 1 | 5 | `[5,4,3,2]` | `[1,2,4,4]` | 0 | 4 | 10 |
| 1 | 3 | 5 | `[5,4,1]` | `[3,1,2,4,4]` | 3 | 5 | 15 |
| 0 | 6 | 1 | `[0]` | `[6,3]` | 3 | 2 | 17 |

此方法按起點計數，方法一按終點計數。本方法各輪新增數量合計為 `1 + 2 + 3 + 4 + 5 + 2 = 17`，與方法一的結果一致。

### 4.9 正確性與複雜度

- 新左端點只會抬高比它低的平台；以高度差乘上區段寬度，恰好補上新增的必要成本。
- 移除右端點只影響該元素自己的提升量，不影響其餘位置的前綴最大值，因此扣除公式正確。
- `right` 只往左移。對先前已排除的更遠終點，加入新的左端元素也不可能降低其成本，因此不需再把右端向右擴展。
- 縮窗完成後，所有較短終點都合法，所有更遠終點都超支；本輪計數完整且不重複。

每個索引只加入一次，且至多由前端或尾端移除一次；兩個視窗端點也各移動至多 `n` 次。雖然有巢狀 `while`，累計操作數仍為線性，總時間為 **`O(n)`**，額外空間為 **`O(n)`**。

陣列佇列不需要環狀索引：全程只有 `n` 次加入，尾端彈出只會減少 `tail`，因此長度 `n` 已足夠。

## 5. 暴力參考解法：用來交叉驗證

對應函式：`CountNonDecreasingSubarraysBruteForce(int[] nums, int k)`，僅供小陣列測試使用。

對每個起點向右枚舉終點，一邊維護前綴最大值，一邊加入 `prefixMax - nums[right]`。成本不超過 `k` 就將答案加一；超支後停止這個起點的延伸。

例如 `[3,1,2]`、`k = 2`：

```text
left = 0:
  [3]       -> target [3]       -> cost 0 -> count
  [3,1]     -> target [3,3]     -> cost 2 -> count
  [3,1,2]   -> target [3,3,3]   -> cost 3 -> stop

left = 1:
  [1]       -> target [1]       -> cost 0 -> count
  [1,2]     -> target [1,2]     -> cost 0 -> count

left = 2:
  [2]       -> target [2]       -> cost 0 -> count

answer = 2 + 2 + 1 = 5
```

這個參考方法直接依照最低成本定義計算，不使用兩種正式解法的平台資料結構，可以用來檢查成本維護是否出錯。最壞時間為 `O(n²)`、額外空間為 `O(1)`，不在大型案例上執行。

## 6. 解法比較與數值範圍

| 方法 | 掃描方向 | 成本更新方式 | 時間 | 額外空間 |
|---|---|---|---|---|
| 方法一 | 終點由左往右 | 最大堆取得新元素成本；覆蓋關係處理移除左端點 | `O(n log n)` | `O(n)` |
| 方法二 | 起點由右往左 | 尾端合併平台；前端取得移除右端點的成本 | `O(n)` | `O(n)` |
| 暴力參考 | 逐一起點向右枚舉 | 直接累加前綴最大值與原值之差 | `O(n²)` | `O(1)` |

答案最多為 `100000 × 100001 / 2 = 5000050000`，超過 `int` 上限，必須使用 `long`。

成本也必須使用 `long`。例如第二種解法處理「第一個值為 `1000000000`、後面 99999 個值為 `1`」時，在縮窗前會暫時計算：

```text
(1000000000 - 1) * 99999 = 99998999900001
```

即使 `k` 不超過十億，暫時計算的成本仍可能很大。程式在乘法前先轉為 `long`，避免先以 `int` 計算後才轉型。

## 7. 可執行測試

`Main` 包含以下 13 筆固定案例，每筆都執行兩種正式解法，分別檢查答案及輸入陣列未被修改。

| 測試名稱 | nums | k | 預期答案 | 檢查重點 |
|---|---|---:|---:|---|
| Example 1 | `[6,3,1,2,4,4]` | 7 | 17 | 官方範例、平台合併及縮窗 |
| Example 2 | `[6,3,1,3,6]` | 4 | 12 | 官方範例、後方高峰 |
| Single element | `[1]` | 1 | 1 | 最短合法輸入 |
| Increasing | `[1,2,3,4]` | 1 | 10 | 原本嚴格遞增，所有子陣列合法 |
| All equal | `[2,2,2,2]` | 1 | 10 | 相等高度、所有子陣列合法 |
| Decreasing | `[5,4,3,2,1]` | 3 | 12 | 反覆累積成本；長度至多三才合法 |
| Exact budget | `[3,1,2]` | 3 | 6 | 成本剛好等於 k 仍合法 |
| Below required budget | `[3,1,2]` | 2 | 5 | 完整陣列差一次操作而不合法 |
| Gap exceeds budget | `[5,1]` | 3 | 2 | 即使只有兩個元素仍可能超支 |
| Repeated plateaus | `[4,4,1,1,5]` | 3 | 11 | 重複高峰與低平台的邊界 |
| Large values | `[1000000000,1,1]` | 1000000000 | 5 | 元素及 k 的上限 |
| Large equal array | 100000 個 `1` | 1 | 5000050000 | 長度上限、答案超過 int |
| Large plateau cost | `1000000000` 後接 99999 個 `1` | 1000000000 | 4999950002 | 平台成本超過 int、連續縮窗 |

最後一筆的後 99999 個元素全相等，形成 `99999 × 100000 / 2 = 4999950000` 個合法子陣列。從第一個元素開始，只有長度一與二合法，因此再加 `2` 得到 `4999950002`。

固定案例之外，還有 1000 筆小陣列交叉驗證：

- 使用 `.NET Random`，固定種子為 `3420`。
- 長度為 1～12，元素為 1～10，`k` 為 1～50。
- 先由暴力參考方法計算答案，再比較兩種正式解法。
- 任一答案不符或輸入被修改，就拋出 `InvalidOperationException`；錯誤訊息包含測試名稱、輸入、預算、預期值及實際值。
- 固定案例逐筆列印；隨機案例全部通過後只顯示總結，讓正常輸出保持精簡。

## 8. 建置與執行

需要 **.NET 11 SDK**，目標框架為 `net11.0`，不需要額外的第三方套件或獨立測試專案。

在 PowerShell 切換到本 README 所在的專案根目錄：

```powershell
Set-Location C:\GitHubFolder\Leetcode_folder\leetcode_3420
dotnet build .\leetcode_3420\leetcode_3420.csproj --configuration Release
dotnet run --project .\leetcode_3420\leetcode_3420.csproj --configuration Release --no-build
git diff --check
```

`--no-build` 使用前一個命令產生的 Release 組件。首次直接執行時，也可讓 `dotnet run` 同時建置：

```powershell
dotnet run --project .\leetcode_3420\leetcode_3420.csproj --configuration Release
```

Release 建置應成功且為 0 個警告、0 個錯誤。實際範例執行輸出如下：

```text
LeetCode 3420: fixed test cases
[PASS] Example 1: expected=17, method1=17, method2=17
[PASS] Example 2: expected=12, method1=12, method2=12
[PASS] Single element: expected=1, method1=1, method2=1
[PASS] Increasing: expected=10, method1=10, method2=10
[PASS] All equal: expected=10, method1=10, method2=10
[PASS] Decreasing: expected=12, method1=12, method2=12
[PASS] Exact budget: expected=6, method1=6, method2=6
[PASS] Below required budget: expected=5, method1=5, method2=5
[PASS] Gap exceeds budget: expected=2, method1=2, method2=2
[PASS] Repeated plateaus: expected=11, method1=11, method2=11
[PASS] Large values: expected=5, method1=5, method2=5
[PASS] Large equal array: expected=5000050000, method1=5000050000, method2=5000050000
[PASS] Large plateau cost: expected=4999950002, method1=4999950002, method2=4999950002
[PASS] Random cross-check: 1000 cases (seed=3420).
All tests passed: 13 fixed cases + 1000 random cases.
```

全部通過時程序回傳 `0`；驗證失敗時因未處理例外而回傳非零狀態。`git diff --check` 通過時不會列印任何訊息，回傳值為 `0`。
