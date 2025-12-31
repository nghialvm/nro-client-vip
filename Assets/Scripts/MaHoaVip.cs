using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class MaHoaVip
    {
        // Tạo một private static instance để lưu trữ thể hiện duy nhất của lớp.
        private static MaHoaVip instance;

        // Tạo private constructor để ngăn việc tạo thể hiện từ bên ngoài.
        private MaHoaVip()
        {
        }

        // Tạo một public static phương thức để truy xuất hoặc tạo thể hiện duy nhất.
        public static MaHoaVip gI()
        {
            if (instance == null)
            {
                instance = new MaHoaVip();
            }
            return instance;
        }
        public const long MOD = 8941489;

        public const long e = 127;

        public const long PRK = 5134843;
        long sqr(long x)
        {
            return x * x;
        }
        public long powMod(long a, long b)
        {
            if (b == 0) return 1 % MOD;
            else
         if (b % 2 == 0)
                return sqr(powMod(a, b / 2)) % MOD;
            else
                return a * (sqr(powMod(a, b / 2)) % MOD) % MOD;
        }
        public string longToBin(long number)
        {
            string binary = "";
            long mask = 1L << 63; // Bắt đầu với bit cao nhất (bit thứ 63)

            for (int i = 0; i < 64; i++) // Long có 64 bit
            {
                binary += ((number & mask) == 0) ? "0" : "1";
                number <<= 1; // Dịch số sang trái 1 bit
            }

            return binary;
        }
        public long binToLong(string binary)
        {
            long result = 0;
            int index = binary.Length - 1; // Bắt đầu từ bit thấp nhất (bên phải)

            while (index >= 0)
            {
                if (binary[index] == '1')
                {
                    result |= (1L << (binary.Length - 1 - index));
                }

                index--;
            }

            return result;
        }
        public List<string> encode(string s)
        {
            List<string> ans = new List<string>();
            foreach (char x in s)
            {
                long z = (byte)x;
                z = powMod(z, e);
                string k = longToBin(z);
                ans.Add(k);
            }
            return ans;
        }

        public string decode(string input)
        {
            string[] arr = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> x = new List<string>(arr);
            string ans = "";
            foreach (string z in x)
            {
                long k = binToLong(z);
                long t = powMod(k, PRK);
                ans += (char)t;
            }
            return ans;
        }
    }
}
