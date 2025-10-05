## LeetCode 417 — Pacific Atlantic Water Flow (太平洋 / 大西洋 水流問題)

本專案以 C# (.NET 8) 實作 LeetCode 第 417 題「Pacific Atlantic Water Flow」。
程式檔案：`leetcode_417/Program.cs`。此 README 會詳細說明解題想法、輸入/輸出契約、演算法流程（含 DFS 的判斷細節）、時間/空間複雜度、邊界情況，以及如何在本機執行專案。

## 問題摘要

給定一個 m x n 的整數矩陣 `heights`，表示每個格子的高度。雨水可以向上下左右流動，條件是下一格的高度小於等於目前格子的高度。島的左邊與上邊接太平洋（Pacific），右邊與下邊接大西洋（Atlantic）。請找出所有能同時流向太平洋與大西洋的格子座標。

直覺上若從任一格模擬水流，會產生大量重複搜索。更好的做法是「反向搜索」：

- 從太平洋相鄰的邊界（左、上）往內搜尋，標記所有能抵達太平洋的格子。
- 從大西洋相鄰的邊界（右、下）往內搜尋，標記所有能抵達大西洋的格子。
- 最後取兩組標記的交集即為答案。

為何可行？水只能從高往低或同高度流動。逆向搜尋時，我們只往高度「相同或更高」的格子擴張，這等價於正向水流從內部流向海邊的條件。

## 輸入 / 輸出契約

- 輸入：`int[][] heights`（m x n 矩陣，m >= 1, n >= 1）。
- 輸出：`IList<IList<int>>`，每個項目為 `[row, col]`，代表該格能同時流到太平洋與大西洋。

注意：目前 `Program.cs` 假設矩陣 非空且為矩形（每行長度相同）。若需要更健壯的版本，應在一開始檢查 `heights == null` 或 `heights.Length == 0` 或 `heights[0].Length == 0`。

## 演算法概要（採用於 `Program.cs`）

1. 取得 m、n（行、列）。建立兩個布林矩陣 `pacificVisited[m,n]` 與 `atlanticVisited[m,n]`。
2. 太平洋起點：對每一列 `i` 呼叫 DFS(i, 0, pacificVisited, heights[i][0])，對每一欄 `j` 呼叫 DFS(0, j, pacificVisited, heights[0][j])。
3. 大西洋起點：對每一列 `i` 呼叫 DFS(i, n-1, atlanticVisited, heights[i][n-1])，對每一欄 `j` 呼叫 DFS(m-1, j, atlanticVisited, heights[m-1][j])。
4. 遍歷所有格子，若 `pacificVisited[i,j] && atlanticVisited[i,j]`，將 `[i, j]` 加入結果。

核心概念：從邊界「逆向」往內部擴張，僅允許從目前格子前往高度 >= 當前高度的鄰格。如此一來，每個格子在每個海洋的搜尋中最多被拜訪一次（visited 避免重複），整體時間為 O(m*n)。

## DFS 的實作細節（重要判斷條件）

`DFS(heights, r, c, visited, prevHeight)` 的關鍵步驟：

- 若 r 或 c 超出邊界，或 `visited[r,c] == true`，或 `heights[r][c] < prevHeight`，則直接返回（不繼續擴張）。
- 否則標記 `visited[r,c] = true`，並對 4 個方向（下、上、右、左）遞迴呼叫 DFS，下一層的 `prevHeight` 會傳入 `heights[r][c]`。

這個邏輯確保了：從邊界出發時，只能沿著高度不小於前一步的格子向內擴張，模擬逆向的「水可以從內部流到海邊」條件。

在 `Program.cs` 中，`directions` 定義如下：

```
public static int[][] directions = new int[][]
{
    new int[] {1, 0},   // 下
    new int[] {-1, 0},  // 上
    new int[] {0, 1},   // 右
    new int[] {0, -1}   // 左
};
```

順序不影響結果，但會影響遞迴/擴張路徑的順序與呼叫堆疊深度。

## 時間與空間複雜度

- 時間複雜度：O(m * n)。每格最多被兩次訪問（一次來自太平洋搜尋，一次來自大西洋搜尋）。
- 空間複雜度：O(m * n)。兩個 m x n 的 `bool[,]` 陣列；此外遞迴堆疊深度在最壞情況下也可能達 O(m*n)，但實際輸入通常低於此上限。

若要避免遞迴深度過深，可改寫為迭代式 DFS（使用 `Stack<(int r,int c)>`）或 BFS（使用 `Queue<(int r,int c)>` / 佇列）。

## 邊界情況與注意點

- 空輸入：若 `heights` 為 null 或空應直接回傳空列表（`new List<IList<int>>()`）。
- 非矩形輸入（jagged array 每行長度不同）：`Program.cs` 假設矩陣為矩形。若要支援不規則列長，呼叫前應檢查每行長度。
- 遞迴深度：在極大輸入上可能導致 StackOverflow，建議改為 BFS 或顯式堆疊的 DFS。

## 範例解析（程式中的測資）

範例 1（5x5）：

```
[1,2,2,3,5]
[3,2,3,4,4]
[2,4,5,3,1]
[6,7,1,4,5]
[5,1,1,2,4]
```

程式會：

- 從太平洋邊界（上、左）出發，標記所有能被太平洋逆向到達的格子。
- 從大西洋邊界（下、右）出發，標記所有能被大西洋逆向到達的格子。
- 最後取交集輸出（例如會包含 `[0,4], [1,3], [1,4], [2,2], ...` 等座標，詳細輸出可執行 `dotnet run` 確認）。

範例 2（2x2）：

```
[2,1]
[1,2]
```

輸出會列出能同時流向兩海的格子（此簡單矩陣可手動推導）。

## 實作建議與最佳化（可選）

- 在 `PacificAtlantic` 開頭加入輸入檢查：防止 `NullReferenceException` 或空陣列。
- 若擔心遞迴深度，將 DFS 改寫為 BFS（使用佇列）或用顯式 `Stack` 做迭代式 DFS。
- 若矩陣非常大且記憶體有限，可考慮用 bitset 或壓縮方法儲存 visited，但通常 bool[,] 已足夠且效能良好。

## 如何在本機執行

1. 進入專案資料夾（含 `leetcode_417.csproj` 的目錄）：

```powershell
cd d:\Leetcode_folder\Leetcode_folder\leetcode_417
```

2. 建構並執行（以 Debug 組態為例）：

```powershell
dotnet build leetcode_417.csproj -c Debug
dotnet run --project leetcode_417.csproj -c Debug
```

程式會在主控台列印兩組測試案例的結果，`PrintResult` 會顯示找到的每個座標。

## 參考檔案

- 主程式與解法：`leetcode_417/Program.cs`
- 題目說明（作者筆記）：`leetcode_417/題目描述.MD`

---

若你想要我直接：

- 幫你把 `PacificAtlantic` 加上輸入檢查並跑一次 `dotnet build` 與 `dotnet run`，或
- 把 `DFS` 換成迭代式 BFS 並替換現有實作，然後驗證輸出

請告訴我你要哪一項，我會立刻幫你修改並執行驗證。 
