namespace leetcode_2353;

class Program
{
    /// <summary>
    /// 2353. Design a Food Rating System
    /// https://leetcode.com/problems/design-a-food-rating-system/description/?envType=daily-question&envId=2025-09-17
    /// 2353. 设计食物评分系统
    /// https://leetcode.cn/problems/design-a-food-rating-system/description/
    /// 
    /// 題目中文翻譯：
    /// 設計一個食物評分系統，需支援下列操作：
    /// 1. 更改系統中某項食物的評分（changeRating）
    /// 2. 回傳指定料理類型中評分最高的食物（highestRated）。若有評分相同，回傳字典序較小的名稱。
    /// 
    /// 介面規格：
    /// - FoodRatings(string[] foods, string[] cuisines, int[] ratings)：用 foods, cuisines, ratings 初始化系統，三者長度皆為 n。
    ///   foods[i] 為第 i 個食物名稱；cuisines[i] 為其料理類型；ratings[i] 為其初始評分。
    /// - void changeRating(string food, int newRating)：更改名為 food 的食物評分為 newRating。
    /// - string highestRated(string cuisine)：回傳 cuisine 的最高評分食物名稱；若有並列，回傳字典序較小者。
    /// 
    /// 字典序說明：字串 x 在字典序上小於 y 當且僅當 x 為 y 的前綴，或在第一個不同的位置 x[i] 的字元小於 y[i] 的字元。
    /// </summary>
    /// <param name="args"></param> <summary>
    static void Main(string[] args)
    {
        // 範例測試：根據 LeetCode 題目的行為示範 FoodRatings
        string[] foods = new string[] { "kimchi", "miso", "sushi", "ramen", "bulgogi" };
        string[] cuisines = new string[] { "korean", "japanese", "japanese", "japanese", "korean" };
        int[] ratings = new int[] { 9, 12, 8, 15, 7 };

        var fr = new FoodRatings(foods, cuisines, ratings);

        Console.WriteLine(fr.highestRated("korean"));   // expect "kimchi"
        Console.WriteLine(fr.highestRated("japanese")); // expect "ramen"

        fr.changeRating("sushi", 16);
        Console.WriteLine(fr.highestRated("japanese")); // expect "sushi"

        // === 解法二（官方：PriorityQueue + lazy deletion）測試 ===
        var fr2 = new FoodRatings2(foods, cuisines, ratings);
        Console.WriteLine("-- 解法二 開始 --");
        Console.WriteLine(fr2.HighestRated("korean"));   // expect "kimchi"
        Console.WriteLine(fr2.HighestRated("japanese")); // expect "ramen"

        fr2.ChangeRating("sushi", 16);
        Console.WriteLine(fr2.HighestRated("japanese")); // expect "sushi"
        Console.WriteLine("-- 解法二 結束 --");
    }

    /// <summary>
    /// 解法一：基於 SortedSet 的食物評分系統
    /// 
    /// 解題思路：
    /// 1. 使用三個 Dictionary 分別存儲食物到料理類型、食物到評分的映射關係
    /// 2. 對每個料理類型維護一個 SortedSet，確保食物按評分降序和名稱升序排列
    /// 3. 透過自訂 FoodEntryComparer 實現排序邏輯：評分高的優先，同分時字典序小的優先
    /// 4. 查詢時直接取 SortedSet 的最小元素（因比較器邏輯使最高評分排在前面）
    /// 5. 更新時先移除舊的 FoodEntry，再插入新的 FoodEntry
    /// 
    /// 時間複雜度：
    /// - 初始化：O(n log n)
    /// - changeRating：O(log n) - 一次移除 + 一次插入
    /// - highestRated：O(1) - 直接存取 SortedSet 首元素
    /// 
    /// 空間複雜度：O(n) - 每個食物只儲存一次
    /// 
    /// 優點：查詢效能恆定 O(1)，記憶體使用穩定
    /// 缺點：更新需要兩次樹操作，實作複雜度較高
    /// </summary>
    class FoodRatings
    {
        /// <summary>
        /// 食物名稱到料理類型的映射，用於快速查找食物屬於哪種料理
        /// </summary>
        private readonly Dictionary<string, string> foodToCuisine = new Dictionary<string, string>();
        
