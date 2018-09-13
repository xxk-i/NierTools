using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTPRepacker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: WTPRepacker.exe (path)");
                Environment.Exit(1);
            }

            string wtpFolder = args[0];
            string[] fileList = Directory.GetFiles(wtpFolder);
            var ddsList = new List<string>();
            byte[] ddsMAGIC = { 0x44, 0x44, 0x53, 0x20 };

            foreach (var fileName in fileList)
            {
                if (fileName.Substring(fileName.Length - 4) == ".dds")
                {
                    ddsList.Add(fileName);
                }

                else
                {
                    Console.WriteLine(fileName + " is not a dds, skipping...");
                }
            }

            foreach (var ddsFile in ddsList)
            {
                string outWTP = wtpFolder + ".wtp";
                using (FileStream fs = new FileStream(ddsFile, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer;
                    using (var reader = new BinaryReader(fs))
                    {
                        buffer = reader.ReadBytes(4);
                    }

                    if (buffer.SequenceEqual(ddsMAGIC) == false)
                    {
                        Console.Write(ddsFile + " has the .dds extension but the wrong magic number. Skip? (y/n)");
                        string skipDDSInput = Console.ReadLine();
                        if (skipDDSInput == "yes" || skipDDSInput == "y")
                        {
                            continue;
                        }
                    }

                    using (FileStream outFile = new FileStream(wtpFolder + ".wtp", FileMode.Append, FileAccess.Write))
                    {
                        int outFileOffset = 0;
                        byte[] bytestoWrite = File.ReadAllBytes(ddsFile);
                        outFile.Write(bytestoWrite, outFileOffset, bytestoWrite.Length);
                        outFileOffset += bytestoWrite.Length;
                    }
                }
            }
        }
    }
}