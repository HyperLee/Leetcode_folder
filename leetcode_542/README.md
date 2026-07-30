# 542. 01 Matrix／01 矩陣

給定一個只包含 `0` 與 `1` 的矩陣，計算每一格到最近 `0` 的距離。本專案保留兩種會原地
更新輸入矩陣的教學解法：從所有 0 同時向外擴散的多源廣度優先搜尋，以及從相反方向各掃描
一次的雙向動態規劃。

- [LeetCode English](https://leetcode.com/problems/01-matrix/description/)
- [LeetCode 中文](https://leetcode.cn/problems/01-matrix/description/)

## 題目說明

給定 `m x n` 二元矩陣 `mat`，回傳一個相同大小的矩陣，其中每一格的值代表原位置到最近
`0` 的距離。只能沿上、下、左、右移動；兩個共用邊的相鄰格距離為 1。

問題可以視為「每個格子到任一個 0 的最短路徑」：

- 原值為 0 的格子距離必定是 0。
- 原值為 1 的格子可能從不同方向抵達 0，必須取其中最短距離。
- 題目保證至少存在一個 0，因此每格都能得到有限答案。

## 限制條件

- `m == mat.Length`
- `n == mat[row].Length`
- `1 <= m, n <= 10^4`
- `1 <= m * n <= 10^4`
- `mat[row][column]` 只會是 `0` 或 `1`
- `mat` 至少包含一個 `0`
- `UpdateMatrix` 與 `UpdateMatrix2` 都只處理上述有效輸入
- 兩個公開方法都會原地修改 `mat`，並回傳同一個矩陣參考

## 解法比較

| 解法 | 核心做法 | 時間複雜度 | 輔助空間 | 是否修改輸入 |
| --- | --- | --- | --- | --- |
| `UpdateMatrix` | 所有 0 同時入隊，執行多源 BFS | `O(mn)` | 最壞 `O(mn)` | 是 |
| `UpdateMatrix2` | 依相反方向進行兩趟 DP 掃描 | `O(mn)` | `O(1)` | 是 |

兩種方法都直接把答案寫回輸入矩陣，因此不另外配置 `m x n` 的結果矩陣。BFS 的佇列在最壞
情況下仍可能保存 `O(mn)` 個位置；雙向 DP 只使用迴圈索引、尺寸與哨兵值，所以輔助空間為
`O(1)`。

## 解法一：多源廣度優先搜尋

### 解題出發點

如果從每一個 1 分別搜尋最近的 0，許多搜尋路徑會重複。反過來思考，可以把所有 0 都當成
距離為 0 的起點，同時向外執行 BFS：

1. 掃描矩陣，把每個 0 的座標加入佇列。
2. 把每個 1 改成 `-1`，表示尚未造訪。
3. 依序取出佇列中的位置，檢查上、下、左、右四個鄰居。
4. 若鄰居仍是 `-1`，它第一次被抵達時的距離就是目前格距離加 1。
5. 在鄰居入隊前立刻寫入距離，避免相同位置被其他來源重複加入。

BFS 會按照距離由小到大處理位置。某個格子第一次被任何來源抵達時，不可能再由後續路徑
取得更短距離，因此不需要重複更新。

### 範例演示

使用官網第二個範例：

```plaintext
輸入：
[[0,0,0],
 [0,1,0],
 [1,1,1]]
```

先將所有 0 入隊，並把 1 標成尚未造訪的 `-1`：

```plaintext
[[ 0, 0, 0],
 [ 0,-1, 0],
 [-1,-1,-1]]
```

處理距離為 0 的來源後，`(1,1)`、`(2,0)` 與 `(2,2)` 第一次被抵達，距離都是 1：

```plaintext
[[ 0, 0, 0],
 [ 0, 1, 0],
 [ 1,-1, 1]]
```

接著處理距離為 1 的格子，中央下方 `(2,1)` 第一次被抵達，距離為 2：

```plaintext
[[0,0,0],
 [0,1,0],
 [1,2,1]]
```

佇列清空後，每一格都已得到最近距離。

## 解法二：雙向動態規劃

### 解題出發點

一個格子的最近 0 可能位於四個方向。如果只從左上往右下掃描，當前格只能安全使用已處理
過的上方與左方；位於右方或下方的 0 尚未被看見。因此需要兩趟互補掃描：

1. 把所有 1 改成 `rows + columns`。
2. 第一趟由左上往右下，使用上方與左方距離更新目前格。
3. 第二趟由右下往左上，使用下方與右方距離再次更新目前格。
4. 每次更新都保留目前值與鄰格距離加 1 的較小者。

矩陣內兩格最長的曼哈頓距離是 `rows + columns - 2`，所以 `rows + columns` 一定大於任何
有效答案，適合作為「尚未找到 0」的哨兵值。兩趟掃描分別涵蓋四個相鄰方向，最後留下所有
候選路徑中的最小距離。

### 範例演示

使用右下角唯一 0 的案例：

```plaintext
輸入：
[[1,1,1],
 [1,1,1],
 [1,1,0]]
```

矩陣有 3 列、3 欄，哨兵值為 `3 + 3 = 6`。初始化後：

```plaintext
[[6,6,6],
 [6,6,6],
 [6,6,0]]
```

第一趟從左上往右下時，每個 1 的上方與左方都尚未連到右下角的 0，因此仍保持 6：

```plaintext
[[6,6,6],
 [6,6,6],
 [6,6,0]]
```

第二趟從右下往左上，0 的距離會依序向左與向上傳遞：

```plaintext
[[4,3,2],
 [3,2,1],
 [2,1,0]]
```

這也說明為何只做第一趟不足：若最近的 0 位於目前格的右方或下方，必須靠反向掃描補上。

## Acceptance Harness

專案沒有獨立的自動化測試專案；`Main` 是可重複執行的 acceptance harness。每個案例會為
BFS 與 DP 建立兩份深層副本，避免第一個原地演算法改變第二個演算法的輸入。案例只有在下列
條件全部成立時才會顯示 `PASS`：

1. `UpdateMatrix` 的結果等於預期矩陣。
2. `UpdateMatrix2` 的結果等於預期矩陣。
3. 兩個方法都回傳各自的輸入矩陣參考。

任何案例失敗都會讓 process exit code 成為 1。

| # | 案例 | 輸入 | 預期輸出 |
| ---: | --- | --- | --- |
| 1 | 官網範例 1 | `[[0,0,0],[0,1,0],[0,0,0]]` | `[[0,0,0],[0,1,0],[0,0,0]]` |
| 2 | 官網範例 2 | `[[0,0,0],[0,1,0],[1,1,1]]` | `[[0,0,0],[0,1,0],[1,2,1]]` |
| 3 | 單一格 | `[[0]]` | `[[0]]` |
| 4 | 單列遠距離 | `[[0,1,1,1]]` | `[[0,1,2,3]]` |
| 5 | 單欄且 0 位於中間 | `[[1],[1],[0],[1]]` | `[[2],[1],[0],[1]]` |
| 6 | 非方形、多個來源 | `[[0,1,1,1],[1,1,1,0]]` | `[[0,1,2,1],[1,2,1,0]]` |
| 7 | 右下角唯一 0 | `[[1,1,1],[1,1,1],[1,1,0]]` | `[[4,3,2],[3,2,1],[2,1,0]]` |

## 建置與執行

從 `leetcode_542` 題目目錄執行：

```bash
dotnet restore leetcode_542/leetcode_542.csproj
dotnet build leetcode_542/leetcode_542.csproj --no-restore --nologo
dotnet run --no-build --project leetcode_542/leetcode_542.csproj
```

專案目前沒有測試專案，因此 build 與 acceptance harness 是行為驗收依據。建置結果應為
0 個警告、0 個錯誤；以下是 fresh run 的完整輸出：

```text
Case: Official example 1
Input: [[0,0,0],[0,1,0],[0,0,0]]
Expected: [[0,0,0],[0,1,0],[0,0,0]]
UpdateMatrix (BFS): [[0,0,0],[0,1,0],[0,0,0]]
UpdateMatrix2 (DP): [[0,0,0],[0,1,0],[0,0,0]]
BFS returned input reference: True
DP returned input reference: True
Result: PASS

Case: Official example 2
Input: [[0,0,0],[0,1,0],[1,1,1]]
Expected: [[0,0,0],[0,1,0],[1,2,1]]
UpdateMatrix (BFS): [[0,0,0],[0,1,0],[1,2,1]]
UpdateMatrix2 (DP): [[0,0,0],[0,1,0],[1,2,1]]
BFS returned input reference: True
DP returned input reference: True
Result: PASS

Case: Single cell
Input: [[0]]
Expected: [[0]]
UpdateMatrix (BFS): [[0]]
UpdateMatrix2 (DP): [[0]]
BFS returned input reference: True
DP returned input reference: True
Result: PASS

Case: Single row / distant zero
Input: [[0,1,1,1]]
Expected: [[0,1,2,3]]
UpdateMatrix (BFS): [[0,1,2,3]]
UpdateMatrix2 (DP): [[0,1,2,3]]
BFS returned input reference: True
DP returned input reference: True
Result: PASS

Case: Single column / middle zero
Input: [[1],[1],[0],[1]]
Expected: [[2],[1],[0],[1]]
UpdateMatrix (BFS): [[2],[1],[0],[1]]
UpdateMatrix2 (DP): [[2],[1],[0],[1]]
BFS returned input reference: True
DP returned input reference: True
Result: PASS

Case: Rectangular matrix / multiple sources
Input: [[0,1,1,1],[1,1,1,0]]
Expected: [[0,1,2,1],[1,2,1,0]]
UpdateMatrix (BFS): [[0,1,2,1],[1,2,1,0]]
UpdateMatrix2 (DP): [[0,1,2,1],[1,2,1,0]]
BFS returned input reference: True
DP returned input reference: True
Result: PASS

Case: Bottom-right zero / long distances
Input: [[1,1,1],[1,1,1],[1,1,0]]
Expected: [[4,3,2],[3,2,1],[2,1,0]]
UpdateMatrix (BFS): [[4,3,2],[3,2,1],[2,1,0]]
UpdateMatrix2 (DP): [[4,3,2],[3,2,1],[2,1,0]]
BFS returned input reference: True
DP returned input reference: True
Result: PASS

Summary: 7/7 checks passed.
```

## 專案結構

```plaintext
leetcode_542/
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_542.sln
└── leetcode_542/
    ├── Program.cs
    └── leetcode_542.csproj
```

`bin/` 與 `obj/` 是建置產物，不應納入版本控制。