        /// <summary>
        /// 食物名稱到當前評分的映射，用於在更新評分時獲取舊評分
        /// </summary>
        private readonly Dictionary<string, int> foodToRating = new Dictionary<string, int>();
        
        /// <summary>
        /// 料理類型到有序食物集合的映射
        /// SortedSet 根據自訂比較器維護食物的排序：評分高的優先，同分時字典序小的優先
        /// </summary>
        private readonly Dictionary<string, SortedSet<FoodEntry>> cuisineSets = new Dictionary<string, SortedSet<FoodEntry>>();

        /// <summary>
        /// 食物條目的內部表示，包含食物名稱和評分
        /// 設計為不可變物件以確保在 SortedSet 中的穩定性
        /// </summary>
        private class FoodEntry
        {
            /// <summary>
            /// 食物名稱，用於標識和排序
            /// </summary>
            public string Name { get; }
            
            /// <summary>
            /// 食物評分，用於排序比較
            /// </summary>
            public int Rating { get; }

            /// <summary>
            /// 建構函式：建立一個不可變的食物條目
            /// </summary>
            /// <param name="name">食物名稱</param>
            /// <param name="rating">食物評分</param>
            public FoodEntry(string name, int rating)
            {
                Name = name;
                Rating = rating;
            }
        }

        /// <summary>
        /// 自訂比較器，實現食物條目的排序邏輯
        /// 排序規則：
        /// 1. 首先按評分降序排列（評分高的排在前面）
        /// 2. 評分相同時按食物名稱升序排列（字典序小的排在前面）
        /// </summary>
        private class FoodEntryComparer : IComparer<FoodEntry>
        {
            /// <summary>
            /// 比較兩個 FoodEntry 物件的方法
            /// </summary>
            /// <param name="x">第一個食物條目</param>
            /// <param name="y">第二個食物條目</param>
            /// <returns>
            /// 負數：x 小於 y（x 排在 y 前面）
            /// 零：x 等於 y
            /// 正數：x 大於 y（x 排在 y 後面）
            /// </returns>
            public int Compare(FoodEntry? x, FoodEntry? y)
            {
                // 處理 null 值的邊界情況
                if (x is null && y is null) return 0;
                if (x is null) return -1;
                if (y is null) return 1;

                // 首要排序條件：按評分降序（y.Rating 與 x.Rating 比較，評分高的排前面）
                int cmp = y.Rating.CompareTo(x.Rating);
                if (cmp != 0) return cmp;
                
                // 次要排序條件：評分相同時按食物名稱升序（字典序小的排前面）
                return string.CompareOrdinal(x.Name, y.Name);
            }
        }

        /// <summary>
        /// 建構函式：初始化食物評分系統
        /// </summary>
        /// <param name="foods">食物名稱陣列</param>
        /// <param name="cuisines">對應的料理類型陣列</param>
        /// <param name="ratings">對應的初始評分陣列</param>
        public FoodRatings(string[] foods, string[] cuisines, int[] ratings)
        {
            // 建立自訂比較器實例，用於所有 SortedSet
            var comparer = new FoodEntryComparer();

            // 逐一處理每個食物項目
            for (int i = 0; i < foods.Length; i++)
            {
                string food = foods[i];
                string cuisine = cuisines[i];
                int rating = ratings[i];

                // 建立食物到料理類型和評分的映射
                foodToCuisine[food] = cuisine;
                foodToRating[food] = rating;

                // 為每個料理類型建立或取得對應的 SortedSet
                if (!cuisineSets.TryGetValue(cuisine, out var set))
                {
                    set = new SortedSet<FoodEntry>(comparer);
                    cuisineSets[cuisine] = set;
                }

                // 將食物加入對應料理類型的有序集合
                set.Add(new FoodEntry(food, rating));
            }
        }

