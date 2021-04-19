using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace Lab3
{
    class Student
    {
        private string s_name, s_surname;
        private int exam;
        private int[] hw = new int[5];
        private float final_points_avg;
        private float final_points_med;
        private Dictionary<int, (string, string, int[], int, float, float)> studentas = new Dictionary<int, (string, string, int[], int, float, float)>();
        public int[] Hw { get => hw; set => hw = value; }
        public int Exam { get => exam; set => exam = value; }
        public string S_name { get => s_name; set => s_name = value; }
        public string S_surname { get => s_surname; set => s_surname = value; }
        public float Final_points_avg { get => final_points_avg; set => final_points_avg = value; }
        public float Final_points_med { get => final_points_med; set => final_points_med = value; }
        public Dictionary<int, (string, string, int[], int, float, float)> Studentas { get => studentas; set => studentas = value; }

        public Student(string s_name, string s_surname)
        {
            Random random = new Random();
            Exam = random.Next(1, 10);
            for (int i = 0; i < 5; i++)
            {
                Hw.SetValue(random.Next(1, 10), i);
        }
            Studentas.Add(0, (s_name, s_surname, Hw, Exam, 0, 0));
        }
        public int[] generateRandom()
        {
            Random random = new Random();
            Exam = random.Next(1, 10);
            int[] rand = new int[5];
            for (int i = 0; i < 5; i++)
            {
                rand.SetValue(random.Next(1, 10), i);
            }
            return rand;
        }
        public List<string> generateRandomNames(int kiekis)
        {
            Random random = new Random();
            List<string> names = new List<string>();
            int randid=0;
            string first_name;
            string last_name;
            randid=random.Next(1, kiekis*2);
            string tempnumber = randid.ToString();
            string tempfirstname = "Name" + tempnumber;
            randid = random.Next(1, kiekis);
            tempnumber = randid.ToString();
            string templastname = "Surname" + tempnumber;
            first_name = tempfirstname;
            last_name = templastname;
            names.Add(first_name);
            names.Add(last_name);
            return names;
        }
        public float GetMedian(int[] array)
        {
            int[] tempArray = array;
            int count = tempArray.Length;

            Array.Sort(tempArray);

            float medianValue;

            if (count % 2 == 0)
            {
                int middleElement1 = tempArray[(count / 2) - 1];
                int middleElement2 = tempArray[(count / 2)];
                medianValue = (middleElement1 + middleElement2) / 2;
            }
            else
            {
                medianValue = tempArray[(count / 2)];
            }

            return medianValue;
        }
    }
    class Program
    {
        static void Main()
        {
            

            string path1 = @"C:\Users\sintx\source\repos\Lab3\studentas1.txt";
            string path2 = @"C:\Users\sintx\source\repos\Lab3\studentas2.txt";
            string path3 = @"C:\Users\sintx\source\repos\Lab3\studentas3.txt";
            string path4 = @"C:\Users\sintx\source\repos\Lab3\studentas4.txt";

            string path1_p = @"C:\Users\sintx\source\repos\Lab3\studentas1_PASSED.txt";
            string path1_f = @"C:\Users\sintx\source\repos\Lab3\studentas1_FAILED.txt";

            string path2_p = @"C:\Users\sintx\source\repos\Lab3\studentas2_PASSED.txt";
            string path2_f = @"C:\Users\sintx\source\repos\Lab3\studentas2_FAILED.txt";

            string path3_p = @"C:\Users\sintx\source\repos\Lab3\studentas3_PASSED.txt";
            string path3_f = @"C:\Users\sintx\source\repos\Lab3\studentas3_FAILED.txt";

            string path4_p = @"C:\Users\sintx\source\repos\Lab3\studentas4_PASSED.txt";
            string path4_f = @"C:\Users\sintx\source\repos\Lab3\studentas4_FAILED.txt";
            List<string> studentPassed = new List<string>();
            List<string> studentFailed = new List<string>();
            Console.WriteLine("Enter student count: ");
            int kiekis=Int32.Parse(Console.ReadLine());
            var student1 = new Student("Name0", "Surname0");
            for (int i = 1; i < kiekis; i++)
            {
                student1.Studentas.Add(i, (student1.generateRandomNames(kiekis)[0], student1.generateRandomNames(kiekis)[1], student1.generateRandom(), student1.Exam, 0, 0));
            }
            foreach (var item in student1.Studentas)
            {
                //Console.WriteLine("Student: "+item.Value.Item1+" "+ item.Value.Item2);
            }
            for (int i = 0; i < kiekis; i++)
            {
                float suma = 0;
                int index = 1;
                //Console.WriteLine("Name = {0}, Lastname = {1}", student1.Studentas[i].Item1, student1.Studentas[i].Item2);
                foreach (var item1 in student1.Studentas[i].Item3)
                {
                    suma += item1;
                    //Console.WriteLine("HW" + index + " score is " + item1);
                    index++;
                }
                student1.Final_points_avg = suma / 5;
                student1.Final_points_med = student1.GetMedian(student1.Studentas[i].Item3);
                student1.Studentas[i] = (student1.Studentas[i].Item1, student1.Studentas[i].Item2, student1.Studentas[i].Item3, student1.Studentas[i].Item4, student1.Final_points_avg, student1.Final_points_med);
                //Console.WriteLine("Exam = {0}", student1.Studentas[i].Item4);
                //Console.WriteLine("Final points (Avg.) " + String.Format("{0:0.00}", student1.Studentas[i].Item5));
                //Console.WriteLine("Final points (Med.) " + String.Format("{0:0.00}", student1.GetMedian(student1.Studentas[i].Item3)));
            }
            Stopwatch firstFileCreation = Stopwatch.StartNew();
            if (kiekis == 10000) {
                using (var sw = new StreamWriter(path1))
                {
                    foreach (var item in student1.Studentas)
                    {
                        sw.WriteLine(item);
                    }
                }
                firstFileCreation.Stop();
            }
            else if (kiekis == 100000)
            {
                using (var sw = new StreamWriter(path2))
                {
                    foreach (var item in student1.Studentas)
                    {
                        sw.WriteLine(item);
                    }
                }
                firstFileCreation.Stop();
            }
            else if (kiekis == 1000000)
            {
                using (var sw = new StreamWriter(path3))
                {
                    foreach (var item in student1.Studentas)
                    {
                        sw.WriteLine(item);
                    }
                }
                firstFileCreation.Stop();
            }
            else if (kiekis == 10000000)
            {
                using (var sw = new StreamWriter(path4))
                {
                    foreach (var item in student1.Studentas)
                    {
                        sw.WriteLine(item);
                    }
                }
                firstFileCreation.Stop();
            }
            Console.WriteLine("First file creation: "+firstFileCreation.ElapsedMilliseconds);
            Stopwatch dataSorting = Stopwatch.StartNew();
            var sortedDict = from entry in student1.Studentas orderby entry.Value.Item6 descending select entry;
            sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
            var naujas = sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
            dataSorting.Stop();
            Console.WriteLine("Data sorting: "+dataSorting.ElapsedMilliseconds);

            Stopwatch dataSplit = Stopwatch.StartNew();
            foreach (var item in student1.Studentas)
            {
                if (item.Value.Item5 >= 5.0)
                {
                    string grades="";
                    foreach (var grade in item.Value.Item3)
                    {
                        grades = grades + grade + ", ";
                    }
                    string temp = item.Value.Item1 + " " + item.Value.Item2 + " " +grades;
                    studentPassed.Add(temp + item.Value.Item4 + ", " + item.Value.Item5 + ", " + item.Value.Item6);
                }
                else if (item.Value.Item5 < 5.0)
                {
                    string grades = "";
                    foreach (var grade in item.Value.Item3)
                    {
                        grades = grades + grade + ", ";
                    }
                    string temp = item.Value.Item1 + " " + item.Value.Item2 + " " + grades;
                    studentFailed.Add(temp + item.Value.Item4 + ", " + item.Value.Item5 + ", " + item.Value.Item6);
                }
            }
            dataSplit.Stop();
            Console.WriteLine("Data splitting: " + dataSplit.ElapsedMilliseconds);
            Stopwatch dataOutput = Stopwatch.StartNew();
            if (kiekis == 10000)
            {
                using (var sw = new StreamWriter(path1_p))
                {
                    sw.WriteLine("----------------------------------PASSED----------------------------------");
                    foreach (var item in studentPassed)
                    {
                        sw.WriteLine(item);
                    }
                }
                using (var sw = new StreamWriter(path1_f))
                {
                    sw.WriteLine("----------------------------------FAILED----------------------------------");
                    foreach (var item in studentFailed)
                    {
                        sw.WriteLine(item);
                    }
                }
                dataOutput.Stop();
            }
            else if (kiekis == 100000)
            {
                using (var sw = new StreamWriter(path2_p))
                {
                    sw.WriteLine("----------------------------------PASSED----------------------------------");
                    foreach (var item in studentPassed)
                    {
                        sw.WriteLine(item);
                    }
                }
                using (var sw = new StreamWriter(path2_f))
                {
                    sw.WriteLine("----------------------------------FAILED----------------------------------");
                    foreach (var item in studentFailed)
                    {
                        sw.WriteLine(item);
                    }
                }
                dataOutput.Stop();
            }
            else if (kiekis == 1000000)
            {
                using (var sw = new StreamWriter(path3_p))
                {
                    sw.WriteLine("----------------------------------PASSED----------------------------------");
                    foreach (var item in studentPassed)
                    {
                        sw.WriteLine(item);
                    }
                }
                using (var sw = new StreamWriter(path3_f))
                {
                    sw.WriteLine("----------------------------------FAILED----------------------------------");
                    foreach (var item in studentFailed)
                    {
                        sw.WriteLine(item);
                    }
                }
                dataOutput.Stop();
            }
            else if (kiekis == 10000000) {
                using (var sw = new StreamWriter(path4_p))
                {
                    sw.WriteLine("----------------------------------PASSED----------------------------------");
                    foreach (var item in studentPassed)
                    {
                        sw.WriteLine(item);
                    }
                }
                using (var sw = new StreamWriter(path4_f))
                {
                    sw.WriteLine("----------------------------------FAILED----------------------------------");
                    foreach (var item in studentFailed)
                    {
                        sw.WriteLine(item);
                    }
                }
                dataOutput.Stop();
            }
            Console.WriteLine("Data output: " + dataSplit.ElapsedMilliseconds);
        }
    }
}
