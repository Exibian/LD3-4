using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;

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
        public Student() { }
        public decimal GetMedian(int[] array)
        {
            int[] tempArray = array;
            int count = tempArray.Length;

            Array.Sort(tempArray);

            decimal medianValue;

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
            var student1 = new Student();
            string path = @"C:\Users\sintx\source\repos\Lab3\students.txt";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int index = 0;
                    while (sr.Peek() >= 0)
                    {
                        int[] homework = new int[5];
                        var variables = sr.ReadLine().Split(' ');
                        for (int i = 2; i < 7; i++)
                        {
                            homework[i - 2] = int.Parse(variables[i]);
                        }
                        student1.Studentas.Add(index, (variables[0], variables[1], homework, int.Parse(variables[7]), 0, 0));
                        index++;
                    }

                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("File was not found.");
                System.Environment.Exit(1);
            }
            
            var sortedDict = from entry in student1.Studentas orderby entry.Value.Item1 ascending select entry;
            sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
            var naujas = sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
            Console.WriteLine("Surname  " + "Name         " + "Final points (Avg.)    " + "Final points (Med.)");
            Console.WriteLine("----------------------------------------------------------------");
            try
            {
                for (int i = 0; i < student1.Studentas.Count; i++)
                {
                    float suma = 0;
                    Console.Write(naujas[i].Item1 + " " + naujas[i].Item2 + " ");
                    foreach (var item1 in naujas[i].Item3)
                    {
                        suma += item1;
                    }
                    student1.Final_points_avg = suma / naujas[i].Item3.Length;
                    naujas[i] = (naujas[i].Item1, naujas[i].Item2, naujas[i].Item3, naujas[i].Item4, student1.Final_points_avg, student1.Final_points_med);
                    Console.Write("                      " + String.Format("{0:0.00}", naujas[i].Item5));
                    Console.WriteLine("                   " + String.Format("{0:0.00}", student1.GetMedian(naujas[i].Item3)));
                }
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                Console.WriteLine("Value with provided key not found in dictionary.");
                System.Environment.Exit(1);
            }
        }
    }
}
