#nullable enable
using ScreenRecorderLib;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace automatic_testing.Helpers.Recording
{
    public class ScreenRecorder
    {
        private readonly Recorder _screenCaptureJob = Recorder.CreateRecorder();
        private readonly string _outputDirectoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

        /// <summary>
        /// Zapne nahrávání a uloží video do definovaného souboru
        /// </summary>
        /// <param name="testName">Název testu, který provádíme (podle toho se pojmenuje soubor)</param>
        /// <param name="testModule">Složka druhé úrovně. Označuje modul, který se testuje</param>
        /// <param name="version">Do jména souboru vloží verzi programu</param>
        /// <param name="project">Hlavní složka</param>
        public void StartRecording(string testName = "Automatický test", string testModule = "Obecné", string? version = null, string project = "Obecné")
        {
                _screenCaptureJob.Record(Path.Combine(_outputDirectoryName, "Záznamy", "Videa", project, testModule,
                    GetName(testName, string.IsNullOrEmpty(version) ? "" : version)));
        }
        /// <summary>
        /// Zastaví nahrávání
        /// </summary>
        public void StopRecording()
        {
            //TODO: časem přidat parametr delay, který bude oddalovat konec nahrávání
            Thread.Sleep(1000);
            _screenCaptureJob.Stop();
        }
        /*public void TakeScreenShot(AppiumWebElement driver, string testName = "Automatický test",
                                   string testModule = "Obecné", string? version = null, string project = "Obecné")
        {
            var screenshot = driver.GetScreenshot();
            if (version != null)
                screenshot.SaveAsFile(Path.Combine(_outputDirectoryName,
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
            if (version != null)
                screenshot.SaveAsFile(Path.Combine(_outputDirectoryName, "Záznamy", "Fotky", project, testModule,
                    GetName(testName, version, DateTime.Now)));
        }*/
        private string GetName(string name, string version)
        {
            var dateTime = DateTime.Now;
            var stringDate = dateTime.ToString("g");
            return $"{name} ({version}) {stringDate}.mp4".Replace(":", ".");
        }
    }
}