namespace leetcode_721
{
    internal class Program
    {
        /// <summary>
        /// 721. Accounts Merge
        /// https://leetcode.com/problems/accounts-merge/description/
        /// 721. 账户合并
        /// https://leetcode.cn/problems/accounts-merge/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Write(RunSamples());
        }

        /// <summary>
        /// 執行五組固定案例，分別驗證 DFS 與 Union-Find 解法的合併結果，
        /// 並確認兩種解法都不會修改傳入的帳戶資料，最後回傳完整驗證報告。
        /// </summary>
        /// <returns>包含各案例 Expected/Actual 與 PASS/FAIL 狀態的主控台報告。</returns>
        private static string RunSamples()
        {
            var samples = new List<SampleCase>
            {
                new(
                    "官方合併案例",
                    new List<IList<string>>
                    {
                        new List<string> { "John", "johnsmith@mail.com", "john_newyork@mail.com" },
                        new List<string> { "John", "johnsmith@mail.com", "john00@mail.com" },
                        new List<string> { "Mary", "mary@mail.com" },
                        new List<string> { "John", "johnnybravo@mail.com" }
                    },
                    new List<IList<string>>
                    {
                        new List<string> { "John", "john00@mail.com", "john_newyork@mail.com", "johnsmith@mail.com" },
                        new List<string> { "Mary", "mary@mail.com" },
                        new List<string> { "John", "johnnybravo@mail.com" }
                    }),
                new(
                    "傳遞合併",
                    new List<IList<string>>
                    {
                        new List<string> { "Alex", "alex-a@mail.com", "alex-b@mail.com" },
                        new List<string> { "Alex", "alex-c@mail.com", "alex-d@mail.com" },
                        new List<string> { "Alex", "alex-b@mail.com", "alex-c@mail.com" }
                    },
                    new List<IList<string>>
                    {
                        new List<string> { "Alex", "alex-a@mail.com", "alex-b@mail.com", "alex-c@mail.com", "alex-d@mail.com" }
                    }),
                new(
                    "同名但不相連",
                    new List<IList<string>>
                    {
                        new List<string> { "Lee", "lee-one@mail.com" },
                        new List<string> { "Lee", "lee-two@mail.com" }
                    },
                    new List<IList<string>>
                    {
                        new List<string> { "Lee", "lee-one@mail.com" },
                        new List<string> { "Lee", "lee-two@mail.com" }
                    }),
                new(
                    "多個獨立元件",
                    new List<IList<string>>
                    {
                        new List<string> { "Bob", "bob-b@mail.com", "bob-a@mail.com" },
                        new List<string> { "Carol", "carol@mail.com" },
                        new List<string> { "Dana", "dana-c@mail.com", "dana-b@mail.com" },
                        new List<string> { "Bob", "bob-c@mail.com", "bob-b@mail.com" },
                        new List<string> { "Dana", "dana-a@mail.com", "dana-b@mail.com" }
                    },
                    new List<IList<string>>
                    {
                        new List<string> { "Bob", "bob-a@mail.com", "bob-b@mail.com", "bob-c@mail.com" },
                        new List<string> { "Carol", "carol@mail.com" },
                        new List<string> { "Dana", "dana-a@mail.com", "dana-b@mail.com", "dana-c@mail.com" }
                    }),
                new(
                    "單一帳戶與字典序排序",
                    new List<IList<string>>
                    {
                        new List<string> { "Eve", "zeta@mail.com", "alpha@mail.com", "middle@mail.com" }
                    },
                    new List<IList<string>>
                    {
                        new List<string> { "Eve", "alpha@mail.com", "middle@mail.com", "zeta@mail.com" }
                    })
            };

            var output = new System.Text.StringBuilder();
            int passedChecks = 0;
            int totalChecks = samples.Count * 2;

            for (int i = 0; i < samples.Count; i++)
            {
                SampleCase sample = samples[i];
                output.AppendLine($"案例 {i + 1}：{sample.Name}");
                output.AppendLine($"輸入：{FormatAccounts(sample.Accounts)}");
                output.AppendLine($"預期：{FormatAccounts(sample.Expected)}");

                if (RunSolution("解法一（DFS）", AccountsMerge, sample, output))
                {
                    passedChecks++;
                }

                if (RunSolution("解法二（Union-Find）", AccountsMerge2, sample, output))
                {
                    passedChecks++;
                }

                output.AppendLine();
            }

            output.AppendLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
            return output.ToString();
        }

