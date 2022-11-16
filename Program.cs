internal class Program
{
    private static void Main(string[] args)
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
        string line;
        try
        {
            //Pass the file path and file name to the StreamReader constructor.
            //Again thank you too Chion82, his project is linked in the ReadME.
            StreamReader sr = new StreamReader(allFrames);
            //We start by reading the first line, again example did so.
            line = sr.ReadLine();

            /*Finally some original code, though it may not be as optimized, we make a boolean called split, 
            if it gets set to true, we get to wait, then clear the screen, which is how we "render" frames. */
            bool split;

            //Setting up a list, to load the current frame, instead of just writing it from the streamreader
            List<string> currentFrame = new List<string>();

            //Cool while loop, as long as the line we are trying to read is valid, we can continue to render frames.
            while (line != null)
            {
                //The switch case here just checks for if the current line has an SPLIT in it, since SPLIT is what we use to identify where the "frames" split.
                switch (line)
                {
                    case var containsSplit when line.Contains("SPLIT"):
                        split = true;
                        //This makes it so we can split the line with SPLIT in it, then it looks a bit better :)
                        line = sr.ReadLine();
                        line = sr.ReadLine();
                        break;
                    default:
                        split = false;
                        //This is again a code segment from Microsoft, what it does is print our "line" then it jumps to the next one, a bit like a type writer.
                        currentFrame.Add(line);
                        line = sr.ReadLine();
                        break;
                }

                /*And here we again have our split feature, if an S was found, and split was initiated, 
                then this will let us wait for enough time to emulate a frame, and run the code */
                switch (split)
                {
                    case true:
                        //Here we print the entire list, and clear it for future use
                        currentFrame.ForEach(i=> Console.Write("{0}\n", i));
                        currentFrame.Clear();
                        //This sleeps the process and gets ready to print a new frame, to simulate frametimes
                        Thread.Sleep(50);
                        Console.SetCursorPosition(0, 0);
                        break;
                    case false:
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
}