using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace U8科目末级标志修复
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new 科目末级标志修复());
        }
    }
}
