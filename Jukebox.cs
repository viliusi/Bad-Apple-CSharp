/*
public void jukebox();
{
    //Well this isn't gonna end well, but apperently, C# can do beeps, 
    //so with a few Thread.Sleep's and Beeps at different frequencies, I should definetely be able to recreate the music

    //This is definetely where I'll end up going insane, and learn how to play music.
    //Console.Beep(Frequency, Seconds);
    //Don't mind me, just doing some codey stuff, for me it took quite a while to be able to refer to the project location.
    //The only reason I did it this way, is because I work on both Windows and Linux, over multiple computers, which means this will make it easier.
    Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
    String debug = Directory.GetCurrentDirectory();
    String music = debug.Remove(debug.Length - 17) + "/Music.txt";
    //We start by making a try catch. Why? Because that's what the example online did.
    string musicLine;
    try
    {
        //Pass the file path and file name to the StreamReader constructor.
        //Again thank you too Chion82, his project is linked in the ReadME.
        StreamReader sr = new StreamReader(music);
        //We start by reading the first line, again example did so.
        musicLine = sr.ReadLine();

        //Finally some original code, though it may not be as optimized, we make a boolean called skip, 
        //if it gets set to true, we get to wait, then clear the screen, which is how we "render" frames. 
        bool skip;

        //Cool while loop, as long as the line we are trying to read is valid, we can continue to render frames.
        while (musicLine != null)
        {
            //The switch case here just checks for if the current line has an S in it, since S is what we use to identify where the "frames" split.
            switch (musicLine)
            {
                case var containsS when musicLine.Contains("SKIP"):
                    skip = true;
                    //This makes it so we can skip the line with SKIP in it, then it looks a bit better :)
                    musicLine = sr.ReadLine();
                    musicLine = sr.ReadLine();
                    break;
                default:
                    skip = false;
                    //This is again a code segment from Microsoft, what it does is print our "line" then it jumps to the next one, a bit like a type writer.
                    musicLine = sr.ReadLine();
                    break;
            }

            //And here we again have our skip feature, if an S was found, and skip was initiated, 
            //then this will let us wait for enough time to emulate a frame, and run the code
            switch (skip)
            {
                case true:
                    Thread.Sleep(100);
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
        //yay
        Console.WriteLine("Hope you enjoyed the work, while I myself didn't do too much of the work, I'm still proud of what I achieved :)");
    }
}
*/