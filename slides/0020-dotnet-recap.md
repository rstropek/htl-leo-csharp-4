# .NET Recap

Let's remember .NET fundamentals

---

<!-- .slide: class="left" -->
## .NET Framework

* Development platform for building apps for web, Windows, Windows Server, and Microsoft Azure
* Components
  * Common language runtime (CLR)
  * .NET Framework class library
* .NET Framework services include:
  * Memory management
  * Type and memory safety
  * Security
  * Networking
  * Application deployment
* Data structures and APIs that abstract the lower-level Windows operating system
* Supports many programming languages including C#, F#, and Visual Basic

---

<!-- .slide: class="left" -->
## [.NET Core](https://www.microsoft.com/net/core#windowscmd)

* Cross-platform and open source implementation of .NET
  * Different flavor for different environments
  * E.g. *Xamarin* for mobile development, *Unity*/*Mono* for game development
  * [.NET Implementation Matrix](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support)
* Important components
  * [Core Common Language Runtime (CoreCLR)](https://github.com/dotnet/coreclr)
  * [Core Base Class Library (CoreFX)](https://github.com/dotnet/corefx)
  * [.NET Core SDK](https://docs.microsoft.com/en-us/dotnet/core/sdk)
* [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
  * A formal specification of .NET APIs that are available in each .NET implementation

---

<!-- .slide: class="left" -->
## [.NET 5](https://www.microsoft.com/net/core#windowscmd)

* Future of .NET
* Just one .NET, no more .NET Core/.NET Standard
  * Best of all worlds in one common .NET
  * E.g. Java interop not just on Android
  * E.g. better support for *Ahead of Time* compilation (AoT)
* Two runtimes:
  * Mono
  * CoreCLR (we will focus on that)

---

## [.NET 5](https://www.microsoft.com/net/core#windowscmd)

![Roadmap](https://devblogs.microsoft.com/dotnet/wp-content/uploads/sites/10/2019/05/dotnet_schedule.png)

---

## `dotnet` Command

* [Basic commands](https://docs.microsoft.com/en-us/dotnet/core/tools/)
  * `new`
  * `restore`
  * `run`
  * `test`
  * `build`
  * `publish`
* .NET [global and local tools](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools)
  * We will need that for *Entity Framework*
* [Project file format](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview)

---

<!-- .slide: class="left" -->
## Further Readings and Exercises

* Want to know more? Read/watch...
  * [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
  * [Create your first .NET Core app](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro)
  * [.NET In-Browser Tutorial](https://dotnet.microsoft.com/learn/dotnet/in-browser-tutorial/1)
  * [Writing C# Code in Visual Studio](https://docs.microsoft.com/en-us/visualstudio/ide/writing-code-in-the-code-and-text-editor)
