using System;
using System.IO;

namespace ProjetBanqueV2
{
    public class FichierSortie
    {
        private string _path;
        private FileMode _mode;
        private FileStream _fs;
        private StreamWriter _sw;

        public FichierSortie(string path, FileMode mode)
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
            _fs = new FileStream(_path, _mode, FileAccess.Write);
            _sw = new StreamWriter(_fs);
        }

        public void Close()
        {
            _sw.Close();
        }

        public void WriteLine(string line)
        {
            _sw.WriteLine(line);
        }
    }
}
