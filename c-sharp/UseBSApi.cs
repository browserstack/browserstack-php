using BrowserStackApi;
using System;

namespace UseBSApi {
  public class MainClass {
    public static void Main () {
      var myInst = new BrowserStackTunnel();
      myInst.addArgs("localIdentifier", "wer");
      myInst.addArgs("qweqwe", "wer");
      myInst.addArgs("onlyAutomate", "true");
      myInst.addArgs("hosts", "localhost,3000,0");
    }
  }
}
