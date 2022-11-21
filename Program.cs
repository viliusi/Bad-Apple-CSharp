using NAudio;

internal class Program
{
    static void Main(string[] args)
    {
        /*The reason I don't just combine the ascii art and beeps into one thread, is because then I'd have to sync, the music and visuals in the code
        With how it is now, I won't have to write code to have the notes wait for visuals and in general complicate it more.*/

        //We make the main thread, for the visual code
        Thread mainThread = Thread.CurrentThread;
        mainThread.Name = "Main Thread";

        //Another thread for ascii art
        Thread asciiThread = new Thread(ascii);
        asciiThread.Name = "Ascii Art";

        /*And we make a second thread, for the beeps, would have set up more threads for beeps, 
        but C# only ever allows you to run one beep at a time, otherwise they start fighting for priority*/
        Thread musicThread = new Thread(beeps);
        musicThread.Name = "Beeps";

        Console.WriteLine(@"Choice what kind of audio you would prefer:
        1. No audio
        2. NAudio sine (Intended experience)
        3. NAudio MP3
        4. Console.Beep (Windows only)");

        /*We set up a switch case so that you can decide between some different kinds of audio, 
        like if you'd prefer to use NAudio Signal generator, NAudio mp3 or just console beeps. */
        bool continu = false;
        while (continu == false)
        {
            string audioChoice = Console.ReadLine();

            switch (audioChoice)
            {
                case "1":
                case "No audio":
                    bool audioPreference = false;
                    continu = true;
                    //Don't start beep thread
                    break;
                case "2":
                case "NAudio sine":
                    continu = true;
                    //Method start change method to sine
                    break;
                case "3":
                case "NAudio MP3":
                    continu = true;
                    //Method start change method to MP3
                    break;
                case "4":
                case "Console.Beep":
                    continu = true;
                    //Method start change method to beep
                    break;
                default:
                    Console.WriteLine("Invalid, try writing just writing a number, or write the choice you'd like to take");
                    continu = false;
                    break;
            }
        }
        asciiThread.Start();
        musicThread.Start();
    }
    static void ascii()
    {
        /*Don't mind me, just doing some codey stuff, for me it took quite a while to be able to refer to the project location.
        The only reason I did it this way, is because I work on both Windows and Linux, over multiple computers, which means this will make it easier.*/
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        String debug = Directory.GetCurrentDirectory();
        String allFrames = debug.Remove(debug.Length - 17) + "/allFrames.txt";
        //Since the video is encoded in black on white, we need to set our console to that too.
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Title = "Bad Apple C#";
        //We start by making a try catch. Why? Because that's what the example online did.
        string FrameLine;
        try
        {
            //Pass the file path and file name to the StreamReader constructor.
            //Again thank you too Chion82, his project is linked in the ReadME.
            StreamReader frames = new StreamReader(allFrames);
            //We start by reading the first line, again example did so.
            FrameLine = frames.ReadLine();

            /*Finally some original code, though it may not be as optimized, we make a boolean called split, 
            if it gets set to true, we get to wait, then clear the screen, which is how we "render" frames. */
            bool split;

            //Setting up a list, to load the current frame, instead of just writing it from the streamreader
            List<string> currentFrame = new List<string>();

            //Cool while loop, as long as the FrameLine we are trying to read is valid, we can continue to render frames.
            while (FrameLine != null)
            {
                //The switch case here just checks for if the current FrameLine has an SPLIT in it, since SPLIT is what we use to identify where the "frames" split.
                switch (FrameLine)
                {
                    case var containsSplit when FrameLine.Contains("SPLIT"):
                        split = true;
                        //This makes it so we can split the FrameLine with SPLIT in it, then it looks a bit better :)
                        FrameLine = frames.ReadLine();
                        /* This is where I'd put my code that reads beep frequencies from a txt document and plays them through the beep command,
                        but for some reason microsoft (ew) doesn't support beep with frequencies on linux, which is very not cool, if you ask me */
                        break;
                    default:
                        split = false;
                        //This is again a code segment from Microsoft, what it does is print our "FrameLine" then it jumps to the next one, a bit like a type writer.
                        currentFrame.Add(FrameLine);
                        FrameLine = frames.ReadLine();
                        break;
                }
                switch (split)
                {
                    case true:
                        //Here we print the entire list, and clear it for future use
                        currentFrame.ForEach(i => Console.Write("{0}\n", i));
                        currentFrame.Clear();
                        //This sleeps the process and gets ready to print a new frame, to simulate frametimes
                        Thread.Sleep(50);
                        Console.SetCursorPosition(0, 0);
                        break;
                    default:
                        break;
                }
            }
        }
        finally
        {
            //Setting up for a dramatic end
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            //yay
            Console.WriteLine("Hope you enjoyed the work, while I myself didn't do too much of the work, I'm still proud of what I achieved :)");
            Console.ReadKey();
        }
    }
    public static void beeps()
    {
        try
        {
            //Again we set up the directory so that we can stream the music notes
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String debug = Directory.GetCurrentDirectory();
            String musicLocation = debug.Remove(debug.Length - 17) + "/music.txt";
            StreamReader musicFreq = new StreamReader(musicLocation);
            string freqLine = musicFreq.ReadLine();

            //This loop should run everytime the stream reader reads a non null line
            while (freqLine != null)
            {
                //This sets up an array with our frequencies in an array and makes it easier to read for the beep function
                string[] stringNumbers = freqLine.Split(',');
                int[] numbers = new int[stringNumbers.Length];
                for (int i = 0; i < stringNumbers.Length; i++)
                    if (int.TryParse(stringNumbers[i], out int num))
                    {
                        numbers[i] = num;
                    }
                    else
                        Console.WriteLine("Array broke");

                //Sets up the frequencies for the beep function, these variables, will be used for switch statements and math
                int length = stringNumbers.Length;
                int frequency1 = 0;
                int frequency2 = 0;
                int frequency3 = 0;

                //This just sets up the array's numbers to ints for beeps
                switch (length)
                {
                    case 1:
                        frequency1 = numbers[0];
                        break;
                    case 2:
                        frequency1 = numbers[0];
                        frequency2 = numbers[1];
                        break;
                    case 3:
                        frequency1 = numbers[0];
                        frequency2 = numbers[1];
                        frequency3 = numbers[2];
                        break;
                    default:
                        break;
                }

                //All of this code should be able to be greatly simplefied, but it works for now

                //With this we combine multiple notes, since we can only run one beep at any one time
                int note = frequency1;

                //This just makes sure, that if our frequencies, do not reach the requirement for beeps, it will not try to run it 
                if (37 <= frequency1 && frequency1 <= 32767)
                {
                    //This code should hopefully let me switch between which kind of audio the user wants to hear, if I could get the variables from main
                    /*switch (Main(audioPreference))
                    {
                        case "MP3":
                        case "NAudio":
                        var sine20Seconds = new SignalGenerator()
                            {
                            Gain = 0.2,
                            Frequency = frequency1,
                            Type = SignalGeneratorType.Square
                                }
                                .Take(TimeSpan.FromSeconds(0.204));
                                using (var wo = new WaveOutEvent())
                                {
                            wo.Init(sine20Seconds);
                            wo.Play();
                        while (wo.PlaybackState == PlaybackState.Playing)
                            {

                            }
                        }
                        case "Beep":
                        Console.Beep(note, 204);
                        break;
                        default:
                            break;
                    }*/

                    //Beep
                    Console.Beep(note, 204);
                }
                else
                {

                }
                freqLine = musicFreq.ReadLine();
            }
        }
        finally
        {
            //This is where we should end, once the code is done running, maybe the thread will even terminate itself
        }
    }
}
