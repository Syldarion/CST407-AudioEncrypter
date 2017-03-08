using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST407_AudioEncrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            AudioEncrypter file = new AudioEncrypter();
            file.WavToBytes(@"C:\Users\parag\Desktop\test_file.wav");
            file.AddMessage("Hello World");
            file.BytesToWav(@"C:\Users\parag\Desktop\test_result.wav");
            Console.WriteLine(file.RemoveMessage());
            Console.ReadLine();
        }
    }
}
