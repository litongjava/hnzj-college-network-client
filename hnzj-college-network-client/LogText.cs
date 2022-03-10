using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hnzj_college_network_client {
  public class LogText {
    public static void info(System.Windows.Forms.TextBox box, String str) {
      box.AppendText(str);
      box.AppendText(Environment.NewLine);
      box.ScrollToCaret();
    }
  }
}
