using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace combAlg1
{
    class Program
    {
        static public Int32[] was;
        static public List<List<Int32>> mas;
        static public List<List<Int32>> parts;
        static void Main(string[] args)
        {
            mas = readGraph();
            was = new int[mas.Count];
            parts = new List<List<int>>();
            parts.Add(new List<int>());
            parts.Add(new List<int>());
            fill(0,0);
            if (!isBipartite()) { File.WriteAllText("out.txt", "N"); }
            else
            {
                var output = new List<String>();
                parts[0].Sort();
                parts[1].Sort();
                output.Add("Y");
                if (parts[0].Min() < parts[1].Min())
                {
                    output.Add("");
                    foreach (int i in parts[0])
                    {
                        output[output.Count-1] += (i + 1).ToString() + " ";  
                    }
                    output[output.Count - 1] += "0";
                    output.Add("");
                    foreach (int i in parts[1])
                    {
                        output[output.Count - 1] += (i + 1).ToString() + " ";
                    }
                    output[output.Count - 1] += "0";
                }
                else
                {
                    output.Add("");
                    foreach (int i in parts[1])
                    {
                        output[output.Count - 1] += (i + 1).ToString() + " ";
                    }
                    output[output.Count - 1] += "0";
                    output.Add("");
                    foreach (int i in parts[0])
                    {
                        output[output.Count - 1] += (i + 1).ToString() + " ";
                    }
                    output[output.Count - 1] += "0";
                }
                File.WriteAllLines("out.txt", output); 
            }
        }
        public static List<List<Int32>> readGraph()
            {
                var file = new List<string>(File.ReadAllLines("in.txt"));
                int nodesCount = Int16.Parse(file[0]);
                var mas = new List<List<Int32>>();
                for (int i = 0; i < nodesCount; i++) mas.Add(new List<int>());
                for (int i = 1; i < file.Count; i++)
                {
                    String[] numbers = file[i].Split(' ');
                    for (int j = 0; (j < numbers.Length) && (numbers[j] != "0"); j++)
                    {
                        mas[i - 1].Add(Int32.Parse(numbers[j])-1);
                    }
                }
                return mas;
            }
        private static void fill(int i,int color)
        {
            
            if (was[i]!=1)
            {
                was[i] = 1;
                if (color % 2 == 0)
                {
                    parts[0].Add(i);
                }
                else
                {
                    parts[1].Add(i);
                }
                {
                    foreach (int l in mas[i])
                    {
                        fill(l, color+1);
                    }
                }
            }
        }
        private static bool isBipartite()
        {
            foreach (int i in parts[0])
            {
               foreach (int j in parts[0])
                if (mas[i].Contains(j)) return false;
            }
            foreach (int i in parts[1])
            {
                foreach (int j in parts[1])
                    if (mas[i].Contains(j)) return false;
            }
            return true;
        }
    }
}
