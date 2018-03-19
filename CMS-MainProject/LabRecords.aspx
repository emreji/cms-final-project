<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LabRecords.aspx.cs" Inherits="CMS_MainProject.LabRecords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="X-UA-Compatible" content="IE=10" />
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lbl_err_message" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lbl_success_message" runat="server"></asp:Label>
            <br />
            Select Patient: <asp:DropDownList runat="server" ID="patient_dropdown"></asp:DropDownList>
            <br />
            <br />
            Select Doctor: <asp:DropDownList runat="server" ID="doctor_dropdown"></asp:DropDownList>
            <br />
            <br />
            Test : <asp:TextBox ID="txt_test_name" runat="server"></asp:TextBox>
            <br />
            <br />
            Test Date : <asp:Label ID="lbl_test_date" runat="server"></asp:Label>
            <br />
            <br />
                
        </div>
        <asp:Button ID="btn_add_lab_patient" runat="server" Text="Add Patient" OnClick="btn_add_lab_patient_Click" />
        <asp:Button ID="btn_update_patient" runat="server" Text="Update Patient" OnClick="btn_update_patient_Click" />
        <asp:Button ID="btn_delete_patient" runat="server" Text="Delete Patient" OnClick="btn_delete_patient_Click" />
        <asp:Button ID="btn_display_patients" runat="server" Text="Display Patients" OnClick="btn_display_patients_Click" />
    </form>
    <ul id="patients_lab_list" runat="server"></ul>
</body>
</html>
