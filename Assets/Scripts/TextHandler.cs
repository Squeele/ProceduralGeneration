using System.Collections;
using System.Collections.Generic;
using System.IO;
public class TextHandler  {
    public string path;
    public void WriteString(string str)
    {
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(str);
        writer.Close();
    }
    public TextHandler(string _path)
    {
        path = _path;
    }
}
