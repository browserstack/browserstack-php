using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BrowserStackApi
{
  public class BrowserStackTunnel
  {
    private bool binaryOutput = false;
    private string accessKey = "";
    private string argumentString = "";
    private BrowserStackLocal local = null;
    private static KeyValuePair<string, string> emptyStringPair = new KeyValuePair<string, string>();

    private Action callOnConnected = null;

    private static List<KeyValuePair<string, string>> valueCommands = new List<KeyValuePair<string, string>>() {
      new KeyValuePair<string, string>("localidentifier", "-localIdentifier"),
      new KeyValuePair<string, string>("hosts", ""),
      new KeyValuePair<string, string>("proxyhost", "-proxyHost"),
      new KeyValuePair<string, string>("proxyport", "-proxyPort"),
      new KeyValuePair<string, string>("proxyuser", "-proxyUser"),
      new KeyValuePair<string, string>("proxypass", "-proxyPass"),
    };

    private static List<KeyValuePair<string, string>> booleanCommands = new List<KeyValuePair<string, string>>() {
      new KeyValuePair<string, string>("verbose", "-v"),
      new KeyValuePair<string, string>("forcelocal", "-forcelocal"),
      new KeyValuePair<string, string>("onlyautomate", "-onlyAutomate"),
    };
    private void callOnStateChange(LocalState state)
    {
      Console.WriteLine("Current State " + state);
      if(state == LocalState.Connected && callOnConnected != null)
      {
        callOnConnected();
      }
    }

    public BrowserStackTunnel() : this(Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"), null) { }
    public BrowserStackTunnel(Action callOnConnected) : this(
      Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"), callOnConnected) { }
    public BrowserStackTunnel(string BrowserStackKey) : this(BrowserStackKey, null) { }

    public BrowserStackTunnel(string BrowserStackKey, Action callOnConnected)
    {
      this.callOnConnected = callOnConnected;

      if (BrowserStackKey == null || BrowserStackKey.Trim() == "")
      {
        Console.WriteLine("Browserstack Access Key cannot be empty");
        throw new Exception("Auth Error");
      }
      Regex.Replace(BrowserStackKey, @"\s+", "");

      this.accessKey = BrowserStackKey;
    }

    public void addArgs(string key, string value)
    {
      KeyValuePair<string, string> result;
      key = key.Trim().ToLower();

      result = valueCommands.Find(pair => pair.Key == key);
      if (!result.Equals(emptyStringPair))
      {
        this.argumentString += result.Value + " " + value.Trim().ToLower() + " ";
      }
      result = booleanCommands.Find(pair => pair.Key == key);
      if (!result.Equals(emptyStringPair))
      {
        if (value.Trim().ToLower() == "true")
        {
          this.argumentString += result.Value + " ";
        }
      }
    }

    public void addArgs(string key, bool value)
    {
      KeyValuePair<string, string> result;
      key = key.Trim().ToLower();

      result = booleanCommands.Find(pair => pair.Key == key);
      if (!result.Equals(emptyStringPair))
      {
        if (value == true)
        {
          this.argumentString += result.Value + " ";
        }
      }
    }

    public void logBinaryOutput()
    {
      this.binaryOutput = true;
    }

    public void start()
    {
      this.local = new BrowserStackLocal(accessKey + " " + argumentString);
      this.local.Run(callOnStateChange);
    }

    public void start(string binaryPath)
    {
      this.local = new BrowserStackLocal(binaryPath, accessKey + " " + argumentString);
      this.local.Run(callOnStateChange);
    }

    public void stop()
    {
      if (this.local != null) { 
        this.local.Kill();
      }
    }
  }
}