using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MailClient.WinForms
{
    internal class PluginManager
    {
        public static readonly string ConfigFile = @"plugins.config";
        public static List<String> PluginFiles = new List<string>();
        public static List<IMessagePlugin> PluginInstances = new List<IMessagePlugin>();

        public static void UpdatePluginListView(ListView view)
        {
            view.Items.Clear();
            foreach (var file in PluginFiles)
            {
                var item = new ListViewItem(Path.GetFileName(file));
                view.Items.Add(item);
            }
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
            var instance = Activator.CreateInstance(type) as IMessagePlugin;
            PluginInstances.Add(instance);
        }
        private static void LoadPluginTypes(string path, List<Type> types)
        {
            var assembly = Assembly.LoadFile(path);
            foreach (var type in assembly.GetTypes())
                if (type.IsAbstract || type.IsInterface || !typeof (IMessagePlugin).IsAssignableFrom(type))
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

        public static void UnloadPlugin(string name)
        {
            var pathItem = PluginFiles.Single(p => p.Contains(name));
            PluginFiles.Remove(pathItem);
            SaveOnExit();
            UnloadAllPlugins();
            InitializePlugins();
        }

        private static void UnloadAllPlugins()
        {
            foreach(var obj in PluginInstances)
                obj.Dispose();
            PluginInstances.Clear();
        }
    }
}