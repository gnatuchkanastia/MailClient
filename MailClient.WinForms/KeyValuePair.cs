using System;
using System.Xml.Serialization;

namespace MailClient.WinForms
{
    [Serializable]
    [XmlType(TypeName = "KeyValue")]
    public struct KeyValuePair<K, V>
    {
        public K Key
        { get; set; }

        public V Value
        { get; set; }
    }
}