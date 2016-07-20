# NimbleEdit
VS2015 plugin for using Edit and Continue easier

This plugin add a keyboard binding into VS options for NimbleEdit (default ctrl+,). It functions as follows when used:

- If you are not already debugging, it starts debugging
- If you are debugging and program isn't paused, it pauses the program and navigates back (ctrl+- by default). This is to get you to where you were originally editing your code.
- If paused, it'll unpause the program

I usually bind this to my "Start Debugging"-key (default F5). This simplifies the usual workflow of using Edit and Continue. To make a quick edit to a running program you usually have to do all these steps manually. I make games and I often do the following when I notice a problem in the program:
- Add breakpoint to pause execution (F9)
- Edit the code 
- Remove breakpoint (F9)
- Continue execution (F5)

Using NimbleEdit the workflow becomes:
- Pause execution (F5)
- Edit the code
- Continue execution (F5)

Less keys, less steps, faster editing. It might seem like insignificant difference but if you do this a lot, like you do while developing games, it makes a big difference. Essentially, this plugin does what Andrew Russell showed in his video back in 2012 using a mouse macro program.

You can see his original method here: https://www.youtube.com/watch?v=mfJEug3Y8ss
