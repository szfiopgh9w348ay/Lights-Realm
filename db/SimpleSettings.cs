using log4net;
using System;
using System.Collections.Generic;
using System.IO;

namespace db
{
    public class SimpleSettings : IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SimpleSettings));

        private readonly string cfgFile;
        private readonly string id;
        private readonly Dictionary<string, string> values;

        public SimpleSettings(string id)
        {
            logger.Info($"Loading settings for \"{id}\"...");

            values = new Dictionary<string, string>();
            this.id = id;
            cfgFile = Path.Combine(Environment.CurrentDirectory, id + ".cfg");
            if (File.Exists(cfgFile))
                using (StreamReader rdr = new StreamReader(File.OpenRead(cfgFile)))
                {
                    string line;
                    int lineNum = 1;
                    while ((line = rdr.ReadLine()) != null)
                    {
                        if (line.StartsWith("#")) continue;
                        int i = line.IndexOf(":");
                        if (i == -1)
                        {
                            logger.Info($"Invalid settings at line {lineNum}");
                            throw new ArgumentException("Invalid settings.");
                        }
                        string val = line.Substring(i + 1);

                        values.Add(line.Substring(0, i),
                            val.Equals("null", StringComparison.InvariantCultureIgnoreCase) ? null : val);
                        lineNum++;
                    }
                    logger.Info("Settings loaded.");
                }
            else
                logger.Info("Settings not found.");
        }

        public void Reload()
        {
            logger.Info($"Reloading settings for \"{id}\"...");
            values.Clear();
            if (File.Exists(cfgFile))
                using (StreamReader rdr = new StreamReader(File.OpenRead(cfgFile)))
                {
                    string line;
                    int lineNum = 1;
                    while ((line = rdr.ReadLine()) != null)
                    {
                        if (line.StartsWith("#")) continue;
                        int i = line.IndexOf(":");
                        if (i == -1)
                        {
                            logger.Info($"Invalid settings at line {lineNum}");
                            throw new ArgumentException("Invalid settings.");
                        }
                        string val = line.Substring(i + 1);

                        values.Add(line.Substring(0, i),
                            val.Equals("null", StringComparison.InvariantCultureIgnoreCase) ? null : val);
                        lineNum++;
                    }
                    logger.Info("Settings loaded.");
                }
            else
                logger.Info("Settings not found.");
        }

        public void Dispose()
        {
            try
            {
                logger.Info($"Saving settings for \"{id}\"...");
                using (StreamWriter writer = new StreamWriter(File.OpenWrite(cfgFile)))
                    foreach (KeyValuePair<string, string> i in values)
                        writer.WriteLine("{0}:{1}", i.Key, i.Value == null ? "null" : i.Value);
            }
            catch (Exception e)
            {
                logger.Error("Error when saving settings.", e);
            }
        }

        public string GetValue(string key, string def = null)
        {
            string ret;
            if (!values.TryGetValue(key, out ret))
            {
                if (def == null)
                {
                    logger.Error($"Attempt to access nonexistant settings \"{key}\".");
                    throw new ArgumentException($"\"{key}\" does not exist in settings.");
                }
                ret = values[key] = def;
            }
            return ret;
        }

        public T GetValue<T>(string key, string def = null)
        {
            string ret;
            if (!values.TryGetValue(key, out ret))
            {
                if (def == null)
                {
                    logger.Error($"Attempt to access nonexistant settings '{key}'.");
                    throw new ArgumentException($"\"{key}\" does not exist in settings.");
                }
                ret = values[key] = def;
            }
            return (T)Convert.ChangeType(ret, typeof(T));
        }

        public void SetValue(string key, string val)
        {
            values[key] = val;
        }
    }
}
