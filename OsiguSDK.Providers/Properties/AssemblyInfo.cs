﻿using System.Reflection;
using System.Runtime.InteropServices;
using log4net.Config;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("OsiguSDK.Providers")]
[assembly: AssemblyDescription("Osigu SDK for providers")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("OSIGU")]
[assembly: AssemblyProduct("OsiguSDK.Providers")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(true)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("4a98e636-b8ca-4862-b270-5c8bb661dc71")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.6.3")]
[assembly: AssemblyFileVersion("1.0.6.3")]

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]