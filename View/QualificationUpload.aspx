<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QualificationUpload.aspx.cs" Inherits="MatrixTool.View.QualificationUpload" %>

<%@ Register Src="../Common/matrixTop.ascx" TagName="top1" TagPrefix="uc2" %>
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
            <td style="width:80%">
              <span class="SunFont"> 请在导入数据前清空所有软能力数据</span><br/>
               <span class="VerdFont">Clear Data before import</span> 
            </td>
             <td >
                      <asp:Button ID="ID_Back" class="in_button" runat="server" Text="Back" onclick="ID_Back_Click" Width="80px"  />
                   </td>
            </tr>
  
          </table>
         </div>

        <div class="position">
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
                      <asp:Button ID="Button1" runat="server" CssClass="in_button"  
                      CausesValidation="False"  Text="Clear" onclick="Button1_Click" Width="80px" /> &nbsp;&nbsp;&nbsp;
                   <asp:Button ID="Btn_Import" runat="server" CssClass="in_button"  
                          CausesValidation="False"  Text="Import" onclick="Btn_upload_Click" OnClientClick="parent.loaddatainprocess();" Width="80px"/> <%--OnClientClick="parent.loaddatainprocess();"--%>
                   </td>
            </tr>
  
          </table>
         </div>

          <div class="position">
            <table class="tab_table03">
           <tr>
             <td  style="width:20%">
               <span class="SunFont"> 培训登记表导入</span><br/>
               <span class="VerdFont">Training Roster List Import</span> 
             </td>
             <td style="width:60%">
                  <asp:FileUpload ID="FileUpload2" runat="server"  CssClass="txtBox"   
                      Width="87%"/>
                  </td>
                  <td style="width:20%">
                   <asp:Button ID="BtnRoster" runat="server" CssClass="in_button"  
                          CausesValidation="False"  Text="Import" onclick="BtnRoster_Click" OnClientClick="parent.loaddatainprocess();" Width="80px"/>
                   </td>
            </tr>
  
          </table>
         </div>

          <br />
          <br />
          <br />


<div class="position" style="margin-top:15px">
            <table class="tab_table03">
           <tr>
             <td  style="width:20%">
               <span class="SunFont"> 员工报告导入</span><br/>
               <span class="VerdFont">Employee by Supervisor Report Import</span> 
             </td>
             <td style="width:60%">
                  <asp:FileUpload ID="FileUpload3" runat="server"  CssClass="txtBox"   
                      Width="87%"/>
                  </td>
                  <td style="width:20%">
                  &nbsp;<asp:Button ID="Button2" runat="server" CssClass="in_button"  
                          CausesValidation="False"  Text="Import" onclick="BtnRoster1_Click" Width="80px"/>
                      <asp:Button ID="UpdateMissingReport" CssClass="in_button"  
                          CausesValidation="False" 
                              runat="server" Text="更新MissingReport" 
                          onclick="UpdateMissingReport_Click" Width="150px"/>
                      </td>
            </tr>
  
          </table>
         </div>

         <div class="position">
            <table class="tab_table03">
           <tr>
             <td  style="width:20%">
               <span class="SunFont">ITP报告导入</span><br/>
               <span class="VerdFont">ITP Report Import</span> 
             </td>
             <td style="width:60%">
                  <asp:FileUpload ID="FileSTJboard" runat="server"  CssClass="txtBox"   
                      Width="87%"/>
                  </td>
                  <td style="width:20%">
                  &nbsp;<asp:Button ID="BtnSTJboardImport" runat="server" CssClass="in_button"  
                          CausesValidation="False"  Text="Import" onclick="BtnSTJboardInput_Click" Width="80px" />
                      </td>
            </tr>
  
          </table>
         </div>

           <div class="line4">
                <asp:GridView ID="GridView1" runat="server"  Width="100%"  AutoGenerateColumns="False"
                PageSize="20"    EmptyDataText="No Data！" CssClass="tablestyle" 
                    DataKeyNames="ID" > 
                <AlternatingRowStyle CssClass="altrowstyleList" />
                <HeaderStyle CssClass="headerstyleList" /> <%--Wrap="False" HorizontalAlign="Center" --%>
                <RowStyle  CssClass="rowstyleList" />  <%--Height="32px"  BorderWidth="1px"  BorderStyle="Solid" --%>
               <%-- <PagerStyle Font-Size="14px" />--%>
                <EmptyDataRowStyle CssClass="td2" />
                <Columns>
                      <asp:TemplateField HeaderText="ID" Visible="False">
                             <ItemTemplate>
                                 <asp:Label runat="server" id="Label1" Text='<%#Eval("ID") %>'> </asp:Label>
                             </ItemTemplate>
                             <EditItemTemplate>
                                  <asp:Label runat="server" id="Label1" Text='<%#Eval("ID") %>'> </asp:Label>
                             </EditItemTemplate>
                            
                      </asp:TemplateField>

                     <asp:TemplateField HeaderText="序号/No." HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <ItemStyle  Width="15%" />
                    </asp:TemplateField>

                   <asp:BoundField DataField="SOPNo" HeaderText="SOP号/SOP Number" SortExpression="CourseCode"  >
                        <ItemStyle  Wrap="False" Width="15%"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="descriptipn" HeaderText="描述/Description" SortExpression="userID"  >
                        <ItemStyle Wrap="true" Width="35%"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="initial" HeaderText="负责人/Owner" SortExpression="type"  >
                        <ItemStyle  Wrap="False" Width="20%"/>
                    </asp:BoundField>

                   
                </Columns>
            </asp:GridView>
               <textarea id="ErrorReport" runat="server" style="width:100%;display:none">
               
            </textarea>
        </div>
    </div>
    </form>
</body>
</html>
