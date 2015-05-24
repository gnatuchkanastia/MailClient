using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MailClient.WinForms
{
    internal class PluginManager
    {
        public static readonly string ConfigFile = @"plugins.config";
        public static List<String> PluginFiles = new List<string>();
        public static List<IPlugin> PluginInstances = new List<IPlugin>();

        public static void UpdatePluginListView(ListView view)
        {
            view.Items.Clear();
            foreach (var file in PluginFiles)
                view.Items.Add(Path.GetFileName(file));
        }
        public static void LoadNewPlugin(string path)
        {
            if (PluginFiles.Contains(path))
                throw new InvalidOperationException("This plugin is already loaded");
            var types = new List<Type>();
            LoadPluginTypes(path, types);
            foreach (var t in types)
                InstantiatePluginType(t);
            PluginFiles.Add(path);
        }
        public static void InitializePlugins()
        {
            if (!File.Exists(Path.Combine(Application.StartupPath, ConfigFile)))
                return;
            LoadPluginList();
            LoadPluginInstances();
        }
        private static void LoadPluginInstances()
        {
            var types = new List<Type>();
            foreach (var path in PluginFiles)
                LoadPluginTypes(path, types);
            InstanciateTypes(types);
        }

        private static void InstanciateTypes(List<Type> types)
        {
            foreach (var type in types)
                InstantiatePluginType(type);
        }

        private static void InstantiatePluginType(Type type)
        {
            var instance = Activator.CreateInstance(type) as IPlugin;
            PluginInstances.Add(instance);
        }
        private static void LoadPluginTypes(string path, List<Type> types)
        {
            var assembly = Assembly.LoadFile(path);
            foreach (var type in assembly.GetTypes())
                if (type.IsAbstract || type.IsInterface || !typeof (IPlugin).IsAssignableFrom(type))
                    continue;
                else
                    types.Add(type);
        }
        private static void LoadPluginList()
        {
            using (var fs = new FileStream(ConfigFile, FileMode.Open))
            {
                var ser = new XmlSerializer(typeof (List<String>));
                var obj = ser.Deserialize(fs);
                PluginFiles = obj as List<String>;
            }
        }
        public static void SaveOnExit()
        {
            using (var fs = new FileStream(ConfigFile, FileMode.Create))
            {
                var ser = new XmlSerializer(typeof (List<String>));
                ser.Serialize(fs, PluginFiles);
            }
        }
    }
}