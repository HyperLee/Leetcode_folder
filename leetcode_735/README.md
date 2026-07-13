# LeetCode 735：Asteroid Collision／小行星碰撞

這是一個以 C# 撰寫的 .NET 10 主控台專案。解法保留單一
`Stack<int>` 模擬：`AsteroidCollision` 只計算並回傳存活結果，
`Main` 則負責可重複執行的 acceptance harness。

- [英文題目：735. Asteroid Collision](https://leetcode.com/problems/asteroid-collision/)
- [中文題目：735. 小行星碰撞](https://leetcode.cn/problems/asteroid-collision/)

## 題目說明

每個整數代表一顆小行星的大小與移動方向：絕對值是大小，正值往右移動，負值
往左移動。所有小行星速度相同，因此只有「左邊向右」與「右邊向左」的相鄰存活
小行星會迎面碰撞。

- 較小的小行星被摧毀。
- 大小相同時，兩者都被摧毀。
- 同方向移動的小行星不會彼此碰撞。

回傳所有碰撞完成後仍存活的小行星，且結果必須維持原始的由左至右順序。

## 限制條件

- `2 <= asteroids.Length <= 10^4`
- `-1000 <= asteroids[i] <= 1000`
- `asteroids[i] != 0`
- 所有小行星以相同速度移動。

實作依照題目的有效輸入契約設計，不另外定義無效輸入的行為。

## Stack 不變量與解法設計

`Stack<int>` 從底到頂保存「目前已存活、且尚可能與之後輸入碰撞」的小行星，
順序與原始輸入相同。讀到新小行星時：

1. 正值直接推入 stack；它向右移動，尚未遇到來自右側的左移小行星。
2. 負值只有在 stack 頂端為正值時才可能碰撞。
3. 若頂端較小，頂端被移除，而仍存活的 incoming asteroid 必須繼續比較下一顆。
4. 若大小相同，頂端與 incoming asteroid 都消失。
5. 若頂端較大，incoming asteroid 消失。

所有輸出都由 `Main` 產生；公開 API 不修改輸入陣列，也不寫入主控台。

```csharp
public static int[] AsteroidCollision(int[] asteroids)
```

## `[10, 2, -5]` 逐步走查

| 讀入小行星 | 碰撞處理 | Stack（底 → 頂） |
| ---: | --- | --- |
| `10` | 向右，直接存活 | `[10]` |
| `2` | 向右，直接存活 | `[10, 2]` |
| `-5` | `2` 較小先消失；再與 `10` 比較後自身消失 | `[10]` |

因此答案是：

```plaintext
[10]
```

## 複雜度

- 時間複雜度：`O(n)`。每顆小行星最多推入與彈出 stack 各一次。
- 結果空間：`O(n)`。回傳陣列最多容納全部輸入。
- 輔助空間：`O(n)`。最壞情況下 stack 保存全部尚存活的小行星。

## 可執行驗證案例

`Main` 共有 9 組案例與 11 項檢查：

| 案例 | 輸入 | 檢查數 | 驗證重點 |
| --- | --- | ---: | --- |
| 1 | `[5, 10, -5]` | 1 | 官方範例：較小 incoming asteroid 消失 |
| 2 | `[8, -8]` | 1 | 官方範例：相同大小同時消失 |
| 3 | `[10, 2, -5]` | 1 | 官方範例：連鎖比較後保留 `10` |
| 4 | `[1, -2, -2, -2]` | 1 | 官方範例：左移 incoming asteroid 存活 |
| 5 | `[1, 2]` | 1 | 最小有效輸入且沒有碰撞 |
| 6 | `[-2, -1]` | 1 | 同向左移不碰撞 |
| 7 | `[1, 2, 3, -4]` | 1 | 一顆 incoming asteroid 擊敗多顆存活者 |
| 8 | `[3, 5, -5]` | 1 | 相同碰撞後保留較早的 `3` |
| 9 | 10,000 顆 `1000` 全向右 | 3 | 有效上限大小下的數量、首值與尾值 spot checks |

每項檢查都會輸出輸入、Expected、Actual 與 PASS/FAIL。只要有任何失敗，程式會
將 `Environment.ExitCode` 設為 `1`。本題沒有獨立 test project；這個
acceptance harness 是目前的驗證機制。

## 建置與執行

請從此 README 所在的外層 `leetcode_735` 目錄執行：

```bash
dotnet build leetcode_735/leetcode_735.csproj --nologo
dotnet run --no-build --project leetcode_735/leetcode_735.csproj
```

以下是完成建置後執行第二個命令的完整輸出：

```text
LeetCode 735 acceptance harness

Case 1: Official example
Input: [5, 10, -5]
PASS | Survivors | Expected: [5, 10] | Actual: [5, 10]

Case 2: Official equal-size collision
Input: [8, -8]
PASS | Survivors | Expected: [] | Actual: []

Case 3: Official chained collision
Input: [10, 2, -5]
PASS | Survivors | Expected: [10] | Actual: [10]

Case 4: Official incoming survivor
Input: [1, -2, -2, -2]
PASS | Survivors | Expected: [-2, -2, -2] | Actual: [-2, -2, -2]

Case 5: Minimum valid no-collision input
Input: [1, 2]
PASS | Survivors | Expected: [1, 2] | Actual: [1, 2]

Case 6: Same leftward direction
Input: [-2, -1]
PASS | Survivors | Expected: [-2, -1] | Actual: [-2, -1]

Case 7: Incoming asteroid defeats several survivors
Input: [1, 2, 3, -4]
PASS | Survivors | Expected: [-4] | Actual: [-4]

Case 8: Equal collision preserves earlier survivor
Input: [3, 5, -5]
PASS | Survivors | Expected: [3] | Actual: [3]

Case 9: Upper-bound spot checks
Input: 10,000 right-moving asteroids (all 1000)
PASS | Valid input and survivor count | Expected: 10000 valid asteroids | Actual: 10000 survivors; valid input: True
PASS | First survivor | Expected: 1000 | Actual: 1000
PASS | Last survivor | Expected: 1000 | Actual: 1000

Summary: 11/11 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig              # C# 與結構化檔案格式規範
├── .gitattributes             # 文字與二進位檔案屬性
├── .gitignore                 # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json            # 直接偵錯 net10.0 輸出
│   └── tasks.json             # 預設建置工作
├── docs/
│   └── readme-template.md     # 初次建立 README 的範本
├── leetcode_735/
│   ├── Program.cs             # 純 Stack 解法與 acceptance harness
│   └── leetcode_735.csproj    # .NET 10 SDK 專案設定
├── AGENTS.md                  # 本題協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
