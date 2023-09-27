# Async-Socket-Client
Asynchronous socket client with C#

I wrote the Program.cs from youtube video :
https://www.youtube.com/watch?v=LV45r8BH98Y

but the program could not receive the response from server..
I changed the code in ReceiveCallback(IAsyncResult ar) method to solve the issue.
I also add a while(true) loop to StartClient() method.
You can use the free program Hercules as server.. You should select TCP Server tab
then press Listen button in Hercules.. Download it from :
https://www.hw-group.com/software/hercules-setup-utility

I noticed something that when you write **Can you write a program for async socket client in C#** in **ChatGPT** it gives the same code in youtube video..<br>
I think it is a standard sample program that was published in msdn. If you look into msdn there is a newer version with **async** and **await** keywords.. ChatGPT knows that version too..<br>

**WinForm-sample** folder has a good example with a Windows Form. Form works in a thread, async methods for tcp client are work in different threads. That's why you can not update a text box directly..

