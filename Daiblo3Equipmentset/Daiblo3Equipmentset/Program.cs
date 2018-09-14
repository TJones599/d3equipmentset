namespace Daiblo3Equipmentset
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Media;

    class Program
    {
        private static string folderpath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static SoundPlayer sp = new SoundPlayer();

        ///Pull file names from given file path, and create a string listing them.
        private static string folderContents(string topFolder)
        {
            string fc = "";
            DirectoryInfo dInfo = new DirectoryInfo(topFolder);
            FileInfo[] Files = dInfo.GetFiles("*");
            foreach (FileInfo file in Files)
            {
                fc = (fc + "\n" + file.Name);
            }

            return fc;
        }
       
        ///create a new loadout file, and save all provided information to the new file.
        ///new file name, and gear names are provided by the user.
        public static void newLoadout()
        {
            Console.Clear();
            string[] gearTitle = System.IO.File.ReadAllLines(folderpath+"\\DataSources\\gearTitle.txt");
            string[] Gear = new string[16];

            Console.Write("Enter name of loadout: ");
            string loadoutName = Console.ReadLine();
            loadoutName = loadoutName + ".txt";

            string loadoutFolder = (folderpath + "\\DataSources\\Loadout");
    
            if (!System.IO.Directory.Exists(loadoutFolder))
                System.IO.Directory.CreateDirectory(loadoutFolder);
 
            string filePath = System.IO.Path.Combine(loadoutFolder, loadoutName);

            if (System.IO.File.Exists(filePath))
            {
                Console.WriteLine("Loadout already exists.");
                return;
            }
            else
            {
                Console.WriteLine("Enter names for the following pieces of gear.\n");
                for (int i = 0; i < 16; i++)
                {
                    if (i < 7)
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    else if (i < 11)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (i < 13)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.Write(gearTitle[i] + ": ");
                    Gear[i] = Console.ReadLine();

                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                System.IO.File.WriteAllLines(filePath, Gear);

                string location = folderpath + @"\DataSources\Sound\OOT_Fanfare_Item.wav";

                sp.SoundLocation = location;
                sp.Load();
                sp.Play();

            }
            sp.SoundLocation= folderpath +"";
            Console.Clear();
        }

        ///lists out available loadouts, and displays the gear info of the selected loadout
        private static void viewLoadout()
        {
            Console.Clear();
            string topFolder = (folderpath + "\\DataSources\\Loadout");

            Console.WriteLine("Which File would you like to view?");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(folderContents(topFolder)+"\n");

            Console.ForegroundColor = ConsoleColor.Gray;

            string loadout = (Console.ReadLine());
            string filePath = System.IO.Path.Combine(topFolder, loadout);

            if (!System.IO.File.Exists(filePath))
            {
                Console.Clear();
                Console.WriteLine("File Not Found!\n");
                return;
            }
            string[] gearTitle = System.IO.File.ReadAllLines(folderpath + "\\DataSources\\gearTitle.txt");
            string[] gearInfo = System.IO.File.ReadAllLines(filePath);
            for (int i = 0; i < 16; i++)
            {
                if (i < 7)
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                else if (i < 11)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else if (i < 13)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.Cyan;

                const string format = "{0,-17}";

                string gearout = string.Format(format, gearTitle[i])+": "+gearInfo[i];
                Console.WriteLine(gearout);
                //Console.WriteLine(gearTitle[i] +": "+gearInfo[i]);
            }

            sp.SoundLocation = (folderpath + @"\\DataSources\\Sound\\OOT_AdultLink_Sneeze3.wav");
            sp.Load();
            sp.Play();

            Console.WriteLine();
            Console.ResetColor();
        }

        ///moves unwanted loadouts to "OldLoadout" folder
        private static void removeLoadout()
        {
            Console.Clear();
            string sourcePath= (folderpath + "\\DataSources\\Loadout");
            string destPath= (folderpath + "\\DataSources\\Loadout\\OldLoadout");
            string moveFile;
            
            Console.WriteLine("Which file would you like to remove:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(folderContents(sourcePath)+"\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            moveFile = Console.ReadLine();

            string sourceFile = System.IO.Path.Combine(sourcePath, moveFile);
            string destFile = System.IO.Path.Combine(destPath, moveFile);
            
            if (!System.IO.Directory.Exists(destPath))
                System.IO.Directory.CreateDirectory(destPath);

            if (System.IO.File.Exists(sourceFile))
            {
                System.IO.File.Copy(sourceFile, destFile, true);
                System.IO.File.Delete(sourceFile);
                sp.SoundLocation = folderpath + @"\\DataSources\\Sound\\LTTP_Link_Fall.wav";
                sp.Load();
                sp.Play();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("File Not Found!");
                return;
            }

            Console.Clear();
        }

        ///restores removed loadouts from OldLoadout.
        private static void restoreLoadout()
        {

            Console.Clear();
            string destPath = folderpath + "\\DataSources\\Loadout";
            string sourcePath = folderpath + "\\DataSources\\Loadout\\OldLoadout";
            string moveFile;
         
            Console.WriteLine("Which file would you like to restore:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(folderContents(sourcePath)+"\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            moveFile = Console.ReadLine();

            string sourceFile = System.IO.Path.Combine(sourcePath, moveFile);
            string destFile = System.IO.Path.Combine(destPath, moveFile);

            if (!System.IO.Directory.Exists(destPath))
                System.IO.Directory.CreateDirectory(destPath);

            if (System.IO.File.Exists(sourceFile))
            {
                System.IO.File.Copy(sourceFile, destFile, true);
                System.IO.File.Delete(sourceFile);

                sp.SoundLocation = folderpath + @"\\DataSources\\Sound\\OOT_Fanfare_SmallItem.wav";
                sp.Load();
                sp.Play();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("File Not Found!");
                return;
            }

            Console.Clear();
        }

        /*static void callTime()
        {
            TimeSpan time = DateTime.Now.TimeOfDay;
            Console.WriteLine(time.Hours + ":" + time.Minutes + "\n");
        }*/

        private static void Menu()
        {
            bool exit = false;
            bool invalidEntry = false;
            do
            {
                if (invalidEntry)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Option, please try again\n");
                    invalidEntry = false;
                }

              //  callTime();

                Console.WriteLine("Please choose one of the following or hit escape:\n"+
                    "1) Add new loadout\n"+
                    "2) View Loadouts\n"+
                    "3) Remove old loadout\n"+
                    "4) Restore deleted loadout\n"+
                    "5) Exit");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        {
                            Console.WriteLine("Esc pressed, exiting program...");
                            Thread.Sleep(750);
                            exit = true;
                            break;
                        }

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        {
                            newLoadout();
                            break;
                        }
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        {
                            viewLoadout();
                            break;
                        }
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        {
                            removeLoadout();
                            break;
                        }
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        {
                            restoreLoadout();
                            break;
                        }
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        {
                            Console.WriteLine("Exit selected, exiting program...");
                            Thread.Sleep(750);
                            exit = true;
                            break;
                        }
                    default:
                        {
                            invalidEntry = true;
                            break;
                        }
                }
            } while (!exit);

        }

        private static void Main(string[] args)
        {
           /* string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(path);


            string folderPath = string.Join("\\", path, "DataSources");

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Is there really any point to life anymore!?!?!?!?!");
                throw new InvalidOperationException("Unable to perform actions without a data source");
            }


            Console.ReadKey();*/
            Console.SetWindowSize(100, 25);

            Menu();

            sp.SoundLocation = folderpath + @"\\DataSources\\Sound\\OOT_AdultLink_Scream2.wav";
            sp.Load();
            sp.Play();
            Thread.Sleep(2500);


        }
    }
}
