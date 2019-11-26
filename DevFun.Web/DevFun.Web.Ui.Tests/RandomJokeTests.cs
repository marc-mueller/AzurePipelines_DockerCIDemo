using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DevFun.Web.Ui.Tests
{
    [TestClass]
    public class RandomJokeTests : WebUITestBase
    {
        [TestMethod]
        public void DevFun_GetNextRandomJoke_ShowNewJoke()
        {
            // Arrange
            var app = Launch();
            var randomJokesPanel = app.NavigateToRandomJokes();

            var oldJoke = randomJokesPanel.GetJokeText();

            // Act
            randomJokesPanel = randomJokesPanel.GotoHome();

            // Assert
            var newJoke = randomJokesPanel.GetJokeText();
            Assert.IsNotNull(newJoke);
            Assert.AreNotEqual(oldJoke, newJoke);
        }

        [TestMethod]
        public void DevFun_GotoAbout_ShowAboutInfo()
        {
            // Arrange
            var app = Launch();
            var randomJokesPanel = app.NavigateToRandomJokes();

            // Act
            var aboutPanel = randomJokesPanel.GotoAbout();

            // Assert
            var text = aboutPanel.GetAboutText();
            Assert.IsNotNull(text);
            Assert.IsFalse(string.IsNullOrWhiteSpace(text));
        }

        [TestMethod]
        public void DevFun_NavigateMultipleTimes_ShowJoke()
        {
            // Arrange
            var app = Launch();
            var randomJokesPanel = app.NavigateToRandomJokes();

            var oldJoke = randomJokesPanel.GetJokeText();

            // Act
            randomJokesPanel = randomJokesPanel
                .GotoHome()
                .GotoAbout()
                .GotoHome()
                .GotoAbout()
                .GotoHome();

            // Assert
            var newJoke = randomJokesPanel.GetJokeText();
            Assert.IsNotNull(newJoke);
            Assert.AreNotEqual(oldJoke, newJoke);
        }
    }
}