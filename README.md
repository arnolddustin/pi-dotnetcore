#  pi-dotnetcore
A library for .NET Core applications and services for the RaspberryPi.

Current features include a dependency-injectable adapter for working with the RaspberryPi's [GPIO pins](https://www.raspberrypi.org/documentation/hardware/raspberrypi/gpio/README.md).

# Getting Started Guide
Getting started from scratch with a new, out-of-the-box RaspberryPi.

### Part 1: Prepare the RaspberryPi
1. If your RaspberryPi came with a pre-installed microSD card, skip ahead to step 3.
2. Download and install the RaspberryPi operating system software (NOOBS) on a microSD card. [Full instructions can be found here](https://www.raspberrypi.org/downloads/noobs/).
3. Boot up your RaspberryPi
4. During initial bootup, configure the wifi settings for the device.
5. From the NOOBS screen, install Raspbian Stretch Lite

**Security Information:** The default username on the device is `pi` and the default password is `raspberry`. It is strongly recommendended to change the default password at this point.

### Part 2: Enabling ssh on the RaspberryPi
1. Connect a keyboard to the RaspberryPi
2. Boot up the device
3. Login to terminal
4. Run the Raspbian configuration tool: `sudo raspi-config`
5. Select `Interfacing Options`
6. Navigate to `SSH`
7. Enable ssh then exit the configuration tool

* optional: [configure passwordless ssh access](https://www.raspberrypi.org/documentation/remote-access/ssh/passwordless.md)

### Part 3: Install .NET Core on the RaspberryPi
1. Connect to the RaspberryPi via SSH: `ssh pi@raspberrypi` and enter your password
2. Install prerequisite packages: `sudo apt-get install curl libunwind8 gettext`
* Download the .NET Runtime: `curl -sSL -o dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Runtime/release/2.0.0/dotnet-runtime-latest-linux-arm.tar.gz`
* Extract the .NET Runtime `sudo mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet`
* Make the `dotnet` command available from anywhere: `sudo ln -s /opt/dotnet/dotnet /usr/local/bin`

## References
* installing dotnetcore on raspbian https://blogs.msdn.microsoft.com/david/2017/07/20/setting_up_raspian_and_dotnet_core_2_0_on_a_raspberry_pi/
* raspberry pi GPIO pinouts https://pinout.xyz/
* running a web server on the pi http://alexdberg.blogspot.com/2012/11/creating-public-web-server-on-raspberry.html
