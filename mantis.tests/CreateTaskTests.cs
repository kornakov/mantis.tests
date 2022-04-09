using Bogus;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace mantis.tests
{
    /// <summary>
    /// Тесты на создание задачи. 
    /// </summary>
    [AllureNUnit]
    [TestFixture]
    class CreateTaskTests : BasicTests
    {
        /// <summary>
        /// Действия перед каждым тестом. 
        /// </summary>
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("D:\\");
            driver.Url = url;
            driver.FindElement(By.XPath(@"//*[@id='breadcrumbs']/ul/div/a[1]"))
                  .Click();
            driver.FindElement(By.XPath(@"//*[@id='username']"))
                  .SendKeys(correctLogin);
            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[2]"))
                  .Click();

            driver.FindElement(By.XPath(@"//*[@id='password']"))
                  .SendKeys(correctPassword);

            driver.FindElement(By.XPath(@"//*[@id='login-form']/fieldset/input[3]"))
                  .Click();

            // Создать задачу
            driver.FindElement(By.XPath(@"//*[@id='navbar-container']/div[2]/ul/li[1]/div/a"))
                  .Click();

            // Выбрать проект
            driver.FindElement(By.XPath(@"//*[@id='select-project-form']/div/div[2]/div[2]/input"))
                  .Click();
        }

        [Test]
        public void CreateTask_FillRequiredFields_Success()
        {
            // Выбрать категорию
            var webElement = driver.FindElement(By.XPath(@"//*[@id='category_id']"));
            var select = new SelectElement(webElement);
            select.SelectByIndex(1);

            // Заполнить тему
            var theme = new Faker().Random.Words();
            driver.FindElement(By.XPath(@"//*[@id='summary']")).SendKeys(theme);

            // Заполнить описание
            var desctription = new Faker().Random.Words();
            driver.FindElement(By.XPath(@"//*[@id='description']")).SendKeys(desctription);

            // Создать задачу
            driver.FindElement(By.XPath(@"//*[@id='report_bug_form']/div/div[2]/div[2]/input")).Click();
            Thread.Sleep(2000);

            // Проверить наличие элементов
            var table = driver.FindElement(By.XPath(@"//*[@id='main-container']/div[2]/div[2]/div/div[1]/div/div[2]/div[2]/div/table/tbody"));
            StringAssert.Contains(correctLogin, table.Text);
            StringAssert.Contains(theme, table.Text);
            StringAssert.Contains(desctription, table.Text);
        }

        [Test]
        public void CreateTask_FillAllFields_Success()
        {
            // Выбрать категорию
            var webElement = driver.FindElement(By.XPath(@"//*[@id='category_id']"));
            var select = new SelectElement(webElement);
            select.SelectByIndex(1);

            // Заполнить тему
            var summary = new Faker().Random.Words();
            driver.FindElement(By.XPath(@"//*[@id='summary']")).SendKeys(summary);

            // Заполнить описание
            var desctription = new Faker().Random.Words();
            driver.FindElement(By.XPath(@"//*[@id='description']")).SendKeys(desctription);

            // Заполнить шаги
            var steps = new Faker().Random.Words();
            driver.FindElement(By.XPath(@"//*[@id='steps_to_reproduce']")).SendKeys(steps);

            // Заполнить дополнительную информацию
            var addInfo = new Faker().Random.Words();
            driver.FindElement(By.XPath(@"//*[@id='additional_info']")).SendKeys(addInfo);

            // Создать задачу
            driver.FindElement(By.XPath(@"//*[@id='report_bug_form']/div/div[2]/div[2]/input")).Click();
            Thread.Sleep(2000);

            // Проверить наличие элементов
            var table = driver.FindElement(By.XPath(@"//*[@id='main-container']/div[2]/div[2]/div/div[1]/div/div[2]/div[2]/div/table/tbody"));
            StringAssert.Contains(correctLogin, table.Text);
            StringAssert.Contains(summary, table.Text);
            StringAssert.Contains(desctription, table.Text);
            StringAssert.Contains(steps, table.Text);
            StringAssert.Contains(addInfo, table.Text);
        }

        [Test]
        public void CreateTask_WithoutRequiredFields_Fail([Values("summary", "description")] string field)
        {
            // Выбрать категорию
            var webElement = driver.FindElement(By.XPath(@"//*[@id='category_id']"));
            var select = new SelectElement(webElement);
            select.SelectByIndex(1);

            // Заполнить тему
            var msg = new Faker().Random.Words();
            driver.FindElement(By.XPath($@"//*[@id='{field}']")).SendKeys(msg);

            // Создать задачу
            driver.FindElement(By.XPath(@"//*[@id='report_bug_form']/div/div[2]/div[2]/input")).Click();
            Thread.Sleep(2000);

            try
            {
                driver.FindElement(By.XPath(@"//*[@id='main-container']/div[2]/div[2]/div/div[1]/div/div[2]/div[2]/div/table/tbody"));
            }

            catch (NoSuchElementException ex)
            {
                StringAssert.Contains("no such element: Unable to locate element", ex.Message);
            }
        }

        [Test]
        public void CreateTask_WithoutSelectСategory_Fail()
        {
            // Заполнить тему
            var summary = new Faker().Random.Words();
            driver.FindElement(By.XPath($@"//*[@id='summary']")).SendKeys(summary);

            // Заполнить описание
            var description = new Faker().Random.Words();
            driver.FindElement(By.XPath($@"//*[@id='description']")).SendKeys(description);

            // Создать задачу
            driver.FindElement(By.XPath(@"//*[@id='report_bug_form']/div/div[2]/div[2]/input")).Click();
            Thread.Sleep(1500);
            var errorMsg = driver.FindElement(By.XPath(@"//*[@id='main-container']/div[2]/div[2]/div/div/div[2]/p[2]"));
            StringAssert.Contains(
                "Обязательное поле 'category' не заполнено. Пожалуйста, проверьте правильность заполнения.",
                errorMsg.Text);
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
