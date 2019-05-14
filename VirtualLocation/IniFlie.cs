using System;
using System.IO;
using System.Text;

/// <summary>
/// IniFile 的摘要说明。
/// </summary>
public class IniFile
{

    public static string path;
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    public IniFile()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public IniFile(string INIPath)
    {
        path = INIPath;
    }
    public void IniWriteValue(string Section, string Key, string Value)
    {
        WritePrivateProfileString(Section, Key, Value, path);
    }
    public string IniReadValue(String Section, string Key)
    {
        StringBuilder temp = new StringBuilder(255);
        int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
        return temp.ToString();
    }
    public string IniReadValue(String Section, string Key, string Default)
    {
        StringBuilder temp = new StringBuilder(255);
        int i = GetPrivateProfileString(Section, Key, Default, temp, 255, path);
        return temp.ToString();
    }
}