        /// <summary>
        /// 更改指定食物的評分
        /// 操作步驟：
        /// 1. 查找食物的料理類型和舊評分
        /// 2. 從 SortedSet 中移除舊的 FoodEntry
        /// 3. 更新評分映射
        /// 4. 將新的 FoodEntry 加入 SortedSet
        /// </summary>
        /// <param name="food">要更改評分的食物名稱</param>
        /// <param name="newRating">新的評分值</param>
        public void changeRating(string food, int newRating)
        {
            // 檢查食物是否存在，不存在則直接返回
            if (!foodToCuisine.TryGetValue(food, out var cuisine)) return;
            if (!foodToRating.TryGetValue(food, out var oldRating)) return;

            var set = cuisineSets[cuisine];
            
            // 關鍵步驟：移除舊項目
            // 注意：SortedSet 依據比較器判斷元素相等性，需要建立具有相同評分的 FoodEntry
            set.Remove(new FoodEntry(food, oldRating));

            // 更新食物的評分記錄
            foodToRating[food] = newRating;
            
            // 將具有新評分的食物重新加入集合（會自動排序到正確位置）
            set.Add(new FoodEntry(food, newRating));
        }

        /// <summary>
        /// 查詢指定料理類型中評分最高的食物
        /// 由於使用自訂比較器，評分最高且字典序最小的食物會排在 SortedSet 的最前面
        /// </summary>
        /// <param name="cuisine">料理類型</param>
        /// <returns>評分最高的食物名稱，若料理類型不存在或無食物則返回空字串</returns>
        public string highestRated(string cuisine)
        {
            // 檢查料理類型是否存在且集合非空
            if (!cuisineSets.TryGetValue(cuisine, out var set) || set.Count == 0)
                return string.Empty;

            // 由於自訂比較器的邏輯，SortedSet.Min 實際上是評分最高的食物
            // 這是因為比較器將高評分的項目排在前面
            var first = set.Min;
            if (first is null) return string.Empty;
            return first.Name;
        }
    }

    /// <summary>
    /// 解法二：基於 PriorityQueue + Lazy Deletion 的食物評分系統
    /// 
    /// 解題思路：
    /// 1. 使用 foodMap 記錄每個食物的當前評分和料理類型
    /// 2. 對每個料理類型維護一個優先佇列，按評分降序和名稱升序排列
    /// 3. 採用「延遲刪除」策略：更新評分時不移除舊項目，只加入新項目
    /// 4. 查詢時檢查佇列頂端項目是否為當前有效評分，無效則移除並繼續
    /// 5. 透過查詢操作逐步清理過期的佇列項目
    /// 
    /// 時間複雜度：
    /// - 初始化：O(n log n)
    /// - ChangeRating：O(log n) - 只需一次插入操作
    /// - HighestRated：O(1) 平均，O(k log n) 最壞情況（k 為過期項目數量）
    /// 
    /// 空間複雜度：O(n + m) - n 為食物數量，m 為歷史更新次數
    /// 
    /// 優點：更新操作簡潔高效，實作直觀
    /// 缺點：可能累積過期項目導致記憶體開銷增加
    /// 
    /// ref:https://leetcode.cn/problems/design-a-food-rating-system/solutions/3078910/she-ji-shi-wu-ping-fen-xi-tong-by-leetco-vk42/
    /// 
    /// </summary>
    public class FoodRatings2
    {
        /// <summary>
        /// 食物資訊映射：存儲每個食物的當前評分和所屬料理類型
        /// Key: 食物名稱
        /// Value: (當前評分, 料理類型)
        /// </summary>
        private Dictionary<string, (int Rating, string Cuisine)> foodMap;
        
        /// <summary>
        /// 料理類型到優先佇列的映射
        /// Key: 料理類型
        /// Value: 該料理類型下所有食物的優先佇列（按評分和名稱排序）
        /// 注意：佇列可能包含過期的評分項目，需要在查詢時進行延遲清理
        /// 
        /// 前半部 (TElement) = (string Food, int Rating)：這是佇列中實際儲存（payload）的元素，
        /// 也就是你從 Peek() / Dequeue() 得到的物件（包含食物名稱與當時的評分）。
        /// 
        /// 後半部 (TPriority) = (int Rating, string Food)：這是用來排序的「優先權」值。
        /// PriorityQueue 根據優先權（以及你提供的 comparer）來決定元素的先後順序，
        /// element 本身並不參與排序（除非你把 element 的資料也當作 priority 傳入）
        /// </summary>
        private Dictionary<string, PriorityQueue<(string Food, int Rating), (int Rating, string Food)>> ratingMap;