        /// <summary>
        /// 使用獨立的深層副本執行指定解法，比對預期結果並檢查輸入內容是否保持不變。
        /// </summary>
        /// <param name="solutionName">顯示在主控台上的解法名稱。</param>
        /// <param name="solution">接受帳戶清單並回傳合併結果的解法。</param>
        /// <param name="sample">包含輸入、預期結果與案例名稱的測試資料。</param>
        /// <param name="output">累積完整測試報告的字串建構器。</param>
        /// <returns>結果正確且輸入未被修改時回傳 <see langword="true"/>。</returns>
        private static bool RunSolution(
            string solutionName,
            Func<IList<IList<string>>, IList<IList<string>>> solution,
            SampleCase sample,
            System.Text.StringBuilder output)
        {
            IList<IList<string>> input = CloneAccounts(sample.Accounts);
            IList<IList<string>> original = CloneAccounts(input);
            IList<IList<string>> actual = solution(input);
            bool resultMatches = NormalizeResult(actual).SequenceEqual(NormalizeResult(sample.Expected));
            bool inputPreserved = AccountsEqual(input, original);
            bool passed = resultMatches && inputPreserved;

            output.AppendLine($"{solutionName}：");
            output.AppendLine($"  實際：{FormatAccounts(actual)}");
            output.AppendLine($"  輸入未修改：{inputPreserved}");
            output.AppendLine($"  驗證：{(passed ? "PASS" : "FAIL")}");

            return passed;
        }

        /// <summary>
        /// 深層複製帳戶與其內部字串清單，讓不同解法取得互不影響的輸入。
        /// </summary>
        /// <param name="accounts">符合題目格式的帳戶清單。</param>
        /// <returns>具有相同內容但不同內部清單實例的副本。</returns>
        private static IList<IList<string>> CloneAccounts(IList<IList<string>> accounts)
        {
            return accounts
                .Select(account => (IList<string>)new List<string>(account))
                .ToList();
        }

        /// <summary>
        /// 逐列、逐欄比較兩份帳戶資料，確認原始順序與內容完全相同。
        /// </summary>
        /// <param name="first">第一份帳戶清單。</param>
        /// <param name="second">第二份帳戶清單。</param>
        /// <returns>兩份巢狀清單內容與順序皆相同時回傳 <see langword="true"/>。</returns>
        private static bool AccountsEqual(IList<IList<string>> first, IList<IList<string>> second)
        {
            return first.Count == second.Count
                && first.Zip(second).All(pair => pair.First.SequenceEqual(pair.Second));
        }

        /// <summary>
        /// 將每個帳戶保留原有欄位順序轉成可比較文字，再只排序外層帳戶順序。
        /// 此方式容許題目所允許的任意群組順序，同時仍會驗證每組 email 的排序。
        /// </summary>
        /// <param name="accounts">待正規化的合併結果。</param>
        /// <returns>依 Ordinal 排序的帳戶文字清單。</returns>
        private static IList<string> NormalizeResult(IList<IList<string>> accounts)
        {
            return accounts
                .Select(account => string.Join('\u001F', account))
                .OrderBy(account => account, StringComparer.Ordinal)
                .ToList();
        }

        /// <summary>
        /// 將帳戶巢狀清單格式化為單行文字，供案例輸入、預期與實際結果顯示。
        /// </summary>
        /// <param name="accounts">要顯示的帳戶清單。</param>
        /// <returns>格式為 <c>[[name,email], [name,email]]</c> 的字串。</returns>
        private static string FormatAccounts(IList<IList<string>> accounts)
        {
            return $"[{string.Join(", ", accounts.Select(account => $"[{string.Join(",", account)}]"))}]";
        }

