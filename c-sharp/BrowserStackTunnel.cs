using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BrowserStackApi {
  public class BrowserStackTunnel {
    String accessKey = "";
    String argumentString = "";
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
  }
}
