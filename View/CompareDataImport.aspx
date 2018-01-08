<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompareDataImport.aspx.cs" Inherits="MatrixTool.View.CompareDataImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="../../../Styles/base-ET.css" rel="Stylesheet" />
    <link type="text/css" href="../../../Style/StyleSheet.css" rel="Stylesheet" />
    <script src="../../../js/EasyTraining/jquery-1.11.2.min.js" type="text/javascript"></script>
     <script type="text/javascript" src="../../../js/EasyTraining/pageLoad.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="cont" id="printdiv">
           <div class="position">
               <asp:Label ID="labTitle" runat="server" Text="对比数据导入/Compare Data Import"></asp:Label>
           </div>
            <table class="tab_table03">
           <tr>
            <td style="width:80%">
              <span class="SunFont"> 请在导入数据前清空所有对比数据</span><br/>
               <span class="VerdFont">Clear Data before import</span> 
            </td>
             <td>
                   <asp:Button ID="btn_Clear" runat="server" CssClass="in_button"   Width="60px"  CausesValidation="False"  Text="Clear" onclick="btn_Clear_Click" /> 
                      <asp:Button ID="ID_Back" class="in_button" runat="server"    Width="60px" Text="Back" onclick="ID_Back_Click"  />
              </td>
            </tr>
  
          </table>
        <div class="line4">
            <table class="tab_table03">
           <tr>
             <td style="width:20%" >
               <span class="SunFont"> 培训计划列表导入</span><br/>
               <span class="VerdFont">Training Plan List Import</span> 
             </td>
             <td style="width:60%">
                  <asp:FileUpload ID="FileUpload1" runat="server"  CssClass="txtBox"   
                      Width="87%"/>
              </td>
                  <td style="width:20%">
                   <asp:Button ID="Btn_Import" runat="server" CssClass="in_button"  Width="60px" 
                          CausesValidation="False"  Text="Import" onclick="Btn_upload_Click" OnClientClick="parent.loaddatainprocess();" /> <%--OnClientClick="parent.loaddatainprocess();"--%>
                          &nbsp;&nbsp;
                          <asp:Label  ID="LabTrainingPLan" runat="server"></asp:Label>
                   </td>
            </tr>
           <tr>
             <td  style="width:20%">
               <span class="SunFont"> 讲师表导入</span><br/>
               <span class="VerdFont">Trainer List Import</span> 
             </td>
             <td style="width:60%">
                  <asp:FileUpload ID="FileUpload2" runat="server"  CssClass="txtBox"   
                      Width="87%"/>
                  </td>
                  <td style="width:20%">
                   <asp:Button ID="BtnTrainer" runat="server" CssClass="in_button"  Width="60px" 
                          CausesValidation="False"  Text="Import" onclick="BtnTrainer_Click" OnClientClick="parent.loaddatainprocess();"/>
                    &nbsp;&nbsp;
                          <asp:Label  ID="LabTrainer" runat="server"></asp:Label>
                   </td>
            </tr>
           <tr>
             <td  style="width:20%">
               <span class="SunFont"> 课程和SOP表导入</span><br/>
               <span class="VerdFont">Course and SOP  List Import</span> 
             </td>
             <td style="width:60%">
                  <asp:FileUpload ID="FileUpload3" runat="server"  CssClass="txtBox"   
                      Width="87%"/>
                  </td>
                  <td style="width:20%">
                   <asp:Button ID="Button1" runat="server" CssClass="in_button"   Width="60px"
                          CausesValidation="False"  Text="Import" onclick="BtnCourse_Click" OnClientClick="parent.loaddatainprocess();"/>
                    &nbsp;&nbsp;
                          <asp:Label  ID="LabCourseSOP" runat="server"></asp:Label>
                   </td>
            </tr>
           <tr>
             <td  style="width:20%">
               <span class="SunFont"> 关系表导入</span><br/>
               <span class="VerdFont">Relationship List Import</span> 
             </td>
             <td style="width:60%">
                  <asp:FileUpload ID="FileUpload4" runat="server"  CssClass="txtBox"   
                      Width="87%"/>
                  </td>
                  <td style="width:20%">
                   <asp:Button ID="Button2" runat="server" CssClass="in_button"  Width="60px"
                          CausesValidation="False"  Text="Import" onclick="BtnRelation_Click" OnClientClick="parent.loaddatainprocess();"/>
                    &nbsp;&nbsp;
                          <asp:Label  ID="LabRelation" runat="server"></asp:Label>
                   </td>
            </tr>
            </table>
             <textarea id="ErrorReport" runat="server" style="width:100%;display:none"> </textarea>
         </div>
         <div class="position">
               <asp:Label ID="Label1" runat="server" Text="数据对比/Compare Data"></asp:Label>
           </div>
           <div class="line4">
            <table class="tab_table06" style="width:100%">
           <tr>
             <td style="width:80%;text-align:center" >
               <span class="SunFont"> 培训计划数据对比</span><br/>
               <span class="VerdFont">Training Plan </span> 
             </td>
             <td style="width:10%; text-align:center">
                  <asp:Button ID="Btn_Compare" runat="server" CssClass="in_button"   Width="80px"
                          CausesValidation="False"  Text="Compare" onclick="Btn_CompareTrainingPlan_Click" OnClientClick="parent.loaddatainprocess();"/>
              </td>
              <td style="width:10%;text-align:center">
                  <asp:Button ID="Btn_Export" runat="server" CssClass="in_button"   Width="60px"
                          CausesValidation="False"  Text="Export" onclick="Btn_Export_Click"/>
                          <asp:Label ID="LabForTrainingPlan" runat="server" style="display:none"></asp:Label>
              </td>
            </tr>
           <tr>
             <td style="text-align:center" >
               <span class="SunFont"> 讲师对比</span><br/>
               <span class="VerdFont">Trainer </span> 
             </td>
              <td style="text-align:center">
                  <asp:Button ID="Button4" runat="server" CssClass="in_button"   Width="80px"
                          CausesValidation="False"  Text="Compare" onclick="Btn_CompareTrainer_Click" OnClientClick="parent.loaddatainprocess();"/>
              </td>
              <td style="text-align:center">
                  <asp:Button ID="Button3" runat="server" CssClass="in_button"  Width="60px" 
                          CausesValidation="False"  Text="Export" onclick="btn_Trainer_Click" />
                          <asp:Label ID="LabForTrainer" runat="server" style="display:none"></asp:Label>
              </td>
            </tr>
           <tr>
             <td style="text-align:center" >
               <span class="SunFont"> 课程和SOP对比</span><br/>
               <span class="VerdFont">Course and SOP</span> 
             </td>
             <td style="text-align:center">
                  <asp:Button ID="Button5" runat="server" CssClass="in_button"   Width="80px"
                          CausesValidation="False"  Text="Compare" onclick="Btn_CompareCourseAndSOP_Click" OnClientClick="parent.loaddatainprocess();"/>
              </td>
              <td style="text-align:center">
                  <asp:Button ID="CourseExport" runat="server" CssClass="in_button"  Width="60px" 
                          CausesValidation="False"  Text="Export" onclick="btn_CourseAndSOPExport_Click" />
                         
             </td>
             </tr>
     

            <tr>
             <td style="text-align:center" >
               <span class="SunFont"> 工作组、Module、课程和关系对比</span><br/>
               <span class="VerdFont">JobFunction、Module、Course and Relation </span> 
             </td>
             <td style="text-align:center">
                  <asp:Button ID="Button6" runat="server" CssClass="in_button"   Width="80px"
                          CausesValidation="False"  Text="Compare" onclick="Btn_CompareCourse2Module_Click" OnClientClick="parent.loaddatainprocess();"/>
              </td>
              <td style="text-align:center">
                  <asp:Button ID="Button13" runat="server" CssClass="in_button" Width="60px"   
                          CausesValidation="False"  Text="Export" onclick="btn_Course2ModuleExport_Click" />
              </td>
            </tr>
  
          </table>
       </div>
    </div>
    </form>
</body>
</html>
