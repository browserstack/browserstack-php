using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BrowserStackApi
{
  public class BrowserStackTunnel
  {
    string accessKey = "";
    string argumentString = "";
    BrowserStackLocal local = null;
    KeyValuePair<string, string> emptyStringPair = new KeyValuePair<string, string>();

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

    public BrowserStackTunnel() : this(Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"))
    {
    }

    public BrowserStackTunnel(string BrowserStackKey)
    {
      if (BrowserStackKey == null || BrowserStackKey.Trim() == "")
      {
        Console.WriteLine("Browserstack Access Key cannot be empty");
        throw new Exception("Auth Error");
      }
      Regex.Replace(BrowserStackKey, @"\s+", "");

      accessKey = BrowserStackKey;
    }

    public void addArgs(string key, string value)
    {
      KeyValuePair<string, string> result;
      key = key.Trim().ToLower();

      result = valueCommands.Find(pair => pair.Key == key);
      if (!result.Equals(emptyStringPair))
      {
        argumentString += result.Value + " " + value.Trim().ToLower() + " ";
      }
      result = booleanCommands.Find(pair => pair.Key == key);
      if (!result.Equals(emptyStringPair))
      {
        if (value.Trim().ToLower() == "true")
        {
          argumentString += result.Value;
        }
      }
    }

    public void start()
    {
      this.local = new BrowserStackLocal(accessKey + " " + argumentString);
      local.Run();
    }

    public void stop()
    {
      if (local != null) { 
        local.Kill();
      }
    }
  }
}