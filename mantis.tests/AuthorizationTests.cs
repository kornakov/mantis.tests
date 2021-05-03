using Bogus;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace mantis.tests
{
    /// <summary>
    /// Тесты на авторизацию. 
    /// </summary>
    class AuthorizationTests : BasicTests
    {

        /// <summary>
        /// Действия перед каждым тестом. 
        /// </summary>
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("D:\\");
            driver.Url = url;
        }

        
        /// <summary>
        /// Попытка входа без логина.
        /// </summary>
        [Test]
        public void EmptyLogin_Fail()
        {
            // Вход
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();
            // Вход
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();

            // Проверка сообщения
            var msg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div/div/div/div/div[4]/p"));
            Assert.AreEqual(msg.Text, "Возможно, ваша учётная запись заблокирована, или введённое регистрационное имя/пароль неправильны.");
        }

        /// <summary>
        /// Попытка входа с несуществующим пользователем и пустым паролем.
        /// </summary>
        [Test]
        public void NonexistenceLoginAndEmptyPass_Fail()
        {
            var text = new Faker().Random.Guid().ToString();

            // Вход
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();
            // Заполнение логина
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(text);

            // Вход
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();          
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();
            
            // Проерка сообщения
            var msg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div/div/div/div/div[4]/p"));
            Assert.AreEqual(msg.Text, "Возможно, ваша учётная запись заблокирована, или введённое регистрационное имя/пароль неправильны.");
        }

        /// <summary>
        /// Попытка входа с несуществующим пользователем и любым паролем.
        /// </summary>
        [Test]
        public void NonexistenceLoginAndRandomPass_Fail()
        {
            var text = new Faker().Random.Guid().ToString();

            // Вход
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();

            // Заполнение логина
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(text);

            // Вход
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();

            // Ввод пароля
            driver.FindElement(By.XPath(@"//*[@id='password']"))
                  .SendKeys(text);

            // Вход
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();

            // Проверка сообщения
            var msg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div/div/div/div/div[4]/p"));
            Assert.AreEqual(msg.Text, "Возможно, ваша учётная запись заблокирована, или введённое регистрационное имя/пароль неправильны.");
        }

        /// <summary>
        /// Попытка входа с реальным пользователем без пароля. 
        /// </summary>
        [Test]
        public void RealLoginAndEmptyPass_Fail()
        {
            var text = new Faker().Random.Guid().ToString();

            // Вход
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();

            // Ввод логина
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(correctLogin);

            // Вход
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();

            // Проверка сообщения
            var msg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div/div/div/div/div[4]/p"));
            Assert.AreEqual(msg.Text, "Возможно, ваша учётная запись заблокирована, или введённое регистрационное имя/пароль неправильны.");
        }

        /// <summary>
        /// Попытка входа с реальным пользователем и любым паролем. 
        /// </summary>
        [Test]
        public void RealLoginAndRandomPass_Fail()
        {
            var text = new Faker().Random.Guid().ToString();

            // Вход
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();

            // Ввод логина
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(correctLogin);

            // Вход
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();

            // Заполнение пароля
            driver.FindElement(By.XPath(@"//*[@id='password']"))
                  .SendKeys(text);

            // Вход
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();

            // Проверка сообщения
            var msg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div/div/div/div/div[4]/p"));
            Assert.AreEqual(msg.Text, "Возможно, ваша учётная запись заблокирована, или введённое регистрационное имя/пароль неправильны.");
        }

        /// <summary>
        /// Вход реального пользователя с корректным паролем. 
        /// </summary>
        [Test]
        public void RealLoginAndRealPass_Success()
        {
            // Вход
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();

            // Заполнение корректным логином
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(correctLogin);
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();

            // Заполнение корректным паролем
            driver.FindElement(By.XPath(@"//*[@id='password']"))
                  .SendKeys(correctPassword);
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();

            // Проверка входа
            var msg = driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/li/a"));
            StringAssert.Contains(correctLogin, msg.Text);
        }

        /// <summary>
        /// Действие после теста.
        /// </summary>
        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}