using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Chrome;


namespace TmallPOC
{
    class Program
    {
        IWebDriver driver;
        static void Main(string[] args)
        {
            Program program = new Program();
            program.automateLinkdin();
            // Console.ReadLine();  //remove this in prod
        }

        public void automateLinkdin()
        {
            try
            {
                using (StreamWriter write = File.CreateText("E:\\Linkindin-auto\\Linkedin-jobs\\CrawlTmall.csv"))
                {
                    
                    using (var driver = new ChromeDriver((@"E:\Linkindin-auto\Linkedin-jobs\chromedriver.exe")))
                    {
                        write.WriteLine("ProductID" + "," + "Price" + "," + "Title" + "," + "Stats");
                        this.driver = driver;
                      //  driver.Manage();
                        driver.Manage().Window.Maximize();
                        //driver.Navigate().GoToUrl("https://login.taobao.com/member/login.jhtml?tpl_redirect_url=https%3A%2F%2Fwww.tmall.com%2F&style=miniall&enup=true&newMini2=true&full_redirect=true&sub=true&from=tmall&allp=assets_css%3D3.0.10/login_pc.css&pms=1624260698441");
                        Thread.Sleep(1000);
                        driver.Navigate().GoToUrl("https://www.google.com/");
                        //driver.Manage().Cookies.DeleteAllCookies();
                        //Thread.Sleep(5000);
                       // driver.SwitchTo().Frame("J_loginIframe");
                        Thread.Sleep(5000);
                        driver.FindElement(By.XPath("/html/body/div[1]/div[3]/form/div[1]/div[1]/div[1]/div/div[2]/input")).SendKeys("linkedIn login" + Keys.Enter);
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("//*[@id=\"rso\"]/div[1]/div/div/div[1]/div/a/h3")).Click();

                        driver.FindElement(By.XPath("//*[@id=\"username\"]")).SendKeys("xx");
                        driver.FindElement(By.XPath("//*[@id=\"password\"]")).SendKeys("xxxx" +Keys.Enter);
                        Thread.Sleep(3000);
                        driver.Url = "https://www.linkedin.com/jobs/search/?currentJobId=3507449664&distance=&f_AL=true&f_BE=&f_C=&f_CF=&f_CM=&f_CR=&f_CT=&f_E=2%2C3&f_EA=&f_EL=&f_ES=&f_ET=&f_F=&f_FCE=&f_GC=&f_I=&f_JC=&f_JIYN=&f_JT=F%2CC&f_LF=&f_PP=&f_SB=&f_SB2=&f_SB3=&f_T=&f_TP=&f_TPR=r86400&f_WRA=&f_WT=&geoId=103644278&keywords=data%20scientist&latLong=&location=United%20States&originToLandingJobPostings=&refresh=true&sortBy=&start=25";
                        driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/form/div[4]/button")).Click();
                        //job id is the param to link easy apply
                        Thread.Sleep(4000);
                        driver.SwitchTo().ActiveElement();
                        //mimic normal user login
                        driver.FindElement(By.XPath("//html/body/div[1]/div[2]/div/div/div/div[2]/form/fieldset/div/div/div/input")).SendKeys("nike" + Keys.Enter);
                        driver.FindElement(By.XPath("//*[@id=\"rso\"]/div[1]/div/div/div[1]/div/a/h3")).Click();

                        for (int pg = 1; pg < 85; pg++)
                        {
                            try
                            {
                                // Thread.Sleep(2000); uncomment 4
                                //  string currentPage = driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/div/div[4]/p/b[1]")).Text;
                                if (pg == 1)
                                {


                                    Thread.Sleep(2000);
                                    WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
// waitForElement.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div[3]/div/div[7]/div")));
                                    List<IWebElement> TmallProductUrl = driver.FindElements(By.XPath("/html/body/div[1]/div/div[3]/div/div[7]/div")).ToList();
                                    List<IWebElement> Price = driver.FindElements(By.XPath("//html/body/div[1]/div/div[3]/div/div[7]//div/p[1]/em")).ToList();
                                    List<IWebElement> Title = driver.FindElements(By.XPath($"//*[@id=\"J_ItemList\"]//div/p[2]/a")).ToList();
                                    List<IWebElement> stats = driver.FindElements(By.XPath("/html/body/div[1]/div/div[3]/div/div[7]//div/p[3]")).ToList();

                                    for (int i = 0; i < TmallProductUrl.Count(); i++)
                                    {
                                        string attrValue = TmallProductUrl[i].GetAttribute("data-id");
                                        string ProdPrice = Price[i].Text;
                                        string ProdTitle = Title[i].Text;
                                        string ProdStats = stats[i].Text;
                                        Console.WriteLine("Value of Product id :" + attrValue + "\t" + "product price" + ProdPrice + "\t" + ProdStats + "\t" + ProdTitle);
                                        write.WriteLine(attrValue + "," + ProdPrice + "," + ProdTitle + "," + ProdStats);
                                    }
                                    Console.WriteLine("+================Boom===================page=+" + pg);
                                }
                                else
                                {
                                    //Thread.Sleep(2000); //iuncoment increase time3
                                    //string PgCheckpointAnchor = currentPage.Split('/')[0].ToString();
                                    //int test = Int16.Parse(PgCheckpointAnchor); uncomment 5
                                    if (pg == 1)
                                    {
                                        driver.FindElement(By.XPath("//html/body/div[1]/div/div[3]/div/div[4]/p/a")).Click();
                                        Thread.Sleep(2000);
                                        BotSlider();
                                    }
                                    else
                                    {
                                        driver.FindElement(By.XPath("//html/body/div[1]/div/div[3]/div/div[4]/p/a[2]")).Click();
                                        Thread.Sleep(2000);
                                    }
                                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                                    js.ExecuteScript("window.scrollBy(0,1000)");
                                    //try //uncomment 1
                                    //{
                                    //    bool captcha2 = driver.FindElement(By.ClassName("baxia-dialog-close")).Displayed;
                                    //    if (captcha2 == true)
                                    //    {
                                    //        Thread.Sleep(1000);
                                    //        driver.SwitchTo().Frame("baxia-dialog-content");
                                    //        IWebElement slider = driver.FindElement(By.XPath("//span[@class='nc_iconfont btn_slide']"));
                                    //        //slider.Click();
                                    //        Actions actions = new Actions(driver);
                                    //        actions.DragAndDropToOffset(slider, offsetX: 777, offsetY: 470).Release().Build().Perform();
                                    //        Thread.Sleep(1000);
                                    //        Console.WriteLine("Captcha bypassed successfully :D");
                                    //        driver.SwitchTo().ParentFrame();
                                    //    }//working but need to slide
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    Console.WriteLine("Bot is happy ,No Captcha detected");

                                    //}

                                    WebDriverWait waitForElement2 = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
// waitForElement2.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div[3]/div/div[7]/div")));
                                    List<IWebElement> TmallProductUrl2 = driver.FindElements(By.XPath("/html/body/div[1]/div/div[3]/div/div[7]/div")).ToList();
                                    List<IWebElement> Price = driver.FindElements(By.XPath("//html/body/div[1]/div/div[3]/div/div[7]//div/p[1]/em")).ToList();
                                    List<IWebElement> Title = driver.FindElements(By.XPath($"//*[@id=\"J_ItemList\"]//div/p[2]/a")).ToList();
                                    List<IWebElement> stats = driver.FindElements(By.XPath("/html/body/div[1]/div/div[3]/div/div[7]//div/p[3]")).ToList();

                                    for (int i = 0; i < TmallProductUrl2.Count(); i++)
                                    {
                                        string attrValue2 = TmallProductUrl2[i].GetAttribute("data-id");
                                        string ProdPrice = Price[i].Text;
                                        string ProdTitle = Title[i].Text;
                                        string ProdStats = stats[i].Text;
                                        Console.WriteLine("Value of Product id :" + attrValue2 + "\t" + "product price" + ProdPrice + "\t" + ProdStats + "\t" + ProdTitle);
                                        write.WriteLine(attrValue2 + "," + ProdPrice + "," + ProdTitle + "," + ProdStats);

                                    }

                                    Console.WriteLine("+================Boom=====================pg+" + pg);
                                    TmallProductUrl2.Clear();
                                    Price.Clear();//clearing memory
                                    Title.Clear();
                                    stats.Clear();
                                    // Thread.Sleep(2000); //uncomment 2
                                    js.ExecuteScript("window.scrollBy(0,1500)");
                                    // Thread.Sleep(3000);//todo:make thread sleep different
                                    //  Console.WriteLine($"++++++++++Current_tmall_page: {currentPage}+++++++++++++++");
                                }
                            }
                            catch (Exception ex)
                            {
                                //_Todo_IFpage_breakycaptach_recrawlit -high
                                Console.WriteLine("Opss " + ex.Message);
                                BotSlider();
                                //pg--; //making sure bot missed crawled
                                //_T-mallProductUrl2.Clear();
                            }


                        }



                        // driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/div/div[7]/div[1]/div/div[1]/a")).Click();

                        //// Actions actions = new Actions(driver);
                        // // driver.Url = "http://www4.tjrj.jus.br/consultaProcessoWebV2/consultaMov.do?v=2&numProcesso=2007.001.179581-8&acessoIP=internet&tipoUsuario=";
                        // SelectElement test = new SelectElement(driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div[2]/div[2]/div[1]/select")));
                        // test.SelectByIndex(1);        //WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        //                               //waitForElement.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[2]/div[4]/div[2]/div/section/div[2]/div[5]/div[2]/div[1]/div/div[1]/div[2]/div[2]/h1")));
                        // string a = driver.FindElementByXPath("/html/body/div[3]/div/div/div/div[2]/h3").Text;
                        // string b = driver.FindElementByXPath("/html/body/div[3]/div/div/div/div[2]/ul[2]/li/h3/span").Text;
                        // var quantity = test.WrappedElement.Text;
                        // string trick = quantity.Replace("--- Please Select ---", "").Replace("\t", "");
                        // trick.Split();
                        Console.ReadLine();

                    }
                }

                //todo high important-bot failed case should logout

                // string logout = "/html/body/div[1]/div[1]/div[2]/div/p/span[4]/a"
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at " + ex.Message);

            }

        }

