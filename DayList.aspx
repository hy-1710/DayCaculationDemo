<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DayList.aspx.cs" Inherits="DayCaculationDemo.DayList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .template {
            height: 200px;
            overflow-y: scroll;
        }
    </style>

    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:Calendar ID="startDate" runat="server" SelectionMode="DayWeekMonth"></asp:Calendar>
            </div>
            <div class="col-md-6">
                <asp:Calendar ID="endDate" runat="server" SelectionMode="DayWeekMonth"></asp:Calendar>
            </div>
        </div>

        <br />
        <div class="row">
            <div class="text-center">
                <asp:DropDownList ID="ddlDays" runat="server">
                    <asp:ListItem Text="Sunday" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                    <asp:ListItem Text="friDay" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>
        <br />
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:RadioButton ID="rbAlter" runat="server" Text="Alternet Days" GroupName="check" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rbWorking" runat="server" Text="Working Days" GroupName="check" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rbFirstandLast" runat="server" Text="First and Last" GroupName="check" />
            </div>
        </div>
        <br />

        <asp:Label ID="lblTotalWorkingDay" runat="server" CssClass="text-center" />
        <br />

        <div class="row text-center">
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
        </div>
        <br />
        <div class="row">
            <div class="col-md-6">
                <div class="template" >
                    <asp:Repeater ID="dayslist" runat="server">
                        <HeaderTemplate>
                            <th>Alternate Days</th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDay" runat="server" Text='<%#Eval("AlternateDate") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-md-6">
                <div class="template" >
                    <asp:Repeater ID="rptfstLstDay" runat="server">
                        <HeaderTemplate>
                            <th>First & Last Days</th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDay" runat="server" Text='<%#Eval("FirstLastDate", "{0:dd-MM-yyyy}") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>



    </form>

    <script src="Scripts/bootstrap.min.js"></script>
</body>

</html>