        /// <summary>
        /// 建構函式：初始化基於優先佇列的食物評分系統
        /// </summary>
        /// <param name="foods">食物名稱陣列</param>
        /// <param name="cuisines">對應的料理類型陣列</param>
        /// <param name="ratings">對應的初始評分陣列</param>
        public FoodRatings2(string[] foods, string[] cuisines, int[] ratings)
        {
            // 初始化兩個主要的資料結構
            foodMap = new Dictionary<string, (int Rating, string Cuisine)>();
            ratingMap = new Dictionary<string, PriorityQueue<(string Food, int Rating), (int Rating, string Food)>>();

            // 處理每個輸入的食物項目
            for (int i = 0; i < foods.Length; i++)
            {
                string food = foods[i];
                string cuisine = cuisines[i];
                int rating = ratings[i];
                
                // 記錄食物的當前評分和料理類型
                foodMap[food] = (rating, cuisine);

                // 為新的料理類型建立優先佇列
                if (!ratingMap.ContainsKey(cuisine))
                {
                    // 建立優先佇列，使用自訂比較器
                    // 優先佇列的元素類型：(string Food, int Rating)
                    // 優先權類型：(int Rating, string Food)
                    ratingMap[cuisine] = new PriorityQueue<(string Food, int Rating), (int Rating, string Food)>(
                        Comparer<(int Rating, string Food)>.Create((a, b) =>
                        {
                            // 首要條件：評分高的優先（降序）
                            if (a.Rating != b.Rating)
                            {
                                return b.Rating.CompareTo(a.Rating);
                            }
                            // 次要條件：評分相同時，字典序小的優先（升序）
                            return a.Food.CompareTo(b.Food);
                        })
                    );
                }

                // 將食物項目加入對應料理類型的優先佇列
                ratingMap[cuisine].Enqueue((food, rating), (rating, food));
            }
        }

        /// <summary>
        /// 更改指定食物的評分（使用延遲刪除策略）
        /// 
        /// 延遲刪除的優點：
        /// 1. 避免了從優先佇列中刪除特定元素的複雜操作
        /// 2. 更新操作簡單且高效
        /// 3. 過期項目會在後續查詢時被自動清理
        /// </summary>
        /// <param name="food">要更改評分的食物名稱</param>
        /// <param name="newRating">新的評分值</param>
        public void ChangeRating(string food, int newRating)
        {
            // 取得食物的舊評分和料理類型
            var (oldRating, cuisine) = foodMap[food];
            
            // 關鍵策略：不移除舊項目，直接加入新的評分項目
            // 舊項目成為「過期項目」，會在查詢時被清理
            ratingMap[cuisine].Enqueue((food, newRating), (newRating, food));
            
            // 更新食物的當前評分記錄
            foodMap[food] = (newRating, cuisine);
        }

        /// <summary>
        /// 查詢指定料理類型中評分最高的食物（實現延遲清理）
        /// 
        /// 延遲清理機制：
        /// 1. 檢查佇列頂端的項目是否為有效的當前評分
        /// 2. 如果是過期項目（評分與 foodMap 中記錄的不符），則移除並繼續
        /// 3. 重複此過程直到找到有效項目或佇列為空
        /// 
        /// 這種設計在平均情況下能提供 O(1) 的查詢效能
        /// </summary>
        /// <param name="cuisine">料理類型</param>
        /// <returns>評分最高的食物名稱，若料理類型不存在或無食物則返回空字串</returns>
        public string HighestRated(string cuisine)
        {
            // 取出對應料理類型的優先佇列,
            // 也就是 value (宣告時候是 PriorityQueue<(string Food, int Rating), (int Rating, string Food)>)
            var q = ratingMap[cuisine];
            
            // 持續檢查佇列頂端，直到找到有效項目
            while (q.Count > 0)
            {
                var top = q.Peek();
                string food = top.Food;
                int rating = top.Rating;
                
                // 檢查這個項目是否為當前有效的評分
                if (foodMap[food].Rating == rating)
                {
                    // 找到有效項目，返回食物名稱
                    return food;
                }
                
                // 這是過期項目，移除並繼續搜尋
                // 這就是「延遲刪除」的清理過程
                q.Dequeue();
            }

            // 佇列為空，返回空字串
            return string.Empty;
        }
    }
}
