internal class Program
{
    private static void Main(string[] args)
    {
        //Since the video is encoded in black on white, we need to set our console to that too.
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;

        //We start by making a try catch. Why? Because that's what the example online did.
        string line;
        try
        {
            //Pass the file path and file name to the StreamReader constructor.
            //Again thank you too Chion82, his project is linked in the ReadME.
            StreamReader sr = new StreamReader(@"/home/vilius/Desktop/Bad Apple/allFrames.txt");
            //We start by reading the first line, again example did so.
            line = sr.ReadLine();

            /*Finally some original code, though it may not be as optimized, we make a boolean called skip, 
            if it gets set to true, we get to wait, then clear the screen, which is how we "render" frames. */
            bool skip;

            //Cool while loop, as long as the line we are trying to read is valid, we can continue to render frames.
            while (line != null)
            {
                //The switch case here just checks for if the current line has an S in it, since S is what we use to identify where the "frames" split.
                switch (line)
                {
                    case var containsS when line.Contains("S"):
                        skip = true;
                        break;
                    default:
                        skip = false;
                        break;
                }

                //This is again a code segment from Microsoft, what it does is print our "line" then it jumps to the next one, a bit like a type writer.
                Console.WriteLine(line);
                line = sr.ReadLine();

                /*And here we again have our skip feature, if an S was found, and skip was initiated, 
                then this will let us wait for enough time to emulate a frame, and run the code */
                switch (skip)
                {
                    case true:
                        Thread.Sleep(100);
                        Console.Clear();
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
            Console.WriteLine("Hope you enjoyed the work, while I myself didn't do too much of the work, I'm still proud of what I achieved :)");
        }
    }
}