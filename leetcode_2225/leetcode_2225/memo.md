### 比賽資料

```csharp
new int[] { 1, 3 },   // 1 贏 3
new int[] { 2, 3 },   // 2 贏 3
new int[] { 3, 6 },   // 3 贏 6
new int[] { 5, 6 },   // 5 贏 6
new int[] { 5, 7 },   // 5 贏 7
new int[] { 4, 5 },   // 4 贏 5
new int[] { 4, 8 },   // 4 贏 8
new int[] { 4, 9 },   // 4 贏 9
new int[] { 10, 4 },  // 10 贏 4
new int[] { 10, 9 }   // 10 贏 9

玩家分析
玩家 1、2 和 10 都沒有輸掉任何比賽。
玩家 4、5、7 和 8 每個都輸掉一場比賽。
玩家 3、6 和 9 每個都輸掉兩場比賽。

因此：

answer[0] = [1,2,10]
answer[1] = [4,5,7,8]

題目描述
給你一個整數陣列 matches，其中 matches[i] = [winneri, loseri] 表示在一場比賽中 winneri 擊敗了 loseri。

[x, y] 代表 x 贏 y。
簡單分析
x 贏 y
如果 y 這變數都沒有出現，x 就代表 x 沒輸過。
如果 x 出現一次，那就是指輸一次。
如果 x 出現兩次，那就是輸兩次。
依此類推。