        /// <summary>
        /// 保存一組可重複執行的帳戶合併案例。
        /// </summary>
        /// <param name="Name">案例名稱。</param>
        /// <param name="Accounts">符合題目限制的輸入帳戶。</param>
        /// <param name="Expected">email 已依 Ordinal 排序的預期合併結果。</param>
        private sealed record SampleCase(
            string Name,
            IList<IList<string>> Accounts,
            IList<IList<string>> Expected);

        /// <summary>
        /// 使用深度優先搜尋合併帳戶。方法先建立 email 到帳戶索引的反向映射，
        /// 再把共享 email 的帳戶視為相鄰節點，逐一走訪每個連通分量並收集唯一 email。
        /// 輸入必須符合題目限制：每個帳戶的第一欄是姓名，其後至少有一個有效 email；
        /// 同一人的所有帳戶具有相同姓名。本方法只讀取輸入，不會修改任何內部清單。
        /// </summary>
        /// <param name="accounts">待合併的非空帳戶清單。</param>
        /// <returns>每組以姓名開頭、其後 email 依 Ordinal 字典序排列的合併結果。</returns>
        public static IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            var emailToIndexes = new Dictionary<string, List<int>>(StringComparer.Ordinal);

            // 反向映射讓 DFS 能由一個 email 找出所有共享該 email 的帳戶。
            for (int i = 0; i < accounts.Count; i++)
            {
                for (int k = 1; k < accounts[i].Count; k++)
                {
                    string email = accounts[i][k];
                    if (!emailToIndexes.TryGetValue(email, out List<int>? indexes))
                    {
                        indexes = new List<int>();
                        emailToIndexes[email] = indexes;
                    }

                    indexes.Add(i);
                }
            }

            var mergedAccounts = new List<IList<string>>();
            var visited = new bool[accounts.Count];
            var componentEmails = new HashSet<string>(StringComparer.Ordinal);

            for (int i = 0; i < accounts.Count; i++)
            {
                if (visited[i])
                {
                    continue;
                }

                componentEmails.Clear();
                Dfs(i, accounts, emailToIndexes, visited, componentEmails);

                var mergedAccount = new List<string>(componentEmails);
                mergedAccount.Sort(StringComparer.Ordinal);
                mergedAccount.Insert(0, accounts[i][0]);

                mergedAccounts.Add(mergedAccount);
            }

