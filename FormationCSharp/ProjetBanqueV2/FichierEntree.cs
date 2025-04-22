using System;
using System.IO;

namespace ProjetBanqueV2
{
    public class FichierEntree
    {
        private string _path;
        private FileMode _mode;
        private FileStream _fs;
        private StreamReader _sr;

        public FichierEntree(string path, FileMode mode)
        {
            this._path = path;
            this._mode = mode;
        }

        public string Path
        {
            get => _path;
            set => _path = value;
        }

        public FileMode Mode
        { 
            get => _mode;
            set => _mode = value;
        }

        public void Open()
        {
            _fs = new FileStream(_path, _mode, FileAccess.Read);
            _sr = new StreamReader(_fs);
        }

        public void Close()
        {
            _sr.Close();
        }

        public string ReadLine()
        {
            return _sr.ReadLine();
        }

        public bool EndOfStream()
        {
            return _sr.EndOfStream;
        }
    }
}
