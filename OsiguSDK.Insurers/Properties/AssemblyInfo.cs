﻿using System.Reflection;
using System.Runtime.InteropServices;
using log4net.Config;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("OsiguSDK.Insurers")]
[assembly: AssemblyDescription("Osigu SDK for insurers")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("OSIGU")]
[assembly: AssemblyProduct("OsiguSDK.Insurers")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("54c2fad1-6968-4bda-8a81-4a6e972c5e6b")]

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
[assembly: AssemblyVersion("1.0.6.5")]
[assembly: AssemblyFileVersion("1.0.6.5")]

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]