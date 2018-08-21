using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace EatBall
{
    class Player
    {     
        public Dictionary<char, string[]> number = new Dictionary<char,string[]>();
        public List<int> SortChampion = new List<int>();

        public int Bal { get; set; } = 0;

        public Player()
        {
            AddNumberString();
        }


        public void SortTable()
        {
            try
            {
                string path = @"D:\Table.txt";

                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                   sw.WriteLine(Bal);
                }

                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        SortChampion.Add(Int32.Parse(line));
                    }
                }

                
                SortChampion.Sort();
                SortChampion.Reverse();

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    for (int i = 0; i < SortChampion.Count; i++)
                        if(SortChampion[i]>=0)
                         sw.WriteLine(SortChampion[i]);

                }
                SortChampion.Clear();
                Bal = 0;

            }
            catch(Exception ex)
            {
                Console.WriteLine($"File:{ex.Message}");
            }
        }

        public void PrintTable()
        {
            try
            {
                string path = @"D:\Table.txt";

                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        SortChampion.Add(Int32.Parse(line));
                    }
                }

                int step = 10;
                for (int i = 0; i < SortChampion.Count; i++)
                {
                    SetPosBalu(150, 20 + step, SortChampion[i]);
                    step += 10;

                    if (i == 6)
                        break;
                }

                SortChampion.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File:{ex.Message}");
            }

        }

        public void SetPosBalu(int xc, int yc, int num)
        {
            
            string[] str;
            string qwert = Convert.ToString(num);
            string cleen = " ";
            char[] ch;
            ch = qwert.ToCharArray();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;

            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                Console.Write(cleen.PadLeft((int)WindowSize.Width));
            }

            for (int j = 0; j < ch.Count(); j++)
            {

                number.TryGetValue(ch[j], out str);
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(xc, i + yc);
                    Console.Write(str[i]);
                }
                xc += 10;
                str = null;
            }




        }

        public void AddNumberString()
        {
      
            number.Add('0', number0);
            number.Add('1', number1);
            number.Add('2', number2);
            number.Add('3', number3);
            number.Add('4', number4);
            number.Add('5', number5);
            number.Add('6', number6);
            number.Add('7', number7);
            number.Add('8', number8);
            number.Add('9', number9);
            number.Add('-', minus);

        }

        private string[] number1 =  { "     *" ,
                                      "   * *" ,
                                      " *   *" ,
                                      "     *" ,
                                      "     *" };

        private string[] number2 =  { "  *** " ,
                                      " *   *" ,
                                      "    * " ,
                                      "  *   " ,
                                      " *****" };

        private string[] number3 =  { "******" ,
                                      "     *" ,
                                      " *****" ,
                                      "     *" ,
                                      "******" };

        private string[] number4 =  { "*    *" ,
                                      "*    *" ,
                                      "******" ,
                                      "     *" ,
                                      "     *" };

        private string[] number5 =  { "******" ,
                                      "*    " ,
                                      "******" ,
                                      "     *" ,
                                      "******" };

        private string[] number6 =  { "******" ,
                                      "*     " ,
                                      "******" ,
                                      "*    *" ,
                                      "******" };

        private string[] number7 =  { "******" ,
                                      "    * " ,
                                      "  *** " ,
                                      "   *  " ,
                                      "   *  " };

        private string[] number8 =  { "******" ,
                                      "*    *" ,
                                      "******" ,
                                      "*    *" ,
                                      "******" };

        private string[] number9 =  { "******" ,
                                      "*    *" ,
                                      "******" ,
                                      "     *" ,
                                      "******" };

        private string[] number0 =  { "******" ,
                                      "*    *" ,
                                      "*    *" ,
                                      "*    *" ,
                                      "******" };

        private string[] minus =  { "      " ,
                                    "      " ,
                                    "******" ,
                                    "      " ,
                                    "      " };
    }
}
