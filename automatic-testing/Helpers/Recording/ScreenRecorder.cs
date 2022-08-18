using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using ScreenRecorderLib;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace automatic_testing.Helpers.Recording
{
    public class ScreenRecorder
    {
        private readonly Recorder screenCaptureJob = Recorder.CreateRecorder();
        private readonly string OutputDirectoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        /// <summary>
        /// Zapne nahrávání a uloží video
        /// </summary>
        /// <param name="testName">Název souboru po vytvoření</param>
        /// <param name="testModule">Složka pod projektem</param>
        /// <param name="project">Projektová složka</param>
        public void StartRecording(string testName = "Automatický test", string testModule = "Obecné", string? version = null, string project = "Obecné")
        {
            screenCaptureJob.Record(Path.Combine(OutputDirectoryName, "Záznamy", "Videa", project, testModule, GetName(testName, version, DateTime.Now)));
        }
        /// <summary>
        /// Zastaví nahrávání
        /// </summary>
        public void StopRecording()
        {
            Thread.Sleep(1000);
            screenCaptureJob.Stop();
        }
        public void TakeScreenShot(AppiumWebElement driver, string testName = "Automatický test",
                                   string testModule = "Obecné", string? version = null, string project = "Obecné")
        {
            var screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile(Path.Combine(OutputDirectoryName,
                                               "Záznamy",
                                               "Fotky",
                                               project,
                                               testModule,
                                               GetName(testName, version, DateTime.Now)));
        }
        public void TakeScreenShot(WindowsDriver<WindowsElement> driver, string testName = "Automatický test",
                                   string testModule = "Obecné", string? version = null, string project = "Obecné")
        {
            var screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile(Path.Combine(OutputDirectoryName, "Záznamy", "Fotky", project, testModule, GetName(testName, version, DateTime.Now)));
        }
        private string GetName(string name, string version, DateTime dateTime)
        {
            return $"{name} ({version}) {dateTime:g}.mp4".Replace(":", ".");
        }
    }
}