using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        public static string alphaRus = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public static string alphaEng = "abcdefghijklmnopqrstuvwxyz";
        public static char cipher(char ch, int key, bool isRus)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            bool d = char.IsUpper(ch);
            ch = char.ToLower(ch);
            if (char.IsDigit(ch) || ch == ' ' || ch == '.' || ch == '-')
                return ch;
            char res = isRus ? alphaRus[(alphaRus.IndexOf(ch) + key) % alphaRus.Length] : alphaEng[(alphaEng.IndexOf(ch) + key) % alphaEng.Length];
            res = d ? char.ToUpper(res) : res;
            return res;
        }

        public static string Encipher(string input, int key, bool isRus = true)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += cipher(ch, key, isRus);

            return output;
        }

        public static string Decipher(string input, int key, bool isRus = true)
        {
            return Encipher(input, (isRus ? alphaRus.Length : alphaEng.Length) - key, isRus);
        }


        static void Main(string[] args)
        {
            var userStringAddress = new[]{
                "81-й км МКАД д.47 кв.494",
                "Винтовая ул. д.13 кв.438",
                "Заваруевский пер. д.57 кв.98",
                "Четырёхдомный пер.д.92 кв.87",
                "Первомайская ул.д.28 кв.453",
                "Тушинская пл.д.19 кв.494",
                "Кустанайская ул.д.52 кв.37",
                "пл. Академика Кутафина д.88 кв.444",
                "Криворожский пр.д.69 кв.275",
                "Садовая ул. д. 13 кв. 234"
            };

            var userStringMail = new[]
            {
                "lloyc.cattio@fmaumez.com",
                "kfeenez@koch.com",
                "hartmann.eve@xest.com",
                "eldsidge.siuchie@lebtack.com",
                "nowella.oruia@houmail.com",
                "sotalia42@avfdeshas.com",
                "ukuhlmzn@cole.net",
                "verlie.schmitt@mzyer.com",
                "huranuox@zahoo.com",
                "rodrigo.lehner@crist.com"
            };

            using (var file = File.CreateText("Расшифрованные данные.txt"))
            {
                for (int i = 0; i < userStringAddress.Length; i++)
                {
                    file.WriteLine(userStringMail[i] + "\t\t\t" + userStringAddress[i]);
                }
                Console.WriteLine("Данные были записаны в файл \"Расшифрованные данные.txt\"");
            }

            Console.ReadKey();

        }
    }
}
