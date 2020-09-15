# Course Software Prerequisites

## Exercise

Install the *required software* mentioned below in this document.

Send an email to your teacher Mr. Stropek ([r.stropek@htl-leonding.ac.at](mailto:r.stropek@htl-leonding.ac.at)) with the following screenshots:

* Screenshot of *Visual Studio Installer* showing that Visual Studio has been installed
* Screenshot after you ran the command `dotnet --list-runtimes` in a console window (should show that *.NET Core 3.1* and *.NET 5* are installed)
* Visual Studio Code:
  * Create a new folder named *HelloWorld*, 
  * run `dotnet new console` in this new folder in a console window, 
  * open the new folder in *Visual Studio Code*, and 
  * send a screenshot of *Program.cs* that shows that the file could have been opened without any issues.
* Screenshot of the Git GUI client of your choice

## Required Software

### Operating System

Use Windows, Linux, or Mac. Windows is the most convenient option.

### Visual Studio

* [*Visual Studio 2019 Preview*](https://visualstudio.microsoft.com/vs/preview/) on Windows (most convenient option)
  * *Preview* version is sufficient
  * Use a licensed version (e.g. *Professional*) if you have a license
* Install at least the following modules:
  * *.NET Core cross-platform development*
  * *ASP.NET and web development*
  * *.NET desktop development*
  * *Azure development*

### .NET

**Note** that the following SDKs are automatically installed if you install *Visual Studio 2019 Preview* (see above). You only need to install the SDKs manually if you do not want to use Visual Studio.

* Install [*.NET*](https://dotnet.microsoft.com/download/dotnet-core)
  * *.NET Core 3.1 SDK* (LTS)
  * *.NET 5.0* (Preview)
* Install [*dnSpy*](https://github.com/0xd4d/dnSpy/releases)

### Visual Studio Code

* [Visual Studio Code](https://code.visualstudio.com)
* Install at least the following plugins:
  * [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
  * [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)
  * [Live Share](https://marketplace.visualstudio.com/items?itemName=MS-vsliveshare.vsliveshare-pack)

### Other Development Tools

* [Git](https://git-scm.com)
* A [Git GUI Client](https://git-scm.com/download/gui/win) of your choice (I use *Git Extensions*)

### Recommended, but Optional

* Docker
  * [Docker Desktop](https://www.docker.com/products/docker-desktop) (Windows, Mac)
  * [Docker CE](https://docs.docker.com/engine/install/#supported-platforms) (Linux)
* Windows: [*Windows Subsystem for Linux 2*](https://docs.microsoft.com/en-us/windows/wsl/about)
