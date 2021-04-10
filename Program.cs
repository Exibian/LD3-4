using System;
using System.Collections.Generic;
using System.Collections;

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
            var student1 = new Student(Console.ReadLine(), Console.ReadLine());
            for (int i = 1; i < 5; i++)
            {
                student1.Studentas.Add(i, (Console.ReadLine(), Console.ReadLine(), student1.generateRandom(), student1.Exam, 0, 0));
            }
            for (int i = 0; i < student1.Hw.Length; i++)
            {
                float suma = 0;
                int index = 1;
                Console.WriteLine("Name = {0}, Lastname = {1}", student1.Studentas[i].Item1, student1.Studentas[i].Item2);
                foreach (var item1 in student1.Studentas[i].Item3)
                {
                    suma += item1;
                    Console.WriteLine("HW" + index + " score is " + item1);
                    index++;
                }
                student1.Final_points_avg = suma / 5;
                student1.Studentas[i] = (student1.Studentas[i].Item1, student1.Studentas[i].Item2, student1.Studentas[i].Item3, student1.Studentas[i].Item4, student1.Final_points_avg, student1.Final_points_med);
                Console.WriteLine("Exam = {0}", student1.Studentas[i].Item4);
                Console.WriteLine("Final points (Avg.) " + String.Format("{0:0.00}", student1.Studentas[i].Item5));
                Console.WriteLine("Final points (Med.) " + String.Format("{0:0.00}", student1.GetMedian(student1.Studentas[i].Item3)));
            }
        }
    }
}
