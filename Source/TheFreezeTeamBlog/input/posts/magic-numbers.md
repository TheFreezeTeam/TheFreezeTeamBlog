Title: Change your Windows 10 Azure VM RDP port
Published: 01/20/2017
Tags: 
  - Azure 
Author: Steven T. Cramer
Description: First of all to all the security guys out there. Increasing functionality securely is the goal.  Just turning all the computers off and disconnecting the internet is secure.
Excerpt: First of all to all the security guys out there. Increasing functionality securely is the goal.  Just turning all the computers off and disconnecting the internet is secure.

---

First of all, to all the "security" guys out there. Increasing functionality securely is the goal.  Just turning all the computers off and disconnecting the internet is secure.  Anyone can do that.  A port number in TCP/IP is just an integer in a packet. It is NOT a new attack vector.  Every protocol can run over any port, it is just a number.  If you want to block something it requires more thought than just blocking a port. Ok, rant over.

In case your "security" guys think it makes sense to block outbound RDP on port 3389, I show here how we can still be functional and connect to an Azure Windows 10 VM via RDP.

You need to do 3 things and you have to be able to RDP into the system to do 2 of them.  (So do from home or use Teamviewer to get out of your "secure" environment.):

1. From Azure Portal Add Inbound Security Rule;

![](/content/images/2017/01/2017-01-20_1143.png)

2. We need to open the firewall to allow incoming connections to the new port.

https://superuser.com/questions/723832/windows-firewall-blocks-remote-desktop-with-custom-port

Open windows Firewall on the Azure VM.

![](/content/images/2017/01/2017-01-20_1402.png)

Add New Rule that allows incoming connection on port 3390 (Or any number not being used)

![](/content/images/2017/01/2017-01-20_1401.png)


3. Last change the port on which Windows 10 is listening for RDP.

* Start RegEdit
* go to HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\TerminalServer\WinStations\RDP-Tcp\PortNumber 
* Change the decimal value to 3390

![](/content/images/2017/01/2017-01-20_1436.png)

* Close Regedit
* Restart your VM.  CAUTION: IF YOU DID ANYTHING WRONG YOU MAY NOT BE ABLE TO ATTACH TO THE VM.





