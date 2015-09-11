using System;


public class InfoBox
    {
        public static string Text;
        public static void Info(string s)
        {
            Text += s + "\n";
            int l = Text.IndexOf('\n');
            if (l != -1) Text = Text.Remove(0, l+1);
        }
    }

