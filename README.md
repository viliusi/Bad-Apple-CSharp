# Bad-Apple-CSharp
This is my attempt at making the touhou music video Bad Aple in the C# console

For now I think it's ok, but I want to change to a higher rip of the ascii art, I have all the frames as PNG, so my next goal will be converting that too ASCII art and setting it up in a similar manner as the current ascii rip.

Also thanks to Chion82 and his project with doing this in Python, I made good use of his data, to set it up C#, though I didn't look at how he coded his, instead I just did mine out of what I thought would work.
https://github.com/Chion82/ASCII_bad_apple

##Things to implement

1. I need to actually implement the rest of the song, currently I have the first 4 seconds
2. Chart explaining how far removed from reality this project is, from my point of view. (Shows the use of python, midi files, midi converters and multiple threads of music, hopefully)
3. Simplify the beep script, should be able to be reduced greatly
4. Make a better way of adding together two frequencies, for now I just add them together, but I need something that sounds bette, would just play all the frequencies, but C# only allows one beep at a time
