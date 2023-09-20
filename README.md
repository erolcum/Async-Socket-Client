# Async-Socket-Client
Asynchronous socket client with C#

I wrote the code from youtube video :
https://www.youtube.com/watch?v=LV45r8BH98Y

but the program could not receive the response from server..
I changed the code in ReceiveCallback(IAsyncResult ar) method to solve the issue.
I also add a while(true) loop to StartClient() method.
You can use the free program Hercules as server.. You should select TCP Server tab
then press Listen button in Hercules.. Download it from :
https://www.hw-group.com/software/hercules-setup-utility

