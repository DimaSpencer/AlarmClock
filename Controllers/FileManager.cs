using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WindowsFormsApp1.Model
{
    public class FileManager<T> where T : new() //тип который файл менеджер будет сереализовать и доставать
    {
        private readonly FileManagerConfiguration _configuration;
        private readonly XmlSerializer _serializer;
        private TextWriter _textWriter;
        private TextReader _textReader;

        public FileManager(string path) : this(new FileManagerConfiguration(path)) { }
        public FileManager(FileManagerConfiguration configuration)
        {
            #region CheckInputData
            if (configuration == null)
                throw new ArgumentNullException("Configuration is null", nameof(configuration));
            if (string.IsNullOrEmpty(configuration.FilePath))
                throw new ArgumentNullException("File path is empty or null", nameof(configuration.FilePath));
            #endregion

            _configuration = configuration;
            _serializer = new XmlSerializer(typeof(T));
        }

        public virtual void UpdateFile(T fileObject)
        {
            try
            {
                _textWriter = new StreamWriter(_configuration.FilePath);
                _serializer.Serialize(_textWriter, fileObject);
                _textWriter.Close();
            }
            catch (SerializationException ex)
            {
                throw new FileLoadException(ex.Message, _configuration.FilePath);
            }
        }

        public virtual T LoadObjectFromFile()
        {
            if(!File.Exists(_configuration.FilePath))
            {
                File.Create(_configuration.FilePath);
                return new T();
            }
            try
            {
                _textReader = new StreamReader(_configuration.FilePath);
                T xmlObject = (T)_serializer.Deserialize(_textReader);
                _textReader.Close();
                return xmlObject;
            }
            catch (SerializationException ex)
            {
                throw new FileLoadException(ex.Message, _configuration.FilePath);
            }
        }
    }
}
