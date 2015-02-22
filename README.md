####SessionWatch

The idea behind this project was to react to session change events by performing a configurable set of actions, driven by my initial need to kill Steam as different users log into my children's computer (you cannot launch Steam if it is already running under a different user).

In its current state, it runs as a Windows service that will respond to session unlock or logon events by running the following command, but only if the newly logged in user is different than the last logged in user:

```taskkill /F /IM steam*```

####Installation

1. Build SessionWatch or download latest assembly here:  https://github.com/TravisTroyer/SessionWatch/tree/master/releases
1. Copy or extract somewhere reasonable (e.g. c:\program files\sessionwatch).
1. Run SessionWatch.Service.exe as administrator.
1. You'll be presenter with a setup menu, press I to install the service.
1. Assuming success, the service is installed and configured to automatically run on start-up, though the installer does not currently start the service, so you'll need to start it manually.