        public void BotSlider()
        {
            //TODO:add checkpoint if the ement present -medium
            //one more captch to solve
            try
            {
                Thread.Sleep(2000);//key area
                                   // IWebElement myLink = driver.f(By.Id("myId"));


                {
                    bool captcha = driver.FindElement(By.XPath("//html/body/div/div[2]/div/div[1]/div[2]/center/div[1]/div/div[1]/span")).Displayed;
                    if (captcha == true)
                    {
                        IWebElement slider = driver.FindElement(By.XPath("//html/body/div/div[2]/div/div[1]/div[2]/center/div[1]/div/div[1]/span"));
                        Actions actions = new Actions(driver);
                        actions.DragAndDropToOffset(slider, offsetX: 1066, offsetY: 365).Release().Build().Perform();
                    }
                }


            }
            catch (Exception ex)
            {
                try
                {

                    var element = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[1]/div[2]/center/div[1]/div/span/a"));
                    Console.WriteLine("issue at captcha refresh");
                    if (element != null && element.Displayed)
                    {
                        driver.Navigate().Refresh();
                        Thread.Sleep(1000);
                        IWebElement slider = driver.FindElement(By.XPath("//html/body/div/div[2]/div/div[1]/div[2]/center/div[1]/div/div[1]/span"));
                        Actions actions = new Actions(driver);
                        actions.DragAndDropToOffset(slider, offsetX: 1066, offsetY: 365).Release().Build().Perform();
                    }
                }
                catch (Exception ex2)
                {

                    Console.WriteLine("Bot got error at 2nd captcha:" + ex2.Message);
                }

            }

            //slider.Click();

        }


    }
}
