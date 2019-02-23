using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.AbnormityFrame;

namespace U8科目末级标志修复
{
    public partial class 科目末级标志修复 : AbnormityForm
    {
        string exSqlkn = @"
            -- 修复科目末级标志 用于U8 10.0以上版本
            -- @iyear 用于要修复的年度 
            use {0}
            begin tran

            declare @iyear varchar(8)
            set @iyear= '{1}'

            declare mycode cursor for select ccode from code where iyear =@iyear
            declare @code varchar(250)
            open mycode
            fetch next from mycode into @code

            while @@fetch_status=0
                begin
                if exists(select 1 from code where ccode<>@code and ccode like @code+'%')
                   update code set bend=0 where ccode=@code and bend=1  and iyear =@iyear
                   else
                   update code set bend=1 where ccode=@code and bend=0  and iyear =@iyear
               fetch next from mycode into @code
               end
            close mycode

            deallocate mycode
            commit tran
            --rollback tran
            ";
        string exSql = @"
-- 修复科目末级标志  用于要修复的年度 
  use {0}
    begin tran
    declare mycode cursor for select ccode from code 
    declare @code varchar(250)
    open mycode
    fetch next from mycode into @code
    while @@fetch_status=0
        begin
        if exists(select 1 from code where ccode<>@code and ccode like @code+'%')
           update code set bend=0 where ccode=@code and bend=1  
           else
           update code set bend=1 where ccode=@code and bend=0 
       fetch next from mycode into @code
       end
    close mycode
    deallocate mycode
    commit tran
    --rollback tran
";
        HongHu.UI.SelcetcAcc ScAccForm;
        public 科目末级标志修复()
        {
            InitializeComponent();
           HongHu.UI.ClassLong.SetClassLong(this.Handle);
           }
      
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           new  HongHu.UI.SetConnString().ShowDialog();
           if (HongHu.SetString.SQLConn != "")//已设置
           {
               linkLabel1.LinkColor = System.Drawing.Color.FromArgb(25, 153, 0);
               linkLabel2.Enabled = true;
           }
           else//未设置
           {
               linkLabel1.LinkColor = System.Drawing.Color.Gray;
               linkLabel2.Enabled = false;
           }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ScAccForm = new HongHu.UI.SelcetcAcc();
            ScAccForm.ShowDialog();
            if (ScAccForm.cAcc_Id != "")//已设置
            {
                linkLabel2.LinkColor = System.Drawing.Color.FromArgb(25, 153, 0);
                linkLabel3.Enabled = true;
            }
            else//未设置
            {
                linkLabel2.LinkColor = System.Drawing.Color.Gray;
                linkLabel3.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //开始执行修复
            if (ScAccForm.cAcc_hiYear)
            {
                HongHu.DBUtility.SqlHelper.ExecuteNonQuery(
                    string.Format(exSqlkn, ScAccForm.cAcc_Database, ScAccForm.cAcc_iYear));
            }
            else
            {
                HongHu.DBUtility.SqlHelper.ExecuteNonQuery(
                              string.Format(exSql, ScAccForm.cAcc_Database));
            }
            MessageBox.Show("已修复,请检查");
        }
    }
}