            return mergedAccounts;
        }

        /// <summary>
        /// 從指定帳戶進行深度優先搜尋，透過共享 email 走訪同一連通分量的帳戶，
        /// 並將分量內不重複的 email 累積到集合中。
        /// </summary>
        /// <param name="accountIndex">目前走訪的帳戶索引。</param>
        /// <param name="accounts">只讀取的原始帳戶清單。</param>
        /// <param name="emailToIndexes">email 到所有所屬帳戶索引的反向映射。</param>
        /// <param name="visited">記錄帳戶是否已納入某個連通分量。</param>
        /// <param name="componentEmails">目前連通分量內已收集的唯一 email。</param>
        private static void Dfs(
            int accountIndex,
            IList<IList<string>> accounts,
            Dictionary<string, List<int>> emailToIndexes,
            bool[] visited,
            HashSet<string> componentEmails)
        {
            visited[accountIndex] = true;

            for (int k = 1; k < accounts[accountIndex].Count; k++)
            {
                string email = accounts[accountIndex][k];

                // 每個 email 的鄰接帳戶只需展開一次，避免在環狀連結中重複走訪。
                if (!componentEmails.Add(email))
                {
                    continue;
                }

                foreach (int nextAccountIndex in emailToIndexes[email])
                {
                    if (!visited[nextAccountIndex])
                    {
                        Dfs(
                            nextAccountIndex,
                            accounts,
                            emailToIndexes,
                            visited,
                            componentEmails);
                    }
                }
            }
        }

        /// <summary>
        /// 使用帳戶索引並查集合併帳戶。掃描 email 時，若該 email 已由其他帳戶登記，
        /// 便合併兩個帳戶的集合；完成後依根節點彙整唯一 email。
        /// 輸入必須符合題目限制，且同一人的帳戶姓名一致。本方法只讀取輸入資料。
        /// </summary>
        /// <param name="accounts">待合併的非空帳戶清單。</param>
        /// <returns>每組以姓名開頭、其後 email 依 Ordinal 字典序排列的合併結果。</returns>
        public static IList<IList<string>> AccountsMerge2(IList<IList<string>> accounts)
        {
            int[] parent = Enumerable.Range(0, accounts.Count).ToArray();
            int[] rank = new int[accounts.Count];
            var emailOwner = new Dictionary<string, int>(StringComparer.Ordinal);

            for (int accountIndex = 0; accountIndex < accounts.Count; accountIndex++)
            {
                for (int emailIndex = 1; emailIndex < accounts[accountIndex].Count; emailIndex++)
                {
                    string email = accounts[accountIndex][emailIndex];
                    if (emailOwner.TryGetValue(email, out int ownerIndex))
                    {
                        // 共享 email 足以證明兩個帳戶屬於同一人。
                        Union(parent, rank, accountIndex, ownerIndex);
                    }
                    else
                    {
                        emailOwner[email] = accountIndex;
                    }
                }
            }

            var rootToEmails = new Dictionary<int, List<string>>();

            // 每個唯一 email 只加入其最終根節點一次，自然完成去重。
            foreach ((string email, int ownerIndex) in emailOwner)
            {
                int root = Find(parent, ownerIndex);
                if (!rootToEmails.TryGetValue(root, out List<string>? emails))
                {
                    emails = new List<string>();
                    rootToEmails[root] = emails;
                }

                emails.Add(email);
            }

            var mergedAccounts = new List<IList<string>>();
            foreach ((int root, List<string> emails) in rootToEmails)
            {
                emails.Sort(StringComparer.Ordinal);
                var mergedAccount = new List<string> { accounts[root][0] };
                mergedAccount.AddRange(emails);
                mergedAccounts.Add(mergedAccount);
            }

            return mergedAccounts;
        }

        /// <summary>
        /// 尋找帳戶所在集合的根節點，並在遞迴回程時執行路徑壓縮，
        /// 讓後續查詢可直接接近根節點。
        /// </summary>
        /// <param name="parent">每個帳戶索引目前指向的父節點。</param>
        /// <param name="accountIndex">要查詢的帳戶索引。</param>
        /// <returns>該帳戶所屬集合的根節點索引。</returns>
        private static int Find(int[] parent, int accountIndex)
        {
            if (parent[accountIndex] != accountIndex)
            {
                parent[accountIndex] = Find(parent, parent[accountIndex]);
            }

            return parent[accountIndex];
        }

        /// <summary>
        /// 依 rank 合併兩個帳戶集合；較淺的樹接到較深的樹，
        /// rank 相同時選擇第一個根節點並增加其 rank。
        /// </summary>
        /// <param name="parent">每個帳戶索引目前指向的父節點。</param>
        /// <param name="rank">各根節點的樹高上界。</param>
        /// <param name="firstAccountIndex">第一個帳戶索引。</param>
        /// <param name="secondAccountIndex">第二個帳戶索引。</param>
        private static void Union(
            int[] parent,
            int[] rank,
            int firstAccountIndex,
            int secondAccountIndex)
        {
            int firstRoot = Find(parent, firstAccountIndex);
            int secondRoot = Find(parent, secondAccountIndex);

            if (firstRoot == secondRoot)
            {
                return;
            }

            if (rank[firstRoot] < rank[secondRoot])
            {
                parent[firstRoot] = secondRoot;
            }
            else if (rank[firstRoot] > rank[secondRoot])
            {
                parent[secondRoot] = firstRoot;
            }
            else
            {
                parent[secondRoot] = firstRoot;
                rank[firstRoot]++;
            }
        }
    }
}