using System;
using System.IO;
using System.Net;
using System.Web;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BrowserStackApi {
  public class BrowserStackTunnel {
    String accessKey = "";
    String argumentString = "";
    ZipBinary binary = new ZipBinary();
    KeyValuePair<String, String> emptyStringPair = new KeyValuePair<String, String>();

    List<KeyValuePair<string, string>> valueCommands = new List<KeyValuePair<string, string>>() {
      new KeyValuePair<string, string>("localidentifier", "-localIdentifier"),
      new KeyValuePair<string, string>("hosts", ""),
      new KeyValuePair<string, string>("proxyhost", "-proxyHost"),
      new KeyValuePair<string, string>("proxyport", "-proxyPort"),
      new KeyValuePair<string, string>("proxyuser", "-proxyUser"),
      new KeyValuePair<string, string>("proxypass", "-proxyPass"),
    };

    List<KeyValuePair<string, string>> booleanCommands = new List<KeyValuePair<string, string>>() {
      new KeyValuePair<string, string>("verbose", "-v"),
      new KeyValuePair<string, string>("onlyautomate", "-onlyAutomate"),
    };

    public BrowserStackTunnel() : this(Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY")) {
    }

    public BrowserStackTunnel(String BrowserStackKey) {
      Regex.Replace(BrowserStackKey, @"\s+", "");
      if(BrowserStackKey == "") {
        Console.WriteLine ("Browserstack Access Key cannot be empty");
        throw new Exception("Auth Error");
      }
      accessKey = BrowserStackKey;
    }

    public void addArgs(String key, String value) {
      KeyValuePair<String, String> result;
      key = key.Trim().ToLower();

      result = valueCommands.Find (pair => pair.Key == key);
      if(!result.Equals(emptyStringPair)) {
        argumentString += result.Value + " " + value.Trim().ToLower() + " ";
      }
      result = booleanCommands.Find (pair => pair.Key == key);
      if(!result.Equals(emptyStringPair)) {
        if(value.Trim().ToLower() == "true") {
          argumentString += result.Value;
        }
      }

      Console.WriteLine (accessKey + " " + argumentString);
    }

    public bool start() {
      binary.getBinary();
      return true;
    }
  }

  internal class ZipBinary {
    String zipPath = "bin/";
    String zipName = "BrowserStackLocal.zip";
    String binaryName = "BrowserStackLocal.exe";
    String downloadURL = "https://www.browserstack.com/browserstack-local/BrowserStackLocal-win32.zip";

    public String getBinary() {
      System.IO.Directory.CreateDirectory(zipPath);
      File.SetAttributes(zipPath, FileAttributes.Normal);

      using (var client = new WebClient()) {
        client.DownloadFile(downloadURL, zipPath + zipName);

        ZipStorer zip = System.IO.Compression.ZipStorer.Open(zipPath + zipName, FileAccess.Read);
        List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();
        foreach (ZipStorer.ZipFileEntry entry in dir) {
          if (Path.GetFileName(entry.FilenameInZip) == binaryName) {
            zip.ExtractFile(entry, zipPath + binaryName);
            break;
          }
        }
        zip.Close();

        File.Delete(zipPath + zipName);

        ////Create process
        //System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
        ////
        //////strCommand is path and file name of command to run
        //pProcess.StartInfo.FileName = binaryName;
        /////
        ///////strCommandParameters are parameters to pass to program
        //pProcess.StartInfo.Arguments = "";
        /////
        //pProcess.StartInfo.UseShellExecute = false;
        /////
        ///////Set output of program to be written to process output stream
        //pProcess.StartInfo.RedirectStandardOutput = true;   
        /////
        ///////Optional
        //pProcess.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + "/" + zipPath;
        /////
        ///////Start the process
        //pProcess.Start();
        /////
        ///////Get program output
        /////string strOutput = pProcess.StandardOutput.ReadToEnd();
        /////
        ///////Wait for process to finish
        /////pProcess.WaitForExit();
      }

      return "QwE";
    }

    //private String getOperatingSystem() {
    //  if (Path.DirectorySeparatorChar == '\\')
    //    return "Windows";
    //  else if (IsRunningOnMac ())
    //    return "Mac";
    //  else if (Environment.OSVersion.Platform == PlatformID.Unix)
    //    return "X11";
    //  else
    //    return "Other";
    //}

    //[DllImport ("libc")]
    //static extern int uname (IntPtr buf);

    //private bool IsRunningOnMac() {
    //  IntPtr buf = IntPtr.Zero;
    //  try {
    //    buf = Marshal.AllocHGlobal (8192);
    //    if (uname (buf) == 0) {
    //      string os = Marshal.PtrToStringAnsi (buf);
    //      if (os == "Darwin")
    //        return true;
    //    }
    //  } catch {
    //  } finally {
    //    if (buf != IntPtr.Zero)
    //      Marshal.FreeHGlobal (buf);
    //  }
    //  return false;
    //}
//public static Platform RunningPlatform()
//{
//switch (Environment.OSVersion.Platform)
//{
//case PlatformID.Unix:
//// Well, there are chances MacOSX is reported as Unix instead of MacOSX.
//// Instead of platform check, we'll do a feature checks (Mac specific root folders)
//if (Directory.Exists("/Applications")
//& Directory.Exists("/System")
//& Directory.Exists("/Users")
//& Directory.Exists("/Volumes"))
//return Platform.Mac;
//else
//return Platform.Linux;
//
//case PlatformID.MacOSX:
//return Platform.Mac;
//
//default:
//return Platform.Windows;
//}
//}
  }
}
