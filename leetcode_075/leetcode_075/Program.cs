namespace leetcode_075
{
    internal class Program
    {
        /// <summary>
        /// 75. Sort Colors
        /// https://leetcode.com/problems/sort-colors/?envType=daily-question&envId=2024-06-12
        /// 75. 颜色分类
        /// https://leetcode.cn/problems/sort-colors/description/
        /// 
        /// 排序一個只包含 0、1 和 2 的整數數組。這是一個經典的問題，稱為荷蘭國旗問題。
        /// 目標是就地排序數組，使所有的 0 排在最前面，接著是所有的 1，最後是所有的 2。
        /// 
        /// 題目給定一個只包含 0（紅色）、1（白色）、2（藍色）三種數字的陣列 nums，要求 就地（in-place） 進行排序，使得 0 在最前，1 在中間，2 在最後。
        /// 
        /// 不能使用API呼叫排序
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input1 = { 2, 0, 2, 1, 1, 0 };
            int[] input2 = { 2, 0, 2, 1, 1, 0 };
            int[] input3 = { 2, 0, 2, 1, 1, 0 };
            int[] input4 = { 2, 0, 2, 1, 1, 0 };
            int[] input5 = { 2, 0, 2, 1, 1, 0 };
            SortColors(input1);
            SortColors2(input2);
            CountingSortAlgorithm(input3);
            SortColors3(input4);
            SortColors4(input5);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        public static void SortColors(int[] nums)
        {
            BubbleSortAlgorithm(nums);
        }


        /// <summary>
        /// bubble sort
        /// </summary>
        /// <param name="arr"></param>
        public static void BubbleSortAlgorithm(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // swap arr[j] and arr[j+1]
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }

            Console.Write("SortColors: ");
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }


        /// <summary>
        /// https://leetcode.cn/problems/sort-colors/solutions/1522053/by-stormsunshine-bhqo/
        /// 计数排序
        /// </summary>
        /// <param name="nums"></param>
        public static void SortColors2(int[] nums)
        {
            // 先計數, 每個element 數量
            int[] counts = new int[3];
            foreach (int num in nums)
            {
                counts[num]++;
            }

            // 依據結果, 再去排序
            int n = nums.Length;
            for (int i = 0, j = 0; i < n; i++)
            {
                while (counts[j] == 0)
                {
                    j++;
                }

                nums[i] = j;
                counts[j]--;
            }

            Console.Write("SortColors2: ");
            foreach (int num in nums)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

        }


        /// <summary>
        /// 計數排序
        /// </summary>
        /// <param name="arr"></param>
        public static void CountingSortAlgorithm(int[] arr)
        {
            int n = arr.Length;
            int[] output = new int[n];

            // Find the maximum element of the array
            int max = arr[0];
            for (int i = 1; i < n; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }

            // Create a count array to store count of individual elements
            int[] count = new int[max + 1];

            // Initialize count array with all zeros
            for (int i = 0; i <= max; ++i)
            {
                count[i] = 0;
            }

            // Store count of each character
            for (int i = 0; i < n; ++i)
            {
                ++count[arr[i]];
            }

            // Change count[i] so that count[i] now contains actual position of this element in output array
            // 修改計數數組
            for (int i = 1; i <= max; ++i)
            {
                count[i] += count[i - 1];
            }

            // Build the output array
            // 構建排序後的數組
            for (int i = n - 1; i >= 0; i--)
            {
                output[count[arr[i]] - 1] = arr[i];
                --count[arr[i]];
            }

            // Copy the output array to arr, so that arr now contains sorted characters
            // 複製回原始數組
            for (int i = 0; i < n; ++i)
            {
                arr[i] = output[i];
            }


            // print
            Console.Write("CountingSortAlgorithm: ");
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }


        /// <summary>
        /// 這段程式碼使用 雙指標（Two Pointers）+ 單次遍歷（One-pass） 的方法來解決問題：
        /// p0：指向下一個 0 的位置的指針（最左側）。
        /// p2：指向下一個 2 的位置的指針（最右側）。
        /// 
        /// 結論:
        /// 該方法使用兩個指針，p0 和 p2，分別跟蹤 0 和 2 的位置。
        /// 它遍歷數組，通過交換元素來確保所有的 0 都在開頭，所有的 2 都在末尾，而 1 自然落在中間。
        /// 
        /// 時間複雜度為 O(n)，空間複雜度為 O(1)。
        /// </summary>
        /// <param name="nums"></param>
        public static void SortColors3(int[] nums)
        {
            int n = nums.Length;
            // p0 指向應該放 0 的位置
            int p0 = 0;
            // p2 指向應該放 2 的位置
            int p2 = n - 1;

            // 遍歷數組
            for (int i = 0; i < n; i++)
            {
                // 處理 2： 若 nums[i] == 2，則將 nums[i] 和 nums[p2] 交換，並減小 p2，
                // 但 i 不變（因為交換後的新 nums[i] 仍需檢查）。
                // 這確保所有的 2 都被移動到數組的末尾。
                while (i <= p2 && nums[i] == 2)
                {
                    int temp = nums[i];
                    nums[i] = nums[p2];
                    nums[p2] = temp;
                    p2--;
                }

                // 處理 0： 若 nums[i] == 0，則將 nums[i] 和 nums[p0] 交換，
                // 並增大 p0，同時繼續前進 i。
                // 這確保所有的 0 都被移動到數組的開頭。
                if (nums[i] == 0)
                {
                    int temp = nums[i];
                    nums[i] = nums[p0];
                    nums[p0] = temp;
                    p0++;
                }

            }

            Console.Write("SortColors3: ");
            foreach (int num in nums)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }


        /// <summary>
        /// 雙指針 類似 SortColors3 方法
        /// 
        /// 將包含 0、1 和 2 的陣列就地排序。
        /// 
        /// mid 類似於遍歷陣列的指標，low 和 high 是用於交換元素的指標。
        /// 也就是 for 迴圈的 i 變數
        /// </summary>
        /// <param name="nums">要排序的整數陣列。</param>
        public static void SortColors4(int[] nums)
        {
            // 指向應該放 0 的位置
            int low = 0;
            // 指向應該放 2 的位置
            int high = nums.Length - 1;
            // 用於遍歷陣列的指標
            int mid = 0; 
            int temp = 0;

            // 從頭到尾遍歷陣列
            while (mid <= high)
            {
                switch (nums[mid])
                {
                    // 如果元素是 0
                    case 0:
                        {
                            // 將 mid 位置的元素與 low 位置的元素交換
                            temp = nums[low];
                            nums[low] = nums[mid];
                            nums[mid] = temp;
                            // 左邊界往右移動
                            low++;
                            mid++;
                            break;
                        }
                    // 如果元素是 1
                    case 1:
                        {
                            // 移動到下一個元素
                            mid++;
                            break;
                        }
                    // 如果元素是 2
                    case 2:
                        {
                            // 將 mid 位置的元素與 high 位置的元素交換
                            temp = nums[mid];
                            nums[mid] = nums[high];
                            nums[high] = temp;
                            // 右邊界往左移動
                            high--;
                            break;
                        }
                }
            }

            // 輸出排序後的陣列
            Console.Write("SortColors4: ");
            foreach (int num in nums)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();


        }

    }
}
