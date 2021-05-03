using OpenQA.Selenium;

namespace mantis.tests
{
    /// <summary>
    /// Базовый класс тестов.
    /// </summary>
    abstract class BasicTests
    {
        protected IWebDriver driver;
        protected const string url = "http://www.mantisbt.org/bugs/my_view_page.php";
        protected const string correctLogin = "tester91";
        protected const string correctPassword = "1qaz2wsx";
    }
}
