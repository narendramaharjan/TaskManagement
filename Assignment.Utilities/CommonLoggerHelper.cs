using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Utilities
{
    public class CommonLoggerHelper
    {
        private static readonly object Locker = new object();

        /// <summary>
        /// Log Exception
        /// </summary>
        /// <param name="ex"></param>
        public static void LogException(Exception ex)
        {
            try
            {
                lock (Locker)
                {
                    var logFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/");
                    logFolderPath = logFolderPath + "Log";
                    if (!Directory.Exists(logFolderPath))
                    {
                        Directory.CreateDirectory(logFolderPath);
                    }

                    var path = Path.Combine(logFolderPath, "Log" + "-" + DateTime.Today.ToString("d-MM-yyyy") + ".log");
                    if (!File.Exists(path))
                    {
                        File.Create(path).Close();
                    }

                    using (var writer = new StreamWriter(path, true))
                    {
                        var type = ex.GetType();
                        var builder = new StringBuilder();
                        builder.AppendLine("------------------------------------");
                        builder.AppendLine($"Timestamp: {DateTime.Now}");
                        builder.AppendLine($"An exception of type \"{type.FullName}\" occured and was caught.");
                        builder.AppendLine("------------------------------------");
                        builder.AppendLine();

                        var errCode = string.Empty;
                        builder.AppendLine($"Type: {type.AssemblyQualifiedName}");
                        if (!string.IsNullOrEmpty(errCode))
                        {
                            builder.AppendLine($"Error Code: {errCode}");
                        }

                        if (ex.InnerException != null)
                        {
                            builder.AppendLine($"Inner Exception: {ex.InnerException}");
                        }

                        builder.AppendLine($"Message: {ex.Message}");
                        builder.AppendLine($"Source: {ex.Source}");
                        builder.AppendLine($"Help Link: {ex.HelpLink}");
                        builder.AppendLine($"Target Site: {ex.TargetSite}");
                        builder.AppendLine($"Stack Trace: {ex.StackTrace}");
                        builder.AppendLine();

                        writer.WriteLine(builder.ToString());
                        writer.Close();
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static void LogMessage(string message)
        {
            try
            {
                lock (Locker)
                {
                    //var date = DateTime.Today.ToString("d-MM-yyyy");
                    var logFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/");
                    logFolderPath = logFolderPath + "Log";
                    if (!Directory.Exists(logFolderPath))
                    {
                        Directory.CreateDirectory(logFolderPath);
                    }
                    var path = Path.Combine(logFolderPath, "LogMessage" + "-" + DateTime.Today.ToString("d-MM-yyyy") + ".log");
                    if (!File.Exists(path))
                    {
                        File.Create(path).Close();
                    }
                    //File.AppendAllText(path, message + dateTime + Environment.NewLine);
                    using (var writer = new StreamWriter(path, true))
                    {
                        var builder = new StringBuilder();
                        builder.AppendLine("------------------------------------");
                        builder.AppendLine(string.Format("Timestamp: {0}", DateTime.Now));
                        builder.AppendLine("------------------------------------");
                        builder.AppendLine();

                        builder.AppendLine(string.Format("Message: {0}", message));
                        builder.AppendLine();

                        writer.WriteLine(builder.ToString());
                        writer.Close();
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
