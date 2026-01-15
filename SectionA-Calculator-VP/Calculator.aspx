<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calculator.aspx.cs" Inherits="SectionA_Calculator_VP.Calculator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calculator</title>
    <style>
        #Image1 {
            filter: brightness(1);
            transition: filter 0.3s ease;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Panel ID="CalcPanel" runat="server" style="z-index: 1; left: 37px; top: 70px; position: absolute; height: 532px; width: 413px" BorderColor="Black" BorderStyle="Double">
            <asp:Label ID="lblEquation" runat="server" Text="0" Font-Size="XX-Large"
    style="z-index: 1; left: 6px; top: 11px; position: absolute; height: 83px; width: 362px; text-align: right; display: block; padding-right: 8px;"
    BorderStyle="Groove"></asp:Label>
            <asp:Label ID="lblResult" runat="server" BorderStyle="Groove" 
    style="z-index: 1; left: 9px; top: 127px; position: absolute; height: 40px; width: 361px; 
           text-align: right; display: block; padding-right: 8px; font-size: 40px; font-weight: bold;" 
    Text="Result" ToolTip="Output/ Result"></asp:Label>
            <asp:Button ID="Button2" runat="server" CssClass="btn-clear" Font-Bold="True" Font-Size="XX-Large" OnClick="AllClear_Click" style="z-index: 1; left: 11px; top: 200px; position: absolute; height: 57px; width: 195px" Text="AC" />
            <asp:Button ID="Button3" runat="server" CssClass="btn-clear" Font-Bold="True" Font-Size="XX-Large" OnClick="Clear_Click" style="z-index: 1; left: 213px; top: 201px; position: absolute; height: 55px; width: 92px" Text="C" />
            <asp:Button ID="Button4" runat="server" CssClass="btn-operator" Font-Bold="True" Font-Size="XX-Large" OnClick="Operator_Click" style="z-index: 1; left: 313px; top: 329px; position: absolute; height: 55px; width: 92px" Text="*" />
            <asp:Button ID="Button5" runat="server" CssClass="btn-operator" Font-Bold="True" Font-Size="XX-Large" OnClick="Operator_Click" style="z-index: 1; left: 312px; top: 199px; position: absolute; height: 55px; width: 92px" Text="+" />
            <asp:Button ID="Button6" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" OnClick="Number_CLick" style="z-index: 1; left: 214px; top: 264px; position: absolute; height: 55px; width: 92px" Text="9" />
            <asp:Button ID="Button7" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" OnClick="Number_CLick" style="z-index: 1; left: 113px; top: 264px; position: absolute; height: 55px; width: 92px; right: 230px;" Text="8" />
            <asp:Button ID="Button10" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" OnClick="Number_CLick" style="z-index: 1; left: 115px; top: 329px; position: absolute; height: 55px; width: 92px; right: 228px;" Text="5" />
            <asp:Button ID="Button9" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" OnClick="Number_CLick" style="z-index: 1; left: 13px; top: 265px; position: absolute; height: 55px; width: 92px; right: 330px;" Text="7" />
            <asp:Button ID="Button11" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" OnClick="Number_CLick" style="z-index: 1; left: 15px; top: 330px; position: absolute; height: 55px; width: 92px; right: 328px;" Text="4" />
            <asp:Button ID="Button12" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" OnClick="Number_CLick" style="z-index: 1; left: 214px; top: 329px; position: absolute; height: 55px; width: 92px; right: 129px;" Text="6" />
            <asp:Button ID="Button13" runat="server" CssClass="btn-operator" Font-Bold="True" Font-Size="XX-Large" OnClick="Operator_Click" style="z-index: 1; left: 313px; top: 264px; position: absolute; height: 55px; width: 92px" Text="-" />
            <asp:Button ID="Button14" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" OnClick="Number_CLick" style="z-index: 1; left: 15px; top: 396px; position: absolute; height: 55px; width: 92px; right: 328px;" Text="1" />
            <asp:Button ID="Button15" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" OnClick="Number_CLick" style="z-index: 1; left: 116px; top: 395px; position: absolute; height: 55px; width: 92px; right: 227px;" Text="2" />
            <asp:Button ID="Button16" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" OnClick="Number_CLick" style="z-index: 1; left: 214px; top: 394px; position: absolute; height: 55px; width: 92px; right: 129px;" Text="3" />
            <asp:Button ID="Button17" runat="server" CssClass="btn-equal" Font-Bold="True" Font-Size="XX-Large" OnClick="EqualTo_Click" style="z-index: 1; left: 312px; top: 459px; position: absolute; height: 55px; width: 92px" Text="=" />
            <asp:Button ID="Button18" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" OnClick="Decimal_Click" style="z-index: 1; left: 214px; top: 460px; position: absolute; height: 55px; width: 92px; right: 129px;" Text="." />
            <asp:Button ID="Button19" runat="server" CssClass="btn-operator" Font-Bold="True" Font-Size="XX-Large" OnClick="Operator_Click" style="z-index: 1; left: 313px; top: 393px; position: absolute; height: 55px; width: 92px" Text="/" />
            <asp:Button ID="Button1" runat="server" CssClass="btn-number" Font-Bold="True" Font-Size="XX-Large" style="z-index: 1; left: 14px; top: 460px; position: absolute; height: 57px; width: 192px" Text="0" OnClick="Number_CLick" />
        </asp:Panel>
       
        <p>
            <asp:Button ID="btnLoadTheme" runat="server" OnClick="btnLoadTheme_Click" Text="LoadTheme" />
        </p>
        <p>
            &nbsp;</p>
        <asp:DropDownList ID="drpTheme" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" style="z-index: 1; left: 493px; top: 133px; position: absolute; height: 27px; width: 116px">
        </asp:DropDownList>
        <p>
            <asp:Image ID="Image1" runat="server" style="z-index: 2; left: 487px; top: 177px; position: absolute; height: 170px; width: 278px" />
        </p>
       
    </form>
</body>
</html>
