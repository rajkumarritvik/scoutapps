# Setting up the 2026 app

To set up the app in the folder ``` scoutapps/2026 ``` onto your scouting tablet, follow the instructions below.

## Materials

* USB Thumbdrive
* USB Type-A to Type-C Converter
* Scouting Tablet(s)
* Personal Wi-Fi Network (Optional, preferably for those using school Wi-Fi)
* Internet access or Google Play Store on the **tablet**

## Setup

On your scouting tablet, install File Manager through Google Play Store, either through the app or through [this link](https://play.google.com/store/apps/details?id=com.alphainventor.filemanager&hl=en_US). It should be this one:
![filemanagerplus](../assets/filemanagerplus.png)

Also through Google Play Store, install the Simple HTTP Server app through the Play Store app or through [this link](https://play.google.com/store/apps/details?id=com.phlox.simpleserver&hl=en_US). It should be this one:
![simplehttp](../assets/simplehttp.png)

On your computer, copy over the folder ` scoutapps\2026\ScoutApp.Browser\bin\Release\net8.0-browser\publish\wwwroot ` to the USB thumbdrive that was listed in the materials in your file explorer. You can recognize that it is the right folder if you see that it contains ` index.html `:
![wwwroot](../assets/wwwroot.png)

If needed, use the USB type-a to type-c converter to connect the thumbdrive to the tablet. It should give you a popup saying:
![]()

Click "OK", then go back into File Manager +, go to the thumbdrive, and you should get a popup like this:
![]()

Click "OK", then on the next screen, and then click "USE THIS FOLDER".
![]()

Click "ALLOW", then as it moves back over to File Manager +, click and hold the wwwroot folder, copy it, and click "Paste" in the bottom right of the screen.
![]()

Open the Simple HTTP Server, and in the top left, click the dropdown, click "Downloads", click the wwwroot folder, and then click the "SELECT THIS FOLDER" button at the bottom of the screen.
![]()

Double check that the "Root folder" says /scorage/emulated/0/Download/wwwroot, then click the "START" button in the center of the screen, then click on the URL and click the "Navigate" button to open it in the browser.

If you have followed these instructions correctly then the app should work successfully in the browser! Enjoy scouting for FRC 2026 Rebuilt, presented by Haas!