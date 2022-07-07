using Kimbrary.Extensions;

namespace Kimbrary.Logging
{
	public enum LogType
	{
		None,
		Successful,
		Critical,
		Debug,
		Error,
		Information,
		Trace,
		Warning
	}

	public class KLogger
	{
		public string LogFilePath { get; set; } = "";

		public KLogger()
		{

		}

		public KLogger(string logFilePath)
		{
			LogFilePath = logFilePath;
		}

		/// <summary>
		/// Write a new log.
		/// </summary>
		public void DoLog(string log)
		{
			try
			{
				System.IO.File.AppendAllText(LogFilePath, $"[{DateTime.Now} (UTC-TD: {(DateTime.Now - DateTime.UtcNow).Hours})] {log}\n");
			}
			catch (Exception ex)
			{
				throw new Exception($"Error writing the log: {ex.Message}");
			}
		}

		/// <summary>
		/// Write a new log.
		/// </summary>
		public void DoLog(string log, LogType logType)
		{
			string _logType = GetLogTypeAsString(logType);

			try
			{
				if (!string.IsNullOrEmpty(_logType))
				{
					System.IO.File.AppendAllText(LogFilePath, $"[{DateTime.Now} (UTC-TD: {(DateTime.Now - DateTime.UtcNow).Hours}) {_logType}] {log}\n");
				}
				else
				{
					System.IO.File.AppendAllText(LogFilePath, $"[{DateTime.Now} (UTC-TD: {(DateTime.Now - DateTime.UtcNow).Hours})] {log}\n");
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error writing the log: {ex.Message}");
			}
		}

		/// <summary>
		/// Write a new log.
		/// </summary>
		public void DoLog(string log, LogType logType, string logFilePath)
		{
			string _logType = GetLogTypeAsString(logType);

			try
			{
				if (!string.IsNullOrEmpty(_logType))
				{
					System.IO.File.AppendAllText(logFilePath, $"[{DateTime.Now} (UTC-TD: {(DateTime.Now - DateTime.UtcNow).Hours}) {_logType}] {log}\n");
				}
				else
				{
					System.IO.File.AppendAllText(logFilePath, $"[{DateTime.Now} (UTC-TD: {(DateTime.Now - DateTime.UtcNow).Hours})] {log}\n");
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error writing the log: {ex.Message}");
			}
		}

		/// <summary>
		/// Clear the logs file.
		/// </summary>
		public void ClearLogsFile()
		{
			if (System.IO.File.Exists(LogFilePath))
			{
				System.IO.File.WriteAllText(LogFilePath, "");
			}
		}

		/// <summary>
		/// Remove old logs from the logs file.
		/// </summary>
		public void RunGarbageCleaner(int maxAmountOfLogs)
		{
			if (!string.IsNullOrEmpty(LogFilePath))
			{
				try
				{
					string[] oldLogs = System.IO.File.ReadAllLines(LogFilePath);

					if (oldLogs.Length > maxAmountOfLogs)
					{
						string[] newLogs = oldLogs.GetLatestElements(maxAmountOfLogs);

						try
						{
							System.IO.File.WriteAllLines(LogFilePath, newLogs);
						}
						catch
						{
							throw new FormatException("Writing to the logs file failed.");
						}
					}  
				}
				catch
				{
					throw new FormatException("Reading the logs file failed.");
				}
			}
			else
			{
				throw new MissingFieldException("LogFilePath is null or empty!");
			}
		}

		/// <summary>
		/// Remove old logs from the logs file.
		/// </summary>
		public void RunGarbageCleaner(DateTime olderThan)
		{
			if (!string.IsNullOrEmpty(LogFilePath))
			{
				try
				{
					string[] oldLogs = System.IO.File.ReadAllLines(LogFilePath);

					if (oldLogs.Length > 0)
					{
						List<string> newLogs = new();

						foreach (var oldLog in oldLogs)
						{
							try
							{
								DateTime oldLogDateTime = Convert.ToDateTime(oldLog[(oldLog.IndexOf("[") + 1)..(oldLog.IndexOf("(UTC-TD:") - 1)]);

								if (oldLogDateTime > olderThan)
								{
									newLogs.Add(oldLog);
								}
							}
							catch
							{
								throw new FormatException("Log DateTime parsing failed.");
							}
						}

						try
						{
							System.IO.File.WriteAllLines(LogFilePath, newLogs);
						}
						catch
						{
							throw new FormatException("Writing to the logs file failed.");
						}
					}
				}
				catch (Exception ex)
				{
					if (ex is FormatException)
					{
						throw;
					}
					else
					{
						throw new FormatException("Reading the logs file failed.");
					}
				}
			}
			else
			{
				throw new MissingFieldException("LogFilePath is null or empty!");
			}
		}

		private string GetLogTypeAsString(LogType logType)
		{
			switch (logType)
			{
				case LogType.Successful:
					return "SUCCESSFUL";
				case LogType.Critical:
					return "CRITICAL";
				case LogType.Debug:
					return "DEBUG";
				case LogType.Error:
					return "ERROR";
				case LogType.Information:
					return "INFORMATION";
				case LogType.Trace:
					return "TRACE";
				case LogType.Warning:
					return "WARNING";
			}

			return string.Empty;
		}
	}
}
