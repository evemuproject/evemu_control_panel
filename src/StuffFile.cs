using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StuffArchiver
{
    public delegate void FileProcessedHandler(string message, int processed, int total);

    public struct StuffFileEntry
    {
        public int Length; // File Length
        public string Path; // File Path (relative)
    }

    public class StuffFile
    {
        // The events.  Your GUI should hook into them.
        public event FileProcessedHandler FileExtracted;
        public event FileProcessedHandler FileArchived;

        // This is where files end up when we ReadDirectory.
        private List<string> _Files;

        public void Extract(string openfile, string savedir)
        {
            // Condition savedir.
            savedir = savedir.TrimEnd('\\') + "\\";

            // Initialize the input streams.
            FileStream fsi = new FileStream(openfile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fsi);

            // Read the table of contents.
            StuffFileEntry[] toc = new StuffFileEntry[br.ReadInt32()];
            int length_temp;
            for (int i = 0; i < toc.Length; i++)
            {
                toc[i].Length = br.ReadInt32();
                length_temp = br.ReadInt32();
                toc[i].Path = Encoding.ASCII.GetString(br.ReadBytes(length_temp));
                fsi.Position++;
            }

            // Reserve memory for the handler.
            FileProcessedHandler handle;

            // Start reading from our archive and writing it to individual files.
            int BUFFER_SIZE = 4096;
            byte[] buffer = new byte[BUFFER_SIZE];
            int toread = 0;
            for (int i = 0; i < toc.Length; i++)
            {
                toread = toc[i].Length;
                FileInfo fi = new FileInfo(savedir + toc[i].Path);
                Directory.CreateDirectory(fi.DirectoryName); // Make sure the directory exists first.
                FileStream fso = new FileStream(fi.FullName, FileMode.Create, FileAccess.Write);
                while (toread > 0)
                {
                    if (toread > BUFFER_SIZE)
                    {
                        buffer = br.ReadBytes(BUFFER_SIZE); // Fill the buffer.
                        toread -= BUFFER_SIZE;
                    }
                    else
                    {
                        buffer = br.ReadBytes(toread); // Read what's left.
                        toread -= toread;
                    }
                    fso.Write(buffer, 0, buffer.Length); // Write the data to the file.
                    fso.Flush();
                }
                fso.Close();

                #region " Raise Event "
                handle = FileExtracted;
                if (handle != null)
                    handle("Extracting...", i + 1, toc.Length);
                #endregion
            }

            // Close up shop.
            br.Close();
            fsi.Close();
        }
        public void Archive(string opendir, string savefile)
        {
            // Initialize the list then comb the directory for files.
            _Files = new List<string>();
            ReadDirectory(opendir);

            // The "\\" is actually not important--it is there to make sure the substring takes it off.
            opendir = opendir.TrimEnd('\\') + "\\";

            // Reserve memory for the handle.
            FileProcessedHandler handle;

            #region " Raise Event "
            handle = FileArchived;
            if (handle != null)
                handle("Reindexing...", -1, -1);
            #endregion

            // Prepare to write the file by making sure the directory exists and the file name is good.
            FileInfo fi_save = new FileInfo(savefile);
            Directory.CreateDirectory(fi_save.DirectoryName);
            FileStream fso = new FileStream(fi_save.FullName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fso);
            bw.Write(_Files.Count);  // Write the number of files.

            // Write the table of contents.
            string temp;
            for (int i = 0; i < _Files.Count; i++)
            {
                FileInfo fi = new FileInfo(_Files[i]);
                bw.Write(Convert.ToInt32(fi.Length));
                temp = fi.FullName.Substring(opendir.Length); // This is where we make the path relative again.
                bw.Write(temp.Length);
                bw.Write(Encoding.ASCII.GetBytes(temp + "\0")); // Remember, it is null terminated, hence the \0.
            }

            // Read the individual files and write them to the output file.
            byte[] buffer = new byte[4096];
            int read = 0;
            for (int i = 0; i < _Files.Count; i++)
            {
                FileStream fsi = new FileStream(_Files[i], FileMode.Open, FileAccess.Read);
                while (fsi.Position != fsi.Length)
                {

                    read = fsi.Read(buffer, 0, buffer.Length);
                    bw.Write(buffer, 0, read);
                    bw.Flush();
                }
                fsi.Close();

                #region " Raise Event "
                handle = FileArchived;
                if (handle != null)
                    handle("Archiving...", i + 1, _Files.Count);
                #endregion
            }

            // No idea what this is about but it makes the number of bytes match so here it is:
            // ....EmbedFs 1.0.
            bw.Write(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x45, 0x6D, 0x62, 0x65, 0x64, 0x46, 0x73, 0x20, 0x31, 0x2E, 0x30, 0x00 });
            bw.Flush();

            // Close up shop.
            bw.Close();
            fso.Close();
        }
        private void ReadDirectory(string directory)
        {
            foreach (string file in Directory.GetFiles(directory))
                _Files.Add(file);

            foreach (string dir in Directory.GetDirectories(directory))
                ReadDirectory(dir);
        }

        private void Reindex(string indexfile, string trim)
        {
            // Read index file.
            FileStream fs_index = new FileStream(indexfile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs_index);

            int length;
            string[] index = new string[br.ReadInt32()];
            for (int i = 0; i < index.Length; i++)
            {
                length = br.ReadInt32();
                index[i] = Encoding.ASCII.GetString(br.ReadBytes(length));
            }

            br.Close();
            fs_index.Close();


            // Insert indexed into prescribed slots.
            string[] indexed = new string[_Files.Count];
            for (int i = 0; i < index.Length; i++)
            {
                if (i < indexed.Length)
                {
                    int idx = IndexOf(trim + index[i]);
                    if (idx != -1)
                    {
                        indexed[i] = _Files[idx];
                        _Files.RemoveAt(idx);
                    }
                }
            }

            // Fill in holes.
            for (int i = 0; i < indexed.Length; i++)
            {
                if (indexed[i] == null)
                {
                    indexed[i] = _Files[0];
                    _Files.RemoveAt(0);
                }
            }

            // Restore files.
            _Files.Clear();
            for (int i = 0; i < indexed.Length; i++)
                _Files.Add(indexed[i]);
        }
        private int IndexOf(string name)
        {
            name = name.ToLower();
            for (int i = 0; i < _Files.Count; i++)
            {
                if (_Files[i].ToLower() == name)
                    return i;
            }
            return -1;
        }
    }
}