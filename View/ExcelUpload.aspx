<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelUpload.aspx.cs" Inherits="MatrixTool.View.ExcelUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Styles/base-ET.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="cont" id="printdiv">
       <div class="position">
            <table class="tab_table03">
           <tr>
             <td style="width:20%" >
               <span class="SunFont"> 列表导入</span><br/>
               <span class="VerdFont">List Import</span> 
             </td>
             <td style="width:60%">
                  <asp:FileUpload ID="FileUpload1" runat="server"  CssClass="txtBox"   
                      Width="87%"/>
              </td>
                  <td style="width:20%">
                   <asp:Button ID="Btn_Import" runat="server" CssClass="in_button"  
                          Text="Import" onclick="Btn_upload_Click" OnClientClick="parent.loaddatainprocess();" />  
                          <asp:Button ID="ID_Back" class="in_button" runat="server" Text="Back" onclick="ID_Back_Click"  Width="70px" />
                   </td>
            </tr>
  
          </table>
         </div>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    </div>
    </form>
</body>
</html>
