using hnzj_college_network_client.helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hnzj_college_network_client {
  public partial class mainFrm : Form {
    private IniFileHelper iniFileHelper = new IniFileHelper();
    public mainFrm() {
      InitializeComponent();
      initizlizeApp();
    }

    //初始化App
    private void initizlizeApp() {
      //从本地文件中恢复用户名和密码
      restoreConfig();
      GetLocalIp();
      generatorLoginUrl();

    }

    private void textBox1_TextChanged(object sender, EventArgs e) {

    }

    private void button1_Click(object sender, EventArgs e) {
      generatorLoginUrl();
    }

    private void generatorLoginUrl() {
      
      //用户名
      string username = textBox1.Text;
      //密码
      string upass = textBox2.Text;
      //将用户名和密码保存到本地文件
      saveUsernameAndPassword(username, upass);

      string ip = textBox4.Text;
      if ("".Equals(ip)) {
        MessageBox.Show("ip地址不能为空", "提示", MessageBoxButtons.OK);
        return;
      }

      string strTemplet = ",0,{0}@{1}";
      string suffix = null;
      if (checkBox1.Checked) {
        suffix = "cmcc";
      } else if (checkBox2.Checked) {
        suffix = "unicom";
      } else if (checkBox3.Checked) {
        suffix = "telecom";
      } else {
        suffix = "cmcc";
      }

      string ddddd = string.Format(strTemplet, username, suffix);

      string loginUrlTemplet = "http://172.16.1.38:801/eportal/?c=ACSetting&a=Login&loginMethod=1&protocol=http%3A&hostname=172.16.1.38" +
        "&port=&iTermType=1&wlanuserip={0}&wlanacip=null&wlanacname=null&redirect=null&session=null&vlanid=0&mac=00-00-00-00-00-00" +
        "&ip={1}&enAdvert=0&jsVersion=2.4.3&DDDDD={2}&upass={3}&R1=0&R2=0&R3=0&R6=0&para=00&0MKKey=123456" +
        "&buttonClicked=&redirect_url=&err_flag=&username=&password=&user=&cmd=&Login=&v6ip=";
      textBox3.Text = string.Format(loginUrlTemplet, ip, ip, ddddd, upass);
    }

    private void button2_Click(object sender, EventArgs e) {
      textBox3.Text = "http://172.16.1.38/a70.htm";
    }

    //获取本机ip
    private void button4_Click(object sender, EventArgs e) {
      //清除textBox内容
      textBox3.Text = string.Empty;
      GetLocalIp();
    }
    public string GetLocalIp() {
      ///获取本地的IP地址
      string hostname =Dns.GetHostName();

      IPHostEntry hostEntry = Dns.GetHostEntry(hostname);
      
      string AddressIP = string.Empty;
      foreach (IPAddress iPAddress in hostEntry.AddressList) {
        LogText.info(textBox3, iPAddress.AddressFamily.ToString() + "\t" + iPAddress.ToString());
      }
      
      textBox4.Text=hostEntry.AddressList[hostEntry.AddressList.Length - 1].ToString();
      return AddressIP;
    }

    private void button5_Click(object sender, EventArgs e) {
      textBox3.Text = "";
    }

    private void button3_Click(object sender, EventArgs e) {
      string logoutUrlTemplet = "http://172.16.1.38:801/eportal/?c=ACSetting&a=Logout&loginMethod=1&protocol=http%3A&hostname=172.16.1.38" +
        "&port=&iTermType=1&wlanuserip=10.1.53.240&wlanacip=172.20.1.1&wlanacname=huawei-me60&redirect=null&session=null" +
        "&vlanid=null%3Cinput+type%3D&ip=10.1.53.240&queryACIP=0&jsVersion=2.4.3";

      string ip = textBox4.Text;
      if ("".Equals(ip)) {
        MessageBox.Show("ip地址不能为空", "提示", MessageBoxButtons.OK);
        return;
      }
      textBox3.Text=string.Format(logoutUrlTemplet, ip, ip);
    }

    private void label2_Click(object sender, EventArgs e) {

    }

    private void checkBox3_CheckedChanged(object sender, EventArgs e) {

    }

    /// <summary>
    /// 将用户名和密码保存到本地文件
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    private void saveUsernameAndPassword(string username, string password) {
      iniFileHelper.WriteIniString(Constants.SECTION_NAME, Constants.username, username);
      iniFileHelper.WriteIniString(Constants.SECTION_NAME, Constants.password, password);
    }

    private void restoreConfig() {
      StringBuilder username = new StringBuilder(60);
      iniFileHelper.GetIniString(Constants.SECTION_NAME, Constants.username, "", username, username.Capacity);
      textBox1.Text = username.ToString();

      StringBuilder password = new StringBuilder(60);
      iniFileHelper.GetIniString(Constants.SECTION_NAME, Constants.password, "", password, password.Capacity);
      textBox2.Text = password.ToString() ;
    }
  }
}
