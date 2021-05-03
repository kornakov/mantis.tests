using Bogus;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace mantis.tests
{
    /// <summary>
    /// ����� �� �����������. 
    /// </summary>
    class AuthorizationTests : BasicTests
    {

        /// <summary>
        /// �������� ����� ������ ������. 
        /// </summary>
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("D:\\");
            driver.Url = url;
        }

        
        /// <summary>
        /// ������� ����� ��� ������.
        /// </summary>
        [Test]
        public void EmptyLogin_Fail()
        {
            // ����
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();
            // ����
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();

            // �������� ���������
            var msg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div/div/div/div/div[4]/p"));
            Assert.AreEqual(msg.Text, "��������, ���� ������� ������ �������������, ��� �������� ��������������� ���/������ �����������.");
        }

        /// <summary>
        /// ������� ����� � �������������� ������������� � ������ �������.
        /// </summary>
        [Test]
        public void NonexistenceLoginAndEmptyPass_Fail()
        {
            var text = new Faker().Random.Guid().ToString();

            // ����
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();
            // ���������� ������
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(text);

            // ����
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();          
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();
            
            // ������� ���������
            var msg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div/div/div/div/div[4]/p"));
            Assert.AreEqual(msg.Text, "��������, ���� ������� ������ �������������, ��� �������� ��������������� ���/������ �����������.");
        }

        /// <summary>
        /// ������� ����� � �������������� ������������� � ����� �������.
        /// </summary>
        [Test]
        public void NonexistenceLoginAndRandomPass_Fail()
        {
            var text = new Faker().Random.Guid().ToString();

            // ����
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();

            // ���������� ������
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(text);

            // ����
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();

            // ���� ������
            driver.FindElement(By.XPath(@"//*[@id='password']"))
                  .SendKeys(text);

            // ����
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();

            // �������� ���������
            var msg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div/div/div/div/div[4]/p"));
            Assert.AreEqual(msg.Text, "��������, ���� ������� ������ �������������, ��� �������� ��������������� ���/������ �����������.");
        }

        /// <summary>
        /// ������� ����� � �������� ������������� ��� ������. 
        /// </summary>
        [Test]
        public void RealLoginAndEmptyPass_Fail()
        {
            var text = new Faker().Random.Guid().ToString();

            // ����
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();

            // ���� ������
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(correctLogin);

            // ����
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();

            // �������� ���������
            var msg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div/div/div/div/div[4]/p"));
            Assert.AreEqual(msg.Text, "��������, ���� ������� ������ �������������, ��� �������� ��������������� ���/������ �����������.");
        }

        /// <summary>
        /// ������� ����� � �������� ������������� � ����� �������. 
        /// </summary>
        [Test]
        public void RealLoginAndRandomPass_Fail()
        {
            var text = new Faker().Random.Guid().ToString();

            // ����
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();

            // ���� ������
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(correctLogin);

            // ����
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();

            // ���������� ������
            driver.FindElement(By.XPath(@"//*[@id='password']"))
                  .SendKeys(text);

            // ����
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();

            // �������� ���������
            var msg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div/div/div/div/div[4]/p"));
            Assert.AreEqual(msg.Text, "��������, ���� ������� ������ �������������, ��� �������� ��������������� ���/������ �����������.");
        }

        /// <summary>
        /// ���� ��������� ������������ � ���������� �������. 
        /// </summary>
        [Test]
        public void RealLoginAndRealPass_Success()
        {
            // ����
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();

            // ���������� ���������� �������
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(correctLogin);
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();

            // ���������� ���������� �������
            driver.FindElement(By.XPath(@"//*[@id='password']"))
                  .SendKeys(correctPassword);
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();

            // �������� �����
            var msg = driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/li/a"));
            StringAssert.Contains(correctLogin, msg.Text);
        }

        /// <summary>
        /// �������� ����� �����.
        /// </summary>
        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}