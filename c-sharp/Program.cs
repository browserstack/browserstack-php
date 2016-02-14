using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserStackApi;

namespace ConsoleApplication1
{
  class Program
  {
    static void Main(string[] args)
    {
      var myInst = new BrowserStackTunnel("sUiJatw6NhJZpsttcY35");
      myInst.addArgs("localIdentifier", "wer");
      myInst.addArgs("qweqwe", "wer");
      myInst.addArgs("onlyAutomate", "true");
      myInst.addArgs("forcelocal", true);
      myInst.addArgs("hosts", "localhost,3000,0");
      myInst.addArgs("verbose", true);
      myInst.start("C:\\Users\\Admin\\Desktop\\");
      Console.ReadLine();
      myInst.stop();
      Console.ReadLine();
    }
  }
}
