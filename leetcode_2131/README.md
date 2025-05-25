# LeetCode 2131: 連接兩字母單詞得到的最長回文字串

## 問題描述

給定一個字串陣列 words。words 中的每個元素由兩個小寫英文字母組成。

通過從 words 中選擇一些元素並以任意順序將它們連接起來，建立最長的回文字串。每個元素最多只能選擇一次。

返回你能建立的最長回文字串的長度。如果不可**優點**：

- 使用二維陣列可以直接用字母的 ASCII 值作為索引，省去了字串的比較和轉換操作
- 定長的二維陣列 (26x26) 無論輸入規模多大，記憶體使用量保持恆定
- 適用於字母集有限的情況
- 使用位元運算 (odd |= c % 2) 高效處理中間回文單詞的判斷

**缺點**：

- 使用位元運算可能降低程式碼可讀性
- 固定分配 26x26 的陣列，若單詞集中使用少量字母可能浪費空間
- 限制為小寫字母的處理方式，不適用於更廣泛的字元集返回 0。

回文字串是指正向和反向讀取都相同的字串。

## 範例

### 範例 1

```
輸入: ["lc", "cl", "gg"]
輸出: 6
解釋: 我們可以連接 "lc", "gg", "cl" 得到 "lcggcl"，長度為 6。
```

### 範例 2

```
輸入: ["ab", "ty", "yt", "lc", "cl", "ab"]
輸出: 8
解釋: 我們可以連接 "ab", "ty", "yt", "lc", "cl", "ab" 得到 "abtytylcclab"，但這不是回文。
      我們可以連接 "ty", "yt", "lc", "cl" 得到 "tyytlccl"，長度為 8。
```

### 範例 3

```
輸入: ["cc", "ll", "xx"]
輸出: 2
解釋: 我們可以連接 "cc" 得到 "cc"，長度為 2。
```

## 解題思路

要建立最長的回文字串，我們需要考慮兩種類型的單詞：

1. **回文單詞**：如 "aa", "bb" 等，首尾字母相同的單詞
2. **非回文單詞**：如 "ab", "cd" 等，首尾字母不同的單詞

對於非回文單詞，如 "ab"，它只能與其反轉 "ba" 配對使用，這樣才能形成回文。對於回文單詞，可以單獨放在回文的中間 (且最多只能放一個)，或者成對放在回文的兩端。

## 解法比較

本專案實作了三種不同的解法，每種都有其特點與優缺點：

### 1. 字典 + 單字特性比較法 (LongestPalindrome)

```csharp
public static int LongestPalindrome(string[] words)
{
    Dictionary<string, int> count = new Dictionary<string, int>();
    int answer = 0;
    bool central = false;
    
    // 建立所有字串的頻率字典
    foreach (string word in words)
    {
        if (!count.ContainsKey(word))
        {
            // 如果字典中不存在，則初始化頻率為 1
            count[word] = 1;
        }
        else
        {
            // 如果已經存在，則增加頻率 
            count[word]++;
        }
    }
    
    foreach (var entry in count) 
    {
        string word = entry.Key;
        int frequency = entry.Value;
        
        // 處理 "aa", "bb" 等形式的回文單詞(首尾字母相同)
        if (word[0] == word[1])
            {
                // 將偶數個回文單詞放在回文的兩側，每對貢獻 4 個字元
                answer += (frequency / 2) * 4;

                // 如果有奇數個回文單詞，可以將其中一個放在中間
                if (frequency % 2 == 1)
                {
                    central = true; // 標記可以放一個在中間
                }
            }
            // 處理 "ab" 和 "ba" 等需要配對的單詞(首尾字母不同)
            else if (word[0] < word[1])
            {
                // 構造反轉單詞，如 "ab" -> "ba"
                string reversed = "" + word[1] + word[0];

                // 如果反轉單詞存在，就可以形成配對
                if (count.ContainsKey(reversed))
                {
                    // 取較小頻率作為配對數，每對貢獻 4 個字元
                    answer += Math.Min(frequency, count[reversed]) * 4;
                }
            }
    }
    
    if (central) 
        answer += 2;
        
    return answer;
}
```

**優點**：

- 直觀易懂，使用字典儲存並查找每個單詞的頻率
- 使用字典鍵比較避免重複計算，只處理 word \[0] < word \[1] 的情況
- 程式碼簡潔，邏輯清晰

**缺點**：

- 需要額外的記憶體來儲存字典
- 字串合併操作 ("" + word \[1] + word \[0]) 可能不如直接索引效率高

**時間複雜度**： O (n)，其中 n 是單詞數量
**空間複雜度**  ： O(n)

**參考資料**：

- [LeetCode 官方解法](https://leetcode.com/problems/longest-palindrome-by-concatenating-two-letter-words/editorial/)

### 2. 二維陣列實作法 (LongestPalindrome2)

```csharp
public static int LongestPalindrome2(string[] words)
{
    int[][] cnt = new int[26][];
    for (int i = 0; i < 26; i++)
    {
        cnt[i] = new int[26];
    }

    foreach (string w in words)
    {
        cnt[w[0] - 'a'][w[1] - 'a']++;
    }

    int ans = 0;
    int odd = 0;
    
    for (int i = 0; i < 26; i++)
    {
        int c = cnt[i][i];
        ans += c - c % 2;
        odd |= c % 2;
        
        for (int j = i + 1; j < 26; j++)
        {
            ans += Math.Min(cnt[i][j], cnt[j][i]) * 2;
        }
    }
    
    return (ans + odd) * 2;
}
```

**優點**：

- 使用二維陣列可以直接用字母的 ASCII 值作為索引，省去了字串的比較和轉換操作
- 定長的二維陣列 (26x26) 無論輸入規模多大，記憶體使用量保持恆定
- 適用於字母集有限的情況

**缺點**：

- 使用位元運算可能降低程式碼可讀性
- 固定分配 26x26 的陣列，若單詞集中使用少量字母可能浪費空間
- 限制為小寫字母的處理方式，不適用於更廣泛的字元集

**時間複雜度**： O (n + 26²)，其中 n 是單詞數量
**空間複雜度**  ： O(26²) = O(1)

**參考資料**：

- [構造貪心分類討論](https://leetcode.cn/problems/longest-palindrome-by-concatenating-two-letter-words/solutions/1199641/gou-zao-tan-xin-fen-lei-tao-lun-by-endle-dqr8/)

### 3. 字典及字串比較法 (LongestPalindrome3)

```csharp
public static int LongestPalindrome3(string[] words)
{
    Dictionary<string, int> freq = new Dictionary<string, int>();
    foreach (string word in words)
    {
        freq[word] = freq.GetValueOrDefault(word, 0) + 1;
    }

    int res = 0;
    bool mid = false;
    
    foreach (var entry in freq)
    {
        string word = entry.Key;
        int cnt = entry.Value;
        string rev = "" + word[1] + word[0];
        
        // 如果單詞本身就是回文(如 "aa", "bb")
        if (word == rev)
        {
            // 如果出現次數為奇數，可以放一個在中間
            if (cnt % 2 == 1)
                mid = true;
            // 偶數次數的回文單詞可以放在兩側，每對貢獻 4 個字元
            res += 2 * (cnt / 2 * 2);
        }
        // 對於需要配對的單詞，使用字串比較確保每一對只計算一次
        else if (string.Compare(word, rev) > 0)
        {                    
            // 找出 word 和 rev 中較少的出現次數，每對貢獻 4 個字元
            res += 4 * Math.Min(cnt, freq.GetValueOrDefault(rev, 0));
        }
    }

    if (mid)
        res += 2;
        
    return res;
}
```

**優點**：

- 使用 `GetValueOrDefault` 簡化字典操作
- 通過 `string.Compare` 確保每對配對單詞只計算一次
- 程式碼結構清晰，邏輯分明
- 使用變數 `cnt` 儲存已查詢的值，減少重複查詢字典

**缺點**：

- `string.Compare` 比單純的字元比較可能增加一些開銷
- 每次構建反轉字串，造成額外的記憶體分配和操作
- 使用 `GetValueOrDefault` 處理不存在的鍵值，略微增加程式碼複雜度

**時間複雜度**： O (n)，其中 n 是單詞數量
**空間複雜度**  ： O(n)

**參考資料**：

- [連接兩字母單詞得到的最長回文字串](https://leetcode.cn/problems/longest-palindrome-by-concatenating-two-letter-words/solutions/1202034/lian-jie-liang-zi-mu-dan-ci-de-dao-de-zu-vs99/)

## 解法差異總結

1. **資料結構選擇**：
   - 解法 1 和 3 使用 `Dictionary<string, int>` 儲存單詞頻率
   - 解法 2 使用二維陣列 `int[][]` 儲存字母對頻率

2. **迭代策略**：
   - 解法 1：基於字典鍵的字元順序篩選處理
   - 解法 2：遍歷所有可能的字母對組合
   - 解法 3：使用字串比較函數決定處理順序

3. **避免重複計算**：
   - 解法 1：比較首尾字母大小 (`word[0] < word[1]`)
   - 解法 2：只考慮上三角矩陣 (`j = i + 1` 到 25)
   - 解法 3：使用 `string.Compare` 比較字典序

4. **記憶體效率**：
   - 解法 2 的空間複雜度為常數 O (26²)，在處理大規模輸入時更有效率
   - 解法 1 和 3 的空間複雜度與輸入規模成正比 O (n)

## 執行測試

程式包含 5 個測試案例，覆蓋了不同情境：

1. 基本配對 + 一個回文單詞
2. 多種配對組合
3. 純回文單詞
4. 複雜情況 (多對單詞和多個回文單詞)
5. 混合情況

測試結果顯示所有三種解法在所有測試案例上都能得出正確結果。
